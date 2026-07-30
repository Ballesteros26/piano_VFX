using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000049 RID: 73
	internal class CanonicalXmlText : XmlText, ICanonicalizableNode
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000609E File Offset: 0x0000429E
		public CanonicalXmlText(string strData, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(strData, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000060AF File Offset: 0x000042AF
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000060B7 File Offset: 0x000042B7
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

		// Token: 0x060001A6 RID: 422 RVA: 0x000060C0 File Offset: 0x000042C0
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeTextData(this.Value));
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000060DC File Offset: 0x000042DC
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeTextData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000118 RID: 280
		private bool _isInNodeSet;
	}
}
