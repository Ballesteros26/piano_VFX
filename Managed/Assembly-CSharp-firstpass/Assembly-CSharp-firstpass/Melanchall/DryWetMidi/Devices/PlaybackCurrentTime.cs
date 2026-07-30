using System;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FD RID: 253
	public sealed class PlaybackCurrentTime
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001A392 File Offset: 0x00018592
		internal PlaybackCurrentTime(Playback playback, ITimeSpan time)
		{
			this.Playback = playback;
			this.Time = time;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001A3A8 File Offset: 0x000185A8
		public Playback Playback { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001A3B0 File Offset: 0x000185B0
		public ITimeSpan Time { get; }
	}
}
