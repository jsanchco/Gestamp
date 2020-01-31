namespace SGDE.API.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Domain.Supervisor;
    using Microsoft.Extensions.Logging;
    using System;
    using Domain.ViewModels;
    using System.Linq;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, ISupervisor supervisor)
        {
            _logger = logger;
            _supervisor = supervisor;
        }

        // GET api/order/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            try
            {
                return _supervisor.GetOrderById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                var queryString = Request.Query;
                var skip = Convert.ToInt32(queryString["$skip"]);
                var take = Convert.ToInt32(queryString["$top"]);

                var data = _supervisor.GetAllOrders(skip, take).ToList();
                var numReg = data.Count();

                return new { Items = data, Count = _supervisor.OrdersTotalRegs() };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public object Post([FromBody]OrderViewModel orderViewModel)
        {
            try
            {
                var result = _supervisor.AddOrder(orderViewModel);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public object Put([FromBody]OrderViewModel orderViewModel)
        {
            try
            {
                if (_supervisor.UpdateOrder(orderViewModel))
                {
                    return _supervisor.GetOrderById(orderViewModel.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/order/5
        [HttpDelete("{id}")]
        public object Delete(string id)
        {
            try
            {
                return _supervisor.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: ");
                return StatusCode(500, ex);
            }
        }
    }
}