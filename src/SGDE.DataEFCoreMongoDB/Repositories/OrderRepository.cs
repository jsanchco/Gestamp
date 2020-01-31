namespace SGDE.DataEFCoreMongoDB.Repositories
{  
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Domain.Repositories;
    using MongoDB.Driver;
    using System;
    using Domain.Helpers;

    #endregion

    public class OrderRepository : IOrderRepository
    {
        private readonly EFContextMongoDB _context;

        public OrderRepository(InfrastructureAppSettings infrastructure)
        {
            _context = new EFContextMongoDB(infrastructure);
        }

        public Order Add(Order newOrder)
        {
            newOrder.Id = Guid.NewGuid().ToString(); 
            _context.Orders.InsertOne(newOrder);
            return newOrder;
        }

        public bool Delete(string id)
        {
            var resultRemove = _context.Orders.DeleteOne(order => order.Id == id);

            return resultRemove.DeletedCount > 0;
        }

        public List<Order> GetAll(int skip = 0, int take = 0)
        {
            if (skip != 0 || take != 0)
                return _context.Orders.Find(order => true).Skip(skip).Limit(take).ToList();

            return _context.Orders.Find(order => true).ToList();
        }

        public Order GetById(string id)
        {
            return _context.Orders.Find(x => x.Id == id).FirstOrDefault();
        }

        public Order GetByOrderId(int orderId)
        {
            var orders = _context.Orders.Find(x => x.OrderId == orderId);
            if (!orders.Any())
                return null;

            return orders.FirstOrDefault();
        }

        public long TotalRegs()
        {
            return _context.Orders.Find(order => true).CountDocuments();
        }

        public bool Update(Order order)
        {
            var resultUpdate = _context.Orders.ReplaceOne(x => x.Id == order.Id, order);

            return resultUpdate.ModifiedCount > 0;
        }
    }
}
