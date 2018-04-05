using System;
using System.Threading;

namespace Grid.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = int.Parse(args[0]);
            var data = args[1];
            var id = int.Parse(args[2]);
            //var worker = new GridWorker(size, data);
            //worker.Calc();
            if (id == 3)
                Thread.Sleep(6000);
            Console.WriteLine(size);
            Console.WriteLine(data);
            Console.WriteLine(id);
        }
    }
}
