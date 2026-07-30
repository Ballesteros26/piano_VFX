using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents the default settings for <see cref="P:System.Data.DataView.ApplyDefaultSort" />, <see cref="P:System.Data.DataView.DataViewManager" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.RowStateFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="P:System.Data.DataView.Table" /> for DataViews created from the <see cref="T:System.Data.DataViewManager" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009F RID: 159
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0002CD03 File Offset: 0x0002AF03
		internal DataViewSetting()
		{
		}

		/// <summary>Gets or sets a value indicating whether to use the default sort.</summary>
		/// <returns>true if the default sort is used; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002CD29 File Offset: 0x0002AF29
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002CD31 File Offset: 0x0002AF31
		public bool ApplyDefaultSort
		{
			get
			{
				return this._applyDefaultSort;
			}
			set
			{
				if (this._applyDefaultSort != value)
				{
					this._applyDefaultSort = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> that contains this <see cref="T:System.Data.DataViewSetting" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002CD43 File Offset: 0x0002AF43
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this._dataViewManager;
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002CD4B File Offset: 0x0002AF4B
		internal void SetDataViewManager(DataViewManager dataViewManager)
		{
			if (this._dataViewManager != dataViewManager)
			{
				this._dataViewManager = dataViewManager;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the <see cref="T:System.Data.DataViewSetting" /> properties apply.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002CD5D File Offset: 0x0002AF5D
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002CD65 File Offset: 0x0002AF65
		internal void SetDataTable(DataTable table)
		{
			if (this._table != table)
			{
				this._table = table;
			}
		}

		/// <summary>Gets or sets the filter to apply in the <see cref="T:System.Data.DataView" />. See <see cref="P:System.Data.DataView.RowFilter" /> for a code sample using RowFilter.</summary>
		/// <returns>A string that contains the filter to apply.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002CD77 File Offset: 0x0002AF77
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0002CD7F File Offset: 0x0002AF7F
		public string RowFilter
		{
			get
			{
				return this._rowFilter;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._rowFilter != value)
				{
					this._rowFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to display Current, Deleted, Modified Current, ModifiedOriginal, New, Original, Unchanged, or no rows in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A value that indicates which rows to display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this._rowStateFilter;
			}
			set
			{
				if (this._rowStateFilter != value)
				{
					this._rowStateFilter = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating the sort to apply in the <see cref="T:System.Data.DataView" />. </summary>
		/// <returns>The sort to apply in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002CDBA File Offset: 0x0002AFBA
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0002CDC2 File Offset: 0x0002AFC2
		public string Sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this._sort != value)
				{
					this._sort = value;
				}
			}
		}

		// Token: 0x0400067C RID: 1660
		private DataViewManager _dataViewManager;

		// Token: 0x0400067D RID: 1661
		private DataTable _table;

		// Token: 0x0400067E RID: 1662
		private string _sort = string.Empty;

		// Token: 0x0400067F RID: 1663
		private string _rowFilter = string.Empty;

		// Token: 0x04000680 RID: 1664
		private DataViewRowState _rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x04000681 RID: 1665
		private bool _applyDefaultSort;
	}
}
