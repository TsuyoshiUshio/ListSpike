using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SampleString
{
    class Program
    {
        static void Main(string[] args)
        {

            var InstancesControllerSegment = "/instances/";
            var path = "/admin/extensions/DurableTaskExtension/instances/";
            int i = path.IndexOf(InstancesControllerSegment, StringComparison.OrdinalIgnoreCase);

            i += InstancesControllerSegment.Length;
            int nextSlash = path.IndexOf('/', i);
            string instanceId = path.Substring(i);
            if(instanceId == string.Empty)
            {
                Console.WriteLine($"Empty instanceId: {instanceId}");
            }
            TestAddList();
            Console.ReadLine();
        }

        public class SomeClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public static void TestAddList()
        {
            var number = 100000000;
            // Add 10000 instances to the List. 
            var watch = new Stopwatch();
            watch.Start();

            var list = new List<SomeClass>(number);
            list.Add(
                new SomeClass
                {
                    Name = "ushio",
                    Age = 47
                }
                );
            watch.Stop();
            Console.WriteLine($"List With number constructor: {watch.ElapsedMilliseconds}");
            watch.Restart();
            list = new List<SomeClass>(number);
            list.Add(
                new SomeClass
                {
                    Name = "ushio",
                    Age = 47
                });
            watch.Stop();
            Console.WriteLine($"List without constructor: {watch.ElapsedMilliseconds}");
            RunoutThreading().GetAwaiter().GetResult();
    
        }

        private static async Task SomeExecAsync()
        {
            // Console.WriteLine($"SomeExecAsync(): Current: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000 * 3);
        }

        public static async Task RunoutThreading()
        {
            var watch = new Stopwatch();
            watch.Start();
            var number = 100000;
            var list = new List<Task>(number);
            for(var i = 0; i < number; i++)
            {
                list.Add(SomeExecAsync());
            }
            watch.Stop();
            Console.WriteLine($"RunoutThread finished: elapse : {watch.ElapsedMilliseconds}");
            await Task.WhenAll(list);
        }

    }
}
