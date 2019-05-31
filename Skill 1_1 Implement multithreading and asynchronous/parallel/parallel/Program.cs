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

        static Task[] tasks = new Task[]
            {
                new Task { Name = "Task 1", Time = 500},
                new Task { Name = "Task 2", Time = 500},
                new Task { Name = "Task 3", Time = 500},
                new Task { Name = "Task 4", Time = 500},
                new Task { Name = "Task 5", Time = 500},
                new Task { Name = "Task 6", Time = 500},
                new Task { Name = "Task 7", Time = 500},
                new Task { Name = "Task 8", Time = 500},
                new Task { Name = "Task 9", Time = 500},
                new Task { Name = "Task 10", Time = 500}
            };

        static void SimpleInvoke()
        {
            Parallel.Invoke(() => tasks[0].Launch(), () => tasks[1].Launch());
        }

        static void UsingForEach()
        {        
            Parallel.ForEach(tasks, task =>
            {
                task.Launch();
            });
        }

        static void UsingFor()
        {
            Parallel.For(0, tasks.Length, i =>
            {
                tasks[i].Launch();
            });
        }

        static void ControlLoop()
        {
            ParallelLoopResult result = Parallel.For(0, tasks.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 2)
                    loopState.Stop();
                tasks[i].Launch();
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
        }

        static void UsingLinq()
        {
            var result = from task in tasks.AsParallel()
                             //.AsOrdered()     Para 
                             //.WithDegreeOfParallelism(4)
                             //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                         where task.Name.Equals("Task 6")
                         select task;

            foreach (var task in result)
                Console.WriteLine(task.Name);
        }

        static void Main(string[] args)
        {
            SimpleInvoke();
            UsingForEach();
            UsingFor();
            ControlLoop();
            UsingLinq();
            Console.ReadKey();
        }
    }
}
