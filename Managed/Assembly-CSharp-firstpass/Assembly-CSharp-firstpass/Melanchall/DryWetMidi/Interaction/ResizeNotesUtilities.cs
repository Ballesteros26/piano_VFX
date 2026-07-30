using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000A6 RID: 166
	public static class ResizeNotesUtilities
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00012294 File Offset: 0x00010494
		public static void ResizeNotes(this IEnumerable<Note> notes, ITimeSpan length, TimeSpanType distanceCalculationType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("distanceCalculationType", distanceCalculationType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (distanceCalculationType == TimeSpanType.BarBeatTicks || distanceCalculationType == TimeSpanType.BarBeatFraction)
			{
				throw new ArgumentException("Bar/beat distance calculation type is not supported.", "distanceCalculationType");
			}
			IEnumerable<Note> enumerable = notes.Where((Note n) => n != null);
			if (!enumerable.Any<Note>())
			{
				return;
			}
			long num = long.MaxValue;
			long num2 = 0L;
			foreach (Note note in enumerable)
			{
				long time = note.Time;
				long num3 = time + note.Length;
				num = Math.Min(num, time);
				num2 = Math.Max(num2, num3);
			}
			ITimeSpan timeSpan = LengthConverter.ConvertTo(num2 - num, distanceCalculationType, num, tempoMap);
			double num4 = TimeSpanUtilities.Divide(LengthConverter.ConvertTo(length, distanceCalculationType, num, tempoMap), timeSpan);
			ITimeSpan timeSpan2 = TimeConverter.ConvertTo(num, distanceCalculationType, tempoMap);
			ResizeNotesUtilities.ResizeNotesByRatio(enumerable, num4, distanceCalculationType, tempoMap, timeSpan2);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000123B8 File Offset: 0x000105B8
		public static void ResizeNotes(this IEnumerable<Note> notes, double ratio, TimeSpanType distanceCalculationType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNegative("ratio", ratio, "Ratio is negative");
			ThrowIfArgument.IsInvalidEnumValue<TimeSpanType>("distanceCalculationType", distanceCalculationType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (distanceCalculationType == TimeSpanType.BarBeatTicks || distanceCalculationType == TimeSpanType.BarBeatFraction)
			{
				throw new ArgumentException("BarBeat distance calculation type is not supported.", "distanceCalculationType");
			}
			IEnumerable<Note> enumerable = notes.Where((Note n) => n != null);
			if (!enumerable.Any<Note>())
			{
				return;
			}
			ITimeSpan timeSpan = TimeConverter.ConvertTo(enumerable.Select((Note n) => n.Time).Min(), distanceCalculationType, tempoMap);
			ResizeNotesUtilities.ResizeNotesByRatio(enumerable, ratio, distanceCalculationType, tempoMap, timeSpan);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001247C File Offset: 0x0001067C
		private static void ResizeNotesByRatio(IEnumerable<Note> notes, double ratio, TimeSpanType distanceCalculationType, TempoMap tempoMap, ITimeSpan startTime)
		{
			foreach (Note note in notes)
			{
				ITimeSpan timeSpan = note.LengthAs(distanceCalculationType, tempoMap);
				ITimeSpan timeSpan2 = note.TimeAs(distanceCalculationType, tempoMap).Subtract(startTime, TimeSpanMode.TimeTime).Multiply(ratio);
				note.Time = TimeConverter.ConvertFrom(startTime.Add(timeSpan2, TimeSpanMode.TimeLength), tempoMap);
				ITimeSpan timeSpan3 = timeSpan.Multiply(ratio);
				note.Length = LengthConverter.ConvertFrom(timeSpan3, note.Time, tempoMap);
			}
		}
	}
}
