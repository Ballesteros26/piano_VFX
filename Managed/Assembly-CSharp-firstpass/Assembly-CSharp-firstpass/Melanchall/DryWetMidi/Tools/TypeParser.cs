using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.MusicTheory;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x02000023 RID: 35
	internal static class TypeParser
	{
		// Token: 0x0400009E RID: 158
		public static readonly ParameterParser Byte = (string p, MidiFileCsvConversionSettings s) => byte.Parse(p);

		// Token: 0x0400009F RID: 159
		public static readonly ParameterParser SByte = (string p, MidiFileCsvConversionSettings s) => sbyte.Parse(p);

		// Token: 0x040000A0 RID: 160
		public static readonly ParameterParser Long = (string p, MidiFileCsvConversionSettings s) => long.Parse(p);

		// Token: 0x040000A1 RID: 161
		public static readonly ParameterParser UShort = (string p, MidiFileCsvConversionSettings s) => ushort.Parse(p);

		// Token: 0x040000A2 RID: 162
		public static readonly ParameterParser String = (string p, MidiFileCsvConversionSettings s) => CsvUtilities.UnescapeString(p);

		// Token: 0x040000A3 RID: 163
		public static readonly ParameterParser Int = (string p, MidiFileCsvConversionSettings s) => int.Parse(p);

		// Token: 0x040000A4 RID: 164
		public static readonly ParameterParser FourBitNumber = (string p, MidiFileCsvConversionSettings s) => (FourBitNumber)byte.Parse(p);

		// Token: 0x040000A5 RID: 165
		public static readonly ParameterParser SevenBitNumber = (string p, MidiFileCsvConversionSettings s) => (SevenBitNumber)byte.Parse(p);

		// Token: 0x040000A6 RID: 166
		public static readonly ParameterParser NoteNumber = delegate(string p, MidiFileCsvConversionSettings s)
		{
			NoteNumberFormat noteNumberFormat = s.NoteNumberFormat;
			if (noteNumberFormat == NoteNumberFormat.NoteNumber)
			{
				return TypeParser.SevenBitNumber(p, s);
			}
			if (noteNumberFormat != NoteNumberFormat.Letter)
			{
				return null;
			}
			return Note.Parse(p).NoteNumber;
		};
	}
}
