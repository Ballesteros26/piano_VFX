using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000079 RID: 121
	public sealed class ChordProgression
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000D06F File Offset: 0x0000B26F
		public ChordProgression(IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("chords", chords);
			ThrowIfArgument.ContainsNull<Chord>("chords", chords);
			this.Chords = chords;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D094 File Offset: 0x0000B294
		public ChordProgression(params Chord[] chords)
			: this(chords)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D09D File Offset: 0x0000B29D
		public IEnumerable<Chord> Chords { get; }

		// Token: 0x0600024F RID: 591 RVA: 0x0000D0A5 File Offset: 0x0000B2A5
		public static bool TryParse(string input, Scale scale, out ChordProgression chordProgression)
		{
			return ParsingUtilities.TryParse<ChordProgression>(input, ChordProgression.GetParsing(input, scale), out chordProgression);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D0B5 File Offset: 0x0000B2B5
		public static ChordProgression Parse(string input, Scale scale)
		{
			return ParsingUtilities.Parse<ChordProgression>(input, ChordProgression.GetParsing(input, scale));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		private static Parsing<ChordProgression> GetParsing(string input, Scale scale)
		{
			ChordProgression chordProgression;
			ParsingResult result = ChordProgressionParser.TryParse(input, scale, out chordProgression);
			return delegate(string i, out ChordProgression cp)
			{
				cp = chordProgression;
				return result;
			};
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D0F6 File Offset: 0x0000B2F6
		public static bool operator ==(ChordProgression chordProgression1, ChordProgression chordProgression2)
		{
			return chordProgression1 == chordProgression2 || (chordProgression1 != null && chordProgression2 != null && chordProgression1.Chords.SequenceEqual(chordProgression2.Chords));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000D117 File Offset: 0x0000B317
		public static bool operator !=(ChordProgression chordProgression1, ChordProgression chordProgression2)
		{
			return !(chordProgression1 == chordProgression2);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D123 File Offset: 0x0000B323
		public override string ToString()
		{
			return string.Join<Chord>("; ", this.Chords);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D135 File Offset: 0x0000B335
		public override bool Equals(object obj)
		{
			return this == obj as ChordProgression;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D144 File Offset: 0x0000B344
		public override int GetHashCode()
		{
			int num = 17;
			foreach (Chord chord in this.Chords)
			{
				num = num * 23 + chord.GetHashCode();
			}
			return num;
		}
	}
}
