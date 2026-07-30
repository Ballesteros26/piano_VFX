using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000150 RID: 336
	public sealed class TimeSignatureEvent : MetaEvent
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		public TimeSignatureEvent()
			: base(MidiEventType.TimeSignature)
		{
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001F4CF File Offset: 0x0001D6CF
		public TimeSignatureEvent(byte numerator, byte denominator)
			: this(numerator, denominator, 24, 8)
		{
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001F4DC File Offset: 0x0001D6DC
		public TimeSignatureEvent(byte numerator, byte denominator, byte clocksPerClick, byte thirtySecondNotesPerBeat)
			: this()
		{
			this.Numerator = numerator;
			this.Denominator = denominator;
			this.ClocksPerClick = clocksPerClick;
			this.ThirtySecondNotesPerBeat = thirtySecondNotesPerBeat;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001F501 File Offset: 0x0001D701
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001F509 File Offset: 0x0001D709
		public byte Numerator { get; set; } = 4;

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001F512 File Offset: 0x0001D712
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001F51A File Offset: 0x0001D71A
		public byte Denominator
		{
			get
			{
				return this._denominator;
			}
			set
			{
				ThrowIfArgument.DoesntSatisfyCondition("value", (int)value, new Predicate<int>(MathUtilities.IsPowerOfTwo), "Denominator is zero or is not a power of two.");
				this._denominator = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001F53F File Offset: 0x0001D73F
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001F547 File Offset: 0x0001D747
		public byte ClocksPerClick { get; set; } = 24;

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001F550 File Offset: 0x0001D750
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001F558 File Offset: 0x0001D758
		public byte ThirtySecondNotesPerBeat { get; set; } = 8;

		// Token: 0x060008AF RID: 2223 RVA: 0x0001F564 File Offset: 0x0001D764
		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			this.Numerator = reader.ReadByte();
			this.Denominator = (byte)Math.Pow(2.0, (double)reader.ReadByte());
			if (size >= 4)
			{
				this.ClocksPerClick = reader.ReadByte();
				this.ThirtySecondNotesPerBeat = reader.ReadByte();
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(this.Numerator);
			writer.WriteByte((byte)Math.Log((double)this.Denominator, 2.0));
			writer.WriteByte(this.ClocksPerClick);
			writer.WriteByte(this.ThirtySecondNotesPerBeat);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001F605 File Offset: 0x0001D805
		protected override int GetContentSize(WritingSettings settings)
		{
			return 4;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001F608 File Offset: 0x0001D808
		protected override MidiEvent CloneEvent()
		{
			return new TimeSignatureEvent(this.Numerator, this.Denominator, this.ClocksPerClick, this.ThirtySecondNotesPerBeat);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001F628 File Offset: 0x0001D828
		public override string ToString()
		{
			return string.Format("Time Signature ({0}/{1}, {2} clock/click, {3} 32nd/beat)", new object[] { this.Numerator, this.Denominator, this.ClocksPerClick, this.ThirtySecondNotesPerBeat });
		}

		// Token: 0x040008AB RID: 2219
		public const byte DefaultNumerator = 4;

		// Token: 0x040008AC RID: 2220
		public const byte DefaultDenominator = 4;

		// Token: 0x040008AD RID: 2221
		public const byte DefaultClocksPerClick = 24;

		// Token: 0x040008AE RID: 2222
		public const byte DefaultThirtySecondNotesPerBeat = 8;

		// Token: 0x040008AF RID: 2223
		private byte _denominator = 4;
	}
}
