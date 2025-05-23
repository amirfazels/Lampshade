﻿namespace ShopManagement.Application.Contacts.Order
{
    public class PlaceOrder
    {
        public long AccountId { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<AddOrderItem> Items { get; set; }
    }
}
