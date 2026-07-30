using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016D RID: 365
	internal sealed class SystemRealTimeEventWriter : IEventWriter
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x00020564 File Offset: 0x0001E764
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = this.GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00020224 File Offset: 0x0001E424
		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002058C File Offset: 0x0001E78C
		public byte GetStatusByte(MidiEvent midiEvent)
		{
			switch (midiEvent.EventType)
			{
			case MidiEventType.TimingClock:
				return 248;
			case MidiEventType.Start:
				return 250;
			case MidiEventType.Continue:
				return 251;
			case MidiEventType.Stop:
				return 252;
			case MidiEventType.ActiveSensing:
				return 254;
			case MidiEventType.Reset:
				return byte.MaxValue;
			default:
				return 0;
			}
		}
	}
}
