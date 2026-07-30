using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200003A RID: 58
	public class SliceMidiFileSettings
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008C2A File Offset: 0x00006E2A
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00008C32 File Offset: 0x00006E32
		public bool SplitNotes { get; set; } = true;

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008C3B File Offset: 0x00006E3B
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00008C43 File Offset: 0x00006E43
		public bool PreserveTimes { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008C4C File Offset: 0x00006E4C
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00008C54 File Offset: 0x00006E54
		public bool PreserveTrackChunks { get; set; }
	}
}
