using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contacts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public ICollection<InventoryViewModel> Inventory;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var Inventory = new CreateInventory();
            Inventory.Products = _productApplication.GetProducts();
            return Partial("./Create", Inventory);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _inventoryApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
