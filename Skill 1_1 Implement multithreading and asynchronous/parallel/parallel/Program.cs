using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace parallel
{
    class Program
    {
        static void Task(string taskName)
        {
            Console.WriteLine(taskName + " starting");
            Thread.Sleep(500);
            Console.WriteLine(taskName + " ending");
        }
 
        static void UsingForEach()
        {
            var items = Enumerable.Range(0, 100);

            Parallel.ForEach(items, item =>
            {
                Console.WriteLine("Started working on: " + item);
                Thread.Sleep(50);
                Console.WriteLine("Finished working on: " + item);
            });
        }

        static void Main(string[] args)
        {
            UsingForEach();

            Parallel.Invoke(() => Task("Task 1"), () => Task("Task 2"));

            Console.ReadKey();
        }
    }
}
