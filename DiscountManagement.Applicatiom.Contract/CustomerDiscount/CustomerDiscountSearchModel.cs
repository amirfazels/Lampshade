﻿namespace DiscountManagement.Applicatiom.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
