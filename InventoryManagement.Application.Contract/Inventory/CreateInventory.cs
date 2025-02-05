using ShopManagement.Application.Contacts.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
    public ICollection<ProductViewModel> Products { get; set; }
}