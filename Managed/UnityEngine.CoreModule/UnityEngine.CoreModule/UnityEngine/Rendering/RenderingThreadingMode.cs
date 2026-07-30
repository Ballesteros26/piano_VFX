using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x02000349 RID: 841
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public enum RenderingThreadingMode
	{
		// Token: 0x040009EA RID: 2538
		Direct,
		// Token: 0x040009EB RID: 2539
		SingleThreaded,
		// Token: 0x040009EC RID: 2540
		MultiThreaded,
		// Token: 0x040009ED RID: 2541
		LegacyJobified,
		// Token: 0x040009EE RID: 2542
		NativeGraphicsJobs,
		// Token: 0x040009EF RID: 2543
		NativeGraphicsJobsWithoutRenderThread
	}
}
