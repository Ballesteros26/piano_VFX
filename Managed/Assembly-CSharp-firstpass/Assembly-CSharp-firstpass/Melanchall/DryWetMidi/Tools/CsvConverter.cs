using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000019 RID: 25
	public sealed class CsvConverter
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00004B1C File Offset: 0x00002D1C
		public void ConvertMidiFileToCsv(MidiFile midiFile, string filePath, bool overwriteFile = false, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			using (FileStream fileStream = FileUtilities.OpenFileForWrite(filePath, overwriteFile))
			{
				this.ConvertMidiFileToCsv(midiFile, fileStream, settings);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004B64 File Offset: 0x00002D64
		public void ConvertMidiFileToCsv(MidiFile midiFile, Stream stream, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			MidiFileToCsvConverter.ConvertToCsv(midiFile, stream, settings ?? new MidiFileCsvConversionSettings());
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public MidiFile ConvertCsvToMidiFile(string filePath, MidiFileCsvConversionSettings settings = null)
		{
			MidiFile midiFile;
			using (FileStream fileStream = FileUtilities.OpenFileForRead(filePath))
			{
				midiFile = this.ConvertCsvToMidiFile(fileStream, settings);
			}
			return midiFile;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004BEC File Offset: 0x00002DEC
		public MidiFile ConvertCsvToMidiFile(Stream stream, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			return CsvToMidiFileConverter.ConvertToMidiFile(stream, settings ?? new MidiFileCsvConversionSettings());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004C24 File Offset: 0x00002E24
		public void ConvertNotesToCsv(IEnumerable<Note> notes, string filePath, TempoMap tempoMap, bool overwriteFile = false, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using (FileStream fileStream = FileUtilities.OpenFileForWrite(filePath, overwriteFile))
			{
				this.ConvertNotesToCsv(notes, fileStream, tempoMap, settings);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004C78 File Offset: 0x00002E78
		public void ConvertNotesToCsv(IEnumerable<Note> notes, Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			NotesToCsvConverter.ConvertToCsv(notes, stream, tempoMap, settings ?? new NoteCsvConversionSettings());
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public IEnumerable<Note> ConvertCsvToNotes(string filePath, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			IEnumerable<Note> enumerable;
			using (FileStream fileStream = FileUtilities.OpenFileForRead(filePath))
			{
				enumerable = this.ConvertCsvToNotes(fileStream, tempoMap, settings).ToList<Note>();
			}
			return enumerable;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004D20 File Offset: 0x00002F20
		public IEnumerable<Note> ConvertCsvToNotes(Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			return CsvToNotesConverter.ConvertToNotes(stream, tempoMap, settings ?? new NoteCsvConversionSettings());
		}
	}
}
