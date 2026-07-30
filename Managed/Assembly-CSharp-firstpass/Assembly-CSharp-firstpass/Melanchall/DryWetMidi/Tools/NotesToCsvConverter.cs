using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000032 RID: 50
	internal static class NotesToCsvConverter
	{
		// Token: 0x06000133 RID: 307 RVA: 0x000073F0 File Offset: 0x000055F0
		public static void ConvertToCsv(IEnumerable<Note> notes, Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings)
		{
			using (CsvWriter csvWriter = new CsvWriter(stream, settings.CsvSettings))
			{
				foreach (Note note in notes.Where((Note n) => n != null))
				{
					csvWriter.WriteRecord(new object[]
					{
						note.TimeAs(settings.TimeType, tempoMap),
						note.Channel,
						NoteCsvConversionUtilities.FormatNoteNumber(note.NoteNumber, settings.NoteNumberFormat),
						note.LengthAs(settings.NoteLengthType, tempoMap),
						note.Velocity,
						note.OffVelocity
					});
				}
			}
		}
	}
}
