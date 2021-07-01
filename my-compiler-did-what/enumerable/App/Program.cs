using System;
using System.Collections.Generic;
using System.Linq;

foreach(var n in Fibonacci().Take(10))
{
    Console.Write($"{n}, ");
}
Console.WriteLine($"{Fibonacci().ElementAt(10)}");


static IEnumerable<int> Fibonacci()
{
    yield return 0;

    int value = 1;
    int next = 1;
    while (true)
    {
        yield return value;

        int t = value;
        value = next;
        next += t;
    }
}