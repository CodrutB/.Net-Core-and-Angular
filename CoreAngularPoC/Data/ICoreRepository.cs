using System.Collections.Generic;
using CoreAngularPoC.Data.Entities;

namespace CoreAngularPoC.Data
{
    public interface ICoreRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}