using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Day25_DataBase
{
    internal class Program
    {
        
        IAuthentication authentication;
        IServices services;

        public Program() {
            authentication = new Authentication();
            services = new Services();
        }
        void choice()
        {
            Console.WriteLine("1.Login");
            Console.WriteLine("2.Register");
            Console.WriteLine("3.Update Password");
            Console.WriteLine("4.Exit");
        }

        void userInteraction()
        {
            Console.WriteLine("1.View Order history");
            Console.WriteLine("2.View Shipper");
            Console.WriteLine("3.View Order summary");
            Console.WriteLine("4.Exit");
        }


        static void Main(string[] args)
        {
            Program program = new Program();
            int choice = 0;
            do
            {
                program.choice();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) { 
                  case 1:if (program.authentication.login())
                        {
                            int num = 0;
                            do
                            {
                                program.userInteraction();
                                Console.WriteLine("Enter the choice");
                                num = Convert.ToInt32(Console.ReadLine());
                                switch (num)
                                {
                                    case 1:
                                        program.services.viewOrder();
                                        break;
                                    case 2:
                                        program.services.shipperDetails();
                                        break;
                                    case 3:
                                        program.services.orderSummary();
                                        break;
                                }

                            } while (num != 4);   
                        }
                        break;
                    case 2: program.authentication.register(); 
                        break;
                  case 3:program.authentication.updatePassword();
                        break;
                
                }

            }while(choice !=4);
        }
    }
}
