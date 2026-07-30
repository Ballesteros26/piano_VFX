using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000086 RID: 134
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public NoInterpVector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
