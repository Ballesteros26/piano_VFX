using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000466 RID: 1126
	internal static class WebExceptionMapping
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x00080108 File Offset: 0x0007E308
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x04001E1E RID: 7710
		private static readonly string[] s_Mapping = new string[21];
	}
}
