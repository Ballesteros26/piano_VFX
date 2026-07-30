using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x020004E8 RID: 1256
	[StructLayout(LayoutKind.Auto)]
	internal struct IndexRange
	{
		// Token: 0x04001E47 RID: 7751
		internal long m_nFromInclusive;

		// Token: 0x04001E48 RID: 7752
		internal long m_nToExclusive;

		// Token: 0x04001E49 RID: 7753
		internal volatile Shared<long> m_nSharedCurrentIndexOffset;

		// Token: 0x04001E4A RID: 7754
		internal int m_bRangeFinished;
	}
}
