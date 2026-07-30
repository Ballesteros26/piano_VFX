using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x0200010E RID: 270
	internal sealed class RecordingEvent
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x0001C7B3 File Offset: 0x0001A9B3
		public RecordingEvent(MidiEvent midiEvent, TimeSpan time)
		{
			this.Event = midiEvent;
			this.Time = time;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001C7C9 File Offset: 0x0001A9C9
		public MidiEvent Event { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
		public TimeSpan Time { get; }
	}
}
