using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000103 RID: 259
	internal sealed class PlaybackEvent
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001B898 File Offset: 0x00019A98
		public PlaybackEvent(MidiEvent midiEvent, TimeSpan time, long rawTime)
		{
			this.Event = midiEvent;
			this.Time = time;
			this.RawTime = rawTime;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		public MidiEvent Event { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		public TimeSpan Time { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001B8D0 File Offset: 0x00019AD0
		public long RawTime { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public PlaybackEventMetadata Metadata { get; } = new PlaybackEventMetadata();
	}
}
