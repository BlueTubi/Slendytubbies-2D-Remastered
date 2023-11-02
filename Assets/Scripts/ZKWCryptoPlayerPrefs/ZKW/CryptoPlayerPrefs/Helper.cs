using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZKW.CryptoPlayerPrefs
{
	public static class Helper
	{
		private static HashAlgorithm hash;

		private static Dictionary<string, SymmetricAlgorithm> rijndaelDict;

		public static byte[] hashBytes(byte[] input)
		{
			if (hash == null)
			{
				hash = MD5.Create();
			}
			return hash.ComputeHash(input);
		}

		private static byte[] EncryptString(byte[] clearText, SymmetricAlgorithm alg)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, alg.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(clearText, 0, clearText.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}

		public static string EncryptString(string clearText, string Password)
		{
			SymmetricAlgorithm rijndaelForKey = getRijndaelForKey(Password);
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(clearText);
			byte[] inArray = EncryptString(bytes, rijndaelForKey);
			return Convert.ToBase64String(inArray);
		}

		private static byte[] DecryptString(byte[] cipherData, SymmetricAlgorithm alg)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, alg.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(cipherData, 0, cipherData.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}

		public static string DecryptString(string cipherText, string Password)
		{
			if (rijndaelDict == null)
			{
				rijndaelDict = new Dictionary<string, SymmetricAlgorithm>();
			}
			byte[] cipherData = Convert.FromBase64String(cipherText);
			SymmetricAlgorithm rijndaelForKey = getRijndaelForKey(Password);
			byte[] array = DecryptString(cipherData, rijndaelForKey);
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			return unicodeEncoding.GetString(array, 0, array.Length);
		}

		private static SymmetricAlgorithm getRijndaelForKey(string key)
		{
			if (rijndaelDict == null)
			{
				rijndaelDict = new Dictionary<string, SymmetricAlgorithm>();
			}
			SymmetricAlgorithm symmetricAlgorithm;
			if (rijndaelDict.ContainsKey(key))
			{
				symmetricAlgorithm = rijndaelDict[key];
			}
			else
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, new byte[13]
				{
					73, 97, 110, 32, 77, 100, 118, 101, 101, 100,
					101, 118, 118
				});
				symmetricAlgorithm = Rijndael.Create();
				symmetricAlgorithm.Key = rfc2898DeriveBytes.GetBytes(32);
				symmetricAlgorithm.IV = rfc2898DeriveBytes.GetBytes(16);
				rijndaelDict.Add(key, symmetricAlgorithm);
			}
			return symmetricAlgorithm;
		}
	}
}
