using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE3 RID: 2787
	internal static class Statics
	{
		// Token: 0x060063EF RID: 25583 RVA: 0x00143E00 File Offset: 0x00142000
		public static byte[] MetadataForString(string name, int prefixSize, int suffixSize, int additionalSize)
		{
			Statics.CheckName(name);
			int num = Encoding.UTF8.GetByteCount(name) + 3 + prefixSize + suffixSize;
			byte[] array = new byte[num];
			ushort num2 = checked((ushort)(num + additionalSize));
			array[0] = (byte)num2;
			array[1] = (byte)(num2 >> 8);
			Encoding.UTF8.GetBytes(name, 0, name.Length, array, 2 + prefixSize);
			return array;
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00143E54 File Offset: 0x00142054
		public static void EncodeTags(int tags, ref int pos, byte[] metadata)
		{
			int num = tags & 268435455;
			bool flag;
			do
			{
				byte b = (byte)((num >> 21) & 127);
				flag = (num & 2097151) != 0;
				b |= (flag ? 128 : 0);
				num <<= 7;
				if (metadata != null)
				{
					metadata[pos] = b;
				}
				pos++;
			}
			while (flag);
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00143EA2 File Offset: 0x001420A2
		public static byte Combine(int settingValue, byte defaultValue)
		{
			if ((int)((byte)settingValue) != settingValue)
			{
				return defaultValue;
			}
			return (byte)settingValue;
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x00143EAD File Offset: 0x001420AD
		public static byte Combine(int settingValue1, int settingValue2, byte defaultValue)
		{
			if ((int)((byte)settingValue1) == settingValue1)
			{
				return (byte)settingValue1;
			}
			if ((int)((byte)settingValue2) != settingValue2)
			{
				return defaultValue;
			}
			return (byte)settingValue2;
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x00143EC0 File Offset: 0x001420C0
		public static int Combine(int settingValue1, int settingValue2)
		{
			if ((int)((byte)settingValue1) != settingValue1)
			{
				return settingValue2;
			}
			return settingValue1;
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00143ECA File Offset: 0x001420CA
		public static void CheckName(string name)
		{
			if (name != null && 0 <= name.IndexOf('\0'))
			{
				throw new ArgumentOutOfRangeException("name");
			}
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x00143EE4 File Offset: 0x001420E4
		public static bool ShouldOverrideFieldName(string fieldName)
		{
			return fieldName.Length <= 2 && fieldName[0] == '_';
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x00143EFC File Offset: 0x001420FC
		public static TraceLoggingDataType MakeDataType(TraceLoggingDataType baseType, EventFieldFormat format)
		{
			return (baseType & (TraceLoggingDataType)31) | (TraceLoggingDataType)(format << 8);
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00143F06 File Offset: 0x00142106
		public static TraceLoggingDataType Format8(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char8;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean8;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt8;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x00143F3F File Offset: 0x0014213F
		public static TraceLoggingDataType Format16(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char16;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt16;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x00143F72 File Offset: 0x00142172
		public static TraceLoggingDataType Format32(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case (EventFieldFormat)1:
			case EventFieldFormat.String:
				break;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean32;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt32;
			default:
				if (format == EventFieldFormat.HResult)
				{
					return TraceLoggingDataType.HResult;
				}
				break;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x00143FAA File Offset: 0x001421AA
		public static TraceLoggingDataType Format64(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return TraceLoggingDataType.HexInt64;
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x00143FC1 File Offset: 0x001421C1
		public static TraceLoggingDataType FormatPtr(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return Statics.HexIntPtrType;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x00143FDB File Offset: 0x001421DB
		public static object CreateInstance(Type type, params object[] parameters)
		{
			return Activator.CreateInstance(type, parameters);
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x00143FE4 File Offset: 0x001421E4
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x0001F0E0 File Offset: 0x0001D2E0
		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x00143FEC File Offset: 0x001421EC
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties();
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x00143FF4 File Offset: 0x001421F4
		public static MethodInfo GetGetMethod(PropertyInfo propInfo)
		{
			return propInfo.GetGetMethod();
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x00143FFC File Offset: 0x001421FC
		public static MethodInfo GetDeclaredStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00144007 File Offset: 0x00142207
		public static bool HasCustomAttribute(PropertyInfo propInfo, Type attributeType)
		{
			return propInfo.GetCustomAttributes(attributeType, false).Length != 0;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x00144018 File Offset: 0x00142218
		public static AttributeType GetCustomAttribute<AttributeType>(PropertyInfo propInfo) where AttributeType : Attribute
		{
			AttributeType attributeType = default(AttributeType);
			object[] customAttributes = propInfo.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				attributeType = (AttributeType)((object)customAttributes[0]);
			}
			return attributeType;
		}

		// Token: 0x06006404 RID: 25604 RVA: 0x00144050 File Offset: 0x00142250
		public static AttributeType GetCustomAttribute<AttributeType>(Type type) where AttributeType : Attribute
		{
			AttributeType attributeType = default(AttributeType);
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				attributeType = (AttributeType)((object)customAttributes[0]);
			}
			return attributeType;
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x00144085 File Offset: 0x00142285
		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x06006406 RID: 25606 RVA: 0x00144090 File Offset: 0x00142290
		public static Type FindEnumerableElementType(Type type)
		{
			Type type2 = null;
			if (Statics.IsGenericMatch(type, typeof(IEnumerable<>)))
			{
				type2 = Statics.GetGenericArguments(type)[0];
			}
			else
			{
				foreach (Type type3 in type.FindInterfaces(new TypeFilter(Statics.IsGenericMatch), typeof(IEnumerable<>)))
				{
					if (type2 != null)
					{
						type2 = null;
						break;
					}
					type2 = Statics.GetGenericArguments(type3)[0];
				}
			}
			return type2;
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x00144102 File Offset: 0x00142302
		public static bool IsGenericMatch(Type type, object openType)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == (Type)openType;
		}

		// Token: 0x06006408 RID: 25608 RVA: 0x0014411F File Offset: 0x0014231F
		public static Delegate CreateDelegate(Type delegateType, MethodInfo methodInfo)
		{
			return Delegate.CreateDelegate(delegateType, methodInfo);
		}

		// Token: 0x06006409 RID: 25609 RVA: 0x00144128 File Offset: 0x00142328
		public static TraceLoggingTypeInfo GetTypeInfoInstance(Type dataType, List<Type> recursionCheck)
		{
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (dataType == typeof(int))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<int>.Instance;
			}
			else if (dataType == typeof(long))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<long>.Instance;
			}
			else if (dataType == typeof(string))
			{
				traceLoggingTypeInfo = TraceLoggingTypeInfo<string>.Instance;
			}
			else
			{
				traceLoggingTypeInfo = (TraceLoggingTypeInfo)Statics.GetDeclaredStaticMethod(typeof(TraceLoggingTypeInfo<>).MakeGenericType(new Type[] { dataType }), "GetInstance").Invoke(null, new object[] { recursionCheck });
			}
			return traceLoggingTypeInfo;
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x001441C0 File Offset: 0x001423C0
		public static TraceLoggingTypeInfo<DataType> CreateDefaultTypeInfo<DataType>(List<Type> recursionCheck)
		{
			Type typeFromHandle = typeof(DataType);
			if (recursionCheck.Contains(typeFromHandle))
			{
				throw new NotSupportedException(Environment.GetResourceString("Recursive type definition is not supported."));
			}
			recursionCheck.Add(typeFromHandle);
			EventDataAttribute customAttribute = Statics.GetCustomAttribute<EventDataAttribute>(typeFromHandle);
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (customAttribute != null || Statics.GetCustomAttribute<CompilerGeneratedAttribute>(typeFromHandle) != null)
			{
				traceLoggingTypeInfo = new InvokeTypeInfo<DataType>(new TypeAnalysis(typeFromHandle, customAttribute, recursionCheck));
			}
			else if (typeFromHandle.IsArray)
			{
				Type elementType = typeFromHandle.GetElementType();
				if (elementType == typeof(bool))
				{
					traceLoggingTypeInfo = new BooleanArrayTypeInfo();
				}
				else if (elementType == typeof(byte))
				{
					traceLoggingTypeInfo = new ByteArrayTypeInfo();
				}
				else if (elementType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new SByteArrayTypeInfo();
				}
				else if (elementType == typeof(short))
				{
					traceLoggingTypeInfo = new Int16ArrayTypeInfo();
				}
				else if (elementType == typeof(ushort))
				{
					traceLoggingTypeInfo = new UInt16ArrayTypeInfo();
				}
				else if (elementType == typeof(int))
				{
					traceLoggingTypeInfo = new Int32ArrayTypeInfo();
				}
				else if (elementType == typeof(uint))
				{
					traceLoggingTypeInfo = new UInt32ArrayTypeInfo();
				}
				else if (elementType == typeof(long))
				{
					traceLoggingTypeInfo = new Int64ArrayTypeInfo();
				}
				else if (elementType == typeof(ulong))
				{
					traceLoggingTypeInfo = new UInt64ArrayTypeInfo();
				}
				else if (elementType == typeof(char))
				{
					traceLoggingTypeInfo = new CharArrayTypeInfo();
				}
				else if (elementType == typeof(double))
				{
					traceLoggingTypeInfo = new DoubleArrayTypeInfo();
				}
				else if (elementType == typeof(float))
				{
					traceLoggingTypeInfo = new SingleArrayTypeInfo();
				}
				else if (elementType == typeof(IntPtr))
				{
					traceLoggingTypeInfo = new IntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(UIntPtr))
				{
					traceLoggingTypeInfo = new UIntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(Guid))
				{
					traceLoggingTypeInfo = new GuidArrayTypeInfo();
				}
				else
				{
					traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(ArrayTypeInfo<>).MakeGenericType(new Type[] { elementType }), new object[] { Statics.GetTypeInfoInstance(elementType, recursionCheck) });
				}
			}
			else if (Statics.IsEnum(typeFromHandle))
			{
				Type underlyingType = Enum.GetUnderlyingType(typeFromHandle);
				if (underlyingType == typeof(int))
				{
					traceLoggingTypeInfo = new EnumInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(uint))
				{
					traceLoggingTypeInfo = new EnumUInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(byte))
				{
					traceLoggingTypeInfo = new EnumByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new EnumSByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(short))
				{
					traceLoggingTypeInfo = new EnumInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(ushort))
				{
					traceLoggingTypeInfo = new EnumUInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(long))
				{
					traceLoggingTypeInfo = new EnumInt64TypeInfo<DataType>();
				}
				else
				{
					if (!(underlyingType == typeof(ulong)))
					{
						throw new NotSupportedException(Environment.GetResourceString("Enum type {0} underlying type {1} is not supported for serialization.", new object[] { typeFromHandle.Name, underlyingType.Name }));
					}
					traceLoggingTypeInfo = new EnumUInt64TypeInfo<DataType>();
				}
			}
			else if (typeFromHandle == typeof(string))
			{
				traceLoggingTypeInfo = new StringTypeInfo();
			}
			else if (typeFromHandle == typeof(bool))
			{
				traceLoggingTypeInfo = new BooleanTypeInfo();
			}
			else if (typeFromHandle == typeof(byte))
			{
				traceLoggingTypeInfo = new ByteTypeInfo();
			}
			else if (typeFromHandle == typeof(sbyte))
			{
				traceLoggingTypeInfo = new SByteTypeInfo();
			}
			else if (typeFromHandle == typeof(short))
			{
				traceLoggingTypeInfo = new Int16TypeInfo();
			}
			else if (typeFromHandle == typeof(ushort))
			{
				traceLoggingTypeInfo = new UInt16TypeInfo();
			}
			else if (typeFromHandle == typeof(int))
			{
				traceLoggingTypeInfo = new Int32TypeInfo();
			}
			else if (typeFromHandle == typeof(uint))
			{
				traceLoggingTypeInfo = new UInt32TypeInfo();
			}
			else if (typeFromHandle == typeof(long))
			{
				traceLoggingTypeInfo = new Int64TypeInfo();
			}
			else if (typeFromHandle == typeof(ulong))
			{
				traceLoggingTypeInfo = new UInt64TypeInfo();
			}
			else if (typeFromHandle == typeof(char))
			{
				traceLoggingTypeInfo = new CharTypeInfo();
			}
			else if (typeFromHandle == typeof(double))
			{
				traceLoggingTypeInfo = new DoubleTypeInfo();
			}
			else if (typeFromHandle == typeof(float))
			{
				traceLoggingTypeInfo = new SingleTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				traceLoggingTypeInfo = new DateTimeTypeInfo();
			}
			else if (typeFromHandle == typeof(decimal))
			{
				traceLoggingTypeInfo = new DecimalTypeInfo();
			}
			else if (typeFromHandle == typeof(IntPtr))
			{
				traceLoggingTypeInfo = new IntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(UIntPtr))
			{
				traceLoggingTypeInfo = new UIntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(Guid))
			{
				traceLoggingTypeInfo = new GuidTypeInfo();
			}
			else if (typeFromHandle == typeof(TimeSpan))
			{
				traceLoggingTypeInfo = new TimeSpanTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTimeOffset))
			{
				traceLoggingTypeInfo = new DateTimeOffsetTypeInfo();
			}
			else if (typeFromHandle == typeof(EmptyStruct))
			{
				traceLoggingTypeInfo = new NullTypeInfo<EmptyStruct>();
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(KeyValuePair<, >)))
			{
				Type[] genericArguments = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(KeyValuePairTypeInfo<, >).MakeGenericType(new Type[]
				{
					genericArguments[0],
					genericArguments[1]
				}), new object[] { recursionCheck });
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(Nullable<>)))
			{
				Type[] genericArguments2 = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(NullableTypeInfo<>).MakeGenericType(new Type[] { genericArguments2[0] }), new object[] { recursionCheck });
			}
			else
			{
				Type type = Statics.FindEnumerableElementType(typeFromHandle);
				if (!(type != null))
				{
					throw new ArgumentException(Environment.GetResourceString("The API supports only anonymous types or types decorated with the EventDataAttribute. Non-compliant type: {0} dataType.", new object[] { typeFromHandle.Name }));
				}
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(EnumerableTypeInfo<, >).MakeGenericType(new Type[] { typeFromHandle, type }), new object[] { Statics.GetTypeInfoInstance(type, recursionCheck) });
			}
			return (TraceLoggingTypeInfo<DataType>)traceLoggingTypeInfo;
		}

		// Token: 0x0400318A RID: 12682
		public const byte DefaultLevel = 5;

		// Token: 0x0400318B RID: 12683
		public const byte TraceLoggingChannel = 11;

		// Token: 0x0400318C RID: 12684
		public const byte InTypeMask = 31;

		// Token: 0x0400318D RID: 12685
		public const byte InTypeFixedCountFlag = 32;

		// Token: 0x0400318E RID: 12686
		public const byte InTypeVariableCountFlag = 64;

		// Token: 0x0400318F RID: 12687
		public const byte InTypeCustomCountFlag = 96;

		// Token: 0x04003190 RID: 12688
		public const byte InTypeCountMask = 96;

		// Token: 0x04003191 RID: 12689
		public const byte InTypeChainFlag = 128;

		// Token: 0x04003192 RID: 12690
		public const byte OutTypeMask = 127;

		// Token: 0x04003193 RID: 12691
		public const byte OutTypeChainFlag = 128;

		// Token: 0x04003194 RID: 12692
		public const EventTags EventTagsMask = (EventTags)268435455;

		// Token: 0x04003195 RID: 12693
		public static readonly TraceLoggingDataType IntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.Int64 : TraceLoggingDataType.Int32);

		// Token: 0x04003196 RID: 12694
		public static readonly TraceLoggingDataType UIntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.UInt64 : TraceLoggingDataType.UInt32);

		// Token: 0x04003197 RID: 12695
		public static readonly TraceLoggingDataType HexIntPtrType = ((IntPtr.Size == 8) ? TraceLoggingDataType.HexInt64 : TraceLoggingDataType.HexInt32);
	}
}
