using System.Diagnostics.Metrics;
using System.Text;

using static System.Console;

namespace Homework02_Calculations
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Task1();
			Task3();
		}

		private static void Task1()
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

		private static void Task3()
		{
			int[] key = { 4, 1, 3, 5, 2 };
			string iv = "01011";
			OFB ofb = new(key, iv);
			string message = "DOG";

			WriteLine("Task 3:");
			WriteLine("Part 1:");
			string[] encrypted = ofb.Encrypt(message.Select(Convert).ToArray());
			WriteLine($"Message: {message} as {string.Join(", ", message.Select(Convert))} encrypted with key: {string.Join(", ", key)} and IV: {iv} is:\n{string.Join(", ", encrypted)}");
			ofb.Reset();
			string[] decrypted = ofb.Decrypt(encrypted);
			WriteLine($"Encrypted message: {string.Join(", ", encrypted)} decrypted is:\n{string.Join(", ", decrypted)}");

			// Change the 5th bit:
			encrypted[0] = encrypted[0].Substring(0, 4) + (encrypted[0][4] == '0' ? '1' : '0') + encrypted[0].Substring(5);
			ofb.Reset();
			decrypted = ofb.Decrypt(encrypted);
			WriteLine($"Encrypted message: {string.Join(", ", encrypted)} decrypted is:\n{string.Join(", ", decrypted)}");

			// Change back:
			encrypted[0] = encrypted[0].Substring(0, 4) + (encrypted[0][4] == '0' ? '1' : '0') + encrypted[0].Substring(5);
			string iv2 = "11011";
			OFB ofb2 = new(key, iv2);
			decrypted = ofb2.Decrypt(encrypted);
			WriteLine($"Encrypted message: {string.Join(", ", encrypted)} decrypted with IV: {iv2} is:\n{string.Join(", ", decrypted)}");
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

		private static string Convert(char c)
		{
			char x = c.ToString().ToUpper()[0];
			string r = "";
			r = x switch
			{
				'A' => "00000",
				'B' => "00001",
				'C' => "00010",
				'D' => "00011",
				'E' => "00100",
				'F' => "00101",
				'G' => "00110",
				'H' => "00111",
				'I' => "01000",
				'J' => "01001",
				'K' => "01010",
				'L' => "01011",
				'M' => "01100",
				'N' => "01101",
				'O' => "01110",
				'P' => "01111",
				'Q' => "10000",
				'R' => "10001",
				'S' => "10010",
				'T' => "10011",
				'U' => "10100",
				'V' => "10101",
				'W' => "10110",
				'X' => "10111",
				'Y' => "11000",
				'Z' => "11001",
				_ => "11111",
			};
			return r;
		}
	}
}
