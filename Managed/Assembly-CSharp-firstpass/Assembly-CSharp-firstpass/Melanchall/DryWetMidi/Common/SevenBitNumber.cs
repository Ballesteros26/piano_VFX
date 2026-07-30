using System;
using System.Linq;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C2 RID: 450
	public struct SevenBitNumber : IComparable<SevenBitNumber>, IConvertible
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x000247B1 File Offset: 0x000229B1
		public SevenBitNumber(byte value)
		{
			ThrowIfArgument.IsOutOfRange("value", (int)value, 0, 127, "Value is out of range valid for seven-bit number.");
			this._value = value;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000247D0 File Offset: 0x000229D0
		public static bool TryParse(string input, out SevenBitNumber sevenBitNumber)
		{
			sevenBitNumber = default(SevenBitNumber);
			byte b;
			bool flag = ShortByteParser.TryParse(input, 0, 127, out b).Status == ParsingStatus.Parsed;
			if (flag)
			{
				sevenBitNumber = (SevenBitNumber)b;
			}
			return flag;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00024808 File Offset: 0x00022A08
		public static SevenBitNumber Parse(string input)
		{
			byte b;
			ParsingResult parsingResult = ShortByteParser.TryParse(input, 0, 127, out b);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return (SevenBitNumber)b;
			}
			throw parsingResult.Exception;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00024836 File Offset: 0x00022A36
		public static implicit operator byte(SevenBitNumber number)
		{
			return number._value;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002483E File Offset: 0x00022A3E
		public static explicit operator SevenBitNumber(byte number)
		{
			return new SevenBitNumber(number);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00024848 File Offset: 0x00022A48
		public int CompareTo(SevenBitNumber other)
		{
			return this._value.CompareTo(other._value);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002486C File Offset: 0x00022A6C
		public TypeCode GetTypeCode()
		{
			return this._value.GetTypeCode();
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00024887 File Offset: 0x00022A87
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToBoolean(provider);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002489A File Offset: 0x00022A9A
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToChar(provider);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000248AD File Offset: 0x00022AAD
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToSByte(provider);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000248C0 File Offset: 0x00022AC0
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToByte(provider);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000248D3 File Offset: 0x00022AD3
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt16(provider);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000248E6 File Offset: 0x00022AE6
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt16(provider);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000248F9 File Offset: 0x00022AF9
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt32(provider);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002490C File Offset: 0x00022B0C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt32(provider);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002491F File Offset: 0x00022B1F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt64(provider);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00024932 File Offset: 0x00022B32
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt64(provider);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00024945 File Offset: 0x00022B45
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToSingle(provider);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00024958 File Offset: 0x00022B58
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDouble(provider);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002496B File Offset: 0x00022B6B
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDecimal(provider);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0002497E File Offset: 0x00022B7E
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDateTime(provider);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00024994 File Offset: 0x00022B94
		string IConvertible.ToString(IFormatProvider provider)
		{
			return this._value.ToString(provider);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000249B0 File Offset: 0x00022BB0
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToType(conversionType, provider);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000249C4 File Offset: 0x00022BC4
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000249DF File Offset: 0x00022BDF
		public override bool Equals(object obj)
		{
			return obj is SevenBitNumber && ((SevenBitNumber)obj)._value == this._value;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00024A00 File Offset: 0x00022C00
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x04000A0B RID: 2571
		public static readonly SevenBitNumber MinValue = new SevenBitNumber(0);

		// Token: 0x04000A0C RID: 2572
		public static readonly SevenBitNumber MaxValue = new SevenBitNumber(127);

		// Token: 0x04000A0D RID: 2573
		public static readonly SevenBitNumber[] Values = (from value in Enumerable.Range((int)SevenBitNumber.MinValue, (int)(SevenBitNumber.MaxValue - SevenBitNumber.MinValue + 1))
			select (SevenBitNumber)((byte)value)).ToArray<SevenBitNumber>();

		// Token: 0x04000A0E RID: 2574
		private const byte Min = 0;

		// Token: 0x04000A0F RID: 2575
		private const byte Max = 127;

		// Token: 0x04000A10 RID: 2576
		private readonly byte _value;
	}
}
