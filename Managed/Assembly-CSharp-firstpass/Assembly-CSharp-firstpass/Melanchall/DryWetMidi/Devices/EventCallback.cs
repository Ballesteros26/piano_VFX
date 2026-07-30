using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x020000FA RID: 250
	// (Invoke) Token: 0x0600065B RID: 1627
	public delegate MidiEvent EventCallback(MidiEvent rawEvent, long rawTime, TimeSpan playbackTime);
}
