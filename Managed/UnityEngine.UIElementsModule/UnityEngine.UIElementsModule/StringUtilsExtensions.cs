using System;
using System.Linq;
using System.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000051 RID: 81
	internal static class StringUtilsExtensions
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000081EC File Offset: 0x000063EC
		public static string ToPascalCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00008224 File Offset: 0x00006424
		public static string ToCamelCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000825C File Offset: 0x0000645C
		public static string ToKebabCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008290 File Offset: 0x00006490
		public static string ToTrainCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000082C4 File Offset: 0x000064C4
		public static string ToSnakeCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '_', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000082F8 File Offset: 0x000064F8
		private static string ConvertCase(string text, char outputWordDelimiter, Func<char, char> startOfStringCaseHandler, Func<char, char> middleStringCaseHandler)
		{
			bool flag = text == null;
			if (flag)
			{
				throw new ArgumentNullException("text");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				bool flag5 = Enumerable.Contains<char>(StringUtilsExtensions.WordDelimiters, c);
				if (flag5)
				{
					bool flag6 = c == outputWordDelimiter;
					if (flag6)
					{
						stringBuilder.Append(outputWordDelimiter);
						flag4 = false;
					}
					flag3 = true;
				}
				else
				{
					bool flag7 = !char.IsLetterOrDigit(c);
					if (flag7)
					{
						flag2 = true;
						flag3 = true;
					}
					else
					{
						bool flag8 = flag3 || char.IsUpper(c);
						if (flag8)
						{
							bool flag9 = flag2;
							if (flag9)
							{
								stringBuilder.Append(startOfStringCaseHandler.Invoke(c));
							}
							else
							{
								bool flag10 = flag4 && outputWordDelimiter != StringUtilsExtensions.NoDelimiter;
								if (flag10)
								{
									stringBuilder.Append(outputWordDelimiter);
								}
								stringBuilder.Append(middleStringCaseHandler.Invoke(c));
								flag4 = true;
							}
							flag2 = false;
							flag3 = false;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000EA RID: 234
		private static readonly char NoDelimiter = '\0';

		// Token: 0x040000EB RID: 235
		private static readonly char[] WordDelimiters = new char[] { ' ', '-', '_' };
	}
}
