﻿namespace CoffeeShop.DataAccess.DataAccess.ModelDB
{
    public class Promotion
    {
        public Guid Id { get; set; }
        public int PricePromotion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
