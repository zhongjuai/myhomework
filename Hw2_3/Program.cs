using System;

namespace HW2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new GenericList<int>();
            foreach(int y in new int[] { 6, 3, 4, 1, 2, 7 })
            {
                list.Add(y);
            }
            int minList = list.Head.Date;
            int maxList = list.Head.Date;
            int sum = 0;
            list.ForEach(v =>
            {
                minList = Math.Min(minList, v);
                maxList = Math.Max(maxList, v);
                sum += v;
            });
            Console.WriteLine($"min:{minList},max:{maxList},sum:{sum}");
        }
    }
    class Node<T>
    {
        
        public Node(T t)
        {
            Date = t;
            Next = null;
        }
        public T Date { get; set; }
        public Node<T> Next { get; set; }
    }
    class GenericList<T>
    {
        
        public GenericList(){ head = tail = null; }
        public void Add(T t)
        {
            var n = new Node<T>(t);
            if (tail == null) { head = tail = n; }
            else
            { tail.Next = n;
              tail = n;
            }
        }
        public Node<T> Head { get => head; }
        public object Tail { get; internal set; }

        public void ForEach(Action<T> action)
        {
            for(var p=head;p!=null;p=p.Next){ action(p.Date); }
        }
        private Node<T> head;
        private Node<T> tail;
    }
}
