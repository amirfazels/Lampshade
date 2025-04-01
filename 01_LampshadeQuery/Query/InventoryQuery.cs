using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }


        public StockStatus CheckStock(IsInStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(i => i.ProductId == command.ProductId);
            var product = _shopContext.Products.FirstOrDefault(p => p.Id == command.ProductId);
            if (inventory == null || product == null)
                return new StockStatus
                {
                    IsStock = false,
                };
            return new StockStatus()
            {
                IsStock = inventory.CalculateCurrentCount() >= command.Count,
                ProductName = product.Name
            };
        }
    }
}
