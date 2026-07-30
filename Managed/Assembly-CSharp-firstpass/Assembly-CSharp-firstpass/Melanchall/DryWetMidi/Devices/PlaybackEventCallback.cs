using System;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Devices
{
	// Token: 0x02000104 RID: 260
	// (Invoke) Token: 0x060006DE RID: 1758
	public delegate MidiEvent PlaybackEventCallback(MidiEvent midiEvent, TimeSpan time, long rawTime);
}
