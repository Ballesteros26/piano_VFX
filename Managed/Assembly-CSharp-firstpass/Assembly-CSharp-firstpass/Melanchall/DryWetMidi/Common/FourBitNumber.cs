using System;
using System.Linq;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C1 RID: 449
	public struct FourBitNumber : IComparable<FourBitNumber>, IConvertible
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x000244DE File Offset: 0x000226DE
		public FourBitNumber(byte value)
		{
			ThrowIfArgument.IsOutOfRange("value", (int)value, 0, 15, "Value is out of range valid for four-bit number.");
			this._value = value;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000244FC File Offset: 0x000226FC
		public static bool TryParse(string input, out FourBitNumber fourBitNumber)
		{
			fourBitNumber = default(FourBitNumber);
			byte b;
			bool flag = ShortByteParser.TryParse(input, 0, 15, out b).Status == ParsingStatus.Parsed;
			if (flag)
			{
				fourBitNumber = (FourBitNumber)b;
			}
			return flag;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00024534 File Offset: 0x00022734
		public static FourBitNumber Parse(string input)
		{
			byte b;
			ParsingResult parsingResult = ShortByteParser.TryParse(input, 0, 15, out b);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return (FourBitNumber)b;
			}
			throw parsingResult.Exception;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00024562 File Offset: 0x00022762
		public static implicit operator byte(FourBitNumber number)
		{
			return number._value;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002456A File Offset: 0x0002276A
		public static explicit operator FourBitNumber(byte number)
		{
			return new FourBitNumber(number);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00024574 File Offset: 0x00022774
		public int CompareTo(FourBitNumber other)
		{
			return this._value.CompareTo(other._value);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00024598 File Offset: 0x00022798
		public TypeCode GetTypeCode()
		{
			return this._value.GetTypeCode();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000245B3 File Offset: 0x000227B3
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToBoolean(provider);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000245C6 File Offset: 0x000227C6
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToChar(provider);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000245D9 File Offset: 0x000227D9
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToSByte(provider);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000245EC File Offset: 0x000227EC
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToByte(provider);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000245FF File Offset: 0x000227FF
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt16(provider);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00024612 File Offset: 0x00022812
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt16(provider);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00024625 File Offset: 0x00022825
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt32(provider);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00024638 File Offset: 0x00022838
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt32(provider);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002464B File Offset: 0x0002284B
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToInt64(provider);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0002465E File Offset: 0x0002285E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToUInt64(provider);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00024671 File Offset: 0x00022871
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToSingle(provider);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00024684 File Offset: 0x00022884
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDouble(provider);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00024697 File Offset: 0x00022897
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDecimal(provider);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000246AA File Offset: 0x000228AA
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToDateTime(provider);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000246C0 File Offset: 0x000228C0
		string IConvertible.ToString(IFormatProvider provider)
		{
			return this._value.ToString(provider);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000246DC File Offset: 0x000228DC
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this._value).ToType(conversionType, provider);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000246F0 File Offset: 0x000228F0
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002470B File Offset: 0x0002290B
		public override bool Equals(object obj)
		{
			return obj is FourBitNumber && ((FourBitNumber)obj)._value == this._value;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002472C File Offset: 0x0002292C
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x04000A05 RID: 2565
		public static readonly FourBitNumber MinValue = new FourBitNumber(0);

		// Token: 0x04000A06 RID: 2566
		public static readonly FourBitNumber MaxValue = new FourBitNumber(15);

		// Token: 0x04000A07 RID: 2567
		public static readonly FourBitNumber[] Values = (from value in Enumerable.Range((int)FourBitNumber.MinValue, (int)(FourBitNumber.MaxValue - FourBitNumber.MinValue + 1))
			select (FourBitNumber)((byte)value)).ToArray<FourBitNumber>();

		// Token: 0x04000A08 RID: 2568
		private const byte Min = 0;

		// Token: 0x04000A09 RID: 2569
		private const byte Max = 15;

		// Token: 0x04000A0A RID: 2570
		private readonly byte _value;
	}
}
