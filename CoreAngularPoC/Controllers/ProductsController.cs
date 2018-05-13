using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAngularPoC.Data;
using CoreAngularPoC.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreAngularPoC.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ICoreRepository _repository;

        public ProductsController(ICoreRepository repository, ILogger<ProductsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to get products: {e}");
                return BadRequest("Failed");
            }
        }
    }
}