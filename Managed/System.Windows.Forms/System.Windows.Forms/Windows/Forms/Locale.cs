using System;

namespace System.Windows.Forms
{
	// Token: 0x02000009 RID: 9
	internal sealed class Locale
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		public static string GetText(string msg)
		{
			return msg;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002154 File Offset: 0x00000354
		public static string GetText(string msg, params object[] args)
		{
			return string.Format(Locale.GetText(msg), args);
		}
	}
}
