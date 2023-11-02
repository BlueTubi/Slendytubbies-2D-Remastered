#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ExitGames.Client.Photon
{
	public class SupportClass
	{
		public delegate int IntegerMillisecondsDelegate();

		public class ThreadSafeRandom
		{
			private static readonly Random _r = new Random();

			public static int Next()
			{
				lock (_r)
				{
					return _r.Next();
				}
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1
		{
			public Func<bool> myThread;

			public int millisecondsInterval;

			public void _003CCallInBackground_003Eb__0()
			{
				while (myThread())
				{
					Thread.Sleep(millisecondsInterval);
				}
			}
		}

		protected internal static IntegerMillisecondsDelegate IntegerMilliseconds;

		[CompilerGenerated]
		private static IntegerMillisecondsDelegate CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;

		public static uint CalculateCrc(byte[] buffer, int length)
		{
			uint num = uint.MaxValue;
			uint num2 = 3988292384u;
			byte b = 0;
			for (int i = 0; i < length; i++)
			{
				b = buffer[i];
				num ^= b;
				for (int j = 0; j < 8; j++)
				{
					num = (((num & 1) == 0) ? (num >> 1) : ((num >> 1) ^ num2));
				}
			}
			return num;
		}

		public static List<MethodInfo> GetMethods(Type type, Type attribute)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (type == null)
			{
				return list;
			}
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			MethodInfo[] array = methods;
			foreach (MethodInfo methodInfo in array)
			{
				if (attribute == null || methodInfo.IsDefined(attribute, false))
				{
					list.Add(methodInfo);
				}
			}
			return list;
		}

		public static int GetTickCount()
		{
			return IntegerMilliseconds();
		}

		public static void CallInBackground(Func<bool> myThread)
		{
			CallInBackground(myThread, 100);
		}

		public static void CallInBackground(Func<bool> myThread, int millisecondsInterval)
		{
			_003C_003Ec__DisplayClass1 _003C_003Ec__DisplayClass = new _003C_003Ec__DisplayClass1();
			_003C_003Ec__DisplayClass.myThread = myThread;
			_003C_003Ec__DisplayClass.millisecondsInterval = millisecondsInterval;
			Thread thread = new Thread(_003C_003Ec__DisplayClass._003CCallInBackground_003Eb__0);
			thread.IsBackground = true;
			thread.Start();
		}

		public static void WriteStackTrace(Exception throwable, TextWriter stream)
		{
			if (stream != null)
			{
				stream.WriteLine(throwable.ToString());
				stream.WriteLine(throwable.StackTrace);
				stream.Flush();
			}
			else
			{
				Debug.WriteLine(throwable.ToString());
				Debug.WriteLine(throwable.StackTrace);
			}
		}

		public static void WriteStackTrace(Exception throwable)
		{
			WriteStackTrace(throwable, null);
		}

		public static string DictionaryToString(IDictionary dictionary)
		{
			return DictionaryToString(dictionary, true);
		}

		public static string DictionaryToString(IDictionary dictionary, bool includeTypes)
		{
			if (dictionary == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (object key in dictionary.Keys)
			{
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append(", ");
				}
				Type type;
				string text;
				if (dictionary[key] == null)
				{
					type = typeof(object);
					text = "null";
				}
				else
				{
					type = dictionary[key].GetType();
					text = dictionary[key].ToString();
				}
				if (typeof(IDictionary) == type || typeof(Hashtable) == type)
				{
					text = DictionaryToString((IDictionary)dictionary[key]);
				}
				if (typeof(string[]) == type)
				{
					text = string.Format("{{{0}}}", string.Join(",", (string[])dictionary[key]));
				}
				if (includeTypes)
				{
					stringBuilder.AppendFormat("({0}){1}=({2}){3}", key.GetType().Name, key, type.Name, text);
				}
				else
				{
					stringBuilder.AppendFormat("{0}={1}", key, text);
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		[Obsolete("Use DictionaryToString() instead.")]
		public static string HashtableToString(Hashtable hash)
		{
			return DictionaryToString(hash);
		}

		[Obsolete("Use Protocol.Serialize() instead.")]
		public static void NumberToByteArray(byte[] buffer, int index, short number)
		{
			Protocol.Serialize(number, buffer, ref index);
		}

		[Obsolete("Use Protocol.Serialize() instead.")]
		public static void NumberToByteArray(byte[] buffer, int index, int number)
		{
			Protocol.Serialize(number, buffer, ref index);
		}

		public static string ByteArrayToString(byte[] list)
		{
			if (list == null)
			{
				return string.Empty;
			}
			return BitConverter.ToString(list);
		}

		static SupportClass()
		{
			if (CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 == null)
			{
				CS_0024_003C_003E9__CachedAnonymousMethodDelegate4 = _003C_002Ecctor_003Eb__3;
			}
			IntegerMilliseconds = CS_0024_003C_003E9__CachedAnonymousMethodDelegate4;
		}

		[CompilerGenerated]
		private static int _003C_002Ecctor_003Eb__3()
		{
			return Environment.TickCount;
		}
	}
}
