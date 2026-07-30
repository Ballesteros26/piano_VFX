using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001B3 RID: 435
	internal class SqlDbColumn : DbColumn
	{
		// Token: 0x0600144C RID: 5196 RVA: 0x00065BCA File Offset: 0x00063DCA
		internal SqlDbColumn(_SqlMetaData md)
		{
			this._metadata = md;
			this.Populate();
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00065BE0 File Offset: 0x00063DE0
		private void Populate()
		{
			base.AllowDBNull = new bool?(this._metadata.isNullable);
			base.BaseCatalogName = this._metadata.catalogName;
			base.BaseColumnName = this._metadata.baseColumn;
			base.BaseSchemaName = this._metadata.schemaName;
			base.BaseServerName = this._metadata.serverName;
			base.BaseTableName = this._metadata.tableName;
			base.ColumnName = this._metadata.column;
			base.ColumnOrdinal = new int?(this._metadata.ordinal);
			base.ColumnSize = new int?((this._metadata.metaType.IsSizeInCharacters && this._metadata.length != int.MaxValue) ? (this._metadata.length / 2) : this._metadata.length);
			base.IsAutoIncrement = new bool?(this._metadata.isIdentity);
			base.IsIdentity = new bool?(this._metadata.isIdentity);
			base.IsLong = new bool?(this._metadata.metaType.IsLong);
			if (SqlDbType.Timestamp == this._metadata.type)
			{
				base.IsUnique = new bool?(true);
			}
			else
			{
				base.IsUnique = new bool?(false);
			}
			if (255 != this._metadata.precision)
			{
				base.NumericPrecision = new int?((int)this._metadata.precision);
			}
			else
			{
				base.NumericPrecision = new int?((int)this._metadata.metaType.Precision);
			}
			base.IsReadOnly = new bool?(this._metadata.updatability == 0);
			base.UdtAssemblyQualifiedName = this._metadata.udtAssemblyQualifiedName;
		}

		// Token: 0x170003B8 RID: 952
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00065DA7 File Offset: 0x00063FA7
		internal bool? SqlIsAliased
		{
			set
			{
				base.IsAliased = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00065DB0 File Offset: 0x00063FB0
		internal bool? SqlIsKey
		{
			set
			{
				base.IsKey = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00065DB9 File Offset: 0x00063FB9
		internal bool? SqlIsHidden
		{
			set
			{
				base.IsHidden = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00065DC2 File Offset: 0x00063FC2
		internal bool? SqlIsExpression
		{
			set
			{
				base.IsExpression = value;
			}
		}

		// Token: 0x170003BC RID: 956
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00065DCB File Offset: 0x00063FCB
		internal Type SqlDataType
		{
			set
			{
				base.DataType = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x00065DD4 File Offset: 0x00063FD4
		internal string SqlDataTypeName
		{
			set
			{
				base.DataTypeName = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00065DDD File Offset: 0x00063FDD
		internal int? SqlNumericScale
		{
			set
			{
				base.NumericScale = value;
			}
		}

		// Token: 0x04000D8D RID: 3469
		private readonly _SqlMetaData _metadata;
	}
}
