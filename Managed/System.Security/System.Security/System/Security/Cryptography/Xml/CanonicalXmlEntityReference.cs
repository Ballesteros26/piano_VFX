using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000045 RID: 69
	internal class CanonicalXmlEntityReference : XmlEntityReference, ICanonicalizableNode
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005D00 File Offset: 0x00003F00
		public CanonicalXmlEntityReference(string name, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(name, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005D11 File Offset: 0x00003F11
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005D19 File Offset: 0x00003F19
		public bool IsInNodeSet
		{
			get
			{
				return this._isInNodeSet;
			}
			set
			{
				this._isInNodeSet = value;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005D22 File Offset: 0x00003F22
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteGenericNode(this, strBuilder, docPos, anc);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005D35 File Offset: 0x00003F35
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteHashGenericNode(this, hash, docPos, anc);
			}
		}

		// Token: 0x04000114 RID: 276
		private bool _isInNodeSet;
	}
}
