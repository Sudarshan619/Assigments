using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Strategy
    {
        public interface IShippingStrategy
        {
            double CalculateShippingCost(double weight);
        }

        public class FedExShipping : IShippingStrategy
        {
            public double CalculateShippingCost(double weight)
            {
                return weight * 5;
            }
        }

        public class UPSShipping : IShippingStrategy
        {
            public double CalculateShippingCost(double weight)
            {
                return weight * 4.5;
            }
        }

        public class ShippingCostCalculator
        {
            private readonly IShippingStrategy _strategy;

            public ShippingCostCalculator(IShippingStrategy strategy)
            {
                _strategy = strategy;
            }

            public double Calculate(double weight)
            {
                return _strategy.CalculateShippingCost(weight);
            }
        }
    }
}
