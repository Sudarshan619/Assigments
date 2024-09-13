using System.Collections;

namespace Day8
{
    internal class Program
    {
        //Customer customer= new Customer();
        void UnderstandingCollection()
        {
            List<int> numbers = new List<int>(5);
            numbers.Add(100);
            numbers.Add(234);
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Add(new Random().Next(100, 200));
            numbers.Remove(100);
            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine(numbers);
            }

        }
        void UnderstandingDictionary()
        {
            Dictionary<int, Customer> Customer = new Dictionary<int, Customer>();
            Customer.Add(100, new Customer(100, "Ramu", 1234567890));
            Customer.Add(101, new Customer(101, "Bimu", 5544332211));
            Customer.Add(102, new Customer(101, "Domu", 9876543210));
            foreach (var item in Customer.Keys)
            {
                Console.WriteLine(Customer[item]);
            }
        }
        void UnderstandingMoreOnList()
        {
            List<Customer> Customer = new List<Customer>();
            int choice = 0;
            do
            {
                Customer customer = new Customer();
                customer.GetCustomerDetaislFromConsole();
                customer.Id = Customer.Count + 100;
                Customer.Add(customer);
                Console.WriteLine("Do you want to continue? Then enter any number otehr than 0.");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice != 0);
            Console.WriteLine("----------------------------------------");
            bool isCustomerFound = Customer.Contains(new Customer(100, "Ramu", 1234567890));
            Customer.Sort();
            Console.WriteLine("Is Ramu present " + isCustomerFound);
            foreach (var customer in Customer)
            {
                Console.WriteLine(customer);
            }

        }

        void UnderstaningLimitationOfArray()
        {
            int[] numbers = new int[10];
            for (int i = 0; i < 10; i++)
            {
                numbers[i] = i * 100 + new Random().Next(10, 100);
            }
            //To increase the size of array we have to create a new array and copy the old array to new array
            int[] nums1 = new int[12];
            for (int i = 0; i < 10; i++)
            {
                nums1[i] = numbers[i];
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
            
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.UnderstaningLimitationOfArray();
            program.UnderstandingDictionary();
            program.UnderstandingMoreOnList();
        }
    }
}
