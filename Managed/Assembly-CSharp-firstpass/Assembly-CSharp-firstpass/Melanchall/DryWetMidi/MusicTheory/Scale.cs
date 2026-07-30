using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000088 RID: 136
	public sealed class Scale
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000EE20 File Offset: 0x0000D020
		public Scale(IEnumerable<Interval> intervals, NoteName rootNote)
		{
			ThrowIfArgument.IsNull("intervals", intervals);
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("rootNote", rootNote);
			this.Intervals = intervals;
			this.RootNote = rootNote;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public IEnumerable<Interval> Intervals { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000EE54 File Offset: 0x0000D054
		public NoteName RootNote { get; }

		// Token: 0x060002BF RID: 703 RVA: 0x0000EE5C File Offset: 0x0000D05C
		public static bool TryParse(string input, out Scale scale)
		{
			return ParsingUtilities.TryParse<Scale>(input, new Parsing<Scale>(ScaleParser.TryParse), out scale);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000EE71 File Offset: 0x0000D071
		public static Scale Parse(string input)
		{
			return ParsingUtilities.Parse<Scale>(input, new Parsing<Scale>(ScaleParser.TryParse));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000EE85 File Offset: 0x0000D085
		public static bool operator ==(Scale scale1, Scale scale2)
		{
			return scale1 == scale2 || (scale1 != null && scale2 != null && scale1.RootNote == scale2.RootNote && scale1.Intervals.SequenceEqual(scale2.Intervals));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		public static bool operator !=(Scale scale1, Scale scale2)
		{
			return !(scale1 == scale2);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
		public override string ToString()
		{
			return string.Format("{0} {1}", this.RootNote, string.Join<Interval>(" ", this.Intervals));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
		public override bool Equals(object obj)
		{
			return this == obj as Scale;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public override int GetHashCode()
		{
			return (17 * 23 + this.RootNote.GetHashCode()) * 23 + this.Intervals.GetHashCode();
		}
	}
}
