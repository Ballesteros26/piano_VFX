using System;

namespace System
{
	// Token: 0x02000022 RID: 34
	internal static class MonoUtil
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000022F0 File Offset: 0x000004F0
		static MonoUtil()
		{
			int platform = (int)Environment.OSVersion.Platform;
			MonoUtil.IsUnix = platform == 4 || platform == 128 || platform == 6;
		}

		// Token: 0x040001CC RID: 460
		public static readonly bool IsUnix;
	}
}
