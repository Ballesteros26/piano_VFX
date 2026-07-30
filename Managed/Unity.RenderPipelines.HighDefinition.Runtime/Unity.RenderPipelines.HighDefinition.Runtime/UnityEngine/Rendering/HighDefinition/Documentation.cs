using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200003A RID: 58
	internal class Documentation
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000ADF1 File Offset: 0x00008FF1
		internal static string GetPageLink(string pageName)
		{
			return "https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/" + pageName + ".html";
		}

		// Token: 0x0400017F RID: 383
		internal const string baseURL = "https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@";

		// Token: 0x04000180 RID: 384
		internal const string version = "8.0";

		// Token: 0x04000181 RID: 385
		internal const string subURL = "/manual/";

		// Token: 0x04000182 RID: 386
		internal const string endURL = ".html";
	}
}
