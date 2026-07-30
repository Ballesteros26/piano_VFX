using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000074 RID: 116
	public sealed class Chord
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000B377 File Offset: 0x00009577
		public Chord(ICollection<NoteName> notesNames)
		{
			ThrowIfArgument.IsNull("notesNames", notesNames);
			ThrowIfArgument.ContainsInvalidEnumValue<NoteName>("notesNames", notesNames);
			ThrowIfArgument.IsEmptyCollection<NoteName>("notesNames", notesNames, "Notes names collection is empty.");
			this.NotesNames = notesNames;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B3AC File Offset: 0x000095AC
		public Chord(NoteName rootNoteName, params NoteName[] notesNamesAboveRoot)
			: this(new NoteName[] { rootNoteName }.Concat(notesNamesAboveRoot ?? Enumerable.Empty<NoteName>()).ToArray<NoteName>())
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public Chord(NoteName rootNoteName, IEnumerable<Interval> intervalsFromRoot)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("rootNoteName", rootNoteName);
			ThrowIfArgument.IsNull("intervalsFromRoot", intervalsFromRoot);
			this.NotesNames = (from i in new Interval[] { Interval.Zero }.Concat(intervalsFromRoot)
				where i != null
				orderby i.HalfSteps
				select rootNoteName.Transpose(i)).ToArray<NoteName>();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B493 File Offset: 0x00009693
		public Chord(NoteName rootNoteName, params Interval[] intervalsFromRoot)
			: this(rootNoteName, intervalsFromRoot)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B49D File Offset: 0x0000969D
		public ICollection<NoteName> NotesNames { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000B4A5 File Offset: 0x000096A5
		public NoteName RootNoteName
		{
			get
			{
				return this.NotesNames.First<NoteName>();
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public IReadOnlyCollection<string> GetNames()
		{
			if (this._chordNames != null)
			{
				return this._chordNames;
			}
			IList<string> chordNames = ChordsNamesTable.GetChordNames(this.NotesNames.ToArray<NoteName>());
			return this._chordNames = new ReadOnlyCollection<string>(chordNames);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public static bool TryParse(string input, out Chord chord)
		{
			return ParsingUtilities.TryParse<Chord>(input, new Parsing<Chord>(ChordParser.TryParse), out chord);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B505 File Offset: 0x00009705
		public static Chord Parse(string input)
		{
			return ParsingUtilities.Parse<Chord>(input, new Parsing<Chord>(ChordParser.TryParse));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B51C File Offset: 0x0000971C
		public static Chord GetByTriad(NoteName rootNoteName, ChordQuality chordQuality, params Interval[] intervalsFromRoot)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("rootNoteName", rootNoteName);
			ThrowIfArgument.IsInvalidEnumValue<ChordQuality>("chordQuality", chordQuality);
			ThrowIfArgument.IsNull("intervalsFromRoot", intervalsFromRoot);
			IntervalDefinition[] array = Chord.IntervalsByQuality[chordQuality];
			return new Chord(rootNoteName, array.Select((IntervalDefinition i) => Interval.FromDefinition(i)).Concat(intervalsFromRoot));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B587 File Offset: 0x00009787
		public static bool operator ==(Chord chord1, Chord chord2)
		{
			return chord1 == chord2 || (chord1 != null && chord2 != null && chord1.NotesNames.SequenceEqual(chord2.NotesNames));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public static bool operator !=(Chord chord1, Chord chord2)
		{
			return !(chord1 == chord2);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B5B4 File Offset: 0x000097B4
		public override string ToString()
		{
			return string.Join(" ", this.NotesNames.Select((NoteName n) => n.ToString().Replace("Sharp", "#")));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B5EA File Offset: 0x000097EA
		public override bool Equals(object obj)
		{
			return this == obj as Chord;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public override int GetHashCode()
		{
			int num = 17;
			foreach (NoteName noteName in this.NotesNames)
			{
				num = num * 23 + noteName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B658 File Offset: 0x00009858
		// Note: this type is marked as 'beforefieldinit'.
		static Chord()
		{
			Dictionary<ChordQuality, IntervalDefinition[]> dictionary = new Dictionary<ChordQuality, IntervalDefinition[]>();
			dictionary[ChordQuality.Major] = new IntervalDefinition[]
			{
				new IntervalDefinition(3, IntervalQuality.Major),
				new IntervalDefinition(5, IntervalQuality.Perfect)
			};
			dictionary[ChordQuality.Minor] = new IntervalDefinition[]
			{
				new IntervalDefinition(3, IntervalQuality.Minor),
				new IntervalDefinition(5, IntervalQuality.Perfect)
			};
			dictionary[ChordQuality.Augmented] = new IntervalDefinition[]
			{
				new IntervalDefinition(3, IntervalQuality.Major),
				new IntervalDefinition(5, IntervalQuality.Augmented)
			};
			dictionary[ChordQuality.Diminished] = new IntervalDefinition[]
			{
				new IntervalDefinition(3, IntervalQuality.Minor),
				new IntervalDefinition(5, IntervalQuality.Diminished)
			};
			Chord.IntervalsByQuality = dictionary;
		}

		// Token: 0x040004D3 RID: 1235
		private static readonly Dictionary<ChordQuality, IntervalDefinition[]> IntervalsByQuality;

		// Token: 0x040004D4 RID: 1236
		private IReadOnlyCollection<string> _chordNames;
	}
}
