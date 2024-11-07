using Day27_Webapi_EF.DTO;

namespace Day27_Webapi_EF.Interface
{
    public interface ICustomerBasicService
    {
        Task<int> CreateCustomer(CustomerDTO customer);
    }
}
