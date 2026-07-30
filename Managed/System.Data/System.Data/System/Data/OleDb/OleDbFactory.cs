using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of methods for creating instances of the OLEDB provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012C RID: 300
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x00050F42 File Offset: 0x0004F142
		internal OleDbFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F90 RID: 3984 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbCommand CreateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F91 RID: 3985 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F92 RID: 3986 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbConnection CreateConnection()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000F93 RID: 3987 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F94 RID: 3988 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbDataAdapter CreateDataAdapter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F95 RID: 3989 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override DbParameter CreateParameter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <returns>A strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F96 RID: 3990 RVA: 0x00050D50 File Offset: 0x0004EF50
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.OleDb.OleDbFactory" />. This can be used to retrieve strongly-typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A2C RID: 2604
		public static readonly OleDbFactory Instance;
	}
}
