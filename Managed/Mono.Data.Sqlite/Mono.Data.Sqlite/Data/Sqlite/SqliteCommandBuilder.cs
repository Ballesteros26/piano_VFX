using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200000C RID: 12
	public sealed class SqliteCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00007EAE File Offset: 0x000060AE
		public SqliteCommandBuilder()
			: this(null)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007EB7 File Offset: 0x000060B7
		public SqliteCommandBuilder(SqliteDataAdapter adp)
		{
			this.QuotePrefix = "[";
			this.QuoteSuffix = "]";
			this.DataAdapter = adp;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007EDC File Offset: 0x000060DC
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			((SqliteParameter)parameter).DbType = (DbType)row[SchemaTableColumn.ProviderType];
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007EF9 File Offset: 0x000060F9
		protected override string GetParameterName(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", parameterName);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007F0B File Offset: 0x0000610B
		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format(CultureInfo.InvariantCulture, "@param{0}", parameterOrdinal);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007F22 File Offset: 0x00006122
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return this.GetParameterName(parameterOrdinal);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007F2B File Offset: 0x0000612B
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqliteDataAdapter)adapter).RowUpdating -= this.RowUpdatingEventHandler;
				return;
			}
			((SqliteDataAdapter)adapter).RowUpdating += this.RowUpdatingEventHandler;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00007F65 File Offset: 0x00006165
		private void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e)
		{
			base.RowUpdatingHandler(e);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007F6E File Offset: 0x0000616E
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00007F7B File Offset: 0x0000617B
		public new SqliteDataAdapter DataAdapter
		{
			get
			{
				return (SqliteDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007F84 File Offset: 0x00006184
		public new SqliteCommand GetDeleteCommand()
		{
			return (SqliteCommand)base.GetDeleteCommand();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007F91 File Offset: 0x00006191
		public new SqliteCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007F9F File Offset: 0x0000619F
		public new SqliteCommand GetUpdateCommand()
		{
			return (SqliteCommand)base.GetUpdateCommand();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007FAC File Offset: 0x000061AC
		public new SqliteCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007FBA File Offset: 0x000061BA
		public new SqliteCommand GetInsertCommand()
		{
			return (SqliteCommand)base.GetInsertCommand();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007FC7 File Offset: 0x000061C7
		public new SqliteCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqliteCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00007FD5 File Offset: 0x000061D5
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00007FDD File Offset: 0x000061DD
		[Browsable(false)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return base.CatalogLocation;
			}
			set
			{
				base.CatalogLocation = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007FE6 File Offset: 0x000061E6
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00007FEE File Offset: 0x000061EE
		[Browsable(false)]
		public override string CatalogSeparator
		{
			get
			{
				return base.CatalogSeparator;
			}
			set
			{
				base.CatalogSeparator = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007FF7 File Offset: 0x000061F7
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00007FFF File Offset: 0x000061FF
		[Browsable(false)]
		[DefaultValue("[")]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				base.QuotePrefix = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00008008 File Offset: 0x00006208
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00008010 File Offset: 0x00006210
		[Browsable(false)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				base.QuoteSuffix = value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000801C File Offset: 0x0000621C
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(unquotedIdentifier))
			{
				return unquotedIdentifier;
			}
			return this.QuotePrefix + unquotedIdentifier.Replace(this.QuoteSuffix, this.QuoteSuffix + this.QuoteSuffix) + this.QuoteSuffix;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000807C File Offset: 0x0000627C
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (string.IsNullOrEmpty(this.QuotePrefix) || string.IsNullOrEmpty(this.QuoteSuffix) || string.IsNullOrEmpty(quotedIdentifier))
			{
				return quotedIdentifier;
			}
			if (!quotedIdentifier.StartsWith(this.QuotePrefix, StringComparison.InvariantCultureIgnoreCase) || !quotedIdentifier.EndsWith(this.QuoteSuffix, StringComparison.InvariantCultureIgnoreCase))
			{
				return quotedIdentifier;
			}
			return quotedIdentifier.Substring(this.QuotePrefix.Length, quotedIdentifier.Length - (this.QuotePrefix.Length + this.QuoteSuffix.Length)).Replace(this.QuoteSuffix + this.QuoteSuffix, this.QuoteSuffix);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008118 File Offset: 0x00006318
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00008120 File Offset: 0x00006320
		[Browsable(false)]
		public override string SchemaSeparator
		{
			get
			{
				return base.SchemaSeparator;
			}
			set
			{
				base.SchemaSeparator = value;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000812C File Offset: 0x0000632C
		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable dataTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				DataTable schemaTable = dataReader.GetSchemaTable();
				if (this.HasSchemaPrimaryKey(schemaTable))
				{
					this.ResetIsUniqueSchemaColumn(schemaTable);
				}
				dataTable = schemaTable;
			}
			return dataTable;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008178 File Offset: 0x00006378
		private bool HasSchemaPrimaryKey(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsKey];
			using (IEnumerator enumerator = schema.Rows.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((bool)((DataRow)enumerator.Current)[dataColumn])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000081F0 File Offset: 0x000063F0
		private void ResetIsUniqueSchemaColumn(DataTable schema)
		{
			DataColumn dataColumn = schema.Columns[SchemaTableColumn.IsUnique];
			DataColumn dataColumn2 = schema.Columns[SchemaTableColumn.IsKey];
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!(bool)dataRow[dataColumn2])
				{
					dataRow[dataColumn] = false;
				}
			}
			schema.AcceptChanges();
		}
	}
}
