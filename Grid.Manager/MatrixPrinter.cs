using System;
using System.Collections.Generic;
using System.Linq;

namespace Grid.Manager
{
    public class MatrixHelper
    {
        public static void PreFillMatrix(char[,] matrix, int rowNumber, string data)
        {
            var matrixSize = (int)Math.Sqrt(matrix.Length);
            for (var i = 0; i < matrixSize; i++)
            {
                if (matrix[rowNumber, i] == ' ')
                {
                    matrix[rowNumber, i] = data.First();
                    data = data.Substring(1);
                }
            }
            if (data.Length != 0)
                throw new InvalidOperationException("it's impossible!");
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


