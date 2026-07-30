using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C5 RID: 197
	public sealed class MusicalTimeSpan : ITimeSpan, IComparable, IComparable<MusicalTimeSpan>, IEquatable<MusicalTimeSpan>
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001666F File Offset: 0x0001486F
		public MusicalTimeSpan()
			: this(0L, 1L, true)
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001667C File Offset: 0x0001487C
		public MusicalTimeSpan(long fraction)
			: this(1L, fraction, true)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00016688 File Offset: 0x00014888
		public MusicalTimeSpan(long numerator, long denominator, bool simplify = true)
		{
			ThrowIfArgument.IsNegative("numerator", numerator, "Numerator is negative.");
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			long num = (simplify ? MathUtilities.GreatestCommonDivisor(numerator, denominator) : 1L);
			this.Numerator = numerator / num;
			this.Denominator = denominator / num;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000166DC File Offset: 0x000148DC
		public long Numerator { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000166E4 File Offset: 0x000148E4
		public long Denominator { get; }

		// Token: 0x060004D0 RID: 1232 RVA: 0x000166EC File Offset: 0x000148EC
		public MusicalTimeSpan Dotted(int dotsCount)
		{
			ThrowIfArgument.IsNegative("dotsCount", dotsCount, "Dots count is negative.");
			return new MusicalTimeSpan(this.Numerator * (long)((1 << dotsCount + 1) - 1), this.Denominator * (1L << (dotsCount & 31)), true);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00016724 File Offset: 0x00014924
		public MusicalTimeSpan SingleDotted()
		{
			return this.Dotted(1);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001672D File Offset: 0x0001492D
		public MusicalTimeSpan DoubleDotted()
		{
			return this.Dotted(2);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00016736 File Offset: 0x00014936
		public MusicalTimeSpan Tuplet(int tupletNotesCount, int tupletSpaceSize)
		{
			ThrowIfArgument.IsNonpositive("tupletNotesCount", tupletNotesCount, "Tuplet's notes count is zero or negative.");
			ThrowIfArgument.IsNonpositive("tupletSpaceSize", tupletSpaceSize, "Tuplet's space size is zero or negative.");
			return new MusicalTimeSpan(this.Numerator * (long)tupletSpaceSize, this.Denominator * (long)tupletNotesCount, true);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00016770 File Offset: 0x00014970
		public MusicalTimeSpan Triplet()
		{
			return this.Tuplet(3, 2);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001677A File Offset: 0x0001497A
		public MusicalTimeSpan Duplet()
		{
			return this.Tuplet(2, 3);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00016784 File Offset: 0x00014984
		public double Divide(MusicalTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			if (timeSpan.Numerator == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			return (double)this.Numerator * (double)timeSpan.Denominator / (double)(this.Denominator * timeSpan.Numerator);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000167C2 File Offset: 0x000149C2
		public MusicalTimeSpan ChangeDenominator(long denominator)
		{
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			return new MusicalTimeSpan(MathUtilities.RoundToLong((double)denominator / (double)this.Denominator * (double)this.Numerator), denominator, false);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000167F2 File Offset: 0x000149F2
		public static bool TryParse(string input, out MusicalTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse<MusicalTimeSpan>(input, new Parsing<MusicalTimeSpan>(MusicalTimeSpanParser.TryParse), out timeSpan);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00016807 File Offset: 0x00014A07
		public static MusicalTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MusicalTimeSpan>(input, new Parsing<MusicalTimeSpan>(MusicalTimeSpanParser.TryParse));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001681B File Offset: 0x00014A1B
		private static void ReduceToCommonDenominator(MusicalTimeSpan fraction1, MusicalTimeSpan fraction2, out long numerator1, out long numerator2, out long denominator)
		{
			denominator = MathUtilities.LeastCommonMultiple(fraction1.Denominator, fraction2.Denominator);
			numerator1 = fraction1.Numerator * denominator / fraction1.Denominator;
			numerator2 = fraction2.Numerator * denominator / fraction2.Denominator;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00016857 File Offset: 0x00014A57
		public static bool operator ==(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			if (timeSpan1 == null)
			{
				return timeSpan2 == null;
			}
			return timeSpan1.Equals(timeSpan2);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016868 File Offset: 0x00014A68
		public static bool operator !=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016874 File Offset: 0x00014A74
		public static MusicalTimeSpan operator *(MusicalTimeSpan timeSpan, long number)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsNegative("number", number, "Number is negative.");
			return new MusicalTimeSpan(timeSpan.Numerator * number, timeSpan.Denominator, true);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000168A5 File Offset: 0x00014AA5
		public static MusicalTimeSpan operator *(long number, MusicalTimeSpan timeSpan)
		{
			return timeSpan * number;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000168AE File Offset: 0x00014AAE
		public static MusicalTimeSpan operator /(MusicalTimeSpan timeSpan, long number)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsNonpositive("number", number, "Number is zero or negative.");
			return new MusicalTimeSpan(timeSpan.Numerator, timeSpan.Denominator * number, true);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000168E0 File Offset: 0x00014AE0
		public static MusicalTimeSpan operator +(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			long num;
			long num2;
			long num3;
			MusicalTimeSpan.ReduceToCommonDenominator(timeSpan1, timeSpan2, out num, out num2, out num3);
			return new MusicalTimeSpan(num + num2, num3, true);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001691C File Offset: 0x00014B1C
		public static MusicalTimeSpan operator -(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			long num;
			long num2;
			long num3;
			MusicalTimeSpan.ReduceToCommonDenominator(timeSpan1, timeSpan2, out num, out num2, out num3);
			if (num < num2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MusicalTimeSpan(num - num2, num3, true);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001696A File Offset: 0x00014B6A
		public static bool operator <(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001698C File Offset: 0x00014B8C
		public static bool operator >(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000169AE File Offset: 0x00014BAE
		public static bool operator <=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000169D3 File Offset: 0x00014BD3
		public static bool operator >=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000169F8 File Offset: 0x00014BF8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MusicalTimeSpan);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016A08 File Offset: 0x00014C08
		public override int GetHashCode()
		{
			return (17 * 23 + this.Numerator.GetHashCode()) * 23 + this.Denominator.GetHashCode();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016A3B File Offset: 0x00014C3B
		public override string ToString()
		{
			return string.Format("{0}/{1}", this.Numerator, this.Denominator);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016A60 File Offset: 0x00014C60
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MusicalTimeSpan musicalTimeSpan = timeSpan as MusicalTimeSpan;
			if (!(musicalTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + musicalTimeSpan;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016AA8 File Offset: 0x00014CA8
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MusicalTimeSpan musicalTimeSpan = timeSpan as MusicalTimeSpan;
			if (!(musicalTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - musicalTimeSpan;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016AED File Offset: 0x00014CED
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MusicalTimeSpan(MathUtilities.RoundToLong((double)this.Numerator * MathUtilities.Round(multiplier, 3) * (double)MusicalTimeSpan.FractionPartMultiplier), this.Denominator * (long)MusicalTimeSpan.FractionPartMultiplier, true);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00016B2D File Offset: 0x00014D2D
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MusicalTimeSpan(this.Numerator * (long)MusicalTimeSpan.FractionPartMultiplier, MathUtilities.RoundToLong((double)this.Denominator * MathUtilities.Round(divisor, 3) * (double)MusicalTimeSpan.FractionPartMultiplier), true);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016B6D File Offset: 0x00014D6D
		public ITimeSpan Clone()
		{
			return new MusicalTimeSpan(this.Numerator, this.Denominator, true);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00016B84 File Offset: 0x00014D84
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			MusicalTimeSpan musicalTimeSpan = other as MusicalTimeSpan;
			if (musicalTimeSpan == null)
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return this.CompareTo(musicalTimeSpan);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00016BB7 File Offset: 0x00014DB7
		public int CompareTo(MusicalTimeSpan other)
		{
			if (other == null)
			{
				return 1;
			}
			return Math.Sign(((double)this.Numerator * (double)other.Denominator - (double)other.Numerator * (double)this.Denominator) / ((double)this.Denominator * (double)other.Denominator));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public bool Equals(MusicalTimeSpan other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			long num;
			long num2;
			long num3;
			MusicalTimeSpan.ReduceToCommonDenominator(this, other, out num, out num2, out num3);
			return num == num2;
		}

		// Token: 0x040006F6 RID: 1782
		public static readonly MusicalTimeSpan Whole = new MusicalTimeSpan(1L);

		// Token: 0x040006F7 RID: 1783
		public static readonly MusicalTimeSpan Half = new MusicalTimeSpan(2L);

		// Token: 0x040006F8 RID: 1784
		public static readonly MusicalTimeSpan Quarter = new MusicalTimeSpan(4L);

		// Token: 0x040006F9 RID: 1785
		public static readonly MusicalTimeSpan Eighth = new MusicalTimeSpan(8L);

		// Token: 0x040006FA RID: 1786
		public static readonly MusicalTimeSpan Sixteenth = new MusicalTimeSpan(16L);

		// Token: 0x040006FB RID: 1787
		public static readonly MusicalTimeSpan ThirtySecond = new MusicalTimeSpan(32L);

		// Token: 0x040006FC RID: 1788
		public static readonly MusicalTimeSpan SixtyFourth = new MusicalTimeSpan(64L);

		// Token: 0x040006FD RID: 1789
		private const long ZeroTimeSpanNumerator = 0L;

		// Token: 0x040006FE RID: 1790
		private const long ZeroTimeSpanDenominator = 1L;

		// Token: 0x040006FF RID: 1791
		private const long FractionNumerator = 1L;

		// Token: 0x04000700 RID: 1792
		private const int WholeFraction = 1;

		// Token: 0x04000701 RID: 1793
		private const int HalfFraction = 2;

		// Token: 0x04000702 RID: 1794
		private const int QuarterFraction = 4;

		// Token: 0x04000703 RID: 1795
		private const int EighthFraction = 8;

		// Token: 0x04000704 RID: 1796
		private const int SixteenthFraction = 16;

		// Token: 0x04000705 RID: 1797
		private const int ThirtySecondFraction = 32;

		// Token: 0x04000706 RID: 1798
		private const int SixtyFourthFraction = 64;

		// Token: 0x04000707 RID: 1799
		private const int TripletNotesCount = 3;

		// Token: 0x04000708 RID: 1800
		private const int TripletSpaceSize = 2;

		// Token: 0x04000709 RID: 1801
		private const int DupletNotesCount = 2;

		// Token: 0x0400070A RID: 1802
		private const int DupletSpaceSize = 3;

		// Token: 0x0400070B RID: 1803
		private const int SingleDotCount = 1;

		// Token: 0x0400070C RID: 1804
		private const int DoubleDotCount = 2;

		// Token: 0x0400070D RID: 1805
		private const int NumberOfDigitsAfterDecimalPoint = 3;

		// Token: 0x0400070E RID: 1806
		private static readonly int FractionPartMultiplier = (int)Math.Pow(10.0, 3.0);
	}
}
