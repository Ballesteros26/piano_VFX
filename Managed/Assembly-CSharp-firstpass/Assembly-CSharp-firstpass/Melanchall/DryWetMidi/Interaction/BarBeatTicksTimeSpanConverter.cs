using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B0 RID: 176
	internal sealed class BarBeatTicksTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00013948 File Offset: 0x00011B48
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new BarBeatTicksTimeSpan();
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
			long num6;
			BarBeatTicksTimeSpanConverter.CalculateComponents(num2 - time, timeSignature2, ticksPerQuarterNote, out num4, out num5, out num6);
			long num7;
			long num8;
			long num9;
			BarBeatTicksTimeSpanConverter.CalculateComponents(time + timeSpan - num3, timeSignature3, ticksPerQuarterNote, out num7, out num8, out num9);
			num += num4 + num7;
			long num10 = num5 + num8;
			if (num10 > 0L && num5 > 0L && num10 >= (long)timeSignature2.Numerator)
			{
				num += 1L;
				num10 -= (long)timeSignature2.Numerator;
			}
			long num11 = num6 + num9;
			if (num11 > 0L)
			{
				int beatLength = BarBeatUtilities.GetBeatLength(timeSignature2, ticksPerQuarterNote);
				if (num6 > 0L && num11 >= (long)beatLength)
				{
					num10 += 1L;
					num11 -= (long)beatLength;
				}
			}
			return new BarBeatTicksTimeSpan(num, num10, num11);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013B14 File Offset: 0x00011D14
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = (BarBeatTicksTimeSpan)timeSpan;
			if (barBeatTicksTimeSpan.Bars == 0L && barBeatTicksTimeSpan.Beats == 0L && barBeatTicksTimeSpan.Ticks == 0L)
			{
				return 0L;
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			ValueLine<TimeSignature> timeSignature = tempoMap.TimeSignature;
			long num = barBeatTicksTimeSpan.Bars;
			long beats = barBeatTicksTimeSpan.Beats;
			long ticks = barBeatTicksTimeSpan.Ticks;
			TimeSignature timeSignature2 = timeSignature.AtTime(time);
			int barLength = BarBeatUtilities.GetBarLength(timeSignature2, ticksPerQuarterNote);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature2, ticksPerQuarterNote);
			long totalTicks = num * (long)barLength + beats * (long)beatLength + ticks;
			IEnumerable<ValueChange<TimeSignature>> enumerable = timeSignature.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < time + totalTicks).ToList<ValueChange<TimeSignature>>();
			long num2 = 0L;
			long num3 = 0L;
			ValueChange<TimeSignature> valueChange = enumerable.FirstOrDefault<ValueChange<TimeSignature>>();
			TimeSignature timeSignature3 = ((valueChange != null) ? valueChange.Value : null) ?? timeSignature2;
			long lastTime = ((valueChange != null) ? valueChange.Time : time);
			long num4;
			long num5;
			long num6;
			BarBeatTicksTimeSpanConverter.CalculateComponents(lastTime - time, timeSignature2, ticksPerQuarterNote, out num4, out num5, out num6);
			num -= num4;
			IEnumerable<ValueChange<TimeSignature>> enumerable2 = timeSignature;
			Func<ValueChange<TimeSignature>, bool> <>9__1;
			Func<ValueChange<TimeSignature>, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (ValueChange<TimeSignature> v) => v.Time > lastTime);
			}
			foreach (ValueChange<TimeSignature> valueChange2 in enumerable2.Where(func).ToList<ValueChange<TimeSignature>>())
			{
				long num7 = valueChange2.Time - lastTime;
				num2 = (long)BarBeatUtilities.GetBarLength(timeSignature3, ticksPerQuarterNote);
				num3 = (long)BarBeatUtilities.GetBeatLength(timeSignature3, ticksPerQuarterNote);
				long num8 = Math.Min(num7 / num2, num);
				num -= num8;
				lastTime += num8 * num2;
				if (num == 0L)
				{
					break;
				}
				timeSignature3 = valueChange2.Value;
			}
			if (num > 0L)
			{
				num2 = (long)BarBeatUtilities.GetBarLength(timeSignature3, ticksPerQuarterNote);
				num3 = (long)BarBeatUtilities.GetBeatLength(timeSignature3, ticksPerQuarterNote);
				lastTime += num * num2;
			}
			if (beats == num5 && ticks == num6)
			{
				return lastTime - time;
			}
			if (num5 > beats && num2 > 0L)
			{
				lastTime += -num2 + ((long)timeSignature2.Numerator - num5) * num3;
				num5 = 0L;
			}
			if (num5 < beats)
			{
				num3 = (long)BarBeatUtilities.GetBeatLength(timeSignature.AtTime(lastTime), ticksPerQuarterNote);
				lastTime += (beats - num5) * num3;
			}
			if (num6 > ticks && num3 > 0L)
			{
				lastTime += -num3 + (long)beatLength - num6;
				num6 = 0L;
			}
			if (num6 < ticks)
			{
				lastTime += ticks - num6;
			}
			return lastTime - time;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013E04 File Offset: 0x00012004
		private static void CalculateComponents(long totalTicks, TimeSignature timeSignature, short ticksPerQuarterNote, out long bars, out long beats, out long ticks)
		{
			int barLength = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
			bars = Math.DivRem(totalTicks, (long)barLength, out ticks);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
			beats = Math.DivRem(ticks, (long)beatLength, out ticks);
		}
	}
}
