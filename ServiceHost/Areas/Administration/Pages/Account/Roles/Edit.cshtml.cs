using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditRole Role { get; set; }

        private readonly IRoleApplication _roleApplication;

        public EditModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Role = _roleApplication.GetDetails(id);
        }

        public IActionResult OnPost()
        {
            var result = _roleApplication.Edit(Role);
            return RedirectToPage("Index");
        }
    }
}
