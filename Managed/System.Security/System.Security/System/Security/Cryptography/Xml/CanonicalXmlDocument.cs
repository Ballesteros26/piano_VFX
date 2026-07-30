using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000043 RID: 67
	internal class CanonicalXmlDocument : XmlDocument, ICanonicalizableNode
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00005594 File Offset: 0x00003794
		public CanonicalXmlDocument(bool defaultNodeSetInclusionState, bool includeComments)
		{
			base.PreserveWhitespace = true;
			this._includeComments = includeComments;
			this._defaultNodeSetInclusionState = defaultNodeSetInclusionState;
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000055C5 File Offset: 0x000037C5
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000055CD File Offset: 0x000037CD
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

		// Token: 0x06000171 RID: 369 RVA: 0x000055D8 File Offset: 0x000037D8
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, docPos, anc);
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005650 File Offset: 0x00003850
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, docPos, anc);
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000056C8 File Offset: 0x000038C8
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlElement(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000056D9 File Offset: 0x000038D9
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000056D9 File Offset: 0x000038D9
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000056EA File Offset: 0x000038EA
		public override XmlText CreateTextNode(string text)
		{
			return new CanonicalXmlText(text, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000056F9 File Offset: 0x000038F9
		public override XmlWhitespace CreateWhitespace(string prefix)
		{
			return new CanonicalXmlWhitespace(prefix, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005708 File Offset: 0x00003908
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new CanonicalXmlSignificantWhitespace(text, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005717 File Offset: 0x00003917
		public override XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new CanonicalXmlProcessingInstruction(target, data, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005727 File Offset: 0x00003927
		public override XmlComment CreateComment(string data)
		{
			return new CanonicalXmlComment(data, this, this._defaultNodeSetInclusionState, this._includeComments);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000573C File Offset: 0x0000393C
		public override XmlEntityReference CreateEntityReference(string name)
		{
			return new CanonicalXmlEntityReference(name, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000574B File Offset: 0x0000394B
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new CanonicalXmlCDataSection(data, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x04000110 RID: 272
		private bool _defaultNodeSetInclusionState;

		// Token: 0x04000111 RID: 273
		private bool _includeComments;

		// Token: 0x04000112 RID: 274
		private bool _isInNodeSet;
	}
}
