namespace Day7_Problem1
{
    internal class Program
    {
        IShowDoctors ShowDoctors = new ShowDoctors();
       
        void showDoctor()
        {
            ShowDoctors.ShowAllDoctors();
        }

        void AddDoctor()
        {    
                ShowDoctors.CreateDoctors();
        }

        void Menu()
        {
            Console.WriteLine("Select Option");
            Console.WriteLine("1.Add doctor");
            Console.WriteLine("2.Show doctors");
           
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            int choice = 0;
            do {
                program.Menu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) { 
                  case 0:
                        Console.WriteLine("Exiting");
                        break;
                  case 1:
                        program.AddDoctor();
                        break;
                  case 2:
                        program.showDoctor();
                        break;
                  default:
                        Console.WriteLine("None");
                        break;
                
                }


            }while (choice!=0);
           
        }
    }
}
