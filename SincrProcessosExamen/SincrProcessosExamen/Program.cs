
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SincrProcessosExamen
{
    public class Program
    {
        private static int SEM_CAPACITY = 2;
        private static SemaphoreSlim sem = new SemaphoreSlim(SEM_CAPACITY);
        public static int fils;
        public static int increment = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Introdueix el numero de fils:");
            fils = Convert.ToInt32(Console.ReadLine());
            Start();
        }

        static void Start() 
        {
            Thread[] threads = new Thread[fils];

            for (var x = 0; x < fils; x++)
            {
                int j = x + 1;
                threads[x] = new Thread(() => ProcessItem(j))
                {
                    Name = $"Th_{x + 1}"
                };
                threads[x].Start();
            }

            // Wait for all threads to finish
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Increment: {increment}");
        }

        private static void ProcessItem(int X)
        {
            int N = new Random().Next(1, 5);
            int sleepTime = N * 1000;

            for (int i = 0; i < X; i++)
            {
                Console.WriteLine($"Soc el fil número {X}, estic al pas {i + 1} i m’espero {N} segons");
                Thread.Sleep(sleepTime);
                sem.Wait();
                increment = increment + 1;
                Thread.Sleep(sleepTime);
                sem.Release();
                Console.WriteLine("Thread: " + Thread.CurrentThread.Name + " has finished.");
            }
        }
    }
}