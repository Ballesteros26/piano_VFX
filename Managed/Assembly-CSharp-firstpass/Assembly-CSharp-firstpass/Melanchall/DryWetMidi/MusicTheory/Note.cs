using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000081 RID: 129
	public sealed class Note : IComparable<Note>
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000DE71 File Offset: 0x0000C071
		private Note(SevenBitNumber noteNumber)
		{
			this.NoteNumber = noteNumber;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000DE80 File Offset: 0x0000C080
		public SevenBitNumber NoteNumber { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000DE88 File Offset: 0x0000C088
		public NoteName NoteName
		{
			get
			{
				return NoteUtilities.GetNoteName(this.NoteNumber);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000DE95 File Offset: 0x0000C095
		public int Octave
		{
			get
			{
				return NoteUtilities.GetNoteOctave(this.NoteNumber);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DEA2 File Offset: 0x0000C0A2
		public Note Transpose(Interval interval)
		{
			return Note.Get((SevenBitNumber)((byte)((int)this.NoteNumber + interval.HalfSteps)));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DEC4 File Offset: 0x0000C0C4
		public static Note Get(SevenBitNumber noteNumber)
		{
			Note note;
			if (!Note.Cache.TryGetValue(noteNumber, out note))
			{
				Note.Cache.Add(noteNumber, note = new Note(noteNumber));
			}
			return note;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		public static Note Get(NoteName noteName, int octave)
		{
			return Note.Get(NoteUtilities.GetNoteNumber(noteName, octave));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DF02 File Offset: 0x0000C102
		public static bool TryParse(string input, out Note note)
		{
			return ParsingUtilities.TryParse<Note>(input, new Parsing<Note>(NoteParser.TryParse), out note);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DF17 File Offset: 0x0000C117
		public static Note Parse(string input)
		{
			return ParsingUtilities.Parse<Note>(input, new Parsing<Note>(NoteParser.TryParse));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000DF2B File Offset: 0x0000C12B
		public static bool operator ==(Note note1, Note note2)
		{
			return note1 == note2 || (note1 != null && note2 != null && note1.NoteNumber == note2.NoteNumber);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DF53 File Offset: 0x0000C153
		public static bool operator !=(Note note1, Note note2)
		{
			return !(note1 == note2);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DF5F File Offset: 0x0000C15F
		public static Note operator +(Note note, int halfSteps)
		{
			ThrowIfArgument.IsNull("note", note);
			return note.Transpose(Interval.FromHalfSteps(halfSteps));
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000DF78 File Offset: 0x0000C178
		public static Note operator -(Note note, int halfSteps)
		{
			return note + -halfSteps;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000DF84 File Offset: 0x0000C184
		public int CompareTo(Note other)
		{
			return this.NoteNumber.CompareTo(other.NoteNumber);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		public override string ToString()
		{
			return string.Format("{0}{1}", this.NoteName.ToString().Replace("Sharp", "#"), this.Octave);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public override bool Equals(object obj)
		{
			return this == obj as Note;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		public override int GetHashCode()
		{
			return this.NoteNumber.GetHashCode();
		}

		// Token: 0x0400053B RID: 1339
		internal const string SharpLongString = "Sharp";

		// Token: 0x0400053C RID: 1340
		internal const string SharpShortString = "#";

		// Token: 0x0400053D RID: 1341
		internal const string FlatLongString = "Flat";

		// Token: 0x0400053E RID: 1342
		internal const string FlatShortString = "b";

		// Token: 0x0400053F RID: 1343
		private static readonly Dictionary<SevenBitNumber, Note> Cache = new Dictionary<SevenBitNumber, Note>();
	}
}
