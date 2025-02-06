using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    [Range(1,long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
    public long ProductId { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
    public double UnitPrice { get; set; }
    public ICollection<ProductViewModel> Products { get; set; }

    public CreateInventory()
    {
        Products = new List<ProductViewModel>();
    }
}