using Day14_Login.Models.ViewModel;

namespace Day14_Login.Interface
{
    public interface IPizzaService
    {
        List<PizzaImageViewModel> GetAllPizzas();
    }
}
