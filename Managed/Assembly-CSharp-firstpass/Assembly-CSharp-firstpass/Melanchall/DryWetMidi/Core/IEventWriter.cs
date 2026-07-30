using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000169 RID: 361
	internal interface IEventWriter
	{
		// Token: 0x06000912 RID: 2322
		void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte);

		// Token: 0x06000913 RID: 2323
		int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte);

		// Token: 0x06000914 RID: 2324
		byte GetStatusByte(MidiEvent midiEvent);
	}
}
