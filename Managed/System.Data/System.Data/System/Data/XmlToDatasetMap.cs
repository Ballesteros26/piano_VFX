using System;
using System.Collections;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200010A RID: 266
	internal sealed class XmlToDatasetMap
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x00044A24 File Offset: 0x00042C24
		public XmlToDatasetMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataSet, nameTable);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00044A34 File Offset: 0x00042C34
		public XmlToDatasetMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.BuildIdentityMap(nameTable, dataSet);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00044A44 File Offset: 0x00042C44
		public XmlToDatasetMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataTable, nameTable);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00044A54 File Offset: 0x00042C54
		public XmlToDatasetMap(XmlNameTable nameTable, DataTable dataTable)
		{
			this.BuildIdentityMap(nameTable, dataTable);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00044A64 File Offset: 0x00042C64
		internal static bool IsMappedColumn(DataColumn c)
		{
			return c.ColumnMapping != MappingType.Hidden;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00044A74 File Offset: 0x00042C74
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(DataTable table, XmlNameTable nameTable)
		{
			string text = nameTable.Get(table.EncodedTableName);
			string text2 = nameTable.Get(table.Namespace);
			if (text == null)
			{
				return null;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00044ABC File Offset: 0x00042CBC
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(XmlNameTable nameTable, DataTable table)
		{
			string encodedTableName = table.EncodedTableName;
			string text = nameTable.Get(encodedTableName);
			if (text == null)
			{
				text = nameTable.Add(encodedTableName);
			}
			table._encodedTableName = text;
			string text2 = nameTable.Get(table.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(table.Namespace);
			}
			else if (table._tableNamespace != null)
			{
				table._tableNamespace = text2;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this._tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00044B34 File Offset: 0x00042D34
		private bool AddColumnSchema(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = nameTable.Get(col.EncodedColumnName);
			string text2 = nameTable.Get(col.Namespace);
			if (text == null)
			{
				return false;
			}
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[xmlNodeIdentety] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00044B90 File Offset: 0x00042D90
		private bool AddColumnSchema(XmlNameTable nameTable, DataColumn col, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = XmlConvert.EncodeLocalName(col.ColumnName);
			string text2 = nameTable.Get(text);
			if (text2 == null)
			{
				text2 = nameTable.Add(text);
			}
			col._encodedColumnName = text2;
			string text3 = nameTable.Get(col.Namespace);
			if (text3 == null)
			{
				text3 = nameTable.Add(col.Namespace);
			}
			else if (col._columnUri != null)
			{
				col._columnUri = text3;
			}
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
			columns[xmlNodeIdentety] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00044C20 File Offset: 0x00042E20
		private void BuildIdentityMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
				}
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00044CF8 File Offset: 0x00042EF8
		private void BuildIdentityMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			string text = nameTable.Get(dataSet.Namespace);
			if (text == null)
			{
				text = nameTable.Add(dataSet.Namespace);
			}
			dataSet._namespaceURI = text;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string text2 = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text3 = nameTable.Get(text2);
							if (text3 == null)
							{
								text3 = nameTable.Add(text2);
							}
							string text4 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text4 == null)
							{
								text4 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text3, text4);
							tableSchemaInfo.ColumnsSchemaMap[xmlNodeIdentety] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00044EF0 File Offset: 0x000430F0
		private void BuildIdentityMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(1);
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
			if (tableSchemaInfo != null)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (XmlToDatasetMap.IsMappedColumn(dataColumn))
					{
						this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
					}
				}
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00044F74 File Offset: 0x00043174
		private void BuildIdentityMap(XmlNameTable nameTable, DataTable dataTable)
		{
			ArrayList selfAndDescendants = this.GetSelfAndDescendants(dataTable);
			this._tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(selfAndDescendants.Count);
			foreach (object obj in selfAndDescendants)
			{
				DataTable dataTable2 = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable2);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable2.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string text = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text2 = nameTable.Get(text);
							if (text2 == null)
							{
								text2 = nameTable.Add(text);
							}
							string text3 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text3 == null)
							{
								text3 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
							tableSchemaInfo.ColumnsSchemaMap[xmlNodeIdentety] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00045148 File Offset: 0x00043348
		private ArrayList GetSelfAndDescendants(DataTable dt)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(dt);
			for (int i = 0; i < arrayList.Count; i++)
			{
				foreach (object obj in ((DataTable)arrayList[i]).ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (!arrayList.Contains(dataRelation.ChildTable))
					{
						arrayList.Add(dataRelation.ChildTable);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000451E4 File Offset: 0x000433E4
		public object GetColumnSchema(XmlNode node, bool fIgnoreNamespace)
		{
			XmlNode xmlNode = ((node.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)node).OwnerElement : node.ParentNode);
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
			{
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[xmlNode.LocalName] : this._tableSchemaMap[xmlNode]);
				xmlNode = xmlNode.ParentNode;
				if (tableSchemaInfo != null)
				{
					if (fIgnoreNamespace)
					{
						return tableSchemaInfo.ColumnsSchemaMap[node.LocalName];
					}
					return tableSchemaInfo.ColumnsSchemaMap[node];
				}
			}
			return null;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00045274 File Offset: 0x00043474
		public object GetColumnSchema(DataTable table, XmlReader dataReader, bool fIgnoreNamespace)
		{
			if (this._lastTableSchemaInfo == null || this._lastTableSchemaInfo.TableSchema != table)
			{
				this._lastTableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[table.EncodedTableName] : this._tableSchemaMap[table]);
			}
			if (fIgnoreNamespace)
			{
				return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader.LocalName];
			}
			return this._lastTableSchemaInfo.ColumnsSchemaMap[dataReader];
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000452F0 File Offset: 0x000434F0
		public object GetSchemaForNode(XmlNode node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = null;
			if (node.NodeType == XmlNodeType.Element)
			{
				tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			}
			if (tableSchemaInfo != null)
			{
				return tableSchemaInfo.TableSchema;
			}
			return this.GetColumnSchema(node, fIgnoreNamespace);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00045344 File Offset: 0x00043544
		public DataTable GetTableForNode(XmlReader node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this._tableSchemaMap[node.LocalName] : this._tableSchemaMap[node]);
			if (tableSchemaInfo != null)
			{
				this._lastTableSchemaInfo = tableSchemaInfo;
				return this._lastTableSchemaInfo.TableSchema;
			}
			return null;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00045390 File Offset: 0x00043590
		private void HandleSpecialColumn(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text;
			if ('x' == col.ColumnName[0])
			{
				text = "_x0078_";
			}
			else
			{
				text = "_x0058_";
			}
			text += col.ColumnName.Substring(1);
			if (nameTable.Get(text) == null)
			{
				nameTable.Add(text);
			}
			string text2 = nameTable.Get(col.Namespace);
			XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[xmlNodeIdentety] = col;
		}

		// Token: 0x040009B5 RID: 2485
		private XmlToDatasetMap.XmlNodeIdHashtable _tableSchemaMap;

		// Token: 0x040009B6 RID: 2486
		private XmlToDatasetMap.TableSchemaInfo _lastTableSchemaInfo;

		// Token: 0x0200010B RID: 267
		private sealed class XmlNodeIdentety
		{
			// Token: 0x06000DAF RID: 3503 RVA: 0x000453FC File Offset: 0x000435FC
			public XmlNodeIdentety(string localName, string namespaceURI)
			{
				this.LocalName = localName;
				this.NamespaceURI = namespaceURI;
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00045412 File Offset: 0x00043612
			public override int GetHashCode()
			{
				return this.LocalName.GetHashCode();
			}

			// Token: 0x06000DB1 RID: 3505 RVA: 0x00045420 File Offset: 0x00043620
			public override bool Equals(object obj)
			{
				XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = (XmlToDatasetMap.XmlNodeIdentety)obj;
				return string.Equals(this.LocalName, xmlNodeIdentety.LocalName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.NamespaceURI, xmlNodeIdentety.NamespaceURI, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x040009B7 RID: 2487
			public string LocalName;

			// Token: 0x040009B8 RID: 2488
			public string NamespaceURI;
		}

		// Token: 0x0200010C RID: 268
		internal sealed class XmlNodeIdHashtable : Hashtable
		{
			// Token: 0x06000DB2 RID: 3506 RVA: 0x0004545C File Offset: 0x0004365C
			public XmlNodeIdHashtable(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x17000251 RID: 593
			public object this[XmlNode node]
			{
				get
				{
					this._id.LocalName = node.LocalName;
					this._id.NamespaceURI = node.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x17000252 RID: 594
			public object this[XmlReader dataReader]
			{
				get
				{
					this._id.LocalName = dataReader.LocalName;
					this._id.NamespaceURI = dataReader.NamespaceURI;
					return this[this._id];
				}
			}

			// Token: 0x17000253 RID: 595
			public object this[DataTable table]
			{
				get
				{
					this._id.LocalName = table.EncodedTableName;
					this._id.NamespaceURI = table.Namespace;
					return this[this._id];
				}
			}

			// Token: 0x17000254 RID: 596
			public object this[string name]
			{
				get
				{
					this._id.LocalName = name;
					this._id.NamespaceURI = string.Empty;
					return this[this._id];
				}
			}

			// Token: 0x040009B9 RID: 2489
			private XmlToDatasetMap.XmlNodeIdentety _id = new XmlToDatasetMap.XmlNodeIdentety(string.Empty, string.Empty);
		}

		// Token: 0x0200010D RID: 269
		private sealed class TableSchemaInfo
		{
			// Token: 0x06000DB7 RID: 3511 RVA: 0x00045534 File Offset: 0x00043734
			public TableSchemaInfo(DataTable tableSchema)
			{
				this.TableSchema = tableSchema;
				this.ColumnsSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(tableSchema.Columns.Count);
			}

			// Token: 0x040009BA RID: 2490
			public DataTable TableSchema;

			// Token: 0x040009BB RID: 2491
			public XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;
		}
	}
}
