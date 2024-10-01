namespace Builder_DeignPattern
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Director _director = new Director();
            IBuilder builder = new CarBuilder();
            _director.builder = builder;

            Console.WriteLine("For super car");
            _director.BuildSuperCar();
            Console.WriteLine();
            Console.WriteLine("For normal car");
            _director.BuildNormalCar();
            Console.WriteLine();
            Console.WriteLine("Without diector");
            builder.AddEngine();
            builder.GetCarDetails();
        }
    }
}
