using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a decimal number.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	[Serializable]
	public struct Decimal : IFormattable, IComparable, IConvertible, IDeserializationCallback, IComparable<decimal>, IEquatable<decimal>
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003A544 File Offset: 0x00038744
		public Decimal(int value)
		{
			int num = value;
			if (num >= 0)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				num = -num;
			}
			this.lo = num;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003A583 File Offset: 0x00038783
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x06000DDA RID: 3546 RVA: 0x0003A5A4 File Offset: 0x000387A4
		public Decimal(long value)
		{
			long num = value;
			if (num >= 0L)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				num = -num;
			}
			this.lo = (int)num;
			this.mid = (int)(num >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x0003A5E9 File Offset: 0x000387E9
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified single-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
		// Token: 0x06000DDC RID: 3548
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Decimal(float value);

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified double-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
		// Token: 0x06000DDD RID: 3549
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Decimal(double value);

		/// <summary>Converts the specified <see cref="T:System.Decimal" /> value to the equivalent OLE Automation Currency value, which is contained in a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DDE RID: 3550 RVA: 0x0003A60C File Offset: 0x0003880C
		public static long ToOACurrency(decimal value)
		{
			return (long)(value * 10000m);
		}

		/// <summary>Converts the specified 64-bit signed integer, which contains an OLE Automation Currency value, to the equivalent <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the equivalent of <paramref name="cy" />.</returns>
		/// <param name="cy">An OLE Automation Currency value. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DDF RID: 3551 RVA: 0x0003A623 File Offset: 0x00038823
		public static decimal FromOACurrency(long cy)
		{
			return cy / 10000m;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to a decimal value represented in binary and contained in a specified array.</summary>
		/// <param name="bits">An array of 32-bit signed integers containing a representation of a decimal value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="bits" /> is not 4.-or- The representation of the decimal value in <paramref name="bits" /> is not valid. </exception>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003A63C File Offset: 0x0003883C
		public Decimal(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if ((num & 2130771967) == 0 && (num & 16711680) <= 1835008)
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Decimal byte array constructor requires an array of length four containing valid decimal bytes."));
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003A6AC File Offset: 0x000388AC
		private void SetBits(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if ((num & 2130771967) == 0 && (num & 16711680) <= 1835008)
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Decimal byte array constructor requires an array of length four containing valid decimal bytes."));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> from parameters specifying the instance's constituent parts.</summary>
		/// <param name="lo">The low 32 bits of a 96-bit integer. </param>
		/// <param name="mid">The middle 32 bits of a 96-bit integer. </param>
		/// <param name="hi">The high 32 bits of a 96-bit integer. </param>
		/// <param name="isNegative">true to indicate a negative number; false to indicate a positive number. </param>
		/// <param name="scale">A power of 10 ranging from 0 to 28. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> is greater than 28. </exception>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003A71C File Offset: 0x0003891C
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", Environment.GetResourceString("Decimal's scale value must be between 0 and 28, inclusive."));
			}
			this.lo = lo;
			this.mid = mid;
			this.hi = hi;
			this.flags = (int)scale << 16;
			if (isNegative)
			{
				this.flags |= int.MinValue;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003A77C File Offset: 0x0003897C
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			try
			{
				this.SetBits(decimal.GetBits(this));
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Value was either too large or too small for a Decimal."), ex);
			}
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Decimal" /> object contains invalid or corrupted data.</exception>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003A7C0 File Offset: 0x000389C0
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				this.SetBits(decimal.GetBits(this));
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Value was either too large or too small for a Decimal."), ex);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003A804 File Offset: 0x00038A04
		private Decimal(int lo, int mid, int hi, int flags)
		{
			if ((flags & 2130771967) == 0 && (flags & 16711680) <= 1835008)
			{
				this.lo = lo;
				this.mid = mid;
				this.hi = hi;
				this.flags = flags;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Decimal byte array constructor requires an array of length four containing valid decimal bytes."));
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003A857 File Offset: 0x00038A57
		internal static decimal Abs(decimal d)
		{
			return new decimal(d.lo, d.mid, d.hi, d.flags & int.MaxValue);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The sum of <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to add. </param>
		/// <param name="d2">The second value to add. </param>
		/// <exception cref="T:System.OverflowException">The sum of <paramref name="d1" /> and <paramref name="d2" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003A87C File Offset: 0x00038A7C
		[SecuritySafeCritical]
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 0);
			return d1;
		}

		// Token: 0x06000DE8 RID: 3560
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallAddSub(ref decimal d1, ref decimal d2, byte bSign);

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified decimal number.</summary>
		/// <returns>The smallest integral value that is greater than or equal to the <paramref name="d" /> parameter. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		/// <param name="d">A decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003A889 File Offset: 0x00038A89
		public static decimal Ceiling(decimal d)
		{
			return -decimal.Floor(-d);
		}

		/// <summary>Compares two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>A signed number indicating the relative values of <paramref name="d1" /> and <paramref name="d2" />.Return value Meaning Less than zero <paramref name="d1" /> is less than <paramref name="d2" />. Zero <paramref name="d1" /> and <paramref name="d2" /> are equal. Greater than zero <paramref name="d1" /> is greater than <paramref name="d2" />. </returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0003A89B File Offset: 0x00038A9B
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2);
		}

		// Token: 0x06000DEB RID: 3563
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FCallCompare(ref decimal d1, ref decimal d2);

		/// <summary>Compares this instance to a specified object and returns a comparison of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null. </returns>
		/// <param name="value">The object to compare with this instance, or null. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Decimal" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003A8A8 File Offset: 0x00038AA8
		[SecuritySafeCritical]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Object must be of type Decimal."));
			}
			decimal num = (decimal)value;
			return decimal.FCallCompare(ref this, ref num);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Decimal" /> object and returns a comparison of their relative values.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.Return value Meaning Less than zero This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. Greater than zero This instance is greater than <paramref name="value" />. </returns>
		/// <param name="value">The object to compare with this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DED RID: 3565 RVA: 0x0003A8E1 File Offset: 0x00038AE1
		[SecuritySafeCritical]
		public int CompareTo(decimal value)
		{
			return decimal.FCallCompare(ref this, ref value);
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value (that is, the quotient) is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEE RID: 3566 RVA: 0x0003A8EB File Offset: 0x00038AEB
		[SecuritySafeCritical]
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.FCallDivide(ref d1, ref d2);
			return d1;
		}

		// Token: 0x06000DEF RID: 3567
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallDivide(ref decimal d1, ref decimal d2);

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Decimal" /> and equal to this instance; otherwise, false.</returns>
		/// <param name="value">The object to compare with this instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003A8F8 File Offset: 0x00038AF8
		[SecuritySafeCritical]
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.FCallCompare(ref this, ref num) == 0;
			}
			return false;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Decimal" /> object represent the same value.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">An object to compare to this instance.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003A921 File Offset: 0x00038B21
		[SecuritySafeCritical]
		public bool Equals(decimal value)
		{
			return decimal.FCallCompare(ref this, ref value) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000DF2 RID: 3570
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern int GetHashCode();

		/// <summary>Returns a value indicating whether two specified instances of <see cref="T:System.Decimal" /> represent the same value.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003A92E File Offset: 0x00038B2E
		[SecuritySafeCritical]
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) == 0;
		}

		/// <summary>Rounds a specified <see cref="T:System.Decimal" /> number to the closest integer toward negative infinity.</summary>
		/// <returns>If <paramref name="d" /> has a fractional part, the next whole <see cref="T:System.Decimal" /> number toward negative infinity that is less than <paramref name="d" />.-or- If <paramref name="d" /> doesn't have a fractional part, <paramref name="d" /> is returned unchanged. Note that the method returns an integral value of type <see cref="T:System.Decimal" />. </returns>
		/// <param name="d">The value to round. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003A93C File Offset: 0x00038B3C
		[SecuritySafeCritical]
		public static decimal Floor(decimal d)
		{
			decimal.FCallFloor(ref d);
			return d;
		}

		// Token: 0x06000DF5 RID: 3573
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallFloor(ref decimal d);

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>A string that represents the value of this instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003A946 File Offset: 0x00038B46
		[SecuritySafeCritical]
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <param name="format">A standard or custom numeric format string (see Remarks).</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003A959 File Offset: 0x00038B59
		[SecuritySafeCritical]
		public string ToString(string format)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003A96C File Offset: 0x00038B6C
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <param name="format">A numeric format string (see Remarks).</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003A980 File Offset: 0x00038B80
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <returns>The equivalent to the number contained in <paramref name="s" />.</returns>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003A994 File Offset: 0x00038B94
		public static decimal Parse(string s)
		{
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" />.</returns>
		/// <param name="s">The string representation of the number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003A9A3 File Offset: 0x00038BA3
		public static decimal Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified culture-specific format information.</summary>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <param name="s">The string representation of the number to convert. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific parsing information about <paramref name="s" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003A9B7 File Offset: 0x00038BB7
		public static decimal Parse(string s, IFormatProvider provider)
		{
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format.</summary>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" /> and <paramref name="provider" />.</returns>
		/// <param name="s">The string representation of the number to convert. </param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies culture-specific information about the format of <paramref name="s" />. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFD RID: 3581 RVA: 0x0003A9C7 File Offset: 0x00038BC7
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or is zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not a number in a valid format, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFE RID: 3582 RVA: 0x0003A9DC File Offset: 0x00038BDC
		public static bool TryParse(string s, out decimal result)
		{
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <returns>true if <paramref name="s" /> was converted successfully; otherwise, false.</returns>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />. </param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or is zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is null, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uninitialized. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value. -or-<paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFF RID: 3583 RVA: 0x0003A9EC File Offset: 0x00038BEC
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Converts the value of a specified instance of <see cref="T:System.Decimal" /> to its equivalent binary representation.</summary>
		/// <returns>A 32-bit signed integer array with four elements that contain the binary representation of <paramref name="d" />.</returns>
		/// <param name="d">The value to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0003AA02 File Offset: 0x00038C02
		public static int[] GetBits(decimal d)
		{
			return new int[] { d.lo, d.mid, d.hi, d.flags };
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003AA30 File Offset: 0x00038C30
		internal static void GetBytes(decimal d, byte[] buffer)
		{
			buffer[0] = (byte)d.lo;
			buffer[1] = (byte)(d.lo >> 8);
			buffer[2] = (byte)(d.lo >> 16);
			buffer[3] = (byte)(d.lo >> 24);
			buffer[4] = (byte)d.mid;
			buffer[5] = (byte)(d.mid >> 8);
			buffer[6] = (byte)(d.mid >> 16);
			buffer[7] = (byte)(d.mid >> 24);
			buffer[8] = (byte)d.hi;
			buffer[9] = (byte)(d.hi >> 8);
			buffer[10] = (byte)(d.hi >> 16);
			buffer[11] = (byte)(d.hi >> 24);
			buffer[12] = (byte)d.flags;
			buffer[13] = (byte)(d.flags >> 8);
			buffer[14] = (byte)(d.flags >> 16);
			buffer[15] = (byte)(d.flags >> 24);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003AB04 File Offset: 0x00038D04
		internal static decimal ToDecimal(byte[] buffer)
		{
			int num = (int)buffer[0] | ((int)buffer[1] << 8) | ((int)buffer[2] << 16) | ((int)buffer[3] << 24);
			int num2 = (int)buffer[4] | ((int)buffer[5] << 8) | ((int)buffer[6] << 16) | ((int)buffer[7] << 24);
			int num3 = (int)buffer[8] | ((int)buffer[9] << 8) | ((int)buffer[10] << 16) | ((int)buffer[11] << 24);
			int num4 = (int)buffer[12] | ((int)buffer[13] << 8) | ((int)buffer[14] << 16) | ((int)buffer[15] << 24);
			return new decimal(num, num2, num3, num4);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003AB80 File Offset: 0x00038D80
		private static void InternalAddUInt32RawUnchecked(ref decimal value, uint i)
		{
			uint num = (uint)value.lo;
			uint num2 = num + i;
			value.lo = (int)num2;
			if (num2 < num || num2 < i)
			{
				num = (uint)value.mid;
				num2 = num + 1U;
				value.mid = (int)num2;
				if (num2 < num || num2 < 1U)
				{
					value.hi++;
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003ABD0 File Offset: 0x00038DD0
		private static uint InternalDivRemUInt32(ref decimal value, uint divisor)
		{
			uint num = 0U;
			if (value.hi != 0)
			{
				ulong num2 = (ulong)value.hi;
				value.hi = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			if (value.mid != 0 || num != 0U)
			{
				ulong num2 = ((ulong)num << 32) | (ulong)value.mid;
				value.mid = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			if (value.lo != 0 || num != 0U)
			{
				ulong num2 = ((ulong)num << 32) | (ulong)value.lo;
				value.lo = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			return num;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003AC58 File Offset: 0x00038E58
		private static void InternalRoundFromZero(ref decimal d, int decimalCount)
		{
			int num = ((d.flags & 16711680) >> 16) - decimalCount;
			if (num <= 0)
			{
				return;
			}
			uint num3;
			uint num4;
			do
			{
				int num2 = ((num > 9) ? 9 : num);
				num3 = decimal.Powers10[num2];
				num4 = decimal.InternalDivRemUInt32(ref d, num3);
				num -= num2;
			}
			while (num > 0);
			if (num4 >= num3 >> 1)
			{
				decimal.InternalAddUInt32RawUnchecked(ref d, 1U);
			}
			d.flags = ((decimalCount << 16) & 16711680) | (d.flags & int.MinValue);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003ACC9 File Offset: 0x00038EC9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		internal static decimal Max(decimal d1, decimal d2)
		{
			if (decimal.FCallCompare(ref d1, ref d2) < 0)
			{
				return d2;
			}
			return d1;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003ACDA File Offset: 0x00038EDA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		internal static decimal Min(decimal d1, decimal d2)
		{
			if (decimal.FCallCompare(ref d1, ref d2) >= 0)
			{
				return d2;
			}
			return d1;
		}

		/// <summary>Computes the remainder after dividing two <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The remainder after dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E08 RID: 3592 RVA: 0x0003ACEC File Offset: 0x00038EEC
		public static decimal Remainder(decimal d1, decimal d2)
		{
			d2.flags = (d2.flags & int.MaxValue) | (d1.flags & int.MinValue);
			if (decimal.Abs(d1) < decimal.Abs(d2))
			{
				return d1;
			}
			d1 -= d2;
			if (d1 == 0m)
			{
				d1.flags = (d1.flags & int.MaxValue) | (d2.flags & int.MinValue);
			}
			decimal num = decimal.Truncate(d1 / d2) * d2;
			decimal num2 = d1 - num;
			if ((d1.flags & -2147483648) != (num2.flags & -2147483648))
			{
				if (-0.000000000000000000000000001m <= num2 && num2 <= 0.000000000000000000000000001m)
				{
					num2.flags = (num2.flags & int.MaxValue) | (d1.flags & int.MinValue);
				}
				else
				{
					num2 += d2;
				}
			}
			return num2;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of multiplying <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The multiplicand. </param>
		/// <param name="d2">The multiplier. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E09 RID: 3593 RVA: 0x0003ADEA File Offset: 0x00038FEA
		[SecuritySafeCritical]
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.FCallMultiply(ref d1, ref d2);
			return d1;
		}

		// Token: 0x06000E0A RID: 3594
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallMultiply(ref decimal d1, ref decimal d2);

		/// <summary>Returns the result of multiplying the specified <see cref="T:System.Decimal" /> value by negative one.</summary>
		/// <returns>A decimal number with the value of <paramref name="d" />, but the opposite sign.-or- Zero, if <paramref name="d" /> is zero.</returns>
		/// <param name="d">The value to negate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0B RID: 3595 RVA: 0x0003ADF6 File Offset: 0x00038FF6
		public static decimal Negate(decimal d)
		{
			return new decimal(d.lo, d.mid, d.hi, d.flags ^ int.MinValue);
		}

		/// <summary>Rounds a decimal value to the nearest integer.</summary>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned.</returns>
		/// <param name="d">A decimal number to round. </param>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0003AE1B File Offset: 0x0003901B
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		/// <summary>Rounds a <see cref="T:System.Decimal" /> value to a specified number of decimal places.</summary>
		/// <returns>The decimal number equivalent to <paramref name="d" /> rounded to <paramref name="decimals" /> number of decimal places.</returns>
		/// <param name="d">A decimal number to round. </param>
		/// <param name="decimals">A value from 0 to 28 that specifies the number of decimal places to round to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is not a value from 0 to 28. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0003AE24 File Offset: 0x00039024
		[SecuritySafeCritical]
		public static decimal Round(decimal d, int decimals)
		{
			decimal.FCallRound(ref d, decimals);
			return d;
		}

		/// <summary>Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned. </returns>
		/// <param name="d">A decimal number to round. </param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0E RID: 3598 RVA: 0x0003AE2F File Offset: 0x0003902F
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(d, 0, mode);
		}

		/// <summary>Rounds a decimal value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The number that is nearest to the <paramref name="d" /> parameter with a precision equal to the <paramref name="decimals" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned. If the precision of <paramref name="d" /> is less than <paramref name="decimals" />, <paramref name="d" /> is returned unchanged.</returns>
		/// <param name="d">A decimal number to round. </param>
		/// <param name="decimals">The number of significant decimal places (precision) in the return value. </param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003AE3C File Offset: 0x0003903C
		[SecuritySafeCritical]
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals < 0 || decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", Environment.GetResourceString("Decimal can only round to between 0 and 28 digits of precision."));
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(Environment.GetResourceString("The value '{0}' is not valid for this usage of the type {1}.", new object[] { mode, "MidpointRounding" }), "mode");
			}
			if (mode == MidpointRounding.ToEven)
			{
				decimal.FCallRound(ref d, decimals);
			}
			else
			{
				decimal.InternalRoundFromZero(ref d, decimals);
			}
			return d;
		}

		// Token: 0x06000E10 RID: 3600
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallRound(ref decimal d, int decimals);

		/// <summary>Subtracts one specified <see cref="T:System.Decimal" /> value from another.</summary>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <param name="d1">The minuend. </param>
		/// <param name="d2">The subtrahend. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003AEB1 File Offset: 0x000390B1
		[SecuritySafeCritical]
		public static decimal Subtract(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 128);
			return d1;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E12 RID: 3602 RVA: 0x0003AEC4 File Offset: 0x000390C4
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."), ex);
			}
			if (num < 0U || num > 255U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an unsigned byte."));
			}
			return (byte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0003AF1C File Offset: 0x0003911C
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."), ex);
			}
			if (num < -128 || num > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a signed byte."));
			}
			return (sbyte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E14 RID: 3604 RVA: 0x0003AF70 File Offset: 0x00039170
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."), ex);
			}
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int16."));
			}
			return (short)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E15 RID: 3605
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double ToDouble(decimal d);

		// Token: 0x06000E16 RID: 3606
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int FCallToInt32(decimal d);

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E17 RID: 3607 RVA: 0x0003AFCC File Offset: 0x000391CC
		[SecuritySafeCritical]
		public static int ToInt32(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0 && d.mid == 0)
			{
				int num = d.lo;
				if (d.flags >= 0)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int32."));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0003B02C File Offset: 0x0003922C
		[SecuritySafeCritical]
		public static long ToInt64(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0)
			{
				long num = ((long)d.lo & (long)((ulong)(-1))) | ((long)d.mid << 32);
				if (d.flags >= 0)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for an Int64."));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer equivalent to the value of <paramref name="value" />.</returns>
		/// <param name="value">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E19 RID: 3609 RVA: 0x0003B098 File Offset: 0x00039298
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."), ex);
			}
			if (num < 0U || num > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt16."));
			}
			return (ushort)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E1A RID: 3610 RVA: 0x0003B0F0 File Offset: 0x000392F0
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static uint ToUInt32(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0 && d.mid == 0)
			{
				uint num = (uint)d.lo;
				if (d.flags >= 0 || num == 0U)
				{
					return num;
				}
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt32."));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E1B RID: 3611 RVA: 0x0003B148 File Offset: 0x00039348
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static ulong ToUInt64(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0)
			{
				ulong num = (ulong)d.lo | ((ulong)d.mid << 32);
				if (d.flags >= 0 || num == 0UL)
				{
					return num;
				}
			}
			throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a UInt64."));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number equivalent to the value of <paramref name="d" />.</returns>
		/// <param name="d">The decimal number to convert. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E1C RID: 3612
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float ToSingle(decimal d);

		/// <summary>Returns the integral digits of the specified <see cref="T:System.Decimal" />; any fractional digits are discarded.</summary>
		/// <returns>The result of <paramref name="d" /> rounded toward zero, to the nearest whole number.</returns>
		/// <param name="d">The decimal number to truncate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E1D RID: 3613 RVA: 0x0003B1A2 File Offset: 0x000393A2
		[SecuritySafeCritical]
		public static decimal Truncate(decimal d)
		{
			decimal.FCallTruncate(ref d);
			return d;
		}

		// Token: 0x06000E1E RID: 3614
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallTruncate(ref decimal d);

		/// <summary>Defines an explicit conversion of an 8-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 8-bit unsigned integer.</returns>
		/// <param name="value">The 8-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E1F RID: 3615 RVA: 0x0003B1AC File Offset: 0x000393AC
		public static implicit operator decimal(byte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of an 8-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 8-bit signed integer.</returns>
		/// <param name="value">The 8-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E20 RID: 3616 RVA: 0x0003B1AC File Offset: 0x000393AC
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a 16-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 16-bit signed integer.</returns>
		/// <param name="value">The16-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E21 RID: 3617 RVA: 0x0003B1AC File Offset: 0x000393AC
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 16-bit unsigned integer.</returns>
		/// <param name="value">The 16-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E22 RID: 3618 RVA: 0x0003B1AC File Offset: 0x000393AC
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a Unicode character to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted Unicode character.</returns>
		/// <param name="value">The Unicode character to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E23 RID: 3619 RVA: 0x0003B1AC File Offset: 0x000393AC
		public static implicit operator decimal(char value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an explicit conversion of a 32-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 32-bit signed integer.</returns>
		/// <param name="value">The 32-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0003B1AC File Offset: 0x000393AC
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 32-bit unsigned integer.</returns>
		/// <param name="value">The 32-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E25 RID: 3621 RVA: 0x0003B1B4 File Offset: 0x000393B4
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 64-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 64-bit signed integer.</returns>
		/// <param name="value">The 64-bit signed integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0003B1BC File Offset: 0x000393BC
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted 64-bit unsigned integer.</returns>
		/// <param name="value">The 64-bit unsigned integer to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E27 RID: 3623 RVA: 0x0003B1C4 File Offset: 0x000393C4
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a single-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted single-precision floating point number.</returns>
		/// <param name="value">The single-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0003B1CC File Offset: 0x000393CC
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a double-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The converted double-precision floating point number.</returns>
		/// <param name="value">The double-precision floating-point number to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.-or- <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E29 RID: 3625 RVA: 0x0003B1D4 File Offset: 0x000393D4
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit unsigned integer.</summary>
		/// <returns>An 8-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2A RID: 3626 RVA: 0x0003B1DC File Offset: 0x000393DC
		public static explicit operator byte(decimal value)
		{
			return decimal.ToByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2B RID: 3627 RVA: 0x0003B1E4 File Offset: 0x000393E4
		[CLSCompliant(false)]
		public static explicit operator sbyte(decimal value)
		{
			return decimal.ToSByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a Unicode character.</summary>
		/// <returns>A Unicode character that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2C RID: 3628 RVA: 0x0003B1EC File Offset: 0x000393EC
		public static explicit operator char(decimal value)
		{
			ushort num;
			try
			{
				num = decimal.ToUInt16(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Value was either too large or too small for a character."), ex);
			}
			return (char)num;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003B228 File Offset: 0x00039428
		public static explicit operator short(decimal value)
		{
			return decimal.ToInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit unsigned integer.</summary>
		/// <returns>A 16-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2E RID: 3630 RVA: 0x0003B230 File Offset: 0x00039430
		[CLSCompliant(false)]
		public static explicit operator ushort(decimal value)
		{
			return decimal.ToUInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E2F RID: 3631 RVA: 0x0003B238 File Offset: 0x00039438
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit unsigned integer.</summary>
		/// <returns>A 32-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E30 RID: 3632 RVA: 0x0003B240 File Offset: 0x00039440
		[CLSCompliant(false)]
		public static explicit operator uint(decimal value)
		{
			return decimal.ToUInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E31 RID: 3633 RVA: 0x0003B248 File Offset: 0x00039448
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit unsigned integer.</summary>
		/// <returns>A 64-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E32 RID: 3634 RVA: 0x0003B250 File Offset: 0x00039450
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E33 RID: 3635 RVA: 0x0003B258 File Offset: 0x00039458
		public static explicit operator float(decimal value)
		{
			return decimal.ToSingle(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to convert. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E34 RID: 3636 RVA: 0x0003B260 File Offset: 0x00039460
		public static explicit operator double(decimal value)
		{
			return decimal.ToDouble(value);
		}

		/// <summary>Returns the value of the <see cref="T:System.Decimal" /> operand (the sign of the operand is unchanged).</summary>
		/// <returns>The value of the operand, <paramref name="d" />.</returns>
		/// <param name="d">The operand to return.</param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E35 RID: 3637 RVA: 0x00002119 File Offset: 0x00000319
		public static decimal operator +(decimal d)
		{
			return d;
		}

		/// <summary>Negates the value of the specified <see cref="T:System.Decimal" /> operand.</summary>
		/// <returns>The result of <paramref name="d" /> multiplied by negative one (-1).</returns>
		/// <param name="d">The value to negate. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E36 RID: 3638 RVA: 0x0003B268 File Offset: 0x00039468
		public static decimal operator -(decimal d)
		{
			return decimal.Negate(d);
		}

		/// <summary>Increments the <see cref="T:System.Decimal" /> operand by 1.</summary>
		/// <returns>The value of <paramref name="d" /> incremented by 1.</returns>
		/// <param name="d">The value to increment. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E37 RID: 3639 RVA: 0x0003B270 File Offset: 0x00039470
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		/// <summary>Decrements the <see cref="T:System.Decimal" /> operand by one.</summary>
		/// <returns>The value of <paramref name="d" /> decremented by 1.</returns>
		/// <param name="d">The value to decrement. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E38 RID: 3640 RVA: 0x0003B27D File Offset: 0x0003947D
		public static decimal operator --(decimal d)
		{
			return decimal.Subtract(d, 1m);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of adding <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to add. </param>
		/// <param name="d2">The second value to add. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E39 RID: 3641 RVA: 0x0003A87C File Offset: 0x00038A7C
		[SecuritySafeCritical]
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 0);
			return d1;
		}

		/// <summary>Subtracts two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <param name="d1">The minuend. </param>
		/// <param name="d2">The subtrahend. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3A RID: 3642 RVA: 0x0003AEB1 File Offset: 0x000390B1
		[SecuritySafeCritical]
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 128);
			return d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of multiplying <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The first value to multiply. </param>
		/// <param name="d2">The second value to multiply. </param>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003ADEA File Offset: 0x00038FEA
		[SecuritySafeCritical]
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.FCallMultiply(ref d1, ref d2);
			return d1;
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3C RID: 3644 RVA: 0x0003A8EB File Offset: 0x00038AEB
		[SecuritySafeCritical]
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.FCallDivide(ref d1, ref d2);
			return d1;
		}

		/// <summary>Returns the remainder resulting from dividing two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The remainder resulting from dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <param name="d1">The dividend. </param>
		/// <param name="d2">The divisor. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero. </exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3D RID: 3645 RVA: 0x0003B28A File Offset: 0x0003948A
		public static decimal operator %(decimal d1, decimal d2)
		{
			return decimal.Remainder(d1, d2);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> values are equal.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3E RID: 3646 RVA: 0x0003A92E File Offset: 0x00038B2E
		[SecuritySafeCritical]
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) == 0;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> objects have different values.</summary>
		/// <returns>true if <paramref name="d1" /> and <paramref name="d2" /> are not equal; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E3F RID: 3647 RVA: 0x0003B293 File Offset: 0x00039493
		[SecuritySafeCritical]
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) != 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is less than <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003B2A1 File Offset: 0x000394A1
		[SecuritySafeCritical]
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) < 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is less than or equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E41 RID: 3649 RVA: 0x0003B2AF File Offset: 0x000394AF
		[SecuritySafeCritical]
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) <= 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is greater than <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0003B2C0 File Offset: 0x000394C0
		[SecuritySafeCritical]
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) > 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <returns>true if <paramref name="d1" /> is greater than or equal to <paramref name="d2" />; otherwise, false.</returns>
		/// <param name="d1">The first value to compare. </param>
		/// <param name="d2">The second value to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000E43 RID: 3651 RVA: 0x0003B2CE File Offset: 0x000394CE
		[SecuritySafeCritical]
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) >= 0;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Decimal" />.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Decimal" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E44 RID: 3652 RVA: 0x0003B2DF File Offset: 0x000394DF
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <returns>true if the value of the current instance is not zero; otherwise, false.</returns>
		/// <param name="provider">This parameter is ignored. </param>
		// Token: 0x06000E45 RID: 3653 RVA: 0x0003B2E3 File Offset: 0x000394E3
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. This conversion is not supported. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases. </exception>
		// Token: 0x06000E46 RID: 3654 RVA: 0x0003B2F0 File Offset: 0x000394F0
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Decimal", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.SByte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		// Token: 0x06000E47 RID: 3655 RVA: 0x0003B317 File Offset: 0x00039517
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		// Token: 0x06000E48 RID: 3656 RVA: 0x0003B324 File Offset: 0x00039524
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000E49 RID: 3657 RVA: 0x0003B331 File Offset: 0x00039531
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06000E4A RID: 3658 RVA: 0x0003B33E File Offset: 0x0003953E
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int32" />.</returns>
		/// <param name="provider">The parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000E4B RID: 3659 RVA: 0x0003B34B File Offset: 0x0003954B
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06000E4C RID: 3660 RVA: 0x0003B358 File Offset: 0x00039558
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		// Token: 0x06000E4D RID: 3661 RVA: 0x0003B365 File Offset: 0x00039565
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06000E4E RID: 3662 RVA: 0x0003B372 File Offset: 0x00039572
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E4F RID: 3663 RVA: 0x0003B37F File Offset: 0x0003957F
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E50 RID: 3664 RVA: 0x0003B38C File Offset: 0x0003958C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, unchanged.</returns>
		/// <param name="provider">This parameter is ignored.</param>
		// Token: 0x06000E51 RID: 3665 RVA: 0x0003B399 File Offset: 0x00039599
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <returns>None. This conversion is not supported. </returns>
		/// <param name="provider">This parameter is ignored.</param>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0003B3A1 File Offset: 0x000395A1
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[] { "Decimal", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <returns>The value of the current instance, converted to a <paramref name="type" />.</returns>
		/// <param name="type">The type to which to convert the value of this <see cref="T:System.Decimal" /> instance. </param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported. </exception>
		// Token: 0x06000E53 RID: 3667 RVA: 0x0003B3C8 File Offset: 0x000395C8
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040008CE RID: 2254
		private const int SignMask = -2147483648;

		// Token: 0x040008CF RID: 2255
		private const byte DECIMAL_NEG = 128;

		// Token: 0x040008D0 RID: 2256
		private const byte DECIMAL_ADD = 0;

		// Token: 0x040008D1 RID: 2257
		private const int ScaleMask = 16711680;

		// Token: 0x040008D2 RID: 2258
		private const int ScaleShift = 16;

		// Token: 0x040008D3 RID: 2259
		private const int MaxInt32Scale = 9;

		// Token: 0x040008D4 RID: 2260
		private static uint[] Powers10 = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		/// <summary>Represents the number zero (0).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008D5 RID: 2261
		public const decimal Zero = 0m;

		/// <summary>Represents the number one (1).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008D6 RID: 2262
		public const decimal One = 1m;

		/// <summary>Represents the number negative one (-1).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008D7 RID: 2263
		public const decimal MinusOne = -1m;

		/// <summary>Represents the largest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008D8 RID: 2264
		public const decimal MaxValue = 79228162514264337593543950335m;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008D9 RID: 2265
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040008DA RID: 2266
		private const decimal NearNegativeZero = -0.000000000000000000000000001m;

		// Token: 0x040008DB RID: 2267
		private const decimal NearPositiveZero = 0.000000000000000000000000001m;

		// Token: 0x040008DC RID: 2268
		private int flags;

		// Token: 0x040008DD RID: 2269
		private int hi;

		// Token: 0x040008DE RID: 2270
		private int lo;

		// Token: 0x040008DF RID: 2271
		private int mid;
	}
}
