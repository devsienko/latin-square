using Grid.Manager;
using System;
using System.IO;
using System.Threading;

namespace Grid.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new InvalidOperationException("args");
            Work(args[0]);
            //Work("ba#a ");

            //var size = int.Parse(args[0]);
            //var data = args[1];
            //var id = int.Parse(args[2]);
            //GridWorker.GetMatrix(data);
            ////var worker = new GridWorker(size, data);
            ////worker.Calc();

            //Console.WriteLine(size);
            //Console.WriteLine(data);
            //Console.WriteLine(id);
        }

        static void Work(string matrixAsOneLineString)
        {
            //var lsPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\ls.txt";
            //while (true)
            //{
            //    if (File.Exists(lsPath))
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Wait 1 sec.");
            //        Thread.Sleep(1000);
            //    }
            //}
            //var lsData = File.ReadAllText(lsPath);
            var lsData = matrixAsOneLineString.Replace('#', '\n');

            var worker = new GridWorker(GridWorker.GetMatrix(lsData));
            try
            {
                var grid = worker.Calc();
                MatrixHelper.PrintMatrix(grid);
                var dataToWrite = MatrixHelper.GetAsString(grid);
                var lsResult = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\result\ls.txt";
                File.WriteAllText(lsResult, dataToWrite);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
