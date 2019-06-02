using System;
using System.Threading;

namespace Threads
{
    class Program
    {
        static void UsingThreads()
        {
            Thread myThread = new Thread(SomeCode);
            myThread.Start();
        }

        static void SomeCode()
        {
            Console.WriteLine("Something");
        }

        static void UsingLambdaWithThreads()
        {
            Thread myThread = new Thread(() =>
            {
                Console.WriteLine("Something from Lambda");
            });

            myThread.Start();
        }

        static void PassingDataToThreads()
        {
            Thread myThread = new Thread((data) =>
            {
                Console.WriteLine("Data: " + data);
            });

            myThread.Start("inserted");
        }

        static void AbortingThreads()
        {
            bool tickRunning;
            Thread myThread = new Thread(() =>
            {
                tickRunning = true;
                while (tickRunning)
                {
                    Console.WriteLine("Looping");
                    Thread.Sleep(1000);
                }
            });

            myThread.Start();
            Console.WriteLine("Press a key to stop the loop");
            Console.ReadKey();
            tickRunning = false;
            //myThread.Abort();
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            UsingThreads();
            UsingLambdaWithThreads();
            PassingDataToThreads();
            AbortingThreads();
            Console.ReadKey();
        }
    }
}
