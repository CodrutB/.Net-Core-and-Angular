using CoreAngularPoC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreAngularPoC.Data
{
    public class CoreRepository : ICoreRepository
    {
        private readonly CoreContext _context;
        private readonly ILogger<CoreRepository> _logger;

        public CoreRepository(CoreContext context, ILogger<CoreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).ToList();
            }

            return _context.Orders.ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products called.");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
           catch(Exception e)
            {
                _logger.LogError("Exception: " + e.ToString());
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category.Equals(category)).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Order GetOrderBy(int id)
        {
            return _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).Where(o => o.Id == id).FirstOrDefault();
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }
    }
}
