using System;
using System.Collections.Generic;

namespace Day8_MediumQuestion
{
    internal class EmployeeOperations : IEmployeeOperations, IComparable<Employee>, IEquatable<Employee>
    {
        Employee employee = new Employee();

        public string Name { get; set; }

        public void GetAllEmployee(Dictionary<int, Employee> employees)
        {
            try
            {
                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees found.");
                    return;
                }

                foreach (var item in employees.Values)
                {
                    Console.WriteLine($"Employee id: {item.Id}");
                    Console.WriteLine($"Employee name: {item.Name}");
                    Console.WriteLine($"Employee age: {item.Age}");
                    Console.WriteLine($"Employee salary: {item.Salary}");
                    Console.WriteLine("-----------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving employees: " + ex.Message);
            }
        }

        public void GetEmployeeById(Dictionary<int, Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter employee id:");
                var id = Convert.ToInt32(Console.ReadLine());

                
                //new thing
                var employee = employees.Values.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    Console.WriteLine($"Employee id: {employee.Id}");
                    Console.WriteLine($"Employee name: {employee.Name}");
                    Console.WriteLine($"Employee age: {employee.Age}");
                    Console.WriteLine($"Employee salary: {employee.Salary}");
                    Console.WriteLine("-------------------------------------");
                }
                else
                {
                    Console.WriteLine("Employee with the given ID not found.");
                }
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

        public void SortEmployeeBySalary(Dictionary<int, Employee> employees)
        {
            try
            {
                if (employees.Count == 0)
                {
                    Console.WriteLine("No employees to sort.");
                    return;
                }

                var sortedEmployees = employees.Values.OrderBy(e => e.Salary).ToList();

                Console.WriteLine("Employees sorted by salary:");
                foreach (var employee in sortedEmployees)
                {
                    Console.WriteLine($"Employee id: {employee.Id}, Name: {employee.Name}, Salary: {employee.Salary}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sorting employees: " + ex.Message);
            }
        }

        public void GetAllEmployeeByName(Dictionary<int, Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter employee name:");
                var name = Console.ReadLine();
                bool found = false;

                foreach (var employee in employees.Values)
                {
                    if (employee.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Employee id :{employee.Id}");
                        Console.WriteLine($"Employee name: {employee.Name}");
                        Console.WriteLine($"Employee age: {employee.Age}");
                        Console.WriteLine($"Employee salary: {employee.Salary}");
                        Console.WriteLine("-------------------------------------");
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("No employee found with that name.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving employees by name: " + ex.Message);
            }
        }

        public void GetAllElderEmployee(Dictionary<int, Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter the name of the employee:");
                var name = Console.ReadLine();

                
                Employee selectedEmployee = employees.Values.First(e => e.Name.Equals(name));

                if (selectedEmployee != null)
                {
                   
                    foreach (var employee in employees.Values)
                    {
                        if (employee.Age < selectedEmployee.Age)
                        {
                            Console.WriteLine($"Employee id: {employee.Id}");
                            Console.WriteLine($"Employee name: {employee.Name}");
                            Console.WriteLine($"Employee age: {employee.Age}");
                            Console.WriteLine($"Employee salary: {employee.Salary}");
                            Console.WriteLine("-------------------------------------");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No employee found with that name.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving older employees: " + ex.Message);
            }
        }

        public void UpdateEmployee(Dictionary<int, Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter employee id:");
                int id = Convert.ToInt32(Console.ReadLine());

                if (employees.ContainsKey(id))
                {
                    var employee = employees[id];

                    Console.WriteLine("Enter new name (press Enter to skip):");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        employee.Name = newName;
                    }

                    Console.WriteLine("Enter new age (press Enter to skip):");
                    string ageInput = Console.ReadLine();
                    if (int.TryParse(ageInput, out int newAge))
                    {
                        employee.Age = newAge;
                    }

                    Console.WriteLine("Enter new salary (press Enter to skip):");
                    double salaryInput = Convert.ToDouble(Console.ReadLine());
                    if (salaryInput!= null)
                    {
                        employee.Salary = salaryInput;
                    }

                    Console.WriteLine("Employee details updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee ID not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating employee details: " + ex.Message);
            }
        }
        public void DeleteEmployeeById(Dictionary<int, Employee> employees)
        {
            try
            {
                Console.WriteLine("Enter employee id to delete:");
                var id = Convert.ToInt32(Console.ReadLine());

                
                var key = employees.First(e => e.Value.Id == id).Key;

                
                if (key != 0 || employees.ContainsKey(key))
                {
                    employees.Remove(key);
                    Console.WriteLine($"Employee with Id {id} deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Employee with the given ID not found.");
                }
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

        public int CompareTo(Employee? other)
        {
            return this.Name.CompareTo(other?.Name);
        }

        public bool Equals(Employee other)
        {
            if (other == null) return false;
            return this.employee.Id == other.Id;
        }
    }
}
