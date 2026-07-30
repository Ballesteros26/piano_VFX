using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200021E RID: 542
	internal sealed class _SqlMetaData : SqlMetaDataPriv
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x0007D146 File Offset: 0x0007B346
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0007D155 File Offset: 0x0007B355
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0007D162 File Offset: 0x0007B362
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0007D16F File Offset: 0x0007B36F
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0007D17C File Offset: 0x0007B37C
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0007D189 File Offset: 0x0007B389
		internal bool IsNewKatmaiDateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0007D1B5 File Offset: 0x0007B3B5
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007D1D0 File Offset: 0x0007B3D0
		public object Clone()
		{
			_SqlMetaData sqlMetaData = new _SqlMetaData(this.ordinal);
			sqlMetaData.CopyFrom(this);
			sqlMetaData.column = this.column;
			sqlMetaData.baseColumn = this.baseColumn;
			sqlMetaData.multiPartTableName = this.multiPartTableName;
			sqlMetaData.updatability = this.updatability;
			sqlMetaData.tableNum = this.tableNum;
			sqlMetaData.isDifferentName = this.isDifferentName;
			sqlMetaData.isKey = this.isKey;
			sqlMetaData.isHidden = this.isHidden;
			sqlMetaData.isExpression = this.isExpression;
			sqlMetaData.isIdentity = this.isIdentity;
			sqlMetaData.isColumnSet = this.isColumnSet;
			sqlMetaData.op = this.op;
			sqlMetaData.operand = this.operand;
			return sqlMetaData;
		}

		// Token: 0x0400117E RID: 4478
		internal string column;

		// Token: 0x0400117F RID: 4479
		internal string baseColumn;

		// Token: 0x04001180 RID: 4480
		internal MultiPartTableName multiPartTableName;

		// Token: 0x04001181 RID: 4481
		internal readonly int ordinal;

		// Token: 0x04001182 RID: 4482
		internal byte updatability;

		// Token: 0x04001183 RID: 4483
		internal byte tableNum;

		// Token: 0x04001184 RID: 4484
		internal bool isDifferentName;

		// Token: 0x04001185 RID: 4485
		internal bool isKey;

		// Token: 0x04001186 RID: 4486
		internal bool isHidden;

		// Token: 0x04001187 RID: 4487
		internal bool isExpression;

		// Token: 0x04001188 RID: 4488
		internal bool isIdentity;

		// Token: 0x04001189 RID: 4489
		internal bool isColumnSet;

		// Token: 0x0400118A RID: 4490
		internal byte op;

		// Token: 0x0400118B RID: 4491
		internal ushort operand;
	}
}
