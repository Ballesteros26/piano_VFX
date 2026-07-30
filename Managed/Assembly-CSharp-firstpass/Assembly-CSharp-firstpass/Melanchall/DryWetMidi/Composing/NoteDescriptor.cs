using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B2 RID: 434
	public sealed class NoteDescriptor
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x00022C45 File Offset: 0x00020E45
		public NoteDescriptor(Melanchall.DryWetMidi.MusicTheory.Note note, SevenBitNumber velocity, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("length", length);
			this.Note = note;
			this.Velocity = velocity;
			this.Length = length;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00022C78 File Offset: 0x00020E78
		public Melanchall.DryWetMidi.MusicTheory.Note Note { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00022C80 File Offset: 0x00020E80
		public SevenBitNumber Velocity { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00022C88 File Offset: 0x00020E88
		public ITimeSpan Length { get; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x00022C90 File Offset: 0x00020E90
		public static bool operator ==(NoteDescriptor noteDescriptor1, NoteDescriptor noteDescriptor2)
		{
			return noteDescriptor1 == noteDescriptor2 || (noteDescriptor1 != null && noteDescriptor2 != null && (noteDescriptor1.Note == noteDescriptor2.Note && noteDescriptor1.Velocity == noteDescriptor2.Velocity) && noteDescriptor1.Length.Equals(noteDescriptor2.Length));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00022CE9 File Offset: 0x00020EE9
		public static bool operator !=(NoteDescriptor noteDescriptor1, NoteDescriptor noteDescriptor2)
		{
			return !(noteDescriptor1 == noteDescriptor2);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00022CF5 File Offset: 0x00020EF5
		public override string ToString()
		{
			return string.Format("{0} [{1}]: {2}", this.Note, this.Velocity, this.Length);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00022D18 File Offset: 0x00020F18
		public override bool Equals(object obj)
		{
			return this == obj as NoteDescriptor;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00022D28 File Offset: 0x00020F28
		public override int GetHashCode()
		{
			return ((17 * 23 + this.Note.GetHashCode()) * 23 + this.Velocity.GetHashCode()) * 23 + this.Length.GetHashCode();
		}
	}
}
