using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Product", new List<PermissionDto>()
                    {
                        new PermissionDto(100, "ListProducts"),
                        new PermissionDto(101, "SearchProducts"),
                        new PermissionDto(102, "CreateProduct"),
                        new PermissionDto(103, "EditProduct"),
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>()
                    {
                        new PermissionDto(200, "ListProductCategories"),
                        new PermissionDto(201, "SearchProductCategories"),
                        new PermissionDto(203, "CreateProductCategory"),
                        new PermissionDto(202, "EditProductCategory"),
                    }
                },
            };
        }
    }
}
