﻿using ScottPlot;
using System.Text;

using static System.Console;

namespace Homework01_Calculations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string sample = "In a hole in the ground there lived a hobbit. Not a nasty, dirty, wet hole, filled with the ends of worms and an oozy smell, nor yet a dry, bare, sandy hole with nothing in it to sit down on or to eat: it was a hobbit-hole, and that means comfort";
			//Task2(sample);

			string exercise = "FHKOJASZAFUDTBJQLVMKFHKZKFWGACXWGGUMNGAVKSNWEWWNMPANKWHFHKUIXIJMFEUJLGZLEBJDOAOJMDUWTKOAEGDEZZAUNMBQAPKVPQAXTATEGLNQSYVKOKCIUMLCIAEHGXRKTUXQUNZVGIGXGHRITLQDSOVVTEXQITTJQTQCZQQZBABRQEBMUFHKXQXTKZIQIYBYMSCWTFHZEQXOISGPDUWTEATLCFROKMETGQTOAYMKRYUCOQTNQOIHKVAAUCMTQLGBGROXKNMSYPGIOATFPRUXYMSZMRMPKZDMSQMVEOTGQGRNMCPPATNDUMAHDOSCPPEXGQGRLMGFPKTVKOAEKFHHQVEOLKJMLQWTENKIMGPHMJUNJGQGITDKEIHTGSRGJAAUXVQEEGVFECXMGOHMWVKOAZEANQ";
			Task5(exercise);
		}

		private static void Task2(string text)
		{
			text = new string(text.Where(char.IsLetter).ToArray()).ToLower();
			CreateHistogram(text, "original.png");
			string shifted = ShiftCipher(text, 7);
			CreateHistogram(shifted, "shifted.png");
			var key = new int[] { 7, 2, 5, 3, 8, 4, 1, 6 };
			string permuted = PermutationCipher(text, key);
			CreateHistogram(permuted, "permuted.png");
			string vigenere = VigenereCipher(text, "tolkien");
			CreateHistogram(vigenere, "vigenere.png");

			WriteLine("Original: " + text);
			WriteLine("Shifted: " + shifted);
			WriteLine("Permuted: " + permuted);
			WriteLine("Vigenere: " + vigenere);
			WriteLine("Done!");
		}

		private static void Task5(string text)
		{
			//Part1(text);

			Part2(text, 5);
		}

		private static void Part1(string text)
		{
			WriteLine("Task 5:");
			for (int i = 1; i < text.Length; i++)
			{
				var offset = OverlayText(text, i);
				var diff = offset.Select(text => text.Item1 - text.Item2);
				var zeros = FindConsecutiveZeros(diff);
				if (zeros.Any())
				{
					WriteLine($"{i} offset:");
					foreach (var zero in zeros)
					{
						WriteLine($"Position: {zero.pos}, Length: {zero.len}");
						WriteLine($"Text: {text.Substring(zero.pos, zero.len)}");
					}
				}
			}

			WriteLine("Finding candidates of actual repetition:");
			do
			{
				WriteLine("Enter the candidate:");
				string candidate = ReadLine().ToUpper();
				var indices = AllOccurences(text, candidate);
				foreach (var index in indices)
				{
					WriteLine($"Position: {index}");
				}
				WriteLine("Do you want to continue? (Y/N)");
			} while (ReadLine().ToLower() != "y");
		}

		private static void Part2(string text, int keylength)
		{
			string[] parts = SeparateStrings(text, keylength);
			foreach (var item in parts)
			{
				double IC = IndexOfCoincidence(item);
				WriteLine($"Index of Coincidence: {IC}");
			}
		}

		private static void CreateHistogram(string text, string result)
		{
			int length = text.Select(c => char.IsLetter(c)).Count();
			WriteLine($"The length of the example text is: {length}");

			var frequencies = "abcdefghijklmnopqrstuvwxyz".ToDictionary(c => c, c => 0);

			foreach (char c in text.ToLower().Where(char.IsLetter))
			{
				frequencies[c]++;
			}

			// Display results
			//foreach (var kvp in frequencies)
			//{
			//	Console.WriteLine($"{kvp.Key}: {kvp.Value}");
			//}

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
			while (message.Length > keylength * i)
			{
				for (int j = 0; j < keylength; j++)
				{
					int pos = key[j] + keylength * i - 1;
					char c = message.Length > pos ? message[pos] : 'X';
					result.Append(c);
				}
				i++;
			}

			return result.ToString();
		}

		private static string VigenereCipher(string message, string password)
		{
			StringBuilder result = new(message.Length);

			for (int i = 0; i < message.Length; i++)
			{
				char plainChar = message[i];
				char keyChar = password[i % password.Length]; // Repeat key if text is longer

				char encryptedChar = (char)((plainChar - 'a' + (keyChar - 'a')) % 26 + 'a');
				result.Append(encryptedChar);
			}

			return result.ToString();
		}

		private static IEnumerable<(char, char)> OverlayText(string text, int offset)
		{
			for (int i = 0; i < text.Length; i++)
			{
				yield return (text[i], text[(i + offset) % text.Length]);
			}
		}

		private static IEnumerable<(int pos, int len)> FindConsecutiveZeros(IEnumerable<int> numbers)
		{
			int start = -1;
			int length = 0;
			int index = 0;

			foreach (var num in numbers)
			{
				if (num == 0)
				{
					if (length == 0)
						start = index;
					length++;
				}
				else
				{
					if (length >= 3)
						yield return (start, length);

					length = 0;
				}

				index++;
			}

			if (length >= 2)
				yield return (start, length);
		}
		
		private static IEnumerable<int> AllOccurences(string text, string candidate)
		{
			int index = 0;
			while (index < text.Length)
			{
				index = text.IndexOf(candidate, index);
				if (index == -1)
					break;
				yield return index;
				index++;
			}
		}

		private static string[] SeparateStrings(string text, int keylength)
		{
			string[] parts = new string[keylength];
			for (int i = 0; i < text.Length; i++)
			{
				parts[i % keylength] += text[i];
			}
			return parts;
		}

		private static double IndexOfCoincidence(string text)
		{
			int N = text.Length;
			var frequencies = "abcdefghijklmnopqrstuvwxyz".ToDictionary(c => c, c => 0D);

			foreach (char c in text.ToLower().Where(char.IsLetter))
			{
				frequencies[c]++;
			}

			return frequencies.Values.Where(x => x > 0).Sum(x => x * (x - 1)) / (N * (N - 1));
		}
	}
}