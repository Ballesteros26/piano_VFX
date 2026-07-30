using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C3 RID: 195
	public sealed class MetricTimeSpan : ITimeSpan, IComparable, IComparable<MetricTimeSpan>, IEquatable<MetricTimeSpan>
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x00015E39 File Offset: 0x00014039
		public MetricTimeSpan()
			: this(0L)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00015E43 File Offset: 0x00014043
		public MetricTimeSpan(long totalMicroseconds)
		{
			ThrowIfArgument.IsNegative("totalMicroseconds", totalMicroseconds, "Number of microseconds is negative.");
			this._timeSpan = new TimeSpan(totalMicroseconds * 10L);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00015E6B File Offset: 0x0001406B
		public MetricTimeSpan(TimeSpan timeSpan)
		{
			this._timeSpan = timeSpan;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00015E7A File Offset: 0x0001407A
		public MetricTimeSpan(int hours, int minutes, int seconds)
			: this(hours, minutes, seconds, 0)
		{
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015E88 File Offset: 0x00014088
		public MetricTimeSpan(int hours, int minutes, int seconds, int milliseconds)
		{
			ThrowIfArgument.IsNegative("hours", hours, "Number of hours is negative.");
			ThrowIfArgument.IsNegative("minutes", minutes, "Number of minutes is negative.");
			ThrowIfArgument.IsNegative("seconds", seconds, "Number of seconds is negative.");
			ThrowIfArgument.IsNegative("milliseconds", milliseconds, "Number of milliseconds is negative.");
			this._timeSpan = new TimeSpan(0, hours, minutes, seconds, milliseconds);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015EF0 File Offset: 0x000140F0
		public long TotalMicroseconds
		{
			get
			{
				return this._timeSpan.Ticks / 10L;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00015F10 File Offset: 0x00014110
		public int Hours
		{
			get
			{
				return this._timeSpan.Hours;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00015F2C File Offset: 0x0001412C
		public int Minutes
		{
			get
			{
				return this._timeSpan.Minutes;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00015F48 File Offset: 0x00014148
		public int Seconds
		{
			get
			{
				return this._timeSpan.Seconds;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00015F64 File Offset: 0x00014164
		public int Milliseconds
		{
			get
			{
				return this._timeSpan.Milliseconds;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015F80 File Offset: 0x00014180
		public double Divide(MetricTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			if (timeSpan._timeSpan.Ticks == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			return (double)this._timeSpan.Ticks / (double)timeSpan._timeSpan.Ticks;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015FD2 File Offset: 0x000141D2
		public static bool TryParse(string input, out MetricTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse<MetricTimeSpan>(input, new Parsing<MetricTimeSpan>(MetricTimeSpanParser.TryParse), out timeSpan);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015FE7 File Offset: 0x000141E7
		public static MetricTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MetricTimeSpan>(input, new Parsing<MetricTimeSpan>(MetricTimeSpanParser.TryParse));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015FFB File Offset: 0x000141FB
		public static implicit operator MetricTimeSpan(TimeSpan timeSpan)
		{
			return new MetricTimeSpan(timeSpan);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016003 File Offset: 0x00014203
		public static implicit operator TimeSpan(MetricTimeSpan timeSpan)
		{
			return timeSpan._timeSpan;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001600B File Offset: 0x0001420B
		public static bool operator ==(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			if (timeSpan1 == null)
			{
				return timeSpan2 == null;
			}
			return timeSpan1.Equals(timeSpan2);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001601C File Offset: 0x0001421C
		public static bool operator !=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016028 File Offset: 0x00014228
		public static MetricTimeSpan operator +(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new MetricTimeSpan(timeSpan1.TotalMicroseconds + timeSpan2.TotalMicroseconds);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016054 File Offset: 0x00014254
		public static MetricTimeSpan operator -(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MetricTimeSpan(timeSpan1.TotalMicroseconds - timeSpan2.TotalMicroseconds);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000160A2 File Offset: 0x000142A2
		public static bool operator <(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000160C4 File Offset: 0x000142C4
		public static bool operator >(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000160E6 File Offset: 0x000142E6
		public static bool operator <=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001610B File Offset: 0x0001430B
		public static bool operator >=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016130 File Offset: 0x00014330
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MetricTimeSpan);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016140 File Offset: 0x00014340
		public override int GetHashCode()
		{
			return this.TotalMicroseconds.GetHashCode();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001615C File Offset: 0x0001435C
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}", new object[] { this.Hours, this.Minutes, this.Seconds, this.Milliseconds });
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000161B4 File Offset: 0x000143B4
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MetricTimeSpan metricTimeSpan = timeSpan as MetricTimeSpan;
			if (!(metricTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + metricTimeSpan;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000161FC File Offset: 0x000143FC
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MetricTimeSpan metricTimeSpan = timeSpan as MetricTimeSpan;
			if (!(metricTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - metricTimeSpan;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00016241 File Offset: 0x00014441
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MetricTimeSpan(MathUtilities.RoundToLong((double)this.TotalMicroseconds * multiplier));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016266 File Offset: 0x00014466
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MetricTimeSpan(MathUtilities.RoundToLong((double)this.TotalMicroseconds / divisor));
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001628B File Offset: 0x0001448B
		public ITimeSpan Clone()
		{
			return new MetricTimeSpan(this.TotalMicroseconds);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016298 File Offset: 0x00014498
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			MetricTimeSpan metricTimeSpan = other as MetricTimeSpan;
			if (metricTimeSpan == null)
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return this.CompareTo(metricTimeSpan);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000162CC File Offset: 0x000144CC
		public int CompareTo(MetricTimeSpan other)
		{
			if (other == null)
			{
				return 1;
			}
			return this._timeSpan.CompareTo(other._timeSpan);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000162F2 File Offset: 0x000144F2
		public bool Equals(MetricTimeSpan other)
		{
			return this == other || (other != null && this._timeSpan == other._timeSpan);
		}

		// Token: 0x040006F2 RID: 1778
		private const int MicrosecondsInMillisecond = 1000;

		// Token: 0x040006F3 RID: 1779
		private const long TicksInMicrosecond = 10L;

		// Token: 0x040006F4 RID: 1780
		private readonly TimeSpan _timeSpan;
	}
}
