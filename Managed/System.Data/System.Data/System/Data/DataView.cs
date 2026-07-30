using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a databindable, customized view of a <see cref="T:System.Data.DataTable" /> for sorting, filtering, searching, editing, and navigation. The <see cref="T:System.Data.DataView" /> does not store data, but instead represents a connected view of its corresponding <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataView" />’s data will affect the <see cref="T:System.Data.DataTable" />. Changes to the <see cref="T:System.Data.DataTable" />’s data will affect all <see cref="T:System.Data.DataView" />s associated with it.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000098 RID: 152
	[DefaultProperty("Table")]
	[DefaultEvent("PositionChanged")]
	public class DataView : MarshalByValueComponent, IBindingListView, IBindingList, IList, ICollection, IEnumerable, ITypedList, ISupportInitializeNotification, ISupportInitialize
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0002A3C0 File Offset: 0x000285C0
		internal DataView(DataTable table, bool locked)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, int, bool>("<ds.DataView.DataView|INFO> {0}, table={1}, locked={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, locked);
			this._dvListener = new DataViewListener(this);
			this._locked = locked;
			this._table = table;
			this._dvListener.RegisterMetaDataEvents(this._table);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class.</summary>
		// Token: 0x06000913 RID: 2323 RVA: 0x0002A48C File Offset: 0x0002868C
		public DataView()
			: this(null)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />. </param>
		// Token: 0x06000914 RID: 2324 RVA: 0x0002A4A4 File Offset: 0x000286A4
		public DataView(DataTable table)
			: this(table, false)
		{
			this.SetIndex2("", DataViewRowState.CurrentRows, null, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="T:System.Data.DataViewRowState" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="RowFilter">A <see cref="P:System.Data.DataView.RowFilter" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="Sort">A <see cref="P:System.Data.DataView.Sort" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="RowState">A <see cref="T:System.Data.DataViewRowState" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		// Token: 0x06000915 RID: 2325 RVA: 0x0002A4C0 File Offset: 0x000286C0
		public DataView(DataTable table, string RowFilter, string Sort, DataViewRowState RowState)
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int, int, string, string, DataViewRowState>("<ds.DataView.DataView|API> {0}, table={1}, RowFilter='{2}', Sort='{3}', RowState={4}", this.ObjectID, (table != null) ? table.ObjectID : 0, RowFilter, Sort, RowState);
			if (table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			this._dvListener = new DataViewListener(this);
			this._locked = false;
			this._table = table;
			this._dvListener.RegisterMetaDataEvents(this._table);
			if ((RowState & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
			{
				throw ExceptionBuilder.RecordStateRange();
			}
			if ((RowState & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (RowState & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
			{
				throw ExceptionBuilder.SetRowStateFilter();
			}
			if (Sort == null)
			{
				Sort = string.Empty;
			}
			if (RowFilter == null)
			{
				RowFilter = string.Empty;
			}
			DataExpression dataExpression = new DataExpression(table, RowFilter);
			this.SetIndex(Sort, RowState, dataExpression);
		}

		/// <summary>Sets or gets a value that indicates whether deletes are allowed.</summary>
		/// <returns>true, if deletes are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002A5DF File Offset: 0x000287DF
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0002A5E7 File Offset: 0x000287E7
		[DefaultValue(true)]
		public bool AllowDelete
		{
			get
			{
				return this._allowDelete;
			}
			set
			{
				if (this._allowDelete != value)
				{
					this._allowDelete = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether to use the default sort. The default sort is (ascending) by all primary keys as specified by <see cref="P:System.Data.DataTable.PrimaryKey" />.</summary>
		/// <returns>true, if the default sort is used; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002A604 File Offset: 0x00028804
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0002A60C File Offset: 0x0002880C
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, bool>("<ds.DataView.set_ApplyDefaultSort|API> {0}, {1}", this.ObjectID, value);
				if (this._applyDefaultSort != value)
				{
					this._comparison = null;
					this._applyDefaultSort = value;
					this.UpdateIndex(true);
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether edits are allowed.</summary>
		/// <returns>true, if edits are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0002A658 File Offset: 0x00028858
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0002A660 File Offset: 0x00028860
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return this._allowEdit;
			}
			set
			{
				if (this._allowEdit != value)
				{
					this._allowEdit = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the new rows can be added by using the <see cref="M:System.Data.DataView.AddNew" /> method.</summary>
		/// <returns>true, if new rows can be added; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002A67D File Offset: 0x0002887D
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0002A685 File Offset: 0x00028885
		[DefaultValue(true)]
		public bool AllowNew
		{
			get
			{
				return this._allowNew;
			}
			set
			{
				if (this._allowNew != value)
				{
					this._allowNew = value;
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>Gets the number of records in the <see cref="T:System.Data.DataView" /> after <see cref="P:System.Data.DataView.RowFilter" /> and <see cref="P:System.Data.DataView.RowStateFilter" /> have been applied.</summary>
		/// <returns>The number of records in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0002A6A2 File Offset: 0x000288A2
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this._rowViewCache.Count;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0002A6AF File Offset: 0x000288AF
		private int CountFromIndex
		{
			get
			{
				return ((this._index != null) ? this._index.RecordCount : 0) + ((this._addNewRow != null) ? 1 : 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> associated with this view.</summary>
		/// <returns>The DataViewManager that created this view. If this is the default <see cref="T:System.Data.DataView" /> for a <see cref="T:System.Data.DataTable" />, the DataViewManager property returns the default DataViewManager for the DataSet. Otherwise, if the DataView was created without a DataViewManager, this property is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002A6D4 File Offset: 0x000288D4
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		/// <summary>Gets a value that indicates whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002A6DC File Offset: 0x000288DC
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this._fInitInProgress;
			}
		}

		/// <summary>Gets a value that indicates whether the data source is currently open and projecting views of data on the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>true, if the source is open; otherwise, false.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002A6E7 File Offset: 0x000288E7
		[Browsable(false)]
		protected bool IsOpen
		{
			get
			{
				return this._open;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</returns>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000061D5 File Offset: 0x000043D5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the expression used to filter which rows are viewed in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that specifies how rows are to be filtered. For more information, see the Remarks section.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0002A6F0 File Offset: 0x000288F0
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0002A718 File Offset: 0x00028918
		[DefaultValue("")]
		public virtual string RowFilter
		{
			get
			{
				DataExpression dataExpression = this._rowFilter as DataExpression;
				if (dataExpression != null)
				{
					return dataExpression.Expression;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataView.set_RowFilter|API> {0}, '{1}'", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedRowFilter = value;
					return;
				}
				CultureInfo cultureInfo = ((this._table != null) ? this._table.Locale : CultureInfo.CurrentCulture);
				if (this._rowFilter == null || string.Compare(this.RowFilter, value, false, cultureInfo) != 0)
				{
					DataExpression dataExpression = new DataExpression(this._table, value);
					this.SetIndex(this._sort, this._recordStates, dataExpression);
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0002A7A8 File Offset: 0x000289A8
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0002A7CC File Offset: 0x000289CC
		internal Predicate<DataRow> RowPredicate
		{
			get
			{
				DataView.RowPredicateFilter rowPredicateFilter = this.GetFilter() as DataView.RowPredicateFilter;
				if (rowPredicateFilter == null)
				{
					return null;
				}
				return rowPredicateFilter._predicateFilter;
			}
			set
			{
				if (this.RowPredicate != value)
				{
					this.SetIndex(this.Sort, this.RowStateFilter, (value != null) ? new DataView.RowPredicateFilter(value) : null);
				}
			}
		}

		/// <summary>Gets or sets the row state filter used in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataViewRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002A7F5 File Offset: 0x000289F5
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0002A800 File Offset: 0x00028A00
		[DefaultValue(DataViewRowState.CurrentRows)]
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._recordStates;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, DataViewRowState>("<ds.DataView.set_RowStateFilter|API> {0}, {1}", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedRecordStates = value;
					return;
				}
				if ((value & ~(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal)) != DataViewRowState.None)
				{
					throw ExceptionBuilder.RecordStateRange();
				}
				if ((value & DataViewRowState.ModifiedOriginal) != DataViewRowState.None && (value & DataViewRowState.ModifiedCurrent) != DataViewRowState.None)
				{
					throw ExceptionBuilder.SetRowStateFilter();
				}
				if (this._recordStates != value)
				{
					this.SetIndex(this._sort, value, this._rowFilter);
				}
			}
		}

		/// <summary>Gets or sets the sort column or columns, and sort order for the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that contains the column name followed by "ASC" (ascending) or "DESC" (descending). Columns are sorted ascending by default. Multiple columns can be separated by commas.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002A870 File Offset: 0x00028A70
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		[DefaultValue("")]
		public string Sort
		{
			get
			{
				if (this._sort.Length == 0 && this._applyDefaultSort && this._table != null && this._table._primaryIndex.Length != 0)
				{
					return this._table.FormatSortString(this._table._primaryIndex);
				}
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataView.set_Sort|API> {0}, '{1}'", this.ObjectID, value);
				if (this._fInitInProgress)
				{
					this._delayedSort = value;
					return;
				}
				CultureInfo cultureInfo = ((this._table != null) ? this._table.Locale : CultureInfo.CurrentCulture);
				if (string.Compare(this._sort, value, false, cultureInfo) != 0 || this._comparison != null)
				{
					this.CheckSort(value);
					this._comparison = null;
					this.SetIndex(value, this._recordStates, this._rowFilter);
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002A959 File Offset: 0x00028B59
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0002A961 File Offset: 0x00028B61
		internal Comparison<DataRow> SortComparison
		{
			get
			{
				return this._comparison;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.set_SortComparison|API> {0}", this.ObjectID);
				if (this._comparison != value)
				{
					this._comparison = value;
					this.SetIndex("", this._recordStates, this._rowFilter);
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets or sets the source <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that provides the data for this view.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0002A99F File Offset: 0x00028B9F
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		[TypeConverter(typeof(DataTableTypeConverter))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, int>("<ds.DataView.set_Table|API> {0}, {1}", this.ObjectID, (value != null) ? value.ObjectID : 0);
				if (this._fInitInProgress && value != null)
				{
					this._delayedTable = value;
					return;
				}
				if (this._locked)
				{
					throw ExceptionBuilder.SetTable();
				}
				if (this._dataViewManager != null)
				{
					throw ExceptionBuilder.CanNotSetTable();
				}
				if (value != null && value.TableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotBindTable();
				}
				if (this._table != value)
				{
					this._dvListener.UnregisterMetaDataEvents();
					this._table = value;
					if (this._table != null)
					{
						this._dvListener.RegisterMetaDataEvents(this._table);
					}
					this.SetIndex2("", DataViewRowState.CurrentRows, null, false);
					if (this._table != null)
					{
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, new DataTablePropertyDescriptor(this._table)));
					}
					this.OnListChanged(DataView.s_resetEventArgs);
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</returns>
		/// <param name="recordIndex">A <see cref="System.Int32" /> value.</param>
		// Token: 0x170001A9 RID: 425
		object IList.this[int recordIndex]
		{
			get
			{
				return this[recordIndex];
			}
			set
			{
				throw ExceptionBuilder.SetIListObject();
			}
		}

		/// <summary>Gets a row of data from a specified table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowView" /> of the row that you want.</returns>
		/// <param name="recordIndex">The index of a record in the <see cref="T:System.Data.DataTable" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001AA RID: 426
		public DataRowView this[int recordIndex]
		{
			get
			{
				return this.GetRowView(this.GetRow(recordIndex));
			}
		}

		/// <summary>Adds a new row to the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataRowView" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000934 RID: 2356 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public virtual DataRowView AddNew()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataView.AddNew|API> {0}", this.ObjectID);
			DataRowView dataRowView2;
			try
			{
				this.CheckOpen();
				if (!this.AllowNew)
				{
					throw ExceptionBuilder.AddNewNotAllowNull();
				}
				if (this._addNewRow != null)
				{
					this._rowViewCache[this._addNewRow].EndEdit();
				}
				this._addNewRow = this._table.NewRow();
				DataRowView dataRowView = new DataRowView(this, this._addNewRow);
				this._rowViewCache.Add(this._addNewRow, dataRowView);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.IndexOf(dataRowView)));
				dataRowView2 = dataRowView;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataRowView2;
		}

		/// <summary>Starts the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000935 RID: 2357 RVA: 0x0002AB64 File Offset: 0x00028D64
		public void BeginInit()
		{
			this._fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000936 RID: 2358 RVA: 0x0002AB70 File Offset: 0x00028D70
		public void EndInit()
		{
			if (this._delayedTable != null && this._delayedTable.fInitInProgress)
			{
				this._delayedTable._delayedViews.Add(this);
				return;
			}
			this._fInitInProgress = false;
			this._fEndInitInProgress = true;
			if (this._delayedTable != null)
			{
				this.Table = this._delayedTable;
				this._delayedTable = null;
			}
			if (this._delayedSort != null)
			{
				this.Sort = this._delayedSort;
				this._delayedSort = null;
			}
			if (this._delayedRowFilter != null)
			{
				this.RowFilter = this._delayedRowFilter;
				this._delayedRowFilter = null;
			}
			if (this._delayedRecordStates != (DataViewRowState)(-1))
			{
				this.RowStateFilter = this._delayedRecordStates;
				this._delayedRecordStates = (DataViewRowState)(-1);
			}
			this._fEndInitInProgress = false;
			this.SetIndex(this.Sort, this.RowStateFilter, this._rowFilter);
			this.OnInitialized();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002AC44 File Offset: 0x00028E44
		private void CheckOpen()
		{
			if (!this.IsOpen)
			{
				throw ExceptionBuilder.NotOpen();
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002AC54 File Offset: 0x00028E54
		private void CheckSort(string sort)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (sort.Length == 0)
			{
				return;
			}
			this._table.ParseSortString(sort);
		}

		/// <summary>Closes the <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x06000939 RID: 2361 RVA: 0x0002AC7A File Offset: 0x00028E7A
		protected void Close()
		{
			this._shouldOpen = false;
			this.UpdateIndex();
			this._dvListener.UnregisterMetaDataEvents();
		}

		/// <summary>Copies items into an array. Only for Web Forms Interfaces.</summary>
		/// <param name="array">array to copy into. </param>
		/// <param name="index">index to start at. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600093A RID: 2362 RVA: 0x0002AC94 File Offset: 0x00028E94
		public void CopyTo(Array array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						array.SetValue(this.GetRowView(num), index);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array.SetValue(this._rowViewCache[this._addNewRow], index);
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002ACFC File Offset: 0x00028EFC
		private void CopyTo(DataRowView[] array, int index)
		{
			checked
			{
				if (this._index != null)
				{
					RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						array[index] = this.GetRowView(num);
						index++;
					}
				}
				if (this._addNewRow != null)
				{
					array[index] = this._rowViewCache[this._addNewRow];
				}
			}
		}

		/// <summary>Deletes a row at the specified index.</summary>
		/// <param name="index">The index of the row to delete. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600093C RID: 2364 RVA: 0x0002AD5A File Offset: 0x00028F5A
		public void Delete(int index)
		{
			this.Delete(this.GetRow(index));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002AD6C File Offset: 0x00028F6C
		internal void Delete(DataRow row)
		{
			if (row != null)
			{
				long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataView.Delete|API> {0}, row={1}", this.ObjectID, row._objectID);
				try
				{
					this.CheckOpen();
					if (row == this._addNewRow)
					{
						this.FinishAddNew(false);
					}
					else
					{
						if (!this.AllowDelete)
						{
							throw ExceptionBuilder.CanNotDelete();
						}
						row.Delete();
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600093E RID: 2366 RVA: 0x0002ADE4 File Offset: 0x00028FE4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key value.</summary>
		/// <returns>The index of the row in the <see cref="T:System.Data.DataView" /> that contains the sort key value specified; otherwise -1 if the sort key value does not exist.</returns>
		/// <param name="key">The object to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600093F RID: 2367 RVA: 0x0002ADF6 File Offset: 0x00028FF6
		public int Find(object key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002ADFF File Offset: 0x00028FFF
		internal virtual int FindByKey(object key)
		{
			return this._index.FindRecordByKey(key);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key values.</summary>
		/// <returns>The index of the position of the first row in the <see cref="T:System.Data.DataView" /> that matches the sort key values specified; otherwise -1 if there are no matching sort key values. </returns>
		/// <param name="key">An array of values, typed as <see cref="T:System.Object" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000941 RID: 2369 RVA: 0x0002AE0D File Offset: 0x0002900D
		public int Find(object[] key)
		{
			return this.FindByKey(key);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002AE16 File Offset: 0x00029016
		internal virtual int FindByKey(object[] key)
		{
			return this._index.FindRecordByKey(key);
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <returns>An array of DataRowView objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty DataRowView array.</returns>
		/// <param name="key">The column value, typed as <see cref="T:System.Object" />, to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000943 RID: 2371 RVA: 0x0002AE24 File Offset: 0x00029024
		public DataRowView[] FindRows(object key)
		{
			return this.FindRowsByKey(new object[] { key });
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <returns>An array of DataRowView objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty DataRowView array.</returns>
		/// <param name="key">An array of column values, typed as <see cref="T:System.Object" />, to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000944 RID: 2372 RVA: 0x0002AE36 File Offset: 0x00029036
		public DataRowView[] FindRows(object[] key)
		{
			return this.FindRowsByKey(key);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002AE40 File Offset: 0x00029040
		internal virtual DataRowView[] FindRowsByKey(object[] key)
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataView.FindRows|API> {0}", this.ObjectID);
			DataRowView[] dataRowViewFromRange;
			try
			{
				Range range = this._index.FindRecords(key);
				dataRowViewFromRange = this.GetDataRowViewFromRange(range);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return dataRowViewFromRange;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002AE98 File Offset: 0x00029098
		internal DataRowView[] GetDataRowViewFromRange(Range range)
		{
			if (range.IsNull)
			{
				return Array.Empty<DataRowView>();
			}
			DataRowView[] array = new DataRowView[range.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this[i + range.Min];
			}
			return array;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002AEE4 File Offset: 0x000290E4
		internal void FinishAddNew(bool success)
		{
			DataCommonEventSource.Log.Trace<int, bool>("<ds.DataView.FinishAddNew|INFO> {0}, success={1}", this.ObjectID, success);
			DataRow addNewRow = this._addNewRow;
			if (success)
			{
				if (DataRowState.Detached == addNewRow.RowState)
				{
					this._table.Rows.Add(addNewRow);
				}
				else
				{
					addNewRow.EndEdit();
				}
			}
			if (addNewRow == this._addNewRow)
			{
				this._rowViewCache.Remove(this._addNewRow);
				this._addNewRow = null;
				if (!success)
				{
					addNewRow.CancelEdit();
				}
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count));
			}
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000948 RID: 2376 RVA: 0x0002AF70 File Offset: 0x00029170
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000061D5 File Offset: 0x000043D5
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x0600094B RID: 2379 RVA: 0x0002AF97 File Offset: 0x00029197
		int IList.Add(object value)
		{
			if (value == null)
			{
				this.AddNew();
				return this.Count - 1;
			}
			throw ExceptionBuilder.AddExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x0600094C RID: 2380 RVA: 0x0002AFB1 File Offset: 0x000291B1
		void IList.Clear()
		{
			throw ExceptionBuilder.CanNotClear();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x0600094D RID: 2381 RVA: 0x0002AFB8 File Offset: 0x000291B8
		bool IList.Contains(object value)
		{
			return 0 <= this.IndexOf(value as DataRowView);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</returns>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x0600094E RID: 2382 RVA: 0x0002AFCC File Offset: 0x000291CC
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as DataRowView);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002AFDC File Offset: 0x000291DC
		internal int IndexOf(DataRowView rowview)
		{
			if (rowview != null)
			{
				if (this._addNewRow == rowview.Row)
				{
					return this.Count - 1;
				}
				DataRowView dataRowView;
				if (this._index != null && DataRowState.Detached != rowview.Row.RowState && this._rowViewCache.TryGetValue(rowview.Row, out dataRowView) && dataRowView == rowview)
				{
					return this.IndexOfDataRowView(rowview);
				}
			}
			return -1;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002B03B File Offset: 0x0002923B
		private int IndexOfDataRowView(DataRowView rowview)
		{
			return this._index.GetIndex(rowview.Row.GetRecordFromVersion(rowview.Row.GetDefaultRowVersion(this.RowStateFilter) & (DataRowVersion)(-1025)));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">A <see cref="System.Int32" /> value.</param>
		/// <param name="value">A <see cref="System.Object" /> value to be inserted.</param>
		// Token: 0x06000951 RID: 2385 RVA: 0x0002B06A File Offset: 0x0002926A
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.InsertExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">A <see cref="System.Object" /> value.</param>
		// Token: 0x06000952 RID: 2386 RVA: 0x0002B074 File Offset: 0x00029274
		void IList.Remove(object value)
		{
			int num = this.IndexOf(value as DataRowView);
			if (0 <= num)
			{
				((IList)this).RemoveAt(num);
				return;
			}
			throw ExceptionBuilder.RemoveExternalObject();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">A <see cref="System.Int32" /> value.</param>
		// Token: 0x06000953 RID: 2387 RVA: 0x0002B09F File Offset: 0x0002929F
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002B0A8 File Offset: 0x000292A8
		internal Index GetFindIndex(string column, bool keepIndex)
		{
			if (this._findIndexes == null)
			{
				this._findIndexes = new Dictionary<string, Index>();
			}
			Index index;
			if (this._findIndexes.TryGetValue(column, out index))
			{
				if (!keepIndex)
				{
					this._findIndexes.Remove(column);
					index.RemoveRef();
					if (index.RefCount == 1)
					{
						index.RemoveRef();
					}
				}
			}
			else if (keepIndex)
			{
				index = this._table.GetIndex(column, this._recordStates, this.GetFilter());
				this._findIndexes[column] = index;
				index.AddRef();
			}
			return index;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0002B131 File Offset: 0x00029331
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		// Token: 0x06000956 RID: 2390 RVA: 0x0002B139 File Offset: 0x00029339
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x0002B141 File Offset: 0x00029341
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0002B149 File Offset: 0x00029349
		bool IBindingList.AllowRemove
		{
			get
			{
				return this.AllowDelete;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002B151 File Offset: 0x00029351
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort.Length != 0;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0002B161 File Offset: 0x00029361
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				return this.GetSortProperty();
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002B169 File Offset: 0x00029369
		internal PropertyDescriptor GetSortProperty()
		{
			if (this._table != null && this._index != null && this._index._indexFields.Length == 1)
			{
				return new DataColumnPropertyDescriptor(this._index._indexFields[0].Column);
			}
			return null;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002B1A8 File Offset: 0x000293A8
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				if (this._index._indexFields.Length != 1 || !this._index._indexFields[0].IsDescending)
				{
					return ListSortDirection.Ascending;
				}
				return ListSortDirection.Descending;
			}
		}

		/// <summary>Occurs when the list managed by the <see cref="T:System.Data.DataView" /> changes.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000960 RID: 2400 RVA: 0x0002B1D5 File Offset: 0x000293D5
		// (remove) Token: 0x06000961 RID: 2401 RVA: 0x0002B203 File Offset: 0x00029403
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.add_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Combine(this._onListChanged, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataView.remove_ListChanged|API> {0}", this.ObjectID);
				this._onListChanged = (ListChangedEventHandler)Delegate.Remove(this._onListChanged, value);
			}
		}

		/// <summary>Occurs when initialization of the <see cref="T:System.Data.DataView" /> is completed.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000962 RID: 2402 RVA: 0x0002B234 File Offset: 0x00029434
		// (remove) Token: 0x06000963 RID: 2403 RVA: 0x0002B26C File Offset: 0x0002946C
		public event EventHandler Initialized;

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x06000964 RID: 2404 RVA: 0x0002B2A1 File Offset: 0x000294A1
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, true);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="direction">A <see cref="System.ComponentModel.ListSortDirection" /> object.</param>
		// Token: 0x06000965 RID: 2405 RVA: 0x0002B2B1 File Offset: 0x000294B1
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			this.Sort = this.CreateSortString(property, direction);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</returns>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		/// <param name="key">A <see cref="System.Object" /> value.</param>
		// Token: 0x06000966 RID: 2406 RVA: 0x0002B2C4 File Offset: 0x000294C4
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			if (property != null)
			{
				bool flag = false;
				Index index = null;
				try
				{
					if (this._findIndexes == null || !this._findIndexes.TryGetValue(property.Name, out index))
					{
						flag = true;
						index = this._table.GetIndex(property.Name, this._recordStates, this.GetFilter());
						index.AddRef();
					}
					Range range = index.FindRecords(key);
					if (!range.IsNull)
					{
						return this._index.GetIndex(index.GetRecord(range.Min));
					}
				}
				finally
				{
					if (flag && index != null)
					{
						index.RemoveRef();
						if (index.RefCount == 1)
						{
							index.RemoveRef();
						}
					}
				}
				return -1;
			}
			return -1;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		/// <param name="property">A <see cref="System.ComponentModel.PropertyDescriptor" /> object.</param>
		// Token: 0x06000967 RID: 2407 RVA: 0x0002B380 File Offset: 0x00029580
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			this.GetFindIndex(property.Name, false);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" />.</summary>
		// Token: 0x06000968 RID: 2408 RVA: 0x0002B390 File Offset: 0x00029590
		void IBindingList.RemoveSort()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataView.RemoveSort|API> {0}", this.ObjectID);
			this.Sort = string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" />.</summary>
		/// <param name="sorts">A <see cref="System.ComponentModel.ListSortDescriptionCollection" /> object.</param>
		// Token: 0x06000969 RID: 2409 RVA: 0x0002B3B4 File Offset: 0x000295B4
		void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
		{
			if (sorts == null)
			{
				throw ExceptionBuilder.ArgumentNull("sorts");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				if (listSortDescription == null)
				{
					throw ExceptionBuilder.ArgumentContainsNull("sorts");
				}
				PropertyDescriptor propertyDescriptor = listSortDescription.PropertyDescriptor;
				if (propertyDescriptor == null)
				{
					throw ExceptionBuilder.ArgumentNull("PropertyDescriptor");
				}
				if (!this._table.Columns.Contains(propertyDescriptor.Name))
				{
					throw ExceptionBuilder.ColumnToSortIsOutOfRange(propertyDescriptor.Name);
				}
				ListSortDirection sortDirection = listSortDescription.SortDirection;
				if (flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this.CreateSortString(propertyDescriptor, sortDirection));
				if (!flag)
				{
					flag = true;
				}
			}
			this.Sort = stringBuilder.ToString();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002B498 File Offset: 0x00029698
		private string CreateSortString(PropertyDescriptor property, ListSortDirection direction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(property.Name);
			stringBuilder.Append(']');
			if (ListSortDirection.Descending == direction)
			{
				stringBuilder.Append(" DESC");
			}
			return stringBuilder.ToString();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.RemoveFilter" />.</summary>
		// Token: 0x0600096B RID: 2411 RVA: 0x0002B4E0 File Offset: 0x000296E0
		void IBindingListView.RemoveFilter()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataView.RemoveFilter|API> {0}", this.ObjectID);
			this.RowFilter = string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.Filter" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.Filter" />.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002B502 File Offset: 0x00029702
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0002B50A File Offset: 0x0002970A
		string IBindingListView.Filter
		{
			get
			{
				return this.RowFilter;
			}
			set
			{
				this.RowFilter = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SortDescriptions" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SortDescriptions" />.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002B513 File Offset: 0x00029713
		ListSortDescriptionCollection IBindingListView.SortDescriptions
		{
			get
			{
				return this.GetSortDescriptions();
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002B51C File Offset: 0x0002971C
		internal ListSortDescriptionCollection GetSortDescriptions()
		{
			ListSortDescription[] array = Array.Empty<ListSortDescription>();
			if (this._table != null && this._index != null && this._index._indexFields.Length != 0)
			{
				array = new ListSortDescription[this._index._indexFields.Length];
				for (int i = 0; i < this._index._indexFields.Length; i++)
				{
					DataColumnPropertyDescriptor dataColumnPropertyDescriptor = new DataColumnPropertyDescriptor(this._index._indexFields[i].Column);
					if (this._index._indexFields[i].IsDescending)
					{
						array[i] = new ListSortDescription(dataColumnPropertyDescriptor, ListSortDirection.Descending);
					}
					else
					{
						array[i] = new ListSortDescription(dataColumnPropertyDescriptor, ListSortDirection.Ascending);
					}
				}
			}
			return new ListSortDescriptionCollection(array);
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsAdvancedSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsAdvancedSorting" />.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingListView.SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsFiltering" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsFiltering" />.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0000EF2B File Offset: 0x0000D12B
		bool IBindingListView.SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</returns>
		/// <param name="listAccessors">An array of <see cref="System.ComponentModel.PropertyDescriptor" /> objects.</param>
		// Token: 0x06000972 RID: 2418 RVA: 0x0002B5D0 File Offset: 0x000297D0
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.TableName;
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet != null)
				{
					DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
					if (dataTable != null)
					{
						return dataTable.TableName;
					}
				}
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x06000973 RID: 2419 RVA: 0x0002B628 File Offset: 0x00029828
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this._table != null)
			{
				if (listAccessors == null || listAccessors.Length == 0)
				{
					return this._table.GetPropertyDescriptorCollection(null);
				}
				DataSet dataSet = this._table.DataSet;
				if (dataSet == null)
				{
					return new PropertyDescriptorCollection(null);
				}
				DataTable dataTable = dataSet.FindTable(this._table, listAccessors, 0);
				if (dataTable != null)
				{
					return dataTable.GetPropertyDescriptorCollection(null);
				}
			}
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002B687 File Offset: 0x00029887
		internal virtual IFilter GetFilter()
		{
			return this._rowFilter;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002B68F File Offset: 0x0002988F
		private int GetRecord(int recordIndex)
		{
			if (this.Count <= recordIndex)
			{
				throw ExceptionBuilder.RowOutOfRange(recordIndex);
			}
			if (recordIndex != this._index.RecordCount)
			{
				return this._index.GetRecord(recordIndex);
			}
			return this._addNewRow.GetDefaultRecord();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002B6C8 File Offset: 0x000298C8
		internal DataRow GetRow(int index)
		{
			int count = this.Count;
			if (count <= index)
			{
				throw ExceptionBuilder.GetElementIndex(index);
			}
			if (index == count - 1 && this._addNewRow != null)
			{
				return this._addNewRow;
			}
			return this._table._recordManager[this.GetRecord(index)];
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0002B713 File Offset: 0x00029913
		private DataRowView GetRowView(int record)
		{
			return this.GetRowView(this._table._recordManager[record]);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002B72C File Offset: 0x0002992C
		private DataRowView GetRowView(DataRow dr)
		{
			return this._rowViewCache[dr];
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataView" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000979 RID: 2425 RVA: 0x0002B73A File Offset: 0x0002993A
		protected virtual void IndexListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType != ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
			if (this._addNewRow != null && this._index.RecordCount == 0)
			{
				this.FinishAddNew(false);
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				this.OnListChanged(e);
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002B778 File Offset: 0x00029978
		internal void IndexListChangedInternal(ListChangedEventArgs e)
		{
			this._rowViewBuffer.Clear();
			if (ListChangedType.ItemAdded == e.ListChangedType && this._addNewMoved != null && this._addNewMoved.NewIndex != this._addNewMoved.OldIndex)
			{
				ListChangedEventArgs addNewMoved = this._addNewMoved;
				this._addNewMoved = null;
				this.IndexListChanged(this, addNewMoved);
			}
			this.IndexListChanged(this, e);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0002B7D8 File Offset: 0x000299D8
		internal void MaintainDataView(ListChangedType changedType, DataRow row, bool trackAddRemove)
		{
			DataRowView dataRowView = null;
			switch (changedType)
			{
			case ListChangedType.Reset:
				this.ResetRowViewCache();
				break;
			case ListChangedType.ItemAdded:
				if (trackAddRemove && this._rowViewBuffer.TryGetValue(row, out dataRowView))
				{
					this._rowViewBuffer.Remove(row);
				}
				if (row == this._addNewRow)
				{
					int num = this.IndexOfDataRowView(this._rowViewCache[this._addNewRow]);
					this._addNewRow = null;
					this._addNewMoved = new ListChangedEventArgs(ListChangedType.ItemMoved, num, this.Count - 1);
					return;
				}
				if (!this._rowViewCache.ContainsKey(row))
				{
					this._rowViewCache.Add(row, dataRowView ?? new DataRowView(this, row));
					return;
				}
				break;
			case ListChangedType.ItemDeleted:
				if (trackAddRemove)
				{
					this._rowViewCache.TryGetValue(row, out dataRowView);
					if (dataRowView != null)
					{
						this._rowViewBuffer.Add(row, dataRowView);
					}
				}
				this._rowViewCache.Remove(row);
				return;
			case ListChangedType.ItemMoved:
			case ListChangedType.ItemChanged:
			case ListChangedType.PropertyDescriptorAdded:
			case ListChangedType.PropertyDescriptorDeleted:
			case ListChangedType.PropertyDescriptorChanged:
				break;
			default:
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataView.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x0600097C RID: 2428 RVA: 0x0002B8CC File Offset: 0x00029ACC
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			DataCommonEventSource.Log.Trace<int, ListChangedType>("<ds.DataView.OnListChanged|INFO> {0}, ListChangedType={1}", this.ObjectID, e.ListChangedType);
			try
			{
				DataColumn dataColumn = null;
				string text = null;
				switch (e.ListChangedType)
				{
				case ListChangedType.ItemMoved:
				case ListChangedType.ItemChanged:
					if (0 <= e.NewIndex)
					{
						DataRow row = this.GetRow(e.NewIndex);
						if (row.HasPropertyChanged)
						{
							dataColumn = row.LastChangedColumn;
							text = ((dataColumn != null) ? dataColumn.ColumnName : string.Empty);
						}
					}
					break;
				}
				if (this._onListChanged != null)
				{
					if (dataColumn != null && e.NewIndex == e.OldIndex)
					{
						ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(e.ListChangedType, e.NewIndex, new DataColumnPropertyDescriptor(dataColumn));
						this._onListChanged(this, listChangedEventArgs);
					}
					else
					{
						this._onListChanged(this, e);
					}
				}
				if (text != null)
				{
					this[e.NewIndex].RaisePropertyChangedEvent(text);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		private void OnInitialized()
		{
			EventHandler initialized = this.Initialized;
			if (initialized == null)
			{
				return;
			}
			initialized(this, EventArgs.Empty);
		}

		/// <summary>Opens a <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x0600097E RID: 2430 RVA: 0x0002BA10 File Offset: 0x00029C10
		protected void Open()
		{
			this._shouldOpen = true;
			this.UpdateIndex();
			this._dvListener.RegisterMetaDataEvents(this._table);
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x0600097F RID: 2431 RVA: 0x0002BA30 File Offset: 0x00029C30
		protected void Reset()
		{
			if (this.IsOpen)
			{
				this._index.Reset();
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002BA48 File Offset: 0x00029C48
		internal void ResetRowViewCache()
		{
			Dictionary<DataRow, DataRowView> dictionary = new Dictionary<DataRow, DataRowView>(this.CountFromIndex, DataView.DataRowReferenceComparer.s_default);
			if (this._index != null)
			{
				RBTree<int>.RBTreeEnumerator enumerator = this._index.GetEnumerator(0);
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					DataRow dataRow = this._table._recordManager[num];
					DataRowView dataRowView;
					if (!this._rowViewCache.TryGetValue(dataRow, out dataRowView))
					{
						dataRowView = new DataRowView(this, dataRow);
					}
					dictionary.Add(dataRow, dataRowView);
				}
			}
			if (this._addNewRow != null)
			{
				DataRowView dataRowView;
				this._rowViewCache.TryGetValue(this._addNewRow, out dataRowView);
				dictionary.Add(this._addNewRow, dataRowView);
			}
			this._rowViewCache = dictionary;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._table == null)
			{
				throw ExceptionBuilder.CanNotUse();
			}
			if (this._dataViewManager != dataViewManager)
			{
				if (dataViewManager != null)
				{
					dataViewManager._nViews--;
				}
				this._dataViewManager = dataViewManager;
				if (dataViewManager != null)
				{
					dataViewManager._nViews++;
					DataViewSetting dataViewSetting = dataViewManager.DataViewSettings[this._table];
					try
					{
						this._applyDefaultSort = dataViewSetting.ApplyDefaultSort;
						DataExpression dataExpression = new DataExpression(this._table, dataViewSetting.RowFilter);
						this.SetIndex(dataViewSetting.Sort, dataViewSetting.RowStateFilter, dataExpression);
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
					this._locked = true;
					return;
				}
				this.SetIndex("", DataViewRowState.CurrentRows, null);
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002BBD0 File Offset: 0x00029DD0
		internal virtual void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			this.SetIndex2(newSort, newRowStates, newRowFilter, true);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002BBDC File Offset: 0x00029DDC
		internal void SetIndex2(string newSort, DataViewRowState newRowStates, IFilter newRowFilter, bool fireEvent)
		{
			DataCommonEventSource.Log.Trace<int, string, DataViewRowState>("<ds.DataView.SetIndex|INFO> {0}, newSort='{1}', newRowStates={2}", this.ObjectID, newSort, newRowStates);
			this._sort = newSort;
			this._recordStates = newRowStates;
			this._rowFilter = newRowFilter;
			if (this._fEndInitInProgress)
			{
				return;
			}
			if (fireEvent)
			{
				this.UpdateIndex(true);
			}
			else
			{
				this.UpdateIndex(true, false);
			}
			if (this._findIndexes != null)
			{
				Dictionary<string, Index> findIndexes = this._findIndexes;
				this._findIndexes = null;
				foreach (KeyValuePair<string, Index> keyValuePair in findIndexes)
				{
					keyValuePair.Value.RemoveRef();
				}
			}
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x06000984 RID: 2436 RVA: 0x0002BC90 File Offset: 0x00029E90
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		/// <summary>Reserved for internal use only.</summary>
		/// <param name="force">Reserved for internal use only. </param>
		// Token: 0x06000985 RID: 2437 RVA: 0x0002BC99 File Offset: 0x00029E99
		protected virtual void UpdateIndex(bool force)
		{
			this.UpdateIndex(force, true);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0002BCA4 File Offset: 0x00029EA4
		internal void UpdateIndex(bool force, bool fireEvent)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataView.UpdateIndex|INFO> {0}, force={1}", this.ObjectID, force);
			try
			{
				if (this._open != this._shouldOpen || force)
				{
					this._open = this._shouldOpen;
					Index index = null;
					if (this._open && this._table != null)
					{
						if (this.SortComparison != null)
						{
							index = new Index(this._table, this.SortComparison, this._recordStates, this.GetFilter());
							index.AddRef();
						}
						else
						{
							index = this._table.GetIndex(this.Sort, this._recordStates, this.GetFilter());
						}
					}
					if (this._index != index)
					{
						if (this._index == null)
						{
							DataTable table = index.Table;
						}
						else
						{
							DataTable table2 = this._index.Table;
						}
						if (this._index != null)
						{
							this._dvListener.UnregisterListChangedEvent();
						}
						this._index = index;
						if (this._index != null)
						{
							this._dvListener.RegisterListChangedEvent(this._index);
						}
						this.ResetRowViewCache();
						if (fireEvent)
						{
							this.OnListChanged(DataView.s_resetEventArgs);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		internal void ChildRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002BE44 File Offset: 0x0002A044
		internal void ParentRelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor dataRelationPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataRelationPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataColumnCollection" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000989 RID: 2441 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataColumnPropertyDescriptor dataColumnPropertyDescriptor = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, dataColumnPropertyDescriptor) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataColumnPropertyDescriptor((DataColumn)e.Element)) : null)));
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002BF1A File Offset: 0x0002A11A
		internal void ColumnCollectionChangedInternal(object sender, CollectionChangeEventArgs e)
		{
			this.ColumnCollectionChanged(sender, e);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600098B RID: 2443 RVA: 0x0002BF24 File Offset: 0x0002A124
		public DataTable ToTable()
		{
			return this.ToTable(null, false, Array.Empty<string>());
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600098C RID: 2444 RVA: 0x0002BF33 File Offset: 0x0002A133
		public DataTable ToTable(string tableName)
		{
			return this.ToTable(tableName, false, Array.Empty<string>());
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="distinct">If true, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is false.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> contains the specified columns in the order they appear within this array.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600098D RID: 2445 RVA: 0x0002BF42 File Offset: 0x0002A142
		public DataTable ToTable(bool distinct, params string[] columnNames)
		{
			return this.ToTable(null, distinct, columnNames);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="distinct">If true, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is false.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The DataTable contains the specified columns in the order they appear within this array.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600098E RID: 2446 RVA: 0x0002BF50 File Offset: 0x0002A150
		public DataTable ToTable(string tableName, bool distinct, params string[] columnNames)
		{
			DataCommonEventSource.Log.Trace<int, string, bool>("<ds.DataView.ToTable|API> {0}, TableName='{1}', distinct={2}", this.ObjectID, tableName, distinct);
			if (columnNames == null)
			{
				throw ExceptionBuilder.ArgumentNull("columnNames");
			}
			DataTable dataTable = new DataTable();
			dataTable.Locale = this._table.Locale;
			dataTable.CaseSensitive = this._table.CaseSensitive;
			dataTable.TableName = ((tableName != null) ? tableName : this._table.TableName);
			dataTable.Namespace = this._table.Namespace;
			dataTable.Prefix = this._table.Prefix;
			if (columnNames.Length == 0)
			{
				columnNames = new string[this.Table.Columns.Count];
				for (int i = 0; i < columnNames.Length; i++)
				{
					columnNames[i] = this.Table.Columns[i].ColumnName;
				}
			}
			int[] array = new int[columnNames.Length];
			List<object[]> list = new List<object[]>();
			for (int j = 0; j < columnNames.Length; j++)
			{
				DataColumn dataColumn = this.Table.Columns[columnNames[j]];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.ColumnNotInTheUnderlyingTable(columnNames[j], this.Table.TableName);
				}
				dataTable.Columns.Add(dataColumn.Clone());
				array[j] = this.Table.Columns.IndexOf(dataColumn);
			}
			foreach (object obj in this)
			{
				DataRowView dataRowView = (DataRowView)obj;
				object[] array2 = new object[columnNames.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array2[k] = dataRowView[array[k]];
				}
				if (!distinct || !this.RowExist(list, array2))
				{
					dataTable.Rows.Add(array2);
					list.Add(array2);
				}
			}
			return dataTable;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002C138 File Offset: 0x0002A338
		private bool RowExist(List<object[]> arraylist, object[] objectArray)
		{
			for (int i = 0; i < arraylist.Count; i++)
			{
				object[] array = arraylist[i];
				bool flag = true;
				for (int j = 0; j < objectArray.Length; j++)
				{
					flag &= array[j].Equals(objectArray[j]);
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.DataView" /> instances are considered equal. </summary>
		/// <returns>true if the two <see cref="T:System.Data.DataView" /> instances are equal; otherwise, false. </returns>
		/// <param name="view">The <see cref="T:System.Data.DataView" /> to be compared.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000990 RID: 2448 RVA: 0x0002C184 File Offset: 0x0002A384
		public virtual bool Equals(DataView view)
		{
			return view != null && this.Table == view.Table && this.Count == view.Count && string.Equals(this.RowFilter, view.RowFilter, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Sort, view.Sort, StringComparison.OrdinalIgnoreCase) && this.SortComparison == view.SortComparison && this.RowPredicate == view.RowPredicate && this.RowStateFilter == view.RowStateFilter && this.DataViewManager == view.DataViewManager && this.AllowDelete == view.AllowDelete && this.AllowNew == view.AllowNew && this.AllowEdit == view.AllowEdit;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0002C246 File Offset: 0x0002A446
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x04000645 RID: 1605
		private DataViewManager _dataViewManager;

		// Token: 0x04000646 RID: 1606
		private DataTable _table;

		// Token: 0x04000647 RID: 1607
		private bool _locked;

		// Token: 0x04000648 RID: 1608
		private Index _index;

		// Token: 0x04000649 RID: 1609
		private Dictionary<string, Index> _findIndexes;

		// Token: 0x0400064A RID: 1610
		private string _sort = string.Empty;

		// Token: 0x0400064B RID: 1611
		private Comparison<DataRow> _comparison;

		// Token: 0x0400064C RID: 1612
		private IFilter _rowFilter;

		// Token: 0x0400064D RID: 1613
		private DataViewRowState _recordStates = DataViewRowState.CurrentRows;

		// Token: 0x0400064E RID: 1614
		private bool _shouldOpen = true;

		// Token: 0x0400064F RID: 1615
		private bool _open;

		// Token: 0x04000650 RID: 1616
		private bool _allowNew = true;

		// Token: 0x04000651 RID: 1617
		private bool _allowEdit = true;

		// Token: 0x04000652 RID: 1618
		private bool _allowDelete = true;

		// Token: 0x04000653 RID: 1619
		private bool _applyDefaultSort;

		// Token: 0x04000654 RID: 1620
		internal DataRow _addNewRow;

		// Token: 0x04000655 RID: 1621
		private ListChangedEventArgs _addNewMoved;

		// Token: 0x04000656 RID: 1622
		private ListChangedEventHandler _onListChanged;

		// Token: 0x04000657 RID: 1623
		internal static ListChangedEventArgs s_resetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1);

		// Token: 0x04000658 RID: 1624
		private DataTable _delayedTable;

		// Token: 0x04000659 RID: 1625
		private string _delayedRowFilter;

		// Token: 0x0400065A RID: 1626
		private string _delayedSort;

		// Token: 0x0400065B RID: 1627
		private DataViewRowState _delayedRecordStates = (DataViewRowState)(-1);

		// Token: 0x0400065C RID: 1628
		private bool _fInitInProgress;

		// Token: 0x0400065D RID: 1629
		private bool _fEndInitInProgress;

		// Token: 0x0400065E RID: 1630
		private Dictionary<DataRow, DataRowView> _rowViewCache = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x0400065F RID: 1631
		private readonly Dictionary<DataRow, DataRowView> _rowViewBuffer = new Dictionary<DataRow, DataRowView>(DataView.DataRowReferenceComparer.s_default);

		// Token: 0x04000660 RID: 1632
		private DataViewListener _dvListener;

		// Token: 0x04000661 RID: 1633
		private static int s_objectTypeCount;

		// Token: 0x04000662 RID: 1634
		private readonly int _objectID = Interlocked.Increment(ref DataView.s_objectTypeCount);

		// Token: 0x02000099 RID: 153
		private sealed class DataRowReferenceComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x06000993 RID: 2451 RVA: 0x00005C14 File Offset: 0x00003E14
			private DataRowReferenceComparer()
			{
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0002C25C File Offset: 0x0002A45C
			public bool Equals(DataRow x, DataRow y)
			{
				return x == y;
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x0002C262 File Offset: 0x0002A462
			public int GetHashCode(DataRow obj)
			{
				return obj._objectID;
			}

			// Token: 0x04000664 RID: 1636
			internal static readonly DataView.DataRowReferenceComparer s_default = new DataView.DataRowReferenceComparer();
		}

		// Token: 0x0200009A RID: 154
		private sealed class RowPredicateFilter : IFilter
		{
			// Token: 0x06000997 RID: 2455 RVA: 0x0002C276 File Offset: 0x0002A476
			internal RowPredicateFilter(Predicate<DataRow> predicate)
			{
				this._predicateFilter = predicate;
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0002C285 File Offset: 0x0002A485
			bool IFilter.Invoke(DataRow row, DataRowVersion version)
			{
				return this._predicateFilter(row);
			}

			// Token: 0x04000665 RID: 1637
			internal readonly Predicate<DataRow> _predicateFilter;
		}
	}
}
