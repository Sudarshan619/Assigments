using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8_MediumQuestion
{
    internal interface IEmployeeOperations
    {

        public void GetEmployeeById(Dictionary<int, Employee> employees) { }

        public void SortEmployeeBySalary(Dictionary<int, Employee> employees) { }

        public void GetAllEmployeeByName(Dictionary<int, Employee> employees) { }

        public void GetAllElderEmployee(Dictionary<int, Employee> employees) { }

        public void GetAllEmployee(Dictionary<int, Employee> employees) { }

        public void UpdateEmployee(Dictionary<int, Employee> employees) { }

        public void DeleteEmployeeById(Dictionary<int, Employee> employees) {  }
    }
}
