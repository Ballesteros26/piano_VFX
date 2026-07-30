using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006C RID: 108
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class LayerMaskParameter : VolumeParameter<LayerMask>
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000D194 File Offset: 0x0000B394
		public LayerMaskParameter(LayerMask value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
