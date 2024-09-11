using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal interface ICustomerService
    {
        public void BuyProduct();

        public bool IsProductAvailable(string productName);

        public Product[] ShowProductDetails();

    }
}
