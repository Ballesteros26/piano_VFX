using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C2 RID: 194
	public sealed class MathTimeSpan : ITimeSpan, IComparable
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00015B6B File Offset: 0x00013D6B
		internal MathTimeSpan(ITimeSpan timeSpan1, ITimeSpan timeSpan2, MathOperation operation, TimeSpanMode mode)
		{
			this.TimeSpan1 = timeSpan1;
			this.TimeSpan2 = timeSpan2;
			this.Operation = operation;
			this.Mode = mode;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00015B90 File Offset: 0x00013D90
		public ITimeSpan TimeSpan1 { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00015B98 File Offset: 0x00013D98
		public ITimeSpan TimeSpan2 { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00015BA0 File Offset: 0x00013DA0
		public MathOperation Operation { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00015BA8 File Offset: 0x00013DA8
		public TimeSpanMode Mode { get; }

		// Token: 0x06000482 RID: 1154 RVA: 0x00015BB0 File Offset: 0x00013DB0
		public static bool operator ==(MathTimeSpan timeSpan1, MathTimeSpan timeSpan2)
		{
			return timeSpan1 == timeSpan2 || (timeSpan1 != null && timeSpan2 != null && (timeSpan1.TimeSpan1.Equals(timeSpan2.TimeSpan1) && timeSpan1.TimeSpan2.Equals(timeSpan2.TimeSpan2) && timeSpan1.Operation == timeSpan2.Operation) && timeSpan1.Mode == timeSpan2.Mode);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015C0F File Offset: 0x00013E0F
		public static bool operator !=(MathTimeSpan timeSpan1, MathTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015C1C File Offset: 0x00013E1C
		public override string ToString()
		{
			string text = ((this.Operation == MathOperation.Add) ? "+" : "-");
			Tuple<string, string> tuple = MathTimeSpan.ModeStrings[this.Mode];
			return string.Format("({0}{1} {2} {3}{4})", new object[] { this.TimeSpan1, tuple.Item1, text, this.TimeSpan2, tuple.Item2 });
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015C87 File Offset: 0x00013E87
		public override bool Equals(object obj)
		{
			return this == obj as MathTimeSpan;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015C98 File Offset: 0x00013E98
		public override int GetHashCode()
		{
			return (((17 * 23 + this.TimeSpan1.GetHashCode()) * 23 + this.TimeSpan2.GetHashCode()) * 23 + this.Operation.GetHashCode()) * 23 + this.Mode.GetHashCode();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015CF5 File Offset: 0x00013EF5
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			return TimeSpanUtilities.Add(this, timeSpan, mode);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015D15 File Offset: 0x00013F15
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			return TimeSpanUtilities.Subtract(this, timeSpan, mode);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015D35 File Offset: 0x00013F35
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MathTimeSpan(this.TimeSpan1.Multiply(multiplier), this.TimeSpan2.Multiply(multiplier), this.Operation, this.Mode);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015D70 File Offset: 0x00013F70
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNegative("divisor", divisor, "Divisor is negative.");
			return new MathTimeSpan(this.TimeSpan1.Divide(divisor), this.TimeSpan2.Divide(divisor), this.Operation, this.Mode);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00015DAB File Offset: 0x00013FAB
		public ITimeSpan Clone()
		{
			return new MathTimeSpan(this.TimeSpan1.Clone(), this.TimeSpan2.Clone(), this.Operation, this.Mode);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00015DD4 File Offset: 0x00013FD4
		public int CompareTo(object other)
		{
			throw new InvalidOperationException("Cannot compare MathTimeSpan.");
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00015DE0 File Offset: 0x00013FE0
		// Note: this type is marked as 'beforefieldinit'.
		static MathTimeSpan()
		{
			Dictionary<TimeSpanMode, Tuple<string, string>> dictionary = new Dictionary<TimeSpanMode, Tuple<string, string>>();
			dictionary[TimeSpanMode.TimeTime] = Tuple.Create<string, string>("T", "T");
			dictionary[TimeSpanMode.TimeLength] = Tuple.Create<string, string>("T", "L");
			dictionary[TimeSpanMode.LengthLength] = Tuple.Create<string, string>("L", "L");
			MathTimeSpan.ModeStrings = dictionary;
		}

		// Token: 0x040006EB RID: 1771
		private const string TimeModeString = "T";

		// Token: 0x040006EC RID: 1772
		private const string LengthModeString = "L";

		// Token: 0x040006ED RID: 1773
		private static readonly Dictionary<TimeSpanMode, Tuple<string, string>> ModeStrings;
	}
}
