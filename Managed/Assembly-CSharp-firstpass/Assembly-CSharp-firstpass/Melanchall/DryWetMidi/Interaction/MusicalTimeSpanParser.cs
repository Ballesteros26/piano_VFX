using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Interaction
{
	// Token: 0x020000BF RID: 191
	internal static class MusicalTimeSpanParser
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00014FE8 File Offset: 0x000131E8
		internal static ParsingResult TryParse(string input, out MusicalTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, MusicalTimeSpanParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			long num;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "n", 1L, out num))
			{
				return ParsingResult.Error("Numerator is out of range.");
			}
			long num2;
			if (!ParsingUtilities.ParseNonnegativeLong(match, "d", 1L, out num2))
			{
				return ParsingResult.Error("Denominator is out of range.");
			}
			Group group = match.Groups["fm"];
			if (group.Success)
			{
				Tuple<int, int> tuple = MusicalTimeSpanParser.Fractions[group.Value];
				num = (long)tuple.Item1;
				num2 = (long)tuple.Item2;
			}
			int item;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "tn", 1, out item))
			{
				return ParsingResult.Error("Tuplet's notes count is out of range.");
			}
			int item2;
			if (!ParsingUtilities.ParseNonnegativeInt(match, "ts", 1, out item2))
			{
				return ParsingResult.Error("Tuplet's space size is out of range.");
			}
			Group group2 = match.Groups["tm"];
			if (group2.Success)
			{
				Tuple<int, int> tuple2 = MusicalTimeSpanParser.Tuplets[group2.Value];
				item = tuple2.Item1;
				item2 = tuple2.Item2;
			}
			Group group3 = match.Groups["dt"];
			int num3 = (group3.Success ? group3.Value.Length : 0);
			timeSpan = new MusicalTimeSpan(num, num2, true).Dotted(num3).Tuplet(item, item2);
			return ParsingResult.Parsed;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00015148 File Offset: 0x00013348
		private static string GetMnemonicGroup(string groupName, IEnumerable<string> mnemonics)
		{
			return string.Concat(new string[]
			{
				"(?<",
				groupName,
				">[",
				string.Join(string.Empty, mnemonics),
				"])"
			});
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00015180 File Offset: 0x00013380
		// Note: this type is marked as 'beforefieldinit'.
		static MusicalTimeSpanParser()
		{
			Dictionary<string, Tuple<int, int>> dictionary = new Dictionary<string, Tuple<int, int>>();
			dictionary["w"] = Tuple.Create<int, int>(1, 1);
			dictionary["h"] = Tuple.Create<int, int>(1, 2);
			dictionary["q"] = Tuple.Create<int, int>(1, 4);
			dictionary["e"] = Tuple.Create<int, int>(1, 8);
			dictionary["s"] = Tuple.Create<int, int>(1, 16);
			MusicalTimeSpanParser.Fractions = dictionary;
			Dictionary<string, Tuple<int, int>> dictionary2 = new Dictionary<string, Tuple<int, int>>();
			dictionary2["t"] = Tuple.Create<int, int>(3, 2);
			dictionary2["d"] = Tuple.Create<int, int>(2, 3);
			MusicalTimeSpanParser.Tuplets = dictionary2;
			MusicalTimeSpanParser.FractionGroup = "(?<n>\\d+)?\\/(?<d>\\d+)";
			MusicalTimeSpanParser.FractionMnemonicGroup = MusicalTimeSpanParser.GetMnemonicGroup("fm", MusicalTimeSpanParser.Fractions.Keys);
			MusicalTimeSpanParser.TupletGroup = "\\[\\s*(?<tn>\\d+)\\s*\\:\\s*(?<ts>\\d+)\\s*\\]";
			MusicalTimeSpanParser.TupletMnemonicGroup = MusicalTimeSpanParser.GetMnemonicGroup("tm", MusicalTimeSpanParser.Tuplets.Keys);
			MusicalTimeSpanParser.DotsGroup = "(?<dt>\\.+)";
			MusicalTimeSpanParser.Patterns = new string[] { string.Concat(new string[]
			{
				"(",
				MusicalTimeSpanParser.FractionGroup,
				"|",
				MusicalTimeSpanParser.FractionMnemonicGroup,
				")\\s*(",
				MusicalTimeSpanParser.TupletGroup,
				"|",
				MusicalTimeSpanParser.TupletMnemonicGroup,
				")?\\s*",
				MusicalTimeSpanParser.DotsGroup,
				"?"
			}) };
		}

		// Token: 0x040006D3 RID: 1747
		private static readonly Dictionary<string, Tuple<int, int>> Fractions;

		// Token: 0x040006D4 RID: 1748
		private static readonly Dictionary<string, Tuple<int, int>> Tuplets;

		// Token: 0x040006D5 RID: 1749
		private const string NumeratorGroupName = "n";

		// Token: 0x040006D6 RID: 1750
		private const string DenominatorGroupName = "d";

		// Token: 0x040006D7 RID: 1751
		private const string FractionMnemonicGroupName = "fm";

		// Token: 0x040006D8 RID: 1752
		private const string TupletNotesCountGroupName = "tn";

		// Token: 0x040006D9 RID: 1753
		private const string TupletSpaceSizeGroupName = "ts";

		// Token: 0x040006DA RID: 1754
		private const string TupletMnemonicGroupName = "tm";

		// Token: 0x040006DB RID: 1755
		private const string DotsGroupName = "dt";

		// Token: 0x040006DC RID: 1756
		private static readonly string FractionGroup;

		// Token: 0x040006DD RID: 1757
		private static readonly string FractionMnemonicGroup;

		// Token: 0x040006DE RID: 1758
		private static readonly string TupletGroup;

		// Token: 0x040006DF RID: 1759
		private static readonly string TupletMnemonicGroup;

		// Token: 0x040006E0 RID: 1760
		private static readonly string DotsGroup;

		// Token: 0x040006E1 RID: 1761
		private static readonly string[] Patterns;

		// Token: 0x040006E2 RID: 1762
		private const string NumeratorIsOutOfRange = "Numerator is out of range.";

		// Token: 0x040006E3 RID: 1763
		private const string DenominatorIsOutOfRange = "Denominator is out of range.";

		// Token: 0x040006E4 RID: 1764
		private const string TupletNotesCountIsOutOfRange = "Tuplet's notes count is out of range.";

		// Token: 0x040006E5 RID: 1765
		private const string TupletSpaceSizeIsOutOfRange = "Tuplet's space size is out of range.";
	}
}
