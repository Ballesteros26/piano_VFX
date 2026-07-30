using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200007E RID: 126
	internal static class IntervalParser
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000DCDA File Offset: 0x0000BEDA
		internal static IEnumerable<string> GetPatterns()
		{
			return IntervalParser.Patterns;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		internal static ParsingResult TryParse(string input, out Interval interval)
		{
			interval = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, IntervalParser.Patterns, false);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			Group group = match.Groups["q"];
			if (!group.Success)
			{
				int num;
				if (!ParsingUtilities.ParseInt(match, "hs", 0, out num) || !IntervalUtilities.IsIntervalValid(num))
				{
					return ParsingResult.Error("Interval's half steps number is out of range.");
				}
				interval = Interval.FromHalfSteps(num);
				return ParsingResult.Parsed;
			}
			else
			{
				IntervalQuality intervalQuality = IntervalParser.IntervalQualitiesByLetters[group.Value];
				int num2;
				if (!ParsingUtilities.ParseInt(match, "n", 0, out num2) || num2 < 1)
				{
					return ParsingResult.Error("Interval's number is out of range.");
				}
				interval = Interval.Get(intervalQuality, num2);
				return ParsingResult.Parsed;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		// Note: this type is marked as 'beforefieldinit'.
		static IntervalParser()
		{
			Dictionary<string, IntervalQuality> dictionary = new Dictionary<string, IntervalQuality>();
			dictionary["P"] = IntervalQuality.Perfect;
			dictionary["p"] = IntervalQuality.Perfect;
			dictionary["M"] = IntervalQuality.Major;
			dictionary["m"] = IntervalQuality.Minor;
			dictionary["D"] = IntervalQuality.Diminished;
			dictionary["d"] = IntervalQuality.Diminished;
			dictionary["A"] = IntervalQuality.Augmented;
			dictionary["a"] = IntervalQuality.Augmented;
			IntervalParser.IntervalQualitiesByLetters = dictionary;
		}

		// Token: 0x0400052C RID: 1324
		private const string HalfStepsGroupName = "hs";

		// Token: 0x0400052D RID: 1325
		private const string IntervalQualityGroupName = "q";

		// Token: 0x0400052E RID: 1326
		private const string IntervalNumberGroupName = "n";

		// Token: 0x0400052F RID: 1327
		private static readonly string HalfStepsGroup = ParsingUtilities.GetIntegerNumberGroup("hs");

		// Token: 0x04000530 RID: 1328
		private static readonly string IntervalGroup = "(?<q>P|p|M|m|D|d|A|a)(?<n>\\d+)";

		// Token: 0x04000531 RID: 1329
		private static readonly string[] Patterns = new string[]
		{
			IntervalParser.IntervalGroup,
			IntervalParser.HalfStepsGroup
		};

		// Token: 0x04000532 RID: 1330
		private static readonly Dictionary<string, IntervalQuality> IntervalQualitiesByLetters;

		// Token: 0x04000533 RID: 1331
		private const string HalfStepsNumberIsOutOfRange = "Interval's half steps number is out of range.";

		// Token: 0x04000534 RID: 1332
		private const string IntervalNumberIsOutOfRange = "Interval's number is out of range.";
	}
}
