using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Design_Pattern
{
    public interface IObserver
    {
        void Update(string availability);
    }

    public class Customer : IObserver
    {
        public string Name { get; set; }
        public Customer(string name)
        {
            Name = name;
        }

        public void Update(string availability)
        {
            Console.WriteLine($"{Name}, Product is now {availability}.");
        }
    }

    public class Product
    {
        private List<IObserver> observers = new List<IObserver>();
        private string _productName;
        private string _availability;

        public Product(string productName, string availability)
        {
            _productName = productName;
            _availability = availability;
        }

        public string Availability
        {
            get { return _availability; }
            set
            {
                _availability = value;
                NotifyObservers();
            }
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(_availability);
            }
        }
    }
}
