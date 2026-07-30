using System;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering
{
	// Token: 0x02000016 RID: 22
	[RequireComponent(typeof(Light))]
	[Obsolete("This component will be removed in the future, it's content have been moved to HDAdditionalLightData.")]
	[ExecuteAlways]
	internal class AdditionalShadowData : MonoBehaviour
	{
		// Token: 0x0400005A RID: 90
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.customResolution instead.")]
		[FormerlySerializedAs("shadowResolution")]
		internal int customResolution = 512;

		// Token: 0x0400005B RID: 91
		[SerializeField]
		[Range(0f, 1f)]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowDimmer instead.")]
		internal float shadowDimmer = 1f;

		// Token: 0x0400005C RID: 92
		[SerializeField]
		[Range(0f, 1f)]
		[Obsolete("Obsolete, use HDAdditionalLightData.volumetricShadowDimmer instead.")]
		internal float volumetricShadowDimmer = 1f;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowFadeDistance instead.")]
		internal float shadowFadeDistance = 10000f;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.contactShadows instead.")]
		internal bool contactShadows;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowTint instead.")]
		internal Color shadowTint = Color.black;

		// Token: 0x04000060 RID: 96
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.normalBias instead.")]
		internal float normalBias = 0.75f;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowUpdateMode instead.")]
		internal ShadowUpdateMode shadowUpdateMode;

		// Token: 0x04000062 RID: 98
		[HideInInspector]
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowCascadeRatios instead.")]
		internal float[] shadowCascadeRatios = new float[] { 0.05f, 0.2f, 0.3f };

		// Token: 0x04000063 RID: 99
		[HideInInspector]
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowCascadeBorders instead.")]
		internal float[] shadowCascadeBorders = new float[] { 0.2f, 0.2f, 0.2f, 0.2f };

		// Token: 0x04000064 RID: 100
		[HideInInspector]
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowAlgorithm instead.")]
		internal int shadowAlgorithm;

		// Token: 0x04000065 RID: 101
		[HideInInspector]
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowVariant instead.")]
		internal int shadowVariant;

		// Token: 0x04000066 RID: 102
		[HideInInspector]
		[SerializeField]
		[Obsolete("Obsolete, use HDAdditionalLightData.shadowPrecision instead.")]
		internal int shadowPrecision;
	}
}
