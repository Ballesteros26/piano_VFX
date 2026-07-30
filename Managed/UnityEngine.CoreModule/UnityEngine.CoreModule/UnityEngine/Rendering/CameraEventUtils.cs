using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000326 RID: 806
	internal static class CameraEventUtils
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x0002C15C File Offset: 0x0002A35C
		public static bool IsValid(CameraEvent value)
		{
			return value >= CameraEvent.BeforeDepthTexture && value <= CameraEvent.AfterHaloAndLensFlares;
		}

		// Token: 0x040008EE RID: 2286
		private const CameraEvent k_MinimumValue = CameraEvent.BeforeDepthTexture;

		// Token: 0x040008EF RID: 2287
		private const CameraEvent k_MaximumValue = CameraEvent.AfterHaloAndLensFlares;
	}
}
