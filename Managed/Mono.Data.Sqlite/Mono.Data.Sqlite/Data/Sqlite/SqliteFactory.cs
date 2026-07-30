using System;
using System.Data.Common;
using System.Reflection;
using System.Security.Permissions;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000004 RID: 4
	public sealed class SqliteFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00006564 File Offset: 0x00004764
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ISQLiteSchemaExtensions) || (SqliteFactory._dbProviderServicesType != null && serviceType == SqliteFactory._dbProviderServicesType))
			{
				return this.GetSQLiteProviderServicesInstance();
			}
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000659C File Offset: 0x0000479C
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object GetSQLiteProviderServicesInstance()
		{
			if (SqliteFactory._sqliteServices == null)
			{
				Type type = Type.GetType("Mono.Data.Sqlite.SQLiteProviderServices, Mono.Data.Sqlite.Linq, Version=2.0.38.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", false);
				if (type != null)
				{
					SqliteFactory._sqliteServices = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
				}
			}
			return SqliteFactory._sqliteServices;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000065E2 File Offset: 0x000047E2
		public override DbCommand CreateCommand()
		{
			return new SqliteCommand();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000065E9 File Offset: 0x000047E9
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqliteCommandBuilder();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000065F0 File Offset: 0x000047F0
		public override DbConnection CreateConnection()
		{
			return new SqliteConnection();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000065F7 File Offset: 0x000047F7
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqliteConnectionStringBuilder();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000065FE File Offset: 0x000047FE
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqliteDataAdapter();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00006605 File Offset: 0x00004805
		public override DbParameter CreateParameter()
		{
			return new SqliteParameter();
		}

		// Token: 0x0400003F RID: 63
		private static Type _dbProviderServicesType = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);

		// Token: 0x04000040 RID: 64
		private static object _sqliteServices;

		// Token: 0x04000041 RID: 65
		public static readonly SqliteFactory Instance = new SqliteFactory();
	}
}
