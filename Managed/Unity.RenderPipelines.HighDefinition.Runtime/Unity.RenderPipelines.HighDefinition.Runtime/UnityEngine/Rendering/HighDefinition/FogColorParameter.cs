using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000041 RID: 65
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class FogColorParameter : VolumeParameter<FogColorMode>
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000B44D File Offset: 0x0000964D
		public FogColorParameter(FogColorMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
