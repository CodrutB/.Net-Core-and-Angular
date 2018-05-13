using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreAngularPoC.Data;
using CoreAngularPoC.Data.Entities;
using CoreAngularPoC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreAngularPoC.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICoreRepository _repository;

        public OrdersController(ICoreRepository repository, ILogger<ProductsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var results = _repository.GetAllOrders(includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get orders: {e}");
                return BadRequest("Failed");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderBy(id);
                if(order != null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }
                
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get order: {e}");
                return BadRequest("Failed");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = _mapper.Map<OrderViewModel, Order>(model);

                    if (order.OrderDate == DateTime.MinValue)
                    {
                        order.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(order);
                    if (_repository.SaveAll())
                    {
                        var vm = _mapper.Map<Order, OrderViewModel>(order);
                        return Created($"/api/orders/{vm.OrderId}", vm);
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to post order: {e}");
                return BadRequest("Failed");
            }
        }
    }
}