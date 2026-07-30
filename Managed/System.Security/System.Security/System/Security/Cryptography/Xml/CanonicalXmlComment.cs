using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000042 RID: 66
	internal class CanonicalXmlComment : XmlComment, ICanonicalizableNode
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00005456 File Offset: 0x00003656
		public CanonicalXmlComment(string comment, XmlDocument doc, bool defaultNodeSetInclusionState, bool includeComments)
			: base(comment, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
			this._includeComments = includeComments;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000546F File Offset: 0x0000366F
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005477 File Offset: 0x00003677
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005480 File Offset: 0x00003680
		public bool IncludeComments
		{
			get
			{
				return this._includeComments;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005488 File Offset: 0x00003688
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			if (docPos == DocPosition.AfterRootElement)
			{
				strBuilder.Append('\n');
			}
			strBuilder.Append("<!--");
			strBuilder.Append(this.Value);
			strBuilder.Append("-->");
			if (docPos == DocPosition.BeforeRootElement)
			{
				strBuilder.Append('\n');
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000054E4 File Offset: 0x000036E4
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] array = utf8Encoding.GetBytes("(char) 10");
			if (docPos == DocPosition.AfterRootElement)
			{
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
			array = utf8Encoding.GetBytes("<!--");
			hash.TransformBlock(array, 0, array.Length, array, 0);
			array = utf8Encoding.GetBytes(this.Value);
			hash.TransformBlock(array, 0, array.Length, array, 0);
			array = utf8Encoding.GetBytes("-->");
			hash.TransformBlock(array, 0, array.Length, array, 0);
			if (docPos == DocPosition.BeforeRootElement)
			{
				array = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(array, 0, array.Length, array, 0);
			}
		}

		// Token: 0x0400010E RID: 270
		private bool _isInNodeSet;

		// Token: 0x0400010F RID: 271
		private bool _includeComments;
	}
}
