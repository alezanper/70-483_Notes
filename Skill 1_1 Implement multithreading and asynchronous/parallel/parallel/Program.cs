using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace parallel
{
    class Program
    {
        class Task
        {
            public string Name { get; set; }
            public int Time { get; set; }

            public void Launch()
            {
                Console.WriteLine(this.Name + " starting");
                Thread.Sleep(this.Time);
                Console.WriteLine(this.Name + " ending");
            }
        }
 
        static void UsingForEach()
        {
            Task[] tasks = new Task[]
            {
                new Task { Name = "Task 1", Time = 500},
                new Task { Name = "Task 2", Time = 500},
                new Task { Name = "Task 3", Time = 500},
                new Task { Name = "Task 4", Time = 500},
                new Task { Name = "Task 5", Time = 500}
            };

            var items = Enumerable.Range(0, 100);

            Parallel.ForEach(tasks, task =>
            {
                Console.WriteLine("Starting " + task.Name);
                Thread.Sleep(task.Time);
                Console.WriteLine("Ending " + task.Name);
            });
        }

        static void Main(string[] args)
        {
            Task t1 = new Task { Name = "Task 1", Time = 500 };
            Task t2 = new Task { Name = "Task 2", Time = 500 };

            UsingForEach();

            Parallel.Invoke(() => t1.Launch(), () => t2.Launch());

            Console.ReadKey();
        }
    }
}
