using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F4 RID: 244
	public interface IOutputDevice
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000616 RID: 1558
		// (remove) Token: 0x06000617 RID: 1559
		event EventHandler<MidiEventSentEventArgs> EventSent;

		// Token: 0x06000618 RID: 1560
		void PrepareForEventsSending();

		// Token: 0x06000619 RID: 1561
		void SendEvent(MidiEvent midiEvent);
	}
}
