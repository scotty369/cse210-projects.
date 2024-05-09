using System;

public class Word
{
    public string Text { get; }
    public bool IsHidden { get; set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }
}

public class ScriptureReference
{
    public string Book { get; }
    public int StartVerse { get; }
    public int EndVerse { get; }

    public ScriptureReference(string reference)
    {
        string[] parts = reference.Split(' ');

        Book = parts[0];

        string[] verseParts = parts[1].Split(':');

        if (verseParts.Length == 2)
        {
            StartVerse = int.Parse(verseParts[1]);
            EndVerse = StartVerse;
        }
        else if (verseParts.Length == 3 && verseParts[1].Contains('-'))
        {
            StartVerse = int.Parse(verseParts[1].Split('-')[0]);
            EndVerse = int.Parse(verseParts[1].Split('-')[1]);
        }
        else
        {
            throw new ArgumentException("Invalid scripture reference format.");
        }
    }

    public override string ToString()
    {
        if (StartVerse == EndVerse)
        {
            return $"{Book} {StartVerse}";
        }
        else
        {
            return $"{Book} {StartVerse}-{EndVerse}";
        }
    }
}

public class Scripture
{
    private List<Word> words;

    public ScriptureReference Reference { get; }
    public string Text { get; }

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        Text = text;

        words = Text.Split(' ')
                    .Select(word => new Word(word))
                    .ToList();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{Reference}:");
        foreach (var word in words)
        {
            Console.Write(word.IsHidden ? "***** " : $"{word.Text} ");
        }
        Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        var visibleWords = words.Where(w => !w.IsHidden).ToList(); 

        if (visibleWords.Any()) 
        {
            int indexToHide = random.Next(0, visibleWords.Count);
            visibleWords[indexToHide].IsHidden = true; 
        }
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var scripture = new Scripture("John 3:16", "For God so loved the world...");

        do
        {
            scripture.Display();
            var userInput = Console.ReadLine()?.ToLower();

            if (userInput == "quit")
                break;

            scripture.HideRandomWords();

        } while (!scripture.AllWordsHidden());

        Console.WriteLine("All words are hidden. Press any key to exit...");
        Console.ReadKey();
    }
}
