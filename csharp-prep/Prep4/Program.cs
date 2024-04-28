using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 0)
                break;

            numbers.Add(input);
        }

        if (numbers.Count > 0)
        {
            int sum = numbers.Sum();


            double average = numbers.Average();

            int max = numbers.Max();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");

            var positiveNumbers = numbers.Where(n => n > 0).ToList();
            if (positiveNumbers.Count > 0)
            {
                int minPositive = positiveNumbers.Min();
                Console.WriteLine($"The smallest positive number is: {minPositive}");
            }

            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}