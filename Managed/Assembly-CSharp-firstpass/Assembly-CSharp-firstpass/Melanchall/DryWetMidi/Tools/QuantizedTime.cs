using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000045 RID: 69
	public sealed class QuantizedTime
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00008FCB File Offset: 0x000071CB
		internal QuantizedTime(long newTime, long gridTime, ITimeSpan shift, long distanceToGridTime, ITimeSpan convertedDistanceToGridTime)
		{
			this.NewTime = newTime;
			this.GridTime = gridTime;
			this.Shift = shift;
			this.DistanceToGridTime = distanceToGridTime;
			this.ConvertedDistanceToGridTime = convertedDistanceToGridTime;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008FF8 File Offset: 0x000071F8
		public long NewTime { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009000 File Offset: 0x00007200
		public long GridTime { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009008 File Offset: 0x00007208
		public ITimeSpan Shift { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009010 File Offset: 0x00007210
		public long DistanceToGridTime { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009018 File Offset: 0x00007218
		public ITimeSpan ConvertedDistanceToGridTime { get; }
	}
}
