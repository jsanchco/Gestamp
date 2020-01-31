namespace SGDE.Domain.Supervisor
{
    #region Using

    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels;

    #endregion

    public interface ISupervisor
    {
        #region Order

        List<OrderViewModel> GetAllOrders();
        OrderViewModel GetOrderById(string id);
        OrderViewModel AddOrder(OrderViewModel newOrderViewModel);
        bool UpdateOrder(OrderViewModel orderViewModel);
        bool DeleteOrder(string id);

        #endregion
    }
}
