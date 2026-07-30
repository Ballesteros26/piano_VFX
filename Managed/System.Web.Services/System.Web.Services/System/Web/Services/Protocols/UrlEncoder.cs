using System;
using System.Text;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200004F RID: 79
	internal class UrlEncoder
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000210F File Offset: 0x0000030F
		private UrlEncoder()
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008882 File Offset: 0x00006A82
		internal static string EscapeString(string s, Encoding e)
		{
			return UrlEncoder.EscapeStringInternal(s, (e == null) ? new ASCIIEncoding() : e, false);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008896 File Offset: 0x00006A96
		internal static string UrlEscapeString(string s, Encoding e)
		{
			return UrlEncoder.EscapeStringInternal(s, (e == null) ? new ASCIIEncoding() : e, true);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000088AC File Offset: 0x00006AAC
		private static string EscapeStringInternal(string s, Encoding e, bool escapeUriStuff)
		{
			if (s == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(s);
			StringBuilder stringBuilder = new StringBuilder(bytes.Length);
			foreach (byte b in bytes)
			{
				char c = (char)b;
				if (b > 127 || b < 32 || c == '%' || (escapeUriStuff && !UrlEncoder.IsSafe(c)))
				{
					UrlEncoder.HexEscape8(stringBuilder, c);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000891C File Offset: 0x00006B1C
		internal static string UrlEscapeStringUnicode(string s)
		{
			int length = s.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (UrlEncoder.IsSafe(c))
				{
					stringBuilder.Append(c);
				}
				else if (c == ' ')
				{
					stringBuilder.Append('+');
				}
				else if ((c & 'ﾀ') == '\0')
				{
					UrlEncoder.HexEscape8(stringBuilder, c);
				}
				else
				{
					UrlEncoder.HexEscape16(stringBuilder, c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000898C File Offset: 0x00006B8C
		private static void HexEscape8(StringBuilder sb, char c)
		{
			sb.Append('%');
			sb.Append(UrlEncoder.HexUpperChars[(int)((c >> 4) & '\u000f')]);
			sb.Append(UrlEncoder.HexUpperChars[(int)(c & '\u000f')]);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000089BC File Offset: 0x00006BBC
		private static void HexEscape16(StringBuilder sb, char c)
		{
			sb.Append("%u");
			sb.Append(UrlEncoder.HexUpperChars[(int)((c >> 12) & '\u000f')]);
			sb.Append(UrlEncoder.HexUpperChars[(int)((c >> 8) & '\u000f')]);
			sb.Append(UrlEncoder.HexUpperChars[(int)((c >> 4) & '\u000f')]);
			sb.Append(UrlEncoder.HexUpperChars[(int)(c & '\u000f')]);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008A20 File Offset: 0x00006C20
		private static bool IsSafe(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '\'':
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x04000223 RID: 547
		private const int Max16BitUtf8SequenceLength = 4;

		// Token: 0x04000224 RID: 548
		internal static readonly char[] HexUpperChars = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
