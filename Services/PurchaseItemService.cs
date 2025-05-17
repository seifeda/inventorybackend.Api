using AutoMapper;
using inventorybackend.Api.DTOs.Purchase;
using inventorybackend.Api.Interfaces.Services;
using inventorybackend.Api.Models;

namespace inventorybackend.Api.Services
{
    public class PurchaseItemService : IPurchaseItemService
    {
        private readonly IPurchaseItemRepository _repository;
        private readonly IMapper _mapper;

        public PurchaseItemService(IPurchaseItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PurchaseItemDto>> GetAllAsync()
        {
            var purchaseItems = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PurchaseItemDto>>(purchaseItems);
        }

        public async Task<PurchaseItemDto> GetByIdAsync(int id)
        {
            var purchaseItem = await _repository.GetByIdAsync(id);
            return _mapper.Map<PurchaseItemDto>(purchaseItem);
        }

        public async Task<IEnumerable<PurchaseItemDto>> GetByPurchaseIdAsync(int purchaseId)
        {
            var purchaseItems = await _repository.GetByPurchaseIdAsync(purchaseId);
            return _mapper.Map<IEnumerable<PurchaseItemDto>>(purchaseItems);
        }

        public async Task<PurchaseItemDto> CreateAsync(CreatePurchaseItemDto createDto)
        {
            var purchaseItem = _mapper.Map<PurchaseItem>(createDto);
            var createdPurchaseItem = await _repository.CreateAsync(purchaseItem);
            return _mapper.Map<PurchaseItemDto>(createdPurchaseItem);
        }

        public async Task<PurchaseItemDto> UpdateAsync(int id, UpdatePurchaseItemDto updateDto)
        {
            var existingPurchaseItem = await _repository.GetByIdAsync(id);
            if (existingPurchaseItem == null)
            {
                throw new KeyNotFoundException($"Purchase item with ID {id} not found.");
            }

            _mapper.Map(updateDto, existingPurchaseItem);
            var updatedPurchaseItem = await _repository.UpdateAsync(existingPurchaseItem);
            return _mapper.Map<PurchaseItemDto>(updatedPurchaseItem);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Purchase item with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }
    }
} 