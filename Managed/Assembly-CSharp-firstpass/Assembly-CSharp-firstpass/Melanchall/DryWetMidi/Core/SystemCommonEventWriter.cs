using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016C RID: 364
	internal sealed class SystemCommonEventWriter : IEventWriter
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000204F4 File Offset: 0x0001E6F4
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = this.GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00020224 File Offset: 0x0001E424
		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002051C File Offset: 0x0001E71C
		public byte GetStatusByte(MidiEvent midiEvent)
		{
			switch (midiEvent.EventType)
			{
			case MidiEventType.MidiTimeCode:
				return 241;
			case MidiEventType.SongPositionPointer:
				return 242;
			case MidiEventType.SongSelect:
				return 243;
			case MidiEventType.TuneRequest:
				return 246;
			default:
				return 0;
			}
		}
	}
}
