using _0_Framework.Application;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServiceHost
{
    [HtmlTargetElement(Attributes = "Permission")]
    public class PermissionTagHelper : TagHelper
    {
        public int Permission { get; set; }

        private readonly IAuthHelper _authHelper;

        public PermissionTagHelper(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var accountPermission = _authHelper.GetPermissions();
            if (accountPermission.All(x => x != Permission) || !_authHelper.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }
            base.Process(context, output);
        }
    }
}
