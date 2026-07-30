using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A8 RID: 168
	public sealed class Tempo
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0001250C File Offset: 0x0001070C
		public Tempo(long microsecondsPerQuarterNote)
		{
			ThrowIfArgument.IsNonpositive("microsecondsPerQuarterNote", microsecondsPerQuarterNote, "Number of microseconds per quarter note is zero or negative.");
			this.MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001252B File Offset: 0x0001072B
		public long MicrosecondsPerQuarterNote { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00012533 File Offset: 0x00010733
		public long BeatsPerMinute
		{
			get
			{
				return 60000000L / this.MicrosecondsPerQuarterNote;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00012542 File Offset: 0x00010742
		public static Tempo FromMillisecondsPerQuarterNote(long millisecondsPerQuarterNote)
		{
			ThrowIfArgument.IsNonpositive("millisecondsPerQuarterNote", millisecondsPerQuarterNote, "Number of milliseconds per quarter note is zero or negative.");
			return new Tempo(millisecondsPerQuarterNote * 1000L);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012561 File Offset: 0x00010761
		public static Tempo FromBeatsPerMinute(int beatsPerMinute)
		{
			ThrowIfArgument.IsNonpositive("beatsPerMinute", beatsPerMinute, "Number of beats per minute is zero or negative.");
			return new Tempo(MathUtilities.RoundToLong(60000000.0 / (double)beatsPerMinute));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00012589 File Offset: 0x00010789
		public static bool operator ==(Tempo tempo1, Tempo tempo2)
		{
			return tempo1 == tempo2 || (tempo1 != null && tempo2 != null && tempo1.MicrosecondsPerQuarterNote == tempo2.MicrosecondsPerQuarterNote);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000125A7 File Offset: 0x000107A7
		public static bool operator !=(Tempo tempo1, Tempo tempo2)
		{
			return !(tempo1 == tempo2);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000125B3 File Offset: 0x000107B3
		public override string ToString()
		{
			return string.Format("{0} μs/qnote", this.MicrosecondsPerQuarterNote);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000125CA File Offset: 0x000107CA
		public override bool Equals(object obj)
		{
			return this == obj as Tempo;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000125D8 File Offset: 0x000107D8
		public override int GetHashCode()
		{
			return this.MicrosecondsPerQuarterNote.GetHashCode();
		}

		// Token: 0x0400068F RID: 1679
		public static readonly Tempo Default = new Tempo(500000L);

		// Token: 0x04000690 RID: 1680
		private const int MicrosecondsInMinute = 60000000;

		// Token: 0x04000691 RID: 1681
		private const int MicrosecondsInMillisecond = 1000;
	}
}
