using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200002A RID: 42
	internal static class EventNameGetterProvider
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000610A File Offset: 0x0000430A
		public static EventNameGetter Get(Type eventType, MidiFileCsvLayout layout)
		{
			if (layout == MidiFileCsvLayout.DryWetMidi)
			{
				return EventNameGetterProvider.EventsTypes_DryWetMidi[eventType];
			}
			if (layout != MidiFileCsvLayout.MidiCsv)
			{
				return null;
			}
			return EventNameGetterProvider.EventsTypes_MidiCsv[eventType];
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000612E File Offset: 0x0000432E
		private static EventNameGetter GetType(string type)
		{
			return (MidiEvent e) => type;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006147 File Offset: 0x00004347
		private static EventNameGetter GetSysExType(string completedType, string incompletedType)
		{
			return delegate(MidiEvent e)
			{
				if (!((SysExEvent)e).Completed)
				{
					return incompletedType;
				}
				return completedType;
			};
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006168 File Offset: 0x00004368
		// Note: this type is marked as 'beforefieldinit'.
		static EventNameGetterProvider()
		{
			Dictionary<Type, EventNameGetter> dictionary = new Dictionary<Type, EventNameGetter>();
			Type type = typeof(SequenceTrackNameEvent);
			dictionary[type] = EventNameGetterProvider.GetType("Title_t");
			Type type2 = typeof(CopyrightNoticeEvent);
			dictionary[type2] = EventNameGetterProvider.GetType("Copyright_t");
			Type type3 = typeof(InstrumentNameEvent);
			dictionary[type3] = EventNameGetterProvider.GetType("Instrument_name_t");
			Type type4 = typeof(MarkerEvent);
			dictionary[type4] = EventNameGetterProvider.GetType("Marker_t");
			Type type5 = typeof(CuePointEvent);
			dictionary[type5] = EventNameGetterProvider.GetType("Cue_point_t");
			Type type6 = typeof(LyricEvent);
			dictionary[type6] = EventNameGetterProvider.GetType("Lyric_t");
			Type type7 = typeof(TextEvent);
			dictionary[type7] = EventNameGetterProvider.GetType("Text_t");
			Type type8 = typeof(SequenceNumberEvent);
			dictionary[type8] = EventNameGetterProvider.GetType("Sequence_number");
			Type type9 = typeof(PortPrefixEvent);
			dictionary[type9] = EventNameGetterProvider.GetType("MIDI_port");
			Type type10 = typeof(ChannelPrefixEvent);
			dictionary[type10] = EventNameGetterProvider.GetType("Channel_prefix");
			Type type11 = typeof(TimeSignatureEvent);
			dictionary[type11] = EventNameGetterProvider.GetType("Time_signature");
			Type type12 = typeof(KeySignatureEvent);
			dictionary[type12] = EventNameGetterProvider.GetType("Key_signature");
			Type type13 = typeof(SetTempoEvent);
			dictionary[type13] = EventNameGetterProvider.GetType("Tempo");
			Type type14 = typeof(SmpteOffsetEvent);
			dictionary[type14] = EventNameGetterProvider.GetType("SMPTE_offset");
			Type type15 = typeof(SequencerSpecificEvent);
			dictionary[type15] = EventNameGetterProvider.GetType("Sequencer_specific");
			Type type16 = typeof(UnknownMetaEvent);
			dictionary[type16] = EventNameGetterProvider.GetType("Unknown_meta_event");
			Type type17 = typeof(NoteOnEvent);
			dictionary[type17] = EventNameGetterProvider.GetType("Note_on_c");
			Type type18 = typeof(NoteOffEvent);
			dictionary[type18] = EventNameGetterProvider.GetType("Note_off_c");
			Type type19 = typeof(PitchBendEvent);
			dictionary[type19] = EventNameGetterProvider.GetType("Pitch_bend_c");
			Type type20 = typeof(ControlChangeEvent);
			dictionary[type20] = EventNameGetterProvider.GetType("Control_c");
			Type type21 = typeof(ProgramChangeEvent);
			dictionary[type21] = EventNameGetterProvider.GetType("Program_c");
			Type type22 = typeof(ChannelAftertouchEvent);
			dictionary[type22] = EventNameGetterProvider.GetType("Channel_aftertouch_c");
			Type type23 = typeof(NoteAftertouchEvent);
			dictionary[type23] = EventNameGetterProvider.GetType("Poly_aftertouch_c");
			Type type24 = typeof(NormalSysExEvent);
			dictionary[type24] = EventNameGetterProvider.GetSysExType("System_exclusive", "System_exclusive_packet");
			Type type25 = typeof(EscapeSysExEvent);
			dictionary[type25] = EventNameGetterProvider.GetSysExType("System_exclusive", "System_exclusive_packet");
			EventNameGetterProvider.EventsTypes_MidiCsv = dictionary;
			Dictionary<Type, EventNameGetter> dictionary2 = new Dictionary<Type, EventNameGetter>();
			type25 = typeof(SequenceTrackNameEvent);
			dictionary2[type25] = EventNameGetterProvider.GetType("Sequence/Track Name");
			type24 = typeof(CopyrightNoticeEvent);
			dictionary2[type24] = EventNameGetterProvider.GetType("Copyright Notice");
			type23 = typeof(InstrumentNameEvent);
			dictionary2[type23] = EventNameGetterProvider.GetType("Instrument Name");
			type22 = typeof(MarkerEvent);
			dictionary2[type22] = EventNameGetterProvider.GetType("Marker");
			type21 = typeof(CuePointEvent);
			dictionary2[type21] = EventNameGetterProvider.GetType("Cue Point");
			type20 = typeof(LyricEvent);
			dictionary2[type20] = EventNameGetterProvider.GetType("Lyric");
			type19 = typeof(TextEvent);
			dictionary2[type19] = EventNameGetterProvider.GetType("Text");
			type18 = typeof(SequenceNumberEvent);
			dictionary2[type18] = EventNameGetterProvider.GetType("Sequence Number");
			type17 = typeof(PortPrefixEvent);
			dictionary2[type17] = EventNameGetterProvider.GetType("Port Prefix");
			type16 = typeof(ChannelPrefixEvent);
			dictionary2[type16] = EventNameGetterProvider.GetType("Channel Prefix");
			type15 = typeof(TimeSignatureEvent);
			dictionary2[type15] = EventNameGetterProvider.GetType("Time Signature");
			type14 = typeof(KeySignatureEvent);
			dictionary2[type14] = EventNameGetterProvider.GetType("Key Signature");
			type13 = typeof(SetTempoEvent);
			dictionary2[type13] = EventNameGetterProvider.GetType("Set Tempo");
			type12 = typeof(SmpteOffsetEvent);
			dictionary2[type12] = EventNameGetterProvider.GetType("SMPTE Offset");
			type11 = typeof(SequencerSpecificEvent);
			dictionary2[type11] = EventNameGetterProvider.GetType("Sequencer Specific");
			type10 = typeof(UnknownMetaEvent);
			dictionary2[type10] = EventNameGetterProvider.GetType("Unknown Meta");
			type9 = typeof(NoteOnEvent);
			dictionary2[type9] = EventNameGetterProvider.GetType("Note On");
			type8 = typeof(NoteOffEvent);
			dictionary2[type8] = EventNameGetterProvider.GetType("Note Off");
			type7 = typeof(PitchBendEvent);
			dictionary2[type7] = EventNameGetterProvider.GetType("Pitch Bend");
			type6 = typeof(ControlChangeEvent);
			dictionary2[type6] = EventNameGetterProvider.GetType("Control Change");
			type5 = typeof(ProgramChangeEvent);
			dictionary2[type5] = EventNameGetterProvider.GetType("Program Change");
			type4 = typeof(ChannelAftertouchEvent);
			dictionary2[type4] = EventNameGetterProvider.GetType("Channel Aftertouch");
			type3 = typeof(NoteAftertouchEvent);
			dictionary2[type3] = EventNameGetterProvider.GetType("Note Aftertouch");
			type2 = typeof(NormalSysExEvent);
			dictionary2[type2] = EventNameGetterProvider.GetSysExType("System Exclusive", "System Exclusive Packet");
			type = typeof(EscapeSysExEvent);
			dictionary2[type] = EventNameGetterProvider.GetSysExType("System Exclusive", "System Exclusive Packet");
			EventNameGetterProvider.EventsTypes_DryWetMidi = dictionary2;
		}

		// Token: 0x040000B4 RID: 180
		private static readonly Dictionary<Type, EventNameGetter> EventsTypes_MidiCsv;

		// Token: 0x040000B5 RID: 181
		private static readonly Dictionary<Type, EventNameGetter> EventsTypes_DryWetMidi;
	}
}
