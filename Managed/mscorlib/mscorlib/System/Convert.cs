using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Converts a base data type to another base data type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000145 RID: 325
	public static class Convert
	{
		/// <summary>Returns the <see cref="T:System.TypeCode" /> for the specified object.</summary>
		/// <returns>The <see cref="T:System.TypeCode" /> for <paramref name="value" />, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BAE RID: 2990 RVA: 0x00035C70 File Offset: 0x00033E70
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		/// <summary>Returns an indication whether the specified object is of type <see cref="T:System.DBNull" />.</summary>
		/// <returns>true if <paramref name="value" /> is of type <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="value">An object. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BAF RID: 2991 RVA: 0x00035C94 File Offset: 0x00033E94
		public static bool IsDBNull(object value)
		{
			if (value == global::System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object.</summary>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.-or-A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="typeCode">The type of object to return. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="typeCode" /> specifies a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00035CC0 File Offset: 0x00033EC0
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.-or- A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="typeCode">The type of object to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="typeCode" /> specifies a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for the <paramref name="typeCode" /> type recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x00035CD4 File Offset: 0x00033ED4
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException(Environment.GetResourceString("Object must implement IConvertible."));
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast to Empty."));
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast to DBNull."));
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException(Environment.GetResourceString("Unknown TypeCode value."));
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00035E54 File Offset: 0x00034054
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			RuntimeType runtimeType = targetType as RuntimeType;
			if (runtimeType != null)
			{
				if (value.GetType() == targetType)
				{
					return value;
				}
				if (runtimeType == Convert.ConvertTypes[3])
				{
					return value.ToBoolean(provider);
				}
				if (runtimeType == Convert.ConvertTypes[4])
				{
					return value.ToChar(provider);
				}
				if (runtimeType == Convert.ConvertTypes[5])
				{
					return value.ToSByte(provider);
				}
				if (runtimeType == Convert.ConvertTypes[6])
				{
					return value.ToByte(provider);
				}
				if (runtimeType == Convert.ConvertTypes[7])
				{
					return value.ToInt16(provider);
				}
				if (runtimeType == Convert.ConvertTypes[8])
				{
					return value.ToUInt16(provider);
				}
				if (runtimeType == Convert.ConvertTypes[9])
				{
					return value.ToInt32(provider);
				}
				if (runtimeType == Convert.ConvertTypes[10])
				{
					return value.ToUInt32(provider);
				}
				if (runtimeType == Convert.ConvertTypes[11])
				{
					return value.ToInt64(provider);
				}
				if (runtimeType == Convert.ConvertTypes[12])
				{
					return value.ToUInt64(provider);
				}
				if (runtimeType == Convert.ConvertTypes[13])
				{
					return value.ToSingle(provider);
				}
				if (runtimeType == Convert.ConvertTypes[14])
				{
					return value.ToDouble(provider);
				}
				if (runtimeType == Convert.ConvertTypes[15])
				{
					return value.ToDecimal(provider);
				}
				if (runtimeType == Convert.ConvertTypes[16])
				{
					return value.ToDateTime(provider);
				}
				if (runtimeType == Convert.ConvertTypes[18])
				{
					return value.ToString(provider);
				}
				if (runtimeType == Convert.ConvertTypes[1])
				{
					return value;
				}
				if (runtimeType == Convert.EnumType)
				{
					return (Enum)value;
				}
				if (runtimeType == Convert.ConvertTypes[2])
				{
					throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast to DBNull."));
				}
				if (runtimeType == Convert.ConvertTypes[0])
				{
					throw new InvalidCastException(Environment.GetResourceString("Object cannot be cast to Empty."));
				}
			}
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[]
			{
				value.GetType().FullName,
				targetType.FullName
			}));
		}

		/// <summary>Returns an object of the specified type and whose value is equivalent to the specified object.</summary>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.-or-A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="conversionType" /> is not a value type. </returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="conversionType">The type of object to return. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  -or-<paramref name="value" /> is null and <paramref name="conversionType" /> is a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB3 RID: 2995 RVA: 0x000360CF File Offset: 0x000342CF
		public static object ChangeType(object value, Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, Thread.CurrentThread.CurrentCulture);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.-or- <paramref name="value" />, if the <see cref="T:System.Type" /> of <paramref name="value" /> and <paramref name="conversionType" /> are equal.-or- A null reference (Nothing in Visual Basic), if <paramref name="value" /> is null and <paramref name="conversionType" /> is not a value type.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="conversionType">The type of object to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. -or-<paramref name="value" /> is null and <paramref name="conversionType" /> is a value type.-or-<paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for <paramref name="conversionType" /> recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB4 RID: 2996 RVA: 0x000360E4 File Offset: 0x000342E4
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException(Environment.GetResourceString("Null object cannot be converted to a value type."));
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException(Environment.GetResourceString("Object must implement IConvertible."));
				}
				else
				{
					RuntimeType runtimeType = conversionType as RuntimeType;
					if (runtimeType == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (runtimeType == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (runtimeType == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (runtimeType == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (runtimeType == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (runtimeType == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (runtimeType == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (runtimeType == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (runtimeType == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (runtimeType == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (runtimeType == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (runtimeType == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (runtimeType == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (runtimeType == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (runtimeType == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (runtimeType == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		/// <summary>Converts the value of a specified object to an equivalent Boolean value.</summary>
		/// <returns>true or false, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is null, the method returns false. </returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB5 RID: 2997 RVA: 0x00036314 File Offset: 0x00034514
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified object to an equivalent Boolean value, using the specified culture-specific formatting information.</summary>
		/// <returns>true or false, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is null, the method returns false.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB6 RID: 2998 RVA: 0x00036327 File Offset: 0x00034527
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		/// <summary>Returns the specified Boolean value; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The Boolean value to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB7 RID: 2999 RVA: 0x00002119 File Offset: 0x00000319
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003633A File Offset: 0x0003453A
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BB9 RID: 3001 RVA: 0x00036340 File Offset: 0x00034540
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBA RID: 3002 RVA: 0x0003633A File Offset: 0x0003453A
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBB RID: 3003 RVA: 0x0003633A File Offset: 0x0003453A
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBC RID: 3004 RVA: 0x0003633A File Offset: 0x0003453A
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBD RID: 3005 RVA: 0x0003633A File Offset: 0x0003453A
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBE RID: 3006 RVA: 0x0003633A File Offset: 0x0003453A
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BBF RID: 3007 RVA: 0x0003634E File Offset: 0x0003454E
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC0 RID: 3008 RVA: 0x0003634E File Offset: 0x0003454E
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent.</summary>
		/// <returns>true if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or false if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or null.</returns>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC1 RID: 3009 RVA: 0x00036355 File Offset: 0x00034555
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent, using the specified culture-specific formatting information.</summary>
		/// <returns>true if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or false if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or null.</returns>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC2 RID: 3010 RVA: 0x00036355 File Offset: 0x00034555
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC3 RID: 3011 RVA: 0x00036362 File Offset: 0x00034562
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0003636F File Offset: 0x0003456F
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent Boolean value.</summary>
		/// <returns>true if <paramref name="value" /> is not zero; otherwise, false.</returns>
		/// <param name="value">The number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00036380 File Offset: 0x00034580
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003638D File Offset: 0x0003458D
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified object to a Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to value, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003639B File Offset: 0x0003459B
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		/// <summary>Converts the value of the specified object to its equivalent Unicode character, using the specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC8 RID: 3016 RVA: 0x000363AE File Offset: 0x000345AE
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x000363C1 File Offset: 0x000345C1
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Returns the specified Unicode character value; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The Unicode character to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCA RID: 3018 RVA: 0x00002119 File Offset: 0x00000319
		public static char ToChar(char value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCB RID: 3019 RVA: 0x000363CF File Offset: 0x000345CF
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCC RID: 3020 RVA: 0x00002119 File Offset: 0x00000319
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCD RID: 3021 RVA: 0x000363CF File Offset: 0x000345CF
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCE RID: 3022 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BCF RID: 3023 RVA: 0x000363E7 File Offset: 0x000345E7
		public static char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00036407 File Offset: 0x00034607
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00036423 File Offset: 0x00034623
		public static char ToChar(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD2 RID: 3026 RVA: 0x00036445 File Offset: 0x00034645
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."));
			}
			return (char)value;
		}

		/// <summary>Converts the first character of a specified string to a Unicode character.</summary>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <param name="value">A string of length 1. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD3 RID: 3027 RVA: 0x00036462 File Offset: 0x00034662
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		/// <summary>Converts the first character of a specified string to a Unicode character, using specified culture-specific formatting information.</summary>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <param name="value">A string of length 1 or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003646B File Offset: 0x0003466B
		public static char ToChar(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("String must be exactly one character long."));
			}
			return value[0];
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD5 RID: 3029 RVA: 0x0003649B File Offset: 0x0003469B
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x000364A9 File Offset: 0x000346A9
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD7 RID: 3031 RVA: 0x000364B7 File Offset: 0x000346B7
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD8 RID: 3032 RVA: 0x000364C5 File Offset: 0x000346C5
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BD9 RID: 3033 RVA: 0x000364D3 File Offset: 0x000346D3
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format. </exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDA RID: 3034 RVA: 0x000364E6 File Offset: 0x000346E6
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDB RID: 3035 RVA: 0x000364F9 File Offset: 0x000346F9
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Returns the specified 8-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 8-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDC RID: 3036 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDD RID: 3037 RVA: 0x00036501 File Offset: 0x00034701
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDE RID: 3038 RVA: 0x00036501 File Offset: 0x00034701
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BDF RID: 3039 RVA: 0x0003651A File Offset: 0x0003471A
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE0 RID: 3040 RVA: 0x00036501 File Offset: 0x00034701
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003651A File Offset: 0x0003471A
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE2 RID: 3042 RVA: 0x00036538 File Offset: 0x00034738
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE3 RID: 3043 RVA: 0x00036553 File Offset: 0x00034753
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00036573 File Offset: 0x00034773
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003658D File Offset: 0x0003478D
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE6 RID: 3046 RVA: 0x00036596 File Offset: 0x00034796
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x000365A3 File Offset: 0x000347A3
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if value is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x000365B1 File Offset: 0x000347B1
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BE9 RID: 3049 RVA: 0x000365C3 File Offset: 0x000347C3
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			return sbyte.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BEA RID: 3050 RVA: 0x000365CD File Offset: 0x000347CD
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. -or-Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BEB RID: 3051 RVA: 0x000365DB File Offset: 0x000347DB
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. -or-Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BEC RID: 3052 RVA: 0x000365EE File Offset: 0x000347EE
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BED RID: 3053 RVA: 0x000364F9 File Offset: 0x000346F9
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Returns the specified 8-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 8-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BEE RID: 3054 RVA: 0x00002119 File Offset: 0x00000319
		public static byte ToByte(byte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BEF RID: 3055 RVA: 0x00036601 File Offset: 0x00034801
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to be converted. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0003661D File Offset: 0x0003481D
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x00036635 File Offset: 0x00034835
		public static byte ToByte(short value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF2 RID: 3058 RVA: 0x00036601 File Offset: 0x00034801
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00036635 File Offset: 0x00034835
		public static byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF4 RID: 3060 RVA: 0x00036655 File Offset: 0x00034855
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00036671 File Offset: 0x00034871
		public static byte ToByte(long value)
		{
			if (value < 0L || value > 255L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00036693 File Offset: 0x00034893
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">A single-precision floating-point number. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x000366B0 File Offset: 0x000348B0
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF8 RID: 3064 RVA: 0x000366B9 File Offset: 0x000348B9
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BF9 RID: 3065 RVA: 0x000366C6 File Offset: 0x000348C6
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFA RID: 3066 RVA: 0x000366D4 File Offset: 0x000348D4
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer, using specified culture-specific formatting information.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFB RID: 3067 RVA: 0x000366E6 File Offset: 0x000348E6
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFC RID: 3068 RVA: 0x000366F5 File Offset: 0x000348F5
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFD RID: 3069 RVA: 0x00036703 File Offset: 0x00034903
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFE RID: 3070 RVA: 0x00036716 File Offset: 0x00034916
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000BFF RID: 3071 RVA: 0x000364F9 File Offset: 0x000346F9
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C00 RID: 3072 RVA: 0x00036729 File Offset: 0x00034929
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C01 RID: 3073 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C02 RID: 3074 RVA: 0x00002119 File Offset: 0x00000319
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C03 RID: 3075 RVA: 0x00036729 File Offset: 0x00034929
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>The 16-bit signed integer equivalent of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C04 RID: 3076 RVA: 0x00036745 File Offset: 0x00034945
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C05 RID: 3077 RVA: 0x00036769 File Offset: 0x00034969
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Returns the specified 16-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 16-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C06 RID: 3078 RVA: 0x00002119 File Offset: 0x00000319
		public static short ToInt16(short value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C07 RID: 3079 RVA: 0x00036787 File Offset: 0x00034987
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C08 RID: 3080 RVA: 0x000367AD File Offset: 0x000349AD
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C09 RID: 3081 RVA: 0x000367CA File Offset: 0x000349CA
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0A RID: 3082 RVA: 0x000367D3 File Offset: 0x000349D3
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0B RID: 3083 RVA: 0x000367E0 File Offset: 0x000349E0
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0C RID: 3084 RVA: 0x000367EE File Offset: 0x000349EE
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0D RID: 3085 RVA: 0x00036800 File Offset: 0x00034A00
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0E RID: 3086 RVA: 0x0003680F File Offset: 0x00034A0F
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C0F RID: 3087 RVA: 0x0003681D File Offset: 0x00034A1D
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C10 RID: 3088 RVA: 0x00036830 File Offset: 0x00034A30
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C11 RID: 3089 RVA: 0x000364F9 File Offset: 0x000346F9
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>The 16-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C12 RID: 3090 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C13 RID: 3091 RVA: 0x00036843 File Offset: 0x00034A43
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C14 RID: 3092 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C15 RID: 3093 RVA: 0x00036843 File Offset: 0x00034A43
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C16 RID: 3094 RVA: 0x0003685B File Offset: 0x00034A5B
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Returns the specified 16-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 16-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C17 RID: 3095 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C18 RID: 3096 RVA: 0x0003687B File Offset: 0x00034A7B
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C19 RID: 3097 RVA: 0x00036897 File Offset: 0x00034A97
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			if (value < 0L || value > 65535L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1A RID: 3098 RVA: 0x000368B9 File Offset: 0x00034AB9
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1B RID: 3099 RVA: 0x000368D6 File Offset: 0x00034AD6
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1C RID: 3100 RVA: 0x000368DF File Offset: 0x00034ADF
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1D RID: 3101 RVA: 0x000368EC File Offset: 0x00034AEC
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1E RID: 3102 RVA: 0x000368FA File Offset: 0x00034AFA
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003690C File Offset: 0x00034B0C
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C20 RID: 3104 RVA: 0x0003691B File Offset: 0x00034B1B
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C21 RID: 3105 RVA: 0x00036929 File Offset: 0x00034B29
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C22 RID: 3106 RVA: 0x0003693C File Offset: 0x00034B3C
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C23 RID: 3107 RVA: 0x000364F9 File Offset: 0x000346F9
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C24 RID: 3108 RVA: 0x00002119 File Offset: 0x00000319
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C25 RID: 3109 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C26 RID: 3110 RVA: 0x00002119 File Offset: 0x00000319
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C27 RID: 3111 RVA: 0x00002119 File Offset: 0x00000319
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C28 RID: 3112 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C29 RID: 3113 RVA: 0x0003694F File Offset: 0x00034B4F
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
			}
			return (int)value;
		}

		/// <summary>Returns the specified 32-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 32-bit signed integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2A RID: 3114 RVA: 0x00002119 File Offset: 0x00000319
		public static int ToInt32(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2B RID: 3115 RVA: 0x0003696A File Offset: 0x00034B6A
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2C RID: 3116 RVA: 0x00036990 File Offset: 0x00034B90
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2D RID: 3117 RVA: 0x000369AD File Offset: 0x00034BAD
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2E RID: 3118 RVA: 0x000369B8 File Offset: 0x00034BB8
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C2F RID: 3119 RVA: 0x00036A4E File Offset: 0x00034C4E
		[SecuritySafeCritical]
		public static int ToInt32(decimal value)
		{
			return decimal.FCallToInt32(value);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C30 RID: 3120 RVA: 0x00036A56 File Offset: 0x00034C56
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C31 RID: 3121 RVA: 0x00036A68 File Offset: 0x00034C68
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert.</param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C32 RID: 3122 RVA: 0x00036A77 File Offset: 0x00034C77
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C33 RID: 3123 RVA: 0x00036A85 File Offset: 0x00034C85
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C34 RID: 3124 RVA: 0x00036A98 File Offset: 0x00034C98
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C35 RID: 3125 RVA: 0x000364F9 File Offset: 0x000346F9
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C36 RID: 3126 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C37 RID: 3127 RVA: 0x00036AAB File Offset: 0x00034CAB
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C38 RID: 3128 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C39 RID: 3129 RVA: 0x00036AAB File Offset: 0x00034CAB
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3A RID: 3130 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3B RID: 3131 RVA: 0x00036AAB File Offset: 0x00034CAB
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return (uint)value;
		}

		/// <summary>Returns the specified 32-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 32-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3C RID: 3132 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3D RID: 3133 RVA: 0x00036AC2 File Offset: 0x00034CC2
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)(-1)))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3E RID: 3134 RVA: 0x00036AE0 File Offset: 0x00034CE0
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)(-1))
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C3F RID: 3135 RVA: 0x00036AF9 File Offset: 0x00034CF9
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C40 RID: 3136 RVA: 0x00036B04 File Offset: 0x00034D04
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C41 RID: 3137 RVA: 0x00036B64 File Offset: 0x00034D64
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C42 RID: 3138 RVA: 0x00036B72 File Offset: 0x00034D72
		[CLSCompliant(false)]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C43 RID: 3139 RVA: 0x00036B84 File Offset: 0x00034D84
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C44 RID: 3140 RVA: 0x00036B93 File Offset: 0x00034D93
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C45 RID: 3141 RVA: 0x00036BA1 File Offset: 0x00034DA1
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C46 RID: 3142 RVA: 0x00036BB5 File Offset: 0x00034DB5
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit signed integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C47 RID: 3143 RVA: 0x00036BC9 File Offset: 0x00034DC9
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C48 RID: 3144 RVA: 0x00036BD3 File Offset: 0x00034DD3
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C49 RID: 3145 RVA: 0x00036BD7 File Offset: 0x00034DD7
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4A RID: 3146 RVA: 0x00036BD3 File Offset: 0x00034DD3
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4B RID: 3147 RVA: 0x00036BD7 File Offset: 0x00034DD7
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4C RID: 3148 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4D RID: 3149 RVA: 0x00036BD7 File Offset: 0x00034DD7
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4E RID: 3150 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C4F RID: 3151 RVA: 0x00036BDB File Offset: 0x00034DDB
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int64."));
			}
			return (long)value;
		}

		/// <summary>Returns the specified 64-bit signed integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A 64-bit signed integer. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C50 RID: 3152 RVA: 0x00002119 File Offset: 0x00000319
		public static long ToInt64(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C51 RID: 3153 RVA: 0x00036BFA File Offset: 0x00034DFA
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C52 RID: 3154 RVA: 0x00036C03 File Offset: 0x00034E03
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit signed integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C53 RID: 3155 RVA: 0x00036C0C File Offset: 0x00034E0C
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C54 RID: 3156 RVA: 0x00036C1A File Offset: 0x00034E1A
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C55 RID: 3157 RVA: 0x00036C2D File Offset: 0x00034E2D
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C56 RID: 3158 RVA: 0x00036C3D File Offset: 0x00034E3D
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C57 RID: 3159 RVA: 0x00036C4B File Offset: 0x00034E4B
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C58 RID: 3160 RVA: 0x00036C5F File Offset: 0x00034E5F
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C59 RID: 3161 RVA: 0x00036C73 File Offset: 0x00034E73
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5A RID: 3162 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5B RID: 3163 RVA: 0x00036C7D File Offset: 0x00034E7D
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5C RID: 3164 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5D RID: 3165 RVA: 0x00036C7D File Offset: 0x00034E7D
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5E RID: 3166 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C5F RID: 3167 RVA: 0x00036C7D File Offset: 0x00034E7D
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C60 RID: 3168 RVA: 0x00036BD3 File Offset: 0x00034DD3
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C61 RID: 3169 RVA: 0x00036C95 File Offset: 0x00034E95
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
			}
			return (ulong)value;
		}

		/// <summary>Returns the specified 64-bit unsigned integer; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The 64-bit unsigned integer to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C62 RID: 3170 RVA: 0x00002119 File Offset: 0x00000319
		[CLSCompliant(false)]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C63 RID: 3171 RVA: 0x00036CAD File Offset: 0x00034EAD
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C64 RID: 3172 RVA: 0x00036CB6 File Offset: 0x00034EB6
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C65 RID: 3173 RVA: 0x00036CBF File Offset: 0x00034EBF
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C66 RID: 3174 RVA: 0x00036CCD File Offset: 0x00034ECD
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9). </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C67 RID: 3175 RVA: 0x00036CE0 File Offset: 0x00034EE0
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C68 RID: 3176 RVA: 0x00036CF0 File Offset: 0x00034EF0
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		/// <summary>Converts the value of the specified object to a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C69 RID: 3177 RVA: 0x00036CFE File Offset: 0x00034EFE
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		/// <summary>Converts the value of the specified object to an single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6A RID: 3178 RVA: 0x00036D15 File Offset: 0x00034F15
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6B RID: 3179 RVA: 0x00036D2C File Offset: 0x00034F2C
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6C RID: 3180 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6D RID: 3181 RVA: 0x00036D30 File Offset: 0x00034F30
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6E RID: 3182 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C6F RID: 3183 RVA: 0x00036D2C File Offset: 0x00034F2C
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C70 RID: 3184 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C71 RID: 3185 RVA: 0x00036D3E File Offset: 0x00034F3E
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C72 RID: 3186 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C73 RID: 3187 RVA: 0x00036D3E File Offset: 0x00034F3E
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		/// <summary>Returns the specified single-precision floating-point number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The single-precision floating-point number to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C74 RID: 3188 RVA: 0x00002119 File Offset: 0x00000319
		public static float ToSingle(float value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.<paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C75 RID: 3189 RVA: 0x00036D2C File Offset: 0x00034F2C
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.<paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C76 RID: 3190 RVA: 0x00036D43 File Offset: 0x00034F43
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C77 RID: 3191 RVA: 0x00036D4C File Offset: 0x00034F4C
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C78 RID: 3192 RVA: 0x00036D62 File Offset: 0x00034F62
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent single-precision floating-point number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C79 RID: 3193 RVA: 0x00036D79 File Offset: 0x00034F79
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7A RID: 3194 RVA: 0x00036D83 File Offset: 0x00034F83
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		/// <summary>Converts the value of the specified object to a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7B RID: 3195 RVA: 0x00036D91 File Offset: 0x00034F91
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		/// <summary>Converts the value of the specified object to an double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7C RID: 3196 RVA: 0x00036DAC File Offset: 0x00034FAC
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>The 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7D RID: 3197 RVA: 0x00036DC7 File Offset: 0x00034FC7
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>The double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7E RID: 3198 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C7F RID: 3199 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C80 RID: 3200 RVA: 0x00036DCB File Offset: 0x00034FCB
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C81 RID: 3201 RVA: 0x00036DC7 File Offset: 0x00034FC7
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C82 RID: 3202 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C83 RID: 3203 RVA: 0x00036DD9 File Offset: 0x00034FD9
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C84 RID: 3204 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C85 RID: 3205 RVA: 0x00036DD9 File Offset: 0x00034FD9
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C86 RID: 3206 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		/// <summary>Returns the specified double-precision floating-point number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The double-precision floating-point number to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C87 RID: 3207 RVA: 0x00002119 File Offset: 0x00000319
		public static double ToDouble(double value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C88 RID: 3208 RVA: 0x00036DDE File Offset: 0x00034FDE
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C89 RID: 3209 RVA: 0x00036DE7 File Offset: 0x00034FE7
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8A RID: 3210 RVA: 0x00036E01 File Offset: 0x00035001
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent double-precision floating-point number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8B RID: 3211 RVA: 0x00036E1C File Offset: 0x0003501C
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8C RID: 3212 RVA: 0x00036E26 File Offset: 0x00035026
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8D RID: 3213 RVA: 0x00036E34 File Offset: 0x00035034
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.-or-The conversion is not supported. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8E RID: 3214 RVA: 0x00036E4B File Offset: 0x0003504B
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C8F RID: 3215 RVA: 0x00036E62 File Offset: 0x00035062
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent decimal number.</summary>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C90 RID: 3216 RVA: 0x00036E6A File Offset: 0x0003506A
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C91 RID: 3217 RVA: 0x00036E72 File Offset: 0x00035072
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C92 RID: 3218 RVA: 0x00036E80 File Offset: 0x00035080
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C93 RID: 3219 RVA: 0x00036E88 File Offset: 0x00035088
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C94 RID: 3220 RVA: 0x00036E90 File Offset: 0x00035090
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C95 RID: 3221 RVA: 0x00036E98 File Offset: 0x00035098
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C96 RID: 3222 RVA: 0x00036EA0 File Offset: 0x000350A0
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C97 RID: 3223 RVA: 0x00036EA8 File Offset: 0x000350A8
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to the equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C98 RID: 3224 RVA: 0x00036EB0 File Offset: 0x000350B0
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />. </returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C99 RID: 3225 RVA: 0x00036EB8 File Offset: 0x000350B8
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number.</summary>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9A RID: 3226 RVA: 0x00036EC0 File Offset: 0x000350C0
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9B RID: 3227 RVA: 0x00036ED6 File Offset: 0x000350D6
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, NumberStyles.Number, provider);
		}

		/// <summary>Returns the specified decimal number; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9C RID: 3228 RVA: 0x00002119 File Offset: 0x00000319
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		/// <summary>Converts the specified Boolean value to the equivalent decimal number.</summary>
		/// <returns>The number 1 if <paramref name="value" /> is true; otherwise, 0.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9D RID: 3229 RVA: 0x00036EEA File Offset: 0x000350EA
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9E RID: 3230 RVA: 0x00036EF8 File Offset: 0x000350F8
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		/// <summary>Returns the specified <see cref="T:System.DateTime" /> object; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A date and time value. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000C9F RID: 3231 RVA: 0x00002119 File Offset: 0x00000319
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or a date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or null. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00036F06 File Offset: 0x00035106
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object, using the specified culture-specific formatting information.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface. -or-The conversion is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x00036F1D File Offset: 0x0003511D
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		/// <summary>Converts the specified string representation of a date and time to an equivalent date and time value.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">The string representation of a date and time.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x00036F34 File Offset: 0x00035134
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent date and time, using the specified culture-specific formatting information.</summary>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains a date and time to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x00036F4C File Offset: 0x0003514C
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x00036F60 File Offset: 0x00035160
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00036F6E File Offset: 0x0003516E
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x00036F7C File Offset: 0x0003517C
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA7 RID: 3239 RVA: 0x00036F8A File Offset: 0x0003518A
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x00036F98 File Offset: 0x00035198
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x00036FA6 File Offset: 0x000351A6
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAA RID: 3242 RVA: 0x00036FB4 File Offset: 0x000351B4
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAB RID: 3243 RVA: 0x00036FC2 File Offset: 0x000351C2
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAC RID: 3244 RVA: 0x00036FD0 File Offset: 0x000351D0
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAD RID: 3245 RVA: 0x00036FDE File Offset: 0x000351DE
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The single-precision floating-point value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAE RID: 3246 RVA: 0x00036FEC File Offset: 0x000351EC
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The double-precision floating-point value to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAF RID: 3247 RVA: 0x00036FFA File Offset: 0x000351FA
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <param name="value">The number to convert. </param>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00037008 File Offset: 0x00035208
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if value is null.</returns>
		/// <param name="value">An object that supplies the value to convert, or null. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB1 RID: 3249 RVA: 0x00037016 File Offset: 0x00035216
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if <paramref name="value" /> is null.</returns>
		/// <param name="value">An object that supplies the value to convert, or null. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00037020 File Offset: 0x00035220
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB3 RID: 3251 RVA: 0x00037061 File Offset: 0x00035261
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Boolean value to convert. </param>
		/// <param name="provider">An instance of an object. This parameter is ignored.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x0003706A File Offset: 0x0003526A
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB5 RID: 3253 RVA: 0x00037074 File Offset: 0x00035274
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0003707C File Offset: 0x0003527C
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB7 RID: 3255 RVA: 0x00037086 File Offset: 0x00035286
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00037094 File Offset: 0x00035294
		[CLSCompliant(false)]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CB9 RID: 3257 RVA: 0x0003709E File Offset: 0x0003529E
		public static string ToString(byte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBA RID: 3258 RVA: 0x000370AC File Offset: 0x000352AC
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBB RID: 3259 RVA: 0x000370B6 File Offset: 0x000352B6
		public static string ToString(short value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBC RID: 3260 RVA: 0x000370C4 File Offset: 0x000352C4
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBD RID: 3261 RVA: 0x000370CE File Offset: 0x000352CE
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBE RID: 3262 RVA: 0x000370DC File Offset: 0x000352DC
		[CLSCompliant(false)]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CBF RID: 3263 RVA: 0x000370E6 File Offset: 0x000352E6
		public static string ToString(int value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x000370F4 File Offset: 0x000352F4
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC1 RID: 3265 RVA: 0x000370FE File Offset: 0x000352FE
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003710C File Offset: 0x0003530C
		[CLSCompliant(false)]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC3 RID: 3267 RVA: 0x00037116 File Offset: 0x00035316
		public static string ToString(long value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC4 RID: 3268 RVA: 0x00037124 File Offset: 0x00035324
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0003712E File Offset: 0x0003532E
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003713C File Offset: 0x0003533C
		[CLSCompliant(false)]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00037146 File Offset: 0x00035346
		public static string ToString(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00037154 File Offset: 0x00035354
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003715E File Offset: 0x0003535E
		public static string ToString(double value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCA RID: 3274 RVA: 0x0003716C File Offset: 0x0003536C
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCB RID: 3275 RVA: 0x00037176 File Offset: 0x00035376
		public static string ToString(decimal value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCC RID: 3276 RVA: 0x00037184 File Offset: 0x00035384
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCD RID: 3277 RVA: 0x0003718E File Offset: 0x0003538E
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		/// <param name="value">The date and time value to convert. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCE RID: 3278 RVA: 0x00037197 File Offset: 0x00035397
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The string to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CCF RID: 3279 RVA: 0x00002119 File Offset: 0x00000319
		public static string ToString(string value)
		{
			return value;
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">The string to return. </param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD0 RID: 3280 RVA: 0x00002119 File Offset: 0x00000319
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD1 RID: 3281 RVA: 0x000371A4 File Offset: 0x000353A4
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 255)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00037200 File Offset: 0x00035400
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00037268 File Offset: 0x00035468
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD4 RID: 3284 RVA: 0x000372D8 File Offset: 0x000354D8
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			int num = ParseNumbers.StringToInt(value, fromBase, 4608);
			if (num < 0 || num > 65535)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD5 RID: 3285 RVA: 0x00037332 File Offset: 0x00035532
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.StringToInt(value, fromBase, 4096);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00037362 File Offset: 0x00035562
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return (uint)ParseNumbers.StringToInt(value, fromBase, 4608);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD7 RID: 3287 RVA: 0x00037392 File Offset: 0x00035592
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.StringToLong(value, fromBase, 4096);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is null.</returns>
		/// <param name="value">A string that contains the number to convert. </param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16. -or-<paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.-or-<paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD8 RID: 3288 RVA: 0x000373C2 File Offset: 0x000355C2
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return (ulong)ParseNumbers.StringToLong(value, fromBase, 4608);
		}

		/// <summary>Converts the value of an 8-bit unsigned integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CD9 RID: 3289 RVA: 0x000373F2 File Offset: 0x000355F2
		[SecuritySafeCritical]
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		/// <summary>Converts the value of a 16-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 16-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDA RID: 3290 RVA: 0x00037422 File Offset: 0x00035622
		[SecuritySafeCritical]
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		/// <summary>Converts the value of a 32-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDB RID: 3291 RVA: 0x00037455 File Offset: 0x00035655
		[SecuritySafeCritical]
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		/// <summary>Converts the value of a 64-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDC RID: 3292 RVA: 0x00037484 File Offset: 0x00035684
		[SecuritySafeCritical]
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid Base."));
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.</summary>
		/// <returns>The string representation, in base 64, of the contents of <paramref name="inArray" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDD RID: 3293 RVA: 0x000374B3 File Offset: 0x000356B3
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, Base64FormattingOptions.None);
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. A parameter specifies whether to insert line breaks in the return value.</summary>
		/// <returns>The string representation in base 64 of the elements in <paramref name="inArray" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDE RID: 3294 RVA: 0x000374CE File Offset: 0x000356CE
		[ComVisible(false)]
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(inArray, 0, inArray.Length, options);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of elements in the array to convert.</summary>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="offset">An offset in <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.-or- <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CDF RID: 3295 RVA: 0x000374E9 File Offset: 0x000356E9
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, the number of elements in the array to convert, and whether to insert line breaks in the return value.</summary>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <param name="inArray">An array of 8-bit unsigned integers. </param>
		/// <param name="offset">An offset in <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.-or- <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CE0 RID: 3296 RVA: 0x000374F4 File Offset: 0x000356F4
		[SecuritySafeCritical]
		[ComVisible(false)]
		public unsafe static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Value must be positive."));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)options }));
			}
			int num = inArray.Length;
			if (offset > num - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Offset and length must refer to a position in the string."));
			}
			if (num == 0)
			{
				return string.Empty;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			string text2;
			string text = (text2 = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(length, flag)));
			char* ptr = text2;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			byte* ptr2;
			if (inArray == null || inArray.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &inArray[0];
			}
			Convert.ConvertToBase64Array(ptr, ptr2, offset, length, flag);
			return text;
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, and the number of elements in the input array to convert.</summary>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <param name="inArray">An input array of 8-bit unsigned integers. </param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="outArray">An output array of Unicode characters. </param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.-or- <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.-or- <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CE1 RID: 3297 RVA: 0x000375DA File Offset: 0x000357DA
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, the number of elements in the input array to convert, and whether line breaks are inserted in the output array.</summary>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <param name="inArray">An input array of 8-bit unsigned integers. </param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert. </param>
		/// <param name="outArray">An output array of Unicode characters. </param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />. </param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.-or- <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.-or- <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CE2 RID: 3298 RVA: 0x000375E8 File Offset: 0x000357E8
		[ComVisible(false)]
		[SecuritySafeCritical]
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("Value must be positive."));
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("Value must be positive."));
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[] { (int)options }));
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", Environment.GetResourceString("Offset and length must refer to a position in the string."));
			}
			if (num == 0)
			{
				return 0;
			}
			bool flag = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, flag);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", Environment.GetResourceString("Either offset did not refer to a position in the string, or there is an insufficient length of destination character array."));
			}
			int num4;
			fixed (char* ptr = &outArray[offsetOut])
			{
				char* ptr2 = ptr;
				fixed (byte[] array = inArray)
				{
					byte* ptr3;
					if (inArray == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					num4 = Convert.ConvertToBase64Array(ptr2, ptr3, offsetIn, length, flag);
				}
			}
			return num4;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00037714 File Offset: 0x00035914
		[SecurityCritical]
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			char[] array;
			char* ptr;
			if ((array = Convert.base64Table) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int i;
			for (i = offset; i < num2; i += 3)
			{
				if (insertLineBreaks)
				{
					if (num4 == 76)
					{
						outChars[num3++] = '\r';
						outChars[num3++] = '\n';
						num4 = 0;
					}
					num4 += 4;
				}
				outChars[num3] = ptr[(inData[i] & 252) >> 2];
				outChars[num3 + 1] = ptr[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
				outChars[num3 + 2] = ptr[((int)(inData[i + 1] & 15) << 2) | ((inData[i + 2] & 192) >> 6)];
				outChars[num3 + 3] = ptr[inData[i + 2] & 63];
				num3 += 4;
			}
			i = num2;
			if (insertLineBreaks && num != 0 && num4 == 76)
			{
				outChars[num3++] = '\r';
				outChars[num3++] = '\n';
			}
			if (num != 1)
			{
				if (num == 2)
				{
					outChars[num3] = ptr[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr[((int)(inData[i] & 3) << 4) | ((inData[i + 1] & 240) >> 4)];
					outChars[num3 + 2] = ptr[(inData[i + 1] & 15) << 2];
					outChars[num3 + 3] = ptr[64];
					num3 += 4;
				}
			}
			else
			{
				outChars[num3] = ptr[(inData[i] & 252) >> 2];
				outChars[num3 + 1] = ptr[(inData[i] & 3) << 4];
				outChars[num3 + 2] = ptr[64];
				outChars[num3 + 3] = ptr[64];
				num3 += 4;
			}
			array = null;
			return num3;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003792C File Offset: 0x00035B2C
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		/// <summary>Converts the specified string, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array.</summary>
		/// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="s" />.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" />, ignoring white-space characters, is not zero or a multiple of 4. -or-The format of <paramref name="s" /> is invalid. <paramref name="s" /> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CE5 RID: 3301 RVA: 0x00037984 File Offset: 0x00035B84
		[SecuritySafeCritical]
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Convert.FromBase64CharPtr(ptr, s.Length);
		}

		/// <summary>Converts a subset of a Unicode character array, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array. Parameters specify the subset in the input array and the number of elements to convert.</summary>
		/// <returns>An array of 8-bit unsigned integers equivalent to <paramref name="length" /> elements at position <paramref name="offset" /> in <paramref name="inArray" />.</returns>
		/// <param name="inArray">A Unicode character array. </param>
		/// <param name="offset">A position within <paramref name="inArray" />. </param>
		/// <param name="length">The number of elements in <paramref name="inArray" /> to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is less than 0.-or- <paramref name="offset" /> plus <paramref name="length" /> indicates a position not within <paramref name="inArray" />. </exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="inArray" />, ignoring white-space characters, is not zero or a multiple of 4. -or-The format of <paramref name="inArray" /> is invalid. <paramref name="inArray" /> contains a non-base-64 character, more than two padding characters, or a non-white-space character among the padding characters. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CE6 RID: 3302 RVA: 0x000379BC File Offset: 0x00035BBC
		[SecuritySafeCritical]
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Value must be positive."));
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Offset and length must refer to a position in the string."));
			}
			char* ptr;
			if (inArray == null || inArray.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &inArray[0];
			}
			return Convert.FromBase64CharPtr(ptr + offset, length);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00037A4C File Offset: 0x00035C4C
		[SecurityCritical]
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			int num2 = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
			byte[] array2;
			byte[] array = (array2 = new byte[num2]);
			byte* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Convert.FromBase64_Decode(inputPtr, inputLength, ptr, num2);
			array2 = null;
			return array;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00037AB4 File Offset: 0x00035CB4
		[SecurityCritical]
		private unsafe static int FromBase64_Decode(char* startInputPtr, int inputLength, byte* startDestPtr, int destLength)
		{
			char* ptr = startInputPtr;
			byte* ptr2 = startDestPtr;
			char* ptr3 = ptr + inputLength;
			byte* ptr4 = ptr2 + destLength;
			uint num = 255U;
			while (ptr < ptr3)
			{
				uint num2 = (uint)(*ptr);
				ptr++;
				if (num2 - 65U <= 25U)
				{
					num2 -= 65U;
				}
				else if (num2 - 97U <= 25U)
				{
					num2 -= 71U;
				}
				else
				{
					if (num2 - 48U > 9U)
					{
						if (num2 <= 32U)
						{
							if (num2 - 9U <= 1U || num2 == 13U || num2 == 32U)
							{
								continue;
							}
						}
						else
						{
							if (num2 == 43U)
							{
								num2 = 62U;
								goto IL_00A7;
							}
							if (num2 == 47U)
							{
								num2 = 63U;
								goto IL_00A7;
							}
							if (num2 == 61U)
							{
								if (ptr == ptr3)
								{
									num <<= 6;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Invalid length for a Base-64 char array or string."));
									}
									if ((int)((long)(ptr4 - ptr2)) < 2)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									*(ptr2++) = (byte)(num >> 8);
									num = 255U;
									break;
								}
								else
								{
									while (ptr < ptr3 - 1)
									{
										int num3 = (int)(*ptr);
										if (num3 != 32 && num3 != 10 && num3 != 13 && num3 != 9)
										{
											break;
										}
										ptr++;
									}
									if (ptr != ptr3 - 1 || *ptr != '=')
									{
										throw new FormatException(Environment.GetResourceString("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters."));
									}
									num <<= 12;
									if ((num & 2147483648U) == 0U)
									{
										throw new FormatException(Environment.GetResourceString("Invalid length for a Base-64 char array or string."));
									}
									if ((int)((long)(ptr4 - ptr2)) < 1)
									{
										return -1;
									}
									*(ptr2++) = (byte)(num >> 16);
									num = 255U;
									break;
								}
							}
						}
						throw new FormatException(Environment.GetResourceString("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters."));
					}
					num2 -= 4294967292U;
				}
				IL_00A7:
				num = (num << 6) | num2;
				if ((num & 2147483648U) != 0U)
				{
					if ((int)((long)(ptr4 - ptr2)) < 3)
					{
						return -1;
					}
					*ptr2 = (byte)(num >> 16);
					ptr2[1] = (byte)(num >> 8);
					ptr2[2] = (byte)num;
					ptr2 += 3;
					num = 255U;
				}
			}
			if (num != 255U)
			{
				throw new FormatException(Environment.GetResourceString("Invalid length for a Base-64 char array or string."));
			}
			return (int)((long)(ptr2 - startDestPtr));
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00037CAC File Offset: 0x00035EAC
		[SecurityCritical]
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException(Environment.GetResourceString("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters."));
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x04000886 RID: 2182
		internal static readonly RuntimeType[] ConvertTypes = new RuntimeType[]
		{
			(RuntimeType)typeof(Empty),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(DBNull),
			(RuntimeType)typeof(bool),
			(RuntimeType)typeof(char),
			(RuntimeType)typeof(sbyte),
			(RuntimeType)typeof(byte),
			(RuntimeType)typeof(short),
			(RuntimeType)typeof(ushort),
			(RuntimeType)typeof(int),
			(RuntimeType)typeof(uint),
			(RuntimeType)typeof(long),
			(RuntimeType)typeof(ulong),
			(RuntimeType)typeof(float),
			(RuntimeType)typeof(double),
			(RuntimeType)typeof(decimal),
			(RuntimeType)typeof(DateTime),
			(RuntimeType)typeof(object),
			(RuntimeType)typeof(string)
		};

		// Token: 0x04000887 RID: 2183
		private static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x04000888 RID: 2184
		internal static readonly char[] base64Table = new char[]
		{
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
			'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
			'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
			'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
			'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
			'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', '+', '/', '='
		};

		// Token: 0x04000889 RID: 2185
		private const int base64LineBreakPosition = 76;

		/// <summary>A constant that represents a database column that is absent of data; that is, database null.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400088A RID: 2186
		public static readonly object DBNull = global::System.DBNull.Value;
	}
}
