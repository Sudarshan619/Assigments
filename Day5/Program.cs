namespace Day5
{
    internal class Program
    {
        Employee CreateEmployee(int id, string name, string designation, double salary, DateTime dateOfBirth, int totalLeave)
        {
            Employee emp1 = new Employee(id,name,designation,salary,dateOfBirth,totalLeave);
            return emp1;
        }


        Expenses GenerateExpenses()
        {

            Expenses emp = new Expenses();
            Console.WriteLine("Please enter the employee Id");
            emp.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee Name");
            emp.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the Amount of Expense Id");
            emp.Amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the Expense Gategory ");
            emp.ExpenseCategory = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the Payment Method");
            emp.PaymentMethod = Console.ReadLine()?? "";

            Employee employee = GetEmployee(emp.Id);
            Console.WriteLine(employee.Total);
            emp.Balance = employee.Total - emp.Amount;

            Console.WriteLine($"Expenses for id {emp.Id} has been generated");
            return emp;
        }

        public void GetExpenses()
        {
            Console.WriteLine("You are generating expenses for a particular employee");
            var expense = GenerateExpenses();
            
            Console.WriteLine($"Employee with Id:{expense.Id} and Name:{expense.Name} has Created an Expense of Rs{expense.Amount} on {expense.ExpenseCreatedAt} for the category of {expense.ExpenseCategory} using {expense.PaymentMethod} and has a balance of Rs{expense.Balance} ");
        }
        
        Employee CreateEmployee()
        {
            Employee employee= new Employee();
            
            Console.WriteLine("Please enter the employee Id");
            employee.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the employee Name");
            employee.Name = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the employee Designation");
            employee.Designation = Console.ReadLine() ?? "";
            Console.WriteLine("Please enter the employee Salary");
            employee.Salary = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please enter the employee Date of Birth");
            employee.DateOfBirth = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Please enter the employee Total Leave");
            employee.TotalLeave = Convert.ToInt32(Console.ReadLine());
            return employee;
            
        }
        static int count = 0;
        Employee[] employee = new Employee[count];
        Employee CreateEmployee(int count)

        {
                Employee employee1 = new Employee();
                Console.WriteLine("Please enter the employee Id");
                employee1.Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the employee Name");
                employee1.Name = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter the employee Designation");
                employee1.Designation = Console.ReadLine() ?? "";
                Console.WriteLine("Please enter the employee Salary");
                employee1.Salary = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Please enter the employee Date of Birth");
                employee1.DateOfBirth = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Please enter the employee Total Leave");
                employee1.TotalLeave = Convert.ToInt32(Console.ReadLine());
            

            return employee1;
        }

        Employee GetEmployee(int id)
        {
            int index = 0;
            for(int i = 0; i < employee.Length; i++)
            {
                if (employee[i].Id == id)
                {
                    index = i;
                }
            }
            return employee[index];
        }
        static void Main(string[] args)
        {   
            Program program = new Program();
            //var details = program.CreateEmployee(10, "Sudarshan", "IT", 100.00,new DateTime(), 10);
            Console.WriteLine("Employee details");
            //Console.WriteLine($"Id:{details.Id}\n Name:{details.Name}\n Designation:{details.Designation}\n Salary:{details.Salary}\n DOB:{details.DateOfBirth}\n Leaves:{details.TotalLeave}\n");
            Issue issue1 = new Issue(1,"Spam mails","Getting spam mails","Kishan",3);
            //Console.WriteLine(issue1);
            //Parent parent1 = new Parent();
            //parent1.setName("Raju");
            //parent1.PrintName();

            Problems problems1 = new Problems();
            
            for (int i = 0; i < program.employee.Length; i++)
            {
                program.employee[i] = program.CreateEmployee();
            }

            //program.GetExpenses();
            //double avg = problems1.Average();
            //Console.WriteLine(avg);
            //problems1.DivisibleBy3();
            //var word = problems1.ToWords();
            //Console.WriteLine(word);
            problems1.FindPrime();
           
        }

        class Parent
        {
            protected string Name { get; set; } = string.Empty;
            public Parent()
            {
                Console.WriteLine("parent constructed");
                Name = "Ramu";
            }
            public virtual void PrintName()//If my child class chooses to provide new functionality, it can override the method
            {
                Console.WriteLine("Parent " + Name);
            }

            public void setName(string name)
            {
                Name = name;
            }
        }
        class Child : Parent
        {
            public Child()
            {
                Console.WriteLine("Chlid constructed");
                Name += " Somu";
            }
            public override void PrintName()//Changing the parent functionality
            {
                Console.WriteLine("Child " + Name);
            }
        }
    }
}
