using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008A RID: 138
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpRenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000D787 File Offset: 0x0000B987
		public NoInterpRenderTextureParameter(RenderTexture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
