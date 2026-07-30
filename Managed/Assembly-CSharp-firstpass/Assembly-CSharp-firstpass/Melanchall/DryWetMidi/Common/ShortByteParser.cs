using System;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001C3 RID: 451
	internal static class ShortByteParser
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x00024A88 File Offset: 0x00022C88
		internal static ParsingResult TryParse(string input, byte minValue, byte maxValue, out byte result)
		{
			result = 0;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			byte b;
			if (!byte.TryParse(input.Trim(), out b) || b < minValue || b > maxValue)
			{
				return ParsingResult.Error("Number is invalid or is out of valid range.");
			}
			result = b;
			return ParsingResult.Parsed;
		}
	}
}
