using System.Text;

namespace Homework02_Calculations
{
	internal class OFB
	{
		private readonly int[] key;
		bool[] IV;

		public OFB(int[] key, string iv)
		{
			this.key = key;
			IV = new bool[iv.Length];
			for(int i = 0; i < iv.Length; i++)
				IV[i] = iv[i] == '1';
		}

		public bool[][] Encrypt(bool[][] message)
		{
			bool[][] result = new bool[message.Length][];
			for(int i = 0; i < message.Length; i++)
			{
				result[i] = EncryptOne(message[i]);
			}
			return result;
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

		private static bool[] XOR(bool[] message, bool[] IV)
		{
			bool[] result = new bool[message.Length];
			for(int i = 0; i < message.Length; i++)
				result[i] = message[i] ^ IV[i];
			return result;
		}
	}
}
