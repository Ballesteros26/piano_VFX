using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of methods for creating instances of the <see cref="N:System.Data.SqlClient" /> provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000169 RID: 361
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x00050F42 File Offset: 0x0004F142
		private SqlClientFactory()
		{
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600112F RID: 4399 RVA: 0x00056B2C File Offset: 0x00054D2C
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001130 RID: 4400 RVA: 0x00056B33 File Offset: 0x00054D33
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001131 RID: 4401 RVA: 0x00056B3A File Offset: 0x00054D3A
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001132 RID: 4402 RVA: 0x00056B41 File Offset: 0x00054D41
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001133 RID: 4403 RVA: 0x00056B48 File Offset: 0x00054D48
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001134 RID: 4404 RVA: 0x00056B4F File Offset: 0x00054D4F
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		/// <summary>Returns true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false .</summary>
		/// <returns>true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false.</returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</summary>
		/// <returns>A new data source enumerator.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001136 RID: 4406 RVA: 0x00056B56 File Offset: 0x00054D56
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		/// <summary>Returns a new <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>A strongly typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001137 RID: 4407 RVA: 0x00056B5D File Offset: 0x00054D5D
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IServiceProvider.GetService(System.Type)" />.</summary>
		/// <returns>A service object.</returns>
		/// <param name="serviceType">An object that specifies the type of service object to get. </param>
		// Token: 0x06001139 RID: 4409 RVA: 0x00056B71 File Offset: 0x00054D71
		object IServiceProvider.GetService(Type serviceType)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.SqlClient.SqlClientFactory" />. This can be used to retrieve strongly typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000B72 RID: 2930
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
