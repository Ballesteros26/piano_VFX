using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200019D RID: 413
	public static class NoteEventUtilities
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x00021F64 File Offset: 0x00020164
		public static NoteName GetNoteName(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return NoteUtilities.GetNoteName(noteEvent.NoteNumber);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00021F7C File Offset: 0x0002017C
		public static int GetNoteOctave(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return NoteUtilities.GetNoteOctave(noteEvent.NoteNumber);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00021F94 File Offset: 0x00020194
		public static void SetNoteNumber(this NoteEvent noteEvent, NoteName noteName, int octave)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			noteEvent.NoteNumber = NoteUtilities.GetNoteNumber(noteName, octave);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00021FB0 File Offset: 0x000201B0
		public static bool IsNoteOnCorrespondToNoteOff(NoteOnEvent noteOnEvent, NoteOffEvent noteOffEvent)
		{
			ThrowIfArgument.IsNull("noteOnEvent", noteOnEvent);
			ThrowIfArgument.IsNull("noteOffEvent", noteOffEvent);
			return noteOnEvent.Channel == noteOffEvent.Channel && noteOnEvent.NoteNumber == noteOffEvent.NoteNumber;
		}
	}
}
