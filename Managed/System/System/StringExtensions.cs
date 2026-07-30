using System;

namespace System
{
	// Token: 0x020000EE RID: 238
	internal static class StringExtensions
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001A1DB File Offset: 0x000183DB
		internal static string SubstringTrim(this string value, int startIndex)
		{
			return value.SubstringTrim(startIndex, value.Length - startIndex);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A1EC File Offset: 0x000183EC
		internal static string SubstringTrim(this string value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = startIndex + length - 1;
			while (startIndex <= num)
			{
				if (!char.IsWhiteSpace(value[startIndex]))
				{
					break;
				}
				startIndex++;
			}
			while (num >= startIndex && char.IsWhiteSpace(value[num]))
			{
				num--;
			}
			int num2 = num - startIndex + 1;
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (num2 != value.Length)
			{
				return value.Substring(startIndex, num2);
			}
			return value;
		}
	}
}
