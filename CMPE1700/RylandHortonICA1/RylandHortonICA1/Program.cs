using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RylandHortonICA1
{
    class Program
    {
        static void Main(string[] args)
        {
            int guess;
            int answer;
            int upperBound;
            double average;
            int attempts;
            int gameNumber;
            int upperGuess;
            int lowerGuess;

            bool runProg = true;
            Random rand = new Random();
            do
            {
                average = 0;
                Console.Clear();
                Console.WriteLine("\t\tCMPE 1700 Hi-Low Guesser\n\n");
                upperBound = GetIntValue("Enter the upper bounds of the random number (2 or greater): ", 2);
                gameNumber = GetIntValue("Enter the number of games to play: ", 1);

                for (int i = 0; i < gameNumber; ++i)
                {
                    lowerGuess = 1;
                    upperGuess = upperBound + 1;
                    answer = rand.Next(1, upperBound + 1);
                    guess = 0;
                    attempts = 1;
                    while (guess != answer)
                    {
                        ++attempts;
                        guess = (upperGuess + lowerGuess) / 2;
                        if (answer > guess) lowerGuess = guess + 1;
                        else if (answer < guess) upperGuess = guess;
                    }
                    average += (attempts / (double)gameNumber);
                }
                Console.WriteLine("The game took, on average, {0} attempts to win.", average);
                Console.Write("Press Enter to start again.");
                if (!(Console.ReadKey().Key.Equals(ConsoleKey.Enter)))
                    runProg = false;
            } while (runProg);
        }
        static public int GetIntValue(string prompt, int lower)
        {
            int answer = 0;
            bool error = true;
            do
            {
                error = false;
                try
                {
                    Console.Write(prompt);
                    answer = int.Parse(Console.ReadLine());
                    if (answer < lower)
                    {
                        error = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error. Please enter a valid integer.");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    error = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error. Please enter a valid integer.");
                    Console.ResetColor();
                }
            } while (error);
            return answer;
        }

    }
}

