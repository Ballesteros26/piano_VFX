using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Data
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000A0 RID: 160
	public class DataViewSettingCollection : ICollection, IEnumerable
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0002CDE3 File Offset: 0x0002AFE3
		internal DataViewSettingCollection(DataViewManager dataViewManager)
		{
			this._list = new Hashtable();
			base..ctor();
			if (dataViewManager == null)
			{
				throw ExceptionBuilder.ArgumentNull("dataViewManager");
			}
			this._dataViewManager = dataViewManager;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection. </summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001D3 RID: 467
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				DataViewSetting dataViewSetting = (DataViewSetting)this._list[table];
				if (dataViewSetting == null)
				{
					dataViewSetting = new DataViewSetting();
					this[table] = dataViewSetting;
				}
				return dataViewSetting;
			}
			set
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				value.SetDataViewManager(this._dataViewManager);
				value.SetDataTable(table);
				this._list[table] = value;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002CE7C File Offset: 0x0002B07C
		private DataTable GetTable(string tableName)
		{
			DataTable dataTable = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				dataTable = dataSet.Tables[tableName];
			}
			return dataTable;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002CEA8 File Offset: 0x0002B0A8
		private DataTable GetTable(int index)
		{
			DataTable dataTable = null;
			DataSet dataSet = this._dataViewManager.DataSet;
			if (dataSet != null)
			{
				dataTable = dataSet.Tables[index];
			}
			return dataTable;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> of the <see cref="T:System.Data.DataTable" /> specified by its name.</summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001D4 RID: 468
		public virtual DataViewSetting this[string tableName]
		{
			get
			{
				DataTable table = this.GetTable(tableName);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the <see cref="T:System.Data.DataTable" /> specified by its index.</summary>
		/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001D5 RID: 469
		public virtual DataViewSetting this[int index]
		{
			get
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					return this[table];
				}
				return null;
			}
			set
			{
				DataTable table = this.GetTable(index);
				if (table != null)
				{
					this[table] = value;
				}
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FA RID: 2554 RVA: 0x0002CF3C File Offset: 0x0002B13C
		public void CopyTo(Array ar, int index)
		{
			foreach (object obj in this)
			{
				ar.SetValue(obj, index++);
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FB RID: 2555 RVA: 0x0002CF6C File Offset: 0x0002B16C
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			foreach (object obj in this)
			{
				ar.SetValue(obj, index++);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002CF9C File Offset: 0x0002B19C
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					return dataSet.Tables.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FD RID: 2557 RVA: 0x0002CFC5 File Offset: 0x0002B1C5
		public IEnumerator GetEnumerator()
		{
			return new DataViewSettingCollection.DataViewSettingsEnumerator(this._dataViewManager);
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataViewSettingCollection" /> is read-only.</summary>
		/// <returns>Returns true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0000EF2B File Offset: 0x0000D12B
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>This property is always false, unless overridden by a derived class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000061D5 File Offset: 0x000043D5
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00005D82 File Offset: 0x00003F82
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002CFD2 File Offset: 0x0002B1D2
		internal void Remove(DataTable table)
		{
			this._list.Remove(table);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00010468 File Offset: 0x0000E668
		internal DataViewSettingCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000682 RID: 1666
		private readonly DataViewManager _dataViewManager;

		// Token: 0x04000683 RID: 1667
		private readonly Hashtable _list;

		// Token: 0x020000A1 RID: 161
		private sealed class DataViewSettingsEnumerator : IEnumerator
		{
			// Token: 0x06000A03 RID: 2563 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
			public DataViewSettingsEnumerator(DataViewManager dvm)
			{
				if (dvm.DataSet != null)
				{
					this._dataViewSettings = dvm.DataViewSettings;
					this._tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
					return;
				}
				this._dataViewSettings = null;
				this._tableEnumerator = Array.Empty<DataTable>().GetEnumerator();
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x0002D035 File Offset: 0x0002B235
			public bool MoveNext()
			{
				return this._tableEnumerator.MoveNext();
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x0002D042 File Offset: 0x0002B242
			public void Reset()
			{
				this._tableEnumerator.Reset();
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002D04F File Offset: 0x0002B24F
			public object Current
			{
				get
				{
					return this._dataViewSettings[(DataTable)this._tableEnumerator.Current];
				}
			}

			// Token: 0x04000684 RID: 1668
			private DataViewSettingCollection _dataViewSettings;

			// Token: 0x04000685 RID: 1669
			private IEnumerator _tableEnumerator;
		}
	}
}
