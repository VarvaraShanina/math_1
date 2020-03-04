using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using math_1;

namespace math_1.Tests
{
    [TestClass()]
    public class MatrixTest
    {
        
        [TestMethod()]
        public void InvertMatrixTest()
        {
            //arrange
            int size = 3;
            double[,] A = {
                { 2, 5, 7},
                { 6, 3, 4},
                { 5, -2, -3}
            };
            double[,] expected = {
                { 1, -1, 1},
                { -38, 41, -34},
                { 27, -29, 24}
            };

            //act
            double[,] actual = Matrix.InvertMatrix(size, A);
     
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    actual[i, j] = Math.Round(actual[i, j]); 
                }
            }
           
            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GaussTest()
        {
            //arrange
            int size = 3;
            double[,] A = {
                { 2, 1, 1},
                { 1, -1, 0},
                { 3, -1, 2}
            };
            double[] B = new double[] {2, -2, 2};
            double[] expected = new double[] { -1, 1, 3};

            //act
            double[] actual = Matrix.Gauss(size, A, B);
            for (int i = 0; i < size; i++)
            {
                actual[i] = Math.Round(actual[i]);
            }

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TransposeMatrixTest()
        {
            //arrange
            int size = 3;
            double[,] matrix = new double[size, size];
            double[,] expected = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = j;
                    expected[i, j] = i;
                }
            }

            //act
            double[,] actual = Matrix.TransposeMatrix(size, matrix);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MultiplicationMatrixTest()
        {
            //arrange
            int size = 2;
            double[,] A = new double[size, size];
            double[,] B = new double[size, size];
            double[,] expected = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    A[i, j] = 1;
                    B[i, j] = 1;
                    expected[i, j] = 2;
                }
            }

            //act
            double[,] actual = Matrix.MultiplicationMatrix(size, A, B);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}