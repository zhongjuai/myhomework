using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = { { 1, 2, 3, 4 }, { 5, 1, 2, 3 }, { 9, 5, 1, 2 } };
           
            Console.WriteLine($"result:{IsToeplitzMatrix(matrix)}");
        }
            public static bool IsToeplitzMatrix(int[,]matrix) 
            { 
            for(int i = 1; i < matrix.GetLength(0); i++)
                {
                    for (int j = 1; j < matrix.GetLength(1); j++)
                    {
                        if(matrix[i,j]!= matrix[i - 1, j - 1]) { return false; }
                    }
                }
                return true;
            }
            
    }
}

