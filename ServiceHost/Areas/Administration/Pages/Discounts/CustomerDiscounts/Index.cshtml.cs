using DiscountManagement.Applicatiom.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contacts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public ICollection<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var CustomerDiscount = new DefineCustomerDiscount();
            CustomerDiscount.Products = _productApplication.GetProducts();
            return Partial("./Define", CustomerDiscount);
        }

        public JsonResult OnPostDefine(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
