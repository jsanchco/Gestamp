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

        List<OrderViewModel> GetAllOrders(int skip = 0, int take = 0);
        OrderViewModel GetOrderById(string id);
        OrderViewModel AddOrder(OrderViewModel newOrderViewModel);
        bool UpdateOrder(OrderViewModel orderViewModel);
        bool DeleteOrder(string id);
        long OrdersTotalRegs();

        #endregion
    }
}
