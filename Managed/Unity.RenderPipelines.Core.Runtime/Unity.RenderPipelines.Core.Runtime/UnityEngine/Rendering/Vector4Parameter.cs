using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000085 RID: 133
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector4Parameter : VolumeParameter<Vector4>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public Vector4Parameter(Vector4 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public override void Interp(Vector4 from, Vector4 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
			this.m_Value.w = from.w + (to.w - from.w) * t;
		}
	}
}
