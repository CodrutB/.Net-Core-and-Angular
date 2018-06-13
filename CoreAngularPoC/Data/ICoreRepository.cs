using System.Collections.Generic;
using CoreAngularPoC.Data.Entities;

namespace CoreAngularPoC.Data
{
    public interface ICoreRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        Order GetOrderBy(int id);
        void AddEntity(object model);
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderBy(string name, int orderId);
    }
}