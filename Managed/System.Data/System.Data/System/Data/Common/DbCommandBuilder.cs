using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.Common
{
	/// <summary>Automatically generates single-table commands used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This is an abstract class that can only be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200032A RID: 810
	public abstract class DbCommandBuilder : Component
	{
		/// <summary>Specifies which <see cref="T:System.Data.ConflictOption" /> is to be used by the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>Returns one of the <see cref="T:System.Data.ConflictOption" /> values describing the behavior of this <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x000A83AD File Offset: 0x000A65AD
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x000A83B5 File Offset: 0x000A65B5
		[DefaultValue(ConflictOption.CompareAllSearchableValues)]
		public virtual ConflictOption ConflictOption
		{
			get
			{
				return this._conflictDetection;
			}
			set
			{
				if (value - ConflictOption.CompareAllSearchableValues <= 2)
				{
					this._conflictDetection = value;
					return;
				}
				throw ADP.InvalidConflictOptions(value);
			}
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x000A83CB File Offset: 0x000A65CB
		// (set) Token: 0x060024CE RID: 9422 RVA: 0x000A83D3 File Offset: 0x000A65D3
		[DefaultValue(CatalogLocation.Start)]
		public virtual CatalogLocation CatalogLocation
		{
			get
			{
				return this._catalogLocation;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				if (value - CatalogLocation.Start <= 1)
				{
					this._catalogLocation = value;
					return;
				}
				throw ADP.InvalidCatalogLocation(value);
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A string indicating the catalog separator for use with an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000A83F8 File Offset: 0x000A65F8
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x000A841F File Offset: 0x000A661F
		[DefaultValue(".")]
		public virtual string CatalogSeparator
		{
			get
			{
				string catalogSeparator = this._catalogSeparator;
				if (catalogSeparator == null || 0 >= catalogSeparator.Length)
				{
					return ".";
				}
				return catalogSeparator;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._catalogSeparator = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.Common.DbDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000A8436 File Offset: 0x000A6636
		// (set) Token: 0x060024D2 RID: 9426 RVA: 0x000A843E File Offset: 0x000A663E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbDataAdapter DataAdapter
		{
			get
			{
				return this._dataAdapter;
			}
			set
			{
				if (this._dataAdapter != value)
				{
					this.RefreshSchema();
					if (this._dataAdapter != null)
					{
						this.SetRowUpdatingHandler(this._dataAdapter);
						this._dataAdapter = null;
					}
					if (value != null)
					{
						this.SetRowUpdatingHandler(value);
						this._dataAdapter = value;
					}
				}
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000A847B File Offset: 0x000A667B
		internal int ParameterNameMaxLength
		{
			get
			{
				return this._parameterNameMaxLength;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000A8483 File Offset: 0x000A6683
		internal string ParameterNamePattern
		{
			get
			{
				return this._parameterNamePattern;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000A848B File Offset: 0x000A668B
		private string QuotedBaseTableName
		{
			get
			{
				return this._quotedBaseTableName;
			}
		}

		/// <summary>Gets or sets the beginning character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The beginning character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000A8493 File Offset: 0x000A6693
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x000A84A4 File Offset: 0x000A66A4
		[DefaultValue("")]
		public virtual string QuotePrefix
		{
			get
			{
				return this._quotePrefix ?? string.Empty;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quotePrefix = value;
			}
		}

		/// <summary>Gets or sets the ending character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000A84BC File Offset: 0x000A66BC
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x000A84DA File Offset: 0x000A66DA
		[DefaultValue("")]
		public virtual string QuoteSuffix
		{
			get
			{
				string quoteSuffix = this._quoteSuffix;
				if (quoteSuffix == null)
				{
					return string.Empty;
				}
				return quoteSuffix;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._quoteSuffix = value;
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x000A84F4 File Offset: 0x000A66F4
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x000A851B File Offset: 0x000A671B
		[DefaultValue(".")]
		public virtual string SchemaSeparator
		{
			get
			{
				string schemaSeparator = this._schemaSeparator;
				if (schemaSeparator == null || 0 >= schemaSeparator.Length)
				{
					return ".";
				}
				return schemaSeparator;
			}
			set
			{
				if (this._dbSchemaTable != null)
				{
					throw ADP.NoQuoteChange();
				}
				this._schemaSeparator = value;
			}
		}

		/// <summary>Specifies whether all column values in an update statement are included or only changed ones.</summary>
		/// <returns>true if the UPDATE statement generated by the <see cref="T:System.Data.Common.DbCommandBuilder" /> includes all columns; false if it includes only changed columns.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x000A8532 File Offset: 0x000A6732
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x000A853A File Offset: 0x000A673A
		[DefaultValue(false)]
		public bool SetAllValues
		{
			get
			{
				return this._setAllValues;
			}
			set
			{
				this._setAllValues = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000A8543 File Offset: 0x000A6743
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x000A854B File Offset: 0x000A674B
		private DbCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x000A8554 File Offset: 0x000A6754
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x000A855C File Offset: 0x000A675C
		private DbCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x000A8565 File Offset: 0x000A6765
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x000A856D File Offset: 0x000A676D
		private DbCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000A8578 File Offset: 0x000A6778
		private void BuildCache(bool closeConnection, DataRow dataRow, bool useColumnsForParameterNames)
		{
			if (this._dbSchemaTable != null && (!useColumnsForParameterNames || this._parameterNames != null))
			{
				return;
			}
			DataTable dataTable = null;
			DbCommand selectCommand = this.GetSelectCommand();
			DbConnection connection = selectCommand.Connection;
			if (connection == null)
			{
				throw ADP.MissingSourceCommandConnection();
			}
			try
			{
				if ((ConnectionState.Open & connection.State) == ConnectionState.Closed)
				{
					connection.Open();
				}
				else
				{
					closeConnection = false;
				}
				if (useColumnsForParameterNames)
				{
					DataTable schema = connection.GetSchema(DbMetaDataCollectionNames.DataSourceInformation);
					if (schema.Rows.Count == 1)
					{
						this._parameterNamePattern = schema.Rows[0][DbMetaDataColumnNames.ParameterNamePattern] as string;
						this._parameterMarkerFormat = schema.Rows[0][DbMetaDataColumnNames.ParameterMarkerFormat] as string;
						object obj = schema.Rows[0][DbMetaDataColumnNames.ParameterNameMaxLength];
						this._parameterNameMaxLength = ((obj is int) ? ((int)obj) : 0);
						if (this._parameterNameMaxLength == 0 || this._parameterNamePattern == null || this._parameterMarkerFormat == null)
						{
							useColumnsForParameterNames = false;
						}
					}
					else
					{
						useColumnsForParameterNames = false;
					}
				}
				dataTable = this.GetSchemaTable(selectCommand);
			}
			finally
			{
				if (closeConnection)
				{
					connection.Close();
				}
			}
			if (dataTable == null)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			this.BuildInformation(dataTable);
			this._dbSchemaTable = dataTable;
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				if (dbSchemaRows[i] != null)
				{
					array[i] = dbSchemaRows[i].ColumnName;
				}
			}
			this._sourceColumnNames = array;
			if (useColumnsForParameterNames)
			{
				this._parameterNames = new DbCommandBuilder.ParameterNames(this, dbSchemaRows);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
		}

		/// <summary>Returns the schema table for the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that represents the schema for the specific <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <param name="sourceCommand">The <see cref="T:System.Data.Common.DbCommand" /> for which to retrieve the corresponding schema table.</param>
		// Token: 0x060024E5 RID: 9445 RVA: 0x000A8714 File Offset: 0x000A6914
		protected virtual DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable;
			using (IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				schemaTable = dataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000A8750 File Offset: 0x000A6950
		private void BuildInformation(DataTable schemaTable)
		{
			DbSchemaRow[] sortedSchemaRows = DbSchemaRow.GetSortedSchemaRows(schemaTable, false);
			if (sortedSchemaRows == null || sortedSchemaRows.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = null;
			for (int i = 0; i < sortedSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = sortedSchemaRows[i];
				string baseTableName = dbSchemaRow.BaseTableName;
				if (baseTableName == null || baseTableName.Length == 0)
				{
					sortedSchemaRows[i] = null;
				}
				else
				{
					string text5 = dbSchemaRow.BaseServerName;
					string text6 = dbSchemaRow.BaseCatalogName;
					string text7 = dbSchemaRow.BaseSchemaName;
					if (text5 == null)
					{
						text5 = string.Empty;
					}
					if (text6 == null)
					{
						text6 = string.Empty;
					}
					if (text7 == null)
					{
						text7 = string.Empty;
					}
					if (text4 == null)
					{
						text = text5;
						text2 = text6;
						text3 = text7;
						text4 = baseTableName;
					}
					else if (ADP.SrcCompare(text4, baseTableName) != 0 || ADP.SrcCompare(text3, text7) != 0 || ADP.SrcCompare(text2, text6) != 0 || ADP.SrcCompare(text, text5) != 0)
					{
						throw ADP.DynamicSQLJoinUnsupported();
					}
				}
			}
			if (text.Length == 0)
			{
				text = null;
			}
			if (text2.Length == 0)
			{
				text = null;
				text2 = null;
			}
			if (text3.Length == 0)
			{
				text = null;
				text2 = null;
				text3 = null;
			}
			if (text4 == null || text4.Length == 0)
			{
				throw ADP.DynamicSQLNoTableInfo();
			}
			CatalogLocation catalogLocation = this.CatalogLocation;
			string catalogSeparator = this.CatalogSeparator;
			string schemaSeparator = this.SchemaSeparator;
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (!string.IsNullOrEmpty(quotePrefix) && -1 != text4.IndexOf(quotePrefix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix) && -1 != text4.IndexOf(quoteSuffix, StringComparison.Ordinal))
			{
				throw ADP.DynamicSQLNestedQuote(text4, quoteSuffix);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (CatalogLocation.Start == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
					stringBuilder.Append(catalogSeparator);
				}
				if (text2 != null)
				{
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
					stringBuilder.Append(catalogSeparator);
				}
			}
			if (text3 != null)
			{
				stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text3));
				stringBuilder.Append(schemaSeparator);
			}
			stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text4));
			if (CatalogLocation.End == catalogLocation)
			{
				if (text != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text));
				}
				if (text2 != null)
				{
					stringBuilder.Append(catalogSeparator);
					stringBuilder.Append(ADP.BuildQuotedString(quotePrefix, quoteSuffix, text2));
				}
			}
			this._quotedBaseTableName = stringBuilder.ToString();
			this._hasPartialPrimaryKey = false;
			foreach (DbSchemaRow dbSchemaRow2 in sortedSchemaRows)
			{
				if (dbSchemaRow2 != null && (dbSchemaRow2.IsKey || dbSchemaRow2.IsUnique) && !dbSchemaRow2.IsLong && !dbSchemaRow2.IsRowVersion && dbSchemaRow2.IsHidden)
				{
					this._hasPartialPrimaryKey = true;
					break;
				}
			}
			this._dbSchemaRows = sortedSchemaRows;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000A8A10 File Offset: 0x000A6C10
		private DbCommand BuildDeleteCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.DeleteCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			stringBuilder.Append("DELETE FROM ");
			stringBuilder.Append(this.QuotedBaseTableName);
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, false);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.DeleteCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000A8A74 File Offset: 0x000A6C74
		private DbCommand BuildInsertCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.InsertCommand);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			string text = " (";
			stringBuilder.Append("INSERT INTO ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			string[] array = new string[dbSchemaRows.Length];
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInInsertValues(dbSchemaRow))
				{
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text2, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_011E;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!dbSchemaRow.AllowDBNull && (obj == null || Convert.IsDBNull(obj)))
						{
							goto IL_011E;
						}
					}
					stringBuilder.Append(text);
					text = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					array[num] = this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text2, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Insert, false);
					num++;
				}
				IL_011E:;
			}
			if (num == 0)
			{
				stringBuilder.Append(" DEFAULT VALUES");
			}
			else
			{
				stringBuilder.Append(")");
				stringBuilder.Append(" VALUES ");
				stringBuilder.Append("(");
				stringBuilder.Append(array[0]);
				for (int j = 1; j < num; j++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[j]);
				}
				stringBuilder.Append(")");
			}
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.InsertCommand = dbCommand;
			return dbCommand;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x000A8C40 File Offset: 0x000A6E40
		private DbCommand BuildUpdateCommand(DataTableMapping mappings, DataRow dataRow)
		{
			DbCommand dbCommand = this.InitializeCommand(this.UpdateCommand);
			StringBuilder stringBuilder = new StringBuilder();
			string text = " SET ";
			int num = 0;
			stringBuilder.Append("UPDATE ");
			stringBuilder.Append(this.QuotedBaseTableName);
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInUpdateSet(dbSchemaRow))
				{
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					if (mappings != null && dataRow != null)
					{
						DataColumn dataColumn = this.GetDataColumn(text2, mappings, dataRow);
						if (dataColumn == null || (dbSchemaRow.IsReadOnly && dataColumn.ReadOnly))
						{
							goto IL_013F;
						}
						obj = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Current);
						if (!this.SetAllValues)
						{
							object columnValue = this.GetColumnValue(dataRow, dataColumn, DataRowVersion.Original);
							if (columnValue == obj || (columnValue != null && columnValue.Equals(obj)))
							{
								goto IL_013F;
							}
						}
					}
					stringBuilder.Append(text);
					text = ", ";
					stringBuilder.Append(this.QuotedColumn(dbSchemaRow.BaseColumnName));
					stringBuilder.Append(" = ");
					stringBuilder.Append(this.CreateParameterForValue(dbCommand, this.GetBaseParameterName(i), text2, DataRowVersion.Current, num, obj, dbSchemaRow, StatementType.Update, false));
					num++;
				}
				IL_013F:;
			}
			bool flag = num == 0;
			num = this.BuildWhereClause(mappings, dataRow, stringBuilder, dbCommand, num, true);
			dbCommand.CommandText = stringBuilder.ToString();
			DbCommandBuilder.RemoveExtraParameters(dbCommand, num);
			this.UpdateCommand = dbCommand;
			if (!flag)
			{
				return dbCommand;
			}
			return null;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x000A8DD0 File Offset: 0x000A6FD0
		private int BuildWhereClause(DataTableMapping mappings, DataRow dataRow, StringBuilder builder, DbCommand command, int parameterCount, bool isUpdate)
		{
			string text = string.Empty;
			int num = 0;
			builder.Append(" WHERE ");
			builder.Append("(");
			DbSchemaRow[] dbSchemaRows = this._dbSchemaRows;
			for (int i = 0; i < dbSchemaRows.Length; i++)
			{
				DbSchemaRow dbSchemaRow = dbSchemaRows[i];
				if (dbSchemaRow != null && dbSchemaRow.BaseColumnName.Length != 0 && this.IncludeInWhereClause(dbSchemaRow, isUpdate))
				{
					builder.Append(text);
					text = " AND ";
					object obj = null;
					string text2 = this._sourceColumnNames[i];
					string text3 = this.QuotedColumn(dbSchemaRow.BaseColumnName);
					if (mappings != null && dataRow != null)
					{
						obj = this.GetColumnValue(dataRow, text2, mappings, DataRowVersion.Original);
					}
					if (!dbSchemaRow.AllowDBNull)
					{
						builder.Append("(");
						builder.Append(text3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
					}
					else
					{
						builder.Append("(");
						builder.Append("(");
						builder.Append(this.CreateParameterForNullTest(command, this.GetNullParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(" = 1");
						builder.Append(" AND ");
						builder.Append(text3);
						builder.Append(" IS NULL");
						builder.Append(")");
						builder.Append(" OR ");
						builder.Append("(");
						builder.Append(text3);
						builder.Append(" = ");
						builder.Append(this.CreateParameterForValue(command, this.GetOriginalParameterName(i), text2, DataRowVersion.Original, parameterCount, obj, dbSchemaRow, isUpdate ? StatementType.Update : StatementType.Delete, true));
						parameterCount++;
						builder.Append(")");
						builder.Append(")");
					}
					if (this.IncrementWhereCount(dbSchemaRow))
					{
						num++;
					}
				}
			}
			builder.Append(")");
			if (num != 0)
			{
				return parameterCount;
			}
			if (isUpdate)
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
				}
				throw ADP.DynamicSQLNoKeyInfoUpdate();
			}
			else
			{
				if (ConflictOption.CompareRowVersion == this.ConflictOption)
				{
					throw ADP.DynamicSQLNoKeyInfoRowVersionDelete();
				}
				throw ADP.DynamicSQLNoKeyInfoDelete();
			}
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x000A903C File Offset: 0x000A723C
		private string CreateParameterForNullTest(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = true;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			nextParameter.DbType = DbType.Int32;
			nextParameter.Value = (ADP.IsNull(value) ? DbDataAdapter.s_parameterValueNullValue : DbDataAdapter.s_parameterValueNonNullValue);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, parameterName);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000A910C File Offset: 0x000A730C
		private string CreateParameterForValue(DbCommand command, string parameterName, string sourceColumn, DataRowVersion version, int parameterCount, object value, DbSchemaRow row, StatementType statementType, bool whereClause)
		{
			DbParameter nextParameter = DbCommandBuilder.GetNextParameter(command, parameterCount);
			if (parameterName == null)
			{
				nextParameter.ParameterName = this.GetParameterName(1 + parameterCount);
			}
			else
			{
				nextParameter.ParameterName = parameterName;
			}
			nextParameter.Direction = ParameterDirection.Input;
			nextParameter.SourceColumn = sourceColumn;
			nextParameter.SourceVersion = version;
			nextParameter.SourceColumnNullMapping = false;
			nextParameter.Value = value;
			nextParameter.Size = 0;
			this.ApplyParameterInfo(nextParameter, row.DataRow, statementType, whereClause);
			if (!command.Parameters.Contains(nextParameter))
			{
				command.Parameters.Add(nextParameter);
			}
			if (parameterName == null)
			{
				return this.GetParameterPlaceholder(1 + parameterCount);
			}
			return string.Format(CultureInfo.InvariantCulture, this._parameterMarkerFormat, parameterName);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbCommandBuilder" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060024ED RID: 9453 RVA: 0x000A91B6 File Offset: 0x000A73B6
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DataAdapter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x000A91CC File Offset: 0x000A73CC
		private DataTableMapping GetTableMapping(DataRow dataRow)
		{
			DataTableMapping dataTableMapping = null;
			if (dataRow != null)
			{
				DataTable table = dataRow.Table;
				if (table != null)
				{
					DbDataAdapter dataAdapter = this.DataAdapter;
					if (dataAdapter != null)
					{
						dataTableMapping = dataAdapter.GetTableMapping(table);
					}
					else
					{
						string tableName = table.TableName;
						dataTableMapping = new DataTableMapping(tableName, tableName);
					}
				}
			}
			return dataTableMapping;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000A920A File Offset: 0x000A740A
		private string GetBaseParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetBaseParameterName(index);
			}
			return null;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000A9222 File Offset: 0x000A7422
		private string GetOriginalParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetOriginalParameterName(index);
			}
			return null;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000A923A File Offset: 0x000A743A
		private string GetNullParameterName(int index)
		{
			if (this._parameterNames != null)
			{
				return this._parameterNames.GetNullParameterName(index);
			}
			return null;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000A9254 File Offset: 0x000A7454
		private DbCommand GetSelectCommand()
		{
			DbCommand dbCommand = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this._missingMappingAction == (MissingMappingAction)0)
				{
					this._missingMappingAction = dataAdapter.MissingMappingAction;
				}
				dbCommand = dataAdapter.SelectCommand;
			}
			if (dbCommand == null)
			{
				throw ADP.MissingSourceCommand();
			}
			return dbCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F3 RID: 9459 RVA: 0x000A9292 File Offset: 0x000A7492
		public DbCommand GetInsertCommand()
		{
			return this.GetInsertCommand(null, false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060024F4 RID: 9460 RVA: 0x000A929C File Offset: 0x000A749C
		public DbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return this.GetInsertCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000A92A6 File Offset: 0x000A74A6
		internal DbCommand GetInsertCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildInsertCommand(this.GetTableMapping(dataRow), dataRow);
			return this.InsertCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F6 RID: 9462 RVA: 0x000A92C6 File Offset: 0x000A74C6
		public DbCommand GetUpdateCommand()
		{
			return this.GetUpdateCommand(null, false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060024F7 RID: 9463 RVA: 0x000A92D0 File Offset: 0x000A74D0
		public DbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return this.GetUpdateCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x000A92DA File Offset: 0x000A74DA
		internal DbCommand GetUpdateCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildUpdateCommand(this.GetTableMapping(dataRow), dataRow);
			return this.UpdateCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F9 RID: 9465 RVA: 0x000A92FA File Offset: 0x000A74FA
		public DbCommand GetDeleteCommand()
		{
			return this.GetDeleteCommand(null, false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060024FA RID: 9466 RVA: 0x000A9304 File Offset: 0x000A7504
		public DbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return this.GetDeleteCommand(null, useColumnsForParameterNames);
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000A930E File Offset: 0x000A750E
		internal DbCommand GetDeleteCommand(DataRow dataRow, bool useColumnsForParameterNames)
		{
			this.BuildCache(true, dataRow, useColumnsForParameterNames);
			this.BuildDeleteCommand(this.GetTableMapping(dataRow), dataRow);
			return this.DeleteCommand;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x000A932E File Offset: 0x000A752E
		private object GetColumnValue(DataRow row, string columnName, DataTableMapping mappings, DataRowVersion version)
		{
			return this.GetColumnValue(row, this.GetDataColumn(columnName, mappings, row), version);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000A9344 File Offset: 0x000A7544
		private object GetColumnValue(DataRow row, DataColumn column, DataRowVersion version)
		{
			object obj = null;
			if (column != null)
			{
				obj = row[column, version];
			}
			return obj;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000A9360 File Offset: 0x000A7560
		private DataColumn GetDataColumn(string columnName, DataTableMapping tablemapping, DataRow row)
		{
			DataColumn dataColumn = null;
			if (!string.IsNullOrEmpty(columnName))
			{
				dataColumn = tablemapping.GetDataColumn(columnName, null, row.Table, this._missingMappingAction, MissingSchemaAction.Error);
			}
			return dataColumn;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000A9390 File Offset: 0x000A7590
		private static DbParameter GetNextParameter(DbCommand command, int pcount)
		{
			DbParameter dbParameter;
			if (pcount < command.Parameters.Count)
			{
				dbParameter = command.Parameters[pcount];
			}
			else
			{
				dbParameter = command.CreateParameter();
			}
			return dbParameter;
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000A93C2 File Offset: 0x000A75C2
		private bool IncludeInInsertValues(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsHidden && !row.IsExpression && !row.IsRowVersion && !row.IsReadOnly;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000A93EF File Offset: 0x000A75EF
		private bool IncludeInUpdateSet(DbSchemaRow row)
		{
			return !row.IsAutoIncrement && !row.IsRowVersion && !row.IsHidden && !row.IsReadOnly;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000A9414 File Offset: 0x000A7614
		private bool IncludeInWhereClause(DbSchemaRow row, bool isUpdate)
		{
			bool flag = this.IncrementWhereCount(row);
			if (!flag || !row.IsHidden)
			{
				if (!flag && ConflictOption.CompareAllSearchableValues == this.ConflictOption)
				{
					flag = !row.IsLong && !row.IsRowVersion && !row.IsHidden;
				}
				return flag;
			}
			if (ConflictOption.CompareRowVersion == this.ConflictOption)
			{
				throw ADP.DynamicSQLNoKeyInfoRowVersionUpdate();
			}
			throw ADP.DynamicSQLNoKeyInfoUpdate();
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000A9474 File Offset: 0x000A7674
		private bool IncrementWhereCount(DbSchemaRow row)
		{
			ConflictOption conflictOption = this.ConflictOption;
			switch (conflictOption)
			{
			case ConflictOption.CompareAllSearchableValues:
			case ConflictOption.OverwriteChanges:
				return (row.IsKey || row.IsUnique) && !row.IsLong && !row.IsRowVersion;
			case ConflictOption.CompareRowVersion:
				return (((row.IsKey || row.IsUnique) && !this._hasPartialPrimaryKey) || row.IsRowVersion) && !row.IsLong;
			default:
				throw ADP.InvalidConflictOptions(conflictOption);
			}
		}

		/// <summary>Resets the <see cref="P:System.Data.Common.DbCommand.CommandTimeout" />, <see cref="P:System.Data.Common.DbCommand.Transaction" />, <see cref="P:System.Data.Common.DbCommand.CommandType" />, and <see cref="T:System.Data.UpdateRowSource" /> properties on the <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> instance to use for each insert, update, or delete operation. Passing a null value allows the <see cref="M:System.Data.Common.DbCommandBuilder.InitializeCommand(System.Data.Common.DbCommand)" /> method to create a <see cref="T:System.Data.Common.DbCommand" /> object based on the Select command associated with the <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> to be used by the command builder for the corresponding insert, update, or delete command.</param>
		// Token: 0x06002504 RID: 9476 RVA: 0x000A94F4 File Offset: 0x000A76F4
		protected virtual DbCommand InitializeCommand(DbCommand command)
		{
			if (command == null)
			{
				DbCommand selectCommand = this.GetSelectCommand();
				command = selectCommand.Connection.CreateCommand();
				command.CommandTimeout = selectCommand.CommandTimeout;
				command.Transaction = selectCommand.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.UpdatedRowSource = UpdateRowSource.None;
			return command;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000A953F File Offset: 0x000A773F
		private string QuotedColumn(string column)
		{
			return ADP.BuildQuotedString(this.QuotePrefix, this.QuoteSuffix, column);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier, including properly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are properly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002506 RID: 9478 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Clears the commands associated with this <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002507 RID: 9479 RVA: 0x000A9554 File Offset: 0x000A7754
		public virtual void RefreshSchema()
		{
			this._dbSchemaTable = null;
			this._dbSchemaRows = null;
			this._sourceColumnNames = null;
			this._quotedBaseTableName = null;
			DbDataAdapter dataAdapter = this.DataAdapter;
			if (dataAdapter != null)
			{
				if (this.InsertCommand == dataAdapter.InsertCommand)
				{
					dataAdapter.InsertCommand = null;
				}
				if (this.UpdateCommand == dataAdapter.UpdateCommand)
				{
					dataAdapter.UpdateCommand = null;
				}
				if (this.DeleteCommand == dataAdapter.DeleteCommand)
				{
					dataAdapter.DeleteCommand = null;
				}
			}
			DbCommand dbCommand;
			if ((dbCommand = this.InsertCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.UpdateCommand) != null)
			{
				dbCommand.Dispose();
			}
			if ((dbCommand = this.DeleteCommand) != null)
			{
				dbCommand.Dispose();
			}
			this.InsertCommand = null;
			this.UpdateCommand = null;
			this.DeleteCommand = null;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000A960C File Offset: 0x000A780C
		private static void RemoveExtraParameters(DbCommand command, int usedParameterCount)
		{
			for (int i = command.Parameters.Count - 1; i >= usedParameterCount; i--)
			{
				command.Parameters.RemoveAt(i);
			}
		}

		/// <summary>Adds an event handler for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event.</summary>
		/// <param name="rowUpdatingEvent">A <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> instance containing information about the event.</param>
		// Token: 0x06002509 RID: 9481 RVA: 0x000A9640 File Offset: 0x000A7840
		protected void RowUpdatingHandler(RowUpdatingEventArgs rowUpdatingEvent)
		{
			if (rowUpdatingEvent == null)
			{
				throw ADP.ArgumentNull("rowUpdatingEvent");
			}
			try
			{
				if (rowUpdatingEvent.Status == UpdateStatus.Continue)
				{
					StatementType statementType = rowUpdatingEvent.StatementType;
					DbCommand dbCommand = (DbCommand)rowUpdatingEvent.Command;
					if (dbCommand != null)
					{
						switch (statementType)
						{
						case StatementType.Select:
							return;
						case StatementType.Insert:
							dbCommand = this.InsertCommand;
							break;
						case StatementType.Update:
							dbCommand = this.UpdateCommand;
							break;
						case StatementType.Delete:
							dbCommand = this.DeleteCommand;
							break;
						default:
							throw ADP.InvalidStatementType(statementType);
						}
						if (dbCommand != rowUpdatingEvent.Command)
						{
							dbCommand = (DbCommand)rowUpdatingEvent.Command;
							if (dbCommand != null && dbCommand.Connection == null)
							{
								DbDataAdapter dataAdapter = this.DataAdapter;
								DbCommand dbCommand2 = ((dataAdapter != null) ? dataAdapter.SelectCommand : null);
								if (dbCommand2 != null)
								{
									dbCommand.Connection = dbCommand2.Connection;
								}
							}
						}
						else
						{
							dbCommand = null;
						}
					}
					if (dbCommand == null)
					{
						this.RowUpdatingHandlerBuilder(rowUpdatingEvent);
					}
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				ADP.TraceExceptionForCapture(ex);
				rowUpdatingEvent.Status = UpdateStatus.ErrorsOccurred;
				rowUpdatingEvent.Errors = ex;
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000A9758 File Offset: 0x000A7958
		private void RowUpdatingHandlerBuilder(RowUpdatingEventArgs rowUpdatingEvent)
		{
			DataRow row = rowUpdatingEvent.Row;
			this.BuildCache(false, row, false);
			DbCommand dbCommand;
			switch (rowUpdatingEvent.StatementType)
			{
			case StatementType.Insert:
				dbCommand = this.BuildInsertCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Update:
				dbCommand = this.BuildUpdateCommand(rowUpdatingEvent.TableMapping, row);
				break;
			case StatementType.Delete:
				dbCommand = this.BuildDeleteCommand(rowUpdatingEvent.TableMapping, row);
				break;
			default:
				throw ADP.InvalidStatementType(rowUpdatingEvent.StatementType);
			}
			if (dbCommand == null)
			{
				if (row != null)
				{
					row.AcceptChanges();
				}
				rowUpdatingEvent.Status = UpdateStatus.SkipCurrentRow;
			}
			rowUpdatingEvent.Command = dbCommand;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including properly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes properly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600250B RID: 9483 RVA: 0x000621D6 File Offset: 0x000603D6
		public virtual string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Allows the provider implementation of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class to handle additional parameter properties.</summary>
		/// <param name="parameter">A <see cref="T:System.Data.Common.DbParameter" /> to which the additional modifications are applied. </param>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> from the schema table provided by <see cref="M:System.Data.Common.DbDataReader.GetSchemaTable" />. </param>
		/// <param name="statementType">The type of command being generated; INSERT, UPDATE or DELETE. </param>
		/// <param name="whereClause">true if the parameter is part of the update or delete WHERE clause, false if it is part of the insert or update values. </param>
		// Token: 0x0600250C RID: 9484
		protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);

		/// <summary>Returns the name of the specified parameter in the format of @p#. Use when building a custom command builder.</summary>
		/// <returns>The name of the parameter with the specified number appended as part of the parameter name.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name..</param>
		// Token: 0x0600250D RID: 9485
		protected abstract string GetParameterName(int parameterOrdinal);

		/// <summary>Returns the full parameter name, given the partial parameter name.</summary>
		/// <returns>The full parameter name corresponding to the partial parameter name requested.</returns>
		/// <param name="parameterName">The partial name of the parameter.</param>
		// Token: 0x0600250E RID: 9486
		protected abstract string GetParameterName(string parameterName);

		/// <summary>Returns the placeholder for the parameter in the associated SQL statement.</summary>
		/// <returns>The name of the parameter with the specified number appended.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name.</param>
		// Token: 0x0600250F RID: 9487
		protected abstract string GetParameterPlaceholder(int parameterOrdinal);

		/// <summary>Registers the <see cref="T:System.Data.Common.DbCommandBuilder" /> to handle the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event for a <see cref="T:System.Data.Common.DbDataAdapter" />. </summary>
		/// <param name="adapter">The <see cref="T:System.Data.Common.DbDataAdapter" /> to be used for the update.</param>
		// Token: 0x06002510 RID: 9488
		protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);

		// Token: 0x040017E6 RID: 6118
		private const string DeleteFrom = "DELETE FROM ";

		// Token: 0x040017E7 RID: 6119
		private const string InsertInto = "INSERT INTO ";

		// Token: 0x040017E8 RID: 6120
		private const string DefaultValues = " DEFAULT VALUES";

		// Token: 0x040017E9 RID: 6121
		private const string Values = " VALUES ";

		// Token: 0x040017EA RID: 6122
		private const string Update = "UPDATE ";

		// Token: 0x040017EB RID: 6123
		private const string Set = " SET ";

		// Token: 0x040017EC RID: 6124
		private const string Where = " WHERE ";

		// Token: 0x040017ED RID: 6125
		private const string SpaceLeftParenthesis = " (";

		// Token: 0x040017EE RID: 6126
		private const string Comma = ", ";

		// Token: 0x040017EF RID: 6127
		private const string Equal = " = ";

		// Token: 0x040017F0 RID: 6128
		private const string LeftParenthesis = "(";

		// Token: 0x040017F1 RID: 6129
		private const string RightParenthesis = ")";

		// Token: 0x040017F2 RID: 6130
		private const string NameSeparator = ".";

		// Token: 0x040017F3 RID: 6131
		private const string IsNull = " IS NULL";

		// Token: 0x040017F4 RID: 6132
		private const string EqualOne = " = 1";

		// Token: 0x040017F5 RID: 6133
		private const string And = " AND ";

		// Token: 0x040017F6 RID: 6134
		private const string Or = " OR ";

		// Token: 0x040017F7 RID: 6135
		private DbDataAdapter _dataAdapter;

		// Token: 0x040017F8 RID: 6136
		private DbCommand _insertCommand;

		// Token: 0x040017F9 RID: 6137
		private DbCommand _updateCommand;

		// Token: 0x040017FA RID: 6138
		private DbCommand _deleteCommand;

		// Token: 0x040017FB RID: 6139
		private MissingMappingAction _missingMappingAction;

		// Token: 0x040017FC RID: 6140
		private ConflictOption _conflictDetection = ConflictOption.CompareAllSearchableValues;

		// Token: 0x040017FD RID: 6141
		private bool _setAllValues;

		// Token: 0x040017FE RID: 6142
		private bool _hasPartialPrimaryKey;

		// Token: 0x040017FF RID: 6143
		private DataTable _dbSchemaTable;

		// Token: 0x04001800 RID: 6144
		private DbSchemaRow[] _dbSchemaRows;

		// Token: 0x04001801 RID: 6145
		private string[] _sourceColumnNames;

		// Token: 0x04001802 RID: 6146
		private DbCommandBuilder.ParameterNames _parameterNames;

		// Token: 0x04001803 RID: 6147
		private string _quotedBaseTableName;

		// Token: 0x04001804 RID: 6148
		private CatalogLocation _catalogLocation = CatalogLocation.Start;

		// Token: 0x04001805 RID: 6149
		private string _catalogSeparator = ".";

		// Token: 0x04001806 RID: 6150
		private string _schemaSeparator = ".";

		// Token: 0x04001807 RID: 6151
		private string _quotePrefix = string.Empty;

		// Token: 0x04001808 RID: 6152
		private string _quoteSuffix = string.Empty;

		// Token: 0x04001809 RID: 6153
		private string _parameterNamePattern;

		// Token: 0x0400180A RID: 6154
		private string _parameterMarkerFormat;

		// Token: 0x0400180B RID: 6155
		private int _parameterNameMaxLength;

		// Token: 0x0200032B RID: 811
		private class ParameterNames
		{
			// Token: 0x06002511 RID: 9489 RVA: 0x000A97E8 File Offset: 0x000A79E8
			internal ParameterNames(DbCommandBuilder dbCommandBuilder, DbSchemaRow[] schemaRows)
			{
				this._dbCommandBuilder = dbCommandBuilder;
				this._baseParameterNames = new string[schemaRows.Length];
				this._originalParameterNames = new string[schemaRows.Length];
				this._nullParameterNames = new string[schemaRows.Length];
				this._isMutatedName = new bool[schemaRows.Length];
				this._count = schemaRows.Length;
				this._parameterNameParser = new Regex(this._dbCommandBuilder.ParameterNamePattern, RegexOptions.ExplicitCapture | RegexOptions.Singleline);
				this.SetAndValidateNamePrefixes();
				this._adjustedParameterNameMaxLength = this.GetAdjustedParameterNameMaxLength();
				for (int i = 0; i < schemaRows.Length; i++)
				{
					if (schemaRows[i] != null)
					{
						bool flag = false;
						string text = schemaRows[i].ColumnName;
						if ((this._originalPrefix == null || !text.StartsWith(this._originalPrefix, StringComparison.OrdinalIgnoreCase)) && (this._isNullPrefix == null || !text.StartsWith(this._isNullPrefix, StringComparison.OrdinalIgnoreCase)))
						{
							if (text.IndexOf(' ') >= 0)
							{
								text = text.Replace(' ', '_');
								flag = true;
							}
							if (this._parameterNameParser.IsMatch(text) && text.Length <= this._adjustedParameterNameMaxLength)
							{
								this._baseParameterNames[i] = text;
								this._isMutatedName[i] = flag;
							}
						}
					}
				}
				this.EliminateConflictingNames();
				for (int j = 0; j < schemaRows.Length; j++)
				{
					if (this._baseParameterNames[j] != null)
					{
						if (this._originalPrefix != null)
						{
							this._originalParameterNames[j] = this._originalPrefix + this._baseParameterNames[j];
						}
						if (this._isNullPrefix != null && schemaRows[j].AllowDBNull)
						{
							this._nullParameterNames[j] = this._isNullPrefix + this._baseParameterNames[j];
						}
					}
				}
				this.ApplyProviderSpecificFormat();
				this.GenerateMissingNames(schemaRows);
			}

			// Token: 0x06002512 RID: 9490 RVA: 0x000A9980 File Offset: 0x000A7B80
			private void SetAndValidateNamePrefixes()
			{
				if (this._parameterNameParser.IsMatch("IsNull_"))
				{
					this._isNullPrefix = "IsNull_";
				}
				else if (this._parameterNameParser.IsMatch("isnull"))
				{
					this._isNullPrefix = "isnull";
				}
				else if (this._parameterNameParser.IsMatch("ISNULL"))
				{
					this._isNullPrefix = "ISNULL";
				}
				else
				{
					this._isNullPrefix = null;
				}
				if (this._parameterNameParser.IsMatch("Original_"))
				{
					this._originalPrefix = "Original_";
					return;
				}
				if (this._parameterNameParser.IsMatch("original"))
				{
					this._originalPrefix = "original";
					return;
				}
				if (this._parameterNameParser.IsMatch("ORIGINAL"))
				{
					this._originalPrefix = "ORIGINAL";
					return;
				}
				this._originalPrefix = null;
			}

			// Token: 0x06002513 RID: 9491 RVA: 0x000A9A54 File Offset: 0x000A7C54
			private void ApplyProviderSpecificFormat()
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] != null)
					{
						this._baseParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._baseParameterNames[i]);
					}
					if (this._originalParameterNames[i] != null)
					{
						this._originalParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._originalParameterNames[i]);
					}
					if (this._nullParameterNames[i] != null)
					{
						this._nullParameterNames[i] = this._dbCommandBuilder.GetParameterName(this._nullParameterNames[i]);
					}
				}
			}

			// Token: 0x06002514 RID: 9492 RVA: 0x000A9AE4 File Offset: 0x000A7CE4
			private void EliminateConflictingNames()
			{
				for (int i = 0; i < this._count - 1; i++)
				{
					string text = this._baseParameterNames[i];
					if (text != null)
					{
						for (int j = i + 1; j < this._count; j++)
						{
							if (ADP.CompareInsensitiveInvariant(text, this._baseParameterNames[j]))
							{
								int num = (this._isMutatedName[j] ? j : i);
								this._baseParameterNames[num] = null;
							}
						}
					}
				}
			}

			// Token: 0x06002515 RID: 9493 RVA: 0x000A9B4C File Offset: 0x000A7D4C
			internal void GenerateMissingNames(DbSchemaRow[] schemaRows)
			{
				for (int i = 0; i < this._baseParameterNames.Length; i++)
				{
					if (this._baseParameterNames[i] == null)
					{
						this._baseParameterNames[i] = this.GetNextGenericParameterName();
						this._originalParameterNames[i] = this.GetNextGenericParameterName();
						if (schemaRows[i] != null && schemaRows[i].AllowDBNull)
						{
							this._nullParameterNames[i] = this.GetNextGenericParameterName();
						}
					}
				}
			}

			// Token: 0x06002516 RID: 9494 RVA: 0x000A9BB4 File Offset: 0x000A7DB4
			private int GetAdjustedParameterNameMaxLength()
			{
				int num = Math.Max((this._isNullPrefix != null) ? this._isNullPrefix.Length : 0, (this._originalPrefix != null) ? this._originalPrefix.Length : 0) + this._dbCommandBuilder.GetParameterName("").Length;
				return this._dbCommandBuilder.ParameterNameMaxLength - num;
			}

			// Token: 0x06002517 RID: 9495 RVA: 0x000A9C18 File Offset: 0x000A7E18
			private string GetNextGenericParameterName()
			{
				bool flag;
				string parameterName;
				do
				{
					flag = false;
					this._genericParameterCount++;
					parameterName = this._dbCommandBuilder.GetParameterName(this._genericParameterCount);
					for (int i = 0; i < this._baseParameterNames.Length; i++)
					{
						if (ADP.CompareInsensitiveInvariant(this._baseParameterNames[i], parameterName))
						{
							flag = true;
							break;
						}
					}
				}
				while (flag);
				return parameterName;
			}

			// Token: 0x06002518 RID: 9496 RVA: 0x000A9C72 File Offset: 0x000A7E72
			internal string GetBaseParameterName(int index)
			{
				return this._baseParameterNames[index];
			}

			// Token: 0x06002519 RID: 9497 RVA: 0x000A9C7C File Offset: 0x000A7E7C
			internal string GetOriginalParameterName(int index)
			{
				return this._originalParameterNames[index];
			}

			// Token: 0x0600251A RID: 9498 RVA: 0x000A9C86 File Offset: 0x000A7E86
			internal string GetNullParameterName(int index)
			{
				return this._nullParameterNames[index];
			}

			// Token: 0x0400180C RID: 6156
			private const string DefaultOriginalPrefix = "Original_";

			// Token: 0x0400180D RID: 6157
			private const string DefaultIsNullPrefix = "IsNull_";

			// Token: 0x0400180E RID: 6158
			private const string AlternativeOriginalPrefix = "original";

			// Token: 0x0400180F RID: 6159
			private const string AlternativeIsNullPrefix = "isnull";

			// Token: 0x04001810 RID: 6160
			private const string AlternativeOriginalPrefix2 = "ORIGINAL";

			// Token: 0x04001811 RID: 6161
			private const string AlternativeIsNullPrefix2 = "ISNULL";

			// Token: 0x04001812 RID: 6162
			private string _originalPrefix;

			// Token: 0x04001813 RID: 6163
			private string _isNullPrefix;

			// Token: 0x04001814 RID: 6164
			private Regex _parameterNameParser;

			// Token: 0x04001815 RID: 6165
			private DbCommandBuilder _dbCommandBuilder;

			// Token: 0x04001816 RID: 6166
			private string[] _baseParameterNames;

			// Token: 0x04001817 RID: 6167
			private string[] _originalParameterNames;

			// Token: 0x04001818 RID: 6168
			private string[] _nullParameterNames;

			// Token: 0x04001819 RID: 6169
			private bool[] _isMutatedName;

			// Token: 0x0400181A RID: 6170
			private int _count;

			// Token: 0x0400181B RID: 6171
			private int _genericParameterCount;

			// Token: 0x0400181C RID: 6172
			private int _adjustedParameterNameMaxLength;
		}
	}
}
