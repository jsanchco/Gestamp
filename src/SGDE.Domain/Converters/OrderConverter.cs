namespace SGDE.Domain.Converters
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using ViewModels;

    #endregion

    public class OrderConverter
    {
        public static OrderViewModel Convert(Order order)
        {
            if (order == null)
                return null;

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Region = order.Region,
                Country = order.Country,
                ItemType = order.ItemType,
                SalesChannel = order.SalesChannel,
                OrderPriority = order.OrderPriority,
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                ShipDate = order.ShipDate,
                UnitsSlod = order.UnitsSlod,
                UnitPrice = order.UnitPrice,
                UnitCost = order.UnitCost,
                TotalRevenue = order.TotalRevenue,
                TotalCost = order.TotalCost,
                TotalProfi = order.TotalProfi
            };

            return orderViewModel;
        }

        public static List<OrderViewModel> ConvertList(IEnumerable<Order> orders)
        {
            return orders?.Select(order =>
            {
                var model = new OrderViewModel
                {
                    Id = order.Id,
                    Region = order.Region,
                    Country = order.Country,
                    ItemType = order.ItemType,
                    SalesChannel = order.SalesChannel,
                    OrderPriority = order.OrderPriority,
                    OrderDate = order.OrderDate,
                    OrderId = order.OrderId,
                    ShipDate = order.ShipDate,
                    UnitsSlod = order.UnitsSlod,
                    UnitPrice = order.UnitPrice,
                    UnitCost = order.UnitCost,
                    TotalRevenue = order.TotalRevenue,
                    TotalCost = order.TotalCost,
                    TotalProfi = order.TotalProfi
                };
                return model;
            })
                .ToList();
        }
    }
}
