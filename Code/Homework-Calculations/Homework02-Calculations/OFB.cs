using System.Text;

namespace Homework02_Calculations
{
	internal class OFB
	{
		private readonly int[] key;
		private readonly string originalIV;
		bool[] IV;

		public OFB(int[] key, string iv)
		{
			this.key = key;
			originalIV = iv;
			IV = Convert(iv);
		}

		public void Reset()
		{
			IV = Convert(originalIV);
		}

		public string[] Encrypt(string[] message)
		{
			bool[][] result = new bool[message.Length][];
			for(int i = 0; i < message.Length; i++)
			{
				result[i] = EncryptOne(Convert(message[i]));
			}
			return result.Select(Convert).ToArray();
		}

		public string[] Decrypt(string[] ciphertext)
		{
			bool[][] result = new bool[ciphertext.Length][];
			for(int i = 0; i < ciphertext.Length; i++)
			{
				result[i] = EncryptOne(Convert(ciphertext[i]));
			}
			return result.Select(Convert).ToArray();
		}

		bool[] EncryptOne(bool[] message)
		{
			IV = PermutationCipher(IV, key);
			return XOR(message, IV);
		}

		private static bool[] PermutationCipher(bool[] message, int[] key)
		{
			int keylength = key.Length;
			List<bool> result = new(message.Length);
			int i = 0;
			while (message.Length > keylength * i)
			{
				for (int j = 0; j < keylength; j++)
				{
					int pos = key[j] + keylength * i - 1;
					bool c = message.Length > pos ? message[pos] : false;
					result.Append(c);
				}
				i++;
			}

			return result.ToArray();
		}

		private static bool[] Convert(string s)
		{
			bool[] result = new bool[s.Length];
			for(int i = 0; i < s.Length; i++)
				result[i] = s[i] == '1';
			return result;
		}

		private static string Convert(bool[] b)
		{
			StringBuilder result = new(b.Length);
			for(int i = 0; i < b.Length; i++)
				result.Append(b[i] ? '1' : '0');
			return result.ToString();
		}

		private static bool[] XOR(bool[] message, bool[] IV)
		{
			bool[] result = new bool[message.Length];
			for(int i = 0; i < message.Length; i++)
				result[i] = message[i] ^ IV[i];
			return result;
		}
	}
}
