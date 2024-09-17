namespace Day_10
{
    internal class Program
    {
        void UnderstandingNullables()
        {
            int num1 = 10;
            int? num2 = null;//nullable primitive type
            //num1 = num2;//Not possible without casting
            //We can use this instead
            num1 = num2 ?? 0; //if num2 is null, assign 0 to num1
            num2 = num1; //possible- Implicit conversion
        }
        void UnderstandingLimits()
        {
            int num1 = 0;
            Console.WriteLine("Please enter a number");
            //int num1 = int.MaxValue;
            //Console.WriteLine($"The value of num1 is {num1}");
            //checked//will throw an exception if overflow occurs
            //{
            //    try
            //    {
            //        num1++;
            //        Console.WriteLine($"The value of num1 after incrementing is {num1}");
            //    }
            //    catch (OverflowException)
            //    {
            //        Console.WriteLine("Overflow occurred!");
            //
            //    }
            //}

            //*var result = int.TryParse(Console.ReadLine(), out num1);
            //if (result)
            //{
            //    Console.WriteLine($"The value you have entered is {num1}");
            //}
            //else
            //{
            //    Console.WriteLine("Invalid entry for number");
            //}

            while (int.TryParse(Console.ReadLine(), out num1) == false)
            {
                Console.WriteLine("Invalid entry for number. Try again");
            }
            Console.WriteLine($"The value you have entered is {num1}");

        }
        void UnderstandingThreading()
        {
            lock (this) {
            for (int i = 0; i < 10; i++)
            {
               
                    Thread.Sleep(1000);//mimics a wait for a process to complete
               
                Console.WriteLine(i);
            }
            }
        }
        void UndertsandingUsageOfWaitTime()
        {
            for (int i = 10; i < 101; i = i + 10)
            {
                if (i == 50)
                {
                    Thread.Sleep(3000);//mimics a wait for a process to complete
                }
                Console.WriteLine(i);
            }
        }

        void UnderstandingTask()
        {
            Task task = new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"The task is printing {i}");
                }
            });
            Console.WriteLine("Created the task");
            task.Start();
            task.Wait(5000);
            Console.WriteLine("After the task is completed");
        }
        async Task UnderstandingAsyncAwait()
        {
            Console.WriteLine("First line in task");
            Thread.Sleep(2000);
            Console.WriteLine("Last line in task");
        }
        async Task CallTheAsyncMethod()
        {
            //Console.WriteLine("Calling the async method");
            //Task task = UnderstandingAsyncAwait();
            //Console.WriteLine("Call is done");
            //task.Wait();
            Console.WriteLine("Calling the async method");
            await UnderstandingAsyncAwait();//Completes the execution of the method and then comes back to the caller
            Console.WriteLine("Call is done");

        }
        static async Task Main(string[] args)
        {
            Program program = new Program();
            //Thread t1, t2;
            //t1 = new Thread(program.UnderstandingThreading);
            //t2 = new Thread(program.UnderstandingThreading);
            //t1.Name = "Thread 1";
            //t2.Name = "Thread 2";
            //t1.Start();
            //t2.Start();
            await program.CallTheAsyncMethod();
        }
    }
}
