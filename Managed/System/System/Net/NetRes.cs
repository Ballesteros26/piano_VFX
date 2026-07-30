using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000491 RID: 1169
	internal class NetRes
	{
		// Token: 0x0600227B RID: 8827 RVA: 0x000020EB File Offset: 0x000002EB
		private NetRes()
		{
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00085FA8 File Offset: 0x000841A8
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = global::SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = global::SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, @string);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00085FD4 File Offset: 0x000841D4
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return global::SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00085FE4 File Offset: 0x000841E4
		public static string GetWebStatusCodeString(HttpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = global::SR.GetString("net_httpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0008607C File Offset: 0x0008427C
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = global::SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}
	}
}
