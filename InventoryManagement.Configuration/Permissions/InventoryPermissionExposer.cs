using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(500, "ListInventory"),
                        new PermissionDto(501, "SearchInventory"),
                        new PermissionDto(502, "CreateInventory"),
                        new PermissionDto(503, "EditInventory"),
                    }
                }
            };
        }
    }
}
