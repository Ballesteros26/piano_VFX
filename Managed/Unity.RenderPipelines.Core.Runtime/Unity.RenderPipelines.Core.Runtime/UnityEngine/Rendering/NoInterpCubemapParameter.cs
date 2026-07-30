using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008C RID: 140
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpCubemapParameter : VolumeParameter<Cubemap>
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000D791 File Offset: 0x0000B991
		public NoInterpCubemapParameter(Cubemap value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
