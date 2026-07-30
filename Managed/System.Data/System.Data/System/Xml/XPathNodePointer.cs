using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000032 RID: 50
	internal sealed class XPathNodePointer : IXmlDataVirtualNode
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000076F8 File Offset: 0x000058F8
		static XPathNodePointer()
		{
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[0] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[1] = 1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[2] = 2;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[3] = 4;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[4] = 4;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[5] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[6] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[7] = 7;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[8] = 8;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[9] = 0;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[10] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[11] = 0;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[12] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[13] = 6;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[14] = 5;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[15] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[16] = -1;
			XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[17] = -1;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000077AC File Offset: 0x000059AC
		private XPathNodeType DecideXPNodeTypeForTextNodes(XmlNode node)
		{
			XPathNodeType xpathNodeType = XPathNodeType.Whitespace;
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType - XmlNodeType.Text <= 1)
				{
					return XPathNodeType.Text;
				}
				if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return xpathNodeType;
					}
					xpathNodeType = XPathNodeType.SignificantWhitespace;
				}
				node = this._doc.SafeNextSibling(node);
			}
			return xpathNodeType;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000077F0 File Offset: 0x000059F0
		private XPathNodeType ConvertNodeType(XmlNode node)
		{
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				return this.DecideXPNodeTypeForTextNodes(node);
			}
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType];
			if (num != 2)
			{
				return (XPathNodeType)num;
			}
			if (node.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return XPathNodeType.Namespace;
			}
			return XPathNodeType.Attribute;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000783C File Offset: 0x00005A3C
		private bool IsNamespaceNode(XmlNodeType nt, string ns)
		{
			return nt == XmlNodeType.Attribute && ns == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000784F File Offset: 0x00005A4F
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node)
			: this(owner, doc, node, null, false, null)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000785D File Offset: 0x00005A5D
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XPathNodePointer pointer)
			: this(owner, pointer._doc, pointer._node, pointer._column, pointer._fOnValue, pointer._parentOfNS)
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007884 File Offset: 0x00005A84
		private XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node, DataColumn c, bool bOnValue, XmlBoundElement parentOfNS)
		{
			this._owner = new WeakReference(owner);
			this._doc = doc;
			this._node = node;
			this._column = c;
			this._fOnValue = bOnValue;
			this._parentOfNS = parentOfNS;
			this._doc.AddPointer(this);
			this._bNeedFoliate = false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000078DC File Offset: 0x00005ADC
		internal XPathNodePointer Clone(DataDocumentXPathNavigator owner)
		{
			this.RealFoliate();
			return new XPathNodePointer(owner, this);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000078EB File Offset: 0x00005AEB
		internal bool IsEmptyElement
		{
			get
			{
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007920 File Offset: 0x00005B20
		internal XPathNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return XPathNodeType.All;
				}
				if (this._column == null)
				{
					return this.ConvertNodeType(this._node);
				}
				if (this._fOnValue)
				{
					return XPathNodeType.Text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute)
				{
					return XPathNodeType.Element;
				}
				if (this._column.Namespace == "http://www.w3.org/2000/xmlns/")
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007988 File Offset: 0x00005B88
		internal string LocalName
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI) && this._node.LocalName == "xmlns")
					{
						return string.Empty;
					}
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
					{
						return this._node.LocalName;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007A38 File Offset: 0x00005C38
		internal string Name
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI))
					{
						if (this._node.LocalName == "xmlns")
						{
							return string.Empty;
						}
						return this._node.LocalName;
					}
					else
					{
						if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
						{
							return this._node.Name;
						}
						return string.Empty;
					}
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00007AF4 File Offset: 0x00005CF4
		internal string NamespaceURI
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XPathNodeType xpathNodeType = this.ConvertNodeType(this._node);
					if (xpathNodeType == XPathNodeType.Element || xpathNodeType == XPathNodeType.Root || xpathNodeType == XPathNodeType.Attribute)
					{
						return this._node.NamespaceURI;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					if (this._column.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.Namespace);
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007B90 File Offset: 0x00005D90
		internal string Prefix
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column != null)
				{
					return string.Empty;
				}
				if (this.IsNamespaceNode(this._node.NodeType, this._node.NamespaceURI))
				{
					return string.Empty;
				}
				return this._node.Prefix;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007BF0 File Offset: 0x00005DF0
		internal string Value
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				if (this._column == null)
				{
					string text = this._node.Value;
					if (XmlDataDocument.IsTextNode(this._node.NodeType))
					{
						if (this._node.ParentNode == null)
						{
							return text;
						}
						XmlNode xmlNode = this._doc.SafeNextSibling(this._node);
						while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
						{
							text += xmlNode.Value;
							xmlNode = this._doc.SafeNextSibling(xmlNode);
						}
					}
					return text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute && !this._fOnValue)
				{
					return null;
				}
				DataRow row = this.Row;
				DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
				object obj = row[this._column, dataRowVersion];
				if (!Convert.IsDBNull(obj))
				{
					return this._column.ConvertObjectToXml(obj);
				}
				return null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007CDC File Offset: 0x00005EDC
		internal string InnerText
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					if (this._node.NodeType != XmlNodeType.Document)
					{
						return this._node.InnerText;
					}
					XmlElement documentElement = ((XmlDocument)this._node).DocumentElement;
					if (documentElement != null)
					{
						return documentElement.InnerText;
					}
					return string.Empty;
				}
				else
				{
					DataRow row = this.Row;
					DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
					object obj = row[this._column, dataRowVersion];
					if (!Convert.IsDBNull(obj))
					{
						return this._column.ConvertObjectToXml(obj);
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007D84 File Offset: 0x00005F84
		internal string BaseURI
		{
			get
			{
				this.RealFoliate();
				if (this._node != null)
				{
					return this._node.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00007DA8 File Offset: 0x00005FA8
		internal string XmlLang
		{
			get
			{
				this.RealFoliate();
				XmlNode xmlNode = this._node;
				while (xmlNode != null)
				{
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						if (xmlBoundElement.ElementState == ElementState.Defoliated)
						{
							DataRow row = xmlBoundElement.Row;
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (dataColumn.Prefix == "xml" && dataColumn.EncodedColumnName == "lang")
									{
										object obj2 = row[dataColumn];
										if (obj2 == DBNull.Value)
										{
											break;
										}
										return (string)obj2;
									}
								}
								goto IL_00D4;
							}
						}
						if (xmlBoundElement.HasAttribute("xml:lang"))
						{
							return xmlBoundElement.GetAttribute("xml:lang");
						}
					}
					IL_00D4:
					if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007EC8 File Offset: 0x000060C8
		private XmlBoundElement GetRowElement()
		{
			XmlBoundElement xmlBoundElement;
			if (this._column != null)
			{
				xmlBoundElement = this._node as XmlBoundElement;
				return xmlBoundElement;
			}
			this._doc.Mapper.GetRegion(this._node, out xmlBoundElement);
			return xmlBoundElement;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007F08 File Offset: 0x00006108
		private DataRow Row
		{
			get
			{
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement == null)
				{
					return null;
				}
				return rowElement.Row;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007F28 File Offset: 0x00006128
		internal bool MoveTo(XPathNodePointer pointer)
		{
			if (this._doc != pointer._doc)
			{
				return false;
			}
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
			this._bNeedFoliate = pointer._bNeedFoliate;
			return true;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007F76 File Offset: 0x00006176
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007F8D File Offset: 0x0000618D
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = fOnValue;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007FA4 File Offset: 0x000061A4
		private bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007FC0 File Offset: 0x000061C0
		private int ColumnCount(DataRow row, bool fAttribute)
		{
			DataColumn dataColumn = null;
			int num = 0;
			while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute)) != null)
			{
				if (dataColumn.Namespace != "http://www.w3.org/2000/xmlns/")
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007FF8 File Offset: 0x000061F8
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this._node == null || this._column != null || this._node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!this.IsFoliated(this._node))
				{
					return this.ColumnCount(this.Row, true);
				}
				int num = 0;
				using (IEnumerator enumerator = this._node.Attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((XmlAttribute)enumerator.Current).NamespaceURI != "http://www.w3.org/2000/xmlns/")
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000080AC File Offset: 0x000062AC
		internal DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataColumnCollection columns = row.Table.Columns;
			int i = ((col != null) ? (col.Ordinal + 1) : 0);
			int count = columns.Count;
			DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
			while (i < count)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, dataRowVersion]))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008140 File Offset: 0x00006340
		internal DataColumn PreviousColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataColumnCollection columns = row.Table.Columns;
			int i = ((col != null) ? (col.Ordinal - 1) : (columns.Count - 1));
			int count = columns.Count;
			DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
			while (i >= 0)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, dataRowVersion]))
				{
					return dataColumn;
				}
				i--;
			}
			return null;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000081D8 File Offset: 0x000063D8
		internal bool MoveToAttribute(string localName, string namespaceURI)
		{
			this.RealFoliate();
			if (namespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return false;
			}
			if (this._node != null && (this._column == null || this._column.ColumnMapping == MappingType.Attribute) && this._node.NodeType == XmlNodeType.Element)
			{
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = null;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.EncodedColumnName == localName && dataColumn.Namespace == namespaceURI)
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
				}
				else
				{
					XmlNode namedItem = this._node.Attributes.GetNamedItem(localName, namespaceURI);
					if (namedItem != null)
					{
						this.MoveTo(namedItem, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000082A0 File Offset: 0x000064A0
		internal bool MoveToNextAttribute(bool bFirst)
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (bFirst && (this._column != null || this._node.NodeType != XmlNodeType.Element))
				{
					return false;
				}
				if (!bFirst)
				{
					if (this._column != null && this._column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					if (this._column == null && this._node.NodeType != XmlNodeType.Attribute)
					{
						return false;
					}
				}
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = this._column;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.Namespace != "http://www.w3.org/2000/xmlns/")
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
					return false;
				}
				if (bFirst)
				{
					using (IEnumerator enumerator = this._node.Attributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							if (xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/")
							{
								this.MoveTo(xmlAttribute, null, false);
								return true;
							}
						}
						return false;
					}
				}
				XmlNamedNodeMap attributes = ((XmlAttribute)this._node).OwnerElement.Attributes;
				bool flag = false;
				foreach (object obj2 in attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
					if (flag && xmlAttribute2.NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						this.MoveTo(xmlAttribute2, null, false);
						return true;
					}
					if (xmlAttribute2 == this._node)
					{
						flag = true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000845C File Offset: 0x0000665C
		private bool IsValidChild(XmlNode parent, XmlNode child)
		{
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)child.NodeType];
			if (num == -1)
			{
				return false;
			}
			int num2 = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num2 != 0)
			{
				return num2 == 1 && (num == 1 || num == 4 || num == 8 || num == 6 || num == 5 || num == 7);
			}
			return num == 1 || num == 8 || num == 7;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000084C0 File Offset: 0x000066C0
		private bool IsValidChild(XmlNode parent, DataColumn c)
		{
			int num = XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num != 0)
			{
				return num == 1 && (c.ColumnMapping == MappingType.Element || c.ColumnMapping == MappingType.SimpleContent);
			}
			return c.ColumnMapping == MappingType.Element;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008504 File Offset: 0x00006704
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, this._column, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
					XmlNode xmlNode = this._doc.SafeFirstChild(this._node);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode2 = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode2 = this._doc.SafeNextSibling(xmlNode2);
					}
					while ((xmlNode2 != null && flag && XmlDataDocument.IsTextNode(xmlNode2.NodeType)) || (xmlNode2 != null && !this.IsValidChild(parentNode, xmlNode2)));
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000860C File Offset: 0x0000680C
		internal bool MoveToPreviousSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.PreviousColumn(row, this._column, false); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				else
				{
					XmlNode xmlNode = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode = this._doc.SafePreviousSibling(xmlNode);
					}
					while ((xmlNode != null && flag && XmlDataDocument.IsTextNode(xmlNode.NodeType)) || (xmlNode != null && !this.IsValidChild(parentNode, xmlNode)));
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
					if (!this.IsFoliated(parentNode) && parentNode is XmlBoundElement)
					{
						DataRow row2 = ((XmlBoundElement)parentNode).Row;
						if (row2 != null)
						{
							DataColumn dataColumn2 = this.PreviousColumn(row2, null, false);
							if (dataColumn2 != null)
							{
								this.MoveTo(parentNode, dataColumn2, this._doc.IsTextOnly(dataColumn2));
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008744 File Offset: 0x00006944
		internal bool MoveToFirst()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				DataRow dataRow = null;
				XmlNode xmlNode;
				if (this._column != null)
				{
					dataRow = this.Row;
					xmlNode = this._node;
				}
				else
				{
					xmlNode = this._node.ParentNode;
					if (xmlNode == null)
					{
						return false;
					}
					if (!this.IsFoliated(xmlNode) && xmlNode is XmlBoundElement)
					{
						dataRow = ((XmlBoundElement)xmlNode).Row;
					}
				}
				if (dataRow != null)
				{
					for (DataColumn dataColumn = this.NextColumn(dataRow, null, false); dataColumn != null; dataColumn = this.NextColumn(dataRow, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode2 = this._doc.SafeFirstChild(xmlNode); xmlNode2 != null; xmlNode2 = this._doc.SafeNextSibling(xmlNode2))
				{
					if (this.IsValidChild(xmlNode, xmlNode2))
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008828 File Offset: 0x00006A28
		internal bool HasChildren
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return false;
				}
				if (this._column != null)
				{
					return this._column.ColumnMapping != MappingType.Attribute && this._column.ColumnMapping != MappingType.Hidden && !this._fOnValue;
				}
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000088EC File Offset: 0x00006AEC
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this._node == null)
			{
				return false;
			}
			if (this._column == null)
			{
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				return false;
			}
			if (this._column.ColumnMapping == MappingType.Attribute || this._column.ColumnMapping == MappingType.Hidden)
			{
				return false;
			}
			if (this._fOnValue)
			{
				return false;
			}
			this._fOnValue = true;
			return true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000089D8 File Offset: 0x00006BD8
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this.NodeType == XPathNodeType.Namespace)
			{
				this.MoveTo(this._parentOfNS);
				return true;
			}
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						this.MoveTo(this._node, this._column, false);
						return true;
					}
					this.MoveTo(this._node, null, false);
					return true;
				}
				else
				{
					XmlNode xmlNode;
					if (this._node.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)this._node).OwnerElement;
					}
					else
					{
						xmlNode = this._node.ParentNode;
					}
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008A94 File Offset: 0x00006C94
		private XmlNode GetParent(XmlNode node)
		{
			XPathNodeType xpathNodeType = this.ConvertNodeType(node);
			if (xpathNodeType == XPathNodeType.Namespace)
			{
				return this._parentOfNS;
			}
			if (xpathNodeType == XPathNodeType.Attribute)
			{
				return ((XmlAttribute)node).OwnerElement;
			}
			return node.ParentNode;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008ACC File Offset: 0x00006CCC
		internal void MoveToRoot()
		{
			XmlNode xmlNode = this._node;
			for (XmlNode xmlNode2 = this._node; xmlNode2 != null; xmlNode2 = this.GetParent(xmlNode2))
			{
				xmlNode = xmlNode2;
			}
			this._node = xmlNode;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008B0C File Offset: 0x00006D0C
		internal bool IsSamePosition(XPathNodePointer pointer)
		{
			this.RealFoliate();
			pointer.RealFoliate();
			if (this._column == null && pointer._column == null)
			{
				return pointer._node == this._node && pointer._parentOfNS == this._parentOfNS;
			}
			return pointer._doc == this._doc && pointer._node == this._node && pointer._column == this._column && pointer._fOnValue == this._fOnValue && pointer._parentOfNS == this._parentOfNS;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008B9C File Offset: 0x00006D9C
		private XmlNodeOrder CompareNamespacePosition(XPathNodePointer other)
		{
			XPathNodePointer xpathNodePointer = this.Clone((DataDocumentXPathNavigator)this._owner.Target);
			other.Clone((DataDocumentXPathNavigator)other._owner.Target);
			while (xpathNodePointer.MoveToNextNamespace(XPathNamespaceScope.All))
			{
				if (xpathNodePointer.IsSamePosition(other))
				{
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.After;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008BF0 File Offset: 0x00006DF0
		private static XmlNode GetRoot(XmlNode node, ref int depth)
		{
			depth = 0;
			XmlNode xmlNode = node;
			XmlNode xmlNode2 = ((xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode);
			while (xmlNode2 != null)
			{
				xmlNode = xmlNode2;
				xmlNode2 = xmlNode.ParentNode;
				depth++;
			}
			return xmlNode;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008C34 File Offset: 0x00006E34
		internal XmlNodeOrder ComparePosition(XPathNodePointer other)
		{
			this.RealFoliate();
			other.RealFoliate();
			if (this.IsSamePosition(other))
			{
				return XmlNodeOrder.Same;
			}
			XmlNode xmlNode;
			XmlNode xmlNode2;
			if (this.NodeType == XPathNodeType.Namespace && other.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._parentOfNS)
				{
					return this.CompareNamespacePosition(other);
				}
				xmlNode = this._parentOfNS;
				xmlNode2 = other._parentOfNS;
			}
			else if (this.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._node)
				{
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					xmlNode = this._parentOfNS;
					xmlNode2 = other._node;
				}
			}
			else if (other.NodeType == XPathNodeType.Namespace)
			{
				if (this._node == other._parentOfNS)
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
				else
				{
					xmlNode = this._node;
					xmlNode2 = other._parentOfNS;
				}
			}
			else if (this._node == other._node)
			{
				if (this._column == other._column)
				{
					if (this._fOnValue)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					if (this._column.Ordinal < other._column.Ordinal)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
			}
			else
			{
				xmlNode = this._node;
				xmlNode2 = other._node;
			}
			if (xmlNode == null || xmlNode2 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			int num = -1;
			int num2 = -1;
			XmlNode root = XPathNodePointer.GetRoot(xmlNode, ref num);
			XmlNode root2 = XPathNodePointer.GetRoot(xmlNode2, ref num2);
			if (root != root2)
			{
				return XmlNodeOrder.Unknown;
			}
			if (num > num2)
			{
				while (xmlNode != null && num > num2)
				{
					xmlNode = ((xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode);
					num--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.After;
				}
			}
			else if (num2 > num)
			{
				while (xmlNode2 != null && num2 > num)
				{
					xmlNode2 = ((xmlNode2.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode2).OwnerElement : xmlNode2.ParentNode);
					num2--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.Before;
				}
			}
			XmlNode xmlNode3 = this.GetParent(xmlNode);
			XmlNode xmlNode4 = this.GetParent(xmlNode2);
			while (xmlNode3 != null && xmlNode4 != null)
			{
				if (xmlNode3 == xmlNode4)
				{
					while (xmlNode != null)
					{
						XmlNode nextSibling = xmlNode.NextSibling;
						if (nextSibling == xmlNode2)
						{
							return XmlNodeOrder.Before;
						}
						xmlNode = nextSibling;
					}
					return XmlNodeOrder.After;
				}
				xmlNode = xmlNode3;
				xmlNode2 = xmlNode4;
				xmlNode3 = xmlNode.ParentNode;
				xmlNode4 = xmlNode2.ParentNode;
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008E48 File Offset: 0x00007048
		internal XmlNode Node
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement != null)
				{
					bool isFoliationEnabled = this._doc.IsFoliationEnabled;
					this._doc.IsFoliationEnabled = true;
					this._doc.Foliate(rowElement, ElementState.StrongFoliation);
					this._doc.IsFoliationEnabled = isFoliationEnabled;
				}
				this.RealFoliate();
				return this._node;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008EAC File Offset: 0x000070AC
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008EBD File Offset: 0x000070BD
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008ECE File Offset: 0x000070CE
		void IXmlDataVirtualNode.OnFoliated(XmlNode foliatedNode)
		{
			if (this._node == foliatedNode)
			{
				if (this._column == null)
				{
					return;
				}
				this._bNeedFoliate = true;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008EEC File Offset: 0x000070EC
		private void RealFoliate()
		{
			if (!this._bNeedFoliate)
			{
				return;
			}
			this._bNeedFoliate = false;
			XmlNode xmlNode;
			if (this._doc.IsTextOnly(this._column))
			{
				xmlNode = this._node.FirstChild;
			}
			else
			{
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					xmlNode = this._node.Attributes.GetNamedItem(this._column.EncodedColumnName, this._column.Namespace);
				}
				else
				{
					xmlNode = this._node.FirstChild;
					while (xmlNode != null && (!(xmlNode.LocalName == this._column.EncodedColumnName) || !(xmlNode.NamespaceURI == this._column.Namespace)))
					{
						xmlNode = xmlNode.NextSibling;
					}
				}
				if (xmlNode != null && this._fOnValue)
				{
					xmlNode = xmlNode.FirstChild;
				}
			}
			if (xmlNode == null)
			{
				throw new InvalidOperationException("Invalid foliation.");
			}
			this._node = xmlNode;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008FEC File Offset: 0x000071EC
		private string GetNamespace(XmlBoundElement be, string name)
		{
			if (be == null)
			{
				return null;
			}
			if (be.IsFoliated)
			{
				XmlAttribute attributeNode = be.GetAttributeNode(name, "http://www.w3.org/2000/xmlns/");
				if (attributeNode != null)
				{
					return attributeNode.Value;
				}
				return null;
			}
			else
			{
				DataRow row = be.Row;
				if (row == null)
				{
					return null;
				}
				for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
						return dataColumn.ConvertObjectToXml(row[dataColumn, dataRowVersion]);
					}
				}
				return null;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009080 File Offset: 0x00007280
		internal string GetNamespace(string name)
		{
			if (name == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			if (name == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			if (name != null && name.Length == 0)
			{
				name = "xmlns";
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType xmlNodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				while (xmlNode != null && (xmlNodeType = xmlNode.NodeType) != XmlNodeType.Element)
				{
					if (xmlNodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (xmlNode != null)
				{
					string @namespace = this.GetNamespace((XmlBoundElement)xmlNode, name);
					if (@namespace != null)
					{
						return @namespace;
					}
					xmlNode = xmlNode.ParentNode;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000912C File Offset: 0x0000732C
		internal bool MoveToNamespace(string name)
		{
			this._parentOfNS = this._node as XmlBoundElement;
			if (this._parentOfNS == null)
			{
				return false;
			}
			string text = name;
			if (text == "xmlns")
			{
				text = "xmlns:xmlns";
			}
			if (text == null || text.Length == 0)
			{
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType nodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					if (xmlBoundElement.IsFoliated)
					{
						XmlAttribute attributeNode = xmlBoundElement.GetAttributeNode(name, "http://www.w3.org/2000/xmlns/");
						if (attributeNode != null)
						{
							this.MoveTo(attributeNode);
							return true;
						}
					}
					else
					{
						DataRow row = xmlBoundElement.Row;
						if (row == null)
						{
							return false;
						}
						for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
						{
							if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/" && dataColumn.ColumnName == name)
							{
								this.MoveTo(xmlBoundElement, dataColumn, false);
								return true;
							}
						}
					}
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			this._parentOfNS = null;
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009240 File Offset: 0x00007440
		private bool MoveToNextNamespace(XmlBoundElement be, DataColumn col, XmlAttribute curAttr)
		{
			if (be != null)
			{
				if (be.IsFoliated)
				{
					XmlAttributeCollection attributes = be.Attributes;
					bool flag = false;
					if (curAttr == null)
					{
						flag = true;
					}
					int i = attributes.Count;
					while (i > 0)
					{
						i--;
						XmlAttribute xmlAttribute = attributes[i];
						if (flag && xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/" && !this.DuplicateNS(be, xmlAttribute.LocalName))
						{
							this.MoveTo(xmlAttribute);
							return true;
						}
						if (xmlAttribute == curAttr)
						{
							flag = true;
						}
					}
				}
				else
				{
					DataRow row = be.Row;
					if (row == null)
					{
						return false;
					}
					for (DataColumn dataColumn = this.PreviousColumn(row, col, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
					{
						if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/" && !this.DuplicateNS(be, dataColumn.ColumnName))
						{
							this.MoveTo(be, dataColumn, false);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009318 File Offset: 0x00007518
		internal bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			this._parentOfNS = this._node as XmlBoundElement;
			if (this._parentOfNS == null)
			{
				return false;
			}
			XmlNode xmlNode = this._node;
			while (xmlNode != null)
			{
				XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(xmlBoundElement, null, null))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					IL_0072:
					this._parentOfNS = null;
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc._attrXml, null, false);
				return true;
			}
			goto IL_0072;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000093A0 File Offset: 0x000075A0
		private bool DuplicateNS(XmlBoundElement endElem, string lname)
		{
			if (this._parentOfNS == null || endElem == null)
			{
				return false;
			}
			XmlBoundElement xmlBoundElement = this._parentOfNS;
			while (xmlBoundElement != null && xmlBoundElement != endElem)
			{
				if (this.GetNamespace(xmlBoundElement, lname) != null)
				{
					return true;
				}
				XmlNode xmlNode = xmlBoundElement;
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
				xmlBoundElement = xmlNode as XmlBoundElement;
			}
			return false;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000093F8 File Offset: 0x000075F8
		internal bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			if (this._column != null)
			{
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != this._node)
				{
					return false;
				}
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				for (DataColumn dataColumn = this.PreviousColumn(row, this._column, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == "http://www.w3.org/2000/xmlns/")
					{
						this.MoveTo(xmlBoundElement, dataColumn, false);
						return true;
					}
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			else if (this._node.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this._node;
				xmlNode = xmlAttribute.OwnerElement;
				if (xmlNode == null)
				{
					return false;
				}
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != xmlNode)
				{
					return false;
				}
				if (this.MoveToNextNamespace((XmlBoundElement)xmlNode, null, xmlAttribute))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			while (xmlNode != null)
			{
				XmlBoundElement xmlBoundElement2 = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(xmlBoundElement2, null, null))
				{
					return true;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc._attrXml, null, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000954D File Offset: 0x0000774D
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				DataRowState rowState = (this._node as XmlBoundElement).Row.RowState;
			}
			DataColumn column = this._column;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009576 File Offset: 0x00007776
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000957E File Offset: 0x0000777E
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._owner.IsAlive;
		}

		// Token: 0x0400041D RID: 1053
		private readonly WeakReference _owner;

		// Token: 0x0400041E RID: 1054
		private readonly XmlDataDocument _doc;

		// Token: 0x0400041F RID: 1055
		private XmlNode _node;

		// Token: 0x04000420 RID: 1056
		private DataColumn _column;

		// Token: 0x04000421 RID: 1057
		private bool _fOnValue;

		// Token: 0x04000422 RID: 1058
		internal XmlBoundElement _parentOfNS;

		// Token: 0x04000423 RID: 1059
		internal static readonly int[] s_xmlNodeType_To_XpathNodeType_Map = new int[20];

		// Token: 0x04000424 RID: 1060
		internal const string StrReservedXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000425 RID: 1061
		internal const string StrReservedXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000426 RID: 1062
		internal const string StrXmlNS = "xmlns";

		// Token: 0x04000427 RID: 1063
		private bool _bNeedFoliate;
	}
}
