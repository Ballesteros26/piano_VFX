using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x02000083 RID: 131
	internal static class NoteNameParser
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000E029 File Offset: 0x0000C229
		internal static IEnumerable<string> GetPatterns()
		{
			return NoteNameParser.Patterns;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E030 File Offset: 0x0000C230
		internal static ParsingResult TryParse(string input, out NoteName noteName)
		{
			noteName = NoteName.C;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, NoteNameParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			Group group = match.Groups["n"];
			int num = (int)((NoteName)Enum.Parse(typeof(NoteName), group.Value));
			Group group2 = match.Groups["a"];
			if (group2.Success)
			{
				foreach (object obj in group2.Captures)
				{
					string value = ((Capture)obj).Value;
					if (string.Equals(value, "#", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "Sharp", StringComparison.OrdinalIgnoreCase))
					{
						num++;
					}
					else if (string.Equals(value, "b", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "Flat", StringComparison.OrdinalIgnoreCase))
					{
						num--;
					}
				}
			}
			num %= 12;
			if (num < 0)
			{
				num = 12 + num;
			}
			noteName = (NoteName)num;
			return ParsingResult.Parsed;
		}

		// Token: 0x0400054E RID: 1358
		private const string NoteLetterGroupName = "n";

		// Token: 0x0400054F RID: 1359
		private const string AccidentalGroupName = "a";

		// Token: 0x04000550 RID: 1360
		private static readonly string NoteNameGroup = "(?<n>C|D|E|F|G|A|B)";

		// Token: 0x04000551 RID: 1361
		private static readonly string AccidentalGroup = "((?<a>" + Regex.Escape("#") + "|Sharp|b|Flat)\\s*)+?";

		// Token: 0x04000552 RID: 1362
		private static readonly string[] Patterns = new string[]
		{
			NoteNameParser.NoteNameGroup + "\\s*" + NoteNameParser.AccidentalGroup,
			NoteNameParser.NoteNameGroup ?? ""
		};
	}
}
