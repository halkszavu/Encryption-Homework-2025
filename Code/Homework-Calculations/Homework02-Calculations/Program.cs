using System.Text;

using static System.Console;

namespace Homework02_Calculations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			WriteLine("Task 1:");
			WriteLine("Part 1:");
			string message = "THEWANDCHOOSESTHEWIZARD".ToLower();
			string password = "MAGIC".ToLower();
			string encrypted = VigenereCipher(message, password);
			WriteLine($"Message: {message} encrypted with password: {password} is:\n{encrypted}");

			WriteLine("Part 2:");
			message = "THEWANDCHOOSESTHELIZARD".ToLower();
			encrypted = VigenereCipher(message, password);
			WriteLine($"Message: {message} encrypted with password: {password} is:\n{encrypted}");

			WriteLine("Part 3:");
			message = "THEWANDCHOOSESTHEWIZARD".ToLower();
			password = "MANIC".ToLower();
			encrypted = VigenereCipher(message, password);
			WriteLine($"Message: {message} encrypted with password: {password} is:\n{encrypted}");
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
	}
}
