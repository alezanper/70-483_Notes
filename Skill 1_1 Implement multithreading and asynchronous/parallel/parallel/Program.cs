using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace parallel
{
    class Program
    {
        class Process
        {
            public string Name { get; set; }
            public int Time { get; set; }

            public int Priority { get; set; }

            public void Launch()
            {
                Console.WriteLine(this.Name + " starting");
                Thread.Sleep(this.Time);
                Console.WriteLine(this.Name + " ending");
            }
        }

        static Process[] processes = new Process[]
            {
                new Process { Name = "Process 1", Time = 300, Priority = 1},
                new Process { Name = "Process 2", Time = 100, Priority = 1},
                new Process { Name = "Process 3", Time = 500, Priority = 2},
                new Process { Name = "Process 4", Time = 500, Priority = 3},
                new Process { Name = "Process 5", Time = 100, Priority = 1},
                new Process { Name = "Process 6", Time = 500, Priority = 2},
                new Process { Name = "Process 7", Time = 200, Priority = 3},
                new Process { Name = "Process 8", Time = 100, Priority = 1},
                new Process { Name = "Process 9", Time = 200, Priority = 2},
                new Process { Name = "Process 10", Time = 100, Priority = 3},
            };

        static void SimpleInvoke()
        {
            Parallel.Invoke(() => processes[0].Launch(), () => processes[1].Launch());
        }

        static void UsingForEach()
        {        
            Parallel.ForEach(processes, process =>
            {
                process.Launch();
            });
        }

        static void UsingFor()
        {
            Parallel.For(0, processes.Length, i =>
            {
                processes[i].Launch();
            });
        }

        static void ControlLoop()
        {
            ParallelLoopResult result = Parallel.For(0, processes.Count(), (int i, ParallelLoopState loopState) =>
            {
                if (i == 2)
                    loopState.Stop();
                processes[i].Launch();
            });

            Console.WriteLine("Completed: " + result.IsCompleted);
        }

        static void UsingLinq()
        {
            try
            {
                var result = (from proc in processes.AsParallel()
                         .AsOrdered()
                                  //.WithDegreeOfParallelism(4)
                                  //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                              where proc.Priority == 1
                              select proc).AsSequential().Take(3);

                foreach (var task in result)
                    Console.WriteLine(task.Name);
            }
            catch (Exception e)
            {

            }
            
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
