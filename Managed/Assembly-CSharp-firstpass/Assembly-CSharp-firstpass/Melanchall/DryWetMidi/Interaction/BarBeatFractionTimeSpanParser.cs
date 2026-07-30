using System;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000BB RID: 187
	internal static class BarBeatFractionTimeSpanParser
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x000148D4 File Offset: 0x00012AD4
		internal static ParsingResult TryParse(string input, out BarBeatFractionTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, BarBeatFractionTimeSpanParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			long num;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "bars", 0L, out num))
			{
				return ParsingResult.Error("Bars number is out of range.");
			}
			double num2;
			if (!ParsingUtilities.ParseNonnegativeDouble(match, "beats", 0.0, out num2))
			{
				return ParsingResult.Error("Beats number is out of range.");
			}
			timeSpan = new BarBeatFractionTimeSpan(num, num2);
			return ParsingResult.Parsed;
		}

		// Token: 0x040006AA RID: 1706
		private const string BarsGroupName = "bars";

		// Token: 0x040006AB RID: 1707
		private const string BeatsGroupName = "beats";

		// Token: 0x040006AC RID: 1708
		private static readonly string BarsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("bars");

		// Token: 0x040006AD RID: 1709
		private static readonly string BeatsGroup = ParsingUtilities.GetNonnegativeDoubleNumberGroup("beats");

		// Token: 0x040006AE RID: 1710
		private static readonly string Divider = Regex.Escape("_");

		// Token: 0x040006AF RID: 1711
		private static readonly string[] Patterns = new string[] { string.Concat(new string[]
		{
			BarBeatFractionTimeSpanParser.BarsGroup,
			"\\s*",
			BarBeatFractionTimeSpanParser.Divider,
			"\\s*",
			BarBeatFractionTimeSpanParser.BeatsGroup
		}) };

		// Token: 0x040006B0 RID: 1712
		private const string BarsIsOutOfRange = "Bars number is out of range.";

		// Token: 0x040006B1 RID: 1713
		private const string BeatsIsOutOfRange = "Beats number is out of range.";
	}
}
