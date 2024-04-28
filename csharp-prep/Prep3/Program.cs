using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        bool playAgain = true;

        while (playAgain)
        {
            int magicNumber = random.Next(1, 101); // Generate a random number between 1 and 100

            Console.WriteLine("Welcome to Guess My Number Game!");
            Console.WriteLine("I've picked a number between 1 and 100. Try to guess it!");

            int guessCount = 0;
            int userGuess = -1;

            while (userGuess != magicNumber)
            {
                Console.Write("\nWhat is your guess? ");
                if (!int.TryParse(Console.ReadLine(), out userGuess))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }

                guessCount++;
            }

            Console.WriteLine($"You guessed it! The magic number was {magicNumber}. It took you {guessCount} guesses.");

            Console.Write("\nDo you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().Trim().ToLower();

            if (playAgainInput != "yes")
            {
                playAgain = false;
                Console.WriteLine("Thank you for playing!");
            }
        }
    }
}