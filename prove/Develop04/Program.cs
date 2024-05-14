using System;

public abstract class MindfulnessActivity
{
    protected string name;
    protected string description;
    protected int durationSeconds;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void StartActivity()
    {
        DisplayStartingMessage();

        Console.WriteLine("Get ready to begin...");
        Thread.Sleep(3000); 

        PerformActivity();

        Console.WriteLine("\nGreat job!");
        Console.WriteLine($"You have completed the {name} activity for {durationSeconds} seconds.");
        Thread.Sleep(3000); 
    }

    protected void DisplayStartingMessage()
    {
        Console.WriteLine($"--- {name} Activity ---");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        durationSeconds = Convert.ToInt32(Console.ReadLine());
    }

    protected abstract void PerformActivity();
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through deep breathing.")
    {
    }

    protected override void PerformActivity()
    {
        int remainingSeconds = durationSeconds;
        while (remainingSeconds > 0)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(2000); 

            Console.WriteLine("Breathe out...");
            Thread.Sleep(2000); 

            remainingSeconds -= 4; 
        }
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times of strength and resilience.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        int remainingSeconds = durationSeconds;

        while (remainingSeconds > 0)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);

            foreach (string question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(3000); 
            }

            remainingSeconds -= (prompts.Length * questions.Length * 3); 
        }
    }
}

public class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine(prompt);
        Console.WriteLine($"You have {durationSeconds} seconds to list items...");

        Thread.Sleep(durationSeconds * 1000);

        int itemsCount = random.Next(5, 15);
        Console.WriteLine($"You listed {itemsCount} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
