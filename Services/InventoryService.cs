using AutoMapper;
using inventorybackend.Api.DTOs.Inventory;
using inventorybackend.Api.Interfaces;
using inventorybackend.Api.Models;
using inventorybackend.Api.Repositories;

namespace inventorybackend.Api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetAllAsync()
        {
            var inventoryItems = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<InventoryItemDto>>(inventoryItems);
        }

        public async Task<InventoryItemDto> GetByIdAsync(int id)
        {
            var inventoryItem = await _repository.GetByIdAsync(id);
            if (inventoryItem == null)
            {
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");
            }
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<InventoryItemDto> GetBySkuAsync(string sku)
        {
            var inventoryItem = await _repository.GetBySkuAsync(sku);
            if (inventoryItem == null)
            {
                throw new KeyNotFoundException($"Inventory item with SKU {sku} not found.");
            }
            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        public async Task<IEnumerable<InventoryItemDto>> GetLowStockItemsAsync()
        {
            var lowStockItems = await _repository.GetLowStockItemsAsync();
            return _mapper.Map<IEnumerable<InventoryItemDto>>(lowStockItems);
        }

        public async Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto createDto)
        {
            if (await _repository.ExistsBySkuAsync(createDto.Sku))
            {
                throw new InvalidOperationException($"An inventory item with SKU {createDto.Sku} already exists.");
            }

            var inventoryItem = _mapper.Map<InventoryItem>(createDto);
            var createdItem = await _repository.CreateAsync(inventoryItem);
            return _mapper.Map<InventoryItemDto>(createdItem);
        }

        public async Task<InventoryItemDto> UpdateAsync(int id, UpdateInventoryItemDto updateDto)
        {
            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");
            }

            // Check if SKU is being changed and if it already exists
            if (existingItem.Sku != updateDto.Sku && await _repository.ExistsBySkuAsync(updateDto.Sku))
            {
                throw new InvalidOperationException($"An inventory item with SKU {updateDto.Sku} already exists.");
            }

            _mapper.Map(updateDto, existingItem);
            var updatedItem = await _repository.UpdateAsync(existingItem);
            return _mapper.Map<InventoryItemDto>(updatedItem);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");
            }

            await _repository.DeleteAsync(id);
        }

        public async Task UpdateStockLevelAsync(int id, int quantity)
        {
            if (!await _repository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Inventory item with ID {id} not found.");
            }

            await _repository.UpdateStockLevelAsync(id, quantity);
        }
    }
}

