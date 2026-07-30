using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000089 RID: 137
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class RenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0000D787 File Offset: 0x0000B987
		public RenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
