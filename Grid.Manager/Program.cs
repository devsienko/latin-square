using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grid.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 3;
            if (size > 26)
            {
                Console.WriteLine("Error! The Latin alphabet contains 26 symbols.");
                return;
            }
            var set = GetSet(size);
            var gridManager = new Manager(size, set);
            Console.WriteLine("Source matrix:");
            Console.WriteLine();
            MatrixHelper.PrintMatrix(gridManager.Init());
            var paramRowNumber = MatrixHelper.FindMostEmptyLine(gridManager.GetMatrix());
            var subset = GetParamSubset(set, MatrixHelper.GetExistItems(gridManager.GetMatrix(), paramRowNumber));
            var allCombinations = GetCombinations(subset.ToList());
            var tasksData = new List<string>();
            for (var i = 0; i < allCombinations.Count; i++)
            {
                var matrix = MatrixHelper.PreFillMatrix(gridManager.GetMatrix(), paramRowNumber, allCombinations[i]);
                tasksData.Add(MatrixHelper.GetAsOneLineString(matrix));
            }

            CreateJdf(tasksData );

            WaitResult();

            //var worker = new GridWorker(GridWorker.GetMatrix(data));
            //MatrixHelper.PrintMatrix(worker.Calc());

            //var index = 1;
            //var calcResult = WorkerInvoker.Invoke(data, index);
            //foreach (var d in calcResult)
            //{
            //    Console.WriteLine(d);
            //}

            //var cts = new CancellationTokenSource();
            //var pOptions = new ParallelOptions();
            //pOptions.CancellationToken = cts.Token;
            //pOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            //Parallel.ForEach(rawData, pOptions, rd =>
            //{
            //    var data = WorkerInvoker.Invoke(rd, index++);
            //    //cts.Cancel();
            //    //pOptions.CancellationToken.ThrowIfCancellationRequested();

            //    foreach (var line in data)
            //    {
            //        Console.WriteLine(line);
            //    }

            //});
        }

        static void CreateJdf(List<string> tasksData)
        {
            var workerPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\Grid.Worker\bin\Debug\Grid.Worker.exe";
            var jdfPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\jdf.txt";
            File.Delete(jdfPath);
            var resultPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\result\output$TASK.txt";
            WriteToJdj("job:\n");
            WriteToJdj("\tname: fill-lat-square\n");
            WriteToJdj("\tinit:\n");
            WriteToJdj("\t\tput " + workerPath + " Grid.Worker.exe\n");
            WriteToJdj("\tfinal:\n");
            WriteToJdj("\t\tget result.dat " + resultPath + "\n");
            foreach(var d in tasksData)
            {
                WriteToJdj("task: \n");
                WriteToJdj("\tremote: remote: Grid.Worker.exe \"" + d + "\" > result.dat \n");
            }
        }

        static void WriteToJdj(string data)
        {
            var lsPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\jdf.txt";
            File.AppendAllText(lsPath, data);

        }

        static void Work()
        {
            var size = 13;
            if (size > 26)
            {
                Console.WriteLine("Error! The Latin alphabet contains 26 symbols.");
                return;
            }
            var set = GetSet(size);
            var gridManager = new Manager(size, set);
            Console.WriteLine("Source matrix:");
            Console.WriteLine();
            MatrixHelper.PrintMatrix(gridManager.Init());
            var paramRowNumber = MatrixHelper.FindMostEmptyLine(gridManager.GetMatrix());
            var subset = GetParamSubset(set, MatrixHelper.GetExistItems(gridManager.GetMatrix(), paramRowNumber));
            MatrixHelper.PreFillMatrix(gridManager.GetMatrix(), paramRowNumber, subset);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Pre filled matrix:");
            Console.WriteLine();
            MatrixHelper.PrintMatrix(gridManager.GetMatrix());

            var lsPath = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\ls.txt";
            File.WriteAllText(lsPath, gridManager.GetAsString());

            //WaitResult();
        }

        private static void WaitResult()
        {
            var resultDir = @"C:\Users\daniil_\YandexDisk\Политех\Последняя сессия\Grid\latin-square\data\result";
            string[] files = null;
            while (true)
            {
                files = Directory.GetFiles(resultDir);
                if (files.Any())
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Waiting of result 2 sec.");
                    Thread.Sleep(1000);
                }
            }
            var lsResult = files.First();
            var lsData = File.ReadAllText(lsResult);
            var matrix = GridWorker.GetMatrix(lsData);

            Console.WriteLine("");
            Console.WriteLine("Result");
            Console.WriteLine("");
            MatrixHelper.PrintMatrix(matrix);
            Console.WriteLine("");
            Console.WriteLine("todo: stop job");
            Console.WriteLine("");
        }

        private static List<string> GetCombinations(IList<Char> chars)
        {
            var result = new List<String>();
            var combis = new Facet.Combinatorics.Permutations<char>(chars, Facet.Combinatorics.GenerateOption.WithoutRepetition);
            result.AddRange(combis.Select(c => string.Join("", c)));
            return result;
        }

        private static string GetSet(int n)
        {
            var alphabet = new string(Enumerable.Range('a', 'z').Select(item => (char)item).ToArray());
            var result = alphabet.Substring(0, n);
            return result;
        }

        private static string GetParamSubset(string set, string existedItems)
        {
            var result = new string(set.Except(existedItems).ToArray());
            return result;
        }
    }
}
