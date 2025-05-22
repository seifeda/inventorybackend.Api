using inventorybackend.Api.Models;

namespace inventorybackend.Api.Data
{
    public static class SeedData
    {
        public static async Task SeedDefaultSupplier(ApplicationDbContext context)
        {
            if (!context.Suppliers.Any())
            {
                var defaultSupplier = new Supplier
                {
                    Name = "Default Supplier",
                    ContactPerson = "John Doe",
                    Email = "contact@defaultsupplier.com",
                    Phone = "123-456-7890",
                    Address = "123 Main St",
                    City = "New York",
                    Country = "USA",
                    PostalCode = "10001",
                    IsActive = true
                };

                context.Suppliers.Add(defaultSupplier);
                await context.SaveChangesAsync();
            }
        }
    }
} 