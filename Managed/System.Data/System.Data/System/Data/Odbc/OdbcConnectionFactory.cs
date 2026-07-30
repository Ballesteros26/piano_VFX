using System;
using System.Data.Common;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x0200028F RID: 655
	internal sealed class OdbcConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0005D568 File Offset: 0x0005B768
		private OdbcConnectionFactory()
		{
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00089CE2 File Offset: 0x00087EE2
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return OdbcFactory.Instance;
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00089CE9 File Offset: 0x00087EE9
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningObject)
		{
			return new OdbcConnectionOpen(owningObject as OdbcConnection, options as OdbcConnectionString);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00089CFD File Offset: 0x00087EFD
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new OdbcConnectionString(connectionString, previous != null);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00004526 File Offset: 0x00002726
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00089D09 File Offset: 0x00087F09
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new OdbcConnectionPoolGroupProviderInfo();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00089D10 File Offset: 0x00087F10
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00089D30 File Offset: 0x00087F30
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			OdbcConnection odbcConnection = connection as OdbcConnection;
			if (odbcConnection != null)
			{
				return odbcConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00089D50 File Offset: 0x00087F50
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PermissionDemand();
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00089D70 File Offset: 0x00087F70
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			OdbcConnection odbcConnection = outerConnection as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00089D90 File Offset: 0x00087F90
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00089DB0 File Offset: 0x00087FB0
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			return odbcConnection != null && odbcConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00089DD4 File Offset: 0x00087FD4
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			OdbcConnection odbcConnection = owningObject as OdbcConnection;
			if (odbcConnection != null)
			{
				odbcConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x040014E4 RID: 5348
		public static readonly OdbcConnectionFactory SingletonInstance = new OdbcConnectionFactory();
	}
}
