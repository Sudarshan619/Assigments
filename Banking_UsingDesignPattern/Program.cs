namespace Banking_UsingDesignPattern
{
    internal class Program
    {
        void PrintMenu()
        {
            Console.WriteLine("Enter the choice");
            Console.WriteLine("1.Savings account");
            Console.WriteLine("2.Current account");
        }
        static void Main(string[] args)
        {
            BankCreator creator = new ConcreteSavings();
            IBuilder builder = new ConcreteBuilderSavings();
            builder.CreateAccount(creator);
            creator.Deposit(1000,1);
            creator.ShowBalance(1);
            
        }
    }
}
