using inventorybackend.Api.DTOs.Sale;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventorybackend.Api.Services
{
    public class SalesService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SalesService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return sales.Select(s => MapToSaleDto(s));
        }

        public async Task<SaleDto?> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return null;
            return MapToSaleDto(sale);
        }

        public async Task<SaleDto> CreateSaleAsync(CreateSaleDto createSaleDto)
        {
            var sale = new Models.Sale
            {
                CustomerName = createSaleDto.CustomerName,
                CustomerEmail = createSaleDto.CustomerEmail,
                CustomerPhone = createSaleDto.CustomerPhone,
                PaymentMethod = createSaleDto.PaymentMethod,
                Notes = createSaleDto.Notes,
                TotalAmount = 0, // Will be calculated from items
                SaleDate = DateTime.UtcNow,
                Status = "Completed",
                SaleItems = createSaleDto.SaleItems.Select(i => new Models.SaleItem
                {
                    InventoryItemId = i.InventoryItemId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            // Calculate total amount
            sale.TotalAmount = sale.SaleItems.Sum(i => i.Quantity * i.UnitPrice);

            await _saleRepository.AddAsync(sale);
            return MapToSaleDto(sale);
        }

        public async Task<bool> UpdateSaleAsync(UpdateSaleDto updateSaleDto)
        {
            var sale = await _saleRepository.GetByIdAsync(updateSaleDto.Id);
            if (sale == null) return false;

            // Update properties if they are provided in the DTO
            if (updateSaleDto.CustomerName != null)
                sale.CustomerName = updateSaleDto.CustomerName;
            if (updateSaleDto.CustomerEmail != null)
                sale.CustomerEmail = updateSaleDto.CustomerEmail;
            if (updateSaleDto.CustomerPhone != null)
                sale.CustomerPhone = updateSaleDto.CustomerPhone;
            if (updateSaleDto.PaymentMethod != null)
                sale.PaymentMethod = updateSaleDto.PaymentMethod;
            if (updateSaleDto.Notes != null)
                sale.Notes = updateSaleDto.Notes;
            if (updateSaleDto.Status != null)
                sale.Status = updateSaleDto.Status;

            // Update sale items if provided
            if (updateSaleDto.SaleItems != null)
            {
                // Remove existing items
                sale.SaleItems.Clear();

                // Add updated items
                foreach (var itemDto in updateSaleDto.SaleItems)
                {
                    sale.SaleItems.Add(new Models.SaleItem
                    {
                        InventoryItemId = itemDto.InventoryItemId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice
                    });
                }


                // Recalculate total amount
                sale.TotalAmount = sale.SaleItems.Sum(i => i.Quantity * i.UnitPrice);
            }


            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return false;

            await _saleRepository.DeleteAsync(sale);
            return true;
        }

        public async Task<SalesReportDto> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            
            var totalSales = sales.Sum(s => s.TotalAmount);
            var totalTransactions = sales.Count();
            var averageTransactionValue = totalTransactions > 0 ? totalSales / totalTransactions : 0;

            var salesByPaymentMethod = sales
                .GroupBy(s => s.PaymentMethod)
                .Select(g => new SalesByPaymentMethodDto
                {
                    PaymentMethod = g.Key,
                    TransactionCount = g.Count(),
                    TotalAmount = g.Sum(s => s.TotalAmount)
                })
                .ToList();

            var salesByDay = sales
                .GroupBy(s => s.SaleDate.Date)
                .Select(g => new SalesByDayDto
                {
                    Date = g.Key,
                    TransactionCount = g.Count(),
                    TotalAmount = g.Sum(s => s.TotalAmount)
                })
                .OrderBy(d => d.Date)
                .ToList();

            var topSellingItems = sales
                .SelectMany(s => s.SaleItems)
                .GroupBy(si => new { si.InventoryItemId, si.InventoryItem.Name })
                .Select(g => new TopSellingItemDto
                {
                    InventoryItemId = g.Key.InventoryItemId,
                    ItemName = g.Key.Name,
                    QuantitySold = g.Sum(si => si.Quantity),
                    TotalRevenue = g.Sum(si => si.Quantity * si.UnitPrice)
                })
                .OrderByDescending(i => i.QuantitySold)
                .Take(10)
                .ToList();

            return new SalesReportDto
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalSales = totalSales,
                TotalTransactions = totalTransactions,
                AverageTransactionValue = averageTransactionValue,
                TopSellingItems = topSellingItems,
                SalesByPaymentMethod = salesByPaymentMethod,
                SalesByDay = salesByDay
            };
        }


        public async Task<IEnumerable<SaleDto>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            return sales.Select(s => MapToSaleDto(s));
        }

        public async Task<IEnumerable<SalesByPaymentMethodDto>> GetSalesByPaymentMethodAsync(DateTime startDate, DateTime endDate)
        {
            var sales = await _saleRepository.GetByDateRangeAsync(startDate, endDate);
            
            return sales
                .GroupBy(s => s.PaymentMethod)
                .Select(g => new SalesByPaymentMethodDto
                {
                    PaymentMethod = g.Key,
                    TransactionCount = g.Count(),
                    TotalAmount = g.Sum(s => s.TotalAmount)
                })
                .ToList();
        }

        private static SaleDto MapToSaleDto(Models.Sale sale)
        {
            return new SaleDto
            {
                Id = sale.Id,
                CustomerName = sale.CustomerName,
                CustomerEmail = sale.CustomerEmail,
                CustomerPhone = sale.CustomerPhone,
                PaymentMethod = sale.PaymentMethod,
                Notes = sale.Notes,
                TotalAmount = sale.TotalAmount,
                SaleDate = sale.SaleDate,
                Status = sale.Status,
                SaleItems = sale.SaleItems.Select(si => new SaleItemDto
                {
                    Id = si.Id,
                    InventoryItemId = si.InventoryItemId,
                    ItemName = si.InventoryItem?.Name ?? "Unknown",
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    TotalPrice = si.Quantity * si.UnitPrice
                }).ToList()
            };
        }
    }
}