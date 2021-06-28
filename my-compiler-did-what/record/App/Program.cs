using System;

namespace App
{
    public record PersonRecord(string FirstName, string LastName);
    public class PersonClass
    {
        public PersonClass(string first, string last)
            => (FirstName, LastName) = (first, last);

        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Class record.");

            var mikeRecord = new PersonRecord("Mike", "Harris");
            var otherMikeRecord = new PersonRecord("Mike", "Harris");

            Console.WriteLine($"\tmikeRecord={mikeRecord}");
            Console.WriteLine($"\tHello {mikeRecord.FirstName}!");

            if (mikeRecord == otherMikeRecord)
            {
                Console.WriteLine($"\tSame old {otherMikeRecord.FirstName}.");
            }
            else
            {
                Console.WriteLine($"\tYou have changed {mikeRecord.FirstName}.");
            }

            Console.WriteLine("Class example.");

            var mikeClass = new PersonClass("Mike", "Harris");
            var otherMikeClass = new PersonClass("Mike", "Harris");

            Console.WriteLine($"\tmikeClass={mikeClass}");
            Console.WriteLine($"\tHello {mikeClass.FirstName}!");

            if (mikeClass == otherMikeClass)
            {
                Console.WriteLine($"\tSame old {otherMikeClass.FirstName}.");
            }
            else
            {
                Console.WriteLine($"\tYou have changed {mikeClass.FirstName}.");
            }
        }
    }
}
