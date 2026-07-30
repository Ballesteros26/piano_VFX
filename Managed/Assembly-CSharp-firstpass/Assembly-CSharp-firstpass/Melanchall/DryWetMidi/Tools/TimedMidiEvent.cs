using System;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000022 RID: 34
	internal sealed class TimedMidiEvent
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005F5B File Offset: 0x0000415B
		public TimedMidiEvent(ITimeSpan time, MidiEvent midiEvent)
		{
			this.Time = time;
			this.Event = midiEvent;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005F71 File Offset: 0x00004171
		public ITimeSpan Time { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005F79 File Offset: 0x00004179
		public MidiEvent Event { get; }
	}
}
