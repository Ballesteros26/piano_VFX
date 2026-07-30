using System;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000BC RID: 188
	internal static class BarBeatTicksTimeSpanParser
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x000149D4 File Offset: 0x00012BD4
		internal static ParsingResult TryParse(string input, out BarBeatTicksTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, BarBeatTicksTimeSpanParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			long num;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "bars", 0L, out num))
			{
				return ParsingResult.Error("Bars number is out of range.");
			}
			long num2;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "beats", 0L, out num2))
			{
				return ParsingResult.Error("Beats number is out of range.");
			}
			long num3;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "ticks", 0L, out num3))
			{
				return ParsingResult.Error("Ticks number is out of range.");
			}
			timeSpan = new BarBeatTicksTimeSpan(num, num2, num3);
			return ParsingResult.Parsed;
		}

		// Token: 0x040006B2 RID: 1714
		private const string BarsGroupName = "bars";

		// Token: 0x040006B3 RID: 1715
		private const string BeatsGroupName = "beats";

		// Token: 0x040006B4 RID: 1716
		private const string TicksGroupName = "ticks";

		// Token: 0x040006B5 RID: 1717
		private static readonly string BarsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("bars");

		// Token: 0x040006B6 RID: 1718
		private static readonly string BeatsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("beats");

		// Token: 0x040006B7 RID: 1719
		private static readonly string TicksGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ticks");

		// Token: 0x040006B8 RID: 1720
		private static readonly string Divider = Regex.Escape(".");

		// Token: 0x040006B9 RID: 1721
		private static readonly string[] Patterns = new string[] { string.Concat(new string[]
		{
			BarBeatTicksTimeSpanParser.BarsGroup,
			"\\s*",
			BarBeatTicksTimeSpanParser.Divider,
			"\\s*",
			BarBeatTicksTimeSpanParser.BeatsGroup,
			"\\s*",
			BarBeatTicksTimeSpanParser.Divider,
			"\\s*",
			BarBeatTicksTimeSpanParser.TicksGroup
		}) };

		// Token: 0x040006BA RID: 1722
		private const string BarsIsOutOfRange = "Bars number is out of range.";

		// Token: 0x040006BB RID: 1723
		private const string BeatsIsOutOfRange = "Beats number is out of range.";

		// Token: 0x040006BC RID: 1724
		private const string TicksIsOutOfRange = "Ticks number is out of range.";
	}
}
