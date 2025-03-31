using _0_Framework.Domain;
using ShopManagement.Application.Contacts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        double GetAmountById(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
