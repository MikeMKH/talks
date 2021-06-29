using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var conference = "That";
            Console.WriteLine($"Hello {conference} Conference!");

            Action<string> sorry =
                conference => Console.WriteLine(
                    $"Sorry, {conference} this is a bit ridiculous.");

            sorry(conference);
            Closing("fun");

            static void Closing(string state)
                => Console.WriteLine($"Hope you find it {state}!");
        }
    }
}
