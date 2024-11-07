using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Mapper;

namespace Day27_Webapi_EF.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        IRepository<int, OrderDetail> _orderDetailRepository;
        IMapper _mapper;
        public OrderDetailService(IRepository<int, OrderDetail> repository, IMapper mapper)
        {
            _orderDetailRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddOrderDetail(OrderDetailDTO OrderDetail)
        {

            OrderDetail newOrderDetail = _mapper.Map<OrderDetail>(OrderDetail);
            var addedOrderDetail = await _orderDetailRepository.Add(newOrderDetail);
            return addedOrderDetail != null;
        }

        public async Task<int> UpdateOrderDetail(int id, OrderDetailDTO OrderDetail)
        {
            try
            {
                var prod = _orderDetailRepository.Get(id);
                if (prod != null)
                {
                    OrderDetail newOrderDetail = _mapper.Map<OrderDetail>(OrderDetail);
                    var updatedOrderDetail = await _orderDetailRepository.Update(id, newOrderDetail);
                    return updatedOrderDetail.SNo;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Some error occured");
            }

        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            try
            {
                var OrderDetail = await _orderDetailRepository.Get(id);
                if (OrderDetail != null)
                {
                    return OrderDetail;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
        {
            try
            {
                var OrderDetail = await _orderDetailRepository.GetAll();
                if (OrderDetail != null)
                {
                    return OrderDetail;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
