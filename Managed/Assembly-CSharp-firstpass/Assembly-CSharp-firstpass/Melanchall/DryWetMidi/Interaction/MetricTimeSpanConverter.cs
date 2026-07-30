using System;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000B4 RID: 180
	internal sealed class MetricTimeSpanConverter : ITimeSpanConverter
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x000142D0 File Offset: 0x000124D0
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			if (tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new MetricTimeSpan();
			}
			MetricTimeSpan metricTimeSpan = MetricTimeSpanConverter.TicksToMetricTimeSpan(time, tempoMap);
			return MetricTimeSpanConverter.TicksToMetricTimeSpan(time + timeSpan, tempoMap) - metricTimeSpan;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014320 File Offset: 0x00012520
		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			if (tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			MetricTimeSpan metricTimeSpan = (MetricTimeSpan)timeSpan;
			if (metricTimeSpan == TimeSpan.Zero)
			{
				return 0L;
			}
			return MetricTimeSpanConverter.MetricTimeSpanToTicks(MetricTimeSpanConverter.TicksToMetricTimeSpan(time, tempoMap) + metricTimeSpan, tempoMap) - time;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014384 File Offset: 0x00012584
		private static MetricTimeSpan TicksToMetricTimeSpan(long timeSpan, TempoMap tempoMap)
		{
			if (timeSpan == 0L)
			{
				return new MetricTimeSpan();
			}
			MetricTempoMapValuesCache valuesCache = tempoMap.GetValuesCache<MetricTempoMapValuesCache>();
			MetricTempoMapValuesCache.AccumulatedMicroseconds accumulatedMicroseconds = valuesCache.Microseconds.TakeWhile((MetricTempoMapValuesCache.AccumulatedMicroseconds m) => m.Time < timeSpan).LastOrDefault<MetricTempoMapValuesCache.AccumulatedMicroseconds>();
			double num = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.Microseconds : 0.0);
			long num2 = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.Time : 0L);
			double num3 = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.MicrosecondsPerTick : valuesCache.DefaultMicrosecondsPerTick);
			return new MetricTimeSpan(MetricTimeSpanConverter.RoundMicroseconds(num + MetricTimeSpanConverter.GetMicroseconds(timeSpan - num2, num3)));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014424 File Offset: 0x00012624
		private static long MetricTimeSpanToTicks(MetricTimeSpan timeSpan, TempoMap tempoMap)
		{
			long timeMicroseconds = timeSpan.TotalMicroseconds;
			if (timeMicroseconds == 0L)
			{
				return 0L;
			}
			MetricTempoMapValuesCache valuesCache = tempoMap.GetValuesCache<MetricTempoMapValuesCache>();
			MetricTempoMapValuesCache.AccumulatedMicroseconds accumulatedMicroseconds = valuesCache.Microseconds.TakeWhile((MetricTempoMapValuesCache.AccumulatedMicroseconds m) => m.Microseconds < (double)timeMicroseconds).LastOrDefault<MetricTempoMapValuesCache.AccumulatedMicroseconds>();
			double num = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.Microseconds : 0.0);
			long num2 = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.Time : 0L);
			double num3 = ((accumulatedMicroseconds != null) ? accumulatedMicroseconds.TicksPerMicrosecond : valuesCache.DefaultTicksPerMicrosecond);
			return MetricTimeSpanConverter.RoundMicroseconds((double)num2 + ((double)timeMicroseconds - num) * num3);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000144BD File Offset: 0x000126BD
		private static double GetMicroseconds(long time, double microsecondsPerTick)
		{
			return (double)time * microsecondsPerTick;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000144C3 File Offset: 0x000126C3
		private static long RoundMicroseconds(double microseconds)
		{
			return MathUtilities.RoundToLong(microseconds);
		}
	}
}
