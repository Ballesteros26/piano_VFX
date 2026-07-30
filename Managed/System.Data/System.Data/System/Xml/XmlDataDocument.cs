using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Allows structured data to be stored, retrieved, and manipulated through a relational <see cref="T:System.Data.DataSet" />. </summary>
	// Token: 0x02000035 RID: 53
	[Obsolete("XmlDataDocument class will be removed in a future release.")]
	public class XmlDataDocument : XmlDocument
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00009AC4 File Offset: 0x00007CC4
		internal void AddPointer(IXmlDataVirtualNode pointer)
		{
			Hashtable pointers = this._pointers;
			lock (pointers)
			{
				this._countAddPointer++;
				if (this._countAddPointer >= 5)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in this._pointers)
					{
						IXmlDataVirtualNode xmlDataVirtualNode = (IXmlDataVirtualNode)((DictionaryEntry)obj).Value;
						if (!xmlDataVirtualNode.IsInUse())
						{
							arrayList.Add(xmlDataVirtualNode);
						}
					}
					for (int i = 0; i < arrayList.Count; i++)
					{
						this._pointers.Remove(arrayList[i]);
					}
					this._countAddPointer = 0;
				}
				this._pointers[pointer] = pointer;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void AssertPointerPresent(IXmlDataVirtualNode pointer)
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private void AttachDataSet(DataSet ds)
		{
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException("DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.");
			}
			ds.FBoundToDocument = true;
			this._dataSet = ds;
			this.BindSpecialListeners();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009BEC File Offset: 0x00007DEC
		internal void SyncRows(DataRow parentRow, XmlNode node, bool fAddRowsToTable)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					xmlBoundElement.ElementState = ElementState.WeakFoliation;
					this.DefoliateRegion(xmlBoundElement);
					if (parentRow != null)
					{
						XmlDataDocument.SetNestedParentRow(row, parentRow);
					}
					if (fAddRowsToTable && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					parentRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(parentRow, xmlNode, fAddRowsToTable);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009C78 File Offset: 0x00007E78
		internal void SyncTree(XmlNode node)
		{
			XmlBoundElement xmlBoundElement = null;
			this._mapper.GetRegion(node, out xmlBoundElement);
			DataRow dataRow = null;
			bool flag = this.IsConnected(node);
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					if (node == xmlBoundElement)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
						this.DefoliateRegion(xmlBoundElement);
					}
					if (flag && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					dataRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(dataRow, xmlNode, flag);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009D13 File Offset: 0x00007F13
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009D1B File Offset: 0x00007F1B
		internal ElementState AutoFoliationState
		{
			get
			{
				return this._autoFoliationState;
			}
			set
			{
				this._autoFoliationState = value;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009D24 File Offset: 0x00007F24
		private void BindForLoad()
		{
			this._ignoreDataSetEvents = true;
			this._mapper.SetupMapping(this, this._dataSet);
			if (this._dataSet.Tables.Count > 0)
			{
				this.LoadDataSetFromTree();
			}
			this.BindListeners();
			this._ignoreDataSetEvents = false;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009D70 File Offset: 0x00007F70
		private void Bind(bool fLoadFromDataSet)
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this._mapper.SetupMapping(this, this._dataSet);
			if (base.DocumentElement != null)
			{
				this.LoadDataSetFromTree();
				this.BindListeners();
			}
			else if (fLoadFromDataSet)
			{
				this._bLoadFromDataSet = true;
				this.LoadTreeFromDataSet(this.DataSet);
				this.BindListeners();
			}
			this._ignoreDataSetEvents = false;
			this._ignoreXmlEvents = false;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009DDD File Offset: 0x00007FDD
		internal void Bind(DataRow r, XmlBoundElement e)
		{
			r.Element = e;
			e.Row = r;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009DED File Offset: 0x00007FED
		private void BindSpecialListeners()
		{
			this._dataSet.DataRowCreated += this.OnDataRowCreatedSpecial;
			this._fDataRowCreatedSpecial = true;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009E0D File Offset: 0x0000800D
		private void UnBindSpecialListeners()
		{
			this._dataSet.DataRowCreated -= this.OnDataRowCreatedSpecial;
			this._fDataRowCreatedSpecial = false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009E2D File Offset: 0x0000802D
		private void BindListeners()
		{
			this.BindToDocument();
			this.BindToDataSet();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009E3C File Offset: 0x0000803C
		private void BindToDataSet()
		{
			if (this._fBoundToDataSet)
			{
				return;
			}
			if (this._fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
			}
			this._dataSet.Tables.CollectionChanging += this.OnDataSetTablesChanging;
			this._dataSet.Relations.CollectionChanging += this.OnDataSetRelationsChanging;
			this._dataSet.DataRowCreated += this.OnDataRowCreated;
			this._dataSet.PropertyChanging += this.OnDataSetPropertyChanging;
			this._dataSet.ClearFunctionCalled += this.OnClearCalled;
			if (this._dataSet.Tables.Count > 0)
			{
				foreach (object obj in this._dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					this.BindToTable(dataTable);
				}
			}
			foreach (object obj2 in this._dataSet.Relations)
			{
				((DataRelation)obj2).PropertyChanging += this.OnRelationPropertyChanging;
			}
			this._fBoundToDataSet = true;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009FA0 File Offset: 0x000081A0
		private void BindToDocument()
		{
			if (!this._fBoundToDocument)
			{
				base.NodeInserting += this.OnNodeInserting;
				base.NodeInserted += this.OnNodeInserted;
				base.NodeRemoving += this.OnNodeRemoving;
				base.NodeRemoved += this.OnNodeRemoved;
				base.NodeChanging += this.OnNodeChanging;
				base.NodeChanged += this.OnNodeChanged;
				this._fBoundToDocument = true;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A028 File Offset: 0x00008228
		private void BindToTable(DataTable t)
		{
			t.ColumnChanged += this.OnColumnChanged;
			t.RowChanging += this.OnRowChanging;
			t.RowChanged += this.OnRowChanged;
			t.RowDeleting += this.OnRowChanging;
			t.RowDeleted += this.OnRowChanged;
			t.PropertyChanging += this.OnTablePropertyChanging;
			t.Columns.CollectionChanging += this.OnTableColumnsChanging;
			foreach (object obj in t.Columns)
			{
				((DataColumn)obj).PropertyChanging += this.OnColumnPropertyChanging;
			}
		}

		/// <summary>Creates an element with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" /> , and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <returns>A new <see cref="T:System.Xml.XmlElement" />.</returns>
		/// <param name="prefix">The prefix of the new element. If String.Empty or null, there is no prefix. </param>
		/// <param name="localName">The local name of the new element. </param>
		/// <param name="namespaceURI">The namespace Uniform Resource Identifier (URI) of the new element. If String.Empty or null, there is no namespaceURI. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001AA RID: 426 RVA: 0x0000A110 File Offset: 0x00008310
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (!this._fAssociateDataRow)
			{
				return new XmlBoundElement(prefix, localName, namespaceURI, this);
			}
			this.EnsurePopulatedMode();
			DataTable dataTable = this._mapper.SearchMatchingTableSchema(localName, namespaceURI);
			if (dataTable != null)
			{
				DataRow dataRow = dataTable.CreateEmptyRow();
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						XmlDataDocument.SetRowValueToNull(dataRow, dataColumn);
					}
				}
				XmlBoundElement element = dataRow.Element;
				element.Prefix = prefix;
				return element;
			}
			return new XmlBoundElement(prefix, localName, namespaceURI, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</returns>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x060001AB RID: 427 RVA: 0x0000A1D0 File Offset: 0x000083D0
		public override XmlEntityReference CreateEntityReference(string name)
		{
			throw new NotSupportedException("Cannot create entity references on DataDocument.");
		}

		/// <summary>Gets a <see cref="T:System.Data.DataSet" /> that provides a relational representation of the data in the XmlDataDocument.</summary>
		/// <returns>A DataSet that can be used to access the data in the XmlDataDocument using a relational model.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A1DC File Offset: 0x000083DC
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A1E4 File Offset: 0x000083E4
		private void DefoliateRegion(XmlBoundElement rowElem)
		{
			if (!this._optimizeStorage)
			{
				return;
			}
			if (rowElem.ElementState != ElementState.WeakFoliation)
			{
				return;
			}
			if (!this._mapper.IsRegionRadical(rowElem))
			{
				return;
			}
			bool ignoreXmlEvents = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			rowElem.ElementState = ElementState.Defoliating;
			try
			{
				rowElem.RemoveAllAttributes();
				XmlNode nextSibling;
				for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						break;
					}
					rowElem.RemoveChild(xmlNode);
				}
				rowElem.ElementState = ElementState.Defoliated;
			}
			finally
			{
				this.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A280 File Offset: 0x00008480
		private XmlElement EnsureDocumentElement()
		{
			XmlElement xmlElement = base.DocumentElement;
			if (xmlElement == null)
			{
				string text = XmlConvert.EncodeLocalName(this.DataSet.DataSetName);
				if (text == null || text.Length == 0)
				{
					text = "Xml";
				}
				string text2 = this.DataSet.Namespace;
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				xmlElement = new XmlBoundElement(string.Empty, text, text2, this);
				this.AppendChild(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A2E8 File Offset: 0x000084E8
		private XmlElement EnsureNonRowDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			if (documentElement == null)
			{
				return this.EnsureDocumentElement();
			}
			if (this.GetRowFromElement(documentElement) == null)
			{
				return documentElement;
			}
			return this.DemoteDocumentElement();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A318 File Offset: 0x00008518
		private XmlElement DemoteDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			this.RemoveChild(documentElement);
			XmlElement xmlElement = this.EnsureDocumentElement();
			xmlElement.AppendChild(documentElement);
			return xmlElement;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A342 File Offset: 0x00008542
		private void EnsurePopulatedMode()
		{
			if (this._fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
				this._mapper.SetupMapping(this, this._dataSet);
				this.BindListeners();
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A374 File Offset: 0x00008574
		private void FixNestedChildren(DataRow row, XmlElement rowElement)
		{
			foreach (object obj in this.GetNestedChildRelations(row))
			{
				DataRelation dataRelation = (DataRelation)obj;
				DataRow[] childRows = row.GetChildRows(dataRelation);
				for (int i = 0; i < childRows.Length; i++)
				{
					XmlElement element = childRows[i].Element;
					if (element != null && element.ParentNode != rowElement)
					{
						element.ParentNode.RemoveChild(element);
						rowElement.AppendChild(element);
					}
				}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A410 File Offset: 0x00008610
		internal void Foliate(XmlBoundElement node, ElementState newState)
		{
			if (this.IsFoliationEnabled)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					this.ForceFoliation(node, newState);
					return;
				}
				if (node.ElementState == ElementState.WeakFoliation && newState == ElementState.StrongFoliation)
				{
					node.ElementState = newState;
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A440 File Offset: 0x00008640
		private void Foliate(XmlElement element)
		{
			if (element is XmlBoundElement)
			{
				((XmlBoundElement)element).Foliate(ElementState.WeakFoliation);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A458 File Offset: 0x00008658
		private void FoliateIfDataPointers(DataRow row, XmlElement rowElement)
		{
			if (!this.IsFoliated(rowElement) && this.HasPointers(rowElement))
			{
				bool isFoliationEnabled = this.IsFoliationEnabled;
				this.IsFoliationEnabled = true;
				try
				{
					this.Foliate(rowElement);
				}
				finally
				{
					this.IsFoliationEnabled = isFoliationEnabled;
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private void EnsureFoliation(XmlBoundElement rowElem, ElementState foliation)
		{
			if (rowElem.IsFoliated)
			{
				return;
			}
			this.ForceFoliation(rowElem, foliation);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A4BC File Offset: 0x000086BC
		private void ForceFoliation(XmlBoundElement node, ElementState newState)
		{
			object foliationLock = this._foliationLock;
			lock (foliationLock)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					node.ElementState = ElementState.Foliating;
					bool ignoreXmlEvents = this.IgnoreXmlEvents;
					this.IgnoreXmlEvents = true;
					try
					{
						XmlNode xmlNode = null;
						DataRow row = node.Row;
						DataRowVersion dataRowVersion = ((row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current);
						foreach (object obj in row.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj;
							if (!this.IsNotMapped(dataColumn))
							{
								object obj2 = row[dataColumn, dataRowVersion];
								if (!Convert.IsDBNull(obj2))
								{
									if (dataColumn.ColumnMapping == MappingType.Attribute)
									{
										node.SetAttribute(dataColumn.EncodedColumnName, dataColumn.Namespace, dataColumn.ConvertObjectToXml(obj2));
									}
									else if (dataColumn.ColumnMapping == MappingType.Element)
									{
										XmlNode xmlNode2 = new XmlBoundElement(string.Empty, dataColumn.EncodedColumnName, dataColumn.Namespace, this);
										xmlNode2.AppendChild(this.CreateTextNode(dataColumn.ConvertObjectToXml(obj2)));
										if (xmlNode != null)
										{
											node.InsertAfter(xmlNode2, xmlNode);
										}
										else if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										xmlNode = xmlNode2;
									}
									else
									{
										XmlNode xmlNode2 = this.CreateTextNode(dataColumn.ConvertObjectToXml(obj2));
										if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										if (xmlNode == null)
										{
											xmlNode = xmlNode2;
										}
									}
								}
								else if (dataColumn.ColumnMapping == MappingType.SimpleContent)
								{
									XmlAttribute xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
									xmlAttribute.Value = "true";
									node.SetAttributeNode(xmlAttribute);
									this._bHasXSINIL = true;
								}
							}
						}
					}
					finally
					{
						this.IgnoreXmlEvents = ignoreXmlEvents;
						node.ElementState = newState;
					}
					this.OnFoliated(node);
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A718 File Offset: 0x00008918
		private XmlNode GetColumnInsertAfterLocation(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			XmlNode xmlNode = null;
			if (this.IsTextOnly(col))
			{
				return null;
			}
			for (XmlNode xmlNode2 = rowElement.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
			{
				if (!XmlDataDocument.IsTextLikeNode(xmlNode2))
				{
					IL_0081:
					while (xmlNode2 != null && xmlNode2.NodeType == XmlNodeType.Element)
					{
						XmlElement xmlElement = xmlNode2 as XmlElement;
						if (this._mapper.GetRowFromElement(xmlElement) != null)
						{
							break;
						}
						object columnSchemaForNode = this._mapper.GetColumnSchemaForNode(rowElement, xmlNode2);
						if (columnSchemaForNode == null || !(columnSchemaForNode is DataColumn) || ((DataColumn)columnSchemaForNode).Ordinal > col.Ordinal)
						{
							break;
						}
						xmlNode = xmlNode2;
						xmlNode2 = xmlNode2.NextSibling;
					}
					return xmlNode;
				}
				xmlNode = xmlNode2;
			}
			goto IL_0081;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A7AC File Offset: 0x000089AC
		private ArrayList GetNestedChildRelations(DataRow row)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in row.Table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					arrayList.Add(dataRelation);
				}
			}
			return arrayList;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A81C File Offset: 0x00008A1C
		private DataRow GetNestedParent(DataRow row)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null)
			{
				return row.GetParentRow(nestedParentRelation);
			}
			return null;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A83C File Offset: 0x00008A3C
		private static DataRelation GetNestedParentRelation(DataRow row)
		{
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length == 0)
			{
				return null;
			}
			return nestedParentRelations[0];
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A85E File Offset: 0x00008A5E
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			return row.Table.XmlText;
		}

		/// <summary>Retrieves the <see cref="T:System.Data.DataRow" /> associated with the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <returns>The DataRow containing a representation of the XmlElement; null if there is no DataRow associated with the XmlElement.</returns>
		/// <param name="e">The XmlElement whose associated DataRow you want to retrieve. </param>
		// Token: 0x060001BD RID: 445 RVA: 0x0000A86B File Offset: 0x00008A6B
		public DataRow GetRowFromElement(XmlElement e)
		{
			return this._mapper.GetRowFromElement(e);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A87C File Offset: 0x00008A7C
		private XmlNode GetRowInsertBeforeLocation(DataRow row, XmlElement rowElement, XmlNode parentElement)
		{
			DataRow dataRow = row;
			int i = 0;
			while (i < row.Table.Rows.Count && row != row.Table.Rows[i])
			{
				i++;
			}
			int num = i;
			DataRow nestedParent = this.GetNestedParent(row);
			for (i = num + 1; i < row.Table.Rows.Count; i++)
			{
				dataRow = row.Table.Rows[i];
				if (this.GetNestedParent(dataRow) == nestedParent && this.GetElementFromRow(dataRow).ParentNode == parentElement)
				{
					break;
				}
			}
			if (i < row.Table.Rows.Count)
			{
				return this.GetElementFromRow(dataRow);
			}
			return null;
		}

		/// <summary>Retrieves the <see cref="T:System.Xml.XmlElement" /> associated with the specified <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>The XmlElement containing a representation of the specified DataRow.</returns>
		/// <param name="r">The DataRow whose associated XmlElement you want to retrieve. </param>
		// Token: 0x060001BF RID: 447 RVA: 0x0000A928 File Offset: 0x00008B28
		public XmlElement GetElementFromRow(DataRow r)
		{
			return r.Element;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A930 File Offset: 0x00008B30
		internal bool HasPointers(XmlNode node)
		{
			bool flag;
			for (;;)
			{
				try
				{
					if (this._pointers.Count > 0)
					{
						foreach (object obj in this._pointers)
						{
							if (((IXmlDataVirtualNode)((DictionaryEntry)obj).Value).IsOnNode(node))
							{
								return true;
							}
						}
					}
					flag = false;
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					continue;
				}
				break;
			}
			return flag;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000A9DC File Offset: 0x00008BDC
		internal bool IgnoreXmlEvents
		{
			get
			{
				return this._ignoreXmlEvents;
			}
			set
			{
				this._ignoreXmlEvents = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A9E5 File Offset: 0x00008BE5
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000A9ED File Offset: 0x00008BED
		internal bool IgnoreDataSetEvents
		{
			get
			{
				return this._ignoreDataSetEvents;
			}
			set
			{
				this._ignoreDataSetEvents = value;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		private bool IsFoliated(XmlElement element)
		{
			return !(element is XmlBoundElement) || ((XmlBoundElement)element).IsFoliated;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000AA0D File Offset: 0x00008C0D
		private bool IsFoliated(XmlBoundElement be)
		{
			return be.IsFoliated;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000AA15 File Offset: 0x00008C15
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000AA1D File Offset: 0x00008C1D
		internal bool IsFoliationEnabled
		{
			get
			{
				return this._isFoliationEnabled;
			}
			set
			{
				this._isFoliationEnabled = value;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000AA28 File Offset: 0x00008C28
		internal XmlNode CloneTree(DataPointer other)
		{
			this.EnsurePopulatedMode();
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			bool fAssociateDataRow = this._fAssociateDataRow;
			XmlNode xmlNode;
			try
			{
				this._ignoreDataSetEvents = true;
				this._ignoreXmlEvents = true;
				this.IsFoliationEnabled = false;
				this._fAssociateDataRow = false;
				xmlNode = this.CloneTreeInternal(other);
				this.LoadRows(null, xmlNode);
				this.SyncRows(null, xmlNode, false);
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this._fAssociateDataRow = fAssociateDataRow;
			}
			return xmlNode;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		private XmlNode CloneTreeInternal(DataPointer other)
		{
			XmlNode xmlNode = this.CloneNode(other);
			DataPointer dataPointer = new DataPointer(other);
			try
			{
				dataPointer.AddPointer();
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					int attributeCount = dataPointer.AttributeCount;
					for (int i = 0; i < attributeCount; i++)
					{
						dataPointer.MoveToOwnerElement();
						if (dataPointer.MoveToAttribute(i))
						{
							xmlNode.Attributes.Append((XmlAttribute)this.CloneTreeInternal(dataPointer));
						}
					}
					dataPointer.MoveTo(other);
				}
				bool flag = dataPointer.MoveToFirstChild();
				while (flag)
				{
					xmlNode.AppendChild(this.CloneTreeInternal(dataPointer));
					flag = dataPointer.MoveToNextSibling();
				}
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
			return xmlNode;
		}

		/// <summary>Creates a duplicate of the current node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001CB RID: 459 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)base.CloneNode(false);
			xmlDataDocument.Init(this.DataSet.Clone());
			xmlDataDocument._dataSet.EnforceConstraints = this._dataSet.EnforceConstraints;
			if (deep)
			{
				DataPointer dataPointer = new DataPointer(this, this);
				try
				{
					dataPointer.AddPointer();
					bool flag = dataPointer.MoveToFirstChild();
					while (flag)
					{
						XmlNode xmlNode;
						if (dataPointer.NodeType == XmlNodeType.Element)
						{
							xmlNode = xmlDataDocument.CloneTree(dataPointer);
						}
						else
						{
							xmlNode = xmlDataDocument.CloneNode(dataPointer);
						}
						xmlDataDocument.AppendChild(xmlNode);
						flag = dataPointer.MoveToNextSibling();
					}
				}
				finally
				{
					dataPointer.SetNoLongerUse();
				}
			}
			return xmlDataDocument;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000AC10 File Offset: 0x00008E10
		private XmlNode CloneNode(DataPointer dp)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				return this.CreateElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Attribute:
				return this.CreateAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(dp.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(dp.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(dp.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(dp.Name, dp.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(dp.Value);
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(dp.Name, dp.PublicId, dp.SystemId, dp.InternalSubset);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(dp.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(dp.Value);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration(dp.Version, dp.Encoding, dp.Standalone);
			}
			throw new InvalidOperationException(SR.Format("This type of node cannot be cloned: {0}.", dp.NodeType.ToString()));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000AD6C File Offset: 0x00008F6C
		internal static bool IsTextLikeNode(XmlNode n)
		{
			XmlNodeType nodeType = n.NodeType;
			if (nodeType - XmlNodeType.Text > 1)
			{
				if (nodeType == XmlNodeType.EntityReference)
				{
					return false;
				}
				if (nodeType - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000AD96 File Offset: 0x00008F96
		internal bool IsNotMapped(DataColumn c)
		{
			return DataSetMapper.IsNotMapped(c);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000AD9E File Offset: 0x00008F9E
		private bool IsSame(DataColumn c, int recNo1, int recNo2)
		{
			return c.Compare(recNo1, recNo2) == 0;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000ADAD File Offset: 0x00008FAD
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		/// <summary>Loads the XmlDataDocument using the specified URL.</summary>
		/// <param name="filename">The URL of the file containing the XML document to load. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D1 RID: 465 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public override void Load(string filename)
		{
			this._bForceExpandEntity = true;
			base.Load(filename);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the XmlDataDocument from the specified stream.</summary>
		/// <param name="inStream">The stream containing the XML document to load. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D2 RID: 466 RVA: 0x0000ADCF File Offset: 0x00008FCF
		public override void Load(Stream inStream)
		{
			this._bForceExpandEntity = true;
			base.Load(inStream);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the XmlDataDocument from the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="txtReader">The TextReader used to feed the XML data into the document. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D3 RID: 467 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		public override void Load(TextReader txtReader)
		{
			this._bForceExpandEntity = true;
			base.Load(txtReader);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the XmlDataDocument from the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The XmlReader containing the XML document to load.</param>
		/// <exception cref="T:System.NotSupportedException">The XML being loaded contains entity references, and the reader cannot resolve entities. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D4 RID: 468 RVA: 0x0000AE00 File Offset: 0x00009000
		public override void Load(XmlReader reader)
		{
			if (this.FirstChild != null)
			{
				throw new InvalidOperationException("Cannot load XmlDataDocument if it already contains data. Please use a new XmlDataDocument.");
			}
			try
			{
				this._ignoreXmlEvents = true;
				if (this._fDataRowCreatedSpecial)
				{
					this.UnBindSpecialListeners();
				}
				this._fAssociateDataRow = false;
				this._isFoliationEnabled = false;
				if (this._bForceExpandEntity)
				{
					((XmlTextReader)reader).EntityHandling = EntityHandling.ExpandEntities;
				}
				base.Load(reader);
				this.BindForLoad();
			}
			finally
			{
				this._ignoreXmlEvents = false;
				this._isFoliationEnabled = true;
				this._autoFoliationState = ElementState.StrongFoliation;
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000AE94 File Offset: 0x00009094
		private void LoadDataSetFromTree()
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			bool enforceConstraints = this._dataSet.EnforceConstraints;
			this._dataSet.EnforceConstraints = false;
			try
			{
				this.LoadRows(null, base.DocumentElement);
				this.SyncRows(null, base.DocumentElement, true);
				this._dataSet.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				this._ignoreDataSetEvents = false;
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AF24 File Offset: 0x00009124
		private void LoadTreeFromDataSet(DataSet ds)
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			this._fAssociateDataRow = false;
			DataTable[] array = this.OrderTables(ds);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					foreach (object obj in array[i].Rows)
					{
						DataRow dataRow = (DataRow)obj;
						this.AttachBoundElementToDataRow(dataRow);
						DataRowState rowState = dataRow.RowState;
						switch (rowState)
						{
						case DataRowState.Detached:
						case DataRowState.Detached | DataRowState.Unchanged:
							continue;
						case DataRowState.Unchanged:
						case DataRowState.Added:
							break;
						default:
							if (rowState == DataRowState.Deleted || rowState != DataRowState.Modified)
							{
								continue;
							}
							break;
						}
						this.OnAddRow(dataRow);
					}
				}
			}
			finally
			{
				this._ignoreDataSetEvents = false;
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B024 File Offset: 0x00009224
		private void LoadRows(XmlBoundElement rowElem, XmlNode node)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataTable dataTable = this._mapper.SearchMatchingTableSchema(rowElem, xmlBoundElement);
				if (dataTable != null)
				{
					DataRow dataRow = this.GetRowFromElement(xmlBoundElement);
					if (xmlBoundElement.ElementState == ElementState.None)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
					}
					dataRow = dataTable.CreateEmptyRow();
					this.Bind(dataRow, xmlBoundElement);
					rowElem = xmlBoundElement;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.LoadRows(rowElem, xmlNode);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000B090 File Offset: 0x00009290
		internal DataSetMapper Mapper
		{
			get
			{
				return this._mapper;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B098 File Offset: 0x00009298
		internal void OnDataRowCreated(object oDataSet, DataRow row)
		{
			this.OnNewRow(row);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B0A1 File Offset: 0x000092A1
		internal void OnClearCalled(object oDataSet, DataTable table)
		{
			throw new NotSupportedException("Clear function on DateSet and DataTable is not supported on XmlDataDocument.");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B0AD File Offset: 0x000092AD
		internal void OnDataRowCreatedSpecial(object oDataSet, DataRow row)
		{
			this.Bind(true);
			this.OnNewRow(row);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B0BD File Offset: 0x000092BD
		internal void OnNewRow(DataRow row)
		{
			this.AttachBoundElementToDataRow(row);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B0C8 File Offset: 0x000092C8
		private XmlBoundElement AttachBoundElementToDataRow(DataRow row)
		{
			DataTable table = row.Table;
			XmlBoundElement xmlBoundElement = new XmlBoundElement(string.Empty, table.EncodedTableName, table.Namespace, this);
			xmlBoundElement.IsEmpty = false;
			this.Bind(row, xmlBoundElement);
			xmlBoundElement.ElementState = ElementState.Defoliated;
			return xmlBoundElement;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B10C File Offset: 0x0000930C
		private bool NeedXSI_NilAttr(DataRow row)
		{
			DataTable table = row.Table;
			return table._xmlText != null && Convert.IsDBNull(row[table._xmlText]);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B13C File Offset: 0x0000933C
		private void OnAddRow(DataRow row)
		{
			XmlBoundElement xmlBoundElement = (XmlBoundElement)this.GetElementFromRow(row);
			if (this.NeedXSI_NilAttr(row) && !xmlBoundElement.IsFoliated)
			{
				this.ForceFoliation(xmlBoundElement, this.AutoFoliationState);
			}
			if (this.GetRowFromElement(base.DocumentElement) != null && this.GetNestedParent(row) == null)
			{
				this.DemoteDocumentElement();
			}
			this.EnsureDocumentElement().AppendChild(xmlBoundElement);
			this.FixNestedChildren(row, xmlBoundElement);
			this.OnNestedParentChange(row, xmlBoundElement, null);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B1B0 File Offset: 0x000093B0
		private void OnColumnValueChanged(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			if (!this.IsNotMapped(col))
			{
				object obj = row[col];
				if (col.ColumnMapping == MappingType.SimpleContent && Convert.IsDBNull(obj) && !rowElement.IsFoliated)
				{
					this.ForceFoliation(rowElement, ElementState.WeakFoliation);
				}
				else if (!this.IsFoliated(rowElement))
				{
					goto IL_0318;
				}
				if (this.IsTextOnly(col))
				{
					if (Convert.IsDBNull(obj))
					{
						obj = string.Empty;
						XmlAttribute xmlAttribute = rowElement.GetAttributeNode("xsi:nil");
						if (xmlAttribute == null)
						{
							xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
							xmlAttribute.Value = "true";
							rowElement.SetAttributeNode(xmlAttribute);
							this._bHasXSINIL = true;
						}
						else
						{
							xmlAttribute.Value = "true";
						}
					}
					else
					{
						XmlAttribute attributeNode = rowElement.GetAttributeNode("xsi:nil");
						if (attributeNode != null)
						{
							attributeNode.Value = "false";
						}
					}
					this.ReplaceInitialChildText(rowElement, col.ConvertObjectToXml(obj));
				}
				else
				{
					bool flag = false;
					if (col.ColumnMapping == MappingType.Attribute)
					{
						foreach (object obj2 in rowElement.Attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
							if (xmlAttribute2.LocalName == col.EncodedColumnName && xmlAttribute2.NamespaceURI == col.Namespace)
							{
								if (Convert.IsDBNull(obj))
								{
									xmlAttribute2.OwnerElement.Attributes.Remove(xmlAttribute2);
								}
								else
								{
									xmlAttribute2.Value = col.ConvertObjectToXml(obj);
								}
								flag = true;
								break;
							}
						}
						if (!flag && !Convert.IsDBNull(obj))
						{
							rowElement.SetAttribute(col.EncodedColumnName, col.Namespace, col.ConvertObjectToXml(obj));
						}
					}
					else
					{
						RegionIterator regionIterator = new RegionIterator(rowElement);
						bool flag2 = regionIterator.Next();
						while (flag2)
						{
							if (regionIterator.CurrentNode.NodeType == XmlNodeType.Element)
							{
								XmlElement xmlElement = (XmlElement)regionIterator.CurrentNode;
								XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
								if (xmlBoundElement != null && xmlBoundElement.Row != null)
								{
									flag2 = regionIterator.NextRight();
									continue;
								}
								if (xmlElement.LocalName == col.EncodedColumnName && xmlElement.NamespaceURI == col.Namespace)
								{
									flag = true;
									if (Convert.IsDBNull(obj))
									{
										this.PromoteNonValueChildren(xmlElement);
										flag2 = regionIterator.NextRight();
										xmlElement.ParentNode.RemoveChild(xmlElement);
										continue;
									}
									this.ReplaceInitialChildText(xmlElement, col.ConvertObjectToXml(obj));
									XmlAttribute attributeNode2 = xmlElement.GetAttributeNode("xsi:nil");
									if (attributeNode2 != null)
									{
										attributeNode2.Value = "false";
										goto IL_0318;
									}
									goto IL_0318;
								}
							}
							flag2 = regionIterator.Next();
						}
						if (!flag && !Convert.IsDBNull(obj))
						{
							XmlElement xmlElement2 = new XmlBoundElement(string.Empty, col.EncodedColumnName, col.Namespace, this);
							xmlElement2.AppendChild(this.CreateTextNode(col.ConvertObjectToXml(obj)));
							XmlNode columnInsertAfterLocation = this.GetColumnInsertAfterLocation(row, col, rowElement);
							if (columnInsertAfterLocation != null)
							{
								rowElement.InsertAfter(xmlElement2, columnInsertAfterLocation);
							}
							else if (rowElement.FirstChild != null)
							{
								rowElement.InsertBefore(xmlElement2, rowElement.FirstChild);
							}
							else
							{
								rowElement.AppendChild(xmlElement2);
							}
						}
					}
				}
			}
			IL_0318:
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null && nestedParentRelation.ChildKey.ContainsColumn(col))
			{
				this.OnNestedParentChange(row, rowElement, col);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B50C File Offset: 0x0000970C
		private void OnColumnChanged(object sender, DataColumnChangeEventArgs args)
		{
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				DataColumn column = args.Column;
				object proposedValue = args.ProposedValue;
				if (row.RowState == DataRowState.Detached)
				{
					XmlBoundElement element = row.Element;
					if (element.IsFoliated)
					{
						this.OnColumnValueChanged(row, column, element);
					}
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this._ignoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B598 File Offset: 0x00009798
		private void OnColumnValuesChanged(DataRow row, XmlBoundElement rowElement)
		{
			if (this._columnChangeList.Count > 0)
			{
				if (((DataColumn)this._columnChangeList[0]).Table == row.Table)
				{
					using (IEnumerator enumerator = this._columnChangeList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DataColumn dataColumn = (DataColumn)obj;
							this.OnColumnValueChanged(row, dataColumn, rowElement);
						}
						goto IL_00F8;
					}
				}
				using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataColumn dataColumn2 = (DataColumn)obj2;
						this.OnColumnValueChanged(row, dataColumn2, rowElement);
					}
					goto IL_00F8;
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn dataColumn3 = (DataColumn)obj3;
				this.OnColumnValueChanged(row, dataColumn3, rowElement);
			}
			IL_00F8:
			this._columnChangeList.Clear();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B6D0 File Offset: 0x000098D0
		private void OnDeleteRow(DataRow row, XmlBoundElement rowElement)
		{
			if (rowElement == base.DocumentElement)
			{
				this.DemoteDocumentElement();
			}
			this.PromoteInnerRegions(rowElement);
			rowElement.ParentNode.RemoveChild(rowElement);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private void OnDeletingRow(DataRow row, XmlBoundElement rowElement)
		{
			if (this.IsFoliated(rowElement))
			{
				return;
			}
			bool ignoreXmlEvents = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = true;
			try
			{
				this.Foliate(rowElement);
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B754 File Offset: 0x00009954
		private void OnFoliated(XmlNode node)
		{
			for (;;)
			{
				try
				{
					if (this._pointers.Count > 0)
					{
						foreach (object obj in this._pointers)
						{
							((IXmlDataVirtualNode)((DictionaryEntry)obj).Value).OnFoliated(node);
						}
					}
				}
				catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
				{
					continue;
				}
				break;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B7F0 File Offset: 0x000099F0
		private DataColumn FindAssociatedParentColumn(DataRelation relation, DataColumn childCol)
		{
			DataColumn[] columnsReference = relation.ChildKey.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (childCol == columnsReference[i])
				{
					return relation.ParentKey.ColumnsReference[i];
				}
			}
			return null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B834 File Offset: 0x00009A34
		private void OnNestedParentChange(DataRow child, XmlBoundElement childElement, DataColumn childCol)
		{
			DataRow dataRow;
			if (childElement == base.DocumentElement || childElement.ParentNode == null)
			{
				dataRow = null;
			}
			else
			{
				dataRow = this.GetRowFromElement((XmlElement)childElement.ParentNode);
			}
			DataRow nestedParent = this.GetNestedParent(child);
			if (dataRow != nestedParent)
			{
				if (nestedParent != null)
				{
					this.GetElementFromRow(nestedParent).AppendChild(childElement);
					return;
				}
				DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(child);
				if (childCol == null || nestedParentRelation == null || Convert.IsDBNull(child[childCol]))
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
					return;
				}
				DataColumn dataColumn = this.FindAssociatedParentColumn(nestedParentRelation, childCol);
				object obj = dataColumn.ConvertValue(child[childCol]);
				if (dataRow._tempRecord != -1 && dataColumn.CompareValueTo(dataRow._tempRecord, obj) != 0)
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B8F0 File Offset: 0x00009AF0
		private void OnNodeChanged(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlBoundElement xmlBoundElement = null;
				if (this._mapper.GetRegion(args.Node, out xmlBoundElement))
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B99C File Offset: 0x00009B9C
		private void OnNodeChanging(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		private void OnNodeInserted(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				XmlNode newParent = args.NewParent;
				if (this.IsConnected(newParent))
				{
					this.OnNodeInsertedInTree(node);
				}
				else
				{
					this.OnNodeInsertedInFragment(node);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B99C File Offset: 0x00009B9C
		private void OnNodeInserting(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BA80 File Offset: 0x00009C80
		private void OnNodeRemoved(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				if (this.IsConnected(oldParent))
				{
					this.OnNodeRemovedFromTree(node, oldParent);
				}
				else
				{
					this.OnNodeRemovedFromFragment(node, oldParent);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B99C File Offset: 0x00009B9C
		private void OnNodeRemoving(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BB3C File Offset: 0x00009D3C
		private void OnNodeRemovedFromTree(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement xmlBoundElement;
			if (this._mapper.GetRegion(oldParent, out xmlBoundElement))
			{
				this.SynchronizeRowFromRowElement(xmlBoundElement);
			}
			XmlBoundElement xmlBoundElement2 = node as XmlBoundElement;
			if (xmlBoundElement2 != null && xmlBoundElement2.Row != null)
			{
				this.EnsureDisconnectedDataRow(xmlBoundElement2);
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				xmlBoundElement2 = (XmlBoundElement)treeIterator.CurrentNode;
				this.EnsureDisconnectedDataRow(xmlBoundElement2);
				flag = treeIterator.NextRowElement();
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		private void OnNodeRemovedFromFragment(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement xmlBoundElement;
			if (this._mapper.GetRegion(oldParent, out xmlBoundElement))
			{
				DataRow row = xmlBoundElement.Row;
				if (xmlBoundElement.Row.RowState == DataRowState.Detached)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
				}
			}
			XmlBoundElement xmlBoundElement2 = node as XmlBoundElement;
			if (xmlBoundElement2 != null && xmlBoundElement2.Row != null)
			{
				this.SetNestedParentRegion(xmlBoundElement2, null);
				return;
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement xmlBoundElement3 = (XmlBoundElement)treeIterator.CurrentNode;
				this.SetNestedParentRegion(xmlBoundElement3, null);
				flag = treeIterator.NextRightRowElement();
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000BC2C File Offset: 0x00009E2C
		private void OnRowChanged(object sender, DataRowChangeEventArgs args)
		{
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				XmlBoundElement element = row.Element;
				DataRowAction action = args.Action;
				switch (action)
				{
				case DataRowAction.Delete:
					this.OnDeleteRow(row, element);
					break;
				case DataRowAction.Change:
					this.OnColumnValuesChanged(row, element);
					break;
				case DataRowAction.Delete | DataRowAction.Change:
					break;
				case DataRowAction.Rollback:
				{
					DataRowState rollbackState = this._rollbackState;
					if (rollbackState != DataRowState.Added)
					{
						if (rollbackState != DataRowState.Deleted)
						{
							if (rollbackState == DataRowState.Modified)
							{
								this.OnColumnValuesChanged(row, element);
							}
						}
						else
						{
							this.OnUndeleteRow(row, element);
							this.UpdateAllColumns(row, element);
						}
					}
					else
					{
						element.ParentNode.RemoveChild(element);
					}
					break;
				}
				default:
					if (action != DataRowAction.Commit)
					{
						if (action == DataRowAction.Add)
						{
							this.OnAddRow(row);
						}
					}
					else if (row.RowState == DataRowState.Detached)
					{
						element.RemoveAll();
					}
					break;
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this._ignoreXmlEvents = false;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000BD20 File Offset: 0x00009F20
		private void OnRowChanging(object sender, DataRowChangeEventArgs args)
		{
			DataRow row = args.Row;
			if (args.Action == DataRowAction.Delete && row.Element != null)
			{
				this.OnDeletingRow(row, row.Element);
				return;
			}
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				this._ignoreXmlEvents = true;
				XmlElement elementFromRow = this.GetElementFromRow(row);
				if (elementFromRow != null)
				{
					DataRowAction action = args.Action;
					int num;
					int num2;
					switch (action)
					{
					case DataRowAction.Delete:
					case DataRowAction.Delete | DataRowAction.Change:
						goto IL_0212;
					case DataRowAction.Change:
						break;
					case DataRowAction.Rollback:
					{
						this._rollbackState = row.RowState;
						DataRowState rollbackState = this._rollbackState;
						if (rollbackState <= DataRowState.Added)
						{
							if (rollbackState != DataRowState.Detached && rollbackState != DataRowState.Added)
							{
								return;
							}
							goto IL_0212;
						}
						else
						{
							if (rollbackState == DataRowState.Deleted)
							{
								goto IL_0212;
							}
							if (rollbackState != DataRowState.Modified)
							{
								return;
							}
							this._columnChangeList.Clear();
							num = row.GetRecordFromVersion(DataRowVersion.Original);
							num2 = row.GetRecordFromVersion(DataRowVersion.Current);
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (!this.IsSame(dataColumn, num, num2))
									{
										this._columnChangeList.Add(dataColumn);
									}
								}
								return;
							}
						}
						break;
					}
					default:
						if (action != DataRowAction.Commit && action != DataRowAction.Add)
						{
							goto IL_0212;
						}
						goto IL_0212;
					}
					this._columnChangeList.Clear();
					num = row.GetRecordFromVersion(DataRowVersion.Proposed);
					num2 = row.GetRecordFromVersion(DataRowVersion.Current);
					foreach (object obj2 in row.Table.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						object obj3 = row[dataColumn2, DataRowVersion.Proposed];
						object obj4 = row[dataColumn2, DataRowVersion.Current];
						if (Convert.IsDBNull(obj3) && !Convert.IsDBNull(obj4) && dataColumn2.ColumnMapping != MappingType.Hidden)
						{
							this.FoliateIfDataPointers(row, elementFromRow);
						}
						if (!this.IsSame(dataColumn2, num, num2))
						{
							this._columnChangeList.Add(dataColumn2);
						}
					}
				}
				IL_0212:;
			}
			finally
			{
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000BF9C File Offset: 0x0000A19C
		private void OnDataSetPropertyChanging(object oDataSet, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "DataSetName")
			{
				throw new InvalidOperationException("Cannot change the DataSet name once the DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private void OnColumnPropertyChanging(object oColumn, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "ColumnName")
			{
				throw new InvalidOperationException("Cannot change the column name once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException("Cannot change the column namespace once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "ColumnMapping")
			{
				throw new InvalidOperationException("Cannot change the ColumnMapping property once the associated DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C020 File Offset: 0x0000A220
		private void OnTablePropertyChanging(object oTable, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "TableName")
			{
				throw new InvalidOperationException("Cannot change the table name once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException("Cannot change the table namespace once the associated DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C05C File Offset: 0x0000A25C
		private void OnTableColumnsChanging(object oColumnsCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException("Cannot add or remove columns from the table once the DataSet is mapped to a loaded XML document.");
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C068 File Offset: 0x0000A268
		private void OnDataSetTablesChanging(object oTablesCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException("Cannot add or remove tables from the DataSet once the DataSet is mapped to a loaded XML document.");
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C074 File Offset: 0x0000A274
		private void OnDataSetRelationsChanging(object oRelationsCollection, CollectionChangeEventArgs args)
		{
			DataRelation dataRelation = (DataRelation)args.Element;
			if (dataRelation != null && dataRelation.Nested)
			{
				throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
			}
			if (args.Action == CollectionChangeAction.Refresh)
			{
				using (IEnumerator enumerator = ((DataRelationCollection)oRelationsCollection).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((DataRelation)enumerator.Current).Nested)
						{
							throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
						}
					}
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C104 File Offset: 0x0000A304
		private void OnRelationPropertyChanging(object oRelationsCollection, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Nested")
			{
				throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C124 File Offset: 0x0000A324
		private void OnUndeleteRow(DataRow row, XmlElement rowElement)
		{
			if (rowElement.ParentNode != null)
			{
				rowElement.ParentNode.RemoveChild(rowElement);
			}
			DataRow nestedParent = this.GetNestedParent(row);
			XmlElement xmlElement;
			if (nestedParent == null)
			{
				xmlElement = this.EnsureNonRowDocumentElement();
			}
			else
			{
				xmlElement = this.GetElementFromRow(nestedParent);
			}
			XmlNode rowInsertBeforeLocation;
			if ((rowInsertBeforeLocation = this.GetRowInsertBeforeLocation(row, rowElement, xmlElement)) != null)
			{
				xmlElement.InsertBefore(rowElement, rowInsertBeforeLocation);
			}
			else
			{
				xmlElement.AppendChild(rowElement);
			}
			this.FixNestedChildren(row, rowElement);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C18A File Offset: 0x0000A38A
		private void PromoteChild(XmlNode child, XmlNode prevSibling)
		{
			if (child.ParentNode != null)
			{
				child.ParentNode.RemoveChild(child);
			}
			prevSibling.ParentNode.InsertAfter(child, prevSibling);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		private void PromoteInnerRegions(XmlNode parent)
		{
			XmlBoundElement xmlBoundElement;
			this._mapper.GetRegion(parent.ParentNode, out xmlBoundElement);
			TreeIterator treeIterator = new TreeIterator(parent);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement xmlBoundElement2 = (XmlBoundElement)treeIterator.CurrentNode;
				flag = treeIterator.NextRightRowElement();
				this.PromoteChild(xmlBoundElement2, parent);
				this.SetNestedParentRegion(xmlBoundElement2, xmlBoundElement);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000C20C File Offset: 0x0000A40C
		private void PromoteNonValueChildren(XmlNode parent)
		{
			XmlNode xmlNode = parent;
			XmlNode xmlNode2 = parent.FirstChild;
			bool flag = true;
			while (xmlNode2 != null)
			{
				XmlNode xmlNode3 = xmlNode2.NextSibling;
				if (!flag || !XmlDataDocument.IsTextLikeNode(xmlNode2))
				{
					flag = false;
					xmlNode3 = xmlNode2.NextSibling;
					this.PromoteChild(xmlNode2, xmlNode);
					xmlNode = xmlNode2;
				}
				xmlNode2 = xmlNode3;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000C252 File Offset: 0x0000A452
		private void RemoveInitialTextNodes(XmlNode node)
		{
			while (node != null && XmlDataDocument.IsTextLikeNode(node))
			{
				XmlNode nextSibling = node.NextSibling;
				node.ParentNode.RemoveChild(node);
				node = nextSibling;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C278 File Offset: 0x0000A478
		private void ReplaceInitialChildText(XmlNode parent, string value)
		{
			XmlNode xmlNode = parent.FirstChild;
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Whitespace)
			{
				xmlNode = xmlNode.NextSibling;
			}
			if (xmlNode != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Text)
				{
					xmlNode.Value = value;
				}
				else
				{
					xmlNode = parent.InsertBefore(this.CreateTextNode(value), xmlNode);
				}
				this.RemoveInitialTextNodes(xmlNode.NextSibling);
				return;
			}
			parent.AppendChild(this.CreateTextNode(value));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		internal XmlNode SafeFirstChild(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeFirstChild;
			}
			return n.FirstChild;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C308 File Offset: 0x0000A508
		internal XmlNode SafeNextSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeNextSibling;
			}
			return n.NextSibling;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C32C File Offset: 0x0000A52C
		internal XmlNode SafePreviousSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafePreviousSibling;
			}
			return n.PreviousSibling;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000C350 File Offset: 0x0000A550
		internal static void SetRowValueToNull(DataRow row, DataColumn col)
		{
			if (!row.IsNull(col))
			{
				row[col] = DBNull.Value;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000C368 File Offset: 0x0000A568
		internal static void SetRowValueFromXmlText(DataRow row, DataColumn col, string xmlText)
		{
			object obj;
			try
			{
				obj = col.ConvertXmlToObject(xmlText);
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				XmlDataDocument.SetRowValueToNull(row, col);
				return;
			}
			if (!obj.Equals(row[col]))
			{
				row[col] = obj;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement)
		{
			this.SynchronizeRowFromRowElement(rowElement, null);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			if (row.RowState == DataRowState.Deleted)
			{
				return;
			}
			row.BeginEdit();
			this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			row.EndEdit();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000C408 File Offset: 0x0000A608
		private void SynchronizeRowFromRowElementEx(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			DataTable table = row.Table;
			Hashtable hashtable = new Hashtable();
			string text = string.Empty;
			RegionIterator regionIterator = new RegionIterator(rowElement);
			DataColumn textOnlyColumn = this.GetTextOnlyColumn(row);
			bool flag;
			if (textOnlyColumn != null)
			{
				hashtable[textOnlyColumn] = textOnlyColumn;
				string text2;
				flag = regionIterator.NextInitialTextLikeNodes(out text2);
				if (text2.Length == 0 && ((text = rowElement.GetAttribute("xsi:nil")) == "1" || text == "true"))
				{
					row[textOnlyColumn] = DBNull.Value;
				}
				else
				{
					XmlDataDocument.SetRowValueFromXmlText(row, textOnlyColumn, text2);
				}
			}
			else
			{
				flag = regionIterator.Next();
			}
			while (flag)
			{
				XmlElement xmlElement = regionIterator.CurrentNode as XmlElement;
				if (xmlElement == null)
				{
					flag = regionIterator.Next();
				}
				else
				{
					XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						if (rowElemList != null)
						{
							rowElemList.Add(xmlElement);
						}
						flag = regionIterator.NextRight();
					}
					else
					{
						DataColumn columnSchemaForNode = this._mapper.GetColumnSchemaForNode(rowElement, xmlElement);
						if (columnSchemaForNode != null && hashtable[columnSchemaForNode] == null)
						{
							hashtable[columnSchemaForNode] = columnSchemaForNode;
							string text3;
							flag = regionIterator.NextInitialTextLikeNodes(out text3);
							if (text3.Length == 0 && ((text = xmlElement.GetAttribute("xsi:nil")) == "1" || text == "true"))
							{
								row[columnSchemaForNode] = DBNull.Value;
							}
							else
							{
								XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode, text3);
							}
						}
						else
						{
							flag = regionIterator.Next();
						}
					}
				}
			}
			foreach (object obj in rowElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				DataColumn columnSchemaForNode2 = this._mapper.GetColumnSchemaForNode(rowElement, xmlAttribute);
				if (columnSchemaForNode2 != null && hashtable[columnSchemaForNode2] == null)
				{
					hashtable[columnSchemaForNode2] = columnSchemaForNode2;
					XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode2, xmlAttribute.Value);
				}
			}
			foreach (object obj2 in row.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (hashtable[dataColumn] == null && !this.IsNotMapped(dataColumn))
				{
					if (!dataColumn.AutoIncrement)
					{
						XmlDataDocument.SetRowValueToNull(row, dataColumn);
					}
					else
					{
						dataColumn.Init(row._tempRecord);
					}
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000C694 File Offset: 0x0000A894
		private void UpdateAllColumns(DataRow row, XmlBoundElement rowElement)
		{
			foreach (object obj in row.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				this.OnColumnValueChanged(row, dataColumn, rowElement);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDataDocument" /> class.</summary>
		// Token: 0x06000208 RID: 520 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public XmlDataDocument()
			: base(new XmlDataImplementation())
		{
			this.Init();
			this.AttachDataSet(new DataSet());
			this._dataSet.EnforceConstraints = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDataDocument" /> class with the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataset">The DataSet to load into XmlDataDocument. </param>
		// Token: 0x06000209 RID: 521 RVA: 0x0000C71E File Offset: 0x0000A91E
		public XmlDataDocument(DataSet dataset)
			: base(new XmlDataImplementation())
		{
			this.Init(dataset);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C732 File Offset: 0x0000A932
		internal XmlDataDocument(XmlImplementation imp)
			: base(imp)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000C73C File Offset: 0x0000A93C
		private void Init()
		{
			this._pointers = new Hashtable();
			this._countAddPointer = 0;
			this._columnChangeList = new ArrayList();
			this._ignoreDataSetEvents = false;
			this._isFoliationEnabled = true;
			this._optimizeStorage = true;
			this._fDataRowCreatedSpecial = false;
			this._autoFoliationState = ElementState.StrongFoliation;
			this._fAssociateDataRow = true;
			this._mapper = new DataSetMapper();
			this._foliationLock = new object();
			this._ignoreXmlEvents = true;
			this._attrXml = this.CreateAttribute("xmlns", "xml", "http://www.w3.org/2000/xmlns/");
			this._attrXml.Value = "http://www.w3.org/XML/1998/namespace";
			this._ignoreXmlEvents = false;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		private void Init(DataSet ds)
		{
			if (ds == null)
			{
				throw new ArgumentException("The DataSet parameter is invalid. It cannot be null.");
			}
			this.Init();
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException("DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.");
			}
			ds.FBoundToDocument = true;
			this._dataSet = ds;
			this.Bind(true);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C820 File Offset: 0x0000AA20
		private bool IsConnected(XmlNode node)
		{
			while (node != null)
			{
				if (node == this)
				{
					return true;
				}
				XmlAttribute xmlAttribute = node as XmlAttribute;
				if (xmlAttribute != null)
				{
					node = xmlAttribute.OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			return false;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C855 File Offset: 0x0000AA55
		private bool IsRowLive(DataRow row)
		{
			return (row.RowState & (DataRowState.Unchanged | DataRowState.Added | DataRowState.Modified)) > (DataRowState)0;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C864 File Offset: 0x0000AA64
		private static void SetNestedParentRow(DataRow childRow, DataRow parentRow)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(childRow);
			if (nestedParentRelation != null)
			{
				if (parentRow == null || nestedParentRelation.ParentKey.Table != parentRow.Table)
				{
					childRow.SetParentRow(null, nestedParentRelation);
					return;
				}
				childRow.SetParentRow(parentRow, nestedParentRelation);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		private void OnNodeInsertedInTree(XmlNode node)
		{
			ArrayList arrayList = new ArrayList();
			XmlBoundElement xmlBoundElement;
			if (this._mapper.GetRegion(node, out xmlBoundElement))
			{
				if (xmlBoundElement == node)
				{
					this.OnRowElementInsertedInTree(xmlBoundElement, arrayList);
				}
				else
				{
					this.OnNonRowElementInsertedInTree(node, xmlBoundElement, arrayList);
				}
			}
			else
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					arrayList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
			while (arrayList.Count > 0)
			{
				XmlBoundElement xmlBoundElement2 = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.OnRowElementInsertedInTree(xmlBoundElement2, arrayList);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C934 File Offset: 0x0000AB34
		private void OnNodeInsertedInFragment(XmlNode node)
		{
			XmlBoundElement xmlBoundElement;
			if (!this._mapper.GetRegion(node, out xmlBoundElement))
			{
				return;
			}
			if (xmlBoundElement == node)
			{
				this.SetNestedParentRegion(xmlBoundElement);
				return;
			}
			ArrayList arrayList = new ArrayList();
			this.OnNonRowElementInsertedInFragment(node, xmlBoundElement, arrayList);
			while (arrayList.Count > 0)
			{
				XmlBoundElement xmlBoundElement2 = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.SetNestedParentRegion(xmlBoundElement2, xmlBoundElement);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C994 File Offset: 0x0000AB94
		private void OnRowElementInsertedInTree(XmlBoundElement rowElem, ArrayList rowElemList)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			if (rowState != DataRowState.Detached)
			{
				if (rowState != DataRowState.Deleted)
				{
					return;
				}
				row.RejectChanges();
				this.SynchronizeRowFromRowElement(rowElem, rowElemList);
				this.SetNestedParentRegion(rowElem);
			}
			else
			{
				row.Table.Rows.Add(row);
				this.SetNestedParentRegion(rowElem);
				if (rowElemList != null)
				{
					RegionIterator regionIterator = new RegionIterator(rowElem);
					bool flag = regionIterator.NextRowElement();
					while (flag)
					{
						rowElemList.Add(regionIterator.CurrentNode);
						flag = regionIterator.NextRightRowElement();
					}
					return;
				}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000CA10 File Offset: 0x0000AC10
		private void EnsureDisconnectedDataRow(XmlBoundElement rowElem)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			switch (rowState)
			{
			case DataRowState.Detached:
				this.SetNestedParentRegion(rowElem);
				return;
			case DataRowState.Unchanged:
				break;
			case DataRowState.Detached | DataRowState.Unchanged:
				return;
			case DataRowState.Added:
				this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
				row.Delete();
				this.SetNestedParentRegion(rowElem);
				return;
			default:
				if (rowState == DataRowState.Deleted)
				{
					return;
				}
				if (rowState != DataRowState.Modified)
				{
					return;
				}
				break;
			}
			this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
			row.Delete();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private void OnNonRowElementInsertedInTree(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			this.SynchronizeRowFromRowElement(rowElement);
			if (rowElemList != null)
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					rowElemList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000CAC1 File Offset: 0x0000ACC1
		private void OnNonRowElementInsertedInFragment(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			if (rowElement.Row.RowState == DataRowState.Detached)
			{
				this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000CADC File Offset: 0x0000ACDC
		private void SetNestedParentRegion(XmlBoundElement childRowElem)
		{
			XmlBoundElement xmlBoundElement;
			this._mapper.GetRegion(childRowElem.ParentNode, out xmlBoundElement);
			this.SetNestedParentRegion(childRowElem, xmlBoundElement);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CB08 File Offset: 0x0000AD08
		private void SetNestedParentRegion(XmlBoundElement childRowElem, XmlBoundElement parentRowElem)
		{
			DataRow row = childRowElem.Row;
			if (parentRowElem == null)
			{
				XmlDataDocument.SetNestedParentRow(row, null);
				return;
			}
			DataRow row2 = parentRowElem.Row;
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length != 0 && nestedParentRelations[0].ParentTable == row2.Table)
			{
				XmlDataDocument.SetNestedParentRow(row, row2);
				return;
			}
			XmlDataDocument.SetNestedParentRow(row, null);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document. The XPathNavigator is positioned on the node specified in the <paramref name="node" /> parameter.</summary>
		/// <returns>An XPathNavigator used to navigate the document.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> you want the navigator initially positioned on. </param>
		// Token: 0x06000219 RID: 537 RVA: 0x0000CB70 File Offset: 0x0000AD70
		protected override XPathNavigator CreateNavigator(XmlNode node)
		{
			if (XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType] == -1)
			{
				return null;
			}
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					return null;
				}
				XmlNode xmlNode = node.PreviousSibling;
				while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
				{
					node = xmlNode;
					xmlNode = this.SafePreviousSibling(node);
				}
			}
			return new DataDocumentXPathNavigator(this, node);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		[Conditional("DEBUG")]
		private void AssertLiveRows(XmlNode node)
		{
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					xmlBoundElement = treeIterator.CurrentNode as XmlBoundElement;
					flag = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000CC48 File Offset: 0x0000AE48
		[Conditional("DEBUG")]
		private void AssertNonLiveRows(XmlNode node)
		{
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					xmlBoundElement = treeIterator.CurrentNode as XmlBoundElement;
					flag = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> with the specified ID. This method is not supported by the <see cref="T:System.Xml.XmlDataDocument" /> class. Calling this method throws an exception.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> with the specified ID.</returns>
		/// <param name="elemId">The attribute ID to match.</param>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x0600021C RID: 540 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public override XmlElement GetElementById(string elemId)
		{
			throw new NotSupportedException("GetElementById() is not supported on DataDocument.");
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		/// <param name="name">The qualified name to match. It is matched against the <see cref="P:System.Xml.XmlDocument.Name" /> property of the matching node. The special value "*" matches all tags.</param>
		// Token: 0x0600021D RID: 541 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		private DataTable[] OrderTables(DataSet ds)
		{
			DataTable[] array = null;
			if (ds == null || ds.Tables.Count == 0)
			{
				array = Array.Empty<DataTable>();
			}
			else if (this.TablesAreOrdered(ds))
			{
				array = new DataTable[ds.Tables.Count];
				ds.Tables.CopyTo(array, 0);
			}
			if (array == null)
			{
				array = new DataTable[ds.Tables.Count];
				List<DataTable> list = new List<DataTable>();
				foreach (object obj in ds.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (dataTable.ParentRelations.Count == 0)
					{
						list.Add(dataTable);
					}
				}
				if (list.Count > 0)
				{
					foreach (object obj2 in ds.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						if (this.IsSelfRelatedDataTable(dataTable2))
						{
							list.Add(dataTable2);
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						foreach (object obj3 in list[i].ChildRelations)
						{
							DataTable childTable = ((DataRelation)obj3).ChildTable;
							if (!list.Contains(childTable))
							{
								list.Add(childTable);
							}
						}
					}
					list.CopyTo(array);
				}
				else
				{
					ds.Tables.CopyTo(array, 0);
				}
			}
			return array;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CE8C File Offset: 0x0000B08C
		private bool IsSelfRelatedDataTable(DataTable rootTable)
		{
			List<DataTable> list = new List<DataTable>();
			bool flag = false;
			foreach (object obj in rootTable.ChildRelations)
			{
				DataTable childTable = ((DataRelation)obj).ChildTable;
				if (childTable == rootTable)
				{
					flag = true;
					break;
				}
				if (!list.Contains(childTable))
				{
					list.Add(childTable);
				}
			}
			if (!flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					foreach (object obj2 in list[i].ChildRelations)
					{
						DataTable childTable2 = ((DataRelation)obj2).ChildTable;
						if (childTable2 == rootTable)
						{
							flag = true;
							break;
						}
						if (!list.Contains(childTable2))
						{
							list.Add(childTable2);
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CF90 File Offset: 0x0000B190
		private bool TablesAreOrdered(DataSet ds)
		{
			using (IEnumerator enumerator = ds.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Namespace != ds.Namespace)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000431 RID: 1073
		private DataSet _dataSet;

		// Token: 0x04000432 RID: 1074
		private DataSetMapper _mapper;

		// Token: 0x04000433 RID: 1075
		internal Hashtable _pointers;

		// Token: 0x04000434 RID: 1076
		private int _countAddPointer;

		// Token: 0x04000435 RID: 1077
		private ArrayList _columnChangeList;

		// Token: 0x04000436 RID: 1078
		private DataRowState _rollbackState;

		// Token: 0x04000437 RID: 1079
		private bool _fBoundToDataSet;

		// Token: 0x04000438 RID: 1080
		private bool _fBoundToDocument;

		// Token: 0x04000439 RID: 1081
		private bool _fDataRowCreatedSpecial;

		// Token: 0x0400043A RID: 1082
		private bool _ignoreXmlEvents;

		// Token: 0x0400043B RID: 1083
		private bool _ignoreDataSetEvents;

		// Token: 0x0400043C RID: 1084
		private bool _isFoliationEnabled;

		// Token: 0x0400043D RID: 1085
		private bool _optimizeStorage;

		// Token: 0x0400043E RID: 1086
		private ElementState _autoFoliationState;

		// Token: 0x0400043F RID: 1087
		private bool _fAssociateDataRow;

		// Token: 0x04000440 RID: 1088
		private object _foliationLock;

		// Token: 0x04000441 RID: 1089
		internal const string XSI_NIL = "xsi:nil";

		// Token: 0x04000442 RID: 1090
		internal const string XSI = "xsi";

		// Token: 0x04000443 RID: 1091
		private bool _bForceExpandEntity;

		// Token: 0x04000444 RID: 1092
		internal XmlAttribute _attrXml;

		// Token: 0x04000445 RID: 1093
		internal bool _bLoadFromDataSet;

		// Token: 0x04000446 RID: 1094
		internal bool _bHasXSINIL;
	}
}
