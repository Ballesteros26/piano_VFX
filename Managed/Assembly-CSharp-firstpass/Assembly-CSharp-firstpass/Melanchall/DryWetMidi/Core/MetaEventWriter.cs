using System;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200016A RID: 362
	internal sealed class MetaEventWriter : IEventWriter
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00020320 File Offset: 0x0001E520
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				writer.WriteByte(byte.MaxValue);
			}
			byte b = 0;
			switch (midiEvent.EventType)
			{
			case MidiEventType.SequenceNumber:
				b = 0;
				break;
			case MidiEventType.Text:
				b = 1;
				break;
			case MidiEventType.CopyrightNotice:
				b = 2;
				break;
			case MidiEventType.SequenceTrackName:
				b = 3;
				break;
			case MidiEventType.InstrumentName:
				b = 4;
				break;
			case MidiEventType.Lyric:
				b = 5;
				break;
			case MidiEventType.Marker:
				b = 6;
				break;
			case MidiEventType.CuePoint:
				b = 7;
				break;
			case MidiEventType.ProgramName:
				b = 8;
				break;
			case MidiEventType.DeviceName:
				b = 9;
				break;
			case MidiEventType.ChannelPrefix:
				b = 32;
				break;
			case MidiEventType.PortPrefix:
				b = 33;
				break;
			case MidiEventType.EndOfTrack:
				b = 47;
				break;
			case MidiEventType.SetTempo:
				b = 81;
				break;
			case MidiEventType.SmpteOffset:
				b = 84;
				break;
			case MidiEventType.TimeSignature:
				b = 88;
				break;
			case MidiEventType.KeySignature:
				b = 89;
				break;
			case MidiEventType.SequencerSpecific:
				b = 127;
				break;
			case MidiEventType.UnknownMeta:
				b = ((UnknownMetaEvent)midiEvent).StatusByte;
				break;
			default:
			{
				Type type = midiEvent.GetType();
				EventTypesCollection customMetaEventTypes = settings.CustomMetaEventTypes;
				if (customMetaEventTypes != null)
				{
					bool flag = !customMetaEventTypes.TryGetStatusByte(type, out b);
				}
				break;
			}
			}
			writer.WriteByte(b);
			int size = midiEvent.GetSize(settings);
			writer.WriteVlqNumber(size);
			midiEvent.Write(writer, settings);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00020438 File Offset: 0x0001E638
		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			int size = midiEvent.GetSize(settings);
			return (writeStatusByte ? 1 : 0) + 1 + size.GetVlqLength() + size;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002045F File Offset: 0x0001E65F
		public byte GetStatusByte(MidiEvent midiEvent)
		{
			return byte.MaxValue;
		}
	}
}
