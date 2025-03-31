using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();
            foreach (var item in items)
            {
                command.Add(new ReduceInventory(
                    item.ProductId,
                    item.Count,
                    $"خرید توسط کاربر مشتری",
                    item.OrderId
                ));
            }

            return _inventoryApplication.Reduce(command).IsSuccedded;
        }
    }
}
