using System;
using System.Text;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000050 RID: 80
	internal class ContentType
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x0000210F File Offset: 0x0000030F
		private ContentType()
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008A9C File Offset: 0x00006C9C
		internal static string GetBase(string contentType)
		{
			int num = contentType.IndexOf(';');
			if (num >= 0)
			{
				return contentType.Substring(0, num);
			}
			return contentType;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008AC0 File Offset: 0x00006CC0
		internal static string GetMediaType(string contentType)
		{
			string @base = ContentType.GetBase(contentType);
			int num = @base.IndexOf('/');
			if (num >= 0)
			{
				return @base.Substring(0, num);
			}
			return @base;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008AEB File Offset: 0x00006CEB
		internal static string GetCharset(string contentType)
		{
			return ContentType.GetParameter(contentType, "charset");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008AF8 File Offset: 0x00006CF8
		internal static string GetAction(string contentType)
		{
			return ContentType.GetParameter(contentType, "action");
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008B08 File Offset: 0x00006D08
		private static string GetParameter(string contentType, string paramName)
		{
			string[] array = contentType.Split(new char[] { ';' });
			for (int i = 1; i < array.Length; i++)
			{
				string text = array[i].TrimStart(null);
				if (string.Compare(text, 0, paramName, 0, paramName.Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					int num = text.IndexOf('=', paramName.Length);
					if (num >= 0)
					{
						return text.Substring(num + 1).Trim(new char[] { ' ', '\'', '"', '\t' });
					}
				}
			}
			return null;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008B85 File Offset: 0x00006D85
		internal static bool MatchesBase(string contentType, string baseContentType)
		{
			return string.Compare(ContentType.GetBase(contentType), baseContentType, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008B97 File Offset: 0x00006D97
		internal static bool IsApplication(string contentType)
		{
			return string.Compare(ContentType.GetMediaType(contentType), "application", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008BB0 File Offset: 0x00006DB0
		internal static bool IsSoap(string contentType)
		{
			string @base = ContentType.GetBase(contentType);
			return string.Compare(@base, "text/xml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(@base, "application/soap+xml", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008BE4 File Offset: 0x00006DE4
		internal static bool IsXml(string contentType)
		{
			string @base = ContentType.GetBase(contentType);
			return string.Compare(@base, "text/xml", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(@base, "application/xml", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008C17 File Offset: 0x00006E17
		internal static bool IsHtml(string contentType)
		{
			return string.Compare(ContentType.GetBase(contentType), "text/html", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008C2D File Offset: 0x00006E2D
		internal static string Compose(string contentType, Encoding encoding)
		{
			return ContentType.Compose(contentType, encoding, null);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008C38 File Offset: 0x00006E38
		internal static string Compose(string contentType, Encoding encoding, string action)
		{
			if (encoding == null && action == null)
			{
				return contentType;
			}
			StringBuilder stringBuilder = new StringBuilder(contentType);
			if (encoding != null)
			{
				stringBuilder.Append("; charset=");
				stringBuilder.Append(encoding.WebName);
			}
			if (action != null)
			{
				stringBuilder.Append("; action=\"");
				stringBuilder.Append(action);
				stringBuilder.Append("\"");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000225 RID: 549
		internal const string TextBase = "text";

		// Token: 0x04000226 RID: 550
		internal const string TextXml = "text/xml";

		// Token: 0x04000227 RID: 551
		internal const string TextPlain = "text/plain";

		// Token: 0x04000228 RID: 552
		internal const string TextHtml = "text/html";

		// Token: 0x04000229 RID: 553
		internal const string ApplicationBase = "application";

		// Token: 0x0400022A RID: 554
		internal const string ApplicationXml = "application/xml";

		// Token: 0x0400022B RID: 555
		internal const string ApplicationSoap = "application/soap+xml";

		// Token: 0x0400022C RID: 556
		internal const string ApplicationOctetStream = "application/octet-stream";

		// Token: 0x0400022D RID: 557
		internal const string ContentEncoding = "Content-Encoding";
	}
}
