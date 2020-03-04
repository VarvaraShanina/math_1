using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
// в данном классе описаны основные операции над матрицами
// функция PrintMatrix печатает матрицу (перегружена 3 раза)
// функция InvertMatrix возвращает обратную матрицу (вычисляется методом Гаусса-Жордана)
// функция TransposeMatrix транспонирует матрицу
// функция MultiplicationMatrix перемножает 2 матрицы
// функция Gauss решает систему линейных уравнений методом Гаусса
/// </summary>

namespace math_1
{
    public class Matrix
    {
        // метод для печати матрицы типа double без округления
        public static void PrintMatrix(int size, double[,] matrix)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                    Console.Write("{0} ", matrix[i, j]);
                Console.WriteLine();
            }
        }

        // метод для печати матрицы типа double с округлением до round знаков
        public static void PrintMatrix(int size, double[,] matrix, int round)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                   Console.Write("{0} ", Math.Round(matrix[i, j], round));
                
                Console.WriteLine();
            }
        }

        // метод для печати матрицы типа int
        public static void PrintMatrix(int size, int[,] matrix)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                    Console.Write("{0} ", matrix[i, j]);
                Console.WriteLine();
            }
        }

        // нахождение обратной матрицы методом Гаусса-Жордана
        public static double[,] InvertMatrix(int size, double[,] A)
        {
            // создаем единичную матрицу для вычислений
            double[,] matrix = A;
            double[,] AA = new double[size, size];
            for (var i = 0; i < size; i++)
                AA[i, i] = 1.0;

            // зануление элементов под главной диагональю
            var n = 0;
            for (var i = 0; i < size - 1; i++)
            {
                // поиск главного элемента
                var max = matrix[i, i];
                n = i;

                for (var k = i + 1; k < size; k++)
                {
                    if (Math.Abs(matrix[k, i]) > Math.Abs(max))
                    {
                        n = k;
                        max = Math.Abs(matrix[k, i]);
                    }
                }

                if (Math.Abs(max) < 0.0000001)
                {
                    Console.WriteLine("\nМатрица вырожденная!");
                    return null;
                }

                // переставляем строки местами, если требуется
                if (n != i)
                {
                    double v;
                    for (var q = 0; q < size; q++)
                    {
                        v = matrix[i, q];
                        matrix[i, q] = matrix[n, q];
                        matrix[n, q] = v;
                        v = AA[i, q];
                        AA[i, q] = AA[n, q];
                        AA[n, q] = v;
                    }
                }

                // зануляем столбец под главной диагональю
                for (var y = i + 1; y < size; y++)
                {
                    var C = matrix[y, i] / matrix[i, i];
                    for (var j = i; j < size; j++)
                    {
                        matrix[y, j] = matrix[y, j] - (C * matrix[i, j]);
                        AA[y, j] = AA[y, j] - (C * AA[i, j]);
                    }
                }
            }

            // зануление элементов над главной диагональю
            for (var i = size - 1; i >= 0; i--)
            {
                for (var y = i - 1; y >= 0; y--)
                {
                    var C = matrix[y, i] / matrix[i, i];
                    for (var j = size - 1; j >= 0; j--)
                    {
                        matrix[y, j] = matrix[y, j] - (C * matrix[i, j]);
                        AA[y, j] = AA[y, j] - (C * AA[i, j]);
                    }
                }
            }

            // преобразовываем диагональную матрицу к единичной
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    AA[i, j] = AA[i, j] / matrix[i, i];
                }
                matrix[i, i] = matrix[i, i] / matrix[i, i];
            }

            return AA;
        }

        public static double[] Gauss(int size, double[,] A, double[] B)
        {
            // реализация прямого хода c выбором главного элемента
            int n;
            var counter = 0; // для подсчета количества перестановок
            for (var i = 0; i < size - 1; i++)
            {
                // поиск главного элемента
                var max = 0.0;
                n = i;

                for (var k = i; k < size; k++)
                {
                    if (Math.Abs(A[k, i]) > Math.Abs(max))
                    {
                        n = k;
                        max = Math.Abs(A[k, i]);
                    }
                }

                if (Math.Abs(max) < 0.00000001)
                {
                    Console.WriteLine("Матрица вырожденная");
                    return null;
                }

                // перестановка двух строк, если главный элемент не на диагонале
                if (n != i)
                {
                    counter++;
                    double v;
                    for (var q = 0; q < size; q++)
                    {
                        v = A[n, q];
                        A[n, q] = A[i, q];
                        A[i, q] = v;
                    }
                    v = B[n];
                    B[n] = B[i];
                    B[i] = v;
                }

                // зануляем столбец 
                for (var y = i + 1; y < size; y++)
                {
                    var C = A[y, i] / A[i, i];
                    for (var j = i; j < size; j++)
                    {
                        A[y, j] = A[y, j] - (C * A[i, j]);
                    }
                    B[y] = B[y] - (C * B[i]);
                }
            }

            if (Math.Abs(A[size - 1, size - 1]) < 0.00000001)
            {
                Console.WriteLine("Система линейных уравнений является неопределнной\nМатрица является выражденной");
                return null;
            }

            // обратный ход
            double[] x = new double[size];
            for (int i = size - 1; i >= 0; i--)
            {
                double tmp = 0;
                for (int j = i + 1; j < size; j++)
                {
                    tmp += A[i, j] * x[j];
                }
                x[i] = (B[i] - tmp) / A[i, i];
            }
            
            return x;
        }

        // вычисление транспонированной матрицы
        public static double[,] TransposeMatrix(int size, double[,] matrix)
        {
            double[,] A = new double[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = i; j < size; j++)
                {
                    A[i, j] = matrix[j, i];
                    A[j, i] = matrix[i, j];
                }
            }
            return A;
        }

        // перемножение двух матриц
        public static double[,] MultiplicationMatrix(int size, double[,] A, double[,] B)
        {
            double[,] matrix = new double[size, size];
            for (var k = 0; k < size; k++)
            {
                for (var i = 0; i < size; i++)
                {
                    for (var j = 0; j < size; j++)
                    {
                        matrix[k, i] += A[k, j] * B[j, i];
                    }
                }
            }
            return matrix;
        }

        // end
    }
}
