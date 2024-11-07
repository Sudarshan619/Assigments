using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day28_EventBooking.Repository
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly EventBookingContext _employeeBookingContext;

        public EmployeeRepository(EventBookingContext employeeBookingContext)
        {
            _employeeBookingContext = employeeBookingContext;
        }
        public async Task<Employee> Add(Employee Entity)
        {
            try
            {
                _employeeBookingContext.Employees.Add(Entity);
                await _employeeBookingContext.SaveChangesAsync();
                return Entity;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not available");
            }

        }

        public Task<Employee> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> Get(int key)
        {
            try
            {
                var Employee = await _employeeBookingContext.Employees.FirstOrDefaultAsync(e => e.Id == key);
                if (Employee == null)
                {
                    throw new NotImplementedException();
                }
                return Employee;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }

        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {
                var Employees = await _employeeBookingContext.Employees.ToListAsync();
                if (Employees.Any())
                {
                    return Employees;
                }
                throw new NotImplementedException();

            }
            catch (Exception ex) {
                throw new NotImplementedException();
            }
        }

        public Task<Employee> Update(int id,Employee Entity)
        {
            throw new NotImplementedException();
        }
    }
}
