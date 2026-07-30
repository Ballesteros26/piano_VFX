using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000154 RID: 340
	internal interface IEventReader
	{
		// Token: 0x060008C3 RID: 2243
		MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte);
	}
}
