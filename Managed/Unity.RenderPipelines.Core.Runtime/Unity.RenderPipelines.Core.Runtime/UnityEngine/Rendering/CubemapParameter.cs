using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008B RID: 139
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class CubemapParameter : VolumeParameter<Cubemap>
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000D791 File Offset: 0x0000B991
		public CubemapParameter(Cubemap value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
