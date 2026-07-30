using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000081 RID: 129
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector2Parameter : VolumeParameter<Vector2>
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000D608 File Offset: 0x0000B808
		public Vector2Parameter(Vector2 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D614 File Offset: 0x0000B814
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}
	}
}
