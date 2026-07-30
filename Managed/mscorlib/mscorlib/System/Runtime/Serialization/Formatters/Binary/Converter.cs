using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071C RID: 1820
	internal sealed class Converter
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x00002111 File Offset: 0x00000311
		private Converter()
		{
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0010EBEC File Offset: 0x0010CDEC
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE internalPrimitiveTypeE;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					internalPrimitiveTypeE = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				internalPrimitiveTypeE = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return internalPrimitiveTypeE;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0010EC3C File Offset: 0x0010CE3C
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool flag = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				flag = true;
				break;
			}
			return flag;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0010EC98 File Offset: 0x0010CE98
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int num = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				num = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				num = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				num = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				num = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				num = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				num = 8;
				break;
			}
			return num;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x0010ED20 File Offset: 0x0010CF20
		internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
		{
			InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
			typeName = null;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				switch (code)
				{
				case InternalPrimitiveTypeE.Boolean:
				case InternalPrimitiveTypeE.Byte:
				case InternalPrimitiveTypeE.Char:
				case InternalPrimitiveTypeE.Double:
				case InternalPrimitiveTypeE.Int16:
				case InternalPrimitiveTypeE.Int32:
				case InternalPrimitiveTypeE.Int64:
				case InternalPrimitiveTypeE.SByte:
				case InternalPrimitiveTypeE.Single:
				case InternalPrimitiveTypeE.TimeSpan:
				case InternalPrimitiveTypeE.DateTime:
				case InternalPrimitiveTypeE.UInt16:
				case InternalPrimitiveTypeE.UInt32:
				case InternalPrimitiveTypeE.UInt64:
					internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
					typeName = "System." + Converter.ToComType(code);
					break;
				case InternalPrimitiveTypeE.Decimal:
					internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					typeName = "System." + Converter.ToComType(code);
					break;
				}
			}
			if (internalNameSpaceE == InternalNameSpaceE.None && type != null)
			{
				if (type == Converter.typeofString)
				{
					internalNameSpaceE = InternalNameSpaceE.XdrString;
				}
				else if (objectInfo == null)
				{
					typeName = type.FullName;
					if (type.Assembly == Converter.urtAssembly)
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
				else
				{
					typeName = objectInfo.GetTypeFullName();
					if (objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString))
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
			}
			return internalNameSpaceE;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0010EE01 File Offset: 0x0010D001
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x0010EE1C File Offset: 0x0010D01C
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0010EEC0 File Offset: 0x0010D0C0
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x0010EF62 File Offset: 0x0010D162
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x0010EF7C File Offset: 0x0010D17C
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array array = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				array = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				array = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				array = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				array = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				array = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				array = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				array = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				array = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				array = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				array = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				array = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				array = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				array = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				array = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				array = new ulong[length];
				break;
			}
			return array;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x0010F060 File Offset: 0x0010D260
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool flag = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0010F164 File Offset: 0x0010D364
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0010F206 File Offset: 0x0010D406
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0010F220 File Offset: 0x0010D420
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0010F28E File Offset: 0x0010D48E
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0010F2A8 File Offset: 0x0010D4A8
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0010F320 File Offset: 0x0010D520
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0010F33C File Offset: 0x0010D53C
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object obj;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				obj = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				obj = value;
			}
			return obj;
		}

		// Token: 0x040027B9 RID: 10169
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x040027BA RID: 10170
		private static volatile Type[] typeA;

		// Token: 0x040027BB RID: 10171
		private static volatile Type[] arrayTypeA;

		// Token: 0x040027BC RID: 10172
		private static volatile string[] valueA;

		// Token: 0x040027BD RID: 10173
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x040027BE RID: 10174
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x040027BF RID: 10175
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x040027C0 RID: 10176
		internal static Type typeofString = typeof(string);

		// Token: 0x040027C1 RID: 10177
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x040027C2 RID: 10178
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x040027C3 RID: 10179
		internal static Type typeofByte = typeof(byte);

		// Token: 0x040027C4 RID: 10180
		internal static Type typeofChar = typeof(char);

		// Token: 0x040027C5 RID: 10181
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x040027C6 RID: 10182
		internal static Type typeofDouble = typeof(double);

		// Token: 0x040027C7 RID: 10183
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x040027C8 RID: 10184
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x040027C9 RID: 10185
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x040027CA RID: 10186
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x040027CB RID: 10187
		internal static Type typeofSingle = typeof(float);

		// Token: 0x040027CC RID: 10188
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x040027CD RID: 10189
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x040027CE RID: 10190
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x040027CF RID: 10191
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x040027D0 RID: 10192
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x040027D1 RID: 10193
		internal static Type typeofObject = typeof(object);

		// Token: 0x040027D2 RID: 10194
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x040027D3 RID: 10195
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x040027D4 RID: 10196
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x040027D5 RID: 10197
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x040027D6 RID: 10198
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x040027D7 RID: 10199
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x040027D8 RID: 10200
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x040027D9 RID: 10201
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x040027DA RID: 10202
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x040027DB RID: 10203
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x040027DC RID: 10204
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x040027DD RID: 10205
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x040027DE RID: 10206
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x040027DF RID: 10207
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x040027E0 RID: 10208
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x040027E1 RID: 10209
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x040027E2 RID: 10210
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x040027E3 RID: 10211
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x040027E4 RID: 10212
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x040027E5 RID: 10213
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x040027E6 RID: 10214
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x040027E7 RID: 10215
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
