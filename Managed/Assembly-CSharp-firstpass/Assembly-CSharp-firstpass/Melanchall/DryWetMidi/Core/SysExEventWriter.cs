using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016B RID: 363
	internal sealed class SysExEventWriter : IEventWriter
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00020468 File Offset: 0x0001E668
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = this.GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			int size = midiEvent.GetSize(settings);
			writer.WriteVlqNumber(size);
			midiEvent.Write(writer, settings);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			int size = midiEvent.GetSize(settings);
			return (writeStatusByte ? 1 : 0) + size.GetVlqLength() + size;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000204C8 File Offset: 0x0001E6C8
		public byte GetStatusByte(MidiEvent midiEvent)
		{
			MidiEventType eventType = midiEvent.EventType;
			if (eventType == MidiEventType.NormalSysEx)
			{
				return 240;
			}
			if (eventType != MidiEventType.EscapeSysEx)
			{
				return 0;
			}
			return 247;
		}
	}
}
