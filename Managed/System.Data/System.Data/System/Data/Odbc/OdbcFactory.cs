using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of methods for creating instances of the ODBC provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A0 RID: 672
	public sealed class OdbcFactory : DbProviderFactory
	{
		// Token: 0x06001C97 RID: 7319 RVA: 0x00050F42 File Offset: 0x0004F142
		private OdbcFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C98 RID: 7320 RVA: 0x0008DD02 File Offset: 0x0008BF02
		public override DbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C99 RID: 7321 RVA: 0x0008DD09 File Offset: 0x0008BF09
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OdbcCommandBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001C9A RID: 7322 RVA: 0x0008DD10 File Offset: 0x0008BF10
		public override DbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001C9B RID: 7323 RVA: 0x0008DD17 File Offset: 0x0008BF17
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C9C RID: 7324 RVA: 0x0008DD1E File Offset: 0x0008BF1E
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C9D RID: 7325 RVA: 0x00087EE0 File Offset: 0x000860E0
		public override DbParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />. </returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C9E RID: 7326 RVA: 0x0008DD25 File Offset: 0x0008BF25
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OdbcPermission(state);
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Odbc.OdbcFactory" />, which can be used to retrieve strongly-typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001542 RID: 5442
		public static readonly OdbcFactory Instance = new OdbcFactory();
	}
}
