using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data
{
	/// <summary>Contains a default <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009C RID: 156
	public class DataViewManager : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class.</summary>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002C66C File Offset: 0x0002A86C
		public DataViewManager()
			: this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class for the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The name of the <see cref="T:System.Data.DataSet" /> to use. </param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0002C676 File Offset: 0x0002A876
		public DataViewManager(DataSet dataSet)
			: this(dataSet, false)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002C680 File Offset: 0x0002A880
		internal DataViewManager(DataSet dataSet, bool locked)
		{
			GC.SuppressFinalize(this);
			this._dataSet = dataSet;
			if (this._dataSet != null)
			{
				this._dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this._dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
			}
			this._locked = locked;
			this._item = new DataViewManagerListItemTypeDescriptor(this);
			this._dataViewSettingsCollection = new DataViewSettingCollection(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataSet" /> to use with the <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to use.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002C701 File Offset: 0x0002A901
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0002C70C File Offset: 0x0002A90C
		[DefaultValue(null)]
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.SetFailed("DataSet to null");
				}
				if (this._locked)
				{
					throw ExceptionBuilder.SetDataSetFailed();
				}
				if (this._dataSet != null)
				{
					if (this._nViews > 0)
					{
						throw ExceptionBuilder.CanNotSetDataSet();
					}
					this._dataSet.Tables.CollectionChanged -= this.TableCollectionChanged;
					this._dataSet.Relations.CollectionChanged -= this.RelationCollectionChanged;
				}
				this._dataSet = value;
				this._dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this._dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
				this._dataViewSettingsCollection = new DataViewSettingCollection(this);
				this._item.Reset();
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSettingCollection" /> for each DataTable.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002C7DE File Offset: 0x0002A9DE
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this._dataViewSettingsCollection;
			}
		}

		/// <summary>Gets or sets a value that is used for code persistence.</summary>
		/// <returns>A value that is used for code persistence.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002C7E8 File Offset: 0x0002A9E8
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0002C8C4 File Offset: 0x0002AAC4
		public string DataViewSettingCollectionString
		{
			get
			{
				if (this._dataSet == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<DataViewSettingCollectionString>");
				foreach (object obj in this._dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataViewSetting dataViewSetting = this._dataViewSettingsCollection[dataTable];
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} Sort=\"{1}\" RowFilter=\"{2}\" RowStateFilter=\"{3}\"/>", new object[] { dataTable.EncodedTableName, dataViewSetting.Sort, dataViewSetting.RowFilter, dataViewSetting.RowStateFilter });
				}
				stringBuilder.Append("</DataViewSettingCollectionString>");
				return stringBuilder.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(value));
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
				xmlTextReader.Read();
				if (xmlTextReader.Name != "DataViewSettingCollectionString")
				{
					throw ExceptionBuilder.SetFailed("DataViewSettingCollectionString");
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.NodeType == XmlNodeType.Element)
					{
						string text = XmlConvert.DecodeName(xmlTextReader.LocalName);
						if (xmlTextReader.MoveToAttribute("Sort"))
						{
							this._dataViewSettingsCollection[text].Sort = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowFilter"))
						{
							this._dataViewSettingsCollection[text].RowFilter = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowStateFilter"))
						{
							this._dataViewSettingsCollection[text].RowStateFilter = (DataViewRowState)Enum.Parse(typeof(DataViewRowState), xmlTextReader.Value);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</returns>
		// Token: 0x060009AE RID: 2478 RVA: 0x0002C9B8 File Offset: 0x0002ABB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[1];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0000EF2B File Offset: 0x0000D12B
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000061D5 File Offset: 0x000043D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x060009B4 RID: 2484 RVA: 0x0002C9DA File Offset: 0x0002ABDA
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(new DataViewManagerListItemTypeDescriptor(this), index);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		// Token: 0x170001C3 RID: 451
		object IList.this[int index]
		{
			get
			{
				return this._item;
			}
			set
			{
				throw ExceptionBuilder.CannotModifyCollection();
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x060009B7 RID: 2487 RVA: 0x0002C9F1 File Offset: 0x0002ABF1
		int IList.Add(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x060009B8 RID: 2488 RVA: 0x0002C9F1 File Offset: 0x0002ABF1
		void IList.Clear()
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x060009B9 RID: 2489 RVA: 0x0002C9F8 File Offset: 0x0002ABF8
		bool IList.Contains(object value)
		{
			return value == this._item;
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x060009BA RID: 2490 RVA: 0x0002CA03 File Offset: 0x0002AC03
		int IList.IndexOf(object value)
		{
			if (value != this._item)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x060009BB RID: 2491 RVA: 0x0002C9F1 File Offset: 0x0002ABF1
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />. </param>
		// Token: 0x060009BC RID: 2492 RVA: 0x0002C9F1 File Offset: 0x0002ABF1
		void IList.Remove(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		// Token: 0x060009BD RID: 2493 RVA: 0x0002C9F1 File Offset: 0x0002ABF1
		void IList.RemoveAt(int index)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</returns>
		// Token: 0x060009BF RID: 2495 RVA: 0x0002CA11 File Offset: 0x0002AC11
		object IBindingList.AddNew()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002CA11 File Offset: 0x0002AC11
		bool IBindingList.IsSorted
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002CA11 File Offset: 0x0002AC11
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002CA11 File Offset: 0x0002AC11
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>Occurs after a row is added to or deleted from a <see cref="T:System.Data.DataView" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060009C8 RID: 2504 RVA: 0x0002CA18 File Offset: 0x0002AC18
		// (remove) Token: 0x060009C9 RID: 2505 RVA: 0x0002CA50 File Offset: 0x0002AC50
		public event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
		// Token: 0x060009CA RID: 2506 RVA: 0x00005E03 File Offset: 0x00004003
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
		// Token: 0x060009CB RID: 2507 RVA: 0x0002CA11 File Offset: 0x0002AC11
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the property parameter to search for.</param>
		// Token: 0x060009CC RID: 2508 RVA: 0x0002CA11 File Offset: 0x0002AC11
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
		// Token: 0x060009CD RID: 2509 RVA: 0x00005E03 File Offset: 0x00004003
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		// Token: 0x060009CE RID: 2510 RVA: 0x0002CA11 File Offset: 0x0002AC11
		void IBindingList.RemoveSort()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the name of the list.</summary>
		/// <returns>The name of the list.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be null. </param>
		// Token: 0x060009CF RID: 2511 RVA: 0x0002CA88 File Offset: 0x0002AC88
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return dataSet.DataSetName;
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.TableName;
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x060009D0 RID: 2512 RVA: 0x0002CACC File Offset: 0x0002ACCC
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return ((ICustomTypeDescriptor)new DataViewManagerListItemTypeDescriptor(this)).GetProperties();
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.GetPropertyDescriptorCollection(null);
			}
			return new PropertyDescriptorCollection(null);
		}

		/// <summary>Creates a <see cref="T:System.Data.DataView" /> for the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> object.</returns>
		/// <param name="table">The name of the <see cref="T:System.Data.DataTable" /> to use in the <see cref="T:System.Data.DataView" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060009D1 RID: 2513 RVA: 0x0002CB17 File Offset: 0x0002AD17
		public DataView CreateDataView(DataTable table)
		{
			if (this._dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this);
			return dataView;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataViewManager.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D2 RID: 2514 RVA: 0x0002CB34 File Offset: 0x0002AD34
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			try
			{
				ListChangedEventHandler listChanged = this.ListChanged;
				if (listChanged != null)
				{
					listChanged(this, e);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
			}
		}

		/// <summary>Raises a <see cref="E:System.Data.DataTableCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataTable" /> is added to or removed from the <see cref="T:System.Data.DataTableCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D3 RID: 2515 RVA: 0x0002CB88 File Offset: 0x0002AD88
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			PropertyDescriptor propertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataTablePropertyDescriptor((DataTable)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataTablePropertyDescriptor((DataTable)e.Element)) : null)));
		}

		/// <summary>Raises a <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataRelation" /> is added to or removed from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060009D4 RID: 2516 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x0400066A RID: 1642
		private DataViewSettingCollection _dataViewSettingsCollection;

		// Token: 0x0400066B RID: 1643
		private DataSet _dataSet;

		// Token: 0x0400066C RID: 1644
		private DataViewManagerListItemTypeDescriptor _item;

		// Token: 0x0400066D RID: 1645
		private bool _locked;

		// Token: 0x0400066E RID: 1646
		internal int _nViews;

		// Token: 0x0400066F RID: 1647
		private static NotSupportedException s_notSupported = new NotSupportedException();
	}
}
