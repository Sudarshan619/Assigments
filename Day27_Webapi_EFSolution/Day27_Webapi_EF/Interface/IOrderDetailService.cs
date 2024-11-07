using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface IOrderDetailService
    {
        Task<bool> AddOrderDetail(OrderDetailDTO OrderDetail);

        Task<int> UpdateOrderDetail(int id, OrderDetailDTO OrderDetail);

        Task<OrderDetail> GetOrderDetail(int id);

        Task<IEnumerable<OrderDetail>> GetAllOrderDetails();
    }
}
