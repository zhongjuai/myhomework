using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
namespace HW2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            double areaSum = 0;
            for(int i = 1; i < 11; i++)
            {
                areaSum += ShapeFactory.MakeShape().Area();
            }
            Console.WriteLine($"sum of area:{areaSum}");
        }
    }
    class ShapeFactory
    {
        public static IShape MakeShape()
        {
            int randNum = rand.Next(4);
            if (randNum == 0)
            {
                return new Circle(rand.Next(1, 10));
            }
            else if (randNum == 1)
            {
                return new Square(rand.Next(1, 10));
            }
            else if (randNum == 2)
            {
                return new Rectangle(rand.Next(1, 10), rand.Next(1, 10));
            }
            else 
            {
                int a = rand.Next(1, 10), b = rand.Next(2, 10);
                int c = a + b - 1;
                return new Triangle(a,b,c);
            }           
        }
        private static readonly Random rand = new Random();
    }
    internal interface IShape
    {
        public double Area();
        bool IsLegal();
    }
    class Circle : IShape
    {
        double Radius;
        private int v;

        public Circle(int v)
        {
            this.v = v;
        }

        public double Area() { return Math.PI * Radius * Radius; }
        public bool IsLegal() => Radius > 0;
    }
    class Rectangle : IShape
    {
        double Length;
        double Width;
        private int v1;
        private int v2;

        public Rectangle(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public double Area() { return Length * Width; }
        public bool IsLegal() => Length > 0 && Width > 0;
    }
    class Square : IShape
    {
        double Side;
        private int v;

        public Square(int v)
        {
            this.v = v;
        }

        public double Area() { return Side * Side; }
        public bool IsLegal() => Side > 0;
    }
    class Triangle : IShape
    {
        double Side1;
        double Side2;
        double Side3;
        private int v1;
        private int v2;
        private int v3;

        public Triangle(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public double Area()
        {

            double a = Side1, b = Side2, c = Side3;
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));

        }
        public bool IsLegal()
        {
            if (Side1 <= 0 || Side2 <= 0 || Side3 <= 0)
            {
                return false;
            }
            double[] arr = new double[] { Side1, Side2, Side3 };
            Array.Sort(arr);
            if (arr[0] + arr[1] <= arr[2])
            {
                return false;
            }
            return true;


        }
    }
}
