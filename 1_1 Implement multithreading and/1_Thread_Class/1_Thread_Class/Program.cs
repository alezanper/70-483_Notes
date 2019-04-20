using System;
using System.Threading;

namespace _1_Thread_Class
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();
            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("Main Task 1, {0} %", i*20 );
                Thread.Sleep(0);
            }
            t.Join();
        }

        public static void ThreadMethod()
        {
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine("Other Task 2, {0} %", i*10);
            Thread.Sleep(0);
        }
    }
}
}
