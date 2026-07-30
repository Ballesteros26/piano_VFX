using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200001D RID: 29
	internal static class EventParserProvider
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00005663 File Offset: 0x00003863
		public static EventParser Get(string eventName, MidiFileCsvLayout layout)
		{
			if (layout == MidiFileCsvLayout.DryWetMidi)
			{
				return EventParserProvider.EventsParsers_DryWetMidi[eventName];
			}
			if (layout != MidiFileCsvLayout.MidiCsv)
			{
				return null;
			}
			return EventParserProvider.EventsParsers_MidiCsv[eventName];
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005687 File Offset: 0x00003887
		private static EventParser GetBytesBasedEventParser(Func<object[], MidiEvent> eventCreator, params ParameterParser[] parametersParsers)
		{
			return delegate(string[] p, MidiFileCsvConversionSettings s)
			{
				if (p.Length < parametersParsers.Length)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.", null);
				}
				List<object> list = new List<object>(parametersParsers.Length);
				int i = 0;
				int num;
				for (i = 0; i < parametersParsers.Length; i = num + 1)
				{
					ParameterParser parameterParser = parametersParsers[i];
					try
					{
						object obj = parameterParser(p[i], s);
						list.Add(obj);
					}
					catch
					{
						CsvError.ThrowBadFormat(string.Format("Parameter ({0}) is invalid.", i), null);
					}
					num = i;
				}
				if (p.Length < i)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.", null);
				}
				int num2 = 0;
				try
				{
					num2 = int.Parse(p[i]);
					list.Add(num2);
				}
				catch
				{
					CsvError.ThrowBadFormat(string.Format("Parameter ({0}) is invalid.", i), null);
				}
				num = i;
				i = num + 1;
				if (p.Length < i + num2)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.", null);
				}
				try
				{
					byte[] array = p.Skip(i).Select(delegate(string x)
					{
						byte b = (byte)TypeParser.Byte(x, s);
						int j = i;
						i = j + 1;
						return b;
					}).ToArray<byte>();
					list.Add(array);
				}
				catch
				{
					CsvError.ThrowBadFormat(string.Format("Parameter ({0}) is invalid.", i), null);
				}
				return eventCreator(list.ToArray());
			};
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000056A7 File Offset: 0x000038A7
		private static EventParser GetTextEventParser<TEvent>() where TEvent : BaseTextEvent
		{
			return EventParserProvider.GetEventParser(delegate(object[] x)
			{
				TEvent tevent = Activator.CreateInstance<TEvent>();
				tevent.Text = (string)x[0];
				return tevent;
			}, new ParameterParser[] { TypeParser.String });
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000056DC File Offset: 0x000038DC
		private static EventParser GetNoteEventParser<TEvent>(int parametersNumber) where TEvent : ChannelEvent
		{
			return EventParserProvider.GetChannelEventParser<TEvent>(new ParameterParser[] { TypeParser.NoteNumber }.Concat(from i in Enumerable.Range(0, parametersNumber - 1)
				select TypeParser.SevenBitNumber).ToArray<ParameterParser>());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005733 File Offset: 0x00003933
		private static EventParser GetChannelEventParser<TEvent>(int parametersNumber) where TEvent : ChannelEvent
		{
			return EventParserProvider.GetChannelEventParser<TEvent>((from i in Enumerable.Range(0, parametersNumber)
				select TypeParser.SevenBitNumber).ToArray<ParameterParser>());
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000576C File Offset: 0x0000396C
		private static EventParser GetChannelEventParser<TEvent>(ParameterParser[] parametersParsers) where TEvent : ChannelEvent
		{
			return EventParserProvider.GetEventParser(delegate(object[] x)
			{
				TEvent tevent = Activator.CreateInstance<TEvent>();
				tevent.Channel = (FourBitNumber)x[0];
				byte[] array = (byte[])typeof(ChannelEvent).GetField("_parameters", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tevent);
				for (int i = 0; i < parametersParsers.Length; i++)
				{
					array[i] = Convert.ToByte(x[i + 1]);
				}
				return tevent;
			}, new ParameterParser[] { TypeParser.FourBitNumber }.Concat(parametersParsers).ToArray<ParameterParser>());
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000057B5 File Offset: 0x000039B5
		private static EventParser GetEventParser(Func<object[], MidiEvent> eventCreator, params ParameterParser[] parametersParsers)
		{
			return delegate(string[] p, MidiFileCsvConversionSettings s)
			{
				if (p.Length < parametersParsers.Length)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.", null);
				}
				List<object> list = new List<object>(parametersParsers.Length);
				for (int i = 0; i < parametersParsers.Length; i++)
				{
					ParameterParser parameterParser = parametersParsers[i];
					try
					{
						object obj = parameterParser(p[i], s);
						list.Add(obj);
					}
					catch
					{
						CsvError.ThrowBadFormat(string.Format("Parameter ({0}) is invalid.", i), null);
					}
				}
				return eventCreator(list.ToArray());
			};
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000057D8 File Offset: 0x000039D8
		// Note: this type is marked as 'beforefieldinit'.
		static EventParserProvider()
		{
			Dictionary<string, EventParser> dictionary = new Dictionary<string, EventParser>(StringComparer.OrdinalIgnoreCase);
			dictionary["Title_t"] = EventParserProvider.GetTextEventParser<SequenceTrackNameEvent>();
			dictionary["Copyright_t"] = EventParserProvider.GetTextEventParser<CopyrightNoticeEvent>();
			dictionary["Instrument_name_t"] = EventParserProvider.GetTextEventParser<InstrumentNameEvent>();
			dictionary["Marker_t"] = EventParserProvider.GetTextEventParser<MarkerEvent>();
			dictionary["Cue_point_t"] = EventParserProvider.GetTextEventParser<CuePointEvent>();
			dictionary["Lyric_t"] = EventParserProvider.GetTextEventParser<LyricEvent>();
			dictionary["Text_t"] = EventParserProvider.GetTextEventParser<TextEvent>();
			dictionary["Sequence_number"] = EventParserProvider.GetEventParser((object[] x) => new SequenceNumberEvent((ushort)x[0]), new ParameterParser[] { TypeParser.UShort });
			dictionary["MIDI_port"] = EventParserProvider.GetEventParser((object[] x) => new PortPrefixEvent((byte)x[0]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Channel_prefix"] = EventParserProvider.GetEventParser((object[] x) => new ChannelPrefixEvent((byte)x[0]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Time_signature"] = EventParserProvider.GetEventParser((object[] x) => new TimeSignatureEvent((byte)x[0], (byte)Math.Pow(2.0, (double)((byte)x[1])), (byte)x[2], (byte)x[3]), new ParameterParser[]
			{
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte
			});
			dictionary["Key_signature"] = EventParserProvider.GetEventParser((object[] x) => new KeySignatureEvent((sbyte)x[0], (byte)x[1]), new ParameterParser[]
			{
				TypeParser.SByte,
				TypeParser.Byte
			});
			dictionary["Tempo"] = EventParserProvider.GetEventParser((object[] x) => new SetTempoEvent((long)x[0]), new ParameterParser[] { TypeParser.Long });
			dictionary["SMPTE_offset"] = EventParserProvider.GetEventParser((object[] x) => new SmpteOffsetEvent(SmpteData.GetFormat((byte)x[0]), SmpteData.GetHours((byte)x[0]), (byte)x[1], (byte)x[2], (byte)x[3], (byte)x[4]), new ParameterParser[]
			{
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte
			});
			dictionary["Sequencer_specific"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new SequencerSpecificEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			dictionary["Unknown_meta_event"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new UnknownMetaEvent((byte)x[0], (byte[])x[2]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Note_on_c"] = EventParserProvider.GetNoteEventParser<NoteOnEvent>(2);
			dictionary["Note_off_c"] = EventParserProvider.GetNoteEventParser<NoteOffEvent>(2);
			dictionary["Pitch_bend_c"] = EventParserProvider.GetEventParser((object[] x) => new PitchBendEvent((ushort)x[1])
			{
				Channel = (FourBitNumber)x[0]
			}, new ParameterParser[]
			{
				TypeParser.FourBitNumber,
				TypeParser.UShort
			});
			dictionary["Control_c"] = EventParserProvider.GetChannelEventParser<ControlChangeEvent>(2);
			dictionary["Program_c"] = EventParserProvider.GetChannelEventParser<ProgramChangeEvent>(1);
			dictionary["Channel_aftertouch_c"] = EventParserProvider.GetChannelEventParser<ChannelAftertouchEvent>(1);
			dictionary["Poly_aftertouch_c"] = EventParserProvider.GetNoteEventParser<ChannelAftertouchEvent>(2);
			dictionary["System_exclusive"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			dictionary["System_exclusive_packet"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			EventParserProvider.EventsParsers_MidiCsv = dictionary;
			dictionary = new Dictionary<string, EventParser>(StringComparer.OrdinalIgnoreCase);
			dictionary["Sequence/Track Name"] = EventParserProvider.GetTextEventParser<SequenceTrackNameEvent>();
			dictionary["Copyright Notice"] = EventParserProvider.GetTextEventParser<CopyrightNoticeEvent>();
			dictionary["Instrument Name"] = EventParserProvider.GetTextEventParser<InstrumentNameEvent>();
			dictionary["Marker"] = EventParserProvider.GetTextEventParser<MarkerEvent>();
			dictionary["Cue Point"] = EventParserProvider.GetTextEventParser<CuePointEvent>();
			dictionary["Lyric"] = EventParserProvider.GetTextEventParser<LyricEvent>();
			dictionary["Text"] = EventParserProvider.GetTextEventParser<TextEvent>();
			dictionary["Sequence Number"] = EventParserProvider.GetEventParser((object[] x) => new SequenceNumberEvent((ushort)x[0]), new ParameterParser[] { TypeParser.UShort });
			dictionary["Port Prefix"] = EventParserProvider.GetEventParser((object[] x) => new PortPrefixEvent((byte)x[0]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Channel Prefix"] = EventParserProvider.GetEventParser((object[] x) => new ChannelPrefixEvent((byte)x[0]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Time Signature"] = EventParserProvider.GetEventParser((object[] x) => new TimeSignatureEvent((byte)x[0], (byte)x[1], (byte)x[2], (byte)x[3]), new ParameterParser[]
			{
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte
			});
			dictionary["Key Signature"] = EventParserProvider.GetEventParser((object[] x) => new KeySignatureEvent((sbyte)x[0], (byte)x[1]), new ParameterParser[]
			{
				TypeParser.SByte,
				TypeParser.Byte
			});
			dictionary["Set Tempo"] = EventParserProvider.GetEventParser((object[] x) => new SetTempoEvent((long)x[0]), new ParameterParser[] { TypeParser.Long });
			dictionary["SMPTE Offset"] = EventParserProvider.GetEventParser((object[] x) => new SmpteOffsetEvent(SmpteData.GetFormat((byte)x[0]), SmpteData.GetHours((byte)x[0]), (byte)x[1], (byte)x[2], (byte)x[3], (byte)x[4]), new ParameterParser[]
			{
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte,
				TypeParser.Byte
			});
			dictionary["Sequencer Specific"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new SequencerSpecificEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			dictionary["Unknown Meta"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new UnknownMetaEvent((byte)x[0], (byte[])x[2]), new ParameterParser[] { TypeParser.Byte });
			dictionary["Note On"] = EventParserProvider.GetNoteEventParser<NoteOnEvent>(2);
			dictionary["Note Off"] = EventParserProvider.GetNoteEventParser<NoteOffEvent>(2);
			dictionary["Pitch Bend"] = EventParserProvider.GetEventParser((object[] x) => new PitchBendEvent((ushort)x[1])
			{
				Channel = (FourBitNumber)x[0]
			}, new ParameterParser[]
			{
				TypeParser.FourBitNumber,
				TypeParser.UShort
			});
			dictionary["Control Change"] = EventParserProvider.GetChannelEventParser<ControlChangeEvent>(2);
			dictionary["Program Change"] = EventParserProvider.GetChannelEventParser<ProgramChangeEvent>(1);
			dictionary["Channel Aftertouch"] = EventParserProvider.GetChannelEventParser<ChannelAftertouchEvent>(1);
			dictionary["Note Aftertouch"] = EventParserProvider.GetNoteEventParser<ChannelAftertouchEvent>(2);
			dictionary["System Exclusive"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			dictionary["System Exclusive Packet"] = EventParserProvider.GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]), Array.Empty<ParameterParser>());
			EventParserProvider.EventsParsers_DryWetMidi = dictionary;
		}

		// Token: 0x0400008D RID: 141
		private static readonly Dictionary<string, EventParser> EventsParsers_MidiCsv;

		// Token: 0x0400008E RID: 142
		private static readonly Dictionary<string, EventParser> EventsParsers_DryWetMidi;
	}
}
