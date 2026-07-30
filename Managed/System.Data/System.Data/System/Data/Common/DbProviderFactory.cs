using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Represents a set of methods for creating instances of a provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000350 RID: 848
	public abstract class DbProviderFactory
	{
		/// <summary>Returns a new instance of the provider's class that implements the provider's version of the <see cref="T:System.Security.CodeAccessPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.CodeAccessPermission" /> object for the specified <see cref="T:System.Security.Permissions.PermissionState" />.</returns>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002845 RID: 10309 RVA: 0x00004526 File Offset: 0x00002726
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			return null;
		}

		/// <summary>Specifies whether the specific <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>true if the instance of the <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class; otherwise false.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000061D5 File Offset: 0x000043D5
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommand" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002848 RID: 10312 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbCommand CreateCommand()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002849 RID: 10313 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284A RID: 10314 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbConnection CreateConnection()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284B RID: 10315 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataAdapter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284C RID: 10316 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbDataAdapter CreateDataAdapter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284D RID: 10317 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbParameter CreateParameter()
		{
			return null;
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataSourceEnumerator" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600284E RID: 10318 RVA: 0x00004526 File Offset: 0x00002726
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return null;
		}
	}
}
