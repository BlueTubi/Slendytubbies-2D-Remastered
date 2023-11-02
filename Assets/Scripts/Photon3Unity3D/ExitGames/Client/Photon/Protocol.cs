#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ExitGames.Client.Photon
{
	public class Protocol
	{
		public const string protocolType = "GpBinaryV16";

		internal static readonly Dictionary<Type, CustomType> TypeDict = new Dictionary<Type, CustomType>();

		internal static readonly Dictionary<byte, CustomType> CodeDict = new Dictionary<byte, CustomType>();

		internal static bool TryRegisterType(Type type, byte typeCode, SerializeMethod serializeFunction, DeserializeMethod deserializeFunction)
		{
			if (CodeDict.ContainsKey(typeCode) || TypeDict.ContainsKey(type))
			{
				return false;
			}
			CustomType value = new CustomType(type, typeCode, serializeFunction, deserializeFunction);
			CodeDict.Add(typeCode, value);
			TypeDict.Add(type, value);
			return true;
		}

		private static bool SerializeCustom(MemoryStream dout, object serObject)
		{
			CustomType value;
			if (TypeDict.TryGetValue(serObject.GetType(), out value))
			{
				byte[] array = value.SerializeFunction(serObject);
				dout.WriteByte(99);
				dout.WriteByte(value.Code);
				SerializeShort(dout, (short)array.Length, false);
				dout.Write(array, 0, array.Length);
				return true;
			}
			return false;
		}

		private static object DeserializeCustom(MemoryStream din, byte customTypeCode)
		{
			short num = DeserializeShort(din);
			byte[] array = new byte[num];
			din.Read(array, 0, num);
			CustomType value;
			if (CodeDict.TryGetValue(customTypeCode, out value))
			{
				return value.DeserializeFunction(array);
			}
			return null;
		}

		public static byte[] Serialize(object obj)
		{
			MemoryStream memoryStream = new MemoryStream();
			Serialize(memoryStream, obj, true);
			return memoryStream.ToArray();
		}

		public static object Deserialize(byte[] serializedData)
		{
			MemoryStream memoryStream = new MemoryStream(serializedData);
			return Deserialize(memoryStream, (byte)memoryStream.ReadByte());
		}

		internal static object DeserializeMessage(MemoryStream stream)
		{
			return Deserialize(stream, (byte)stream.ReadByte());
		}

		internal static byte[] DeserializeRawMessage(MemoryStream stream)
		{
			return (byte[])Deserialize(stream, (byte)stream.ReadByte());
		}

		internal static void SerializeMessage(MemoryStream ms, object msg)
		{
			Serialize(ms, msg, true);
		}

		private static Type GetTypeOfCode(byte typeCode)
		{
			switch (typeCode)
			{
			case 105:
				return typeof(int);
			case 115:
				return typeof(string);
			case 97:
				return typeof(string[]);
			case 120:
				return typeof(byte[]);
			case 110:
				return typeof(int[]);
			case 104:
				return typeof(Hashtable);
			case 68:
				return typeof(IDictionary);
			case 111:
				return typeof(bool);
			case 107:
				return typeof(short);
			case 108:
				return typeof(long);
			case 98:
				return typeof(byte);
			case 102:
				return typeof(float);
			case 100:
				return typeof(double);
			case 121:
				return typeof(Array);
			case 99:
				return typeof(CustomType);
			case 122:
				return typeof(object[]);
			case 101:
				return typeof(EventData);
			case 113:
				return typeof(OperationRequest);
			case 112:
				return typeof(OperationResponse);
			case 0:
			case 42:
				return typeof(object);
			default:
				Debug.WriteLine("missing type: " + typeCode);
				throw new Exception("deserialize(): " + typeCode);
			}
		}

		private static byte GetCodeOfType(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Byte:
				return 98;
			case TypeCode.String:
				return 115;
			case TypeCode.Boolean:
				return 111;
			case TypeCode.Int16:
				return 107;
			case TypeCode.Int32:
				return 105;
			case TypeCode.Int64:
				return 108;
			case TypeCode.Single:
				return 102;
			case TypeCode.Double:
				return 100;
			default:
				if (type.IsArray)
				{
					if (type == typeof(byte[]))
					{
						return 120;
					}
					return 121;
				}
				if (type == typeof(Hashtable))
				{
					return 104;
				}
				if (type.IsGenericType && typeof(Dictionary<, >) == type.GetGenericTypeDefinition())
				{
					return 68;
				}
				if (type == typeof(EventData))
				{
					return 101;
				}
				if (type == typeof(OperationRequest))
				{
					return 113;
				}
				if (type == typeof(OperationResponse))
				{
					return 112;
				}
				return 0;
			}
		}

		private static Array CreateArrayByType(byte arrayType, short length)
		{
			return Array.CreateInstance(GetTypeOfCode(arrayType), length);
		}

		internal static void SerializeOperationRequest(MemoryStream memStream, OperationRequest serObject, bool setType)
		{
			SerializeOperationRequest(memStream, serObject.OperationCode, serObject.Parameters, setType);
		}

		internal static void SerializeOperationRequest(MemoryStream memStream, byte operationCode, Dictionary<byte, object> parameters, bool setType)
		{
			if (setType)
			{
				memStream.WriteByte(113);
			}
			memStream.WriteByte(operationCode);
			SerializeParameterTable(memStream, parameters);
		}

		internal static OperationRequest DeserializeOperationRequest(MemoryStream din)
		{
			OperationRequest operationRequest = new OperationRequest();
			operationRequest.OperationCode = DeserializeByte(din);
			operationRequest.Parameters = DeserializeParameterTable(din);
			return operationRequest;
		}

		internal static void SerializeOperationResponse(MemoryStream memStream, OperationResponse serObject, bool setType)
		{
			if (setType)
			{
				memStream.WriteByte(112);
			}
			memStream.WriteByte(serObject.OperationCode);
			SerializeShort(memStream, serObject.ReturnCode, false);
			if (string.IsNullOrEmpty(serObject.DebugMessage))
			{
				memStream.WriteByte(42);
			}
			else
			{
				SerializeString(memStream, serObject.DebugMessage, false);
			}
			SerializeParameterTable(memStream, serObject.Parameters);
		}

		internal static OperationResponse DeserializeOperationResponse(MemoryStream memoryStream)
		{
			OperationResponse operationResponse = new OperationResponse();
			operationResponse.OperationCode = DeserializeByte(memoryStream);
			operationResponse.ReturnCode = DeserializeShort(memoryStream);
			operationResponse.DebugMessage = Deserialize(memoryStream, DeserializeByte(memoryStream)) as string;
			operationResponse.Parameters = DeserializeParameterTable(memoryStream);
			return operationResponse;
		}

		internal static void SerializeEventData(MemoryStream memStream, EventData serObject, bool setType)
		{
			if (setType)
			{
				memStream.WriteByte(101);
			}
			memStream.WriteByte(serObject.Code);
			SerializeParameterTable(memStream, serObject.Parameters);
		}

		internal static EventData DeserializeEventData(MemoryStream din)
		{
			EventData eventData = new EventData();
			eventData.Code = DeserializeByte(din);
			eventData.Parameters = DeserializeParameterTable(din);
			return eventData;
		}

		private static void SerializeParameterTable(MemoryStream memStream, Dictionary<byte, object> parameters)
		{
			if (parameters == null || parameters.Count == 0)
			{
				SerializeShort(memStream, 0, false);
				return;
			}
			SerializeShort(memStream, (short)parameters.Count, false);
			foreach (KeyValuePair<byte, object> parameter in parameters)
			{
				memStream.WriteByte(parameter.Key);
				Serialize(memStream, parameter.Value, true);
			}
		}

		private static Dictionary<byte, object> DeserializeParameterTable(MemoryStream memoryStream)
		{
			short num = DeserializeShort(memoryStream);
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>(num);
			for (int i = 0; i < num; i++)
			{
				byte key = (byte)memoryStream.ReadByte();
				object value = Deserialize(memoryStream, (byte)memoryStream.ReadByte());
				dictionary[key] = value;
			}
			return dictionary;
		}

		private static void Serialize(MemoryStream dout, object serObject, bool setType)
		{
			if (serObject == null)
			{
				if (setType)
				{
					dout.WriteByte(42);
				}
				return;
			}
			Type type = serObject.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Byte:
				SerializeByte(dout, (byte)serObject, setType);
				return;
			case TypeCode.String:
				SerializeString(dout, (string)serObject, setType);
				return;
			case TypeCode.Boolean:
				SerializeBoolean(dout, (bool)serObject, setType);
				return;
			case TypeCode.Int16:
				SerializeShort(dout, (short)serObject, setType);
				return;
			case TypeCode.Int32:
				SerializeInteger(dout, (int)serObject, setType);
				return;
			case TypeCode.Int64:
				SerializeLong(dout, (long)serObject, setType);
				return;
			case TypeCode.Single:
				SerializeFloat(dout, (float)serObject, setType);
				return;
			case TypeCode.Double:
				SerializeDouble(dout, (double)serObject, setType);
				return;
			}
			if (serObject is Hashtable)
			{
				SerializeHashTable(dout, (Hashtable)serObject, setType);
			}
			else if (type.IsArray)
			{
				if (serObject is byte[])
				{
					SerializeByteArray(dout, (byte[])serObject, setType);
				}
				else if (serObject is int[])
				{
					SerializeIntArrayOptimized(dout, (int[])serObject, setType);
				}
				else if (type.GetElementType() == typeof(object))
				{
					SerializeObjectArray(dout, serObject as object[], setType);
				}
				else
				{
					SerializeArray(dout, (Array)serObject, setType);
				}
			}
			else if (serObject is IDictionary)
			{
				SerializeDictionary(dout, (IDictionary)serObject, setType);
			}
			else if (serObject is EventData)
			{
				SerializeEventData(dout, (EventData)serObject, setType);
			}
			else if (serObject is OperationResponse)
			{
				SerializeOperationResponse(dout, (OperationResponse)serObject, setType);
			}
			else if (serObject is OperationRequest)
			{
				SerializeOperationRequest(dout, (OperationRequest)serObject, setType);
			}
			else if (!SerializeCustom(dout, serObject))
			{
				throw new Exception("cannot serialize(): " + serObject.GetType());
			}
		}

		private static void SerializeByte(MemoryStream dout, byte serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(98);
			}
			dout.WriteByte(serObject);
		}

		private static void SerializeBoolean(MemoryStream dout, bool serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(111);
			}
			dout.Write(BitConverter.GetBytes(serObject), 0, 1);
		}

		private static void SerializeShort(MemoryStream dout, short serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(107);
			}
			dout.Write(new byte[2]
			{
				(byte)(serObject >> 8),
				(byte)serObject
			}, 0, 2);
		}

		public static void Serialize(short value, byte[] target, ref int targetOffset)
		{
			target[targetOffset++] = (byte)(value >> 8);
			target[targetOffset++] = (byte)value;
		}

		private static void SerializeInteger(MemoryStream dout, int serObject, bool setType)
		{
			dout.Write(new byte[5]
			{
				105,
				(byte)(serObject >> 24),
				(byte)(serObject >> 16),
				(byte)(serObject >> 8),
				(byte)serObject
			}, (!setType) ? 1 : 0, setType ? 5 : 4);
		}

		public static void Serialize(int value, byte[] target, ref int targetOffset)
		{
			target[targetOffset++] = (byte)(value >> 24);
			target[targetOffset++] = (byte)(value >> 16);
			target[targetOffset++] = (byte)(value >> 8);
			target[targetOffset++] = (byte)value;
		}

		private static void SerializeLong(MemoryStream dout, long serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(108);
			}
			byte[] bytes = BitConverter.GetBytes(serObject);
			if (BitConverter.IsLittleEndian)
			{
				byte b = bytes[0];
				byte b2 = bytes[1];
				byte b3 = bytes[2];
				byte b4 = bytes[3];
				bytes[0] = bytes[7];
				bytes[1] = bytes[6];
				bytes[2] = bytes[5];
				bytes[3] = bytes[4];
				bytes[4] = b4;
				bytes[5] = b3;
				bytes[6] = b2;
				bytes[7] = b;
			}
			dout.Write(bytes, 0, 8);
		}

		private static void SerializeFloat(MemoryStream dout, float serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(102);
			}
			byte[] bytes = BitConverter.GetBytes(serObject);
			if (BitConverter.IsLittleEndian)
			{
				byte b = bytes[0];
				byte b2 = bytes[1];
				bytes[0] = bytes[3];
				bytes[1] = bytes[2];
				bytes[2] = b2;
				bytes[3] = b;
			}
			dout.Write(bytes, 0, 4);
		}

		public static void Serialize(float value, byte[] target, ref int targetOffset)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
			{
				target[targetOffset++] = bytes[3];
				target[targetOffset++] = bytes[2];
				target[targetOffset++] = bytes[1];
				target[targetOffset++] = bytes[0];
			}
			else
			{
				target[targetOffset++] = bytes[0];
				target[targetOffset++] = bytes[1];
				target[targetOffset++] = bytes[2];
				target[targetOffset++] = bytes[3];
			}
		}

		private static void SerializeDouble(MemoryStream dout, double serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(100);
			}
			byte[] bytes = BitConverter.GetBytes(serObject);
			if (BitConverter.IsLittleEndian)
			{
				byte b = bytes[0];
				byte b2 = bytes[1];
				byte b3 = bytes[2];
				byte b4 = bytes[3];
				bytes[0] = bytes[7];
				bytes[1] = bytes[6];
				bytes[2] = bytes[5];
				bytes[3] = bytes[4];
				bytes[4] = b4;
				bytes[5] = b3;
				bytes[6] = b2;
				bytes[7] = b;
			}
			dout.Write(bytes, 0, 8);
		}

		private static void SerializeString(MemoryStream dout, string serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(115);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(serObject);
			SerializeShort(dout, (short)bytes.Length, false);
			dout.Write(bytes, 0, bytes.Length);
		}

		private static void SerializeArray(MemoryStream dout, Array serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(121);
			}
			SerializeShort(dout, (short)serObject.Length, false);
			Type elementType = serObject.GetType().GetElementType();
			byte codeOfType = GetCodeOfType(elementType);
			if (codeOfType != 0)
			{
				dout.WriteByte(codeOfType);
				if (codeOfType == 68)
				{
					bool setKeyType;
					bool setValueType;
					SerializeDictionaryHeader(dout, serObject, out setKeyType, out setValueType);
					for (int i = 0; i < serObject.Length; i++)
					{
						object value = serObject.GetValue(i);
						SerializeDictionaryElements(dout, value, setKeyType, setValueType);
					}
				}
				else
				{
					for (int i = 0; i < serObject.Length; i++)
					{
						object value2 = serObject.GetValue(i);
						Serialize(dout, value2, false);
					}
				}
			}
			else
			{
				CustomType value3;
				if (!TypeDict.TryGetValue(elementType, out value3))
				{
					throw new NotSupportedException("cannot serialize array of type " + elementType);
				}
				dout.WriteByte(99);
				dout.WriteByte(value3.Code);
				for (int i = 0; i < serObject.Length; i++)
				{
					object value4 = serObject.GetValue(i);
					byte[] array = value3.SerializeFunction(value4);
					SerializeShort(dout, (short)array.Length, false);
					dout.Write(array, 0, array.Length);
				}
			}
		}

		private static void SerializeByteArray(MemoryStream dout, byte[] serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(120);
			}
			SerializeInteger(dout, serObject.Length, false);
			dout.Write(serObject, 0, serObject.Length);
		}

		private static void SerializeIntArrayOptimized(MemoryStream inWriter, int[] serObject, bool setType)
		{
			if (setType)
			{
				inWriter.WriteByte(121);
			}
			SerializeShort(inWriter, (short)serObject.Length, false);
			inWriter.WriteByte(105);
			byte[] array = new byte[serObject.Length * 4];
			int num = 0;
			for (int i = 0; i < serObject.Length; i++)
			{
				array[num++] = (byte)(serObject[i] >> 24);
				array[num++] = (byte)(serObject[i] >> 16);
				array[num++] = (byte)(serObject[i] >> 8);
				array[num++] = (byte)serObject[i];
			}
			inWriter.Write(array, 0, array.Length);
		}

		private static void SerializeStringArray(MemoryStream dout, string[] serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(97);
			}
			SerializeShort(dout, (short)serObject.Length, false);
			for (int i = 0; i < serObject.Length; i++)
			{
				SerializeString(dout, serObject[i], false);
			}
		}

		private static void SerializeObjectArray(MemoryStream dout, object[] objects, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(122);
			}
			SerializeShort(dout, (short)objects.Length, false);
			foreach (object serObject in objects)
			{
				Serialize(dout, serObject, true);
			}
		}

		private static void SerializeHashTable(MemoryStream dout, Hashtable serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(104);
			}
			SerializeShort(dout, (short)serObject.Count, false);
			foreach (DictionaryEntry item in serObject)
			{
				Serialize(dout, item.Key, true);
				Serialize(dout, item.Value, true);
			}
		}

		private static void SerializeDictionary(MemoryStream dout, IDictionary serObject, bool setType)
		{
			if (setType)
			{
				dout.WriteByte(68);
			}
			bool setKeyType;
			bool setValueType;
			SerializeDictionaryHeader(dout, serObject, out setKeyType, out setValueType);
			SerializeDictionaryElements(dout, serObject, setKeyType, setValueType);
		}

		private static void SerializeDictionaryHeader(MemoryStream writer, Type dictType)
		{
			bool setKeyType;
			bool setValueType;
			SerializeDictionaryHeader(writer, dictType, out setKeyType, out setValueType);
		}

		private static void SerializeDictionaryHeader(MemoryStream writer, object dict, out bool setKeyType, out bool setValueType)
		{
			Type[] genericArguments = dict.GetType().GetGenericArguments();
			setKeyType = genericArguments[0] == typeof(object);
			setValueType = genericArguments[1] == typeof(object);
			if (setKeyType)
			{
				writer.WriteByte(0);
			}
			else
			{
				GpType codeOfType = (GpType)GetCodeOfType(genericArguments[0]);
				if (codeOfType == GpType.Unknown || codeOfType == GpType.Dictionary)
				{
					throw new Exception("Unexpected - cannot serialize Dictionary with key type: " + genericArguments[0]);
				}
				writer.WriteByte((byte)codeOfType);
			}
			if (setValueType)
			{
				writer.WriteByte(0);
				return;
			}
			GpType codeOfType2 = (GpType)GetCodeOfType(genericArguments[1]);
			if (codeOfType2 == GpType.Unknown)
			{
				throw new Exception("Unexpected - cannot serialize Dictionary with value type: " + genericArguments[0]);
			}
			writer.WriteByte((byte)codeOfType2);
			if (codeOfType2 == GpType.Dictionary)
			{
				SerializeDictionaryHeader(writer, genericArguments[1]);
			}
		}

		private static void SerializeDictionaryElements(MemoryStream writer, object dict, bool setKeyType, bool setValueType)
		{
			IDictionary dictionary = (IDictionary)dict;
			SerializeShort(writer, (short)dictionary.Count, false);
			foreach (DictionaryEntry item in dictionary)
			{
				Serialize(writer, item.Key, setKeyType);
				Serialize(writer, item.Value, setValueType);
			}
		}

		private static object Deserialize(MemoryStream din, byte type)
		{
			switch (type)
			{
			case 105:
				return DeserializeInteger(din);
			case 115:
				return DeserializeString(din);
			case 97:
				return DeserializeStringArray(din);
			case 120:
				return DeserializeByteArray(din);
			case 110:
				return DeserializeIntArray(din);
			case 104:
				return DeserializeHashTable(din);
			case 68:
				return DeserializeDictionary(din);
			case 111:
				return DeserializeBoolean(din);
			case 107:
				return DeserializeShort(din);
			case 108:
				return DeserializeLong(din);
			case 98:
				return DeserializeByte(din);
			case 102:
				return DeserializeFloat(din);
			case 100:
				return DeserializeDouble(din);
			case 121:
				return DeserializeArray(din);
			case 99:
			{
				byte customTypeCode = (byte)din.ReadByte();
				return DeserializeCustom(din, customTypeCode);
			}
			case 122:
				return DeserializeObjectArray(din);
			case 101:
				return DeserializeEventData(din);
			case 113:
				return DeserializeOperationRequest(din);
			case 112:
				return DeserializeOperationResponse(din);
			case 0:
			case 42:
				return null;
			default:
				Debug.WriteLine("missing type: " + type);
				throw new Exception("deserialize(): " + type);
			}
		}

		private static byte DeserializeByte(MemoryStream din)
		{
			return (byte)din.ReadByte();
		}

		private static bool DeserializeBoolean(MemoryStream din)
		{
			return din.ReadByte() != 0;
		}

		private static short DeserializeShort(MemoryStream din)
		{
			byte[] array = new byte[2];
			din.Read(array, 0, 2);
			return (short)((array[0] << 8) | array[1]);
		}

		public static void Deserialize(out short value, byte[] source, ref int offset)
		{
			value = (short)((source[offset++] << 8) | source[offset++]);
		}

		private static int DeserializeInteger(MemoryStream din)
		{
			byte[] array = new byte[4];
			din.Read(array, 0, 4);
			return (array[0] << 24) | (array[1] << 16) | (array[2] << 8) | array[3];
		}

		public static void Deserialize(out int value, byte[] source, ref int offset)
		{
			value = (source[offset++] << 24) | (source[offset++] << 16) | (source[offset++] << 8) | source[offset++];
		}

		private static long DeserializeLong(MemoryStream din)
		{
			byte[] array = new byte[8];
			din.Read(array, 0, 8);
			if (BitConverter.IsLittleEndian)
			{
				return (long)(((ulong)array[0] << 56) | ((ulong)array[1] << 48) | ((ulong)array[2] << 40) | ((ulong)array[3] << 32) | ((ulong)array[4] << 24) | ((ulong)array[5] << 16) | ((ulong)array[6] << 8) | array[7]);
			}
			return BitConverter.ToInt64(array, 0);
		}

		private static float DeserializeFloat(MemoryStream din)
		{
			byte[] array = new byte[4];
			din.Read(array, 0, 4);
			if (BitConverter.IsLittleEndian)
			{
				byte b = array[0];
				byte b2 = array[1];
				array[0] = array[3];
				array[1] = array[2];
				array[2] = b2;
				array[3] = b;
			}
			return BitConverter.ToSingle(array, 0);
		}

		public static void Deserialize(out float value, byte[] source, ref int offset)
		{
			if (BitConverter.IsLittleEndian)
			{
				byte[] array = new byte[4];
				array[3] = source[offset++];
				array[2] = source[offset++];
				array[1] = source[offset++];
				array[0] = source[offset++];
				value = BitConverter.ToSingle(array, 0);
			}
			else
			{
				value = BitConverter.ToSingle(source, offset);
				offset += 4;
			}
		}

		private static double DeserializeDouble(MemoryStream din)
		{
			byte[] array = new byte[8];
			din.Read(array, 0, 8);
			if (BitConverter.IsLittleEndian)
			{
				byte b = array[0];
				byte b2 = array[1];
				byte b3 = array[2];
				byte b4 = array[3];
				array[0] = array[7];
				array[1] = array[6];
				array[2] = array[5];
				array[3] = array[4];
				array[4] = b4;
				array[5] = b3;
				array[6] = b2;
				array[7] = b;
			}
			return BitConverter.ToDouble(array, 0);
		}

		private static string DeserializeString(MemoryStream din)
		{
			short num = DeserializeShort(din);
			if (num == 0)
			{
				return "";
			}
			byte[] array = new byte[num];
			din.Read(array, 0, array.Length);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		private static Array DeserializeArray(MemoryStream din)
		{
			short num = DeserializeShort(din);
			byte b = (byte)din.ReadByte();
			Array array;
			switch (b)
			{
			case 121:
			{
				Array array2 = DeserializeArray(din);
				Type type = array2.GetType();
				array = Array.CreateInstance(type, num);
				array.SetValue(array2, 0);
				for (short num2 = 1; num2 < num; num2 = (short)(num2 + 1))
				{
					array2 = DeserializeArray(din);
					array.SetValue(array2, num2);
				}
				break;
			}
			case 120:
			{
				array = Array.CreateInstance(typeof(byte[]), num);
				for (short num2 = 0; num2 < num; num2 = (short)(num2 + 1))
				{
					Array array2 = DeserializeByteArray(din);
					array.SetValue(array2, num2);
				}
				break;
			}
			case 99:
			{
				byte b2 = (byte)din.ReadByte();
				CustomType value;
				if (CodeDict.TryGetValue(b2, out value))
				{
					array = Array.CreateInstance(value.Type, num);
					for (int i = 0; i < num; i++)
					{
						short num3 = DeserializeShort(din);
						byte[] array3 = new byte[num3];
						din.Read(array3, 0, num3);
						array.SetValue(value.DeserializeFunction(array3), i);
					}
					break;
				}
				throw new Exception("Cannot find deserializer for custom type: " + b2);
			}
			case 68:
			{
				Array arrayResult = null;
				DeserializeDictionaryArray(din, num, out arrayResult);
				return arrayResult;
			}
			default:
			{
				array = CreateArrayByType(b, num);
				for (short num2 = 0; num2 < num; num2 = (short)(num2 + 1))
				{
					array.SetValue(Deserialize(din, b), num2);
				}
				break;
			}
			}
			return array;
		}

		private static byte[] DeserializeByteArray(MemoryStream din)
		{
			int num = DeserializeInteger(din);
			byte[] array = new byte[num];
			din.Read(array, 0, num);
			return array;
		}

		private static int[] DeserializeIntArray(MemoryStream din)
		{
			int num = DeserializeInteger(din);
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = DeserializeInteger(din);
			}
			return array;
		}

		private static string[] DeserializeStringArray(MemoryStream din)
		{
			int num = DeserializeShort(din);
			string[] array = new string[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = DeserializeString(din);
			}
			return array;
		}

		private static object[] DeserializeObjectArray(MemoryStream din)
		{
			short num = DeserializeShort(din);
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				byte type = (byte)din.ReadByte();
				array.SetValue(Deserialize(din, type), i);
			}
			return array;
		}

		private static Hashtable DeserializeHashTable(MemoryStream din)
		{
			int num = DeserializeShort(din);
			Hashtable hashtable = new Hashtable(num);
			for (int i = 0; i < num; i++)
			{
				object key = Deserialize(din, (byte)din.ReadByte());
				object value = Deserialize(din, (byte)din.ReadByte());
				hashtable[key] = value;
			}
			return hashtable;
		}

		private static IDictionary DeserializeDictionary(MemoryStream din)
		{
			byte b = (byte)din.ReadByte();
			byte b2 = (byte)din.ReadByte();
			int num = DeserializeShort(din);
			bool flag = b == 0 || b == 42;
			bool flag2 = b2 == 0 || b2 == 42;
			Type typeOfCode = GetTypeOfCode(b);
			Type typeOfCode2 = GetTypeOfCode(b2);
			Type type = typeof(Dictionary<, >).MakeGenericType(typeOfCode, typeOfCode2);
			IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;
			for (int i = 0; i < num; i++)
			{
				object key = Deserialize(din, flag ? ((byte)din.ReadByte()) : b);
				object value = Deserialize(din, flag2 ? ((byte)din.ReadByte()) : b2);
				dictionary.Add(key, value);
			}
			return dictionary;
		}

		private static bool DeserializeDictionaryArray(MemoryStream din, short size, out Array arrayResult)
		{
			byte keyTypeCode;
			byte valTypeCode;
			Type type = DeserializeDictionaryType(din, out keyTypeCode, out valTypeCode);
			arrayResult = Array.CreateInstance(type, size);
			for (short num = 0; num < size; num = (short)(num + 1))
			{
				IDictionary dictionary = Activator.CreateInstance(type) as IDictionary;
				if (dictionary == null)
				{
					return false;
				}
				short num2 = DeserializeShort(din);
				for (int i = 0; i < num2; i++)
				{
					object key;
					if (keyTypeCode != 0)
					{
						key = Deserialize(din, keyTypeCode);
					}
					else
					{
						byte type2 = (byte)din.ReadByte();
						key = Deserialize(din, type2);
					}
					object value;
					if (valTypeCode != 0)
					{
						value = Deserialize(din, valTypeCode);
					}
					else
					{
						byte type2 = (byte)din.ReadByte();
						value = Deserialize(din, type2);
					}
					dictionary.Add(key, value);
				}
				arrayResult.SetValue(dictionary, num);
			}
			return true;
		}

		private static Type DeserializeDictionaryType(MemoryStream reader, out byte keyTypeCode, out byte valTypeCode)
		{
			keyTypeCode = (byte)reader.ReadByte();
			valTypeCode = (byte)reader.ReadByte();
			GpType gpType = (GpType)keyTypeCode;
			GpType gpType2 = (GpType)valTypeCode;
			Type type = ((gpType != 0) ? GetTypeOfCode(keyTypeCode) : typeof(object));
			Type type2 = ((gpType2 != 0) ? GetTypeOfCode(valTypeCode) : typeof(object));
			return typeof(Dictionary<, >).MakeGenericType(type, type2);
		}
	}
}
