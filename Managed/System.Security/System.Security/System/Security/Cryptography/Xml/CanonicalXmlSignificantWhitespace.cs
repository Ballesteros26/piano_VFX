using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000048 RID: 72
	internal class CanonicalXmlSignificantWhitespace : XmlSignificantWhitespace, ICanonicalizableNode
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000601E File Offset: 0x0000421E
		public CanonicalXmlSignificantWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000602F File Offset: 0x0000422F
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00006037 File Offset: 0x00004237
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

		// Token: 0x060001A1 RID: 417 RVA: 0x00006040 File Offset: 0x00004240
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006060 File Offset: 0x00004260
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000117 RID: 279
		private bool _isInNodeSet;
	}
}
