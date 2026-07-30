using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000CB RID: 203
	public static class BarBeatUtilities
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000171A4 File Offset: 0x000153A4
		public static int GetBarLength(long bars, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			Tuple<TimeSignature, short> timeSignatureAndTicksPerQuarterNote = BarBeatUtilities.GetTimeSignatureAndTicksPerQuarterNote(bars, tempoMap);
			return BarBeatUtilities.GetBarLength(timeSignatureAndTicksPerQuarterNote.Item1, timeSignatureAndTicksPerQuarterNote.Item2);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000171E8 File Offset: 0x000153E8
		public static int GetBeatLength(long bars, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			Tuple<TimeSignature, short> timeSignatureAndTicksPerQuarterNote = BarBeatUtilities.GetTimeSignatureAndTicksPerQuarterNote(bars, tempoMap);
			return BarBeatUtilities.GetBeatLength(timeSignatureAndTicksPerQuarterNote.Item1, timeSignatureAndTicksPerQuarterNote.Item2);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001722C File Offset: 0x0001542C
		internal static int GetBarLength(TimeSignature timeSignature, short ticksPerQuarterNote)
		{
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
			return timeSignature.Numerator * beatLength;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017249 File Offset: 0x00015449
		internal static int GetBeatLength(TimeSignature timeSignature, short ticksPerQuarterNote)
		{
			return (int)(4 * ticksPerQuarterNote) / timeSignature.Denominator;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017258 File Offset: 0x00015458
		private static Tuple<TimeSignature, short> GetTimeSignatureAndTicksPerQuarterNote(long bars, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division of the tempo map is not supported.", "tempoMap");
			}
			long num = TimeConverter.ConvertFrom(new BarBeatTicksTimeSpan(bars), tempoMap);
			TimeSignature timeSignature = tempoMap.TimeSignature.AtTime(num);
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			return Tuple.Create<TimeSignature, short>(timeSignature, ticksPerQuarterNote);
		}
	}
}
