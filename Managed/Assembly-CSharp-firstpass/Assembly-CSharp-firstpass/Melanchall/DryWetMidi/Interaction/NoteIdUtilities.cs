using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000D8 RID: 216
	public static class NoteIdUtilities
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x00017FCB File Offset: 0x000161CB
		public static NoteId GetNoteId(this Note note)
		{
			ThrowIfArgument.IsNull("note", note);
			return new NoteId(note.Channel, note.NoteNumber);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017FE9 File Offset: 0x000161E9
		public static NoteId GetNoteId(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return new NoteId(noteEvent.Channel, noteEvent.NoteNumber);
		}
	}
}
