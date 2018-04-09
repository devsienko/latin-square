using System;
using System.Collections.Generic;
using System.Linq;

namespace Grid.Worker
{
    public class GridWorker
    {
        private char[,] _matrix { get; set; }
        private int _matrixSize { get; set; }
        public string SymbolsSet { get; set; }

        public GridWorker(char[,] matrix)
        {
            _matrix = matrix;
            _matrixSize = (int)Math.Sqrt(_matrix.Length);
            SymbolsSet = new string(Enumerable.Range('a', _matrixSize)
                .Select(item => (char)item)
                .ToArray());
        }

        public static char[,] GetMatrix(string data)
        {
            var lines = data.Split('\n');
            var result = new char[lines.Length, lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var chars = lines[i];
                for (var j = 0; j < lines.Length; j++)
                    result[i, j] = chars[j];
            }
            return result;
        }

        public char[,] Calc()
        {
            var set = "";
            for (var i = 0; i < _matrixSize; i++)
            {
                for (var j = 0; j < _matrixSize; j++)
                {
                    if (_matrix[i, j] == ' ')
                    {
                        _matrix[i, j] = GetNext(i, j);
                    }
                }
            }
            return _matrix;
        }

        private char GetNext(int row, int column)
        {
            if (SymbolsSet.Length == 1)
            {
                var r = SymbolsSet.First();
                SymbolsSet = "";
                return r;
            }
            var subset = GetSubSet(row, column);
            var random = new Random();
            var index = (subset.Length - 1) % random.Next(1, _matrixSize);
            var result = subset[index];
            return result;
        }

        private string GetSubSet(int row, int column)
        {
            var prevValues = GetPrevValues(row, column);
            var result = SymbolsSet.Except(prevValues).ToArray();
            if (result.Length == 0)
                throw new InvalidOperationException("Impossible to build a latin square.");
            return new string(result);
        }

        private string GetPrevValues(int row, int column)
        {
            var result = new List<char>();
            //in column
            for (var i = 0; i < row; i++)
                if (_matrix[i, column] != 0)
                    result.Add(_matrix[i, column]);
            //in row
            for (var j = 0; j < _matrixSize; j++)
                if (_matrix[row, j] != 0)
                    result.Add(_matrix[row, j]);
            return new string(result.ToArray());
        }
    }
}
