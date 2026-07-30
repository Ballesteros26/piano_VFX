using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000176 RID: 374
	public sealed class NotEnoughBytesException : MidiException
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0002073A File Offset: 0x0001E93A
		internal NotEnoughBytesException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00020744 File Offset: 0x0001E944
		internal NotEnoughBytesException(string message, long expectedCount, long actualCount)
			: base(message)
		{
			this.ExpectedCount = expectedCount;
			this.ActualCount = actualCount;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0002075B File Offset: 0x0001E95B
		public long ExpectedCount { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00020763 File Offset: 0x0001E963
		public long ActualCount { get; }
	}
}
