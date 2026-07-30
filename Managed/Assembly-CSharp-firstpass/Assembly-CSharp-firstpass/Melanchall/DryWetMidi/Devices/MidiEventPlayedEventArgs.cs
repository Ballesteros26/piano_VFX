using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000100 RID: 256
	public sealed class MidiEventPlayedEventArgs : EventArgs
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0001A849 File Offset: 0x00018A49
		internal MidiEventPlayedEventArgs(MidiEvent midiEvent)
		{
			this.Event = midiEvent;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001A858 File Offset: 0x00018A58
		public MidiEvent Event { get; }
	}
}
