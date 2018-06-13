using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreAngularPoC.Data;
using CoreAngularPoC.Data.Entities;
using CoreAngularPoC.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreAngularPoC.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;
        private readonly ICoreRepository _repository;

        public OrdersController(ICoreRepository repository, ILogger<ProductsController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var results = _repository.GetAllOrdersByUser(username, includeItems);

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
                var order = _repository.GetOrderBy(User.Identity.Name, id);
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
        public async Task<IActionResult> Post([FromBody]OrderViewModel model)
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

                    order.User = await _userManager.FindByNameAsync(User.Identity.Name);

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