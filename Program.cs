using System;
using System.Threading.Tasks;

namespace Parallel_Programming_Matrix_Calculation
{
    internal class Program
    {
        //Function for Parallel multiplication
        private static int[,] ParallelMult(int[,] x, int[,] y)
        {
            DateTime start = DateTime.Now;
            Task[] tasks = new Task[x.GetLength(0)];
            int[,] a = new int[x.GetLength(0), y.GetLength(1)];
            for (int v = 0; v < x.GetLength(0); v++)
            {
                int index = v;
                tasks[v] = Task.Factory.StartNew(() =>
                {
                    for (int n = 0; n < y.GetLength(1); n++)
                    {
                        for (int b = 0; b < y.GetLength(0); b++)
                        {
                            a[index, n] += x[index, b] * y[b, n];
                        }
                    }
                }
                );
            }
            Task.WaitAll(tasks);
            Console.WriteLine("\nFor Parallel multiplication " + (DateTime.Now - start).TotalSeconds.ToString() + "Seconds\n");
            return a;
        }

        //Function for Sequential multiplication
        private static int[,] SequentialMult(int[,] x, int[,] y)
        {
            DateTime start = DateTime.Now;
            int[,] a = new int[x.GetLength(0), y.GetLength(1)];
            for (int v = 0; v < x.GetLength(0); v++)
            {
                for (int n = 0; n < y.GetLength(1); n++)
                {
                    for (int b = 0; b < y.GetLength(0); b++)
                    {
                        a[v, n] += x[v, b] * y[b, n];
                    }
                }
            }
            Console.WriteLine("\nFor Sequential multiplication " + (DateTime.Now - start).TotalSeconds.ToString() + "Seconds\n");
            return a;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("************************************");
            Console.WriteLine("***Matrix generator Multiplicator***");
            Console.WriteLine("************************************");

            //Array generator bassing on the array size
            int Arrayorder = 0;
            Console.WriteLine("Enter the array size you want to generate");
            Arrayorder = Convert.ToInt32(Console.ReadLine());
            int[,] ArrayOne = new int[Arrayorder, Arrayorder];
            Random Arr = new Random();

            //First Matrix Generator
            Console.WriteLine("First Matrix");
            for (int X = 0; X < Arrayorder; X++)
            {
                for (int y = 0; y < Arrayorder; y++)
                {
                    ArrayOne[X, y] = Arr.Next(0, 100);
                    //if the array the user wants to generate has over 15 rows and columns then it wont be displayed
                    if (Arrayorder < 15) Console.Write(ArrayOne[X, y] + "\t");
                }
                if (Arrayorder < 15) Console.WriteLine();
            }
            Console.WriteLine();

            //Second Matrix Generator
            Console.WriteLine("Second Matrix");
            int[,] ArrayTwo = new int[Arrayorder, Arrayorder];
            for (int x = 0; x < Arrayorder; x++)
            {
                for (int y = 0; y < Arrayorder; y++)
                {
                    ArrayTwo[x, y] = Arr.Next(0, 100);
                    if (Arrayorder < 15) Console.Write(ArrayTwo[x, y] + "\t");
                }
                if (Arrayorder < 15) Console.WriteLine();
            }

            //multiplying the two matrixes using Parrallel Multiplication
            int[,] FirstSolution = ParallelMult(ArrayOne, ArrayTwo);
            if (Arrayorder < 15)
            {
                for (int x = 0; x < Arrayorder; x++)
                {
                    for (int y = 0; y < Arrayorder; y++)
                    {
                        Console.Write(FirstSolution[x, y] + "\t");
                    }
                    if (Arrayorder < 15) Console.WriteLine();
                }
            }

            //multiplying the two matrixes using Sequential Multiplication
            int[,] SecondSolution = SequentialMult(ArrayOne, ArrayTwo);
            if (Arrayorder < 15)
            {
                for (int x = 0; x < Arrayorder; x++)
                {
                    for (int y = 0; y < Arrayorder; y++)
                    {
                        Console.Write(SecondSolution[x, y] + "\t");
                    }
                    if (Arrayorder < 15) Console.WriteLine();
                }
            }
        }
    }
}