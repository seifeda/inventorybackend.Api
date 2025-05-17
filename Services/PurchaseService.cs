using AutoMapper;
using inventorybackend.Api.DTOs.Purchase;
using inventorybackend.Api.Interfaces;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IPurchaseItemRepository _purchaseItemRepository;
        private readonly IMapper _mapper;

        public PurchaseService(
            IPurchaseRepository purchaseRepository,
            IPurchaseItemRepository purchaseItemRepository,
            IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _purchaseItemRepository = purchaseItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllAsync()
        {
            var purchases = await _purchaseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseDto>>(purchases);
        }

        public async Task<PurchaseDto> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
            {
                throw new KeyNotFoundException($"Purchase with ID {id} not found.");
            }
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public async Task<PurchaseDto> CreateAsync(CreatePurchaseDto createDto)
        {
            var purchase = _mapper.Map<Purchase>(createDto);
            
            // Create the purchase first
            var createdPurchase = await _purchaseRepository.CreateAsync(purchase);

            // Create purchase items
            foreach (var itemDto in createDto.PurchaseItems)
            {
                var purchaseItem = _mapper.Map<PurchaseItem>(itemDto);
                purchaseItem.PurchaseId = createdPurchase.Id;
                await _purchaseItemRepository.CreateAsync(purchaseItem);
            }

            // Calculate and update total amount
            createdPurchase.TotalAmount = await _purchaseRepository.CalculateTotalAmountAsync(createdPurchase.Id);
            await _purchaseRepository.UpdateAsync(createdPurchase);

            return await GetByIdAsync(createdPurchase.Id);
        }

        public async Task<PurchaseDto> UpdateAsync(int id, UpdatePurchaseDto updateDto)
        {
            var existingPurchase = await _purchaseRepository.GetByIdAsync(id);
            if (existingPurchase == null)
            {
                throw new KeyNotFoundException($"Purchase with ID {id} not found.");
            }

            _mapper.Map(updateDto, existingPurchase);
            await _purchaseRepository.UpdateAsync(existingPurchase);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _purchaseRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Purchase with ID {id} not found.");
            }

            await _purchaseRepository.DeleteAsync(id);
        }
    }
}
