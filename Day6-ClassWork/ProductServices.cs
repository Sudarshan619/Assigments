using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal class ProductServices:ICustomerService,ISupplierService

    {
        
        static Product[] products = new Product[10];
        static Supplier[] suppliers = new Supplier[10];
        static Customer[] customers = new Customer[10];
        static int indexProduct = 0;
        static int indexSupplier = 0;
        static int indexCustomer = 0;


        public Product GetProductByName(string productName)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Name == productName)
                {
                    return products[i];
                }
            }
            return null;
        }
        public bool isCustomer(int cid)
        {
            // Check if there are any customers in the system
            if (indexCustomer == 0 || customers[0] == null)
            {
                Console.WriteLine("No customers found.");

                return false;
            }

            // Loop through all customers and check if any match the given customer id
            for (int i = 0; i < indexCustomer; i++)
            {
                if (customers[i] != null && customers[i].Id == cid)
                {
                    return true; // Customer found
                }
            }

            Console.WriteLine("Customer Id not found.");
            return false; // Customer not found
        }


        public bool IsProductAvailable(string productName)
        {
            if (products[0] == null) {
                Console.WriteLine("No Products");
                return false;
            }
            for (int i = 0; i < indexProduct; i++)
            {
                if (products[i].Name == productName)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveProduct()
        {

        }

         public Product[] ShowProductDetails()
        {
            Product[] ShowProducts = new Product[indexProduct];
            if (products == null)
            {
                return null;

            }
            for(int i = 0; i < indexProduct; i++)
            {
                ShowProducts[i] = new Product();
                ShowProducts[i] = products[i];
            }
            return ShowProducts;
        }

        public void UpdateProduct()
        {

        }
        public void AddProduct()
        {
            Console.WriteLine("Enter the product name:");
            var productName = Console.ReadLine();

            if (IsProductAvailable(productName))
            {
                Console.WriteLine("Product already exists.");
                return;
            }

            Console.WriteLine("Enter the Supplier Id:");
            var sid = Convert.ToInt32(Console.ReadLine());

            // If no supplier exists, create a new supplier
            if (!isSupplier(sid))
            {
                Console.WriteLine("No suppliers found. Add new supplier.");
                suppliers[indexSupplier] = new Supplier().CreateSupplier();
                indexSupplier++;
                sid = suppliers[indexSupplier - 1].Id; // Set supplier id to newly created supplier's id
            }

            products[indexProduct] = new Product().CreateProduct();
            indexProduct++;
            Console.WriteLine("Product added successfully.");
        }


        public void AddMoreQuantity() {
            Console.WriteLine("Enter the product name");
            var productName = Console.ReadLine();
            if (IsProductAvailable(productName)) {
                var product = GetProductByName(productName);
                Console.WriteLine("Enter the supplier id");
                var sid = Convert.ToInt32(Console.ReadLine());
                if (isSupplier(sid))
                {
                    Console.WriteLine("enter the quantity");
                    var quantity = Convert.ToInt32(Console.ReadLine());
                    product.Quantity += quantity;      
                }
            }
            else {
                Console.WriteLine("product not available");
            }
         }

        public bool isSupplier(int sid)
        {
            if (indexSupplier == 0)
            {
                
                return false;
            }

            for (int i = 0; i < indexSupplier; i++)
            {
                if (suppliers[i] != null && suppliers[i].Id == sid)
                {
                    Console.WriteLine("Supplier found: " + suppliers[i].Name);
                    return true;
                }
            }

            Console.WriteLine("Supplier Id not found.");
            return false;
        }
        public void BuyProduct()
        {
            // Prompt the user to enter the product name
            Console.WriteLine("Enter the product name:");
            var productName = Console.ReadLine();

            // Check if there are any products in the store
            if (products[0] == null)
            {
                Console.WriteLine("No product available in the store.");
                return;
            }

            // Check if the product is available
            if (IsProductAvailable(productName))
            {
                // Prompt the user for the Customer Id
                Console.WriteLine("Enter the Customer Id:");
                var cid = Convert.ToInt32(Console.ReadLine());

                // Check if the customer exists
                if (!isCustomer(cid))
                {
                    // If customer is not found, prompt to create a new customer
                    Console.WriteLine("Customer not found. Would you like to create a new customer? (yes/no)");
                    var response = Console.ReadLine();

                    if (response.ToLower() == "yes")
                    {
                        Console.WriteLine("Enter customer id");
                        customers[indexCustomer] = new Customer().CreateCustomer();
                        indexCustomer++;
                        Console.WriteLine("Customer created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Purchase canceled.");
                        return;
                    }
                }

                // Proceed with the product purchase
                Console.WriteLine("Enter the quantity:");
                var quantity = Convert.ToInt32(Console.ReadLine());

                // Get the product by name
                var product = GetProductByName(productName);

                // Check if there's enough quantity of the product
                if (product.Quantity < quantity)
                {
                    Console.WriteLine("Not enough quantity available.");
                }
                else
                {
                    // Deduct the quantity and confirm the purchase
                    product.Quantity -= quantity;
                    Console.WriteLine($"Product purchased successfully. Remaining quantity: {product.Quantity}");
                }
            }
            else
            {
                // If the product is not available
                Console.WriteLine("Product unavailable.");
            }
        }



    }
}
