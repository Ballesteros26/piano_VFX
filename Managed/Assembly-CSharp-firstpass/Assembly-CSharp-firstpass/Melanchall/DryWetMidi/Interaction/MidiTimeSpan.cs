using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C4 RID: 196
	public sealed class MidiTimeSpan : ITimeSpan, IComparable, IComparable<MidiTimeSpan>, IEquatable<MidiTimeSpan>
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00016310 File Offset: 0x00014510
		public MidiTimeSpan()
			: this(0L)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001631A File Offset: 0x0001451A
		public MidiTimeSpan(long timeSpan)
		{
			ThrowIfLengthArgument.IsNegative("timeSpan", timeSpan);
			this.TimeSpan = timeSpan;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00016334 File Offset: 0x00014534
		public long TimeSpan { get; }

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001633C File Offset: 0x0001453C
		public double Divide(MidiTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			if (timeSpan == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			return (double)this.TimeSpan / (double)timeSpan;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001636B File Offset: 0x0001456B
		public static bool TryParse(string input, out MidiTimeSpan timeSpan)
		{
			return MidiTimeSpanParser.TryParse(input, out timeSpan).Status == ParsingStatus.Parsed;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001637C File Offset: 0x0001457C
		public static MidiTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MidiTimeSpan>(input, new Parsing<MidiTimeSpan>(MidiTimeSpanParser.TryParse));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016390 File Offset: 0x00014590
		public static explicit operator MidiTimeSpan(long timeSpan)
		{
			return new MidiTimeSpan(timeSpan);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016398 File Offset: 0x00014598
		public static implicit operator long(MidiTimeSpan timeSpan)
		{
			return timeSpan.TimeSpan;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000163A0 File Offset: 0x000145A0
		public static bool operator ==(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			if (timeSpan1 == null)
			{
				return timeSpan2 == null;
			}
			return timeSpan1.Equals(timeSpan2);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000163B1 File Offset: 0x000145B1
		public static bool operator !=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000163BD File Offset: 0x000145BD
		public static MidiTimeSpan operator +(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new MidiTimeSpan(timeSpan1.TimeSpan + timeSpan2.TimeSpan);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000163E8 File Offset: 0x000145E8
		public static MidiTimeSpan operator -(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1.TimeSpan < timeSpan2.TimeSpan)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MidiTimeSpan(timeSpan1.TimeSpan - timeSpan2.TimeSpan);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001643B File Offset: 0x0001463B
		public static bool operator <(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan < timeSpan2.TimeSpan;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00016461 File Offset: 0x00014661
		public static bool operator >(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan > timeSpan2.TimeSpan;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016487 File Offset: 0x00014687
		public static bool operator <=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan <= timeSpan2.TimeSpan;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000164B0 File Offset: 0x000146B0
		public static bool operator >=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan >= timeSpan2.TimeSpan;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000164DC File Offset: 0x000146DC
		public override string ToString()
		{
			return this.TimeSpan.ToString();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000164F7 File Offset: 0x000146F7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MidiTimeSpan);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016508 File Offset: 0x00014708
		public override int GetHashCode()
		{
			return this.TimeSpan.GetHashCode();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00016524 File Offset: 0x00014724
		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MidiTimeSpan midiTimeSpan = timeSpan as MidiTimeSpan;
			if (!(midiTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + midiTimeSpan;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001656C File Offset: 0x0001476C
		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanMode>("mode", mode);
			MidiTimeSpan midiTimeSpan = timeSpan as MidiTimeSpan;
			if (!(midiTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - midiTimeSpan;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000165B1 File Offset: 0x000147B1
		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MidiTimeSpan(MathUtilities.RoundToLong((double)this.TimeSpan * multiplier));
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000165D6 File Offset: 0x000147D6
		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MidiTimeSpan(MathUtilities.RoundToLong((double)this.TimeSpan / divisor));
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000165FB File Offset: 0x000147FB
		public ITimeSpan Clone()
		{
			return new MidiTimeSpan(this.TimeSpan);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00016608 File Offset: 0x00014808
		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			MidiTimeSpan midiTimeSpan = other as MidiTimeSpan;
			if (midiTimeSpan == null)
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return this.CompareTo(midiTimeSpan);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001663B File Offset: 0x0001483B
		public int CompareTo(MidiTimeSpan other)
		{
			if (other == null)
			{
				return 1;
			}
			return Math.Sign(this.TimeSpan - other.TimeSpan);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00016654 File Offset: 0x00014854
		public bool Equals(MidiTimeSpan other)
		{
			return this == other || (other != null && this.TimeSpan == other.TimeSpan);
		}
	}
}
