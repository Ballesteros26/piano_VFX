using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200002B RID: 43
	internal sealed class DataDocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00005EB7 File Offset: 0x000040B7
		internal DataDocumentXPathNavigator(XmlDataDocument doc, XmlNode node)
		{
			this._curNode = new XPathNodePointer(this, doc, node);
			this._temp = new XPathNodePointer(this, doc, node);
			this._doc = doc;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005EE2 File Offset: 0x000040E2
		private DataDocumentXPathNavigator(DataDocumentXPathNavigator other)
		{
			this._curNode = other._curNode.Clone(this);
			this._temp = other._temp.Clone(this);
			this._doc = other._doc;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005F1A File Offset: 0x0000411A
		public override XPathNavigator Clone()
		{
			return new DataDocumentXPathNavigator(this);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005F22 File Offset: 0x00004122
		internal XPathNodePointer CurNode
		{
			get
			{
				return this._curNode;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005F2A File Offset: 0x0000412A
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005F32 File Offset: 0x00004132
		public override XPathNodeType NodeType
		{
			get
			{
				return this._curNode.NodeType;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005F3F File Offset: 0x0000413F
		public override string LocalName
		{
			get
			{
				return this._curNode.LocalName;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005F4C File Offset: 0x0000414C
		public override string NamespaceURI
		{
			get
			{
				return this._curNode.NamespaceURI;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005F59 File Offset: 0x00004159
		public override string Name
		{
			get
			{
				return this._curNode.Name;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005F66 File Offset: 0x00004166
		public override string Prefix
		{
			get
			{
				return this._curNode.Prefix;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005F74 File Offset: 0x00004174
		public override string Value
		{
			get
			{
				XPathNodeType nodeType = this._curNode.NodeType;
				if (nodeType != XPathNodeType.Element && nodeType != XPathNodeType.Root)
				{
					return this._curNode.Value;
				}
				return this._curNode.InnerText;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005FAB File Offset: 0x000041AB
		public override string BaseURI
		{
			get
			{
				return this._curNode.BaseURI;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005FB8 File Offset: 0x000041B8
		public override string XmlLang
		{
			get
			{
				return this._curNode.XmlLang;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005FC5 File Offset: 0x000041C5
		public override bool IsEmptyElement
		{
			get
			{
				return this._curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005FD2 File Offset: 0x000041D2
		public override XmlNameTable NameTable
		{
			get
			{
				return this._doc.NameTable;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005FDF File Offset: 0x000041DF
		public override bool HasAttributes
		{
			get
			{
				return this._curNode.AttributeCount > 0;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005FF0 File Offset: 0x000041F0
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (this._curNode.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			this._temp.MoveTo(this._curNode);
			if (!this._temp.MoveToAttribute(localName, namespaceURI))
			{
				return string.Empty;
			}
			return this._temp.Value;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006043 File Offset: 0x00004243
		public override string GetNamespace(string name)
		{
			return this._curNode.GetNamespace(name);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006051 File Offset: 0x00004251
		public override bool MoveToNamespace(string name)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNamespace(name);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000606F File Offset: 0x0000426F
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000608D File Offset: 0x0000428D
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Namespace && this._curNode.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000060AB File Offset: 0x000042AB
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000060CA File Offset: 0x000042CA
		public override bool MoveToFirstAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNextAttribute(true);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000060E8 File Offset: 0x000042E8
		public override bool MoveToNextAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Attribute && this._curNode.MoveToNextAttribute(false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006106 File Offset: 0x00004306
		public override bool MoveToNext()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToNextSibling();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006123 File Offset: 0x00004323
		public override bool MoveToPrevious()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToPreviousSibling();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006140 File Offset: 0x00004340
		public override bool MoveToFirst()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToFirst();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000615D File Offset: 0x0000435D
		public override bool HasChildren
		{
			get
			{
				return this._curNode.HasChildren;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000616A File Offset: 0x0000436A
		public override bool MoveToFirstChild()
		{
			return this._curNode.MoveToFirstChild();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006177 File Offset: 0x00004377
		public override bool MoveToParent()
		{
			return this._curNode.MoveToParent();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006184 File Offset: 0x00004384
		public override void MoveToRoot()
		{
			this._curNode.MoveToRoot();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006194 File Offset: 0x00004394
		public override bool MoveTo(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._curNode.MoveTo(dataDocumentXPathNavigator.CurNode))
				{
					this._doc = this._curNode.Document;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000061D8 File Offset: 0x000043D8
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other != null)
			{
				DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
				if (dataDocumentXPathNavigator != null && this._doc == dataDocumentXPathNavigator.Document && this._curNode.IsSamePosition(dataDocumentXPathNavigator.CurNode))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006216 File Offset: 0x00004416
		XmlNode IHasXmlNode.GetNode()
		{
			return this._curNode.Node;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006224 File Offset: 0x00004424
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return XmlNodeOrder.Unknown;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			if (dataDocumentXPathNavigator != null && dataDocumentXPathNavigator.Document == this._doc)
			{
				return this._curNode.ComparePosition(dataDocumentXPathNavigator.CurNode);
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x0400040B RID: 1035
		private readonly XPathNodePointer _curNode;

		// Token: 0x0400040C RID: 1036
		private XmlDataDocument _doc;

		// Token: 0x0400040D RID: 1037
		private readonly XPathNodePointer _temp;
	}
}
