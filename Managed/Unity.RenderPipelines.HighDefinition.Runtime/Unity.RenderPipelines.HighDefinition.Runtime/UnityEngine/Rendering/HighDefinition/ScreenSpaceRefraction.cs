using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008A RID: 138
	[VolumeComponentMenu("Lighting/Screen Space Refraction")]
	[Serializable]
	public class ScreenSpaceRefraction : VolumeComponent
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0002F155 File Offset: 0x0002D355
		internal static ScreenSpaceRefraction defaultInstance
		{
			get
			{
				if (ScreenSpaceRefraction.s_Default == null)
				{
					ScreenSpaceRefraction.s_Default = ScriptableObject.CreateInstance<ScreenSpaceRefraction>();
					ScreenSpaceRefraction.s_Default.hideFlags = HideFlags.HideAndDontSave;
				}
				return ScreenSpaceRefraction.s_Default;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002F17F File Offset: 0x0002D37F
		internal virtual void PushShaderParameters(CommandBuffer cmd)
		{
			cmd.SetGlobalFloat(this.m_InvScreenFadeDistanceID, 1f / this.screenFadeDistance.value);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002F19E File Offset: 0x0002D39E
		private void FetchIDs(out int invScreenWeightDistanceID)
		{
			invScreenWeightDistanceID = HDShaderIDs._SSRefractionInvScreenWeightDistance;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002F1A7 File Offset: 0x0002D3A7
		private void Awake()
		{
			this.FetchIDs(out this.m_InvScreenFadeDistanceID);
		}

		// Token: 0x040005AC RID: 1452
		private int m_InvScreenFadeDistanceID;

		// Token: 0x040005AD RID: 1453
		public ClampedFloatParameter screenFadeDistance = new ClampedFloatParameter(0.1f, 0.001f, 1f, false);

		// Token: 0x040005AE RID: 1454
		private static ScreenSpaceRefraction s_Default;

		// Token: 0x02000213 RID: 531
		internal enum RefractionModel
		{
			// Token: 0x040013C2 RID: 5058
			None,
			// Token: 0x040013C3 RID: 5059
			Box,
			// Token: 0x040013C4 RID: 5060
			Sphere,
			// Token: 0x040013C5 RID: 5061
			Thin
		}
	}
}
