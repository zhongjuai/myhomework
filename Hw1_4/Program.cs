using System;

namespace Prime
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 100;
            //i是素数时，primes[i]为true
            bool[] primes = new bool[N + 1];
            for(int i=2;i<=N;i++)
            {
                primes[i] = true;
            }
            for(int n=2;n*n<=N+1;n++)
            {
                if (!primes[n])
                    continue;
                for (int mul = 2 * n; mul < primes.Length; mul += n)
                    primes[mul] = false;
            }
            for(int n=2;n<=N; n++)
            {
                if (primes[n]) { Console.WriteLine("素数有：" + n); }
            }
            

        }
    }
}
