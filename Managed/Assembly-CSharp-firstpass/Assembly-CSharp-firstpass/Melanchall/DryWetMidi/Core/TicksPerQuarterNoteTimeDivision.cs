using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000196 RID: 406
	public sealed class TicksPerQuarterNoteTimeDivision : TimeDivision
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x00021C9D File Offset: 0x0001FE9D
		public TicksPerQuarterNoteTimeDivision()
		{
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00021CAD File Offset: 0x0001FEAD
		public TicksPerQuarterNoteTimeDivision(short ticksPerQuarterNote)
		{
			ThrowIfArgument.IsNegative("ticksPerQuarterNote", (int)ticksPerQuarterNote, "Ticks per quarter-note must be non-negative number.");
			this.TicksPerQuarterNote = ticksPerQuarterNote;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public short TicksPerQuarterNote { get; } = 96;

		// Token: 0x060009DF RID: 2527 RVA: 0x00021CDC File Offset: 0x0001FEDC
		public static bool operator ==(TicksPerQuarterNoteTimeDivision timeDivision1, TicksPerQuarterNoteTimeDivision timeDivision2)
		{
			return timeDivision1 == timeDivision2 || (timeDivision1 != null && timeDivision2 != null && timeDivision1.TicksPerQuarterNote == timeDivision2.TicksPerQuarterNote);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00021CFA File Offset: 0x0001FEFA
		public static bool operator !=(TicksPerQuarterNoteTimeDivision timeDivision1, TicksPerQuarterNoteTimeDivision timeDivision2)
		{
			return !(timeDivision1 == timeDivision2);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00021D06 File Offset: 0x0001FF06
		internal override short ToInt16()
		{
			return this.TicksPerQuarterNote;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00021D0E File Offset: 0x0001FF0E
		public override TimeDivision Clone()
		{
			return new TicksPerQuarterNoteTimeDivision(this.TicksPerQuarterNote);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00021D1B File Offset: 0x0001FF1B
		public override string ToString()
		{
			return string.Format("{0} ticks/qnote", this.TicksPerQuarterNote);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00021D32 File Offset: 0x0001FF32
		public override bool Equals(object obj)
		{
			return this == obj as TicksPerQuarterNoteTimeDivision;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00021D40 File Offset: 0x0001FF40
		public override int GetHashCode()
		{
			return this.TicksPerQuarterNote.GetHashCode();
		}

		// Token: 0x0400094B RID: 2379
		public const short DefaultTicksPerQuarterNote = 96;
	}
}
