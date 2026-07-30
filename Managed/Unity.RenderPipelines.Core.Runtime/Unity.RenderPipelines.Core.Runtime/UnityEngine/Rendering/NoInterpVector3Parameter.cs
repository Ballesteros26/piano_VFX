using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000084 RID: 132
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpVector3Parameter : VolumeParameter<Vector3>
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000D663 File Offset: 0x0000B863
		public NoInterpVector3Parameter(Vector3 value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
