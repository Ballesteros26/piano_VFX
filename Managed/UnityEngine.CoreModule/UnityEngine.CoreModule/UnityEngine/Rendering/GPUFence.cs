using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000311 RID: 785
	[Obsolete("GPUFence has been deprecated. Use GraphicsFence instead (UnityUpgradable) -> GraphicsFence", false)]
	public struct GPUFence
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0002C148 File Offset: 0x0002A348
		public bool passed
		{
			get
			{
				return true;
			}
		}
	}
}
