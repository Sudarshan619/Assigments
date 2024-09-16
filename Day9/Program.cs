namespace Day9
{
    internal class Program
    {
        //public delegate void CalculateDelegate(int n1, int n2);

        class Employee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Salary { get; set; }
            public override string ToString()
            {
                return "Id = " + ID + ", Name = " + Name + ", Salary = " + Salary;
            }
        }
        public Program()
        {
            UnderstandingTheUseOfDelegate();
            UnderstandingLINQ();    
        }
        void UnderstandingTheUseOfDelegate()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee { ID = 101, Name = "Mark", Salary = 5000 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 14000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };
            //Console.WriteLine("Please enter the employee Name");
            //string name = Console.ReadLine();
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = numbers.Where(x => x%2==0).OrderBy(x => -x);
            //from num in numbers
            // where (num % 2) == 0
            //  select num;
            //var employee = employees.FirstOrDefault(e=>e.Name == name);

            foreach (var n in numQuery)
            {
                Console.WriteLine(n);
            }

        }
        void UnderstandingLINQ()
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee { ID = 101, Name = "Mark", Salary = 5000 },
                new Employee { ID = 102, Name = "John", Salary = 10000 },
                new Employee { ID = 103, Name = "Smith", Salary = 14000 },
                new Employee { ID = 104, Name = "Watson", Salary = 15000 }
            };

            var highPaidEmployees = employees.Where(e => e.Name.Contains("m")).OrderBy(emp => emp.Salary).Take(2);

            foreach (var employee in highPaidEmployees)
            {
                Console.WriteLine(employee);
            }
        }

           
    //public bool FindEmployee(Employee employee)
    //{
    //    return employee.Name == "John";
    //}
    static void Main(string[] args)
        {
            new Program();
        }

    }
}
