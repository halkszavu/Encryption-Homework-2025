namespace Homework01_Calculations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Task2();
		}

		private static void Task2()
		{
			string text = "This is a simple test text with repeated letters.";

			int length = text.Select(c => char.IsLetter(c)).Count();
			Console.WriteLine($"The length of the example text is: {length}");

			// Count letter frequencies (ignoring spaces, digits, and punctuation)
			var frequencies = text.ToLower()
								  .Where(char.IsLetter)
								  .GroupBy(c => c)
								  .ToDictionary(g => g.Key, g => g.Count());

			// Display results
			foreach (var kvp in frequencies.OrderByDescending(kvp => kvp.Key))
			{
				Console.WriteLine($"{kvp.Key}: {kvp.Value}");
			}
		}
	}
}
