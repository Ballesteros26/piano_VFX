using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200001A RID: 26
	internal static class CsvUtilities
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004D74 File Offset: 0x00002F74
		public static string EscapeString(string input)
		{
			return string.Format("{0}{1}{2}", '"', input.Replace("\"", "\"\""), '"');
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static string UnescapeString(string input)
		{
			if (input.Length > 1 && input[0] == '"' && input[input.Length - 1] == '"')
			{
				input = input.Substring(1, input.Length - 2);
			}
			return input.Replace("\"\"", "\"");
		}

		// Token: 0x04000088 RID: 136
		private const char Quote = '"';

		// Token: 0x04000089 RID: 137
		private const string QuoteString = "\"";

		// Token: 0x0400008A RID: 138
		private const string DoubleQuote = "\"\"";
	}
}
