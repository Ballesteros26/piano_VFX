using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000EE RID: 238
	public sealed class MidiEventReceivedEventArgs : EventArgs
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00019716 File Offset: 0x00017916
		internal MidiEventReceivedEventArgs(MidiEvent midiEvent)
		{
			this.Event = midiEvent;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00019725 File Offset: 0x00017925
		public MidiEvent Event { get; }
	}
}
