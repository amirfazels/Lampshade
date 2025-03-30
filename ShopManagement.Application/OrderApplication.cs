using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contacts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
        }
        public long PlaceOrder(Cart cart)
        {
            long currentAcountId = _authHelper.CurrentAccountId();
            Order order = new Order(currentAcountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

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
            _orderRepository.SaveChanges();
            return issueTrackingNo;
        }

        public double GetAmountById(long id)
        {
            return _orderRepository.GetAmountById(id);
        }
    }
}
