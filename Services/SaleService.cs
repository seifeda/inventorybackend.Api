using AutoMapper;
using inventorybackend.Api.DTOs.Sale;
using inventorybackend.Api.Interfaces.Repositories;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public SaleService(
            ISaleRepository saleRepository,
            IInventoryRepository inventoryRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {id} not found.");
            return sale;
        }

        public async Task<Sale> CreateSaleAsync(CreateSaleDto createSaleDto)
        {
            var sale = new Sale
            {
                SaleDate = createSaleDto.SaleDate,
                CustomerName = createSaleDto.CustomerName,
                CustomerEmail = createSaleDto.CustomerEmail,
                CustomerPhone = createSaleDto.CustomerPhone,
                PaymentMethod = createSaleDto.PaymentMethod,
                Notes = createSaleDto.Notes,
                Status = "Pending",
                SaleItems = new List<SaleItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in createSaleDto.SaleItems)
            {
                var inventoryItem = await _inventoryRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem == null)
                    throw new KeyNotFoundException($"Inventory item with ID {item.InventoryItemId} not found.");

                if (inventoryItem.Quantity < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for item {inventoryItem.Name}");

                var saleItem = new SaleItem
                {
                    InventoryItemId = item.InventoryItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.Quantity * item.UnitPrice,
                    Notes = item.Notes
                };

                sale.SaleItems.Add(saleItem);
                totalAmount += saleItem.TotalPrice;

                // Update inventory
                inventoryItem.Quantity -= item.Quantity;
                await _inventoryRepository.UpdateAsync(inventoryItem);
            }

            sale.TotalAmount = totalAmount;
            return await _saleRepository.CreateAsync(sale);
        }

        public async Task<Sale> UpdateSaleAsync(int id, CreateSaleDto updateSaleDto)
        {
            var existingSale = await _saleRepository.GetByIdAsync(id);
            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID {id} not found.");

            // Restore inventory quantities from existing sale
            foreach (var item in existingSale.SaleItems)
            {
                var inventoryItem = await _inventoryRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += item.Quantity;
                    await _inventoryRepository.UpdateAsync(inventoryItem);
                }
            }

            // Create new sale with updated data
            var updatedSale = await CreateSaleAsync(updateSaleDto);
            updatedSale.Id = id;
            return await _saleRepository.UpdateAsync(updatedSale);
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return false;

            // Restore inventory quantities
            foreach (var item in sale.SaleItems)
            {
                var inventoryItem = await _inventoryRepository.GetByIdAsync(item.InventoryItemId);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += item.Quantity;
                    await _inventoryRepository.UpdateAsync(inventoryItem);
                }
            }

            return await _saleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.GetByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Sale>> GetSalesByCustomerNameAsync(string customerName)
        {
            return await _saleRepository.GetByCustomerNameAsync(customerName);
        }

        public async Task<decimal> GetTotalSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _saleRepository.GetTotalSalesByDateRangeAsync(startDate, endDate);
        }

        public async Task<bool> UpdateSaleStatusAsync(int id, string status)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return false;

            sale.Status = status;
            sale.UpdatedAt = DateTime.UtcNow;
            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        public async Task<SalesReportDto> GenerateSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            var totalSales = sales.Sum(s => s.TotalAmount);
            var totalTransactions = sales.Count();

            var report = new SalesReportDto
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalSales = totalSales,
                TotalTransactions = totalTransactions,
                AverageTransactionValue = totalTransactions > 0 ? totalSales / totalTransactions : 0,
                SalesByPaymentMethod = (await GetSalesByPaymentMethodAsync(startDate, endDate)).ToList(),
                SalesByDay = (await GetSalesByDayAsync(startDate, endDate)).ToList(),
                TopSellingItems = (await GetTopSellingItemsAsync(startDate, endDate)).ToList()
            };

            return report;
        }

        public async Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            
            return sales
                .GroupBy(s => s.PaymentMethod)
                .Select(g => new SalesByPaymentMethodDto
                {
                    PaymentMethod = g.Key,
                    TotalAmount = g.Sum(s => s.TotalAmount),
                    TransactionCount = g.Count()
                })
                .OrderByDescending(x => x.TotalAmount);
        }

        public async Task<IEnumerable<SalesByDayDto>> GetSalesByDayAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            
            return sales
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new SalesByDayDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(s => s.TotalAmount),
                    TransactionCount = g.Count()
                })
                .OrderBy(x => x.Date);
        }

        public async Task<IEnumerable<TopSellingItemDto>> GetTopSellingItemsAsync(DateTime startDate, DateTime endDate, int limit = 10)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            
            return sales
                .SelectMany(s => s.SaleItems)
                .GroupBy(si => new { si.InventoryItemId, si.InventoryItem.Name })
                .Select(g => new TopSellingItemDto
                {
                    InventoryItemId = g.Key.InventoryItemId,
                    ItemName = g.Key.Name,
                    QuantitySold = g.Sum(si => si.Quantity),
                    TotalRevenue = g.Sum(si => si.TotalPrice)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(limit);
        }
    }
} 