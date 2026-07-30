using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000012 RID: 18
	public sealed class TimeProcessingInstruction
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000468B File Offset: 0x0000288B
		public TimeProcessingInstruction(long time)
			: this(TimeProcessingAction.Apply, time)
		{
			ThrowIfArgument.IsNegative("time", time, "Time is negative.");
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000046A5 File Offset: 0x000028A5
		private TimeProcessingInstruction(TimeProcessingAction quantizingInstruction, long time)
		{
			this.Action = quantizingInstruction;
			this.Time = time;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000046BB File Offset: 0x000028BB
		public TimeProcessingAction Action { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000046C3 File Offset: 0x000028C3
		public long Time { get; }

		// Token: 0x04000073 RID: 115
		public static readonly TimeProcessingInstruction Skip = new TimeProcessingInstruction(TimeProcessingAction.Skip, -1L);

		// Token: 0x04000074 RID: 116
		private const long InvalidTime = -1L;
	}
}
