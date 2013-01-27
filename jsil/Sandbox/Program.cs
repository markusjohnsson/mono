using System;
using System.IO;
using System.Reactive.Linq;

namespace Sandbox
{
    
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("test.xml");

            var fsw = new FileSystemWatcher(Environment.CurrentDirectory);

            Observable
                .Merge(
                    //fsw.GetCreated(), 
                    fsw.GetChanged())
                //.Throttle(
                //    TimeSpan.FromSeconds(10))
                .Subscribe(
                    a => Console.WriteLine("Time to update {0}", a));
            fsw.EnableRaisingEvents = true;

            //Console.WriteLine("Creating"); File.Create("test.xml").Close();
            //Console.WriteLine("Deleting"); File.Delete("test.xml");
            //Console.WriteLine("Creating"); File.Create("test.xml").Close();
            Console.WriteLine("Changing"); File.WriteAllLines("test.xml", new[] { "content" });

            Console.ReadLine();
        }
    }
}
