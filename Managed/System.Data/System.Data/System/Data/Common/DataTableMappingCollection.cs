using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>A collection of <see cref="T:System.Data.Common.DataTableMapping" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033A RID: 826
	[ListBindable(false)]
	public sealed class DataTableMappingCollection : MarshalByRefObject, ITableMappingCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000061D5 File Offset: 0x000043D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		// Token: 0x1700068B RID: 1675
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		/// <summary>Gets or sets the instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name.</summary>
		/// <returns>The instance of <see cref="T:System.Data.ITableMapping" /> with the specified SourceTable name.</returns>
		/// <param name="index">The SourceTable name of the <see cref="T:System.Data.ITableMapping" />.</param>
		// Token: 0x1700068C RID: 1676
		object ITableMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		/// <summary>Adds a table mapping to the collection.</summary>
		/// <returns>A reference to the newly-mapped <see cref="T:System.Data.ITableMapping" /> object.</returns>
		/// <param name="sourceTableName">The case-sensitive name of the source table.</param>
		/// <param name="dataSetTableName">The name of the <see cref="T:System.Data.DataSet" /> table.</param>
		// Token: 0x06002633 RID: 9779 RVA: 0x000AC99B File Offset: 0x000AAB9B
		ITableMapping ITableMappingCollection.Add(string sourceTableName, string dataSetTableName)
		{
			return this.Add(sourceTableName, dataSetTableName);
		}

		/// <summary>Gets the TableMapping object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The TableMapping object with the specified DataSet table name.</returns>
		/// <param name="dataSetTableName">The name of the DataSet table within the collection.</param>
		// Token: 0x06002634 RID: 9780 RVA: 0x000AC9A5 File Offset: 0x000AABA5
		ITableMapping ITableMappingCollection.GetByDataSetTable(string dataSetTableName)
		{
			return this.GetByDataSetTable(dataSetTableName);
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataTableMapping" /> objects in the collection.</summary>
		/// <returns>The number of DataTableMapping objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000AC9AE File Offset: 0x000AABAE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000AC9C5 File Offset: 0x000AABC5
		private Type ItemType
		{
			get
			{
				return typeof(DataTableMapping);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700068F RID: 1679
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTableMapping this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return this._items[index];
			}
			set
			{
				this.RangeCheck(index);
				this.Replace(index, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000690 RID: 1680
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTableMapping this[string sourceTable]
		{
			get
			{
				int num = this.RangeCheck(sourceTable);
				return this._items[num];
			}
			set
			{
				int num = this.RangeCheck(sourceTable);
				this.Replace(num, value);
			}
		}

		/// <summary>Adds an <see cref="T:System.Object" /> that is a table mapping to the collection.</summary>
		/// <returns>The index of the DataTableMapping object added to the collection.</returns>
		/// <param name="value">A DataTableMapping object to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataTableMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600263B RID: 9787 RVA: 0x000ACA39 File Offset: 0x000AAC39
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataTableMapping)value);
			return this.Count - 1;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000ACA57 File Offset: 0x000AAC57
		private DataTableMapping Add(DataTableMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataTableMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataTableMapping" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600263D RID: 9789 RVA: 0x000ACA61 File Offset: 0x000AAC61
		public void AddRange(DataTableMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">An <see cref="T:System.Array" /> of values to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600263E RID: 9790 RVA: 0x000ACA61 File Offset: 0x000AAC61
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000ACA6C File Offset: 0x000AAC6C
		private void AddEnumerableRange(IEnumerable values, bool doClone)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object obj in values)
			{
				this.ValidateType(obj);
			}
			if (doClone)
			{
				using (IEnumerator enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						ICloneable cloneable = (ICloneable)obj2;
						this.AddWithoutEvents(cloneable.Clone() as DataTableMapping);
					}
					return;
				}
			}
			foreach (object obj3 in values)
			{
				DataTableMapping dataTableMapping = (DataTableMapping)obj3;
				this.AddWithoutEvents(dataTableMapping);
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataTableMapping" /> object to the collection when given a source table name and a <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object that was added to the collection.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table to map from. </param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to map to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002640 RID: 9792 RVA: 0x000ACB60 File Offset: 0x000AAD60
		public DataTableMapping Add(string sourceTable, string dataSetTable)
		{
			return this.Add(new DataTableMapping(sourceTable, dataSetTable));
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000ACB6F File Offset: 0x000AAD6F
		private void AddWithoutEvents(DataTableMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000ACB8C File Offset: 0x000AAD8C
		private List<DataTableMapping> ArrayList()
		{
			List<DataTableMapping> list;
			if ((list = this._items) == null)
			{
				list = (this._items = new List<DataTableMapping>());
			}
			return list;
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataTableMapping" /> objects from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002643 RID: 9795 RVA: 0x000ACBB1 File Offset: 0x000AADB1
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x000ACBC4 File Offset: 0x000AADC4
		private void ClearWithoutEvents()
		{
			if (this._items != null)
			{
				foreach (DataTableMapping dataTableMapping in this._items)
				{
					dataTableMapping.Parent = null;
				}
				this._items.Clear();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Data.Common.DataTableMapping" /> object with this source table name; otherwise false.</returns>
		/// <param name="value">The case-sensitive source table name containing the <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002645 RID: 9797 RVA: 0x000ACC28 File Offset: 0x000AAE28
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the given <see cref="T:System.Data.Common.DataTableMapping" /> object exists in the collection.</summary>
		/// <returns>true if this collection contains the specified <see cref="T:System.Data.Common.DataTableMapping" />; otherwise false.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002646 RID: 9798 RVA: 0x000ACC37 File Offset: 0x000AAE37
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements. </param>
		/// <param name="index">The starting index of the array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002647 RID: 9799 RVA: 0x000ACC46 File Offset: 0x000AAE46
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMapping" /> to the specified array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataTableMapping" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements.</param>
		/// <param name="index">The starting index of the array.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002648 RID: 9800 RVA: 0x000ACC55 File Offset: 0x000AAE55
		public void CopyTo(DataTableMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</returns>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002649 RID: 9801 RVA: 0x000ACC64 File Offset: 0x000AAE64
		public DataTableMapping GetByDataSetTable(string dataSetTable)
		{
			int num = this.IndexOfDataSetTable(dataSetTable);
			if (0 > num)
			{
				throw ADP.TablesDataSetTable(dataSetTable);
			}
			return this._items[num];
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600264A RID: 9802 RVA: 0x000ACC90 File Offset: 0x000AAE90
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" /> object to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600264B RID: 9803 RVA: 0x000ACCA4 File Offset: 0x000AAEA4
		public int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				for (int i = 0; i < this.Count; i++)
				{
					if (this._items[i] == value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600264C RID: 9804 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
		public int IndexOf(string sourceTable)
		{
			if (!string.IsNullOrEmpty(sourceTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string sourceTable2 = this._items[i].SourceTable;
					if (sourceTable2 != null && ADP.SrcCompare(sourceTable, sourceTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the given <see cref="T:System.Data.DataSet" /> table name, or -1 if the <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist in the collection.</returns>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the DataSet table to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600264D RID: 9805 RVA: 0x000ACD28 File Offset: 0x000AAF28
		public int IndexOfDataSetTable(string dataSetTable)
		{
			if (!string.IsNullOrEmpty(dataSetTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string dataSetTable2 = this._items[i].DataSetTable;
					if (dataSetTable2 != null && ADP.DstCompare(dataSetTable, dataSetTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert. </param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600264E RID: 9806 RVA: 0x000ACD6F File Offset: 0x000AAF6F
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataTableMapping)value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600264F RID: 9807 RVA: 0x000ACD85 File Offset: 0x000AAF85
		public void Insert(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000ACDB1 File Offset: 0x000AAFB1
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.TablesIndexInt32(index, this);
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000ACDC8 File Offset: 0x000AAFC8
		private int RangeCheck(string sourceTable)
		{
			int num = this.IndexOf(sourceTable);
			if (num < 0)
			{
				throw ADP.TablesSourceIndex(sourceTable);
			}
			return num;
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object located at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002652 RID: 9810 RVA: 0x000ACDDC File Offset: 0x000AAFDC
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name from the collection.</summary>
		/// <param name="sourceTable">The case-sensitive source table name to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified source table name. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002653 RID: 9811 RVA: 0x000ACDEC File Offset: 0x000AAFEC
		public void RemoveAt(string sourceTable)
		{
			int num = this.RangeCheck(sourceTable);
			this.RemoveIndex(num);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000ACE08 File Offset: 0x000AB008
		private void RemoveIndex(int index)
		{
			this._items[index].Parent = null;
			this._items.RemoveAt(index);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove. </param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataTableMapping" /> object. </exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002655 RID: 9813 RVA: 0x000ACE28 File Offset: 0x000AB028
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataTableMapping)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002656 RID: 9814 RVA: 0x000ACE40 File Offset: 0x000AB040
		public void Remove(DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000ACE7B File Offset: 0x000AB07B
		private void Replace(int index, DataTableMapping newValue)
		{
			this.Validate(index, newValue);
			this._items[index].Parent = null;
			newValue.Parent = this;
			this._items[index] = newValue;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000ACEAB File Offset: 0x000AB0AB
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataTableMapping(value);
			}
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000ACED0 File Offset: 0x000AB0D0
		private void Validate(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.TablesIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.TablesIsParent(this);
				}
			}
			string text = value.SourceTable;
			if (string.IsNullOrEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceTable" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceTable = text;
				return;
			}
			this.ValidateSourceTable(index, text);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000ACF5C File Offset: 0x000AB15C
		internal void ValidateSourceTable(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.TablesUniqueSourceTable(value);
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source table name and <see cref="T:System.Data.DataSet" /> table name, using the given <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataTableMapping" /> object.</returns>
		/// <param name="tableMappings">The <see cref="T:System.Data.Common.DataTableMappingCollection" /> collection to search. </param>
		/// <param name="sourceTable">The case-sensitive name of the mapped source table. </param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the mapped <see cref="T:System.Data.DataSet" /> table. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600265B RID: 9819 RVA: 0x000ACF80 File Offset: 0x000AB180
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataTableMapping GetTableMappingBySchemaAction(DataTableMappingCollection tableMappings, string sourceTable, string dataSetTable, MissingMappingAction mappingAction)
		{
			if (tableMappings != null)
			{
				int num = tableMappings.IndexOf(sourceTable);
				if (-1 != num)
				{
					return tableMappings._items[num];
				}
			}
			if (string.IsNullOrEmpty(sourceTable))
			{
				throw ADP.InvalidSourceTable("sourceTable");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataTableMapping(sourceTable, dataSetTable);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingTableMapping(sourceTable);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x0400188F RID: 6287
		private List<DataTableMapping> _items;
	}
}
