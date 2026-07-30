using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000179 RID: 377
	public sealed class UnexpectedTrackChunksCountException : MidiException
	{
		// Token: 0x06000942 RID: 2370 RVA: 0x000207A9 File Offset: 0x0001E9A9
		internal UnexpectedTrackChunksCountException(int expectedCount, int actualCount)
			: base(string.Format("Count of track chunks is {0} while {1} expected.", actualCount, expectedCount))
		{
			this.ExpectedCount = expectedCount;
			this.ActualCount = actualCount;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x000207D5 File Offset: 0x0001E9D5
		public int ExpectedCount { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x000207DD File Offset: 0x0001E9DD
		public int ActualCount { get; }
	}
}
