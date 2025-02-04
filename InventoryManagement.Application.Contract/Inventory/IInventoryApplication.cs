using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory  command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult Reduce(ICollection<ReduceInventory> command);
        EditInventory GetDetails(long Id);
        ICollection<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
