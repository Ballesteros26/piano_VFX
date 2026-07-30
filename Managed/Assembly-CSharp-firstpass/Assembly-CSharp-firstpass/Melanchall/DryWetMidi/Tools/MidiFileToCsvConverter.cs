using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200002D RID: 45
	internal static class MidiFileToCsvConverter
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00006E70 File Offset: 0x00005070
		public static void ConvertToCsv(MidiFile midiFile, Stream stream, MidiFileCsvConversionSettings settings)
		{
			using (CsvWriter csvWriter = new CsvWriter(stream, settings.CsvSettings))
			{
				int num = 0;
				TempoMap tempoMap = midiFile.GetTempoMap();
				MidiFileToCsvConverter.WriteHeader(csvWriter, midiFile, settings, tempoMap);
				foreach (TrackChunk trackChunk in midiFile.GetTrackChunks())
				{
					MidiFileToCsvConverter.WriteTrackChunkStart(csvWriter, num, settings, tempoMap);
					long num2 = 0L;
					IEnumerable<TimedEvent> timedEvents = trackChunk.GetTimedEvents();
					IEnumerable<ITimedObject> enumerable;
					if (settings.CsvLayout != MidiFileCsvLayout.MidiCsv && settings.NoteFormat != NoteFormat.Events)
					{
						enumerable = timedEvents.GetTimedEventsAndNotes();
					}
					else
					{
						IEnumerable<ITimedObject> enumerable2 = timedEvents;
						enumerable = enumerable2;
					}
					foreach (ITimedObject timedObject in enumerable)
					{
						num2 = timedObject.Time;
						TimedEvent timedEvent = timedObject as TimedEvent;
						if (timedEvent != null)
						{
							MidiFileToCsvConverter.WriteTimedEvent(timedEvent, csvWriter, num, num2, settings, tempoMap);
						}
						else
						{
							Note note = timedObject as Note;
							if (note != null)
							{
								MidiFileToCsvConverter.WriteNote(note, csvWriter, num, num2, settings, tempoMap);
							}
						}
					}
					MidiFileToCsvConverter.WriteTrackChunkEnd(csvWriter, num, num2, settings, tempoMap);
					num++;
				}
				MidiFileToCsvConverter.WriteFileEnd(csvWriter, settings, tempoMap);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006FB4 File Offset: 0x000051B4
		private static void WriteNote(Note note, CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			object obj = ((settings.NoteNumberFormat == NoteNumberFormat.NoteNumber) ? note.NoteNumber : note);
			ITimeSpan timeSpan = TimeConverter.ConvertTo(note.Length, settings.NoteLengthType, tempoMap);
			MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(trackNumber), new long?(time), "Note", settings, tempoMap, new object[] { note.Channel, obj, timeSpan, note.Velocity, note.OffVelocity });
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007044 File Offset: 0x00005244
		private static void WriteTimedEvent(TimedEvent timedEvent, CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiEvent @event = timedEvent.Event;
			Type type = @event.GetType();
			string text = EventNameGetterProvider.Get(type, settings.CsvLayout)(@event);
			object[] array = EventParametersGetterProvider.Get(type)(@event, settings);
			MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(trackNumber), new long?(time), text, settings, tempoMap, array);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007098 File Offset: 0x00005298
		private static void WriteHeader(CsvWriter csvWriter, MidiFile midiFile, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileFormat? midiFileFormat = null;
			try
			{
				midiFileFormat = new MidiFileFormat?(midiFile.OriginalFormat);
			}
			catch
			{
			}
			int num = midiFile.GetTrackChunks().Count<TrackChunk>();
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout == MidiFileCsvLayout.DryWetMidi)
			{
				MidiFileToCsvConverter.WriteRecord(csvWriter, null, null, "Header", settings, tempoMap, new object[]
				{
					midiFileFormat,
					midiFile.TimeDivision.ToInt16()
				});
				return;
			}
			if (csvLayout != MidiFileCsvLayout.MidiCsv)
			{
				return;
			}
			MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(0), new long?(0L), "Header", settings, tempoMap, new object[]
			{
				(int)((midiFileFormat != null) ? midiFileFormat.Value : ((num > 1) ? MidiFileFormat.MultiTrack : MidiFileFormat.SingleTrack)),
				num,
				midiFile.TimeDivision.ToInt16()
			});
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000718C File Offset: 0x0000538C
		private static void WriteTrackChunkStart(CsvWriter csvWriter, int trackNumber, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout != MidiFileCsvLayout.DryWetMidi && csvLayout == MidiFileCsvLayout.MidiCsv)
			{
				MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(trackNumber), new long?(0L), "Start_track", settings, tempoMap, Array.Empty<object>());
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000071C8 File Offset: 0x000053C8
		private static void WriteTrackChunkEnd(CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout == MidiFileCsvLayout.DryWetMidi)
			{
				return;
			}
			if (csvLayout != MidiFileCsvLayout.MidiCsv)
			{
				return;
			}
			MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(trackNumber), new long?(time), "End_track", settings, tempoMap, Array.Empty<object>());
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007204 File Offset: 0x00005404
		private static void WriteFileEnd(CsvWriter csvWriter, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout == MidiFileCsvLayout.DryWetMidi)
			{
				return;
			}
			if (csvLayout != MidiFileCsvLayout.MidiCsv)
			{
				return;
			}
			MidiFileToCsvConverter.WriteRecord(csvWriter, new int?(0), new long?(0L), "End_of_file", settings, tempoMap, Array.Empty<object>());
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007240 File Offset: 0x00005440
		private static void WriteRecord(CsvWriter csvWriter, int? trackNumber, long? time, string type, MidiFileCsvConversionSettings settings, TempoMap tempoMap, params object[] parameters)
		{
			ITimeSpan timeSpan = ((time == null) ? null : TimeConverter.ConvertTo(time.Value, settings.TimeType, tempoMap));
			IEnumerable<object> enumerable = parameters.SelectMany(new Func<object, IEnumerable<object>>(MidiFileToCsvConverter.ProcessParameter));
			csvWriter.WriteRecord(new object[] { trackNumber, timeSpan, type }.Concat(enumerable));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000072A8 File Offset: 0x000054A8
		private static object[] ProcessParameter(object parameter)
		{
			if (parameter == null)
			{
				return new object[] { string.Empty };
			}
			byte[] array = parameter as byte[];
			if (array != null)
			{
				return array.OfType<object>().ToArray<object>();
			}
			string text = parameter as string;
			if (text != null)
			{
				parameter = CsvUtilities.EscapeString(text);
			}
			return new object[] { parameter };
		}
	}
}
