using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A5 RID: 165
	internal struct DensityVolumeList
	{
		// Token: 0x04000696 RID: 1686
		public List<OrientedBBox> bounds;

		// Token: 0x04000697 RID: 1687
		public List<DensityVolumeEngineData> density;
	}
}
