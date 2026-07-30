using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x0200032C RID: 812
	internal sealed class DbSchemaRow
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x000A9C90 File Offset: 0x000A7E90
		internal static DbSchemaRow[] GetSortedSchemaRows(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			DataColumn dataColumn = dataTable.Columns["SchemaMapping Unsorted Index"];
			if (dataColumn == null)
			{
				dataColumn = new DataColumn("SchemaMapping Unsorted Index", typeof(int));
				dataTable.Columns.Add(dataColumn);
			}
			int count = dataTable.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				dataTable.Rows[i][dataColumn] = i;
			}
			DbSchemaTable dbSchemaTable = new DbSchemaTable(dataTable, returnProviderSpecificTypes);
			DataRow[] array = dataTable.Select(null, "ColumnOrdinal ASC", DataViewRowState.CurrentRows);
			DbSchemaRow[] array2 = new DbSchemaRow[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = new DbSchemaRow(dbSchemaTable, array[j]);
			}
			return array2;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000A9D4C File Offset: 0x000A7F4C
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this._schemaTable = schemaTable;
			this._dataRow = dataRow;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000A9D62 File Offset: 0x000A7F62
		internal DataRow DataRow
		{
			get
			{
				return this._dataRow;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x000A9D6C File Offset: 0x000A7F6C
		internal string ColumnName
		{
			get
			{
				object obj = this._dataRow[this._schemaTable.ColumnName, DataRowVersion.Default];
				if (!Convert.IsDBNull(obj))
				{
					return Convert.ToString(obj, CultureInfo.InvariantCulture);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000A9DB0 File Offset: 0x000A7FB0
		internal int Size
		{
			get
			{
				object obj = this._dataRow[this._schemaTable.Size, DataRowVersion.Default];
				if (!Convert.IsDBNull(obj))
				{
					return Convert.ToInt32(obj, CultureInfo.InvariantCulture);
				}
				return 0;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000A9DF0 File Offset: 0x000A7FF0
		internal string BaseColumnName
		{
			get
			{
				if (this._schemaTable.BaseColumnName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseColumnName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x000A9E40 File Offset: 0x000A8040
		internal string BaseServerName
		{
			get
			{
				if (this._schemaTable.BaseServerName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseServerName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x000A9E90 File Offset: 0x000A8090
		internal string BaseCatalogName
		{
			get
			{
				if (this._schemaTable.BaseCatalogName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseCatalogName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000A9EE0 File Offset: 0x000A80E0
		internal string BaseSchemaName
		{
			get
			{
				if (this._schemaTable.BaseSchemaName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseSchemaName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000A9F30 File Offset: 0x000A8130
		internal string BaseTableName
		{
			get
			{
				if (this._schemaTable.BaseTableName != null)
				{
					object obj = this._dataRow[this._schemaTable.BaseTableName, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToString(obj, CultureInfo.InvariantCulture);
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x000A9F80 File Offset: 0x000A8180
		internal bool IsAutoIncrement
		{
			get
			{
				if (this._schemaTable.IsAutoIncrement != null)
				{
					object obj = this._dataRow[this._schemaTable.IsAutoIncrement, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000A9FCC File Offset: 0x000A81CC
		internal bool IsUnique
		{
			get
			{
				if (this._schemaTable.IsUnique != null)
				{
					object obj = this._dataRow[this._schemaTable.IsUnique, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x000AA018 File Offset: 0x000A8218
		internal bool IsRowVersion
		{
			get
			{
				if (this._schemaTable.IsRowVersion != null)
				{
					object obj = this._dataRow[this._schemaTable.IsRowVersion, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000AA064 File Offset: 0x000A8264
		internal bool IsKey
		{
			get
			{
				if (this._schemaTable.IsKey != null)
				{
					object obj = this._dataRow[this._schemaTable.IsKey, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x000AA0B0 File Offset: 0x000A82B0
		internal bool IsExpression
		{
			get
			{
				if (this._schemaTable.IsExpression != null)
				{
					object obj = this._dataRow[this._schemaTable.IsExpression, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x000AA0FC File Offset: 0x000A82FC
		internal bool IsHidden
		{
			get
			{
				if (this._schemaTable.IsHidden != null)
				{
					object obj = this._dataRow[this._schemaTable.IsHidden, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x000AA148 File Offset: 0x000A8348
		internal bool IsLong
		{
			get
			{
				if (this._schemaTable.IsLong != null)
				{
					object obj = this._dataRow[this._schemaTable.IsLong, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x000AA194 File Offset: 0x000A8394
		internal bool IsReadOnly
		{
			get
			{
				if (this._schemaTable.IsReadOnly != null)
				{
					object obj = this._dataRow[this._schemaTable.IsReadOnly, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x000AA1E0 File Offset: 0x000A83E0
		internal Type DataType
		{
			get
			{
				if (this._schemaTable.DataType != null)
				{
					object obj = this._dataRow[this._schemaTable.DataType, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return (Type)obj;
					}
				}
				return null;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x000AA228 File Offset: 0x000A8428
		internal bool AllowDBNull
		{
			get
			{
				if (this._schemaTable.AllowDBNull != null)
				{
					object obj = this._dataRow[this._schemaTable.AllowDBNull, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
					}
				}
				return true;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x000AA273 File Offset: 0x000A8473
		internal int UnsortedIndex
		{
			get
			{
				return (int)this._dataRow[this._schemaTable.UnsortedIndex, DataRowVersion.Default];
			}
		}

		// Token: 0x0400181D RID: 6173
		internal const string SchemaMappingUnsortedIndex = "SchemaMapping Unsorted Index";

		// Token: 0x0400181E RID: 6174
		private DbSchemaTable _schemaTable;

		// Token: 0x0400181F RID: 6175
		private DataRow _dataRow;
	}
}
