using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002F RID: 47
	public sealed class SqliteParameter : DbParameter, ICloneable
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000D48B File Offset: 0x0000B68B
		public SqliteParameter()
			: this(null, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D49C File Offset: 0x0000B69C
		public SqliteParameter(string parameterName)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D4AD File Offset: 0x0000B6AD
		public SqliteParameter(string parameterName, object value)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
			this.Value = value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		public SqliteParameter(string parameterName, DbType dbType)
			: this(parameterName, dbType, 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public SqliteParameter(string parameterName, DbType dbType, string sourceColumn)
			: this(parameterName, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		public SqliteParameter(string parameterName, DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(parameterName, dbType, 0, sourceColumn, rowVersion)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D4F5 File Offset: 0x0000B6F5
		public SqliteParameter(DbType dbType)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D506 File Offset: 0x0000B706
		public SqliteParameter(DbType dbType, object value)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
			this.Value = value;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D51E File Offset: 0x0000B71E
		public SqliteParameter(DbType dbType, string sourceColumn)
			: this(null, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D52F File Offset: 0x0000B72F
		public SqliteParameter(DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(null, dbType, 0, sourceColumn, rowVersion)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D53C File Offset: 0x0000B73C
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize)
			: this(parameterName, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D54D File Offset: 0x0000B74D
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
			: this(parameterName, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D560 File Offset: 0x0000B760
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
		{
			this._parameterName = parameterName;
			this._dbType = (int)parameterType;
			this._sourceColumn = sourceColumn;
			this._rowVersion = rowVersion;
			this._objValue = null;
			this._dataSize = parameterSize;
			this._nullMapping = false;
			this._nullable = true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		private SqliteParameter(SqliteParameter source)
			: this(source.ParameterName, (DbType)source._dbType, 0, source.Direction, source.IsNullable, 0, 0, source.SourceColumn, source.SourceVersion, source.Value)
		{
			this._nullMapping = source._nullMapping;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D5FC File Offset: 0x0000B7FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.Value = value;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D623 File Offset: 0x0000B823
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqliteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, bool sourceColumnNullMapping, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			this.Direction = direction;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D64A File Offset: 0x0000B84A
		public SqliteParameter(DbType parameterType, int parameterSize)
			: this(null, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D65B File Offset: 0x0000B85B
		public SqliteParameter(DbType parameterType, int parameterSize, string sourceColumn)
			: this(null, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D66C File Offset: 0x0000B86C
		public SqliteParameter(DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
			: this(null, parameterType, parameterSize, sourceColumn, rowVersion)
		{
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000D67A File Offset: 0x0000B87A
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000D682 File Offset: 0x0000B882
		public override bool IsNullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000D68B File Offset: 0x0000B88B
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000D6C5 File Offset: 0x0000B8C5
		[DbProviderSpecificTypeProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override DbType DbType
		{
			get
			{
				if (this._dbType != -1)
				{
					return (DbType)this._dbType;
				}
				if (this._objValue != null && this._objValue != DBNull.Value)
				{
					return SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
				return DbType.String;
			}
			set
			{
				this._dbType = (int)value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000D6D1 File Offset: 0x0000B8D1
		public override ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (value != ParameterDirection.Input)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000D6DD File Offset: 0x0000B8DD
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
		public override string ParameterName
		{
			get
			{
				return this._parameterName;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D6EE File Offset: 0x0000B8EE
		public override void ResetDbType()
		{
			this._dbType = -1;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000D6F7 File Offset: 0x0000B8F7
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000D6FF File Offset: 0x0000B8FF
		[DefaultValue(0)]
		public override int Size
		{
			get
			{
				return this._dataSize;
			}
			set
			{
				this._dataSize = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000D708 File Offset: 0x0000B908
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000D710 File Offset: 0x0000B910
		public override string SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000D719 File Offset: 0x0000B919
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000D721 File Offset: 0x0000B921
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._nullMapping;
			}
			set
			{
				this._nullMapping = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000D72A File Offset: 0x0000B92A
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000D732 File Offset: 0x0000B932
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this._rowVersion;
			}
			set
			{
				this._rowVersion = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000D73B File Offset: 0x0000B93B
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000D743 File Offset: 0x0000B943
		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public override object Value
		{
			get
			{
				return this._objValue;
			}
			set
			{
				this._objValue = value;
				if (this._dbType == -1 && this._objValue != null && this._objValue != DBNull.Value)
				{
					this._dbType = (int)SqliteConvert.TypeToDbType(this._objValue.GetType());
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D780 File Offset: 0x0000B980
		public object Clone()
		{
			return new SqliteParameter(this);
		}

		// Token: 0x040000F0 RID: 240
		internal int _dbType;

		// Token: 0x040000F1 RID: 241
		private DataRowVersion _rowVersion;

		// Token: 0x040000F2 RID: 242
		private object _objValue;

		// Token: 0x040000F3 RID: 243
		private string _sourceColumn;

		// Token: 0x040000F4 RID: 244
		private string _parameterName;

		// Token: 0x040000F5 RID: 245
		private int _dataSize;

		// Token: 0x040000F6 RID: 246
		private bool _nullable;

		// Token: 0x040000F7 RID: 247
		private bool _nullMapping;
	}
}
