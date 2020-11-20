using System;
using System.Collections.Generic;
namespace Homework1
{
    class Factor
    {
        static void Main(string[] args)
        {
           /* int n;
            string m;
            Console.WriteLine("请输入一个数据：");
            m = Console.ReadLine();
            n= int.Parse(m);  */ 
            
            Console.WriteLine("请输入一个数据：");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int f = 1; f * f <= n; f++)
            {
                if (n % f == 0) {
                    n = n / f;
                    
                    Console.WriteLine("数据的素数因子："+f);
                }
            }
        }
    }
}
