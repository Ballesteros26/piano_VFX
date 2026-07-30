using System;
using System.Data.Common;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated SQL Server database. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000183 RID: 387
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		// Token: 0x06001203 RID: 4611 RVA: 0x0005B333 File Offset: 0x00059533
		public SqlCommandBuilder()
		{
			GC.SuppressFinalize(this);
			base.QuotePrefix = "[";
			base.QuoteSuffix = "]";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class with the associated <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</summary>
		/// <param name="adapter">The name of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		// Token: 0x06001204 RID: 4612 RVA: 0x0005B357 File Offset: 0x00059557
		public SqlCommandBuilder(SqlDataAdapter adapter)
			: this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0000EF2B File Offset: 0x0000D12B
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x0005B366 File Offset: 0x00059566
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return CatalogLocation.Start;
			}
			set
			{
				if (CatalogLocation.Start != value)
				{
					throw ADP.SingleValuedProperty("CatalogLocation", "Start");
				}
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A string that indicates the catalog separator for use with an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0005B37C File Offset: 0x0005957C
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x0005B383 File Offset: 0x00059583
		public override string CatalogSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("CatalogSeparator", ".");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0005B3A2 File Offset: 0x000595A2
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0005B3AF File Offset: 0x000595AF
		public new SqlDataAdapter DataAdapter
		{
			get
			{
				return (SqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		/// <summary>Gets or sets the starting character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The starting character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an INSERT, UPDATE, or DELETE command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0005B3B8 File Offset: 0x000595B8
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0005B3C0 File Offset: 0x000595C0
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				if ("[" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuotePrefix", "[", "\"");
				}
				base.QuotePrefix = value;
			}
		}

		/// <summary>Gets or sets the ending character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0005B3F8 File Offset: 0x000595F8
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0005B400 File Offset: 0x00059600
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				if ("]" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuoteSuffix", "]", "\"");
				}
				base.QuoteSuffix = value;
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0005B37C File Offset: 0x0005957C
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0005B438 File Offset: 0x00059638
		public override string SchemaSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("SchemaSeparator", ".");
				}
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0005B457 File Offset: 0x00059657
		private void SqlRowUpdatingHandler(object sender, SqlRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001212 RID: 4626 RVA: 0x0005B460 File Offset: 0x00059660
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001213 RID: 4627 RVA: 0x0005B46D File Offset: 0x0005966D
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001214 RID: 4628 RVA: 0x0005B47B File Offset: 0x0005967B
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001215 RID: 4629 RVA: 0x0005B488 File Offset: 0x00059688
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001216 RID: 4630 RVA: 0x0005B496 File Offset: 0x00059696
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001217 RID: 4631 RVA: 0x0005B4A3 File Offset: 0x000596A3
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0005B4B4 File Offset: 0x000596B4
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			SqlParameter sqlParameter = (SqlParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			sqlParameter.SqlDbType = (SqlDbType)obj;
			sqlParameter.Offset = 0;
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				sqlParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				sqlParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0005B546 File Offset: 0x00059746
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0005B55E File Offset: 0x0005975E
		protected override string GetParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0005B546 File Offset: 0x00059746
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0005B56B File Offset: 0x0005976B
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if (("\"" == quotePrefix && "\"" != quoteSuffix) || ("[" == quotePrefix && "]" != quoteSuffix))
			{
				throw ADP.InvalidPrefixSuffix();
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.SqlClient.SqlCommand" /> and populates the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the <see cref="T:System.Data.SqlClient.SqlCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The command text is not a valid stored procedure name. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600121D RID: 4637 RVA: 0x0005B5A8 File Offset: 0x000597A8
		public static void DeriveParameters(SqlCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				command.DeriveParameters();
			}
			catch (OutOfMemoryException ex)
			{
				if (command != null)
				{
					SqlConnection connection = command.Connection;
					if (connection != null)
					{
						connection.Abort(ex);
					}
				}
				throw;
			}
			catch (StackOverflowException ex2)
			{
				if (command != null)
				{
					SqlConnection connection2 = command.Connection;
					if (connection2 != null)
					{
						connection2.Abort(ex2);
					}
				}
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				if (command != null)
				{
					SqlConnection connection3 = command.Connection;
					if (connection3 != null)
					{
						connection3.Abort(ex3);
					}
				}
				throw;
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0005B640 File Offset: 0x00059840
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			SqlCommand sqlCommand = srcCommand as SqlCommand;
			SqlNotificationRequest notification = sqlCommand.Notification;
			sqlCommand.Notification = null;
			DataTable schemaTable;
			try
			{
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTable = sqlDataReader.GetSchemaTable();
				}
			}
			finally
			{
				sqlCommand.Notification = notification;
			}
			return schemaTable;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0005B6A4 File Offset: 0x000598A4
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			return (SqlCommand)base.InitializeCommand(command);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001220 RID: 4640 RVA: 0x0005B6B4 File Offset: 0x000598B4
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			return ADP.BuildQuotedString(quotePrefix, quoteSuffix, unquotedIdentifier);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0005B6EA File Offset: 0x000598EA
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqlDataAdapter)adapter).RowUpdating -= this.SqlRowUpdatingHandler;
				return;
			}
			((SqlDataAdapter)adapter).RowUpdating += this.SqlRowUpdatingHandler;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes properly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001222 RID: 4642 RVA: 0x0005B724 File Offset: 0x00059924
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			string text;
			ADP.RemoveStringQuotes(quotePrefix, quoteSuffix, quotedIdentifier, out text);
			return text;
		}
	}
}
