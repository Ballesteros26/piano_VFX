using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004A RID: 74
	internal class CanonicalXmlWhitespace : XmlWhitespace, ICanonicalizableNode
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006116 File Offset: 0x00004316
		public CanonicalXmlWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00006127 File Offset: 0x00004327
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000612F File Offset: 0x0000432F
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

		// Token: 0x060001AB RID: 427 RVA: 0x00006138 File Offset: 0x00004338
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006158 File Offset: 0x00004358
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000119 RID: 281
		private bool _isInNodeSet;
	}
}
