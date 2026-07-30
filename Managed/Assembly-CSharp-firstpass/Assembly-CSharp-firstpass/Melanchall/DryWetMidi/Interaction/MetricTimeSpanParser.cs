using System;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000BD RID: 189
	internal static class MetricTimeSpanParser
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00014B18 File Offset: 0x00012D18
		internal static ParsingResult TryParse(string input, out MetricTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, MetricTimeSpanParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			int num;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "h", 0, out num))
			{
				return ParsingResult.Error("Hours number is out of range.");
			}
			int num2;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "m", 0, out num2))
			{
				return ParsingResult.Error("Minutes number is out of range.");
			}
			int num3;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "s", 0, out num3))
			{
				return ParsingResult.Error("Seconds number is out of range.");
			}
			int num4;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "ms", 0, out num4))
			{
				return ParsingResult.Error("Milliseconds number is out of range.");
			}
			timeSpan = new MetricTimeSpan(num, num2, num3, num4);
			return ParsingResult.Parsed;
		}

		// Token: 0x040006BD RID: 1725
		private const string HoursGroupName = "h";

		// Token: 0x040006BE RID: 1726
		private const string MinutesGroupName = "m";

		// Token: 0x040006BF RID: 1727
		private const string SecondsGroupName = "s";

		// Token: 0x040006C0 RID: 1728
		private const string MillisecondsGroupName = "ms";

		// Token: 0x040006C1 RID: 1729
		private static readonly string HoursGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("h");

		// Token: 0x040006C2 RID: 1730
		private static readonly string MinutesGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("m");

		// Token: 0x040006C3 RID: 1731
		private static readonly string SecondsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("s");

		// Token: 0x040006C4 RID: 1732
		private static readonly string MillisecondsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ms");

		// Token: 0x040006C5 RID: 1733
		private static readonly string LetteredHoursGroup = MetricTimeSpanParser.HoursGroup + "\\s*h";

		// Token: 0x040006C6 RID: 1734
		private static readonly string LetteredMinutesGroup = MetricTimeSpanParser.MinutesGroup + "\\s*m";

		// Token: 0x040006C7 RID: 1735
		private static readonly string LetteredSecondsGroup = MetricTimeSpanParser.SecondsGroup + "\\s*s";

		// Token: 0x040006C8 RID: 1736
		private static readonly string LetteredMillisecondsGroup = MetricTimeSpanParser.MillisecondsGroup + "\\s*ms";

		// Token: 0x040006C9 RID: 1737
		private static readonly string Divider = Regex.Escape(":");

		// Token: 0x040006CA RID: 1738
		private static readonly string[] Patterns = new string[]
		{
			string.Concat(new string[]
			{
				MetricTimeSpanParser.HoursGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.MinutesGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.SecondsGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.MillisecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.HoursGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.MinutesGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.SecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.MinutesGroup,
				"\\s*",
				MetricTimeSpanParser.Divider,
				"\\s*",
				MetricTimeSpanParser.SecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.LetteredHoursGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMinutesGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredSecondsGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMillisecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.LetteredHoursGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMinutesGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredSecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.LetteredHoursGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMinutesGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMillisecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.LetteredHoursGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredSecondsGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMillisecondsGroup
			}),
			string.Concat(new string[]
			{
				MetricTimeSpanParser.LetteredMinutesGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredSecondsGroup,
				"\\s*",
				MetricTimeSpanParser.LetteredMillisecondsGroup
			}),
			MetricTimeSpanParser.LetteredHoursGroup + "\\s*" + MetricTimeSpanParser.LetteredMinutesGroup,
			MetricTimeSpanParser.LetteredHoursGroup + "\\s*" + MetricTimeSpanParser.LetteredSecondsGroup,
			MetricTimeSpanParser.LetteredHoursGroup + "\\s*" + MetricTimeSpanParser.LetteredMillisecondsGroup,
			MetricTimeSpanParser.LetteredMinutesGroup + "\\s*" + MetricTimeSpanParser.LetteredSecondsGroup,
			MetricTimeSpanParser.LetteredMinutesGroup + "\\s*" + MetricTimeSpanParser.LetteredMillisecondsGroup,
			MetricTimeSpanParser.LetteredSecondsGroup + "\\s*" + MetricTimeSpanParser.LetteredMillisecondsGroup,
			MetricTimeSpanParser.LetteredHoursGroup,
			MetricTimeSpanParser.LetteredMinutesGroup,
			MetricTimeSpanParser.LetteredSecondsGroup,
			MetricTimeSpanParser.LetteredMillisecondsGroup
		};

		// Token: 0x040006CB RID: 1739
		private const string HoursIsOutOfRange = "Hours number is out of range.";

		// Token: 0x040006CC RID: 1740
		private const string MinutesIsOutOfRange = "Minutes number is out of range.";

		// Token: 0x040006CD RID: 1741
		private const string SecondsIsOutOfRange = "Seconds number is out of range.";

		// Token: 0x040006CE RID: 1742
		private const string MillisecondsIsOutOfRange = "Milliseconds number is out of range.";
	}
}
