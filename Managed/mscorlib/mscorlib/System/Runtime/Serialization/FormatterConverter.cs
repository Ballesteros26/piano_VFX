using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Represents a base implementation of the <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> interface that uses the <see cref="T:System.Convert" /> class and the <see cref="T:System.IConvertible" /> interface.</summary>
	// Token: 0x020006CC RID: 1740
	[ComVisible(true)]
	public class FormatterConverter : IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is converted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049BB RID: 18875 RVA: 0x001082B1 File Offset: 0x001064B1
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <returns>The converted <paramref name="value" />, or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is converted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049BC RID: 18876 RVA: 0x001082CD File Offset: 0x001064CD
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049BD RID: 18877 RVA: 0x001082E9 File Offset: 0x001064E9
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049BE RID: 18878 RVA: 0x00108304 File Offset: 0x00106504
		public char ToChar(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049BF RID: 18879 RVA: 0x0010831F File Offset: 0x0010651F
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C0 RID: 18880 RVA: 0x0010833A File Offset: 0x0010653A
		public byte ToByte(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C1 RID: 18881 RVA: 0x00108355 File Offset: 0x00106555
		public short ToInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C2 RID: 18882 RVA: 0x00108370 File Offset: 0x00106570
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C3 RID: 18883 RVA: 0x0010838B File Offset: 0x0010658B
		public int ToInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C4 RID: 18884 RVA: 0x001083A6 File Offset: 0x001065A6
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C5 RID: 18885 RVA: 0x001083C1 File Offset: 0x001065C1
		public long ToInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C6 RID: 18886 RVA: 0x001083DC File Offset: 0x001065DC
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C7 RID: 18887 RVA: 0x001083F7 File Offset: 0x001065F7
		public float ToSingle(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C8 RID: 18888 RVA: 0x00108412 File Offset: 0x00106612
		public double ToDouble(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049C9 RID: 18889 RVA: 0x0010842D File Offset: 0x0010662D
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049CA RID: 18890 RVA: 0x00108448 File Offset: 0x00106648
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060049CB RID: 18891 RVA: 0x00108463 File Offset: 0x00106663
		public string ToString(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return global::System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}
	}
}
