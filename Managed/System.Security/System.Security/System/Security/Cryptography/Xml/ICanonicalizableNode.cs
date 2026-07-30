using System;
using System.Text;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000060 RID: 96
	internal interface ICanonicalizableNode
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		bool IsInNodeSet { get; set; }

		// Token: 0x06000279 RID: 633
		void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc);

		// Token: 0x0600027A RID: 634
		void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc);
	}
}
