using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000083 RID: 131
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Vector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000D663 File Offset: 0x0000B863
		public Vector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D670 File Offset: 0x0000B870
		public override void Interp(Vector3 from, Vector3 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
			this.m_Value.z = from.z + (to.z - from.z) * t;
		}
	}
}
