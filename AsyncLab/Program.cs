using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AsyncLab1
{
    class Program
    {   
        static void Main(string[] args)
        {

            TestCamp.Major().Wait();

            Worker.Enqueue(() =>
            {
                Console.WriteLine("Task 1");
            });

            Worker.Run();

            //Worker.Task.Wait();
            Worker.tcs.Task.Wait();

            //Task t = Task.Run(() => {
            //    Random rnd = new Random();
            //    long sum = 0;
            //    int n = 1000000;
            //    for (int ctr = 1; ctr <= n; ctr++)
            //    {
            //        int number = rnd.Next(0, 101);
            //        sum += number;
            //    }
            //    Console.WriteLine("Total:   {0:N0}", sum);
            //    Console.WriteLine("Mean:    {0:N2}", sum / n);
            //    Console.WriteLine("N:       {0:N0}", n);
            //});
            //t.Wait();

        }
    }

    static class Worker
    {
        static readonly BlockingCollection<Action> m_actions =
            new BlockingCollection<Action>();

        public static Task Task;
        public static TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        static Worker()
        {

        }

        public static void Run()
        {
            Task = Task.Run(() =>
            {
                Action action;
                while (m_actions.TryTake(out action))
                        action();
                tcs.SetResult(true);
                //foreach (var action in m_actions.GetConsumingEnumerable())
                //{
                //    try
                //    {
                //       // action();
                //    }
                //    catch (Exception e)
                //    {
                //        Debug.WriteLine(e);
                //    }
                //}
            });
        }

        public static void Enqueue(Action action)
        {
            m_actions.Add(action);
        }
        public static void Clear()
        {
            Action dumped;
            while (m_actions.TryTake(out dumped)) ;
        }
    }
}
