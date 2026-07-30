using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B12 RID: 2834
	[Flags]
	public enum EventManifestOptions
	{
		// Token: 0x040032B6 RID: 12982
		None = 0,
		// Token: 0x040032B7 RID: 12983
		Strict = 1,
		// Token: 0x040032B8 RID: 12984
		AllCultures = 2,
		// Token: 0x040032B9 RID: 12985
		OnlyIfNeededForRegistration = 4,
		// Token: 0x040032BA RID: 12986
		AllowEventSourceOverride = 8
	}
}
