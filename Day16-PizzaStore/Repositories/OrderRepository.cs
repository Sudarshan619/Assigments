using Day16_PizzaStore.Exceptions;
using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models;

namespace Day16_PizzaStore.Repositories
{
    public class OrderRepository:IRepository<int,Order>
    {
        List<Order> Orders = new List<Order>();
        
        public async Task<Order> Add(Order entity)
        {
            entity.OrderNumber = Orders.Max(c => c.OrderNumber) + 1;
            Orders.Add(entity);
            return entity;
        }

        public async Task<Order> Delete(int key)
        {
            var Order = await Get(key);
            Orders.Remove(Order);
            return Order;
        }

        public async Task<Order> Get(int key)
        {
            var customer = Orders.FirstOrDefault(c => c.OrderNumber == key);
            if (customer == null)
            {
                throw new NoEntityFoundException("Order", key);
            }
            return customer;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            if (Orders.Count == 0)
            {
                throw new CollectionEmptyException("Order");
            }
            return Orders;
        }

        public async Task<Order> Update(Order entity)
        {
            var Order = await Get(entity.OrderNumber);

            if (Order == null)
            {
                throw new NoEntityFoundException("Order", entity.OrderNumber);
            }
            Order.CustomerId = entity.CustomerId;
            Order.TotalAmount = entity.TotalAmount;
            Order.PaymentMethod = entity.PaymentMethod;
            Order.IsPaymentSuccess = entity.IsPaymentSuccess;
            Order.OrderStatus = entity.OrderStatus;
            return Order;
        }
    }

}
