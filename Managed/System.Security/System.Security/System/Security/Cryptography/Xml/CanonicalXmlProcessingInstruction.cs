using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000047 RID: 71
	internal class CanonicalXmlProcessingInstruction : XmlProcessingInstruction, ICanonicalizableNode
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00005E92 File Offset: 0x00004092
		public CanonicalXmlProcessingInstruction(string target, string data, XmlDocument doc, bool defaultNodeSetInclusionState)
			: base(target, data, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005EA5 File Offset: 0x000040A5
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005EAD File Offset: 0x000040AD
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

		// Token: 0x0600019C RID: 412 RVA: 0x00005EB8 File Offset: 0x000040B8
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet)
			{
				return;
			}
			if (docPos == DocPosition.AfterRootElement)
			{
				strBuilder.Append('\n');
			}
			strBuilder.Append("<?");
			strBuilder.Append(this.Name);
			if (this.Value != null && this.Value.Length > 0)
			{
				strBuilder.Append(" " + this.Value);
			}
			strBuilder.Append("?>");
			if (docPos == DocPosition.BeforeRootElement)
			{
				strBuilder.Append('\n');
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005F3C File Offset: 0x0000413C
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet)
			{
				return;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] array;
			if (docPos == DocPosition.AfterRootElement)
			{
				array = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
			array = utf8Encoding.GetBytes("<?");
			hash.TransformBlock(array, 0, array.Length, array, 0);
			array = utf8Encoding.GetBytes(this.Name);
			hash.TransformBlock(array, 0, array.Length, array, 0);
			if (this.Value != null && this.Value.Length > 0)
			{
				array = utf8Encoding.GetBytes(" " + this.Value);
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
			array = utf8Encoding.GetBytes("?>");
			hash.TransformBlock(array, 0, array.Length, array, 0);
			if (docPos == DocPosition.BeforeRootElement)
			{
				array = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
		}

		// Token: 0x04000116 RID: 278
		private bool _isInNodeSet;
	}
}
