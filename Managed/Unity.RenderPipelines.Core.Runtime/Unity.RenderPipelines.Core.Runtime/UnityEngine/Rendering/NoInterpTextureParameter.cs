using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000088 RID: 136
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpTextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000D77D File Offset: 0x0000B97D
		public NoInterpTextureParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
