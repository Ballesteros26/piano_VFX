using System;

namespace System.Web.UI
{
	// Token: 0x02000244 RID: 580
	internal static class Util
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x00040D3C File Offset: 0x0003EF3C
		internal static string GetUrlWithApplicationPath(HttpContextBase context, string url)
		{
			string text = context.Request.ApplicationPath ?? string.Empty;
			if (!text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				text += "/";
			}
			return context.Response.ApplyAppPathModifier(text + url);
		}
	}
}
