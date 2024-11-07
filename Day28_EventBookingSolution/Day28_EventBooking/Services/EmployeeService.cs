using AutoMapper;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Services
{
    public class EmployeeService
    {
        IRepository<int, Employee> _employeeRepository;
        IMapper _mapper;
        public EmployeeService(IRepository<int, Employee> repository, IMapper mapper)
        {
            _employeeRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddProduct(EmployeeDTO Employee)
        {

            Employee newProduct = _mapper.Map<Employee>(Employee);
            var addedProduct = await _employeeRepository.Add(newProduct);
            return addedProduct != null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                var product = await _employeeRepository.GetAll();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new NotFoundException("employee not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
