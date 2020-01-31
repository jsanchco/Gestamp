namespace SGDE.Domain.Repositories
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;

    #endregion

    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetById(string id);
        Order GetByOrderId(int orderId);
        Order Add(Order newOrder);
        bool Update(Order order);
        bool Delete(string id);
    }
}
