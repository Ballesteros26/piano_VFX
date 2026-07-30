using System;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000BE RID: 190
	internal static class MidiTimeSpanParser
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00014F5C File Offset: 0x0001315C
		internal static ParsingResult TryParse(string input, out MidiTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, MidiTimeSpanParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			long num;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "ts", 0L, out num))
			{
				return ParsingResult.Error("Time span is out of range.");
			}
			timeSpan = new MidiTimeSpan(num);
			return ParsingResult.Parsed;
		}

		// Token: 0x040006CF RID: 1743
		private const string TimeSpanGroupName = "ts";

		// Token: 0x040006D0 RID: 1744
		private static readonly string TimeSpanGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ts");

		// Token: 0x040006D1 RID: 1745
		private static readonly string[] Patterns = new string[] { MidiTimeSpanParser.TimeSpanGroup ?? "" };

		// Token: 0x040006D2 RID: 1746
		private const string OutOfRange = "Time span is out of range.";
	}
}
