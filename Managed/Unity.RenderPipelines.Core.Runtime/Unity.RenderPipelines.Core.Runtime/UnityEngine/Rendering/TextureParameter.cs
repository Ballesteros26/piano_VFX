using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000087 RID: 135
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class TextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x06000361 RID: 865 RVA: 0x0000D77D File Offset: 0x0000B97D
		public TextureParameter(Texture value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
