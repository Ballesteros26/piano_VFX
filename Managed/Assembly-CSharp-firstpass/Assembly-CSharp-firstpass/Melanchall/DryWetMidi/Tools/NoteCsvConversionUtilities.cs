using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000030 RID: 48
	internal static class NoteCsvConversionUtilities
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000073D5 File Offset: 0x000055D5
		public static object FormatNoteNumber(SevenBitNumber noteNumber, NoteNumberFormat noteNumberFormat)
		{
			if (noteNumberFormat == NoteNumberFormat.NoteNumber)
			{
				return noteNumber;
			}
			if (noteNumberFormat != NoteNumberFormat.Letter)
			{
				return null;
			}
			return Note.Get(noteNumber);
		}
	}
}
