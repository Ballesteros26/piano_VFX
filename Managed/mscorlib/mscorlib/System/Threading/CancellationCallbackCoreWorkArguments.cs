using System;

namespace System.Threading
{
	// Token: 0x02000451 RID: 1105
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06003500 RID: 13568 RVA: 0x000C4010 File Offset: 0x000C2210
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this.m_currArrayFragment = currArrayFragment;
			this.m_currArrayIndex = currArrayIndex;
		}

		// Token: 0x04001C3D RID: 7229
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> m_currArrayFragment;

		// Token: 0x04001C3E RID: 7230
		internal int m_currArrayIndex;
	}
}
