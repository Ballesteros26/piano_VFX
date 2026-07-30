using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000082 RID: 130
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x0600035A RID: 858 RVA: 0x0000D608 File Offset: 0x0000B808
		public NoInterpVector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
