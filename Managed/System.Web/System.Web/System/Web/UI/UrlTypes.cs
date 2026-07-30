using System;

namespace System.Web.UI
{
	// Token: 0x02000240 RID: 576
	[Flags]
	[Serializable]
	public enum UrlTypes
	{
		// Token: 0x040015F6 RID: 5622
		Absolute = 1,
		// Token: 0x040015F7 RID: 5623
		AppRelative = 2,
		// Token: 0x040015F8 RID: 5624
		DocRelative = 4,
		// Token: 0x040015F9 RID: 5625
		RootRelative = 8
	}
}
