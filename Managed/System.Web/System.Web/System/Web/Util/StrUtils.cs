using System;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x0200014B RID: 331
	internal sealed class StrUtils
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00002050 File Offset: 0x00000250
		private StrUtils()
		{
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002A784 File Offset: 0x00028984
		public static bool StartsWith(string str1, string str2)
		{
			return StrUtils.StartsWith(str1, str2, false);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002A790 File Offset: 0x00028990
		public static bool StartsWith(string str1, string str2, bool ignore_case)
		{
			int length = str2.Length;
			if (length == 0)
			{
				return true;
			}
			int length2 = str1.Length;
			return length <= length2 && string.Compare(str1, 0, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002A7C9 File Offset: 0x000289C9
		public static bool EndsWith(string str1, string str2)
		{
			return StrUtils.EndsWith(str1, str2, false);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002A7D4 File Offset: 0x000289D4
		public static bool EndsWith(string str1, string str2, bool ignore_case)
		{
			int length = str2.Length;
			if (length == 0)
			{
				return true;
			}
			int length2 = str1.Length;
			return length <= length2 && string.Compare(str1, length2 - length, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002A810 File Offset: 0x00028A10
		public static string EscapeQuotesAndBackslashes(string attributeValue)
		{
			StringBuilder stringBuilder = null;
			for (int i = 0; i < attributeValue.Length; i++)
			{
				char c = attributeValue[i];
				if (c == '\'' || c == '"' || c == '\\')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(attributeValue.Substring(0, i));
					}
					stringBuilder.Append('\\');
					stringBuilder.Append(c);
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return attributeValue;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002A888 File Offset: 0x00028A88
		public static bool IsNullOrEmpty(string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002A890 File Offset: 0x00028A90
		public static string[] SplitRemoveEmptyEntries(string value, char[] separator)
		{
			return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
