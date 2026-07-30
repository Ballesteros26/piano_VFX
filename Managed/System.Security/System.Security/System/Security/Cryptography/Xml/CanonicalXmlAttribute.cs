using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000040 RID: 64
	internal class CanonicalXmlAttribute : XmlAttribute, ICanonicalizableNode
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00005305 File Offset: 0x00003505
		public CanonicalXmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(prefix, localName, namespaceURI, doc)
		{
			this.IsInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000531A File Offset: 0x0000351A
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005322 File Offset: 0x00003522
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

		// Token: 0x06000161 RID: 353 RVA: 0x0000532B File Offset: 0x0000352B
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			strBuilder.Append(" " + this.Name + "=\"");
			strBuilder.Append(Utils.EscapeAttributeValue(this.Value));
			strBuilder.Append("\"");
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005368 File Offset: 0x00003568
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] array = utf8Encoding.GetBytes(" " + this.Name + "=\"");
			hash.TransformBlock(array, 0, array.Length, array, 0);
			array = utf8Encoding.GetBytes(Utils.EscapeAttributeValue(this.Value));
			hash.TransformBlock(array, 0, array.Length, array, 0);
			array = utf8Encoding.GetBytes("\"");
			hash.TransformBlock(array, 0, array.Length, array, 0);
		}

		// Token: 0x0400010C RID: 268
		private bool _isInNodeSet;
	}
}
