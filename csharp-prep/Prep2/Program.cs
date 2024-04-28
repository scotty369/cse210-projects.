using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        double gradePercentage;
        if (!double.TryParse(Console.ReadLine(), out gradePercentage))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        // Determine the letter grade
        string letterGrade;
        if (gradePercentage >= 90)
        {
            letterGrade = "A";
        }
        else if (gradePercentage >= 80)
        {
            letterGrade = "B";
        }
        else if (gradePercentage >= 70)
        {
            letterGrade = "C";
        }
        else if (gradePercentage >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }

        // Output the letter grade
        Console.WriteLine($"Your letter grade is: {letterGrade}");

        // Determine if the user passed the course
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Sorry, you did not pass the course.");
        }
    }
}
