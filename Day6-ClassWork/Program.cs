namespace Day6_ClassWork
{
    internal class Program
    {   
        ProductServices productServices = new ProductServices();
        void PrintMenu()
        {
            Console.WriteLine("Product store");
            Console.WriteLine("1 - Add product");
            Console.WriteLine("2 - Buy product");
            Console.WriteLine("3 - Increase product quantity");
            Console.WriteLine("4 - Show products");
            Console.WriteLine("0 - Exit");
        }

        void StoreTransaction()
        {   
            int choice = 0;
            
            do
            {
                PrintMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 0:
                        Console.WriteLine("Thank you for transaction , Come again");
                        break;

                   case 1:
                    AddProduct();
                        break;
                  case 2:
                        BuyProduct();
                        break;
                  case 3:
                        AddMoreQuantity();
                        break;
                  case 4:
                        ShowProductDetails();
                        break;
                  
                   default:
                        Console.WriteLine("Not valid");
                        break;


                }

            } while (choice !=0);
        }
        void AddProduct()
        {
            //var product = new Product();
            productServices.AddProduct();
  
        }

        public void ShowProductDetails() {

            Product[] products = productServices.ShowProductDetails();
            
            if(products.Length == 0)
            {
                Console.WriteLine("No product");
            }
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] != null)
                {
                    Console.WriteLine($"Product ID: {products[i].Id}");
                    Console.WriteLine($"Name: {products[i].Name}");
                    Console.WriteLine($"Price: {products[i].Price}");
                    Console.WriteLine($"Quantity: {products[i].Quantity}");
                    Console.WriteLine("--------------------------------");
                }
            }
        }

        void BuyProduct()
        {
            productServices.BuyProduct();
        }

        void AddMoreQuantity() {
            productServices.AddMoreQuantity();
        }
        static void Main(string[] args)
        {   
            Program program = new Program();
            program.StoreTransaction();

        }
    }
}
