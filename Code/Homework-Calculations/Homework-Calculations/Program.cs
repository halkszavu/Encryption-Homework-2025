using ScottPlot;
using System.Text;

namespace Homework01_Calculations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string sample = "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort";
			Task2(sample);
		}

		private static void Task2(string text)
		{
			text = text.ToLower();
			CreateHistogram(text, "original.png");
			string shifted = ShiftCipher(text, 7);
			CreateHistogram(shifted, "shifted.png");
			string permuted = PermutationCipher(text, [7, 2, 5, 3, 8, 4, 1, 6]);
			CreateHistogram(permuted, "permuted.png");
			string vigenere = VigenereCipher(text, "tolkien");
			CreateHistogram(vigenere, "vigenere.png");
		}

		private static void CreateHistogram(string text, string result)
		{
			int length = text.Select(c => char.IsLetter(c)).Count();
			Console.WriteLine($"The length of the example text is: {length}");

			var frequencies = "abcdefghijklmnopqrstuvwxyz".ToDictionary(c => c, c => 0);

			foreach (char c in text.ToLower().Where(char.IsLetter))
			{
				frequencies[c]++;
			}

			// Display results
			foreach (var kvp in frequencies)
			{
				Console.WriteLine($"{kvp.Key}: {kvp.Value}");
			}

			Plot plot = new();
			plot.Add.Bars(frequencies.Values.Select(x => (double)x).ToArray());

			Tick[] ticks = frequencies.Keys.Select((c, i) => new Tick(i, c.ToString())).ToArray();

			plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);

			plot.SavePng(result, 500, 600);
		}

		private static string ShiftCipher(string message, int shift)
		{
			StringBuilder result = new(message.Length);

			foreach (char c in message)
			{
				char shifted = (char)((c - 'a' + shift) % 26 + 'a');
				result.Append(shifted);
			}

			return result.ToString();
		}

		private static string PermutationCipher(string message, int[] key)
		{
			int keylength = key.Length;
			StringBuilder result = new(message.Length);
			int i = 0;
			while (message.Length <= keylength * i)
			{
				for (int j = 0; j < keylength; j++)
				{
					char c = message.Length < key[j] + keylength * i ? message[key[j] + keylength * i] : 'X';
					result.Append(c);
				}
			}

			return result.ToString();
		}

		private static string VigenereCipher(string message, string password)
		{
			StringBuilder result = new(message.Length);

			for(int i = 0; i < password.Length; i++)
			{
				char c = message[i];
				int shift = password[i % password.Length] - 'a';
				char shifted = (char)((c - 'a' + shift) % 26 + 'a');
				result.Append(shifted);
			}

			return result.ToString();
		}
	}
}