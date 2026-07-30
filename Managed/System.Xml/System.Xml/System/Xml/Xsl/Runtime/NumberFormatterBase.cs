using System;
using System.Text;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D3 RID: 1491
	internal class NumberFormatterBase
	{
		// Token: 0x06003AF0 RID: 15088 RVA: 0x0014CE88 File Offset: 0x0014B088
		public static void ConvertToAlphabetic(StringBuilder sb, double val, char firstChar, int totalChars)
		{
			char[] array = new char[7];
			int num = 7;
			int i;
			int num2;
			for (i = (int)val; i > totalChars; i = num2)
			{
				num2 = --i / totalChars;
				array[--num] = (char)((int)firstChar + (i - num2 * totalChars));
			}
			array[--num] = (char)((int)firstChar + (i - 1));
			sb.Append(array, num, 7 - num);
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x0014CEDC File Offset: 0x0014B0DC
		public static void ConvertToRoman(StringBuilder sb, double val, bool upperCase)
		{
			int i = (int)val;
			string text = (upperCase ? "IIVIXXLXCCDCM" : "iivixxlxccdcm");
			int num = NumberFormatterBase.RomanDigitValue.Length;
			while (num-- != 0)
			{
				while (i >= NumberFormatterBase.RomanDigitValue[num])
				{
					i -= NumberFormatterBase.RomanDigitValue[num];
					sb.Append(text, num, 1 + (num & 1));
				}
			}
		}

		// Token: 0x040026A1 RID: 9889
		protected const int MaxAlphabeticValue = 2147483647;

		// Token: 0x040026A2 RID: 9890
		private const int MaxAlphabeticLength = 7;

		// Token: 0x040026A3 RID: 9891
		protected const int MaxRomanValue = 32767;

		// Token: 0x040026A4 RID: 9892
		private const string RomanDigitsUC = "IIVIXXLXCCDCM";

		// Token: 0x040026A5 RID: 9893
		private const string RomanDigitsLC = "iivixxlxccdcm";

		// Token: 0x040026A6 RID: 9894
		private static readonly int[] RomanDigitValue = new int[]
		{
			1, 4, 5, 9, 10, 40, 50, 90, 100, 400,
			500, 900, 1000
		};

		// Token: 0x040026A7 RID: 9895
		private const string hiraganaAiueo = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん";

		// Token: 0x040026A8 RID: 9896
		private const string hiraganaIroha = "いろはにほへとちりぬるをわかよたれそつねならむうゐのおくやまけふこえてあさきゆめみしゑひもせす";

		// Token: 0x040026A9 RID: 9897
		private const string katakanaAiueo = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン";

		// Token: 0x040026AA RID: 9898
		private const string katakanaIroha = "イロハニホヘトチリヌルヲワカヨタレソツネナラムウヰノオクヤマケフコエテアサキユメミシヱヒモセスン";

		// Token: 0x040026AB RID: 9899
		private const string katakanaAiueoHw = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝ";

		// Token: 0x040026AC RID: 9900
		private const string katakanaIrohaHw = "ｲﾛﾊﾆﾎﾍﾄﾁﾘﾇﾙｦﾜｶﾖﾀﾚｿﾂﾈﾅﾗﾑｳヰﾉｵｸﾔﾏｹﾌｺｴﾃｱｻｷﾕﾒﾐｼヱﾋﾓｾｽﾝ";

		// Token: 0x040026AD RID: 9901
		private const string cjkIdeographic = "〇一二三四五六七八九";
	}
}
