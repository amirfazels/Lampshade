using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        EditInventory GetDetails(long Id);
        Inventory GetBy(long productId);
        ICollection<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
