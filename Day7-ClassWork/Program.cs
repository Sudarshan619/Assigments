namespace Day7_ClassWork
{
    internal class Program
    {
        
        IValidation validateCustomer;
        public Program()
        {
            validateCustomer = new Validation();
        }

        void PrintDetails()
        {
            validateCustomer.GetCustomerAndPrintResult();
        }
        static void Main(string[] args)
        {
            new Program().PrintDetails();
        }
    }
}
