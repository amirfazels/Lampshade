using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application;
using ShopManagement.Application.Contacts.Order;
using ShopManagement.Application.Contacts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public SelectList Accounts;
        public OrderSearchModel SearchModel;
        public ICollection<OrderViewModel> Orders;
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication productCategoryApplication, IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            _orderApplication = productCategoryApplication;
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName"); 
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
        }
    }
}
