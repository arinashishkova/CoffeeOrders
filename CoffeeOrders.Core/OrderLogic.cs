using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeOrders.Core
{
    public static class OrderLogic
    {
        public static decimal GetUnitPrice(DrinkType drinkType, DrinkSize drinkSize)
        {
            return (drinkType, drinkSize) switch
            {
                (DrinkType.Espresso, DrinkSize.Small) => 2.00m,
                (DrinkType.Espresso, DrinkSize.Medium) => 2.50m,
                // Large Espresso is not available.

                (DrinkType.Latte, DrinkSize.Small) => 3.00m,
                (DrinkType.Latte, DrinkSize.Medium) => 3.50m,
                (DrinkType.Latte, DrinkSize.Large) => 4.00m,

                (DrinkType.Cappuccino, DrinkSize.Small) => 3.00m,
                (DrinkType.Cappuccino, DrinkSize.Medium) => 3.50m,
                (DrinkType.Cappuccino, DrinkSize.Large) => 4.00m,

                _ => throw new InvalidOperationException() // rest cases, 'throw' - error/exception
            };
        }

        public static decimal CalculateTotalPrice(
            DrinkType drinkType,
            DrinkSize drinkSize,
            int quantity)
        {
            decimal unitPrice = GetUnitPrice(drinkType, drinkSize);

            return unitPrice * quantity;
        }

        public static int CalculatePreparationTime(
            DrinkType drinkType,
            DrinkSize drinkSize,
            int quantity)
        {
            int minutesPerDrink = (drinkType, drinkSize) switch
            {
                (DrinkType.Espresso, DrinkSize.Small) => 2,
                (DrinkType.Espresso, DrinkSize.Medium) => 3,
                // Large Espresso is not available.

                (DrinkType.Latte, DrinkSize.Small) => 3,
                (DrinkType.Latte, DrinkSize.Medium) => 4,
                (DrinkType.Latte, DrinkSize.Large) => 5,

                (DrinkType.Cappuccino, DrinkSize.Small) => 3,
                (DrinkType.Cappuccino, DrinkSize.Medium) => 4,
                (DrinkType.Cappuccino, DrinkSize.Large) => 5,

                _ => throw new InvalidOperationException()
            };

            return minutesPerDrink * quantity;
        }

        public static void ValidateOrder(
            string customerName,
            DrinkType drinkType,
            DrinkSize drinkSize,
            int quantity)
        {
            if (string.IsNullOrWhiteSpace(customerName) ||
                customerName.Trim().Length < 2 ||
                customerName.Trim().Length > 30)
            {
                throw new ArgumentException();
            }

            if (quantity < 1 || quantity > 10)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (drinkType == DrinkType.Espresso &&
                drinkSize == DrinkSize.Large)
            {
                throw new InvalidOperationException();
            }
        }

        public static CoffeeOrder CreateOrder(
            string customerName,
            DrinkType drinkType,
            DrinkSize drinkSize,
            int quantity,
            OrderStatus status)
        {
            ValidateOrder(customerName, drinkType, drinkSize, quantity);

            return new CoffeeOrder
            {
                CustomerName = customerName.Trim(),
                DrinkType = drinkType,
                DrinkSize = drinkSize,
                Quantity = quantity,
                Price = CalculateTotalPrice(drinkType, drinkSize, quantity),
                PreparationTime = CalculatePreparationTime(
                    drinkType,
                    drinkSize,
                    quantity),
                Status = status
            };
        }
    }
}