using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C1 RID: 193
	public sealed class BarBeatTicksTimeSpan : ITimeSpan, IComparable, IComparable<BarBeatTicksTimeSpan>, IEquatable<BarBeatTicksTimeSpan>
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x000156C6 File Offset: 0x000138C6
		public BarBeatTicksTimeSpan()
			: this(0L, 0L)
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000156D2 File Offset: 0x000138D2
		public BarBeatTicksTimeSpan(long bars)
			: this(bars, 0L)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000156DD File Offset: 0x000138DD
		public BarBeatTicksTimeSpan(long bars, long beats)
			: this(bars, beats, 0L)
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000156EC File Offset: 0x000138EC
		public BarBeatTicksTimeSpan(long bars, long beats, long ticks)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNegative("beats", beats, "Beats number is negative.");
			ThrowIfArgument.IsNegative("ticks", ticks, "Ticks number is negative.");
			this.Bars = bars;
			this.Beats = beats;
			this.Ticks = ticks;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00015744 File Offset: 0x00013944
		public long Bars { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001574C File Offset: 0x0001394C
		public long Beats { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00015754 File Offset: 0x00013954
		public long Ticks { get; }

		// Token: 0x06000468 RID: 1128 RVA: 0x0001575C File Offset: 0x0001395C
		public static bool TryParse(string input, out BarBeatTicksTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse<BarBeatTicksTimeSpan>(input, new Parsing<BarBeatTicksTimeSpan>(BarBeatTicksTimeSpanParser.TryParse), out timeSpan);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015771 File Offset: 0x00013971
		public static BarBeatTicksTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<BarBeatTicksTimeSpan>(input, new Parsing<BarBeatTicksTimeSpan>(BarBeatTicksTimeSpanParser.TryParse));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00015785 File Offset: 0x00013985
		public static bool operator ==(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			if (timeSpan1 == null)
			{
				return timeSpan2 == null;
			}
			return timeSpan1.Equals(timeSpan2);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00015796 File Offset: 0x00013996
		public static bool operator !=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000157A4 File Offset: 0x000139A4
		public static BarBeatTicksTimeSpan operator +(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new BarBeatTicksTimeSpan(timeSpan1.Bars + timeSpan2.Bars, timeSpan1.Beats + timeSpan2.Beats, timeSpan1.Ticks + timeSpan2.Ticks);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000157F4 File Offset: 0x000139F4
		public static BarBeatTicksTimeSpan operator -(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new BarBeatTicksTimeSpan(timeSpan1.Bars - timeSpan2.Bars, timeSpan1.Beats - timeSpan2.Beats, timeSpan1.Ticks - timeSpan2.Ticks);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001585C File Offset: 0x00013A5C
		public static bool operator <(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001587E File Offset: 0x00013A7E
		public static bool operator >(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000158A0 File Offset: 0x00013AA0
		public static bool operator <=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000158C5 File Offset: 0x00013AC5
		public static bool operator >=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000158EA File Offset: 0x00013AEA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BarBeatTicksTimeSpan);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000158F8 File Offset: 0x00013AF8
		public override int GetHashCode()
		{
			return ((17 * 23 + this.Bars.GetHashCode()) * 23 + this.Beats.GetHashCode()) * 23 + this.Ticks.GetHashCode();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001593D File Offset: 0x00013B3D
		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", this.Bars, this.Beats, this.Ticks);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001596C File Offset: 0x00013B6C
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = timeSpan as BarBeatTicksTimeSpan;
			if (!(barBeatTicksTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + barBeatTicksTimeSpan;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000159B4 File Offset: 0x00013BB4
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = timeSpan as BarBeatTicksTimeSpan;
			if (!(barBeatTicksTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - barBeatTicksTimeSpan;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000159FC File Offset: 0x00013BFC
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new BarBeatTicksTimeSpan(MathUtilities.RoundToLong((double)this.Bars * multiplier), MathUtilities.RoundToLong((double)this.Beats * multiplier), MathUtilities.RoundToLong((double)this.Ticks * multiplier));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00015A48 File Offset: 0x00013C48
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new BarBeatTicksTimeSpan(MathUtilities.RoundToLong((double)this.Bars / divisor), MathUtilities.RoundToLong((double)this.Beats / divisor), MathUtilities.RoundToLong((double)this.Ticks / divisor));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00015A94 File Offset: 0x00013C94
		public ITimeSpan Clone()
		{
			return new BarBeatTicksTimeSpan(this.Bars, this.Beats, this.Ticks);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00015AB0 File Offset: 0x00013CB0
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = other as BarBeatTicksTimeSpan;
			if (barBeatTicksTimeSpan == null)
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return this.CompareTo(barBeatTicksTimeSpan);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015AE4 File Offset: 0x00013CE4
		public int CompareTo(BarBeatTicksTimeSpan other)
		{
			if (other == null)
			{
				return 1;
			}
			long num = this.Bars - other.Bars;
			long num2 = this.Beats - other.Beats;
			long num3 = this.Ticks - other.Ticks;
			return Math.Sign((num != 0L) ? num : ((num2 != 0L) ? num2 : num3));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015B32 File Offset: 0x00013D32
		public bool Equals(BarBeatTicksTimeSpan other)
		{
			return this == other || (other != null && (this.Bars == other.Bars && this.Beats == other.Beats) && this.Ticks == other.Ticks);
		}
	}
}
