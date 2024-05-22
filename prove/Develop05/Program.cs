using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class Goal
{
    public string Name { get; protected set; }
    public int Points { get; protected set; }
    public bool IsCompleted { get; protected set; }

    [JsonConstructor]
    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
}

public class SimpleGoal : Goal
{
    [JsonConstructor]
    public SimpleGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return IsCompleted ? "[X]" : "[ ]";
    }
}

public class EternalGoal : Goal
{
    [JsonConstructor]
    public EternalGoal(string name, int points) : base(name, points) { }

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetStatus()
    {
        return "[∞]";
    }
}

public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; private set; }

    [JsonConstructor]
    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            CurrentCount++;
            if (CurrentCount >= TargetCount)
            {
                IsCompleted = true;
                return Points + BonusPoints;
            }
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return IsCompleted ? $"[X] Completed {CurrentCount}/{TargetCount}" : $"[ ] Completed {CurrentCount}/{TargetCount}";
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main(string[] args)
    {
        LoadGoals();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            DisplayGoals();
            Console.WriteLine($"\nScore: {score}");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Simple Goal");
            Console.WriteLine("2. Add Eternal Goal");
            Console.WriteLine("3. Add Checklist Goal");
            Console.WriteLine("4. Record Event");
            Console.WriteLine("5. Save and Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    AddSimpleGoal();
                    break;
                case "2":
                    AddEternalGoal();
                    break;
                case "3":
                    AddChecklistGoal();
                    break;
                case "4":
                    RecordEvent();
                    break;
                case "5":
                    SaveGoals();
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    static void AddSimpleGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new SimpleGoal(name, points));
    }

    static void AddEternalGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());
        goals.Add(new EternalGoal(name, points));
    }

    static void AddChecklistGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter points per completion: ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("Enter target count: ");
        int targetCount = int.Parse(Console.ReadLine());
        Console.Write("Enter bonus points: ");
        int bonusPoints = int.Parse(Console.ReadLine());
        goals.Add(new ChecklistGoal(name, points, targetCount, bonusPoints));
    }

    static void RecordEvent()
    {
        Console.WriteLine("\nSelect a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }

        int choice = int.Parse(Console.ReadLine()) - 1;
        if (choice >= 0 && choice < goals.Count)
        {
            score += goals[choice].RecordEvent();
        }
        else
        {
            Console.WriteLine("Invalid choice, try again.");
        }
    }

    static void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine($"{goal.GetStatus()} {goal.Name}");
        }
    }

    static void SaveGoals()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new { goals, score }, options);
        File.WriteAllText("goals.json", jsonString);
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.json"))
        {
            string jsonString = File.ReadAllText("goals.json");
            var data = JsonSerializer.Deserialize<GoalData>(jsonString);
            goals = data.goals;
            score = data.score;
        }
    }
}

public class GoalData
{
    public List<Goal> goals { get; set; }
    public int score { get; set; }
}
