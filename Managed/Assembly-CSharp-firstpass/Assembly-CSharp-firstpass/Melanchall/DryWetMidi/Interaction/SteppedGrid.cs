using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x02000095 RID: 149
	public sealed class SteppedGrid : IGrid
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00010EF5 File Offset: 0x0000F0F5
		public SteppedGrid(ITimeSpan step)
			: this((MidiTimeSpan)0L, step)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00010F05 File Offset: 0x0000F105
		public SteppedGrid(ITimeSpan start, ITimeSpan step)
		{
			ThrowIfArgument.IsNull("start", start);
			ThrowIfArgument.IsNull("step", step);
			this.Start = start;
			this.Steps = new ITimeSpan[] { step };
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010F3A File Offset: 0x0000F13A
		public SteppedGrid(IEnumerable<ITimeSpan> steps)
			: this((MidiTimeSpan)0L, steps)
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00010F4A File Offset: 0x0000F14A
		public SteppedGrid(ITimeSpan start, IEnumerable<ITimeSpan> steps)
		{
			ThrowIfArgument.IsNull("start", start);
			ThrowIfArgument.IsNull("steps", steps);
			ThrowIfArgument.ContainsNull<ITimeSpan>("steps", steps);
			this.Start = start;
			this.Steps = steps;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010F81 File Offset: 0x0000F181
		public ITimeSpan Start { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00010F89 File Offset: 0x0000F189
		public IEnumerable<ITimeSpan> Steps { get; }

		// Token: 0x06000328 RID: 808 RVA: 0x00010F91 File Offset: 0x0000F191
		public IEnumerable<long> GetTimes(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!this.Steps.Any<ITimeSpan>())
			{
				yield break;
			}
			long time = TimeConverter.ConvertFrom(this.Start, tempoMap);
			yield return time;
			for (;;)
			{
				foreach (ITimeSpan timeSpan in this.Steps)
				{
					time += LengthConverter.ConvertFrom(timeSpan, time, tempoMap);
					yield return time;
				}
				IEnumerator<ITimeSpan> enumerator = null;
			}
			yield break;
			yield break;
		}
	}
}
