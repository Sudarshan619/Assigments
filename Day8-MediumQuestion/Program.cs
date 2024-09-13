using System;
using System.Collections.Generic;

namespace Day8_MediumQuestion
{
    internal class Program
    {
        Dictionary<int, Employee> employeeDictionary = new Dictionary<int, Employee>();
        IEmployeeOperations operations;
        static int key = 0;

        public Program()
        {
            operations = new EmployeeOperations();
        }

        // Get employee by ID with exception handling
        void GetEmployeeById()
        {
            try
            {
                operations.GetEmployeeById(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting employee by ID: " + ex.Message);
            }
        }

        // Get all employees with exception handling
        void GetAllEmployee()
        {
            try
            {
                operations.GetAllEmployee(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting all employees: " + ex.Message);
            }
        }

        // Add an employee with exception handling
        public void AddEmployee()
        {
            try
            {
                Employee employee = new Employee();
                employee.TakeEmployeeDetailsFromUser();
                employeeDictionary.Add(key, employee);
                key++;
                Console.WriteLine("Employee added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding an employee: " + ex.Message);
            }
        }

        // Sort employees by salary with exception handling
        public void SortEmployeeBySalary()
        {
            try
            {
                operations.SortEmployeeBySalary(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sorting employees: " + ex.Message);
            }
        }

        // Get employees by name with exception handling
        public void GetAllEmployeeByName()
        {
            try
            {
                operations.GetAllEmployeeByName(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting employees by name: " + ex.Message);
            }
        }

        // Get all older employees by name with exception handling
        public void GetAllElderEmployee()
        {
            try
            {
                operations.GetAllElderEmployee(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting elder employees: " + ex.Message);
            }
        }

        void DeleteEmployee()
        {
            try
            {
                operations.DeleteEmployeeById(employeeDictionary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the employee: " + ex.Message);
            }
        }

        // Display menu
        void Menu()
        {
            Console.WriteLine("1. Add employee");
            Console.WriteLine("2. Get employee by id");
            Console.WriteLine("3. Sort employee by salary");
            Console.WriteLine("4. Get all employees");
            Console.WriteLine("5. Get employee by name");
            Console.WriteLine("6. Get all employees older than a given employee");
            Console.WriteLine("7. Delete employee by id");
            Console.WriteLine("0. Exit");
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
                    Console.WriteLine("Enter your choice:");
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exiting the application");
                            break;
                        case 1:
                            program.AddEmployee();
                            break;
                        case 2:
                            program.GetEmployeeById();
                            break;
                        case 3:
                            program.SortEmployeeBySalary();
                            break;
                        case 4:
                            program.GetAllEmployee();
                            break;
                        case 5:
                            program.GetAllEmployeeByName();
                            break;
                        case 6:
                            program.GetAllElderEmployee();
                            break;
                        case 7:
                            program.DeleteEmployee();
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
