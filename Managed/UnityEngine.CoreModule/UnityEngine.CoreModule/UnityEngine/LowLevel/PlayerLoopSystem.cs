using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000279 RID: 633
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public struct PlayerLoopSystem
	{
		// Token: 0x04000817 RID: 2071
		public Type type;

		// Token: 0x04000818 RID: 2072
		public PlayerLoopSystem[] subSystemList;

		// Token: 0x04000819 RID: 2073
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x0400081A RID: 2074
		public IntPtr updateFunction;

		// Token: 0x0400081B RID: 2075
		public IntPtr loopConditionFunction;

		// Token: 0x0200027A RID: 634
		// (Invoke) Token: 0x06001A5D RID: 6749
		public delegate void UpdateFunction();
	}
}
