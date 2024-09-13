using System;
using System.Collections.Generic;

namespace Day8_EasyQuestion
{
    internal class Program
    {
        IEmployeePromotion _promotion;

        public Program()
        {
            new Employee();
            new EmployeePromotion();
        }

        static List<Employee> employeeList = new List<Employee>();

        void AddEmployees()
        {
            try
            {
                Employee employee = new Employee();
                employee.TakeEmployeeDetailsFromUser();
                employeeList.Add(employee);
                Console.WriteLine("Employee added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the employee: " + ex.Message);
            }
        }

        List<string> employeeNames = new List<string>();

        public void AddEmployeeByPromotion()
        {
            try
            {
                Console.WriteLine("Enter the name until you choose to exit, For exit use 0");
                int choice = 0;
                do
                {
                    Console.WriteLine("Enter the employee name");
                    employeeNames.Add(Console.ReadLine());
                    Console.WriteLine("To add more, enter any number other than 0; to exit, enter 0");
                    choice = Convert.ToInt32(Console.ReadLine());
                } while (choice != 0);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        void GetEmployeeByIndex()
        {
            try
            {
                Console.WriteLine("Enter the employee name to get the promotion order");
                var name = Console.ReadLine();
                int position = employeeNames.IndexOf(name) + 1;

                if (position <= 0)
                {
                    Console.WriteLine($"{name} does not match with any employee in the list.");
                }
                else
                {
                    Console.WriteLine($"Employee {name} is at position {position}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching employee by index: " + ex.Message);
            }
        }

        void GetEmployeeOrder()
        {
            try
            {
                new EmployeePromotion().GetEmployeeByOrder(employeeNames);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting the employee order: " + ex.Message);
            }
        }

        void GetEmployeeByPromotion()
        {
            try
            {
                if(employeeNames.Count == 0)
                {
                    Console.WriteLine("No Employees in the list");
                }
                else {
                    foreach (string employee in employeeNames)
                    {
                        Console.WriteLine(employee);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting employees by promotion: " + ex.Message);
            }
        }

        void GetEmployeeSize()
        {
            try
            {
                Console.WriteLine($"Current number of elements: {employeeNames.Count}");
                Console.WriteLine($"Max available space: {employeeNames.Capacity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while checking the employee size: " + ex.Message);
            }
        }

        void Menu()
        {
            Console.WriteLine("1. Add employee");
            Console.WriteLine("2. Add employee names by promotion");
            Console.WriteLine("3. Get employee names by promotion");
            Console.WriteLine("4. Get employee index");
            Console.WriteLine("5. Check size");
            Console.WriteLine("6. Print all employees by order");
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            int choice = 0;
            do
            {
                try
                {
                    program.Menu();
                    Console.WriteLine("Enter the choice:");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exit");
                            break;
                        case 1:
                            program.AddEmployees();
                            break;
                        case 2:
                            program.AddEmployeeByPromotion();
                            break;
                        case 3:
                            program.GetEmployeeByPromotion();
                            break;
                        case 4:
                            program.GetEmployeeByIndex();
                            break;
                        case 5:
                            program.GetEmployeeSize();
                            break;
                        case 6:
                            program.GetEmployeeOrder();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                }

            } while (choice != 0);
        }
    }
}
