﻿using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using System.Text.Json.Serialization;


namespace CoffeeShop.DataAccess.DataAccess.ModelDB.Order
{
    public class Order
    {
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }

        [JsonIgnore]
        public Address Address { get; set; }
        public int TotalPrices { get; set; }
        public string Currency { get; set; }
        public OrderStatus Status { get; set; }
    }
}
