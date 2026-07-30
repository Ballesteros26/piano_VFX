using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A4 RID: 164
	internal class VolumeRenderingUtils
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00010665 File Offset: 0x0000E865
		public static float MeanFreePathFromExtinction(float extinction)
		{
			return 1f / extinction;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00010665 File Offset: 0x0000E865
		public static float ExtinctionFromMeanFreePath(float meanFreePath)
		{
			return 1f / meanFreePath;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00033ED8 File Offset: 0x000320D8
		public static Vector3 AbsorptionFromExtinctionAndScattering(float extinction, Vector3 scattering)
		{
			return new Vector3(extinction, extinction, extinction) - scattering;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00033EE8 File Offset: 0x000320E8
		public static Vector3 ScatteringFromExtinctionAndAlbedo(float extinction, Vector3 albedo)
		{
			return extinction * albedo;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00033EE8 File Offset: 0x000320E8
		public static Vector3 AlbedoFromMeanFreePathAndScattering(float meanFreePath, Vector3 scattering)
		{
			return meanFreePath * scattering;
		}
	}
}
