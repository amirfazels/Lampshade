using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contacts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration,
            IShopInventoryAcl shopInventoryAcl)
        {
            _authHelper = authHelper;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _shopInventoryAcl = shopInventoryAcl;
        }
        public long PlaceOrder(Cart cart)
        {
            long currentAcountId = _authHelper.CurrentAccountId();
            Order order = new Order(currentAcountId, cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

            foreach (var item in cart.Items)
                order.AddItem(new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate));
            
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var symbol = _configuration.GetValue<string>("symbol");
            string issueTrackingNo = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNo(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }
            return string.Empty;
        }

        public double GetAmountById(long id)
        {
            return _orderRepository.GetAmountById(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }
    }
}
