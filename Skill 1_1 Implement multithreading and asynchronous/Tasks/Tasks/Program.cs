using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
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

        static void UsingTask()
        {
            Task myTask = new Task(() => processes[0].Launch());
            myTask.Start();
            myTask.Wait();
        }

        static void UsingRun()
        {
            Task myTask = Task.Run(() => processes[0].Launch());
            myTask.Wait();
        }

        static void ReturningTaskValue()
        {
            Task<int> myTask = Task.Run(() =>
            {
                return processes[0].Time;
            });

            Console.WriteLine(myTask.Result);
        }

        static void WaitFotAllTasks()
        {
            int i = 0;
            Task[] Tasks = new Task[processes.Length];
            for (i = 0; i < processes.Length; i++)
            {
                int j = i;
                Tasks[i] = Task.Run(() => processes[j].Launch());
            }
            //Task.WaitAny(Tasks);
            Task.WaitAll(Tasks);

            Console.WriteLine("Final task: " + i);
        }

        public static void HelloTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello");
        }
        public static void WorldTask()
        {
            Thread.Sleep(1000);
            Console.WriteLine("World");
        }

        static void ContinueTask()
        {
            Task task = Task.Run(() => processes[0].Launch());
            task.ContinueWith((prevTask) => processes[1].Launch());
        }

        public static void DoChild(object state)
        {
            Console.WriteLine("Child {0} starting", state);
            Thread.Sleep(2000);
            Console.WriteLine("Child {0} finished", state);
        }

        static void UsingChildTasks()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent starts");
                for (int i = 0; i < 10; i++)
                {
                    int j = i;
                    Task.Factory.StartNew((x) => DoChild(x), j, TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait();
        }

        static void Main(string[] args)
        {
            UsingTask();
            UsingRun();
            ReturningTaskValue();
            WaitFotAllTasks();
            ContinueTask();
            UsingChildTasks();
            Console.ReadKey();
        }
    }
}
