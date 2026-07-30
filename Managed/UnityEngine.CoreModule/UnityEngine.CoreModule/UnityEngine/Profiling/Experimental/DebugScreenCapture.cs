using System;
using Unity.Collections;

namespace UnityEngine.Profiling.Experimental
{
	// Token: 0x02000216 RID: 534
	public struct DebugScreenCapture
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00026785 File Offset: 0x00024985
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x0002678D File Offset: 0x0002498D
		public NativeArray<byte> rawImageDataReference { get; set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x00026796 File Offset: 0x00024996
		// (set) Token: 0x060017DE RID: 6110 RVA: 0x0002679E File Offset: 0x0002499E
		public TextureFormat imageFormat { get; set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x000267A7 File Offset: 0x000249A7
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x000267AF File Offset: 0x000249AF
		public int width { get; set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x000267B8 File Offset: 0x000249B8
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x000267C0 File Offset: 0x000249C0
		public int height { get; set; }
	}
}
