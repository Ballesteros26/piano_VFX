using System;

namespace System.Data
{
	// Token: 0x02000075 RID: 117
	internal struct DataKey
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x000159D0 File Offset: 0x00013BD0
		internal DataKey(DataColumn[] columns, bool copyColumns)
		{
			if (columns == null)
			{
				throw ExceptionBuilder.ArgumentNull("columns");
			}
			if (columns.Length == 0)
			{
				throw ExceptionBuilder.KeyNoColumns();
			}
			if (columns.Length > 32)
			{
				throw ExceptionBuilder.KeyTooManyColumns(32);
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i] == null)
				{
					throw ExceptionBuilder.ArgumentNull("column");
				}
			}
			for (int j = 0; j < columns.Length; j++)
			{
				for (int k = 0; k < j; k++)
				{
					if (columns[j] == columns[k])
					{
						throw ExceptionBuilder.KeyDuplicateColumns(columns[j].ColumnName);
					}
				}
			}
			if (copyColumns)
			{
				this._columns = new DataColumn[columns.Length];
				for (int l = 0; l < columns.Length; l++)
				{
					this._columns[l] = columns[l];
				}
			}
			else
			{
				this._columns = columns;
			}
			this.CheckState();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00015A8B File Offset: 0x00013C8B
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00015A93 File Offset: 0x00013C93
		internal bool HasValue
		{
			get
			{
				return this._columns != null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00015A9E File Offset: 0x00013C9E
		internal DataTable Table
		{
			get
			{
				return this._columns[0].Table;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00015AB0 File Offset: 0x00013CB0
		internal void CheckState()
		{
			DataTable table = this._columns[0].Table;
			if (table == null)
			{
				throw ExceptionBuilder.ColumnNotInAnyTable();
			}
			for (int i = 1; i < this._columns.Length; i++)
			{
				if (this._columns[i].Table == null)
				{
					throw ExceptionBuilder.ColumnNotInAnyTable();
				}
				if (this._columns[i].Table != table)
				{
					throw ExceptionBuilder.KeyTableMismatch();
				}
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015B12 File Offset: 0x00013D12
		internal bool ColumnsEqual(DataKey key)
		{
			return DataKey.ColumnsEqual(this._columns, key._columns);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00015B28 File Offset: 0x00013D28
		internal static bool ColumnsEqual(DataColumn[] column1, DataColumn[] column2)
		{
			if (column1 == column2)
			{
				return true;
			}
			if (column1 == null || column2 == null)
			{
				return false;
			}
			if (column1.Length != column2.Length)
			{
				return false;
			}
			for (int i = 0; i < column1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < column2.Length; j++)
				{
					if (column1[i].Equals(column2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00015B84 File Offset: 0x00013D84
		internal bool ContainsColumn(DataColumn column)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (column == this._columns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00015BB2 File Offset: 0x00013DB2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00015BC4 File Offset: 0x00013DC4
		public override bool Equals(object value)
		{
			return this.Equals((DataKey)value);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00015BD4 File Offset: 0x00013DD4
		internal bool Equals(DataKey value)
		{
			DataColumn[] columns = this._columns;
			DataColumn[] columns2 = value._columns;
			if (columns == columns2)
			{
				return true;
			}
			if (columns == null || columns2 == null)
			{
				return false;
			}
			if (columns.Length != columns2.Length)
			{
				return false;
			}
			for (int i = 0; i < columns.Length; i++)
			{
				if (!columns[i].Equals(columns2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00015C28 File Offset: 0x00013E28
		internal string[] GetColumnNames()
		{
			string[] array = new string[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00015C68 File Offset: 0x00013E68
		internal IndexField[] GetIndexDesc()
		{
			IndexField[] array = new IndexField[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = new IndexField(this._columns[i], false);
			}
			return array;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00015CAC File Offset: 0x00013EAC
		internal object[] GetKeyValues(int record)
		{
			object[] array = new object[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i][record];
			}
			return array;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00015CEC File Offset: 0x00013EEC
		internal Index GetSortIndex()
		{
			return this.GetSortIndex(DataViewRowState.CurrentRows);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00015CF8 File Offset: 0x00013EF8
		internal Index GetSortIndex(DataViewRowState recordStates)
		{
			IndexField[] indexDesc = this.GetIndexDesc();
			return this._columns[0].Table.GetIndex(indexDesc, recordStates, null);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00015D24 File Offset: 0x00013F24
		internal bool RecordsEqual(int record1, int record2)
		{
			for (int i = 0; i < this._columns.Length; i++)
			{
				if (this._columns[i].Compare(record1, record2) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00015D58 File Offset: 0x00013F58
		internal DataColumn[] ToArray()
		{
			DataColumn[] array = new DataColumn[this._columns.Length];
			for (int i = 0; i < this._columns.Length; i++)
			{
				array[i] = this._columns[i];
			}
			return array;
		}

		// Token: 0x04000556 RID: 1366
		private const int maxColumns = 32;

		// Token: 0x04000557 RID: 1367
		private readonly DataColumn[] _columns;
	}
}
