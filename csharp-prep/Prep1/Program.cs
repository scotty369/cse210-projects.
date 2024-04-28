using System;

class Program1
{
    static void Main(string[] args)
    {
        // Prompt for first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Prompt for last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Display formatted name
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}