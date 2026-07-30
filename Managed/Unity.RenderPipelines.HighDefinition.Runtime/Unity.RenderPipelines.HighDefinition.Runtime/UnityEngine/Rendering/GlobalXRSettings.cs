using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public struct GlobalXRSettings
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000369C File Offset: 0x0000189C
		internal static GlobalXRSettings NewDefault()
		{
			return new GlobalXRSettings
			{
				singlePass = true,
				occlusionMesh = true
			};
		}

		// Token: 0x0400006F RID: 111
		public bool singlePass;

		// Token: 0x04000070 RID: 112
		public bool occlusionMesh;
	}
}
