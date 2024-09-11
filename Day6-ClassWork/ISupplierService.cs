using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal interface ISupplierService
    {
        public void AddProduct();

        public void RemoveProduct();

        public void UpdateProduct();

        public bool isSupplier(int sid);

    }
}
