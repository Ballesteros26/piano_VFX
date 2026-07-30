using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000139 RID: 313
	[DebuggerDisplay("{mask.humanizedData}")]
	[Serializable]
	public struct FrameSettingsOverrideMask
	{
		// Token: 0x04000EB9 RID: 3769
		[SerializeField]
		public BitArray128 mask;
	}
}
