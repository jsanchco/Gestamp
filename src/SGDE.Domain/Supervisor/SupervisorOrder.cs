namespace SGDE.Domain.Supervisor
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Converters;
    using Entities;
    using ViewModels;

    #endregion

    public partial class Supervisor
    {
        public List<OrderViewModel> GetAllOrders(int skip = 0, int take = 0)
        {
            return OrderConverter.ConvertList(_orderRepository.GetAll(skip, take));
        }

        public OrderViewModel GetOrderById(string id)
        {
            var orderViewModel = OrderConverter.Convert(_orderRepository.GetById(id));

            return orderViewModel;
        }

        public OrderViewModel GetOrderByOrderId(int id)
        {
            var orderViewModel = OrderConverter.Convert(_orderRepository.GetByOrderId(id));

            return orderViewModel;
        }

        public OrderViewModel AddOrder(OrderViewModel newOrderViewModel)
        {
            var builder = new Order
            {
                Region = newOrderViewModel.Region,
                Country = newOrderViewModel.Country,
                ItemType = newOrderViewModel.ItemType,
                SalesChannel = newOrderViewModel.SalesChannel,
                OrderPriority = newOrderViewModel.OrderPriority,
                OrderDate = newOrderViewModel.OrderDate,
                OrderId = newOrderViewModel.OrderId,
                ShipDate = newOrderViewModel.ShipDate,
                UnitsSlod = newOrderViewModel.UnitsSlod,
                UnitPrice = newOrderViewModel.UnitPrice,
                UnitCost = newOrderViewModel.UnitCost,
                TotalRevenue = newOrderViewModel.TotalRevenue,
                TotalCost = newOrderViewModel.TotalCost,
                TotalProfi = newOrderViewModel.TotalProfi
            };

            _orderRepository.Add(builder);
            newOrderViewModel.Id = builder.Id;

            return newOrderViewModel;
        }

        public bool UpdateOrder(OrderViewModel orderViewModel)
        {
            var order = _orderRepository.GetById(orderViewModel.Id);

            if (order == null) return false;

            order.Region = orderViewModel.Region;
            order.Country = orderViewModel.Country;
            order.ItemType = orderViewModel.ItemType;
            order.SalesChannel = orderViewModel.SalesChannel;
            order.OrderPriority = orderViewModel.OrderPriority;
            order.OrderDate = orderViewModel.OrderDate;
            order.OrderId = orderViewModel.OrderId;
            order.ShipDate = orderViewModel.ShipDate;
            order.UnitsSlod = orderViewModel.UnitsSlod;
            order.UnitPrice = orderViewModel.UnitPrice;
            order.UnitCost = orderViewModel.UnitCost;
            order.TotalRevenue = orderViewModel.TotalRevenue;
            order.TotalCost = orderViewModel.TotalCost;
            order.TotalProfi = orderViewModel.TotalProfi;

            return _orderRepository.Update(order);
        }

        public bool DeleteOrder(string id)
        {
            return _orderRepository.Delete(id);
        }

        public long OrdersTotalRegs()
        {
            return _orderRepository.TotalRegs();
        }
    }
}
