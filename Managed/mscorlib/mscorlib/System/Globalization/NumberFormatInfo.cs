using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information for formatting and parsing numeric values. </summary>
	// Token: 0x02000421 RID: 1057
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x0600326E RID: 12910 RVA: 0x000B533F File Offset: 0x000B353F
		public NumberFormatInfo()
			: this(null)
		{
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000B5348 File Offset: 0x000B3548
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x00002194 File Offset: 0x00000394
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00002194 File Offset: 0x00000394
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000B53D3 File Offset: 0x000B35D3
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Decimal separator cannot be the empty string."));
			}
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000B5401 File Offset: 0x000B3601
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000B5418 File Offset: 0x000B3618
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("Array cannot be null."));
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(Environment.GetResourceString("The NativeDigits array must contain exactly ten members."), propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, Environment.GetResourceString("Found a null value within an array."));
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
				}
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000B54F6 File Offset: 0x000B36F6
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(Environment.GetResourceString("The DigitSubstitution property must be of a valid member of the DigitShapes enumeration. Valid entries include Context, NativeNational or None."), propertyName);
			}
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000B5510 File Offset: 0x000B3710
		[SecuritySafeCritical]
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000B5695 File Offset: 0x000B3895
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only  object that is culture-independent (invariant).</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000B56AF File Offset: 0x000B38AF
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		/// <param name="formatProvider">The <see cref="T:System.IFormatProvider" /> used to get the <see cref="T:System.Globalization.NumberFormatInfo" />.-or- null to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />. </param>
		// Token: 0x06003279 RID: 12921 RVA: 0x000B56DC File Offset: 0x000B38DC
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" /> object.</summary>
		/// <returns>A new object copied from the original <see cref="T:System.Globalization.NumberFormatInfo" /> object.</returns>
		// Token: 0x0600327A RID: 12922 RVA: 0x000B573F File Offset: 0x000B393F
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		/// <summary>Gets or sets the number of decimal places to use in currency values.</summary>
		/// <returns>The number of decimal places to use in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000B5753 File Offset: 0x000B3953
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000B575C File Offset: 0x000B395C
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.currencyDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in currency values.</summary>
		/// <returns>The string to use as the decimal separator in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000B57AB File Offset: 0x000B39AB
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000B57B3 File Offset: 0x000B39B3
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
				this.currencyDecimalSeparator = value;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000B57CD File Offset: 0x000B39CD
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000B57D8 File Offset: 0x000B39D8
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
					}
					i++;
				}
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in currency values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000B5830 File Offset: 0x000B3A30
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000B5844 File Offset: 0x000B3A44
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", array);
				this.currencyGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in numeric values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000B588D File Offset: 0x000B3A8D
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000B58A0 File Offset: 0x000B3AA0
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("NumberGroupSizes", array);
				this.numberGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in percent values. </summary>
		/// <returns>The number of digits in each group to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.-or- The property is being set and the array contains an entry, other than the last entry, that is set to 0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000B58E9 File Offset: 0x000B3AE9
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000B58FC File Offset: 0x000B3AFC
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("PercentGroupSizes", array);
				this.percentGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in currency values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000B5945 File Offset: 0x000B3B45
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x000B594D File Offset: 0x000B3B4D
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
				this.currencyGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the currency symbol.</summary>
		/// <returns>The string to use as the currency symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤". </returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000B5967 File Offset: 0x000B3B67
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000B596F File Offset: 0x000B3B6F
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.currencySymbol = value;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the culture of the current thread.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000B5998 File Offset: 0x000B3B98
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		/// <summary>Gets or sets the string that represents the IEEE NaN (not a number) value.</summary>
		/// <returns>The string that represents the IEEE NaN (not a number) value. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000B59DB File Offset: 0x000B3BDB
		// (set) Token: 0x0600328D RID: 12941 RVA: 0x000B59E3 File Offset: 0x000B3BE3
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative currency values.</summary>
		/// <returns>The format pattern for negative currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 15. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000B5A0A File Offset: 0x000B3C0A
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x000B5A14 File Offset: 0x000B3C14
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 15));
				}
				this.VerifyWritable();
				this.currencyNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative numeric values.</summary>
		/// <returns>The format pattern for negative numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 1, which represents "-n", where <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 4. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000B5A63 File Offset: 0x000B3C63
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x000B5A6C File Offset: 0x000B3C6C
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 4));
				}
				this.VerifyWritable();
				this.numberNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive percent values.</summary>
		/// <returns>The format pattern for positive percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000B5AB9 File Offset: 0x000B3CB9
		// (set) Token: 0x06003293 RID: 12947 RVA: 0x000B5AC4 File Offset: 0x000B3CC4
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.percentPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative percent values.</summary>
		/// <returns>The format pattern for negative percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 11. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000B5B11 File Offset: 0x000B3D11
		// (set) Token: 0x06003295 RID: 12949 RVA: 0x000B5B1C File Offset: 0x000B3D1C
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 11));
				}
				this.VerifyWritable();
				this.percentNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents negative infinity.</summary>
		/// <returns>The string that represents negative infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000B5B6B File Offset: 0x000B3D6B
		// (set) Token: 0x06003297 RID: 12951 RVA: 0x000B5B73 File Offset: 0x000B3D73
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is negative.</summary>
		/// <returns>The string that denotes that the associated number is negative. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000B5B9A File Offset: 0x000B3D9A
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x000B5BA2 File Offset: 0x000B3DA2
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in numeric values.</summary>
		/// <returns>The number of decimal places to use in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000B5BC9 File Offset: 0x000B3DC9
		// (set) Token: 0x0600329B RID: 12955 RVA: 0x000B5BD4 File Offset: 0x000B3DD4
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.numberDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in numeric values.</summary>
		/// <returns>The string to use as the decimal separator in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000B5C23 File Offset: 0x000B3E23
		// (set) Token: 0x0600329D RID: 12957 RVA: 0x000B5C2B File Offset: 0x000B3E2B
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
				this.numberDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in numeric values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000B5C45 File Offset: 0x000B3E45
		// (set) Token: 0x0600329F RID: 12959 RVA: 0x000B5C4D File Offset: 0x000B3E4D
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
				this.numberGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive currency values.</summary>
		/// <returns>The format pattern for positive currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000B5C67 File Offset: 0x000B3E67
		// (set) Token: 0x060032A1 RID: 12961 RVA: 0x000B5C70 File Offset: 0x000B3E70
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.currencyPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents positive infinity.</summary>
		/// <returns>The string that represents positive infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000B5CBD File Offset: 0x000B3EBD
		// (set) Token: 0x060032A3 RID: 12963 RVA: 0x000B5CC5 File Offset: 0x000B3EC5
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is positive.</summary>
		/// <returns>The string that denotes that the associated number is positive. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value to be assigned is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000B5CEC File Offset: 0x000B3EEC
		// (set) Token: 0x060032A5 RID: 12965 RVA: 0x000B5CF4 File Offset: 0x000B3EF4
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in percent values. </summary>
		/// <returns>The number of decimal places to use in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000B5D1B File Offset: 0x000B3F1B
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000B5D24 File Offset: 0x000B3F24
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.percentDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in percent values. </summary>
		/// <returns>The string to use as the decimal separator in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000B5D73 File Offset: 0x000B3F73
		// (set) Token: 0x060032A9 RID: 12969 RVA: 0x000B5D7B File Offset: 0x000B3F7B
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
				this.percentDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in percent values. </summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000B5D95 File Offset: 0x000B3F95
		// (set) Token: 0x060032AB RID: 12971 RVA: 0x000B5D9D File Offset: 0x000B3F9D
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
				this.percentGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the percent symbol.</summary>
		/// <returns>The string to use as the percent symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000B5DB7 File Offset: 0x000B3FB7
		// (set) Token: 0x060032AD RID: 12973 RVA: 0x000B5DBF File Offset: 0x000B3FBF
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.percentSymbol = value;
			}
		}

		/// <summary>Gets or sets the string to use as the per mille symbol.</summary>
		/// <returns>The string to use as the per mille symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only. </exception>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x000B5DE6 File Offset: 0x000B3FE6
		// (set) Token: 0x060032AF RID: 12975 RVA: 0x000B5DEE File Offset: 0x000B3FEE
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.perMilleSymbol = value;
			}
		}

		/// <summary>Gets or sets a string array of native digits equivalent to the Western digits 0 through 9.</summary>
		/// <returns>A string array that contains the native equivalent of the Western digits 0 through 9. The default is an array having the elements "0", "1", "2", "3", "4", "5", "6", "7", "8", and "9".</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value is null.-or-In a set operation, an element of the value array is null.</exception>
		/// <exception cref="T:System.ArgumentException">In a set operation, the value array does not contain 10 elements.-or-In a set operation, an element of the value array does not contain either a single <see cref="T:System.Char" /> object or a pair of <see cref="T:System.Char" /> objects that comprise a surrogate pair.-or-In a set operation, an element of the value array is not a number digit as defined by the Unicode Standard. That is, the digit in the array element does not have the Unicode Number, Decimal Digit (Nd) General Category value.-or-In a set operation, the numeric value of an element in the value array does not correspond to the element's position in the array. That is, the element at index 0, which is the first element of the array, does not have a numeric value of 0, or the element at index 1 does not have a numeric value of 1. </exception>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000B5E15 File Offset: 0x000B4015
		// (set) Token: 0x060032B1 RID: 12977 RVA: 0x000B5E27 File Offset: 0x000B4027
		[ComVisible(false)]
		public string[] NativeDigits
		{
			get
			{
				return (string[])this.nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
				this.nativeDigits = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the graphical user interface displays the shape of a digit.</summary>
		/// <returns>One of the enumeration values that specifies the culture-specific digit shape.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The value in a set operation is not a valid <see cref="T:System.Globalization.DigitShapes" /> value.</exception>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000B5E41 File Offset: 0x000B4041
		// (set) Token: 0x060032B3 RID: 12979 RVA: 0x000B5E49 File Offset: 0x000B4049
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
				this.digitSubstitution = (int)value;
			}
		}

		/// <summary>Gets an object of the specified type that provides a number formatting service.</summary>
		/// <returns>The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />; otherwise, null.</returns>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service. </param>
		// Token: 0x060032B4 RID: 12980 RVA: 0x000B5E63 File Offset: 0x000B4063
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.</returns>
		/// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nfi" /> is null. </exception>
		// Token: 0x060032B5 RID: 12981 RVA: 0x000B5E7A File Offset: 0x000B407A
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000B5EA8 File Offset: 0x000B40A8
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber."));
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000B5EF5 File Offset: 0x000B40F5
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The number style AllowHexSpecifier is not supported on floating point data types."));
			}
		}

		// Token: 0x04001AB6 RID: 6838
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04001AB7 RID: 6839
		internal int[] numberGroupSizes = new int[] { 3 };

		// Token: 0x04001AB8 RID: 6840
		internal int[] currencyGroupSizes = new int[] { 3 };

		// Token: 0x04001AB9 RID: 6841
		internal int[] percentGroupSizes = new int[] { 3 };

		// Token: 0x04001ABA RID: 6842
		internal string positiveSign = "+";

		// Token: 0x04001ABB RID: 6843
		internal string negativeSign = "-";

		// Token: 0x04001ABC RID: 6844
		internal string numberDecimalSeparator = ".";

		// Token: 0x04001ABD RID: 6845
		internal string numberGroupSeparator = ",";

		// Token: 0x04001ABE RID: 6846
		internal string currencyGroupSeparator = ",";

		// Token: 0x04001ABF RID: 6847
		internal string currencyDecimalSeparator = ".";

		// Token: 0x04001AC0 RID: 6848
		internal string currencySymbol = "¤";

		// Token: 0x04001AC1 RID: 6849
		internal string ansiCurrencySymbol;

		// Token: 0x04001AC2 RID: 6850
		internal string nanSymbol = "NaN";

		// Token: 0x04001AC3 RID: 6851
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x04001AC4 RID: 6852
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x04001AC5 RID: 6853
		internal string percentDecimalSeparator = ".";

		// Token: 0x04001AC6 RID: 6854
		internal string percentGroupSeparator = ",";

		// Token: 0x04001AC7 RID: 6855
		internal string percentSymbol = "%";

		// Token: 0x04001AC8 RID: 6856
		internal string perMilleSymbol = "‰";

		// Token: 0x04001AC9 RID: 6857
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		// Token: 0x04001ACA RID: 6858
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x04001ACB RID: 6859
		internal int numberDecimalDigits = 2;

		// Token: 0x04001ACC RID: 6860
		internal int currencyDecimalDigits = 2;

		// Token: 0x04001ACD RID: 6861
		internal int currencyPositivePattern;

		// Token: 0x04001ACE RID: 6862
		internal int currencyNegativePattern;

		// Token: 0x04001ACF RID: 6863
		internal int numberNegativePattern = 1;

		// Token: 0x04001AD0 RID: 6864
		internal int percentPositivePattern;

		// Token: 0x04001AD1 RID: 6865
		internal int percentNegativePattern;

		// Token: 0x04001AD2 RID: 6866
		internal int percentDecimalDigits = 2;

		// Token: 0x04001AD3 RID: 6867
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x04001AD4 RID: 6868
		internal bool isReadOnly;

		// Token: 0x04001AD5 RID: 6869
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001AD6 RID: 6870
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x04001AD7 RID: 6871
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x04001AD8 RID: 6872
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x04001AD9 RID: 6873
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
