using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000167 RID: 359
	internal sealed class ChannelEventWriter : IEventWriter
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x000201FC File Offset: 0x0001E3FC
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = this.GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00020224 File Offset: 0x0001E424
		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00020238 File Offset: 0x0001E438
		public byte GetStatusByte(MidiEvent midiEvent)
		{
			byte b = 0;
			switch (midiEvent.EventType)
			{
			case MidiEventType.NoteOff:
				b = 8;
				break;
			case MidiEventType.NoteOn:
				b = 9;
				break;
			case MidiEventType.NoteAftertouch:
				b = 10;
				break;
			case MidiEventType.ControlChange:
				b = 11;
				break;
			case MidiEventType.ProgramChange:
				b = 12;
				break;
			case MidiEventType.ChannelAftertouch:
				b = 13;
				break;
			case MidiEventType.PitchBend:
				b = 14;
				break;
			}
			FourBitNumber channel = ((ChannelEvent)midiEvent).Channel;
			return DataTypesUtilities.Combine((FourBitNumber)b, channel);
		}
	}
}
