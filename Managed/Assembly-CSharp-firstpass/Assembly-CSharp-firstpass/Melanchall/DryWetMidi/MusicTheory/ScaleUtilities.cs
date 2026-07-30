using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200008C RID: 140
	public static class ScaleUtilities
	{
		// Token: 0x060002CB RID: 715 RVA: 0x0000FD22 File Offset: 0x0000DF22
		public static NoteName GetDegree(this Scale scale, ScaleDegree degree)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsInvalidEnumValue<ScaleDegree>("degree", degree);
			ScaleUtilities.ThrowIfDegreeIsOutOfRange(scale, degree);
			return scale.GetStep((int)degree);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000FD48 File Offset: 0x0000DF48
		public static NoteName GetStep(this Scale scale, int step)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNegative("step", step, "Step is negative.");
			return scale.GetNotesNames().Skip(step).First<NoteName>();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000FD76 File Offset: 0x0000DF76
		public static IEnumerable<Note> GetNotes(this Scale scale)
		{
			ThrowIfArgument.IsNull("scale", scale);
			int noteNumber = (int)SevenBitNumber.Values.SkipWhile((SevenBitNumber number) => NoteUtilities.GetNoteName(number) != scale.RootNote).First<SevenBitNumber>();
			yield return Note.Get((SevenBitNumber)((byte)noteNumber));
			for (;;)
			{
				foreach (Interval interval in scale.Intervals)
				{
					noteNumber += interval;
					if (!NoteUtilities.IsNoteNumberValid(noteNumber))
					{
						yield break;
					}
					yield return Note.Get((SevenBitNumber)((byte)noteNumber));
				}
				IEnumerator<Interval> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000FD86 File Offset: 0x0000DF86
		public static IEnumerable<NoteName> GetNotesNames(this Scale scale)
		{
			ThrowIfArgument.IsNull("scale", scale);
			int lastNoteNumber = (int)scale.RootNote;
			yield return scale.RootNote;
			for (;;)
			{
				foreach (Interval interval in scale.Intervals)
				{
					int noteNumber = (lastNoteNumber + interval) % 12;
					yield return (NoteName)noteNumber;
					lastNoteNumber = noteNumber;
				}
				IEnumerator<Interval> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000FD98 File Offset: 0x0000DF98
		public static IEnumerable<Note> GetAscendingNotes(this Scale scale, Note rootNote)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return scale.GetNotes().SkipWhile((Note n) => n != rootNote);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public static IEnumerable<Note> GetDescendingNotes(this Scale scale, Note rootNote)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return new Note[] { rootNote }.Concat(scale.GetNotes().TakeWhile((Note n) => n != rootNote).Reverse<Note>());
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000FE49 File Offset: 0x0000E049
		public static bool IsNoteInScale(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetNotes().Contains(note);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000FE6D File Offset: 0x0000E06D
		public static Note GetNextNote(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetAscendingNotes(note).Skip(1).FirstOrDefault<Note>();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000FE97 File Offset: 0x0000E097
		public static Note GetPreviousNote(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetDescendingNotes(note).Skip(1).FirstOrDefault<Note>();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000FEC1 File Offset: 0x0000E0C1
		private static void ThrowIfDegreeIsOutOfRange(Scale scale, ScaleDegree degree)
		{
			if (degree >= (ScaleDegree)scale.Intervals.Count<Interval>())
			{
				throw new ArgumentOutOfRangeException("degree", degree, "Degree is out of range for the scale.");
			}
		}
	}
}
