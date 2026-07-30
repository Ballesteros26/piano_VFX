using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000155 RID: 341
	internal sealed class MetaEventReader : IEventReader
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			byte b = reader.ReadByte();
			int num = reader.ReadVlqNumber();
			MetaEvent metaEvent;
			if (b <= 47)
			{
				if (b <= 32)
				{
					switch (b)
					{
					case 0:
						metaEvent = new SequenceNumberEvent();
						goto IL_0173;
					case 1:
						metaEvent = new TextEvent();
						goto IL_0173;
					case 2:
						metaEvent = new CopyrightNoticeEvent();
						goto IL_0173;
					case 3:
						metaEvent = new SequenceTrackNameEvent();
						goto IL_0173;
					case 4:
						metaEvent = new InstrumentNameEvent();
						goto IL_0173;
					case 5:
						metaEvent = new LyricEvent();
						goto IL_0173;
					case 6:
						metaEvent = new MarkerEvent();
						goto IL_0173;
					case 7:
						metaEvent = new CuePointEvent();
						goto IL_0173;
					case 8:
						metaEvent = new ProgramNameEvent();
						goto IL_0173;
					case 9:
						metaEvent = new DeviceNameEvent();
						goto IL_0173;
					default:
						if (b == 32)
						{
							metaEvent = new ChannelPrefixEvent();
							goto IL_0173;
						}
						break;
					}
				}
				else
				{
					if (b == 33)
					{
						metaEvent = new PortPrefixEvent();
						goto IL_0173;
					}
					if (b == 47)
					{
						metaEvent = new EndOfTrackEvent();
						goto IL_0173;
					}
				}
			}
			else if (b <= 84)
			{
				if (b == 81)
				{
					metaEvent = new SetTempoEvent();
					goto IL_0173;
				}
				if (b == 84)
				{
					metaEvent = new SmpteOffsetEvent();
					goto IL_0173;
				}
			}
			else
			{
				if (b == 88)
				{
					metaEvent = new TimeSignatureEvent();
					goto IL_0173;
				}
				if (b == 89)
				{
					metaEvent = new KeySignatureEvent();
					goto IL_0173;
				}
				if (b == 127)
				{
					metaEvent = new SequencerSpecificEvent();
					goto IL_0173;
				}
			}
			Type type = null;
			EventTypesCollection customMetaEventTypes = settings.CustomMetaEventTypes;
			metaEvent = ((customMetaEventTypes != null && customMetaEventTypes.TryGetType(b, out type) && MetaEventReader.IsMetaEventType(type)) ? ((MetaEvent)Activator.CreateInstance(type)) : new UnknownMetaEvent(b));
			IL_0173:
			long position = reader.Position;
			metaEvent.Read(reader, settings, num);
			long num2 = reader.Position - position;
			long num3 = (long)num - num2;
			if (num3 > 0L)
			{
				reader.Position += num3;
			}
			return metaEvent;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001FB5F File Offset: 0x0001DD5F
		private static bool IsMetaEventType(Type type)
		{
			return type != null && type.IsSubclassOf(typeof(MetaEvent)) && type.GetConstructor(Type.EmptyTypes) != null;
		}
	}
}
