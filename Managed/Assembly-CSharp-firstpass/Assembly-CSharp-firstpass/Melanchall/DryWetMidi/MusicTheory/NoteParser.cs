using System;
using System.Linq;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000084 RID: 132
	internal static class NoteParser
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		internal static ParsingResult TryParse(string input, out Note note)
		{
			note = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, NoteParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["n"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			int num;
			if (!ParsingUtilities.ParseInt(match, "o", Octave.Middle.Number, out num))
			{
				return ParsingResult.Error("Octave number is out of range.");
			}
			if (!NoteUtilities.IsNoteValid(noteName, num))
			{
				return ParsingResult.Error("Note is out of range.");
			}
			note = Note.Get(noteName, num);
			return ParsingResult.Parsed;
		}

		// Token: 0x04000553 RID: 1363
		private const string NoteNameGroupName = "n";

		// Token: 0x04000554 RID: 1364
		private const string OctaveGroupName = "o";

		// Token: 0x04000555 RID: 1365
		private static readonly string OctaveGroup = ParsingUtilities.GetIntegerNumberGroup("o");

		// Token: 0x04000556 RID: 1366
		private static readonly string[] Patterns = (from p in NoteNameParser.GetPatterns()
			select "(?<n>" + p + ")\\s*" + NoteParser.OctaveGroup).ToArray<string>();

		// Token: 0x04000557 RID: 1367
		private const string OctaveIsOutOfRange = "Octave number is out of range.";

		// Token: 0x04000558 RID: 1368
		private const string NoteIsOutOfRange = "Note is out of range.";
	}
}
