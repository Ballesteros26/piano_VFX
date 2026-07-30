using System;
using System.Text;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200001A RID: 26
	internal static class StringExtention
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000036C4 File Offset: 0x000018C4
		public static string CamelToPascalCaseWithSpace(this string text, bool preserveAcronyms = true)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
			stringBuilder.Append(char.ToUpper(text[0]));
			for (int i = 1; i < text.Length; i++)
			{
				if (char.IsUpper(text[i]) && ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) || (preserveAcronyms && char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1]))))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(text[i]);
			}
			return stringBuilder.ToString();
		}
	}
}
