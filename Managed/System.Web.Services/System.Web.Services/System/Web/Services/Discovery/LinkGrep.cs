using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;

namespace System.Web.Services.Discovery
{
	// Token: 0x020000B7 RID: 183
	internal class LinkGrep
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x0000210F File Offset: 0x0000030F
		private LinkGrep()
		{
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016230 File Offset: 0x00014430
		private static string ReadEntireStream(TextReader input)
		{
			char[] array = new char[4096];
			int num = 0;
			for (;;)
			{
				int num2 = input.Read(array, num, array.Length - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num == array.Length)
				{
					char[] array2 = new char[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00016288 File Offset: 0x00014488
		internal static string SearchForLink(Stream stream)
		{
			string text = LinkGrep.ReadEntireStream(new StreamReader(stream));
			int num = 0;
			Match match;
			if ((match = LinkGrep.doctypeDirectiveRegex.Match(text, num)).Success)
			{
				num += match.Length;
			}
			string text2;
			for (;;)
			{
				bool flag = false;
				if ((match = LinkGrep.whitespaceRegex.Match(text, num)).Success)
				{
					flag = true;
				}
				else if ((match = LinkGrep.textRegex.Match(text, num)).Success)
				{
					flag = true;
				}
				num += match.Length;
				if (num == text.Length)
				{
					goto IL_01EF;
				}
				if ((match = LinkGrep.tagRegex.Match(text, num)).Success)
				{
					flag = true;
					string value = match.Groups["tagname"].Value;
					if (string.Compare(value, "link", StringComparison.OrdinalIgnoreCase) == 0)
					{
						CaptureCollection captures = match.Groups["attrname"].Captures;
						CaptureCollection captures2 = match.Groups["attrval"].Captures;
						int count = captures.Count;
						bool flag2 = false;
						bool flag3 = false;
						text2 = null;
						for (int i = 0; i < count; i++)
						{
							string text3 = captures[i].ToString();
							string text4 = captures2[i].ToString();
							if (string.Compare(text3, "type", StringComparison.OrdinalIgnoreCase) == 0 && ContentType.MatchesBase(text4, "text/xml"))
							{
								flag2 = true;
							}
							else if (string.Compare(text3, "rel", StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(text4, "alternate", StringComparison.OrdinalIgnoreCase) == 0)
							{
								flag3 = true;
							}
							else if (string.Compare(text3, "href", StringComparison.OrdinalIgnoreCase) == 0)
							{
								text2 = text4;
							}
							if (flag2 && flag3 && text2 != null)
							{
								return text2;
							}
						}
					}
					else if (value == "body")
					{
						goto Block_15;
					}
				}
				else if ((match = LinkGrep.endtagRegex.Match(text, num)).Success)
				{
					flag = true;
				}
				else if ((match = LinkGrep.commentRegex.Match(text, num)).Success)
				{
					flag = true;
				}
				num += match.Length;
				if (num == text.Length || !flag)
				{
					goto IL_01EF;
				}
			}
			return text2;
			Block_15:
			IL_01EF:
			return null;
		}

		// Token: 0x0400035D RID: 861
		private static readonly Regex tagRegex = new Regex("\\G<(?<prefix>[\\w:.-]+(?=:)|):?(?<tagname>[\\w.-]+)(?:\\s+(?<attrprefix>[\\w:.-]+(?=:)|):?(?<attrname>[\\w.-]+)\\s*=\\s*(?:\"(?<attrval>[^\"]*)\"|'(?<attrval>[^']*)'|(?<attrval>[a-zA-Z0-9\\-._:]+)))*\\s*(?<empty>/)?>");

		// Token: 0x0400035E RID: 862
		private static readonly Regex doctypeDirectiveRegex = new Regex("\\G<!doctype\\b(([\\s\\w]+)|(\".*\"))*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x0400035F RID: 863
		private static readonly Regex endtagRegex = new Regex("\\G</(?<prefix>[\\w:-]+(?=:)|):?(?<tagname>[\\w-]+)\\s*>");

		// Token: 0x04000360 RID: 864
		private static readonly Regex commentRegex = new Regex("\\G<!--(?>[^-]*-)+?->");

		// Token: 0x04000361 RID: 865
		private static readonly Regex whitespaceRegex = new Regex("\\G\\s+(?=<|\\Z)");

		// Token: 0x04000362 RID: 866
		private static readonly Regex textRegex = new Regex("\\G[^<]+");
	}
}
