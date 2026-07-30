using System;
using System.Diagnostics;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000623 RID: 1571
	internal class TokenInfo
	{
		// Token: 0x06003D79 RID: 15737 RVA: 0x000020FD File Offset: 0x000002FD
		private TokenInfo()
		{
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void AssertSeparator(bool isSeparator)
		{
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x00153CC0 File Offset: 0x00151EC0
		public static TokenInfo CreateSeparator(string formatString, int startIdx, int tokLen)
		{
			return new TokenInfo
			{
				startIdx = startIdx,
				formatString = formatString,
				length = tokLen
			};
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x00153CDC File Offset: 0x00151EDC
		public static TokenInfo CreateFormat(string formatString, int startIdx, int tokLen)
		{
			TokenInfo tokenInfo = new TokenInfo();
			tokenInfo.formatString = null;
			tokenInfo.length = 1;
			bool flag = false;
			char c = formatString[startIdx];
			if (c <= 'A')
			{
				if (c == '1' || c == 'A')
				{
					goto IL_0089;
				}
			}
			else if (c == 'I' || c == 'a' || c == 'i')
			{
				goto IL_0089;
			}
			if (!CharUtil.IsDecimalDigitOne(c))
			{
				if (CharUtil.IsDecimalDigitOne(c + '\u0001'))
				{
					int num = startIdx;
					do
					{
						tokenInfo.length++;
					}
					while (--tokLen > 0 && c == formatString[++num]);
					if (formatString[num] == (c += '\u0001'))
					{
						goto IL_0089;
					}
				}
				flag = true;
			}
			IL_0089:
			if (tokLen != 1)
			{
				flag = true;
			}
			if (flag)
			{
				tokenInfo.startChar = '1';
				tokenInfo.length = 1;
			}
			else
			{
				tokenInfo.startChar = c;
			}
			return tokenInfo;
		}

		// Token: 0x040027D9 RID: 10201
		public char startChar;

		// Token: 0x040027DA RID: 10202
		public int startIdx;

		// Token: 0x040027DB RID: 10203
		public string formatString;

		// Token: 0x040027DC RID: 10204
		public int length;
	}
}
