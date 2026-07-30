using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000077 RID: 119
	public static class ChordUtilities
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000C150 File Offset: 0x0000A350
		public static IEnumerable<Interval> GetIntervalsFromRootNote(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return ChordUtilities.GetIntervalsFromRootNote(chord.NotesNames);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C168 File Offset: 0x0000A368
		public static IEnumerable<Interval> GetIntervalsBetweenNotes(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return (from i in ChordUtilities.GetIntervals(chord)
				select Interval.FromHalfSteps((int)i)).ToList<Interval>();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		public static Note ResolveRootNote(this Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return octave.GetNote(chord.RootNoteName);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public static IEnumerable<Note> ResolveNotes(this Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			Note rootNote = chord.ResolveRootNote(octave);
			List<Note> list = new List<Note>();
			list.Add(rootNote);
			list.AddRange(from i in chord.GetIntervalsFromRootNote()
				select rootNote + i);
			return list;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C22C File Offset: 0x0000A42C
		public static IEnumerable<Chord> GetInversions(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			foreach (NoteName[] array in MathUtilities.GetPermutations<NoteName>(chord.NotesNames.ToArray<NoteName>()))
			{
				if (array[0] != chord.RootNoteName)
				{
					yield return new Chord(array.ToArray<NoteName>());
				}
			}
			IEnumerator<NoteName[]> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C23C File Offset: 0x0000A43C
		internal static IEnumerable<Interval> GetIntervalsFromRootNote(ICollection<NoteName> notesNames)
		{
			SevenBitNumber sevenBitNumber = SevenBitNumber.MinValue;
			List<Interval> list = new List<Interval>();
			foreach (SevenBitNumber sevenBitNumber2 in ChordUtilities.GetIntervals(notesNames))
			{
				if (sevenBitNumber + sevenBitNumber2 > SevenBitNumber.MaxValue)
				{
					throw new InvalidOperationException(string.Format("Some interval(s) are greater than {0}.", SevenBitNumber.MaxValue));
				}
				sevenBitNumber = (SevenBitNumber)(sevenBitNumber + sevenBitNumber2);
				list.Add(Interval.GetUp(sevenBitNumber));
			}
			return list;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		private static IEnumerable<SevenBitNumber> GetIntervals(Chord chord)
		{
			return ChordUtilities.GetIntervals(chord.NotesNames);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C2F1 File Offset: 0x0000A4F1
		private static IEnumerable<SevenBitNumber> GetIntervals(ICollection<NoteName> notesNames)
		{
			int num = (int)notesNames.First<NoteName>();
			foreach (NoteName noteName in notesNames.Skip(1))
			{
				int num2 = noteName - (NoteName)num;
				if (num2 <= 0)
				{
					num2 += 12;
				}
				yield return (SevenBitNumber)((byte)num2);
				num = (int)noteName;
			}
			IEnumerator<NoteName> enumerator = null;
			yield break;
			yield break;
		}
	}
}
