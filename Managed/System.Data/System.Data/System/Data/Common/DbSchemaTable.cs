using System;

namespace System.Data.Common
{
	// Token: 0x0200032D RID: 813
	internal sealed class DbSchemaTable
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x000AA295 File Offset: 0x000A8495
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this._dataTable = dataTable;
			this._columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x000AA2C9 File Offset: 0x000A84C9
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000AA2D2 File Offset: 0x000A84D2
		internal DataColumn Size
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnSize);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000AA2DB File Offset: 0x000A84DB
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000AA2E4 File Offset: 0x000A84E4
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000AA2ED File Offset: 0x000A84ED
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x000AA2F6 File Offset: 0x000A84F6
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000AA2FF File Offset: 0x000A84FF
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000AA308 File Offset: 0x000A8508
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x000AA311 File Offset: 0x000A8511
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x000AA31B File Offset: 0x000A851B
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x000AA325 File Offset: 0x000A8525
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x000AA32F File Offset: 0x000A852F
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x000AA339 File Offset: 0x000A8539
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000AA343 File Offset: 0x000A8543
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x000AA34D File Offset: 0x000A854D
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x000AA357 File Offset: 0x000A8557
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x000AA361 File Offset: 0x000A8561
		internal DataColumn UnsortedIndex
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.SchemaMappingUnsortedIndex);
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000AA36B File Offset: 0x000A856B
		internal DataColumn DataType
		{
			get
			{
				if (this._returnProviderSpecificTypes)
				{
					return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ProviderSpecificDataType, DbSchemaTable.ColumnEnum.DataType);
				}
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.DataType);
			}
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x000AA388 File Offset: 0x000A8588
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000AA394 File Offset: 0x000A8594
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column, DbSchemaTable.ColumnEnum column2)
		{
			DataColumn dataColumn = this._columnCache[(int)column];
			if (dataColumn == null)
			{
				int num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column]);
				if (-1 == num && column != column2)
				{
					num = this._columns.IndexOf(DbSchemaTable.s_DBCOLUMN_NAME[(int)column2]);
				}
				if (-1 != num)
				{
					dataColumn = this._columns[num];
					this._columnCache[(int)column] = dataColumn;
				}
			}
			return dataColumn;
		}

		// Token: 0x04001820 RID: 6176
		private static readonly string[] s_DBCOLUMN_NAME = new string[]
		{
			SchemaTableColumn.ColumnName,
			SchemaTableColumn.ColumnOrdinal,
			SchemaTableColumn.ColumnSize,
			SchemaTableOptionalColumn.BaseServerName,
			SchemaTableOptionalColumn.BaseCatalogName,
			SchemaTableColumn.BaseColumnName,
			SchemaTableColumn.BaseSchemaName,
			SchemaTableColumn.BaseTableName,
			SchemaTableOptionalColumn.IsAutoIncrement,
			SchemaTableColumn.IsUnique,
			SchemaTableColumn.IsKey,
			SchemaTableOptionalColumn.IsRowVersion,
			SchemaTableColumn.DataType,
			SchemaTableOptionalColumn.ProviderSpecificDataType,
			SchemaTableColumn.AllowDBNull,
			SchemaTableColumn.ProviderType,
			SchemaTableColumn.IsExpression,
			SchemaTableOptionalColumn.IsHidden,
			SchemaTableColumn.IsLong,
			SchemaTableOptionalColumn.IsReadOnly,
			"SchemaMapping Unsorted Index"
		};

		// Token: 0x04001821 RID: 6177
		internal DataTable _dataTable;

		// Token: 0x04001822 RID: 6178
		private DataColumnCollection _columns;

		// Token: 0x04001823 RID: 6179
		private DataColumn[] _columnCache = new DataColumn[DbSchemaTable.s_DBCOLUMN_NAME.Length];

		// Token: 0x04001824 RID: 6180
		private bool _returnProviderSpecificTypes;

		// Token: 0x0200032E RID: 814
		private enum ColumnEnum
		{
			// Token: 0x04001826 RID: 6182
			ColumnName,
			// Token: 0x04001827 RID: 6183
			ColumnOrdinal,
			// Token: 0x04001828 RID: 6184
			ColumnSize,
			// Token: 0x04001829 RID: 6185
			BaseServerName,
			// Token: 0x0400182A RID: 6186
			BaseCatalogName,
			// Token: 0x0400182B RID: 6187
			BaseColumnName,
			// Token: 0x0400182C RID: 6188
			BaseSchemaName,
			// Token: 0x0400182D RID: 6189
			BaseTableName,
			// Token: 0x0400182E RID: 6190
			IsAutoIncrement,
			// Token: 0x0400182F RID: 6191
			IsUnique,
			// Token: 0x04001830 RID: 6192
			IsKey,
			// Token: 0x04001831 RID: 6193
			IsRowVersion,
			// Token: 0x04001832 RID: 6194
			DataType,
			// Token: 0x04001833 RID: 6195
			ProviderSpecificDataType,
			// Token: 0x04001834 RID: 6196
			AllowDBNull,
			// Token: 0x04001835 RID: 6197
			ProviderType,
			// Token: 0x04001836 RID: 6198
			IsExpression,
			// Token: 0x04001837 RID: 6199
			IsHidden,
			// Token: 0x04001838 RID: 6200
			IsLong,
			// Token: 0x04001839 RID: 6201
			IsReadOnly,
			// Token: 0x0400183A RID: 6202
			SchemaMappingUnsortedIndex
		}
	}
}
