using System;
using UnityEngine.Rendering.HighDefinition.Attributes;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C8 RID: 200
	internal class Unlit : RenderPipelineMaterial
	{
		// Token: 0x02000251 RID: 593
		[GenerateHLSL(PackingRules.Exact, false, false, true, 300, false, false)]
		public struct SurfaceData
		{
			// Token: 0x04001586 RID: 5510
			[MaterialSharedPropertyMapping(MaterialSharedProperty.Albedo)]
			[SurfaceDataAttributes("Color", false, true, FieldPrecision.Default)]
			public Vector3 color;
		}

		// Token: 0x02000252 RID: 594
		[GenerateHLSL(PackingRules.Exact, false, false, true, 350, false, false)]
		public struct BSDFData
		{
			// Token: 0x04001587 RID: 5511
			[SurfaceDataAttributes("", false, true, FieldPrecision.Default)]
			public Vector3 color;
		}
	}
}
