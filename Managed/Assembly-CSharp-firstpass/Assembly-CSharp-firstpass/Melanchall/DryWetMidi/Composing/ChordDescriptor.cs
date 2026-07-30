using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B1 RID: 433
	public sealed class ChordDescriptor
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x00022B13 File Offset: 0x00020D13
		public ChordDescriptor(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, SevenBitNumber velocity, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			this.Notes = notes;
			this.Velocity = velocity;
			this.Length = length;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00022B46 File Offset: 0x00020D46
		public IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> Notes { get; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00022B4E File Offset: 0x00020D4E
		public SevenBitNumber Velocity { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00022B56 File Offset: 0x00020D56
		public ITimeSpan Length { get; }

		// Token: 0x06000A58 RID: 2648 RVA: 0x00022B60 File Offset: 0x00020D60
		public static bool operator ==(ChordDescriptor chordDescriptor1, ChordDescriptor chordDescriptor2)
		{
			return chordDescriptor1 == chordDescriptor2 || (chordDescriptor1 != null && chordDescriptor2 != null && (chordDescriptor1.Notes.SequenceEqual(chordDescriptor2.Notes) && chordDescriptor1.Velocity == chordDescriptor2.Velocity) && chordDescriptor1.Length.Equals(chordDescriptor2.Length));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00022BB9 File Offset: 0x00020DB9
		public static bool operator !=(ChordDescriptor chordDescriptor1, ChordDescriptor chordDescriptor2)
		{
			return !(chordDescriptor1 == chordDescriptor2);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00022BC5 File Offset: 0x00020DC5
		public override string ToString()
		{
			return string.Format("{0} [{1}]: {2}", string.Join<Melanchall.DryWetMidi.MusicTheory.Note>(" ", this.Notes), this.Velocity, this.Length);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00022BF2 File Offset: 0x00020DF2
		public override bool Equals(object obj)
		{
			return this == obj as ChordDescriptor;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00022C00 File Offset: 0x00020E00
		public override int GetHashCode()
		{
			return ((17 * 23 + this.Notes.GetHashCode()) * 23 + this.Velocity.GetHashCode()) * 23 + this.Length.GetHashCode();
		}
	}
}
