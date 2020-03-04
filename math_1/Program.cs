using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace math_1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double[,] A;         // исходная матрица А
            double[,] invertA;   // обратная матрица к А
            double[] B;          // вектор B
            double[] x;          // массив ответов
            double[] r;          // невязка
            int size;            // размер матрицы

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            line = file.ReadLine();
            size = Convert.ToInt32(line);

            A = new double[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    line = file.ReadLine();
                    A[i, j] = Convert.ToDouble(line);
                }
            }

            B = new double[size];
            for (var i = 0; i < size; i++)
            {
                line = file.ReadLine();
                B[i] = Convert.ToDouble(line);
            }

            Console.WriteLine("Исходная матрица A: ");
            Matrix.PrintMatrix(size, A); // метод для печати матрицы
            Console.WriteLine("\nCтолбец свободных членов В:");
            for (var i = 0; i < size; i++) 
                Console.WriteLine("{0}", B[i]);

            Console.WriteLine();

            invertA = new double[size, size];
            Console.WriteLine("Обратная матрица: ");
            invertA = Matrix.InvertMatrix(size, (double[,])A.Clone()); // нахождение обратной матрицы методом Гаусса
            Matrix.PrintMatrix(size, invertA, 3); // метод для печати матрицы
            Console.WriteLine();

            x = new double[size];
            x = Matrix.Gauss(size, (double[,])A.Clone(), (double[])B.Clone());
            if (x != null)
            {
                Console.WriteLine("Решение системы: ");
                for (var i = 0; i < size; i++)
                {
                    Console.WriteLine("x{0} = {1}", i + 1, x[i]);
                }
            }
            Console.WriteLine();

            Console.WriteLine("Невязка: ");
            r = new double[size];
            for (int i = 0; i < size; i++)
            {
                double t = 0;
                for (int j = 0; j < size; j++)
                    t += A[i, j] * x[j];
                r[i] = B[i] - t;
            }

            for (int i = 0; i < size; i++)
                Console.WriteLine("r{0} = {1}", i + 1, r[i]);
            Console.WriteLine();

            Console.WriteLine("Проверка решения. А * А^(-1): ");
            double[,] E = Matrix.MultiplicationMatrix(size, A, invertA);
            Matrix.PrintMatrix(size, E, 3); // метод для печати матрицы

            Console.Read();
        }
    }
}
