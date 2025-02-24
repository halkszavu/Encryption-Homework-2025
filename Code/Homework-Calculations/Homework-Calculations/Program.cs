using ScottPlot;

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
			CreateHistogram(text, "original.png");
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
	}
}