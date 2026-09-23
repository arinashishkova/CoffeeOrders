using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeOrders.Core
{
    public struct CoffeeOrder
    {
        public string CustomerName { get; set; }

        public DrinkType DrinkType { get; set; }

        public DrinkSize DrinkSize { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int PreparationTime { get; set; }

        public OrderStatus Status { get; set; }
    }
}