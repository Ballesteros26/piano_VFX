using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200028D RID: 653
	public sealed class OdbcCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class.</summary>
		// Token: 0x06001B3C RID: 6972 RVA: 0x0008888C File Offset: 0x00086A8C
		public OdbcCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class with the associated <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object to associate with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />.</param>
		// Token: 0x06001B3D RID: 6973 RVA: 0x0008889A File Offset: 0x00086A9A
		public OdbcCommandBuilder(OdbcDataAdapter adapter)
			: this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object for which this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> object will generate SQL statements.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object that is associated with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000888A9 File Offset: 0x00086AA9
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0005B3AF File Offset: 0x000595AF
		public new OdbcDataAdapter DataAdapter
		{
			get
			{
				return base.DataAdapter as OdbcDataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0005B457 File Offset: 0x00059657
		private void OdbcRowUpdatingHandler(object sender, OdbcRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B41 RID: 6977 RVA: 0x000888B6 File Offset: 0x00086AB6
		public new OdbcCommand GetInsertCommand()
		{
			return (OdbcCommand)base.GetInsertCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B42 RID: 6978 RVA: 0x000888C3 File Offset: 0x00086AC3
		public new OdbcCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B43 RID: 6979 RVA: 0x000888D1 File Offset: 0x00086AD1
		public new OdbcCommand GetUpdateCommand()
		{
			return (OdbcCommand)base.GetUpdateCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B44 RID: 6980 RVA: 0x000888DE File Offset: 0x00086ADE
		public new OdbcCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B45 RID: 6981 RVA: 0x000888EC File Offset: 0x00086AEC
		public new OdbcCommand GetDeleteCommand()
		{
			return (OdbcCommand)base.GetDeleteCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B46 RID: 6982 RVA: 0x000888F9 File Offset: 0x00086AF9
		public new OdbcCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00088907 File Offset: 0x00086B07
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00005DA6 File Offset: 0x00003FA6
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0008891F File Offset: 0x00086B1F
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00088928 File Offset: 0x00086B28
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			OdbcParameter odbcParameter = (OdbcParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			odbcParameter.OdbcType = (OdbcType)obj;
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				odbcParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				odbcParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.Odbc.OdbcCommand" /> and populates the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the specified <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the <see cref="T:System.Data.Odbc.OdbcCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The underlying ODBC driver does not support returning stored procedure parameter information, or the command text is not a valid stored procedure name, or the <see cref="T:System.Data.CommandType" /> specified was not CommandType.StoredProcedure. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B4B RID: 6987 RVA: 0x000889B4 File Offset: 0x00086BB4
		public static void DeriveParameters(OdbcCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(command);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(command.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(command);
			}
			else
			{
				if (string.IsNullOrEmpty(command.CommandText))
				{
					throw ADP.CommandTextRequired("DeriveParameters");
				}
				OdbcConnection connection = command.Connection;
				if (connection == null)
				{
					throw ADP.ConnectionRequired("DeriveParameters");
				}
				ConnectionState state = connection.State;
				if (ConnectionState.Open != state)
				{
					throw ADP.OpenConnectionRequired("DeriveParameters", state);
				}
				OdbcParameter[] array = OdbcCommandBuilder.DeriveParametersFromStoredProcedure(connection, command);
				OdbcParameterCollection parameters = command.Parameters;
				parameters.Clear();
				int num = array.Length;
				if (0 < num)
				{
					for (int i = 0; i < array.Length; i++)
					{
						parameters.Add(array[i]);
					}
				}
				return;
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00088A88 File Offset: 0x00086C88
		private static OdbcParameter[] DeriveParametersFromStoredProcedure(OdbcConnection connection, OdbcCommand command)
		{
			List<OdbcParameter> list = new List<OdbcParameter>();
			CMDWrapper statementHandle = command.GetStatementHandle();
			OdbcStatementHandle statementHandle2 = statementHandle.StatementHandle;
			string text = connection.QuoteChar("DeriveParameters");
			string[] array = MultipartIdentifier.ParseMultipartIdentifier(command.CommandText, text, text, '.', 4, true, "OdbcCommandBuilder.DeriveParameters failed because the OdbcCommand.CommandText property value is an invalid multipart name", false);
			if (array[3] == null)
			{
				array[3] = command.CommandText;
			}
			ODBC32.RetCode retCode = statementHandle2.ProcedureColumns(array[1], array[2], array[3], null);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				connection.HandleError(statementHandle2, retCode);
			}
			using (OdbcDataReader odbcDataReader = new OdbcDataReader(command, statementHandle, CommandBehavior.Default))
			{
				odbcDataReader.FirstResult();
				int fieldCount = odbcDataReader.FieldCount;
				while (odbcDataReader.Read())
				{
					OdbcParameter odbcParameter = new OdbcParameter();
					odbcParameter.ParameterName = odbcDataReader.GetString(3);
					switch (odbcDataReader.GetInt16(4))
					{
					case 1:
						odbcParameter.Direction = ParameterDirection.Input;
						break;
					case 2:
						odbcParameter.Direction = ParameterDirection.InputOutput;
						break;
					case 4:
						odbcParameter.Direction = ParameterDirection.Output;
						break;
					case 5:
						odbcParameter.Direction = ParameterDirection.ReturnValue;
						break;
					}
					odbcParameter.OdbcType = TypeMap.FromSqlType((ODBC32.SQL_TYPE)odbcDataReader.GetInt16(5))._odbcType;
					odbcParameter.Size = odbcDataReader.GetInt32(7);
					OdbcType odbcType = odbcParameter.OdbcType;
					if (odbcType - OdbcType.Decimal <= 1)
					{
						odbcParameter.ScaleInternal = (byte)odbcDataReader.GetInt16(9);
						odbcParameter.PrecisionInternal = (byte)odbcDataReader.GetInt16(10);
					}
					list.Add(odbcParameter);
				}
			}
			retCode = statementHandle2.CloseCursor();
			return list.ToArray();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001B4D RID: 6989 RVA: 0x00088C20 File Offset: 0x00086E20
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <param name="connection">When a connection is passed, causes the managed wrapper to get the quote character from the ODBC driver, calling SQLGetInfo(SQL_IDENTIFIER_QUOTE_CHAR). When no connection is passed, the string is quoted using values from <see cref="P:System.Data.Common.DbCommandBuilder.QuotePrefix" /> and <see cref="P:System.Data.Common.DbCommandBuilder.QuoteSuffix" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001B4E RID: 6990 RVA: 0x00088C2C File Offset: 0x00086E2C
		public string QuoteIdentifier(string unquotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string text = this.QuotePrefix;
			string text2 = this.QuoteSuffix;
			if (string.IsNullOrEmpty(text))
			{
				if (connection == null)
				{
					OdbcDataAdapter dataAdapter = this.DataAdapter;
					OdbcConnection odbcConnection;
					if (dataAdapter == null)
					{
						odbcConnection = null;
					}
					else
					{
						OdbcCommand selectCommand = dataAdapter.SelectCommand;
						odbcConnection = ((selectCommand != null) ? selectCommand.Connection : null);
					}
					connection = odbcConnection;
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("QuoteIdentifier");
					}
				}
				text = connection.QuoteChar("QuoteIdentifier");
				text2 = text;
			}
			if (!string.IsNullOrEmpty(text) && text != " ")
			{
				return ADP.BuildQuotedString(text, text2, unquotedIdentifier);
			}
			return unquotedIdentifier;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00088CB8 File Offset: 0x00086EB8
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OdbcDataAdapter)adapter).RowUpdating -= this.OdbcRowUpdatingHandler;
				return;
			}
			((OdbcDataAdapter)adapter).RowUpdating += this.OdbcRowUpdatingHandler;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001B50 RID: 6992 RVA: 0x00088CF2 File Offset: 0x00086EF2
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.Odbc.OdbcConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001B51 RID: 6993 RVA: 0x00088CFC File Offset: 0x00086EFC
		public string UnquoteIdentifier(string quotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string text = this.QuotePrefix;
			string text2 = this.QuoteSuffix;
			if (string.IsNullOrEmpty(text))
			{
				if (connection == null)
				{
					OdbcDataAdapter dataAdapter = this.DataAdapter;
					OdbcConnection odbcConnection;
					if (dataAdapter == null)
					{
						odbcConnection = null;
					}
					else
					{
						OdbcCommand selectCommand = dataAdapter.SelectCommand;
						odbcConnection = ((selectCommand != null) ? selectCommand.Connection : null);
					}
					connection = odbcConnection;
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
					}
				}
				text = connection.QuoteChar("UnquoteIdentifier");
				text2 = text;
			}
			string text3;
			if (!string.IsNullOrEmpty(text) || text != " ")
			{
				ADP.RemoveStringQuotes(text, text2, quotedIdentifier, out text3);
			}
			else
			{
				text3 = quotedIdentifier;
			}
			return text3;
		}
	}
}
