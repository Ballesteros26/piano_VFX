using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B6 RID: 182
	internal sealed class MusicalTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x000144E0 File Offset: 0x000126E0
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new MusicalTimeSpan();
			}
			Tuple<long, long> tuple = MathUtilities.SolveDiophantineEquation((long)(4 * ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote), -timeSpan);
			return new MusicalTimeSpan(Math.Abs(tuple.Item1), Math.Abs(tuple.Item2), true);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00014548 File Offset: 0x00012748
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			MusicalTimeSpan musicalTimeSpan = (MusicalTimeSpan)timeSpan;
			if (musicalTimeSpan.Numerator == 0L)
			{
				return 0L;
			}
			return MathUtilities.RoundToLong(4.0 * (double)musicalTimeSpan.Numerator * (double)ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote / (double)musicalTimeSpan.Denominator);
		}
	}
}
