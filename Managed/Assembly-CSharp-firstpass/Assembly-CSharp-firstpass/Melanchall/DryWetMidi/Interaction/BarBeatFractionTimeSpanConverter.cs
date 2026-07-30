using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000AF RID: 175
	internal sealed class BarBeatFractionTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0001340C File Offset: 0x0001160C
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new BarBeatFractionTimeSpan();
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			long endTime = time + timeSpan;
			ValueLine<TimeSignature> timeSignature = tempoMap.TimeSignature;
			List<ValueChange<TimeSignature>> list = timeSignature.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < endTime).ToList<ValueChange<TimeSignature>>();
			long num = 0L;
			for (int i = 0; i < list.Count - 1; i++)
			{
				ValueChange<TimeSignature> valueChange = list[i];
				long time2 = list[i + 1].Time;
				int barLength = BarBeatUtilities.GetBarLength(valueChange.Value, ticksPerQuarterNote);
				num += (time2 - valueChange.Time) / (long)barLength;
			}
			ValueChange<TimeSignature> valueChange2 = list.FirstOrDefault<ValueChange<TimeSignature>>();
			long num2 = ((valueChange2 != null) ? valueChange2.Time : time);
			ValueChange<TimeSignature> valueChange3 = list.LastOrDefault<ValueChange<TimeSignature>>();
			long num3 = ((valueChange3 != null) ? valueChange3.Time : time);
			TimeSignature timeSignature2 = timeSignature.AtTime(time);
			TimeSignature timeSignature3 = timeSignature.AtTime(num3);
			long num4;
			long num5;
			double num6;
			BarBeatFractionTimeSpanConverter.CalculateComponents(num2 - time, timeSignature2, ticksPerQuarterNote, out num4, out num5, out num6);
			long num7;
			long num8;
			double num9;
			BarBeatFractionTimeSpanConverter.CalculateComponents(time + timeSpan - num3, timeSignature3, ticksPerQuarterNote, out num7, out num8, out num9);
			num += num4 + num7;
			long num10 = num5 + num8;
			if (num10 > 0L && num5 > 0L && num10 >= (long)timeSignature2.Numerator)
			{
				num += 1L;
				num10 -= (long)timeSignature2.Numerator;
			}
			double num11 = num6 + num9;
			num10 += (long)Math.Truncate(num11);
			num11 -= Math.Truncate(num11);
			return new BarBeatFractionTimeSpan(num, (double)num10 + num11);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000135C8 File Offset: 0x000117C8
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = (BarBeatFractionTimeSpan)timeSpan;
			if (barBeatFractionTimeSpan.Bars == 0L && barBeatFractionTimeSpan.Beats == 0.0)
			{
				return 0L;
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			ValueLine<TimeSignature> timeSignature = tempoMap.TimeSignature;
			double beats = barBeatFractionTimeSpan.Beats;
			long num = barBeatFractionTimeSpan.Bars;
			long num2 = (long)Math.Truncate(beats);
			double num3 = beats - Math.Truncate(beats);
			TimeSignature timeSignature2 = timeSignature.AtTime(time);
			int barLength = BarBeatUtilities.GetBarLength(timeSignature2, ticksPerQuarterNote);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature2, ticksPerQuarterNote);
			long totalTicks = num * (long)barLength + num2 * (long)beatLength + BarBeatFractionTimeSpanConverter.ConvertFractionToTicks(num3, (long)beatLength);
			IEnumerable<ValueChange<TimeSignature>> enumerable = timeSignature.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < time + totalTicks).ToList<ValueChange<TimeSignature>>();
			long num4 = 0L;
			long num5 = 0L;
			ValueChange<TimeSignature> valueChange = enumerable.FirstOrDefault<ValueChange<TimeSignature>>();
			TimeSignature timeSignature3 = ((valueChange != null) ? valueChange.Value : null) ?? timeSignature2;
			long lastTime = ((valueChange != null) ? valueChange.Time : time);
			long num6;
			long num7;
			double num8;
			BarBeatFractionTimeSpanConverter.CalculateComponents(lastTime - time, timeSignature2, ticksPerQuarterNote, out num6, out num7, out num8);
			num -= num6;
			if (num > 0L)
			{
				IEnumerable<ValueChange<TimeSignature>> enumerable2 = timeSignature;
				Func<ValueChange<TimeSignature>, bool> <>9__1;
				Func<ValueChange<TimeSignature>, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (ValueChange<TimeSignature> v) => v.Time > lastTime);
				}
				foreach (ValueChange<TimeSignature> valueChange2 in enumerable2.Where(func).ToList<ValueChange<TimeSignature>>())
				{
					long num9 = valueChange2.Time - lastTime;
					num4 = (long)BarBeatUtilities.GetBarLength(timeSignature3, ticksPerQuarterNote);
					num5 = (long)BarBeatUtilities.GetBeatLength(timeSignature3, ticksPerQuarterNote);
					long num10 = Math.Min(num9 / num4, num);
					num -= num10;
					lastTime += num10 * num4;
					if (num == 0L)
					{
						break;
					}
					timeSignature3 = valueChange2.Value;
				}
				if (num > 0L)
				{
					num4 = (long)BarBeatUtilities.GetBarLength(timeSignature3, ticksPerQuarterNote);
					num5 = (long)BarBeatUtilities.GetBeatLength(timeSignature3, ticksPerQuarterNote);
					lastTime += num * num4;
				}
			}
			if (num2 == num7 && num3 == num8)
			{
				return lastTime - time;
			}
			if (num7 > num2 && num4 > 0L)
			{
				lastTime += -num4 + ((long)timeSignature2.Numerator - num7) * num5;
				num7 = 0L;
			}
			if (num7 < num2)
			{
				num5 = (long)BarBeatUtilities.GetBeatLength(timeSignature.AtTime(lastTime), ticksPerQuarterNote);
				lastTime += (num2 - num7) * num5;
			}
			if (num8 > num3 && num5 > 0L)
			{
				lastTime += -num5 + BarBeatFractionTimeSpanConverter.ConvertFractionToTicks(num3 + 1.0 - num8, num5);
			}
			if (num8 < num3)
			{
				if (num5 == 0L)
				{
					num5 = (long)BarBeatUtilities.GetBeatLength(timeSignature.AtTime(lastTime), ticksPerQuarterNote);
				}
				lastTime += BarBeatFractionTimeSpanConverter.ConvertFractionToTicks(num3 - num8, num5);
			}
			return lastTime - time;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000138FC File Offset: 0x00011AFC
		private static void CalculateComponents(long totalTicks, TimeSignature timeSignature, short ticksPerQuarterNote, out long bars, out long beats, out double fraction)
		{
			int barLength = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
			long num;
			bars = Math.DivRem(totalTicks, (long)barLength, out num);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
			beats = Math.DivRem(num, (long)beatLength, out num);
			fraction = (double)num / (double)beatLength;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001393A File Offset: 0x00011B3A
		private static long ConvertFractionToTicks(double fraction, long beatLength)
		{
			return MathUtilities.RoundToLong((double)beatLength * fraction);
		}
	}
}
