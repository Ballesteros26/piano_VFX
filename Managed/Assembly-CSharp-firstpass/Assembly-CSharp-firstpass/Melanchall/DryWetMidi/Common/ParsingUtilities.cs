using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001CA RID: 458
	internal static class ParsingUtilities
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00024C7C File Offset: 0x00022E7C
		public static bool TryParse<T>(string input, Parsing<T> parsing, out T result)
		{
			return parsing(input, out result).Status == ParsingStatus.Parsed;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00024C90 File Offset: 0x00022E90
		public static T Parse<T>(string input, Parsing<T> parsing)
		{
			T t;
			ParsingResult parsingResult = parsing(input, out t);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return t;
			}
			throw parsingResult.Exception;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00024CB7 File Offset: 0x00022EB7
		public static string GetNonnegativeIntegerNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">\\d+)";
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00024CC9 File Offset: 0x00022EC9
		public static string GetIntegerNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">[\\+\\-]?\\d+)";
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00024CDB File Offset: 0x00022EDB
		public static string GetNonnegativeDoubleNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">\\d+(.\\d+)?)";
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00024CF0 File Offset: 0x00022EF0
		public static Match Match(string input, IEnumerable<string> patterns, bool ignoreCase = true)
		{
			return patterns.Select((string p) => Regex.Match(input.Trim(), "^" + p + "$", ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)).FirstOrDefault((Match m) => m.Success);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00024D48 File Offset: 0x00022F48
		public static Match[] Matches(string input, IEnumerable<string> patterns, bool ignoreCase = true)
		{
			return patterns.Select((string p) => Regex.Matches(input.Trim(), p, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None).OfType<Match>().ToArray<Match>()).FirstOrDefault((Match[] m) => m.Any<Match>());
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00024D9F File Offset: 0x00022F9F
		public static bool ParseNonnegativeInt(Match match, string groupName, int defaultValue, out int value)
		{
			return ParsingUtilities.ParseInt(match, groupName, defaultValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, out value);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00024DAB File Offset: 0x00022FAB
		public static bool ParseInt(Match match, string groupName, int defaultValue, out int value)
		{
			return ParsingUtilities.ParseInt(match, groupName, defaultValue, NumberStyles.Integer, out value);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00024DB7 File Offset: 0x00022FB7
		public static bool ParseNonnegativeDouble(Match match, string groupName, double defaultValue, out double value)
		{
			return ParsingUtilities.ParseDouble(match, groupName, defaultValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint, out value);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public static bool ParseNonnegativeLong(Match match, string groupName, long defaultValue, out long value)
		{
			value = defaultValue;
			Group group = match.Groups[groupName];
			return !group.Success || long.TryParse(group.Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out value);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00024DFC File Offset: 0x00022FFC
		private static bool ParseInt(Match match, string groupName, int defaultValue, NumberStyles numberStyle, out int value)
		{
			value = defaultValue;
			Group group = match.Groups[groupName];
			return !group.Success || int.TryParse(group.Value, numberStyle, null, out value);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00024E34 File Offset: 0x00023034
		private static bool ParseDouble(Match match, string groupName, double defaultValue, NumberStyles numberStyle, out double value)
		{
			value = defaultValue;
			Group group = match.Groups[groupName];
			return !group.Success || double.TryParse(group.Value, numberStyle, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x04000A1D RID: 2589
		private const NumberStyles NonnegativeIntegerNumberStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;

		// Token: 0x04000A1E RID: 2590
		private const NumberStyles IntegerNumberStyle = NumberStyles.Integer;

		// Token: 0x04000A1F RID: 2591
		private const NumberStyles NonnegativeDoubleNumberStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint;
	}
}
