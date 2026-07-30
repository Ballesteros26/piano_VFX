using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class MipMapDebugSettings
	{
		// Token: 0x0600018A RID: 394 RVA: 0x0000A816 File Offset: 0x00008A16
		public bool IsDebugDisplayEnabled()
		{
			return this.debugMipMapMode > DebugMipMapMode.None;
		}

		// Token: 0x04000167 RID: 359
		public DebugMipMapMode debugMipMapMode;

		// Token: 0x04000168 RID: 360
		public DebugMipMapModeTerrainTexture terrainTexture;
	}
}
