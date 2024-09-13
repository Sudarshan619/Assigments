using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8_EasyQuestion
{
    internal class EmployeePromotion:IEmployeePromotion, IComparable<EmployeePromotion>
    {
        public string Name { get; set; }
        public EmployeePromotion() { }

        public void GetEmployeeByOrder(List<string> employeeList) {
            //Cretaing temporary list so that original list does not get affected
            List<string> sortedList = new List<string>(employeeList);
            sortedList.Sort();

            foreach(var sorted in sortedList)
            {
                Console.WriteLine(sorted);
                
            }

        }

        public int CompareTo(EmployeePromotion? other) {

            return this.Name.CompareTo(other.Name);
        }
    }
}
