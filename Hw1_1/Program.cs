using System;

namespace ConsoleA45642pp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 2, 5, 7, 2, 9, 6 };
            //最大值、最小值、平均值和所有数组元素的和。
            int max = a[0];
            int min = a[0];
            int ave;
            int sum=0;
            for(int i=0;i<6;i++)
            {
                if (max < a[i])
                    max = a[i];
                if (min > a[i])
                    min = a[i];
                sum += a[i];
            }
            ave = sum / 6;
            Console.WriteLine("该数组的最大值为"+max);
            Console.WriteLine("该数组的最小值为" + min);
            Console.WriteLine("该数组的平均值为" + ave);
            Console.WriteLine("该数组的总值为" +sum);
        }
    }
}
