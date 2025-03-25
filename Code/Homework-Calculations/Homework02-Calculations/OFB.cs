using System.Text;

namespace Homework02_Calculations
{
	internal class OFB
	{
		private readonly int[] key;
		string IV;

		public OFB(int[] key, string IV)
		{
			this.key = key;
			this.IV = IV;
		}

		public string Encrypt(string message)
		{
			IV = PermutationCipher(IV, key);
			return XOR(message, IV);
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

		private static string XOR(string message, string IV)
		{
			StringBuilder result = new(message.Length);
			for (int i = 0; i < message.Length; i++)
			{
				char c = (char)(message[i] ^ IV[i]);
				result.Append(c);
			}
			return result.ToString();
		}
	}
}
