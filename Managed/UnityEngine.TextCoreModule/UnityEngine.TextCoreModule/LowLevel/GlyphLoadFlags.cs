using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000049 RID: 73
	[UsedByNativeCode]
	public enum GlyphLoadFlags
	{
		// Token: 0x0400038A RID: 906
		LOAD_DEFAULT,
		// Token: 0x0400038B RID: 907
		LOAD_NO_SCALE,
		// Token: 0x0400038C RID: 908
		LOAD_NO_HINTING,
		// Token: 0x0400038D RID: 909
		LOAD_RENDER = 4,
		// Token: 0x0400038E RID: 910
		LOAD_NO_BITMAP = 8,
		// Token: 0x0400038F RID: 911
		LOAD_FORCE_AUTOHINT = 32,
		// Token: 0x04000390 RID: 912
		LOAD_MONOCHROME = 4096,
		// Token: 0x04000391 RID: 913
		LOAD_NO_AUTOHINT = 32768,
		// Token: 0x04000392 RID: 914
		LOAD_COMPUTE_METRICS = 2097152,
		// Token: 0x04000393 RID: 915
		LOAD_BITMAP_METRICS_ONLY = 4194304
	}
}
