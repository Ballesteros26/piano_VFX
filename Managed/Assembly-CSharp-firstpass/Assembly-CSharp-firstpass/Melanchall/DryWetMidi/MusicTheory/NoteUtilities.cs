using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000085 RID: 133
	public static class NoteUtilities
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000E29C File Offset: 0x0000C49C
		public static NoteName Transpose(this NoteName noteName, Interval interval)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("noteName", noteName);
			ThrowIfArgument.IsNull("interval", interval);
			int num = (int)((noteName + interval) % (NoteName)12);
			if (num < 0)
			{
				num += 12;
			}
			return (NoteName)num;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000E2D5 File Offset: 0x0000C4D5
		public static NoteName GetNoteName(SevenBitNumber noteNumber)
		{
			return (NoteName)(noteNumber % 12);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public static int GetNoteOctave(SevenBitNumber noteNumber)
		{
			return (int)(noteNumber / 12 - 1);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E2ED File Offset: 0x0000C4ED
		public static SevenBitNumber GetNoteNumber(NoteName noteName, int octave)
		{
			ThrowIfArgument.IsInvalidEnumValue<NoteName>("noteName", noteName);
			int num = NoteUtilities.CalculateNoteNumber(noteName, octave);
			if (!NoteUtilities.IsNoteNumberValid(num))
			{
				throw new ArgumentException("Note number is out of range for the specified note name and octave.", "octave");
			}
			return (SevenBitNumber)((byte)num);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E31F File Offset: 0x0000C51F
		internal static bool IsNoteValid(NoteName noteName, int octave)
		{
			return NoteUtilities.IsNoteNumberValid(NoteUtilities.CalculateNoteNumber(noteName, octave));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E32D File Offset: 0x0000C52D
		internal static bool IsNoteNumberValid(int noteNumber)
		{
			return noteNumber >= (int)SevenBitNumber.MinValue && noteNumber <= (int)SevenBitNumber.MaxValue;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E34E File Offset: 0x0000C54E
		private static int CalculateNoteNumber(NoteName noteName, int octave)
		{
			return (int)((octave + 1) * 12 + noteName);
		}

		// Token: 0x04000559 RID: 1369
		private const int OctaveOffset = 1;
	}
}
