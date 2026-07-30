using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000123 RID: 291
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class.</summary>
		// Token: 0x06000EED RID: 3821 RVA: 0x00050D57 File Offset: 0x0004EF57
		public OleDbCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class with the associated <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		// Token: 0x06000EEE RID: 3822 RVA: 0x00050D57 File Offset: 0x0004EF57
		public OleDbCommandBuilder(OleDbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object for which SQL statements are automatically generated.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00050D50 File Offset: 0x0004EF50
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00005E03 File Offset: 0x00004003
		public new OleDbDataAdapter DataAdapter
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			throw ADP.OleDb();
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.OleDb.OleDbCommand" /> and populates the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.OleDb.OleDbCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the <see cref="T:System.Data.OleDb.OleDbCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The underlying OLE DB provider does not support returning stored procedure parameter information, the command text is not a valid stored procedure name, or the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> specified was not StoredProcedure.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000EF2 RID: 3826 RVA: 0x00050D50 File Offset: 0x0004EF50
		public static void DeriveParameters(OleDbCommand command)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EF3 RID: 3827 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetDeleteCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetInsertCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override string GetParameterName(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override string GetParameterName(string parameterName)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000EFA RID: 3834 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetUpdateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000EFB RID: 3835 RVA: 0x00050D50 File Offset: 0x0004EF50
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EFC RID: 3836 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The unquoted identifier to be returned in quoted format.</param>
		/// <param name="connection">When a connection is passed, causes the managed wrapper to get the quote character from the OLE DB provider. When no connection is passed, the string is quoted using values from <see cref="P:System.Data.Common.DbCommandBuilder.QuotePrefix" /> and <see cref="P:System.Data.Common.DbCommandBuilder.QuoteSuffix" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EFD RID: 3837 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00050D50 File Offset: 0x0004EF50
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EFF RID: 3839 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.OleDb.OleDbConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F00 RID: 3840 RVA: 0x00050D50 File Offset: 0x0004EF50
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}
	}
}
