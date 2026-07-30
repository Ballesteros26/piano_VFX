using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000096 RID: 150
	internal sealed class DataTableReaderListener
	{
		// Token: 0x06000908 RID: 2312 RVA: 0x0002A154 File Offset: 0x00028354
		internal DataTableReaderListener(DataTableReader reader)
		{
			if (reader == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTableReader");
			}
			if (this._currentDataTable != null)
			{
				this.UnSubscribeEvents();
			}
			this._readerWeak = new WeakReference(reader);
			this._currentDataTable = reader.CurrentDataTable;
			if (this._currentDataTable != null)
			{
				this.SubscribeEvents();
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002A1A9 File Offset: 0x000283A9
		internal void CleanUp()
		{
			this.UnSubscribeEvents();
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002A1B1 File Offset: 0x000283B1
		internal void UpdataTable(DataTable datatable)
		{
			if (datatable == null)
			{
				throw ExceptionBuilder.ArgumentNull("DataTable");
			}
			this.UnSubscribeEvents();
			this._currentDataTable = datatable;
			this.SubscribeEvents();
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002A1D4 File Offset: 0x000283D4
		private void SubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged += this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged += this.SchemaChanged;
			this._currentDataTable.RowChanged += this.DataChanged;
			this._currentDataTable.RowDeleted += this.DataChanged;
			this._currentDataTable.TableCleared += this.DataTableCleared;
			this._isSubscribed = true;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002A278 File Offset: 0x00028478
		private void UnSubscribeEvents()
		{
			if (this._currentDataTable == null)
			{
				return;
			}
			if (!this._isSubscribed)
			{
				return;
			}
			this._currentDataTable.Columns.ColumnPropertyChanged -= this.SchemaChanged;
			this._currentDataTable.Columns.CollectionChanged -= this.SchemaChanged;
			this._currentDataTable.RowChanged -= this.DataChanged;
			this._currentDataTable.RowDeleted -= this.DataChanged;
			this._currentDataTable.TableCleared -= this.DataTableCleared;
			this._isSubscribed = false;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002A31C File Offset: 0x0002851C
		private void DataTableCleared(object sender, DataTableClearEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataTableCleared();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002A34C File Offset: 0x0002854C
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.SchemaChanged();
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002A37C File Offset: 0x0002857C
		private void DataChanged(object sender, DataRowChangeEventArgs args)
		{
			DataTableReader dataTableReader = (DataTableReader)this._readerWeak.Target;
			if (dataTableReader != null)
			{
				dataTableReader.DataChanged(args);
				return;
			}
			this.UnSubscribeEvents();
		}

		// Token: 0x04000642 RID: 1602
		private DataTable _currentDataTable;

		// Token: 0x04000643 RID: 1603
		private bool _isSubscribed;

		// Token: 0x04000644 RID: 1604
		private WeakReference _readerWeak;
	}
}
