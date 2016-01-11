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
			int upperBound;
			double average = 0.0;
			bool runProg = true;
			Random rand = new Random();
			int answer;

			do {
				
				Console.Clear ();
				Console.WriteLine ("\t\tCMPE 1700 Hi-Low Guesser\n\n");
				upperBound = 10;
				do {
					average = 0.0;
					for (int i = 0; i < 100; ++i)
					{
						answer = rand.Next(1, upperBound + 1);
						average += (PlayGame (upperBound, answer) / 100.0);
					}
					Console.WriteLine ("The game took, on average, {0} attempts to win out of 100 games for 1-{1}.", average, upperBound);
					upperBound *= 10;
				} while (upperBound <= 10000);
				Console.Write ("Press Enter to start again.");
				if (!(Console.ReadKey ().Key.Equals (ConsoleKey.Enter)))
					runProg = false;			
			} while (runProg);
		}

		static public int PlayGame(int upperBound, int answer)
		{
			
			int lowerGuess = 1;
			int upperGuess = upperBound;
			int guess = upperBound / 2;
			int attempts = 1;
			while (guess != answer)
			{
				++attempts;
				if (answer > guess) lowerGuess = guess + 1;
				else if (answer < guess) upperGuess = guess;
				guess = (upperGuess + lowerGuess) / 2;
			}
			return attempts;
		}
	}
}



