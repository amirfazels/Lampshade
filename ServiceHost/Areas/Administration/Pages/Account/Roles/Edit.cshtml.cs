using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public EditRole Role { get; set; }
        public List<SelectListItem> Permissions;

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            Role = _roleApplication.GetDetails(id);
            var Permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    Permissions.AddRange(value);
                    foreach (var permission in value)
                    {
                        var items = new SelectListItem(permission.Name, permission.Code.ToString()) 
                        { 
                            Group = new SelectListGroup { Name = key }
                        };
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            var result = _roleApplication.Edit(Role);
            return RedirectToPage("Index");
        }
    }
}
