using Day16_PizzaStore.Exceptions;
using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models;

namespace Day16_PizzaStore.Repositories
{
    public class OrderDetailsRepository : IRepository<int, OrderDetails>
    {
        List<OrderDetails> orderDetails = new List<OrderDetails>();

        public async Task<OrderDetails> Add(OrderDetails entity)
        {
            entity.SINo = orderDetails.Max(c => c.SINo) + 1;
            orderDetails.Add(entity);
            return entity;
        }

        public async Task<OrderDetails> Delete(int key)
        {
            var orderDetail = await Get(key);
            orderDetails.Remove(orderDetail);
            return orderDetail;
        }

        public async Task<OrderDetails> Get(int key)
        {
            var customer = orderDetails.FirstOrDefault(c => c.SINo == key);
            if (customer == null)
            {
                throw new NoEntityFoundException("orderDetail", key);
            }
            return customer;
        }

        public async Task<IEnumerable<OrderDetails>> GetAll()
        {
            if (orderDetails.Count == 0)
            {
                throw new CollectionEmptyException("orderDetail");
            }
            return orderDetails;
        }

        public async Task<OrderDetails> Update(OrderDetails entity)
        {
            var orderDetail = await Get(entity.SINo);

            if (orderDetail == null)
            {
                throw new NoEntityFoundException("orderDetail", entity.SINo);
            }
            orderDetail.OrderNumber = entity.OrderNumber;
            orderDetail.PizzaId = entity.PizzaId;
            orderDetail.Quantity = entity.Quantity;
            orderDetail.Price = entity.Price;
            orderDetail.Discount = entity.Discount;
            return orderDetail;
        }
    }
}
