using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly ArticleQuery _articleQuery;

        public ArticleModel(ArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        public void OnGet()
        {
        }
    }
}
