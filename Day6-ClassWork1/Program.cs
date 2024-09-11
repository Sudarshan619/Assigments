namespace Day6_ClassWork1
{
    internal class Program
    {
        //Note:Expection handling is not handled please enter correct data type value
        //No Customer model is present so it will calculate for only the current customer handling
        static void Main(string[] args)
        {
            IBankAccount account = null;
            Console.WriteLine("Select account type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. NRI Account");
            Console.WriteLine("3. Salary Account");

            int accountType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter initial balance:");
            double initialBalance = Convert.ToDouble(Console.ReadLine());

            switch (accountType)
            {
                case 1:
                    account = new SavingsAccount(initialBalance);
                    break;
                case 2:
                    account = new NRIAccount(initialBalance);
                    break;
                case 3:
                    account = new SalaryAccount(initialBalance);
                    break;
                default:
                    Console.WriteLine("Invalid account type");
                    return;
            }

            while (true)
            {
                Console.WriteLine("\nSelect an operation:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Show Balance");
                Console.WriteLine("4. Exit");

                int operation = Convert.ToInt32(Console.ReadLine());

                switch (operation)
                {
                    case 1:
                        Console.WriteLine("Enter amount to deposit:");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        account.Deposit(depositAmount);
                        break;
                    case 2:
                        Console.WriteLine("Enter amount to withdraw:");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        break;
                    case 3:
                        account.ShowBalance();
                        break;
                    case 4:
                        Console.WriteLine("Thank you for banking with us\nVisit soon");
                        return;
                    default:
                        Console.WriteLine("Invalid operation");
                        break;
                }
            }
        }
     }
}
