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
    }
}
