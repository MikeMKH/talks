using System;
using System.Threading.Tasks;

await PrintAndWait(TimeSpan.FromMilliseconds(10));
await PrintAndWaitAroundLoop(TimeSpan.FromMilliseconds(10));
await PrintAndWaitInLoop(TimeSpan.FromMilliseconds(10));
await PrintAndWaitInTryFinally(TimeSpan.FromMilliseconds(10));

static async Task PrintAndWait(TimeSpan delay)
{
    Console.WriteLine("before delays");
    await Task.Delay(delay);
    Console.WriteLine("between delays");
    await Task.Delay(delay);
    Console.WriteLine("after delays");
}

static async Task PrintAndWaitAroundLoop(TimeSpan delay)
{
    Console.WriteLine("before delays");
    await Task.Delay(delay);
    for(var i = 0; i < 3; i++)
    {
        Console.WriteLine("between delays");
    }
    await Task.Delay(delay);
    Console.WriteLine("after delays");
}

static async Task PrintAndWaitInLoop(TimeSpan delay)
{
    Console.WriteLine("out of loop before delays");
    for (var i = 0; i < 3; i++)
    {
        Console.WriteLine("in loop before delay");
        await Task.Delay(delay);
        Console.WriteLine("in loop after delay");
    }
    Console.WriteLine("out of loop after delays");
}

static async Task PrintAndWaitInTryFinally(TimeSpan delay)
{
    Console.WriteLine("before try");
    try
    {
        Console.WriteLine("in try before delay");
        await Task.Delay(delay);
        Console.WriteLine("in try after delay");
    }
    finally
    {
        Console.WriteLine("in finally before delay");
        await Task.Delay(delay);
        Console.WriteLine("in finally after delay");
    }
    Console.WriteLine("after try");
}