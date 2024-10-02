using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Facade
    {
        public class OrderService
        {
            public void PlaceOrder(string item)
            {
                Console.WriteLine($"{item} has been ordered.");
            }
        }

        public class PaymentService
        {
            public void ProcessPayment(string item)
            {
                Console.WriteLine($"Payment processed for {item}.");
            }
        }

        public class DeliveryService
        {
            public void DeliverItem(string item)
            {
                Console.WriteLine($"{item} is being delivered.");
            }
        }

        public class ShoppingFacade
        {
            private OrderService _orderService = new OrderService();
            private PaymentService _paymentService = new PaymentService();
            private DeliveryService _deliveryService = new DeliveryService();

            public void CompletePurchase(string item)
            {
                _orderService.PlaceOrder(item);
                _paymentService.ProcessPayment(item);
                _deliveryService.DeliverItem(item);
            }
        }
    }
}
