using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D8 RID: 1496
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SetIteratorResult
	{
		// Token: 0x040026B6 RID: 9910
		NoMoreNodes,
		// Token: 0x040026B7 RID: 9911
		InitRightIterator,
		// Token: 0x040026B8 RID: 9912
		NeedLeftNode,
		// Token: 0x040026B9 RID: 9913
		NeedRightNode,
		// Token: 0x040026BA RID: 9914
		HaveCurrentNode
	}
}
