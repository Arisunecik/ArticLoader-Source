using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticLoader.writes
{
    internal class Undetected
    {
		public static string RandomString()
		{
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string text2 = "0123456789";
			char[] array = new char[8];
			Random random = new Random();
			for (int i = 0; i < 2; i++)
			{
				array[i] = text[random.Next(text.Length)];
			}
			for (int j = 2; j < 6; j++)
			{
				array[j] = text2[random.Next(text2.Length)];
			}
			for (int k = 6; k < 8; k++)
			{
				array[k] = text[random.Next(text.Length)];
			}
			return new string(array);
		}

		public static string RandomStringStrong(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
							   select s[Undetected.random.Next(s.Length)]).ToArray<char>());
		}

		public static string RandomStringStrongest(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("!#%&/()=?'*<>-_+|0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
							   select s[Undetected.random.Next(s.Length)]).ToArray<char>());
		}

		private static Random random = new Random();
	}
}
