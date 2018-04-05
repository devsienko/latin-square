using System.Linq;
using System.Collections.Generic;

namespace Grid.Manager
{
    public class Manager
    {
        private string _set { get; set; }
        private char[,] _matrix { get; set; }
        private int _matrixSize { get; set; }

        public Manager(int size, string set)
        {
            _set = set;
            _matrixSize = size;
            _matrix = new char[_matrixSize, _matrixSize];
        }

        public char[,] Init()
        {
            var subset = new string(_set.Substring(0, _matrixSize).ToArray());
            var initializer = new RandomInitializator(_matrix, subset);
            initializer.Init();
            return _matrix;
        }

        public char[,] GetMatrix()
        {
            return _matrix;
        }

        public string GetAsString()
        {
            var lines = new string[_matrixSize];
            for (var i = 0; i < _matrixSize; i++)
            {
                var stringChars = new List<char>();
                for (var j = 0; j < _matrixSize; j++)
                    stringChars.Add(_matrix[i, j]);
                lines[i] = new string(stringChars.ToArray());
            }
            var result = lines.Aggregate((i, j) => i + "\n" + j);
            return result;
        }
    }
}


