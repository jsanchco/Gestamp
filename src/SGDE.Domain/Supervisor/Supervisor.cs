// ReSharper disable InconsistentNaming
namespace SGDE.Domain.Supervisor
{
    #region Using

    using Microsoft.Extensions.Options;
    using Helpers;
    using Repositories;

    #endregion

    public partial class Supervisor : ISupervisor
    {
        private readonly IOrderRepository _orderRepository;

        public Supervisor()
        {
        }

        public Supervisor(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
