using System;
using System.Threading;

namespace HW2_4
{
    class Program
    {
        static void Main(string[] args)
        {
            AlarmClock clock = new AlarmClock(50);
            clock.OnTick += (send, time) => Console.WriteLine($"current time:{time}");
            clock.OnAlarm += (send, time) => Console.WriteLine($"Time is up!");
            clock.Start();
        }
    }
    class AlarmClock
    {
       

        public delegate void TickHandler(Object send, int time);
        public delegate void AlarmHandler(Object send, int time);
        public event TickHandler OnTick = null;
        public event AlarmHandler OnAlarm = null;
        public AlarmClock(int alarmTime) => AlarmTime = alarmTime;
        public int AlarmTime { get; set; }
        public void Start()
        {
            new Thread(() =>
                {
                    for (int time = 1; time <= AlarmTime; time++)
                    {
                        OnTick?.Invoke(this, time);
                    }
                    OnAlarm?.Invoke(this, AlarmTime);
                }).Start();
        }

    }
}
