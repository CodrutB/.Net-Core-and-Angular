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
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICoreRepository _repository;

        public OrderItemsController(ICoreRepository repository, ILogger<ProductsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            try
            {
                var order = _repository.GetOrderBy(orderId);
                if(order != null)
                {
                    return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get order items: {e}");
                return BadRequest("Failed");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = _repository.GetOrderBy(orderId);
                if (order != null)
                {
                    var item = order.Items.Where(i => i.Id == id).FirstOrDefault();

                    if(item != null)
                    {
                        return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                    }
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get order items: {e}");
                return BadRequest("Failed");
            }
        }
    }
}