using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000041 RID: 65
	internal class CanonicalXmlCDataSection : XmlCDataSection, ICanonicalizableNode
	{
		// Token: 0x06000163 RID: 355 RVA: 0x000053DE File Offset: 0x000035DE
		public CanonicalXmlCDataSection(string data, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(data, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000053EF File Offset: 0x000035EF
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000053F7 File Offset: 0x000035F7
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

		// Token: 0x06000166 RID: 358 RVA: 0x00005400 File Offset: 0x00003600
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeCData(this.Data));
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000541C File Offset: 0x0000361C
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeCData(this.Data));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400010D RID: 269
		private bool _isInNodeSet;
	}
}
