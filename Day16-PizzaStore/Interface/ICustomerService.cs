using Day16_PizzaStore.Models;

namespace Day16_PizzaStore.Interface
{
    public interface ICustomerService
    {
        public Task<IEnumerable<Pizza>> ViewPizzas();

    }

}
