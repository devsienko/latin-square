using System;
using System.Collections.Generic;
using System.Linq;
namespace Grid.Manager
{
    public class RandomInitializator
    {
        public string _symbolsSet { get; set; }
        public int MatrixN { get; set; }
        public char[,] Matrix { get; set; }

        public RandomInitializator(char[,] matrix, string symbolsSet)
        {
            _symbolsSet = symbolsSet;
            Matrix = matrix;
            MatrixN = (int)Math.Sqrt(Matrix.Length);
        }

        public void Init()
        {
            var random = new Random();
            for (var i = 0; i < MatrixN; i++)
            {
                for (var j = 0; j < MatrixN; j++)
                {
                    if (random.Next(0, 100) > 70)
                        Matrix[i, j] = GetNext(i, j);
                    else
                        Matrix[i, j] = ' ';
                }
            }
        }

        private char GetNext(int row, int column)
        {
            if (_symbolsSet.Length == 1)
            {
                var r = _symbolsSet.First();
                _symbolsSet = "";
                return r;
            }
            var subset = GetSubSet(row, column);
            var random = new Random();
            var index = (subset.Length - 1) % random.Next(1, MatrixN);
            var result = subset[index];
            return result;
        }

        private string GetSubSet(int row, int column)
        {
            var prevValues = GetPrevValues(row, column);
            var result = _symbolsSet.Except(prevValues).ToArray();
            return new string(result);
        }

        private string GetPrevValues(int row, int column)
        {
            var result = new List<char>();
            //in column
            for (var i = 0; i < row; i++)
                if(Matrix[i, column] != 0)
                    result.Add(Matrix[i, column]);
            //in row
            for (var j = 0; j < MatrixN; j++)
                if (Matrix[row, j] != 0)
                    result.Add(Matrix[row, j]);
            return new string(result.ToArray());
        }
    }
}


