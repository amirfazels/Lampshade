using _0_Framework.Infrastructure;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext) : base(shopContext)
        {
            _shopContext = shopContext;
        }

        public double GetAmountById(long id)
        {
            var order = _shopContext.Orders.FirstOrDefault(x=> x.Id == id);
            return order?.PayAmount ?? 0;
        }
    }
}
