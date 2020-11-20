using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace homework4
{
    class Program
    {
        static void Main(string[] args)
        {
            AllOrder a = new AllOrder();
            bool judge_ = true;
            while (judge_)
            {
                Console.WriteLine("输入1添加订单，输入2删除订单，输入3查询订单，输入4显示所有订单，输入5根据订单号为订单排序,输入6序列化订单，输入7反序列化订单，输入8退出");
                string choose1 = Console.ReadLine();
                switch (choose1)
                {
                    case "1": a.addOrder(); break;
                    case "2": a.removeOrder(); break;
                    case "3": Console.WriteLine("输入1根据订单金额查询订单，输入2根据客户名查询订单"); int i = Convert.ToInt32(Console.ReadLine()); a.searchOrder(i); break;
                    case "4": a.ShowOrder(); break;
                    case "5": a.order.Sort(); break;
                    case "6": a.export(); break;
                    case "7": a.import(); break;
                    case "8": judge_ = false; break;
                    default: Console.WriteLine("输入错误"); break;
                }
            }
        }
    }
    [Serializable]
    public class AllOrder : IOrderService    //所有订单
    {
        public List<Order> order = new List<Order>();

        public AllOrder()
        {

        }

        public void export()
        {
            XmlSerializer a = new XmlSerializer(typeof(List<Order>));
            using (FileStream b = new FileStream("order.xml", FileMode.Create))
            {
                a.Serialize(b, this.order);
            }
            Console.WriteLine("序列化完成");
        }

        public void import()
        {
            try
            {
                XmlSerializer a = new XmlSerializer(typeof(List<Order>));
                using (FileStream b = new FileStream("order.xml", FileMode.Open))
                {
                    List<Order> c = (List<Order>)a.Deserialize(b);
                    Console.WriteLine("反序列化结果：");
                    foreach (Order d in c)
                    {
                        Console.WriteLine("订单号 客户 日期 总金额");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("{0} {1} {2} {3}", d.Id, d.Customer, d.Date, d.Money);
                        d.showOrderItem();
                    }
                }
            }
            catch
            {
                Console.WriteLine("序列化系列操作错误");
            }
        }
        public void ShowOrder()
        {

            foreach (Order a in this.order)
            {
                Console.WriteLine("订单号 客户 日期 总金额");
                Console.WriteLine("----------------------------");
                Console.WriteLine("{0} {1} {2} {3}", a.Id, a.Customer, a.Date, a.Money);
                a.showOrderItem();
            }
        }
        public void addOrder()          //增加订单
        {
            try
            {
                Console.WriteLine("请输入订单编号：");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("请输入客户名称：");
                string customer = Console.ReadLine();
                Console.WriteLine("请输入时间：");
                string date = Console.ReadLine();
                Order a = new Order(id, customer, date);
                Console.WriteLine("输入订单项：");
                bool judge = true;
                bool same = false;
                foreach (Order m in this.order)
                {
                    if (m.Equals(a)) same = true;
                }
                if (same) Console.WriteLine("订单号重复");
                else
                {
                    while (judge && !same)
                    {
                        Console.WriteLine("请输入物品名称：");
                        string name = Console.ReadLine();
                        Console.WriteLine("请输入购买数量：");
                        int number = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("请输入单价：");
                        double price = Convert.ToDouble(Console.ReadLine());
                        a.addOrderItem(name, number, price);
                        Console.WriteLine("是否继续添加订单项：");
                        string x = Console.ReadLine();
                        if (x == "否") judge = false;
                        else if (x == "是") continue;
                        else if (x != "否" && x != "是")
                        {
                            Exception e = new Exception();
                            throw e;
                        }
                    }
                    order.Add(a);
                    a.getAllPrice();
                    Console.WriteLine("建立成功");
                    Console.WriteLine("-------------------------");
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }

        }
        public void removeOrder()           //删除订单
        {
            try
            {
                Console.WriteLine("输入订单号删除订单或相应明细：");
                int id = Convert.ToInt32(Console.ReadLine());
                int index = 0;
                foreach (Order a in this.order)
                {
                    if (a.Id == id) index = this.order.IndexOf(a);
                }
                Console.WriteLine("输入1删除订单，输入2继续删除订单明细");
                int choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1: this.order.RemoveAt(index); Console.WriteLine("删除成功"); Console.WriteLine("-----------------"); break;
                    case 2: this.order[index].showOrderItem(); this.order[index].RemoveOrderItem(); break;
                    default: Console.WriteLine("输入错误"); break;
                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }

        }

        public void searchOrder(int i)  //查询订单
        {
            try
            {
                switch (i)
                {
                    case 1:
                        int minNum, maxNum;
                        Console.WriteLine("输入要查询的最小金额：");
                        minNum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("输入要查询的最大金额：");
                        maxNum = Convert.ToInt32(Console.ReadLine());


                        var query1 = from s1 in order
                                     where maxNum > s1.Money
                                     orderby s1.Money
                                     select s1;
                        var query3 = from s3 in query1
                                     where s3.Money > minNum
                                     orderby s3.Money
                                     select s3;

                        List<Order> a1 = query3.ToList();

                        foreach (Order b1 in a1)
                        {
                            Console.WriteLine("订单号 客户 日期 总金额");
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("{0} {1} {2} {3}", b1.Id, b1.Customer, b1.Date, b1.Money);
                            b1.showOrderItem();
                        }
                        break;
                    case 2:

                        Console.WriteLine("输入客户名称：");
                        string name1 = Console.ReadLine();

                        var query2 = from s2 in order
                                     where s2.Customer == name1
                                     orderby s2.Money
                                     select s2;
                        List<Order> a2 = query2.ToList();

                        foreach (Order b2 in a2)
                        {
                            Console.WriteLine("订单号 客户 日期 总金额");
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("{0} {1} {2} {3}", b2.Id, b2.Customer, b2.Date, b2.Money);
                            b2.showOrderItem();
                        }
                        break;
                    default: Console.WriteLine("输入错误"); break;

                }
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }


    }
    [Serializable]
    public class Order : IComparable  //单个订单项
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public double Money { get; set; }
        public string Date { get; set; }

        public List<OrderDetail> OrderDetail = new List<OrderDetail>();

        public Order()//无参构造函数
        {
            this.Id = 0;
            this.Customer = string.Empty;
            this.Money = 0;
            this.Date = string.Empty;

        }
        public int CompareTo(object obj)
        {
            Order a = obj as Order;
            return this.Id.CompareTo(a.Id);
        }
        public override bool Equals(object obj)
        {
            Order a = obj as Order;
            return this.Id == a.Id;
        }

        public override int GetHashCode()
        {
            return Convert.ToInt32(Id);
        }
        public Order(int id, string customer, string date)
        {
            this.Id = id;
            this.Customer = customer;
            this.Date = date;
        }
        public void getAllPrice()  //计算总价
        {
            double i = 0;
            foreach (OrderDetail a in this.OrderDetail)
            {
                i = i + a.getPrice();
            }
            this.Money = i;


        }

        public void addOrderItem(string name, int number, double price)   //添加订单项
        {
            OrderDetail a = new OrderDetail(name, number, price);
            this.OrderDetail.Add(a);
        }
        public void RemoveOrderItem() //删除订单项
        {
            try
            {
                Console.WriteLine("请输入订单明细序号删除相应订单明细：");
                int a = Convert.ToInt32(Console.ReadLine());
                this.OrderDetail.RemoveAt(a);
                Console.WriteLine("删除成功");
                Console.WriteLine("-------------------------");
            }
            catch
            {
                Console.WriteLine("输入序号错误");
            }
        }

        public void showOrderItem()  //展示订单项4
        {
            Console.WriteLine("序号 名称 数量 单价");
            foreach (OrderDetail a in this.OrderDetail)
            {

                Console.WriteLine("-----------------------");
                Console.WriteLine("{0} {1} {2} {3}", this.OrderDetail.IndexOf(a), a.Name, a.Number, a.Price);
            }
        }

    }
    [Serializable]
    public class OrderDetail               //订单明细项
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private int number;
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value >= 0) number = value;
                else Console.WriteLine("数量不应该小于0");
            }
        }
        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public OrderDetail()//无参构造函数
        {
            this.Name = string.Empty;
            this.Number = 0;
            this.Price = 0;
        }

        public OrderDetail(string name, int number, double price)
        {
            this.name = name;
            this.number = number;
            this.price = price;
        }
        public double getPrice()
        {
            return this.number * this.price;
        }

    }
    public interface IOrderService        //包含所有订单功能的接口
    {
        void addOrder();
        void removeOrder();
        void searchOrder(int i);
        void export();
        void import();

    }
}


