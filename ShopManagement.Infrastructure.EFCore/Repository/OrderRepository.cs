using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contacts.Order;
using ShopManagement.Application.Contracts;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext shopContext, AccountContext accountContext) : base(shopContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public double GetAmountById(long id)
        {
            var order = _shopContext.Orders.FirstOrDefault(x=> x.Id == id);
            return order?.PayAmount ?? 0;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _shopContext.Products
                .Select(x => new 
                { 
                    x.Id, 
                    x.Name
                }).ToList();

            var order = _shopContext.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order == null)
                return new List<OrderItemViewModel>();
            
            var items = order?.Items.Select(x => new OrderItemViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Count = x.Count,
                UnitPrice = x.UnitPrice,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? "";
            }

            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var query = _shopContext.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                IssueTrackingNo = x.IssueTrackingNo,
                PaymentMethodId = x.PaymentMethod,
                PayAmount = x.PayAmount,
                TotalAmount = x.TotalAmount,
                IsPaid = x.IsPaid,
                IsCanceled = x.IsCanceled,
                RefId = x.RefId,
                DiscountAmount = x.DiscountAmount,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.AccountId != 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            if (searchModel.IsCanceled != null)
                query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            var order = query.OrderByDescending(x => x.Id).ToList();

            order.ForEach(item =>
            {
                item.AccountFullName = _accountContext.Accounts.FirstOrDefault(x => x.Id == item.AccountId)?.FullName;
                item.PaymentMethod = PaymentMethod.GetBy(item.PaymentMethodId).Name;
            });
             
            return order;
        }
    }
}
