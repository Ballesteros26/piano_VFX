using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebSockets
{
	// Token: 0x0200010A RID: 266
	internal static class SubProtocolUtil
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x00025C2E File Offset: 0x00023E2E
		public static bool IsValidSubProtocolName(string subprotocol)
		{
			return !string.IsNullOrEmpty(subprotocol) && subprotocol.All(new Func<char, bool>(SubProtocolUtil.IsValidSubProtocolChar));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00025C4C File Offset: 0x00023E4C
		private static bool IsValidSubProtocolChar(char c)
		{
			return '!' <= c && c <= '~' && !SubProtocolUtil.IsSeparatorChar(c);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00025C64 File Offset: 0x00023E64
		private static bool IsSeparatorChar(char c)
		{
			if (c <= ',')
			{
				if (c <= ' ')
				{
					if (c != '\t' && c != ' ')
					{
						return false;
					}
				}
				else if (c != '"')
				{
					switch (c)
					{
					case '(':
					case ')':
					case ',':
						break;
					case '*':
					case '+':
						return false;
					default:
						return false;
					}
				}
			}
			else if (c <= '@')
			{
				if (c != '/')
				{
					switch (c)
					{
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
						break;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (c)
				{
				case '[':
				case '\\':
				case ']':
					break;
				default:
					if (c != '{' && c != '}')
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00025D00 File Offset: 0x00023F00
		public static List<string> ParseHeader(string headerValue)
		{
			if (headerValue == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			string[] array = headerValue.Split(SubProtocolUtil._splitChars);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim(SubProtocolUtil._lwsTrimChars);
				if (text.Length != 0)
				{
					if (!SubProtocolUtil.IsValidSubProtocolName(text))
					{
						return null;
					}
					list.Add(text);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Distinct(StringComparer.Ordinal).Count<string>() != list.Count)
			{
				return null;
			}
			return list;
		}

		// Token: 0x04001171 RID: 4465
		private static readonly char[] _lwsTrimChars = new char[] { ' ', '\t' };

		// Token: 0x04001172 RID: 4466
		private static readonly char[] _splitChars = new char[] { ',' };
	}
}
