using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a collection of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000333 RID: 819
	public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000061D5 File Offset: 0x000043D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000677 RID: 1655
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.IColumnMapping" /> object with the specified SourceColumn name.</summary>
		/// <returns>The IColumnMapping object with the specified SourceColumn name.</returns>
		/// <param name="index">Index of the element.</param>
		// Token: 0x17000678 RID: 1656
		object IColumnMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> by using the source column and <see cref="T:System.Data.DataSet" /> column names.</summary>
		/// <returns>The ColumnMapping object that was added to the collection.</returns>
		/// <param name="sourceColumnName">The case-sensitive name of the source column.</param>
		/// <param name="dataSetColumnName">The name of the <see cref="T:System.Data.DataSet" /> column.</param>
		// Token: 0x06002598 RID: 9624 RVA: 0x000AB29A File Offset: 0x000A949A
		IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
		{
			return this.Add(sourceColumnName, dataSetColumnName);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object that has the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object that  has the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
		/// <param name="dataSetColumnName">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find.</param>
		// Token: 0x06002599 RID: 9625 RVA: 0x000AB2A4 File Offset: 0x000A94A4
		IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
		{
			return this.GetByDataSetColumn(dataSetColumnName);
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataColumnMapping" /> objects in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x000AB2AD File Offset: 0x000A94AD
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000AB2C4 File Offset: 0x000A94C4
		private Type ItemType
		{
			get
			{
				return typeof(DataColumnMapping);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700067B RID: 1659
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnMapping this[int index]
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

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700067C RID: 1660
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnMapping this[string sourceColumn]
		{
			get
			{
				int num = this.RangeCheck(sourceColumn);
				return this._items[num];
			}
			set
			{
				int num = this.RangeCheck(sourceColumn);
				this.Replace(num, value);
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection.</summary>
		/// <returns>The index of the DataColumnMapping object that was added to the collection.</returns>
		/// <param name="value">A DataColumnMapping object to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025A0 RID: 9632 RVA: 0x000AB339 File Offset: 0x000A9539
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataColumnMapping)value);
			return this.Count - 1;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000AB357 File Offset: 0x000A9557
		private DataColumnMapping Add(DataColumnMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection when given a source column name and a <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The DataColumnMapping object that was added to the collection.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column to map to. </param>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to map to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025A2 RID: 9634 RVA: 0x000AB361 File Offset: 0x000A9561
		public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
		{
			return this.Add(new DataColumnMapping(sourceColumn, dataSetColumn));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025A3 RID: 9635 RVA: 0x000AB370 File Offset: 0x000A9570
		public void AddRange(DataColumnMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025A4 RID: 9636 RVA: 0x000AB370 File Offset: 0x000A9570
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000AB37C File Offset: 0x000A957C
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
						this.AddWithoutEvents(cloneable.Clone() as DataColumnMapping);
					}
					return;
				}
			}
			foreach (object obj3 in values)
			{
				DataColumnMapping dataColumnMapping = (DataColumnMapping)obj3;
				this.AddWithoutEvents(dataColumnMapping);
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000AB470 File Offset: 0x000A9670
		private void AddWithoutEvents(DataColumnMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000AB48D File Offset: 0x000A968D
		private List<DataColumnMapping> ArrayList()
		{
			if (this._items == null)
			{
				this._items = new List<DataColumnMapping>();
			}
			return this._items;
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataColumnMapping" /> objects from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060025A8 RID: 9640 RVA: 0x000AB4A8 File Offset: 0x000A96A8
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000AB4BC File Offset: 0x000A96BC
		private void ClearWithoutEvents()
		{
			if (this._items != null)
			{
				foreach (DataColumnMapping dataColumnMapping in this._items)
				{
					dataColumnMapping.Parent = null;
				}
				this._items.Clear();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given source column name exists in the collection.</summary>
		/// <returns>true if collection contains a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name; otherwise, false.</returns>
		/// <param name="value">The case-sensitive source column name of the <see cref="T:System.Data.Common.DataColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025AA RID: 9642 RVA: 0x000AB520 File Offset: 0x000A9720
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given <see cref="T:System.Object" /> exists in the collection.</summary>
		/// <returns>true if the collection contains the specified <see cref="T:System.Data.Common.DataColumnMapping" /> object; otherwise, false.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" />. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025AB RID: 9643 RVA: 0x000AB52F File Offset: 0x000A972F
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements. </param>
		/// <param name="index">The starting index of the array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060025AC RID: 9644 RVA: 0x000AB53E File Offset: 0x000A973E
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataColumnMapping" /> array to which to copy the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060025AD RID: 9645 RVA: 0x000AB54D File Offset: 0x000A974D
		public void CopyTo(DataColumnMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
		/// <param name="value">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025AE RID: 9646 RVA: 0x000AB55C File Offset: 0x000A975C
		public DataColumnMapping GetByDataSetColumn(string value)
		{
			int num = this.IndexOfDataSetColumn(value);
			if (0 > num)
			{
				throw ADP.ColumnsDataSetColumn(value);
			}
			return this._items[num];
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060025AF RID: 9647 RVA: 0x000AB588 File Offset: 0x000A9788
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B0 RID: 9648 RVA: 0x000AB59C File Offset: 0x000A979C
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

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified source column name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified case-sensitive source column name.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B1 RID: 9649 RVA: 0x000AB5D8 File Offset: 0x000A97D8
		public int IndexOf(string sourceColumn)
		{
			if (!string.IsNullOrEmpty(sourceColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.SrcCompare(sourceColumn, this._items[i].SourceColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given DataSet column name, or -1 if the DataColumnMapping object does not exist in the collection.</returns>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the data set column to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060025B2 RID: 9650 RVA: 0x000AB61C File Offset: 0x000A981C
		public int IndexOfDataSetColumn(string dataSetColumn)
		{
			if (!string.IsNullOrEmpty(dataSetColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.DstCompare(dataSetColumn, this._items[i].DataSetColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert. </param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B3 RID: 9651 RVA: 0x000AB660 File Offset: 0x000A9860
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataColumnMapping)value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B4 RID: 9652 RVA: 0x000AB676 File Offset: 0x000A9876
		public void Insert(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000AB6A2 File Offset: 0x000A98A2
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ColumnsIndexInt32(index, this);
			}
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000AB6B9 File Offset: 0x000A98B9
		private int RangeCheck(string sourceColumn)
		{
			int num = this.IndexOf(sourceColumn);
			if (num < 0)
			{
				throw ADP.ColumnsIndexSource(sourceColumn);
			}
			return num;
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B7 RID: 9655 RVA: 0x000AB6CD File Offset: 0x000A98CD
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name from the collection.</summary>
		/// <param name="sourceColumn">The case-sensitive source column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025B8 RID: 9656 RVA: 0x000AB6E0 File Offset: 0x000A98E0
		public void RemoveAt(string sourceColumn)
		{
			int num = this.RangeCheck(sourceColumn);
			this.RemoveIndex(num);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000AB6FC File Offset: 0x000A98FC
		private void RemoveIndex(int index)
		{
			this._items[index].Parent = null;
			this._items.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to remove. </param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025BA RID: 9658 RVA: 0x000AB71C File Offset: 0x000A991C
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataColumnMapping)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025BB RID: 9659 RVA: 0x000AB734 File Offset: 0x000A9934
		public void Remove(DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000AB76F File Offset: 0x000A996F
		private void Replace(int index, DataColumnMapping newValue)
		{
			this.Validate(index, newValue);
			this._items[index].Parent = null;
			newValue.Parent = this;
			this._items[index] = newValue;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000AB79F File Offset: 0x000A999F
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataColumnMapping(value);
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000AB7C4 File Offset: 0x000A99C4
		private void Validate(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.ColumnsIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ColumnsIsParent(this);
				}
			}
			string text = value.SourceColumn;
			if (string.IsNullOrEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceColumn" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceColumn = text;
				return;
			}
			this.ValidateSourceColumn(index, text);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000AB850 File Offset: 0x000A9A50
		internal void ValidateSourceColumn(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.ColumnsUniqueSourceColumn(value);
			}
		}

		/// <summary>A static method that returns a <see cref="T:System.Data.DataColumn" /> object without instantiating a <see cref="T:System.Data.Common.DataColumnMappingCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
		/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <param name="schemaAction">Determines the action to take when the existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025C0 RID: 9664 RVA: 0x000AB874 File Offset: 0x000A9A74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings._items[num].GetDataColumnBySchemaAction(dataTable, dataType, schemaAction);
				}
			}
			if (string.IsNullOrEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return DataColumnMapping.GetDataColumnBySchemaAction(sourceColumn, sourceColumn, dataTable, dataType, schemaAction);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> for the specified <see cref="T:System.Data.Common.DataColumnMappingCollection" />, source column name, and <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataColumnMapping" /> object.</returns>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />. </param>
		/// <param name="sourceColumn">The case-sensitive source column name to find. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060025C1 RID: 9665 RVA: 0x000AB8EC File Offset: 0x000A9AEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings._items[num];
				}
			}
			if (string.IsNullOrEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataColumnMapping(sourceColumn, sourceColumn);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x0400184B RID: 6219
		private List<DataColumnMapping> _items;
	}
}
