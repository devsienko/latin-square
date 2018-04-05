using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grid.Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            //var allCombinations = GetCombinations("abcdefghi".ToList());
            //foreach (var combi in allCombinations)
            //    Console.WriteLine(combi);

            var size = 13;
            if (size > 26)
            {
                Console.WriteLine("Error! The Latin alphabet contains 26 symbols.");
                return;
            }
            var set = GetSet(size);
            var gridManager = new Manager(size, set);
            MatrixHelper.PrintMatrix(gridManager.Init());
            var paramRowNumber = MatrixHelper.FindMostEmptyLine(gridManager.GetMatrix());
            var subset = GetParamSubset(set, MatrixHelper.GetExistItems(gridManager.GetMatrix(), paramRowNumber));
            MatrixHelper.PreFillMatrix(gridManager.GetMatrix(), paramRowNumber, subset);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            MatrixHelper.PrintMatrix(gridManager.GetMatrix());

            
            //var data = gridManager.GetAsString();

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
