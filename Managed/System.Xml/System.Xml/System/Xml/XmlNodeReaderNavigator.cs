using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000236 RID: 566
	internal class XmlNodeReaderNavigator
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00079A80 File Offset: 0x00077C80
		public XmlNodeReaderNavigator(XmlNode node)
		{
			this.curNode = node;
			this.logNode = node;
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Attribute)
			{
				this.elemNode = null;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = true;
			}
			else
			{
				this.elemNode = node;
				this.attrIndex = -1;
				this.bCreatedOnAttribute = false;
			}
			if (nodeType == XmlNodeType.Document)
			{
				this.doc = (XmlDocument)this.curNode;
			}
			else
			{
				this.doc = node.OwnerDocument;
			}
			this.nameTable = this.doc.NameTable;
			this.nAttrInd = -1;
			this.nDeclarationAttrCount = -1;
			this.nDocTypeAttrCount = -1;
			this.bOnAttrVal = false;
			this.bLogOnAttrVal = false;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00079B94 File Offset: 0x00077D94
		public XmlNodeType NodeType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd == -1)
				{
					return nodeType;
				}
				if (this.bOnAttrVal)
				{
					return XmlNodeType.Text;
				}
				return XmlNodeType.Attribute;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00079BC3 File Offset: 0x00077DC3
		public string NamespaceURI
		{
			get
			{
				return this.curNode.NamespaceURI;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x00079BD0 File Offset: 0x00077DD0
		public string Name
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					if (this.bOnAttrVal)
					{
						return string.Empty;
					}
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].name;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].name;
				}
				else
				{
					if (this.IsLocalNameEmpty(this.curNode.NodeType))
					{
						return string.Empty;
					}
					return this.curNode.Name;
				}
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00079C55 File Offset: 0x00077E55
		public string LocalName
		{
			get
			{
				if (this.nAttrInd != -1)
				{
					return this.Name;
				}
				if (this.IsLocalNameEmpty(this.curNode.NodeType))
				{
					return string.Empty;
				}
				return this.curNode.LocalName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00079C8B File Offset: 0x00077E8B
		internal bool IsOnAttrVal
		{
			get
			{
				return this.bOnAttrVal;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00079C93 File Offset: 0x00077E93
		internal XmlNode OwnerElementNode
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return null;
				}
				return this.elemNode;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00079CA5 File Offset: 0x00077EA5
		internal bool CreatedOnAttribute
		{
			get
			{
				return this.bCreatedOnAttribute;
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00079CB0 File Offset: 0x00077EB0
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00079D12 File Offset: 0x00077F12
		public string Prefix
		{
			get
			{
				return this.curNode.Prefix;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x00079D1F File Offset: 0x00077F1F
		public bool HasValue
		{
			get
			{
				return this.nAttrInd != -1 || (this.curNode.Value != null || this.curNode.NodeType == XmlNodeType.DocumentType);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00079D4C File Offset: 0x00077F4C
		public string Value
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				if (this.nAttrInd != -1)
				{
					if (this.curNode.NodeType == XmlNodeType.XmlDeclaration)
					{
						return this.decNodeAttributes[this.nAttrInd].value;
					}
					return this.docTypeNodeAttributes[this.nAttrInd].value;
				}
				else
				{
					string text;
					if (nodeType == XmlNodeType.DocumentType)
					{
						text = ((XmlDocumentType)this.curNode).InternalSubset;
					}
					else if (nodeType == XmlNodeType.XmlDeclaration)
					{
						StringBuilder stringBuilder = new StringBuilder(string.Empty);
						if (this.nDeclarationAttrCount == -1)
						{
							this.InitDecAttr();
						}
						for (int i = 0; i < this.nDeclarationAttrCount; i++)
						{
							stringBuilder.Append(this.decNodeAttributes[i].name + "=\"" + this.decNodeAttributes[i].value + "\"");
							if (i != this.nDeclarationAttrCount - 1)
							{
								stringBuilder.Append(" ");
							}
						}
						text = stringBuilder.ToString();
					}
					else
					{
						text = this.curNode.Value;
					}
					if (text != null)
					{
						return text;
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00079E69 File Offset: 0x00078069
		public string BaseURI
		{
			get
			{
				return this.curNode.BaseURI;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00079E76 File Offset: 0x00078076
		public XmlSpace XmlSpace
		{
			get
			{
				return this.curNode.XmlSpace;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00079E83 File Offset: 0x00078083
		public string XmlLang
		{
			get
			{
				return this.curNode.XmlLang;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00079E90 File Offset: 0x00078090
		public bool IsEmptyElement
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Element && ((XmlElement)this.curNode).IsEmpty;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00079EB2 File Offset: 0x000780B2
		public bool IsDefault
		{
			get
			{
				return this.curNode.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.curNode).Specified;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00079ED7 File Offset: 0x000780D7
		public IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.curNode.SchemaInfo;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00079EE4 File Offset: 0x000780E4
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00079EEC File Offset: 0x000780EC
		public int AttributeCount
		{
			get
			{
				if (this.bCreatedOnAttribute)
				{
					return 0;
				}
				XmlNodeType nodeType = this.curNode.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return ((XmlElement)this.curNode).Attributes.Count;
				}
				if (nodeType == XmlNodeType.Attribute || (this.bOnAttrVal && nodeType != XmlNodeType.XmlDeclaration && nodeType != XmlNodeType.DocumentType))
				{
					return this.elemNode.Attributes.Count;
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount != -1)
					{
						return this.nDeclarationAttrCount;
					}
					this.InitDecAttr();
					return this.nDeclarationAttrCount;
				}
				else
				{
					if (nodeType != XmlNodeType.DocumentType)
					{
						return 0;
					}
					if (this.nDocTypeAttrCount != -1)
					{
						return this.nDocTypeAttrCount;
					}
					this.InitDocTypeAttr();
					return this.nDocTypeAttrCount;
				}
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00079F95 File Offset: 0x00078195
		private void CheckIndexCondition(int attributeIndex)
		{
			if (attributeIndex < 0 || attributeIndex >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("attributeIndex");
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00079FB0 File Offset: 0x000781B0
		private void InitDecAttr()
		{
			int num = 0;
			string text = this.doc.Version;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "version";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Encoding;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "encoding";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			text = this.doc.Standalone;
			if (text != null && text.Length != 0)
			{
				this.decNodeAttributes[num].name = "standalone";
				this.decNodeAttributes[num].value = text;
				num++;
			}
			this.nDeclarationAttrCount = num;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0007A08F File Offset: 0x0007828F
		public string GetDeclarationAttr(XmlDeclaration decl, string name)
		{
			if (name == "version")
			{
				return decl.Version;
			}
			if (name == "encoding")
			{
				return decl.Encoding;
			}
			if (name == "standalone")
			{
				return decl.Standalone;
			}
			return null;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0007A0CE File Offset: 0x000782CE
		public string GetDeclarationAttr(int i)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			return this.decNodeAttributes[i].value;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0007A0F0 File Offset: 0x000782F0
		public int GetDecAttrInd(string name)
		{
			if (this.nDeclarationAttrCount == -1)
			{
				this.InitDecAttr();
			}
			for (int i = 0; i < this.nDeclarationAttrCount; i++)
			{
				if (this.decNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0007A13C File Offset: 0x0007833C
		private void InitDocTypeAttr()
		{
			int num = 0;
			XmlDocumentType documentType = this.doc.DocumentType;
			if (documentType == null)
			{
				this.nDocTypeAttrCount = 0;
				return;
			}
			string text = documentType.PublicId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "PUBLIC";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			text = documentType.SystemId;
			if (text != null)
			{
				this.docTypeNodeAttributes[num].name = "SYSTEM";
				this.docTypeNodeAttributes[num].value = text;
				num++;
			}
			this.nDocTypeAttrCount = num;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0007A1D5 File Offset: 0x000783D5
		public string GetDocumentTypeAttr(XmlDocumentType docType, string name)
		{
			if (name == "PUBLIC")
			{
				return docType.PublicId;
			}
			if (name == "SYSTEM")
			{
				return docType.SystemId;
			}
			return null;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0007A200 File Offset: 0x00078400
		public string GetDocumentTypeAttr(int i)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			return this.docTypeNodeAttributes[i].value;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0007A224 File Offset: 0x00078424
		public int GetDocTypeAttrInd(string name)
		{
			if (this.nDocTypeAttrCount == -1)
			{
				this.InitDocTypeAttr();
			}
			for (int i = 0; i < this.nDocTypeAttrCount; i++)
			{
				if (this.docTypeNodeAttributes[i].name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0007A270 File Offset: 0x00078470
		private string GetAttributeFromElement(XmlElement elem, string name)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0007A290 File Offset: 0x00078490
		public string GetAttribute(string name)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name);
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			return null;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0007A31C File Offset: 0x0007851C
		private string GetAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute attributeNode = elem.GetAttributeNode(name, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return null;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0007A340 File Offset: 0x00078540
		public string GetAttribute(string name, string ns)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					return this.GetAttributeFromElement((XmlElement)this.curNode, name, ns);
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					return this.GetAttributeFromElement((XmlElement)this.elemNode, name, ns);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (ns.Length != 0)
					{
						return null;
					}
					return this.GetDeclarationAttr((XmlDeclaration)this.curNode, name);
				}
			}
			else
			{
				if (ns.Length != 0)
				{
					return null;
				}
				return this.GetDocumentTypeAttr((XmlDocumentType)this.curNode, name);
			}
			return null;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0007A3E0 File Offset: 0x000785E0
		public string GetAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.curNode).Attributes[attributeIndex].Value;
				}
				if (nodeType == XmlNodeType.Attribute)
				{
					this.CheckIndexCondition(attributeIndex);
					return ((XmlElement)this.elemNode).Attributes[attributeIndex].Value;
				}
			}
			else
			{
				if (nodeType == XmlNodeType.DocumentType)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDocumentTypeAttr(attributeIndex);
				}
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					this.CheckIndexCondition(attributeIndex);
					return this.GetDeclarationAttr(attributeIndex);
				}
			}
			throw new ArgumentOutOfRangeException("attributeIndex");
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0007A48B File Offset: 0x0007868B
		public void LogMove(int level)
		{
			this.logNode = this.curNode;
			this.nLogLevel = level;
			this.nLogAttrInd = this.nAttrInd;
			this.logAttrIndex = this.attrIndex;
			this.bLogOnAttrVal = this.bOnAttrVal;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0007A4C4 File Offset: 0x000786C4
		public void RollBackMove(ref int level)
		{
			this.curNode = this.logNode;
			level = this.nLogLevel;
			this.nAttrInd = this.nLogAttrInd;
			this.attrIndex = this.logAttrIndex;
			this.bOnAttrVal = this.bLogOnAttrVal;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0007A500 File Offset: 0x00078700
		private bool IsOnDeclOrDocType
		{
			get
			{
				XmlNodeType nodeType = this.curNode.NodeType;
				return nodeType == XmlNodeType.XmlDeclaration || nodeType == XmlNodeType.DocumentType;
			}
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0007A528 File Offset: 0x00078728
		public void ResetToAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.bOnAttrVal)
			{
				if (this.IsOnDeclOrDocType)
				{
					level -= 2;
				}
				else
				{
					while (this.curNode.NodeType != XmlNodeType.Attribute && (this.curNode = this.curNode.ParentNode) != null)
					{
						level--;
					}
				}
				this.bOnAttrVal = false;
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0007A588 File Offset: 0x00078788
		public void ResetMove(ref int level, ref XmlNodeType nt)
		{
			this.LogMove(level);
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			if (this.nAttrInd != -1)
			{
				if (this.bOnAttrVal)
				{
					level--;
					this.bOnAttrVal = false;
				}
				this.nLogAttrInd = this.nAttrInd;
				level--;
				this.nAttrInd = -1;
				nt = this.curNode.NodeType;
				return;
			}
			if (this.bOnAttrVal && this.curNode.NodeType != XmlNodeType.Attribute)
			{
				this.ResetToAttribute(ref level);
			}
			if (this.curNode.NodeType == XmlNodeType.Attribute)
			{
				this.curNode = ((XmlAttribute)this.curNode).OwnerElement;
				this.attrIndex = -1;
				level--;
				nt = XmlNodeType.Element;
			}
			if (this.curNode.NodeType == XmlNodeType.Element)
			{
				this.elemNode = this.curNode;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0007A656 File Offset: 0x00078856
		public bool MoveToAttribute(string name)
		{
			return this.MoveToAttribute(name, string.Empty);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0007A664 File Offset: 0x00078864
		private bool MoveToAttributeFromElement(XmlElement elem, string name, string ns)
		{
			XmlAttribute xmlAttribute;
			if (ns.Length == 0)
			{
				xmlAttribute = elem.GetAttributeNode(name);
			}
			else
			{
				xmlAttribute = elem.GetAttributeNode(name, ns);
			}
			if (xmlAttribute != null)
			{
				this.bOnAttrVal = false;
				this.elemNode = elem;
				this.curNode = xmlAttribute;
				this.attrIndex = elem.Attributes.FindNodeOffsetNS(xmlAttribute);
				if (this.attrIndex != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0007A6C4 File Offset: 0x000788C4
		public bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.curNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.Attribute)
			{
				return this.MoveToAttributeFromElement((XmlElement)this.elemNode, name, namespaceURI);
			}
			if (nodeType == XmlNodeType.XmlDeclaration && namespaceURI.Length == 0)
			{
				if ((this.nAttrInd = this.GetDecAttrInd(name)) != -1)
				{
					this.bOnAttrVal = false;
					return true;
				}
			}
			else if (nodeType == XmlNodeType.DocumentType && namespaceURI.Length == 0 && (this.nAttrInd = this.GetDocTypeAttrInd(name)) != -1)
			{
				this.bOnAttrVal = false;
				return true;
			}
			return false;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0007A76C File Offset: 0x0007896C
		public void MoveToAttribute(int attributeIndex)
		{
			if (this.bCreatedOnAttribute)
			{
				return;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType <= XmlNodeType.Attribute)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType != XmlNodeType.Attribute)
					{
						return;
					}
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.elemNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
				else
				{
					this.CheckIndexCondition(attributeIndex);
					XmlAttribute xmlAttribute = ((XmlElement)this.curNode).Attributes[attributeIndex];
					if (xmlAttribute != null)
					{
						this.elemNode = this.curNode;
						this.curNode = xmlAttribute;
						this.attrIndex = attributeIndex;
						return;
					}
				}
			}
			else
			{
				if (nodeType != XmlNodeType.DocumentType && nodeType != XmlNodeType.XmlDeclaration)
				{
					return;
				}
				this.CheckIndexCondition(attributeIndex);
				this.nAttrInd = attributeIndex;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0007A824 File Offset: 0x00078A24
		public bool MoveToNextAttribute(ref int level)
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.Element)
				{
					if (this.curNode.Attributes.Count > 0)
					{
						level++;
						this.elemNode = this.curNode;
						this.curNode = this.curNode.Attributes[0];
						this.attrIndex = 0;
						return true;
					}
				}
				else if (nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nDeclarationAttrCount == -1)
					{
						this.InitDecAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDeclarationAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				else if (nodeType == XmlNodeType.DocumentType)
				{
					if (this.nDocTypeAttrCount == -1)
					{
						this.InitDocTypeAttr();
					}
					this.nAttrInd++;
					if (this.nAttrInd < this.nDocTypeAttrCount)
					{
						if (this.nAttrInd == 0)
						{
							level++;
						}
						this.bOnAttrVal = false;
						return true;
					}
					this.nAttrInd--;
				}
				return false;
			}
			if (this.attrIndex >= this.elemNode.Attributes.Count - 1)
			{
				return false;
			}
			XmlAttributeCollection attributes = this.elemNode.Attributes;
			int num = this.attrIndex + 1;
			this.attrIndex = num;
			this.curNode = attributes[num];
			return true;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0007A98C File Offset: 0x00078B8C
		public bool MoveToParent()
		{
			XmlNode parentNode = this.curNode.ParentNode;
			if (parentNode != null)
			{
				this.curNode = parentNode;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = 0;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0007A9C4 File Offset: 0x00078BC4
		public bool MoveToFirstChild()
		{
			XmlNode firstChild = this.curNode.FirstChild;
			if (firstChild != null)
			{
				this.curNode = firstChild;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0007A9FC File Offset: 0x00078BFC
		private bool MoveToNextSibling(XmlNode node)
		{
			XmlNode nextSibling = node.NextSibling;
			if (nextSibling != null)
			{
				this.curNode = nextSibling;
				if (!this.bOnAttrVal)
				{
					this.attrIndex = -1;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0007AA2C File Offset: 0x00078C2C
		public bool MoveToNext()
		{
			if (this.curNode.NodeType != XmlNodeType.Attribute)
			{
				return this.MoveToNextSibling(this.curNode);
			}
			return this.MoveToNextSibling(this.elemNode);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0007AA58 File Offset: 0x00078C58
		public bool MoveToElement()
		{
			if (this.bCreatedOnAttribute)
			{
				return false;
			}
			XmlNodeType nodeType = this.curNode.NodeType;
			if (nodeType != XmlNodeType.Attribute)
			{
				if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
				{
					if (this.nAttrInd != -1)
					{
						this.nAttrInd = -1;
						return true;
					}
				}
			}
			else if (this.elemNode != null)
			{
				this.curNode = this.elemNode;
				this.attrIndex = -1;
				return true;
			}
			return false;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0007AABC File Offset: 0x00078CBC
		public string LookupNamespace(string prefix)
		{
			if (this.bCreatedOnAttribute)
			{
				return null;
			}
			if (prefix == "xmlns")
			{
				return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			}
			if (prefix == "xml")
			{
				return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			string text;
			if (prefix.Length == 0)
			{
				text = "xmlns";
			}
			else
			{
				text = "xmlns:" + prefix;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttribute attributeNode = xmlElement.GetAttributeNode(text);
						if (attributeNode != null)
						{
							return attributeNode.Value;
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (prefix.Length == 0)
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0007AB98 File Offset: 0x00078D98
		internal string DefaultLookupNamespace(string prefix)
		{
			if (!this.bCreatedOnAttribute)
			{
				if (prefix == "xmlns")
				{
					return this.nameTable.Add("http://www.w3.org/2000/xmlns/");
				}
				if (prefix == "xml")
				{
					return this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
				}
				if (prefix == string.Empty)
				{
					return this.nameTable.Add(string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0007AC08 File Offset: 0x00078E08
		internal string LookupPrefix(string namespaceName)
		{
			if (this.bCreatedOnAttribute || namespaceName == null)
			{
				return null;
			}
			if (namespaceName == "http://www.w3.org/2000/xmlns/")
			{
				return this.nameTable.Add("xmlns");
			}
			if (namespaceName == "http://www.w3.org/XML/1998/namespace")
			{
				return this.nameTable.Add("xml");
			}
			if (namespaceName == string.Empty)
			{
				return string.Empty;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.Value == namespaceName)
							{
								if (xmlAttribute.Prefix.Length == 0 && xmlAttribute.LocalName == "xmlns")
								{
									if (this.LookupNamespace(string.Empty) == namespaceName)
									{
										return string.Empty;
									}
								}
								else if (xmlAttribute.Prefix == "xmlns")
								{
									string localName = xmlAttribute.LocalName;
									if (this.LookupNamespace(localName) == namespaceName)
									{
										return this.nameTable.Add(localName);
									}
								}
							}
						}
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			return null;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0007AD6C File Offset: 0x00078F6C
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (this.bCreatedOnAttribute)
			{
				return dictionary;
			}
			for (XmlNode xmlNode = this.curNode; xmlNode != null; xmlNode = xmlNode.ParentNode)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement.HasAttributes)
					{
						XmlAttributeCollection attributes = xmlElement.Attributes;
						for (int i = 0; i < attributes.Count; i++)
						{
							XmlAttribute xmlAttribute = attributes[i];
							if (xmlAttribute.LocalName == "xmlns" && xmlAttribute.Prefix.Length == 0)
							{
								if (!dictionary.ContainsKey(string.Empty))
								{
									dictionary.Add(this.nameTable.Add(string.Empty), this.nameTable.Add(xmlAttribute.Value));
								}
							}
							else if (xmlAttribute.Prefix == "xmlns")
							{
								string localName = xmlAttribute.LocalName;
								if (!dictionary.ContainsKey(localName))
								{
									dictionary.Add(this.nameTable.Add(localName), this.nameTable.Add(xmlAttribute.Value));
								}
							}
						}
					}
					if (scope == XmlNamespaceScope.Local)
					{
						break;
					}
				}
				else if (xmlNode.NodeType == XmlNodeType.Attribute)
				{
					xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					continue;
				}
			}
			if (scope != XmlNamespaceScope.Local)
			{
				if (dictionary.ContainsKey(string.Empty) && dictionary[string.Empty] == string.Empty)
				{
					dictionary.Remove(string.Empty);
				}
				if (scope == XmlNamespaceScope.All)
				{
					dictionary.Add(this.nameTable.Add("xml"), this.nameTable.Add("http://www.w3.org/XML/1998/namespace"));
				}
			}
			return dictionary;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0007AF0C File Offset: 0x0007910C
		public bool ReadAttributeValue(ref int level, ref bool bResolveEntity, ref XmlNodeType nt)
		{
			if (this.nAttrInd == -1)
			{
				if (this.curNode.NodeType == XmlNodeType.Attribute)
				{
					XmlNode firstChild = this.curNode.FirstChild;
					if (firstChild != null)
					{
						this.curNode = firstChild;
						nt = this.curNode.NodeType;
						level++;
						this.bOnAttrVal = true;
						return true;
					}
				}
				else if (this.bOnAttrVal)
				{
					if ((this.curNode.NodeType == XmlNodeType.EntityReference) & bResolveEntity)
					{
						this.curNode = this.curNode.FirstChild;
						nt = this.curNode.NodeType;
						level++;
						bResolveEntity = false;
						return true;
					}
					XmlNode nextSibling = this.curNode.NextSibling;
					if (nextSibling == null)
					{
						XmlNode parentNode = this.curNode.ParentNode;
						if (parentNode != null && parentNode.NodeType == XmlNodeType.EntityReference)
						{
							this.curNode = parentNode;
							nt = XmlNodeType.EndEntity;
							level--;
							return true;
						}
					}
					if (nextSibling != null)
					{
						this.curNode = nextSibling;
						nt = this.curNode.NodeType;
						return true;
					}
					return false;
				}
				return false;
			}
			if (!this.bOnAttrVal)
			{
				this.bOnAttrVal = true;
				level++;
				nt = XmlNodeType.Text;
				return true;
			}
			return false;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0007B020 File Offset: 0x00079220
		public XmlDocument Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x04000E04 RID: 3588
		private XmlNode curNode;

		// Token: 0x04000E05 RID: 3589
		private XmlNode elemNode;

		// Token: 0x04000E06 RID: 3590
		private XmlNode logNode;

		// Token: 0x04000E07 RID: 3591
		private int attrIndex;

		// Token: 0x04000E08 RID: 3592
		private int logAttrIndex;

		// Token: 0x04000E09 RID: 3593
		private XmlNameTable nameTable;

		// Token: 0x04000E0A RID: 3594
		private XmlDocument doc;

		// Token: 0x04000E0B RID: 3595
		private int nAttrInd;

		// Token: 0x04000E0C RID: 3596
		private const string strPublicID = "PUBLIC";

		// Token: 0x04000E0D RID: 3597
		private const string strSystemID = "SYSTEM";

		// Token: 0x04000E0E RID: 3598
		private const string strVersion = "version";

		// Token: 0x04000E0F RID: 3599
		private const string strStandalone = "standalone";

		// Token: 0x04000E10 RID: 3600
		private const string strEncoding = "encoding";

		// Token: 0x04000E11 RID: 3601
		private int nDeclarationAttrCount;

		// Token: 0x04000E12 RID: 3602
		private int nDocTypeAttrCount;

		// Token: 0x04000E13 RID: 3603
		private int nLogLevel;

		// Token: 0x04000E14 RID: 3604
		private int nLogAttrInd;

		// Token: 0x04000E15 RID: 3605
		private bool bLogOnAttrVal;

		// Token: 0x04000E16 RID: 3606
		private bool bCreatedOnAttribute;

		// Token: 0x04000E17 RID: 3607
		internal XmlNodeReaderNavigator.VirtualAttribute[] decNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000E18 RID: 3608
		internal XmlNodeReaderNavigator.VirtualAttribute[] docTypeNodeAttributes = new XmlNodeReaderNavigator.VirtualAttribute[]
		{
			new XmlNodeReaderNavigator.VirtualAttribute(null, null),
			new XmlNodeReaderNavigator.VirtualAttribute(null, null)
		};

		// Token: 0x04000E19 RID: 3609
		private bool bOnAttrVal;

		// Token: 0x02000237 RID: 567
		internal struct VirtualAttribute
		{
			// Token: 0x060015E5 RID: 5605 RVA: 0x0007B028 File Offset: 0x00079228
			internal VirtualAttribute(string name, string value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x04000E1A RID: 3610
			internal string name;

			// Token: 0x04000E1B RID: 3611
			internal string value;
		}
	}
}
