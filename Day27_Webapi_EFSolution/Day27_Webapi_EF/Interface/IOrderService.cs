using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface IOrderService
    {
        Task<bool> AddOrder(OrderDTO Order);

        Task<int> UpdateOrder(int id, OrderDTO Order);

        Task<Order> GetOrder(int id);

        Task<IEnumerable<Order>> GetAllOrders();
    }
}
