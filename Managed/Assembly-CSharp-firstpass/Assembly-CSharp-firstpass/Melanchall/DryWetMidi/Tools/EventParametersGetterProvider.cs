using System;
using System.Collections.Generic;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200002C RID: 44
	internal static class EventParametersGetterProvider
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00006769 File Offset: 0x00004969
		public static EventParametersGetter Get(Type eventType)
		{
			return EventParametersGetterProvider.EventsParametersGetters[eventType];
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006776 File Offset: 0x00004976
		private static EventParametersGetter GetParameters<T>(params Func<T, MidiFileCsvConversionSettings, object>[] parametersGetters) where T : MidiEvent
		{
			return (MidiEvent e, MidiFileCsvConversionSettings s) => parametersGetters.Select((Func<T, MidiFileCsvConversionSettings, object> g) => g((T)((object)e), s)).ToArray<object>();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000678F File Offset: 0x0000498F
		private static object FormatNoteNumber(SevenBitNumber noteNumber, MidiFileCsvConversionSettings settings)
		{
			if (settings.CsvLayout == MidiFileCsvLayout.MidiCsv)
			{
				return noteNumber;
			}
			return NoteCsvConversionUtilities.FormatNoteNumber(noteNumber, settings.NoteNumberFormat);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000067B0 File Offset: 0x000049B0
		// Note: this type is marked as 'beforefieldinit'.
		static EventParametersGetterProvider()
		{
			Dictionary<Type, EventParametersGetter> dictionary = new Dictionary<Type, EventParametersGetter>();
			Type typeFromHandle = typeof(SequenceTrackNameEvent);
			dictionary[typeFromHandle] = EventParametersGetterProvider.GetParameters<SequenceTrackNameEvent>(new Func<SequenceTrackNameEvent, MidiFileCsvConversionSettings, object>[]
			{
				(SequenceTrackNameEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle2 = typeof(CopyrightNoticeEvent);
			dictionary[typeFromHandle2] = EventParametersGetterProvider.GetParameters<CopyrightNoticeEvent>(new Func<CopyrightNoticeEvent, MidiFileCsvConversionSettings, object>[]
			{
				(CopyrightNoticeEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle3 = typeof(InstrumentNameEvent);
			dictionary[typeFromHandle3] = EventParametersGetterProvider.GetParameters<InstrumentNameEvent>(new Func<InstrumentNameEvent, MidiFileCsvConversionSettings, object>[]
			{
				(InstrumentNameEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle4 = typeof(MarkerEvent);
			dictionary[typeFromHandle4] = EventParametersGetterProvider.GetParameters<MarkerEvent>(new Func<MarkerEvent, MidiFileCsvConversionSettings, object>[]
			{
				(MarkerEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle5 = typeof(CuePointEvent);
			dictionary[typeFromHandle5] = EventParametersGetterProvider.GetParameters<CuePointEvent>(new Func<CuePointEvent, MidiFileCsvConversionSettings, object>[]
			{
				(CuePointEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle6 = typeof(LyricEvent);
			dictionary[typeFromHandle6] = EventParametersGetterProvider.GetParameters<LyricEvent>(new Func<LyricEvent, MidiFileCsvConversionSettings, object>[]
			{
				(LyricEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle7 = typeof(TextEvent);
			dictionary[typeFromHandle7] = EventParametersGetterProvider.GetParameters<TextEvent>(new Func<TextEvent, MidiFileCsvConversionSettings, object>[]
			{
				(TextEvent e, MidiFileCsvConversionSettings s) => e.Text
			});
			Type typeFromHandle8 = typeof(SequenceNumberEvent);
			dictionary[typeFromHandle8] = EventParametersGetterProvider.GetParameters<SequenceNumberEvent>(new Func<SequenceNumberEvent, MidiFileCsvConversionSettings, object>[]
			{
				(SequenceNumberEvent e, MidiFileCsvConversionSettings s) => e.Number
			});
			Type typeFromHandle9 = typeof(PortPrefixEvent);
			dictionary[typeFromHandle9] = EventParametersGetterProvider.GetParameters<PortPrefixEvent>(new Func<PortPrefixEvent, MidiFileCsvConversionSettings, object>[]
			{
				(PortPrefixEvent e, MidiFileCsvConversionSettings s) => e.Port
			});
			Type typeFromHandle10 = typeof(ChannelPrefixEvent);
			dictionary[typeFromHandle10] = EventParametersGetterProvider.GetParameters<ChannelPrefixEvent>(new Func<ChannelPrefixEvent, MidiFileCsvConversionSettings, object>[]
			{
				(ChannelPrefixEvent e, MidiFileCsvConversionSettings s) => e.Channel
			});
			Type typeFromHandle11 = typeof(TimeSignatureEvent);
			dictionary[typeFromHandle11] = EventParametersGetterProvider.GetParameters<TimeSignatureEvent>(new Func<TimeSignatureEvent, MidiFileCsvConversionSettings, object>[]
			{
				(TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.Numerator,
				delegate(TimeSignatureEvent e, MidiFileCsvConversionSettings s)
				{
					MidiFileCsvLayout csvLayout = s.CsvLayout;
					if (csvLayout == MidiFileCsvLayout.DryWetMidi)
					{
						return e.Denominator;
					}
					if (csvLayout != MidiFileCsvLayout.MidiCsv)
					{
						return null;
					}
					return (byte)Math.Log((double)e.Denominator, 2.0);
				},
				(TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.ClocksPerClick,
				(TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.ThirtySecondNotesPerBeat
			});
			Type typeFromHandle12 = typeof(KeySignatureEvent);
			dictionary[typeFromHandle12] = EventParametersGetterProvider.GetParameters<KeySignatureEvent>(new Func<KeySignatureEvent, MidiFileCsvConversionSettings, object>[]
			{
				(KeySignatureEvent e, MidiFileCsvConversionSettings s) => e.Key,
				(KeySignatureEvent e, MidiFileCsvConversionSettings s) => e.Scale
			});
			Type typeFromHandle13 = typeof(SetTempoEvent);
			dictionary[typeFromHandle13] = EventParametersGetterProvider.GetParameters<SetTempoEvent>(new Func<SetTempoEvent, MidiFileCsvConversionSettings, object>[]
			{
				(SetTempoEvent e, MidiFileCsvConversionSettings s) => e.MicrosecondsPerQuarterNote
			});
			Type typeFromHandle14 = typeof(SmpteOffsetEvent);
			dictionary[typeFromHandle14] = EventParametersGetterProvider.GetParameters<SmpteOffsetEvent>(new Func<SmpteOffsetEvent, MidiFileCsvConversionSettings, object>[]
			{
				(SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => SmpteData.GetFormatAndHours(e.Format, e.Hours),
				(SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Minutes,
				(SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Seconds,
				(SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Frames,
				(SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.SubFrames
			});
			Type typeFromHandle15 = typeof(SequencerSpecificEvent);
			dictionary[typeFromHandle15] = EventParametersGetterProvider.GetParameters<SequencerSpecificEvent>(new Func<SequencerSpecificEvent, MidiFileCsvConversionSettings, object>[]
			{
				(SequencerSpecificEvent e, MidiFileCsvConversionSettings s) => e.Data.Length,
				(SequencerSpecificEvent e, MidiFileCsvConversionSettings s) => e.Data
			});
			Type typeFromHandle16 = typeof(UnknownMetaEvent);
			dictionary[typeFromHandle16] = EventParametersGetterProvider.GetParameters<UnknownMetaEvent>(new Func<UnknownMetaEvent, MidiFileCsvConversionSettings, object>[]
			{
				(UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.StatusByte,
				(UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.Data.Length,
				(UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.Data
			});
			Type typeFromHandle17 = typeof(NoteOnEvent);
			dictionary[typeFromHandle17] = EventParametersGetterProvider.GetParameters<NoteOnEvent>(new Func<NoteOnEvent, MidiFileCsvConversionSettings, object>[]
			{
				(NoteOnEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(NoteOnEvent e, MidiFileCsvConversionSettings s) => EventParametersGetterProvider.FormatNoteNumber(e.NoteNumber, s),
				(NoteOnEvent e, MidiFileCsvConversionSettings s) => e.Velocity
			});
			Type typeFromHandle18 = typeof(NoteOffEvent);
			dictionary[typeFromHandle18] = EventParametersGetterProvider.GetParameters<NoteOffEvent>(new Func<NoteOffEvent, MidiFileCsvConversionSettings, object>[]
			{
				(NoteOffEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(NoteOffEvent e, MidiFileCsvConversionSettings s) => EventParametersGetterProvider.FormatNoteNumber(e.NoteNumber, s),
				(NoteOffEvent e, MidiFileCsvConversionSettings s) => e.Velocity
			});
			Type typeFromHandle19 = typeof(PitchBendEvent);
			dictionary[typeFromHandle19] = EventParametersGetterProvider.GetParameters<PitchBendEvent>(new Func<PitchBendEvent, MidiFileCsvConversionSettings, object>[]
			{
				(PitchBendEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(PitchBendEvent e, MidiFileCsvConversionSettings s) => e.PitchValue
			});
			Type typeFromHandle20 = typeof(ControlChangeEvent);
			dictionary[typeFromHandle20] = EventParametersGetterProvider.GetParameters<ControlChangeEvent>(new Func<ControlChangeEvent, MidiFileCsvConversionSettings, object>[]
			{
				(ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.ControlNumber,
				(ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.ControlValue
			});
			Type typeFromHandle21 = typeof(ProgramChangeEvent);
			dictionary[typeFromHandle21] = EventParametersGetterProvider.GetParameters<ProgramChangeEvent>(new Func<ProgramChangeEvent, MidiFileCsvConversionSettings, object>[]
			{
				(ProgramChangeEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(ProgramChangeEvent e, MidiFileCsvConversionSettings s) => e.ProgramNumber
			});
			Type typeFromHandle22 = typeof(ChannelAftertouchEvent);
			dictionary[typeFromHandle22] = EventParametersGetterProvider.GetParameters<ChannelAftertouchEvent>(new Func<ChannelAftertouchEvent, MidiFileCsvConversionSettings, object>[]
			{
				(ChannelAftertouchEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(ChannelAftertouchEvent e, MidiFileCsvConversionSettings s) => e.AftertouchValue
			});
			Type typeFromHandle23 = typeof(NoteAftertouchEvent);
			dictionary[typeFromHandle23] = EventParametersGetterProvider.GetParameters<NoteAftertouchEvent>(new Func<NoteAftertouchEvent, MidiFileCsvConversionSettings, object>[]
			{
				(NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => e.Channel,
				(NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => EventParametersGetterProvider.FormatNoteNumber(e.NoteNumber, s),
				(NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => e.AftertouchValue
			});
			Type typeFromHandle24 = typeof(NormalSysExEvent);
			dictionary[typeFromHandle24] = EventParametersGetterProvider.GetParameters<NormalSysExEvent>(new Func<NormalSysExEvent, MidiFileCsvConversionSettings, object>[]
			{
				(NormalSysExEvent e, MidiFileCsvConversionSettings s) => e.Data.Length,
				(NormalSysExEvent e, MidiFileCsvConversionSettings s) => e.Data
			});
			Type typeFromHandle25 = typeof(EscapeSysExEvent);
			dictionary[typeFromHandle25] = EventParametersGetterProvider.GetParameters<EscapeSysExEvent>(new Func<EscapeSysExEvent, MidiFileCsvConversionSettings, object>[]
			{
				(EscapeSysExEvent e, MidiFileCsvConversionSettings s) => e.Data.Length,
				(EscapeSysExEvent e, MidiFileCsvConversionSettings s) => e.Data
			});
			EventParametersGetterProvider.EventsParametersGetters = dictionary;
		}

		// Token: 0x040000B6 RID: 182
		private static readonly Dictionary<Type, EventParametersGetter> EventsParametersGetters;
	}
}
