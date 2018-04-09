using System;
using System.Collections.Generic;
using System.Linq;

namespace Grid.Manager
{
    public class MatrixHelper
    {
        public static char[,] PreFillMatrix(char[,] matrix, int rowNumber, string data)
        {
            var result = (char[,])matrix.Clone();
            var matrixSize = (int)Math.Sqrt(result.Length);
            for (var i = 0; i < matrixSize; i++)
            {
                if (result[rowNumber, i] == ' ')
                {
                    result[rowNumber, i] = data.First();
                    data = data.Substring(1);
                }
            }
            if (data.Length != 0)
                throw new InvalidOperationException("it's impossible!");
            return result;
        }

        public static string GetExistItems(char[,] matrix, int rowNumber)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            var result = new List<char>();
            for (var i = 0; i < matrixSize; i++)
            {
                var c = matrix[rowNumber, i];
                if (c != ' ')
                    result.Add(c);
            }
            return new string(result.ToArray());
        }

        public static void PrintMatrix(char[,] matrix)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            for (var i = 0; i < matrixSize; i++)
            {
                for (var j = 0; j < matrixSize; j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write('|');
                }
                DrawUnderline(matrixSize);
            }
        }

        public static string GetAsString(char[,] matrix)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            var lines = new string[matrixSize];
            for (var i = 0; i < matrixSize; i++)
            {
                var stringChars = new List<char>();
                for (var j = 0; j < matrixSize; j++)
                    stringChars.Add(matrix[i, j]);
                lines[i] = new string(stringChars.ToArray());
            }
            var result = lines.Aggregate((i, j) => i + "\n" + j);
            return result;
        }

        public static string GetAsOneLineString(char[,] matrix)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            var lines = new string[matrixSize];
            for (var i = 0; i < matrixSize; i++)
            {
                var stringChars = new List<char>();
                for (var j = 0; j < matrixSize; j++)
                    stringChars.Add(matrix[i, j]);
                lines[i] = new string(stringChars.ToArray());
            }
            var result = lines.Aggregate((i, j) => i + "#" + j);
            return result;
        }

        public static int FindMostEmptyLine(char[,] matrix)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            var result = 0;
            var maxLength = 0;
            for (var i = 0; i < matrixSize; i++)
            {
                var currentLength = 0;
                for (var j = 0; j < matrixSize; j++)
                {
                    if (matrix[i, j] == ' ')
                        currentLength++;
                }
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    result = i;
                }
            }
            return result;
        }

        private static void DrawUnderline(int matrixSize)
        {
            Console.WriteLine();
            for (var j = 0; j < matrixSize * 2; j++)
                Console.Write('-');
            Console.WriteLine();
        }
    }
}


