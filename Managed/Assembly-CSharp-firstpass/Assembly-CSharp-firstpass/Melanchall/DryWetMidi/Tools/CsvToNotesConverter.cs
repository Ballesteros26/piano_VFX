using System;
using System.Collections.Generic;
using System.IO;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200002E RID: 46
	internal static class CsvToNotesConverter
	{
		// Token: 0x06000128 RID: 296 RVA: 0x000072F9 File Offset: 0x000054F9
		public static IEnumerable<Melanchall.DryWetMidi.Interaction.Note> ConvertToNotes(Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings)
		{
			using (CsvReader csvReader = new CsvReader(stream, settings.CsvSettings))
			{
				CsvRecord csvRecord;
				while ((csvRecord = csvReader.ReadRecord()) != null)
				{
					string[] values = csvRecord.Values;
					if (values.Length < 6)
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Missing required parameters.", null);
					}
					ITimeSpan timeSpan;
					if (!TimeSpanUtilities.TryParse(values[0], settings.TimeType, out timeSpan))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid time.", null);
					}
					FourBitNumber fourBitNumber;
					if (!FourBitNumber.TryParse(values[1], out fourBitNumber))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid channel.", null);
					}
					SevenBitNumber sevenBitNumber;
					if (!CsvToNotesConverter.TryParseNoteNumber(values[2], settings.NoteNumberFormat, out sevenBitNumber))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid note number or letter.", null);
					}
					ITimeSpan timeSpan2;
					if (!TimeSpanUtilities.TryParse(values[3], settings.NoteLengthType, out timeSpan2))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid length.", null);
					}
					SevenBitNumber sevenBitNumber2;
					if (!SevenBitNumber.TryParse(values[4], out sevenBitNumber2))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid velocity.", null);
					}
					SevenBitNumber sevenBitNumber3;
					if (!SevenBitNumber.TryParse(values[5], out sevenBitNumber3))
					{
						CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid off velocity.", null);
					}
					long num = TimeConverter.ConvertFrom(timeSpan, tempoMap);
					long num2 = LengthConverter.ConvertFrom(timeSpan2, num, tempoMap);
					yield return new Melanchall.DryWetMidi.Interaction.Note(sevenBitNumber, num2, num)
					{
						Channel = fourBitNumber,
						Velocity = sevenBitNumber2,
						OffVelocity = sevenBitNumber3
					};
				}
			}
			CsvReader csvReader = null;
			yield break;
			yield break;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007318 File Offset: 0x00005518
		public static bool TryParseNoteNumber(string input, NoteNumberFormat noteNumberFormat, out SevenBitNumber result)
		{
			result = default(SevenBitNumber);
			if (noteNumberFormat == NoteNumberFormat.NoteNumber)
			{
				return SevenBitNumber.TryParse(input, out result);
			}
			if (noteNumberFormat != NoteNumberFormat.Letter)
			{
				return false;
			}
			Melanchall.DryWetMidi.MusicTheory.Note note;
			if (!Melanchall.DryWetMidi.MusicTheory.Note.TryParse(input, out note))
			{
				return false;
			}
			result = note.NoteNumber;
			return true;
		}
	}
}
