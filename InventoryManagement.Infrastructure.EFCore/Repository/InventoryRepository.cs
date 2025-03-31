using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        private readonly InventoryContext _inventoryContext;
        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext,
            AccountContext accountContext) : base(inventoryContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
            _inventoryContext = inventoryContext;
        }

        public EditInventory? GetDetails(long Id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x => x.Id == Id);
        }

        public Inventory? GetBy(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public ICollection<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item => 
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }

        public ICollection<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x=>x.Id == inventoryId);
            var account = _accountContext.Accounts
                .Select(x=> new { x.Id, x.FullName})
                .ToList();
            var operations = inventory.Operations
                .Select(x => new InventoryOperationViewModel
                {
                    Id = x.Id,
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    Operation = x.Operation,
                    OperationDate = x.OperationDate.ToFarsi(),
                    OperatorId = x.OperatorId,
                    OrderId = x.OrderId,
                }).OrderByDescending(x => x.Id).ToList();

            operations.ForEach(item =>
                item.Operator = account.FirstOrDefault(x => x.Id == item.OperatorId)?.FullName ?? "-");
            return operations;
        }
    }
}
