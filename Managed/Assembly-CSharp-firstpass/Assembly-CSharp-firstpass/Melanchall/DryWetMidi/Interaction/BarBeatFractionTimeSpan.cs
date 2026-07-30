using System;
using System.Globalization;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C0 RID: 192
	public sealed class BarBeatFractionTimeSpan : ITimeSpan, IComparable, IComparable<BarBeatFractionTimeSpan>, IEquatable<BarBeatFractionTimeSpan>
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x000152E4 File Offset: 0x000134E4
		public BarBeatFractionTimeSpan()
			: this(0L, 0.0)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000152F7 File Offset: 0x000134F7
		public BarBeatFractionTimeSpan(long bars)
			: this(bars, 0.0)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00015309 File Offset: 0x00013509
		public BarBeatFractionTimeSpan(long bars, double beats)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNegative("beats", beats, "Beats number is negative.");
			this.Bars = bars;
			this.Beats = beats;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001533F File Offset: 0x0001353F
		public long Bars { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00015347 File Offset: 0x00013547
		public double Beats { get; }

		// Token: 0x0600044C RID: 1100 RVA: 0x0001534F File Offset: 0x0001354F
		public static bool TryParse(string input, out BarBeatFractionTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse<BarBeatFractionTimeSpan>(input, new Parsing<BarBeatFractionTimeSpan>(BarBeatFractionTimeSpanParser.TryParse), out timeSpan);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00015364 File Offset: 0x00013564
		public static BarBeatFractionTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<BarBeatFractionTimeSpan>(input, new Parsing<BarBeatFractionTimeSpan>(BarBeatFractionTimeSpanParser.TryParse));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00015378 File Offset: 0x00013578
		public static bool operator ==(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			if (timeSpan1 == null)
			{
				return timeSpan2 == null;
			}
			return timeSpan1.Equals(timeSpan2);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00015389 File Offset: 0x00013589
		public static bool operator !=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015395 File Offset: 0x00013595
		public static BarBeatFractionTimeSpan operator +(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new BarBeatFractionTimeSpan(timeSpan1.Bars + timeSpan2.Bars, timeSpan1.Beats + timeSpan2.Beats);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000153CC File Offset: 0x000135CC
		public static BarBeatFractionTimeSpan operator -(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new BarBeatFractionTimeSpan(timeSpan1.Bars - timeSpan2.Bars, timeSpan1.Beats - timeSpan2.Beats);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015427 File Offset: 0x00013627
		public static bool operator <(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015449 File Offset: 0x00013649
		public static bool operator >(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001546B File Offset: 0x0001366B
		public static bool operator <=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00015490 File Offset: 0x00013690
		public static bool operator >=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000154B5 File Offset: 0x000136B5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BarBeatFractionTimeSpan);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000154C4 File Offset: 0x000136C4
		public override int GetHashCode()
		{
			return (17 * 23 + this.Bars.GetHashCode()) * 23 + this.Beats.GetHashCode();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000154F8 File Offset: 0x000136F8
		public override string ToString()
		{
			return string.Format("{0}_{1}", this.Bars, this.Beats.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00015530 File Offset: 0x00013730
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = timeSpan as BarBeatFractionTimeSpan;
			if (!(barBeatFractionTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + barBeatFractionTimeSpan;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00015578 File Offset: 0x00013778
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = timeSpan as BarBeatFractionTimeSpan;
			if (!(barBeatFractionTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - barBeatFractionTimeSpan;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000155BD File Offset: 0x000137BD
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new BarBeatFractionTimeSpan(MathUtilities.RoundToLong((double)this.Bars * multiplier), this.Beats * multiplier);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000155EA File Offset: 0x000137EA
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new BarBeatFractionTimeSpan(MathUtilities.RoundToLong((double)this.Bars / divisor), this.Beats / divisor);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00015617 File Offset: 0x00013817
		public ITimeSpan Clone()
		{
			return new BarBeatFractionTimeSpan(this.Bars, this.Beats);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001562C File Offset: 0x0001382C
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = other as BarBeatFractionTimeSpan;
			if (barBeatFractionTimeSpan == null)
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return this.CompareTo(barBeatFractionTimeSpan);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015660 File Offset: 0x00013860
		public int CompareTo(BarBeatFractionTimeSpan other)
		{
			if (other == null)
			{
				return 1;
			}
			long num = this.Bars - other.Bars;
			double num2 = this.Beats - other.Beats;
			return Math.Sign((num != 0L) ? ((double)num) : num2);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001569B File Offset: 0x0001389B
		public bool Equals(BarBeatFractionTimeSpan other)
		{
			return this == other || (other != null && this.Bars == other.Bars && this.Beats == other.Beats);
		}
	}
}
