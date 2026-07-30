using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000F5 RID: 245
	public sealed class MidiEventSentEventArgs : EventArgs
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00019980 File Offset: 0x00017B80
		internal MidiEventSentEventArgs(MidiEvent midiEvent)
		{
			this.Event = midiEvent;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001998F File Offset: 0x00017B8F
		public MidiEvent Event { get; }
	}
}
