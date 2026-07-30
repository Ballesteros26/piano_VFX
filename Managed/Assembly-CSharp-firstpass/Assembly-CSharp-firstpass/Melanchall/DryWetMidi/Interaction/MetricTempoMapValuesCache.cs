using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000C6 RID: 198
	internal sealed class MetricTempoMapValuesCache : ITempoMapValuesCache
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00016CA1 File Offset: 0x00014EA1
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00016CA9 File Offset: 0x00014EA9
		public IEnumerable<MetricTempoMapValuesCache.AccumulatedMicroseconds> Microseconds { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00016CB2 File Offset: 0x00014EB2
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00016CBA File Offset: 0x00014EBA
		public double DefaultMicrosecondsPerTick { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00016CC3 File Offset: 0x00014EC3
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00016CCB File Offset: 0x00014ECB
		public double DefaultTicksPerMicrosecond { get; private set; }

		// Token: 0x060004F8 RID: 1272 RVA: 0x00016CD4 File Offset: 0x00014ED4
		private static double GetMicroseconds(long time, Tempo tempo, short ticksPerQuarterNote)
		{
			return (double)(time * tempo.MicrosecondsPerQuarterNote) / (double)ticksPerQuarterNote;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00016CE2 File Offset: 0x00014EE2
		public IEnumerable<TempoMapLine> InvalidateOnLines { get; } = new TempoMapLine[1];

		// Token: 0x060004FA RID: 1274 RVA: 0x00016CEC File Offset: 0x00014EEC
		public void Invalidate(TempoMap tempoMap)
		{
			List<MetricTempoMapValuesCache.AccumulatedMicroseconds> list = new List<MetricTempoMapValuesCache.AccumulatedMicroseconds>();
			short ticksPerQuarterNote = ((TicksPerQuarterNoteTimeDivision)tempoMap.TimeDivision).TicksPerQuarterNote;
			double num = 0.0;
			long num2 = 0L;
			Tempo tempo = Tempo.Default;
			foreach (ValueChange<Tempo> valueChange in tempoMap.Tempo)
			{
				long time = valueChange.Time;
				num += MetricTempoMapValuesCache.GetMicroseconds(time - num2, tempo, ticksPerQuarterNote);
				tempo = valueChange.Value;
				num2 = time;
				list.Add(new MetricTempoMapValuesCache.AccumulatedMicroseconds(time, num, (double)tempo.MicrosecondsPerQuarterNote / (double)ticksPerQuarterNote));
			}
			this.Microseconds = list;
			this.DefaultMicrosecondsPerTick = (double)Tempo.Default.MicrosecondsPerQuarterNote / (double)ticksPerQuarterNote;
			this.DefaultTicksPerMicrosecond = 1.0 / this.DefaultMicrosecondsPerTick;
		}

		// Token: 0x02000253 RID: 595
		internal sealed class AccumulatedMicroseconds
		{
			// Token: 0x06000E0A RID: 3594 RVA: 0x0002A324 File Offset: 0x00028524
			public AccumulatedMicroseconds(long time, double microseconds, double microsecondsPerTick)
			{
				this.Time = time;
				this.Microseconds = microseconds;
				this.MicrosecondsPerTick = microsecondsPerTick;
				this.TicksPerMicrosecond = 1.0 / microsecondsPerTick;
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0002A352 File Offset: 0x00028552
			public long Time { get; }

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002A35A File Offset: 0x0002855A
			public double Microseconds { get; }

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0002A362 File Offset: 0x00028562
			public double MicrosecondsPerTick { get; }

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002A36A File Offset: 0x0002856A
			public double TicksPerMicrosecond { get; }
		}
	}
}
