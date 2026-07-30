using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.MusicTheory
{
	// Token: 0x0200008B RID: 139
	internal static class ScaleParser
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000FB78 File Offset: 0x0000DD78
		internal static ParsingResult TryParse(string input, out Scale scale)
		{
			scale = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, ScaleParser.Patterns, true);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["rn"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			Group group = match.Groups["i"];
			IEnumerable<Interval> enumerable;
			if (group.Success)
			{
				var array = group.Captures.OfType<Capture>().Select(delegate(Capture c)
				{
					Interval interval;
					ParsingResult parsingResult2 = IntervalParser.TryParse(c.Value, out interval);
					return new
					{
						Interval = interval,
						ParsingResult = parsingResult2
					};
				}).ToArray();
				var <>f__AnonymousType = array.FirstOrDefault(r => r.ParsingResult.Status > ParsingStatus.Parsed);
				if (<>f__AnonymousType != null)
				{
					return <>f__AnonymousType.ParsingResult;
				}
				enumerable = array.Select(r => r.Interval).ToArray<Interval>();
			}
			else
			{
				enumerable = ScaleIntervals.GetByName(match.Groups["im"].Value);
			}
			if (enumerable == null)
			{
				return ParsingResult.Error("Scale is unknown.");
			}
			scale = new Scale(enumerable, noteName);
			return ParsingResult.Parsed;
		}

		// Token: 0x04000658 RID: 1624
		private const string RootNoteNameGroupName = "rn";

		// Token: 0x04000659 RID: 1625
		private const string IntervalsMnemonicGroupName = "im";

		// Token: 0x0400065A RID: 1626
		private const string IntervalGroupName = "i";

		// Token: 0x0400065B RID: 1627
		private static readonly string IntervalGroup = "(?<i>(" + string.Join("|", IntervalParser.GetPatterns()) + ")\\s*)+";

		// Token: 0x0400065C RID: 1628
		private static readonly string IntervalsMnemonicGroup = "(?<im>.+?)";

		// Token: 0x0400065D RID: 1629
		private static readonly string[] Patterns = (from p in NoteNameParser.GetPatterns()
			select string.Concat(new string[]
			{
				"(?<rn>",
				p,
				")\\s*(",
				ScaleParser.IntervalGroup,
				"|",
				ScaleParser.IntervalsMnemonicGroup,
				")"
			})).ToArray<string>();

		// Token: 0x0400065E RID: 1630
		private const string ScaleIsUnknown = "Scale is unknown.";
	}
}
