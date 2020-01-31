namespace SGDE.DataEFCoreSQL.Repositories
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class OrderRepository : IOrderRepository
    {
        private readonly EFContextSQL _context;

        public OrderRepository(EFContextSQL context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private bool OrderExists(string id)
        {
            return GetById(id) != null;
        }

        public List<Order> GetAll(int skip = 0, int take = 0)
        {
            if (skip != 0 || take != 0)
                return _context.Order
                .Skip(skip)
                .Take(take)
                .ToList();

            return _context.Order
              .ToList();
        }

        public Order GetById(string id)
        {
            return _context.Order
                .FirstOrDefault(x => x.Id == id);
        }

        public Order GetByOrderId(int orderId)
        {
            return _context.Order
                .FirstOrDefault(x => x.OrderId == orderId);
        }

        public Order Add(Order newOrder)
        {
            _context.Order.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        public bool Update(Order order)
        {
            if (!OrderExists(order.Id))
                return false;

            _context.Order.Update(order);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(string id)
        {
            if (!OrderExists(id))
                return false;

            var toRemove = _context.Order.Find(id);
            _context.Order.Remove(toRemove);
            _context.SaveChanges();
            return true;

        }

        public long TotalRegs()
        {
            return _context.Order.Count();
        }
    }
}
