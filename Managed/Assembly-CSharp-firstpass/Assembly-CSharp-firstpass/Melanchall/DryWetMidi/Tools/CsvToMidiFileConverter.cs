using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200001B RID: 27
	internal static class CsvToMidiFileConverter
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public static MidiFile ConvertToMidiFile(Stream stream, MidiFileCsvConversionSettings settings)
		{
			MidiFile midiFile = new MidiFile();
			Dictionary<int, List<TimedMidiEvent>> dictionary = new Dictionary<int, List<TimedMidiEvent>>();
			using (CsvReader csvReader = new CsvReader(stream, settings.CsvSettings))
			{
				int num = 0;
				Record record;
				while ((record = CsvToMidiFileConverter.ReadRecord(csvReader, settings)) != null)
				{
					RecordType? recordType = CsvToMidiFileConverter.GetRecordType(record.RecordType, settings);
					if (recordType == null)
					{
						CsvError.ThrowBadFormat(num, "Unknown record.", null);
					}
					if (recordType != null)
					{
						switch (recordType.GetValueOrDefault())
						{
						case RecordType.Header:
						{
							HeaderChunk headerChunk = CsvToMidiFileConverter.ParseHeader(record, settings);
							midiFile.TimeDivision = headerChunk.TimeDivision;
							midiFile.OriginalFormat = (MidiFileFormat)headerChunk.FileFormat;
							break;
						}
						case RecordType.Event:
						{
							MidiEvent midiEvent = CsvToMidiFileConverter.ParseEvent(record, settings);
							int value = record.TrackNumber.Value;
							CsvToMidiFileConverter.AddTimedEvents(dictionary, value, new TimedMidiEvent[]
							{
								new TimedMidiEvent(record.Time, midiEvent)
							});
							break;
						}
						case RecordType.Note:
						{
							TimedMidiEvent[] array = CsvToMidiFileConverter.ParseNote(record, settings);
							int value2 = record.TrackNumber.Value;
							CsvToMidiFileConverter.AddTimedEvents(dictionary, value2, array);
							break;
						}
						}
					}
					num = record.LineNumber + 1;
				}
			}
			if (!dictionary.Keys.Any<int>())
			{
				return midiFile;
			}
			TempoMap tempoMap = CsvToMidiFileConverter.GetTempoMap(dictionary.Values.SelectMany((List<TimedMidiEvent> e) => e), midiFile.TimeDivision);
			TrackChunk[] array2 = new TrackChunk[dictionary.Keys.Max() + 1];
			Func<TimedMidiEvent, TimedEvent> <>9__1;
			for (int i = 0; i < array2.Length; i++)
			{
				TrackChunk[] array3 = array2;
				int num2 = i;
				List<TimedMidiEvent> list;
				TrackChunk trackChunk;
				if (!dictionary.TryGetValue(i, out list))
				{
					trackChunk = new TrackChunk();
				}
				else
				{
					IEnumerable<TimedMidiEvent> enumerable = list;
					Func<TimedMidiEvent, TimedEvent> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (TimedMidiEvent e) => new TimedEvent(e.Event, TimeConverter.ConvertFrom(e.Time, tempoMap)));
					}
					trackChunk = enumerable.Select(func).ToTrackChunk();
				}
				array3[num2] = trackChunk;
			}
			midiFile.Chunks.AddRange(array2);
			return midiFile;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005018 File Offset: 0x00003218
		private static void AddTimedEvents(Dictionary<int, List<TimedMidiEvent>> eventsMap, int trackChunkNumber, params TimedMidiEvent[] events)
		{
			List<TimedMidiEvent> list;
			if (!eventsMap.TryGetValue(trackChunkNumber, out list))
			{
				eventsMap.Add(trackChunkNumber, list = new List<TimedMidiEvent>());
			}
			list.AddRange(events);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005048 File Offset: 0x00003248
		private static TempoMap GetTempoMap(IEnumerable<TimedMidiEvent> timedMidiEvents, TimeDivision timeDivision)
		{
			TempoMap tempoMap;
			using (TempoMapManager tempoMapManager = new TempoMapManager(timeDivision))
			{
				foreach (TimedMidiEvent timedMidiEvent in timedMidiEvents.Where((TimedMidiEvent e) => e.Event is SetTempoEvent).OrderBy((TimedMidiEvent e) => e.Time, new TimeSpanComparer()))
				{
					SetTempoEvent setTempoEvent = (SetTempoEvent)timedMidiEvent.Event;
					tempoMapManager.SetTempo(timedMidiEvent.Time, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
				}
				foreach (TimedMidiEvent timedMidiEvent2 in timedMidiEvents.Where((TimedMidiEvent e) => e.Event is TimeSignatureEvent).OrderBy((TimedMidiEvent e) => e.Time, new TimeSpanComparer()))
				{
					TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)timedMidiEvent2.Event;
					tempoMapManager.SetTimeSignature(timedMidiEvent2.Time, new TimeSignature((int)timeSignatureEvent.Numerator, (int)timeSignatureEvent.Denominator));
				}
				tempoMap = tempoMapManager.TempoMap;
			}
			return tempoMap;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000051F4 File Offset: 0x000033F4
		private static RecordType? GetRecordType(string recordType, MidiFileCsvConversionSettings settings)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			Dictionary<string, RecordType> dictionary = ((csvLayout == MidiFileCsvLayout.DryWetMidi) ? CsvToMidiFileConverter.RecordTypes_DryWetMidi : CsvToMidiFileConverter.RecordTypes_MidiCsv);
			string[] array = EventsNamesProvider.Get(csvLayout);
			RecordType recordType2;
			if (dictionary.TryGetValue(recordType, out recordType2))
			{
				return new RecordType?(recordType2);
			}
			if (array.Contains(recordType, StringComparer.OrdinalIgnoreCase))
			{
				return new RecordType?(RecordType.Event);
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005250 File Offset: 0x00003450
		private static HeaderChunk ParseHeader(Record record, MidiFileCsvConversionSettings settings)
		{
			string[] parameters = record.Parameters;
			MidiFileFormat? midiFileFormat = null;
			short num = 0;
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout != MidiFileCsvLayout.DryWetMidi)
			{
				if (csvLayout == MidiFileCsvLayout.MidiCsv)
				{
					if (parameters.Length < 3)
					{
						CsvError.ThrowBadFormat(record.LineNumber, "Parameters count is invalid.", null);
					}
					ushort num2;
					if (ushort.TryParse(parameters[0], out num2) && Enum.IsDefined(typeof(MidiFileFormat), num2))
					{
						midiFileFormat = new MidiFileFormat?((MidiFileFormat)num2);
					}
					if (!short.TryParse(parameters[2], out num))
					{
						CsvError.ThrowBadFormat(record.LineNumber, "Invalid time division.", null);
					}
				}
			}
			else
			{
				if (parameters.Length < 2)
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Parameters count is invalid.", null);
				}
				MidiFileFormat midiFileFormat2;
				if (Enum.TryParse<MidiFileFormat>(parameters[0], true, out midiFileFormat2))
				{
					midiFileFormat = new MidiFileFormat?(midiFileFormat2);
				}
				if (!short.TryParse(parameters[1], out num))
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Invalid time division.", null);
				}
			}
			return new HeaderChunk
			{
				FileFormat = (ushort)((midiFileFormat != null) ? midiFileFormat.Value : ((MidiFileFormat)65535)),
				TimeDivision = TimeDivisionFactory.GetTimeDivision(num)
			};
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000535C File Offset: 0x0000355C
		private static MidiEvent ParseEvent(Record record, MidiFileCsvConversionSettings settings)
		{
			if (record.TrackNumber == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid track number.", null);
			}
			if (record.Time == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid time.", null);
			}
			EventParser eventParser = EventParserProvider.Get(record.RecordType, settings.CsvLayout);
			MidiEvent midiEvent;
			try
			{
				midiEvent = eventParser(record.Parameters, settings);
			}
			catch (FormatException ex)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid format of event record.", ex);
				midiEvent = null;
			}
			return midiEvent;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000053EC File Offset: 0x000035EC
		private static TimedMidiEvent[] ParseNote(Record record, MidiFileCsvConversionSettings settings)
		{
			if (record.TrackNumber == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid track number.", null);
			}
			if (record.Time == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid time.", null);
			}
			string[] parameters = record.Parameters;
			if (parameters.Length < 5)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid number of parameters provided.", null);
			}
			int num = -1;
			try
			{
				FourBitNumber fourBitNumber = (FourBitNumber)TypeParser.FourBitNumber(parameters[++num], settings);
				SevenBitNumber sevenBitNumber = (SevenBitNumber)TypeParser.NoteNumber(parameters[++num], settings);
				ITimeSpan timeSpan;
				TimeSpanUtilities.TryParse(parameters[++num], settings.NoteLengthType, out timeSpan);
				SevenBitNumber sevenBitNumber2 = (SevenBitNumber)TypeParser.SevenBitNumber(parameters[++num], settings);
				SevenBitNumber sevenBitNumber3 = (SevenBitNumber)TypeParser.SevenBitNumber(parameters[++num], settings);
				return new TimedMidiEvent[]
				{
					new TimedMidiEvent(record.Time, new NoteOnEvent(sevenBitNumber, sevenBitNumber2)
					{
						Channel = fourBitNumber
					}),
					new TimedMidiEvent(record.Time.Add(timeSpan, TimeSpanMode.TimeLength), new NoteOffEvent(sevenBitNumber, sevenBitNumber3)
					{
						Channel = fourBitNumber
					})
				};
			}
			catch
			{
				CsvError.ThrowBadFormat(record.LineNumber, string.Format("Parameter ({0}) is invalid.", num), null);
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000554C File Offset: 0x0000374C
		private static Record ReadRecord(CsvReader csvReader, MidiFileCsvConversionSettings settings)
		{
			CsvRecord csvRecord = csvReader.ReadRecord();
			if (csvRecord == null)
			{
				return null;
			}
			string[] values = csvRecord.Values;
			if (values.Length < 3)
			{
				CsvError.ThrowBadFormat(csvRecord.LineNumber, "Missing required parameters.", null);
			}
			int num2;
			int? num = (int.TryParse(values[0], out num2) ? new int?(num2) : null);
			ITimeSpan timeSpan;
			TimeSpanUtilities.TryParse(values[1], settings.TimeType, out timeSpan);
			string text = values[2];
			if (string.IsNullOrEmpty(text))
			{
				CsvError.ThrowBadFormat(csvRecord.LineNumber, "Record type isn't specified.", null);
			}
			string[] array = values.Skip(3).ToArray<string>();
			return new Record(csvRecord.LineNumber, num, timeSpan, text, array);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000055F0 File Offset: 0x000037F0
		// Note: this type is marked as 'beforefieldinit'.
		static CsvToMidiFileConverter()
		{
			Dictionary<string, RecordType> dictionary = new Dictionary<string, RecordType>(StringComparer.OrdinalIgnoreCase);
			dictionary["Header"] = RecordType.Header;
			dictionary["Note"] = RecordType.Note;
			CsvToMidiFileConverter.RecordTypes_DryWetMidi = dictionary;
			Dictionary<string, RecordType> dictionary2 = new Dictionary<string, RecordType>(StringComparer.OrdinalIgnoreCase);
			dictionary2["Header"] = RecordType.Header;
			dictionary2["Start_track"] = RecordType.TrackChunkStart;
			dictionary2["End_track"] = RecordType.TrackChunkEnd;
			dictionary2["End_of_file"] = RecordType.FileEnd;
			CsvToMidiFileConverter.RecordTypes_MidiCsv = dictionary2;
		}

		// Token: 0x0400008B RID: 139
		private static readonly Dictionary<string, RecordType> RecordTypes_DryWetMidi;

		// Token: 0x0400008C RID: 140
		private static readonly Dictionary<string, RecordType> RecordTypes_MidiCsv;
	}
}
