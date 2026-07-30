using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000B00 RID: 2816
	[Flags]
	public enum EventSourceSettings
	{
		// Token: 0x0400325D RID: 12893
		Default = 0,
		// Token: 0x0400325E RID: 12894
		ThrowOnEventWriteErrors = 1,
		// Token: 0x0400325F RID: 12895
		EtwManifestEventFormat = 4,
		// Token: 0x04003260 RID: 12896
		EtwSelfDescribingEventFormat = 8
	}
}
