using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000040 RID: 64
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	internal sealed class FogTypeParameter : VolumeParameter<FogType>
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000B443 File Offset: 0x00009643
		public FogTypeParameter(FogType value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
