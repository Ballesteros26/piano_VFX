using System;
using System.Collections;
using System.Data;

namespace System.Xml
{
	// Token: 0x0200002D RID: 45
	internal sealed class DataSetMapper
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006EC7 File Offset: 0x000050C7
		internal DataSetMapper()
		{
			this._tableSchemaMap = new Hashtable();
			this._columnSchemaMap = new Hashtable();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006EE8 File Offset: 0x000050E8
		internal void SetupMapping(XmlDataDocument xd, DataSet ds)
		{
			if (this.IsMapped())
			{
				this._tableSchemaMap = new Hashtable();
				this._columnSchemaMap = new Hashtable();
			}
			this._doc = xd;
			this._dataSet = ds;
			foreach (object obj in this._dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				this.AddTableSchema(dataTable);
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (!DataSetMapper.IsNotMapped(dataColumn))
					{
						this.AddColumnSchema(dataColumn);
					}
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006FC8 File Offset: 0x000051C8
		internal bool IsMapped()
		{
			return this._dataSet != null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006FD4 File Offset: 0x000051D4
		internal DataTable SearchMatchingTableSchema(string localName, string namespaceURI)
		{
			object identity = DataSetMapper.GetIdentity(localName, namespaceURI);
			return (DataTable)this._tableSchemaMap[identity];
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006FFC File Offset: 0x000051FC
		internal DataTable SearchMatchingTableSchema(XmlBoundElement rowElem, XmlBoundElement elem)
		{
			DataTable dataTable = this.SearchMatchingTableSchema(elem.LocalName, elem.NamespaceURI);
			if (dataTable == null)
			{
				return null;
			}
			if (rowElem == null)
			{
				return dataTable;
			}
			if (this.GetColumnSchemaForNode(rowElem, elem) == null)
			{
				return dataTable;
			}
			using (IEnumerator enumerator = elem.Attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((XmlAttribute)enumerator.Current).NamespaceURI != "http://www.w3.org/2000/xmlns/")
					{
						return dataTable;
					}
				}
			}
			for (XmlNode xmlNode = elem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000070AC File Offset: 0x000052AC
		internal DataColumn GetColumnSchemaForNode(XmlBoundElement rowElem, XmlNode node)
		{
			object identity = DataSetMapper.GetIdentity(rowElem.LocalName, rowElem.NamespaceURI);
			object identity2 = DataSetMapper.GetIdentity(node.LocalName, node.NamespaceURI);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				return null;
			}
			DataColumn dataColumn = (DataColumn)hashtable[identity2];
			if (dataColumn == null)
			{
				return null;
			}
			MappingType columnMapping = dataColumn.ColumnMapping;
			if (node.NodeType == XmlNodeType.Attribute && columnMapping == MappingType.Attribute)
			{
				return dataColumn;
			}
			if (node.NodeType == XmlNodeType.Element && columnMapping == MappingType.Element)
			{
				return dataColumn;
			}
			return null;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007130 File Offset: 0x00005330
		internal DataTable GetTableSchemaForElement(XmlElement elem)
		{
			XmlBoundElement xmlBoundElement = elem as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return this.GetTableSchemaForElement(xmlBoundElement);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007150 File Offset: 0x00005350
		internal DataTable GetTableSchemaForElement(XmlBoundElement be)
		{
			DataRow row = be.Row;
			if (row == null)
			{
				return null;
			}
			return row.Table;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007163 File Offset: 0x00005363
		internal static bool IsNotMapped(DataColumn c)
		{
			return c.ColumnMapping == MappingType.Hidden;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000716E File Offset: 0x0000536E
		internal DataRow GetRowFromElement(XmlElement e)
		{
			XmlBoundElement xmlBoundElement = e as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return xmlBoundElement.Row;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00007181 File Offset: 0x00005381
		internal DataRow GetRowFromElement(XmlBoundElement be)
		{
			return be.Row;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000718C File Offset: 0x0000538C
		internal bool GetRegion(XmlNode node, out XmlBoundElement rowElem)
		{
			while (node != null)
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null && this.GetRowFromElement(xmlBoundElement) != null)
				{
					rowElem = xmlBoundElement;
					return true;
				}
				if (node.NodeType == XmlNodeType.Attribute)
				{
					node = ((XmlAttribute)node).OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			rowElem = null;
			return false;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000071DC File Offset: 0x000053DC
		internal bool IsRegionRadical(XmlBoundElement rowElem)
		{
			if (rowElem.ElementState == ElementState.Defoliated)
			{
				return true;
			}
			DataColumnCollection columns = this.GetTableSchemaForElement(rowElem).Columns;
			int num = 0;
			int count = rowElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				XmlAttribute xmlAttribute = rowElem.Attributes[i];
				if (!xmlAttribute.Specified)
				{
					return false;
				}
				DataColumn columnSchemaForNode = this.GetColumnSchemaForNode(rowElem, xmlAttribute);
				if (columnSchemaForNode == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode))
				{
					return false;
				}
				XmlNode firstChild = xmlAttribute.FirstChild;
				if (firstChild == null || firstChild.NodeType != XmlNodeType.Text || firstChild.NextSibling != null)
				{
					return false;
				}
			}
			num = 0;
			for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				XmlElement xmlElement = xmlNode as XmlElement;
				if (this.GetRowFromElement(xmlElement) != null)
				{
					IL_0135:
					while (xmlNode != null)
					{
						if (xmlNode.NodeType != XmlNodeType.Element)
						{
							return false;
						}
						if (this.GetRowFromElement((XmlElement)xmlNode) == null)
						{
							return false;
						}
						xmlNode = xmlNode.NextSibling;
					}
					return true;
				}
				DataColumn columnSchemaForNode2 = this.GetColumnSchemaForNode(rowElem, xmlElement);
				if (columnSchemaForNode2 == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode2))
				{
					return false;
				}
				if (xmlElement.HasAttributes)
				{
					return false;
				}
				XmlNode firstChild2 = xmlElement.FirstChild;
				if (firstChild2 == null || firstChild2.NodeType != XmlNodeType.Text || firstChild2.NextSibling != null)
				{
					return false;
				}
			}
			goto IL_0135;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007324 File Offset: 0x00005524
		private void AddTableSchema(DataTable table)
		{
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			this._tableSchemaMap[identity] = table;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007350 File Offset: 0x00005550
		private void AddColumnSchema(DataColumn col)
		{
			DataTable table = col.Table;
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			object identity2 = DataSetMapper.GetIdentity(col.EncodedColumnName, col.Namespace);
			Hashtable hashtable = (Hashtable)this._columnSchemaMap[identity];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this._columnSchemaMap[identity] = hashtable;
			}
			hashtable[identity2] = col;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000073B8 File Offset: 0x000055B8
		private static object GetIdentity(string localName, string namespaceURI)
		{
			return localName + ":" + namespaceURI;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000073C6 File Offset: 0x000055C6
		private bool IsNextColumn(DataColumnCollection columns, ref int iColumn, DataColumn col)
		{
			while (iColumn < columns.Count)
			{
				if (columns[iColumn] == col)
				{
					iColumn++;
					return true;
				}
				iColumn++;
			}
			return false;
		}

		// Token: 0x04000414 RID: 1044
		private Hashtable _tableSchemaMap;

		// Token: 0x04000415 RID: 1045
		private Hashtable _columnSchemaMap;

		// Token: 0x04000416 RID: 1046
		private XmlDataDocument _doc;

		// Token: 0x04000417 RID: 1047
		private DataSet _dataSet;

		// Token: 0x04000418 RID: 1048
		internal const string strReservedXmlns = "http://www.w3.org/2000/xmlns/";
	}
}
