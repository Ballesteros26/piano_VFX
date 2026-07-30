using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200012F RID: 303
	internal class RegexUtil
	{
		// Token: 0x06000E4A RID: 3658 RVA: 0x00026C50 File Offset: 0x00024E50
		public static bool IsMatch(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.IsMatch(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.IsMatch(stringToMatch, pattern, regOption);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00026C8C File Offset: 0x00024E8C
		public static Match Match(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.Match(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.Match(stringToMatch, pattern, regOption);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00026CC8 File Offset: 0x00024EC8
		public static Regex CreateRegex(string pattern, RegexOptions option, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return new Regex(pattern, option, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return new Regex(pattern, option);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00026D00 File Offset: 0x00024F00
		internal static Regex CreateRegex(string pattern, RegexOptions option)
		{
			return RegexUtil.CreateRegex(pattern, option, null);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00026D20 File Offset: 0x00024F20
		private static bool IsRegexTimeoutSetInAppDomain
		{
			get
			{
				if (RegexUtil._isRegexTimeoutSetInAppDomain == null)
				{
					bool flag = false;
					try
					{
						flag = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") != null;
					}
					catch
					{
					}
					RegexUtil._isRegexTimeoutSetInAppDomain = new bool?(flag);
				}
				return RegexUtil._isRegexTimeoutSetInAppDomain.Value;
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00026D78 File Offset: 0x00024F78
		private static int GetRegexTimeout(int? timeoutInMillsec)
		{
			int num = -1;
			if (timeoutInMillsec != null)
			{
				num = timeoutInMillsec.Value;
			}
			else if (!RegexUtil.IsRegexTimeoutSetInAppDomain && BinaryCompatibility.Current.TargetsAtLeastFramework461)
			{
				num = 2000;
			}
			return num;
		}

		// Token: 0x040011C8 RID: 4552
		private static bool? _isRegexTimeoutSetInAppDomain;
	}
}
