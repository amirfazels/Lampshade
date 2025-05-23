using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contacts.Order;

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
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName");
        }

        public IActionResult OnGetConfirm(long orderId)
        {
            _orderApplication.PaymentSucceeded(orderId, 0);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long orderId)
        {
            _orderApplication.Cancel(orderId);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long orderId)
        {
            _orderApplication.Restore(orderId);
            return RedirectToPage("./Index");
        }

        public PartialViewResult OnGetItems(long orderId)
        {
            var items = _orderApplication.GetItems(orderId);
            return Partial("./Items", items);
        }
    }
}
