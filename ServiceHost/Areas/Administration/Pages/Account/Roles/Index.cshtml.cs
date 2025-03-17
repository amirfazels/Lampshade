using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public RoleSearchModel SearchModel;
        public ICollection<RoleViewModel> Roles;

        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(RoleSearchModel searchModel)
        {
            Roles = _roleApplication.Search(searchModel);
        }
    }
}
