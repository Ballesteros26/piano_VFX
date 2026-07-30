using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x0200018E RID: 398
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x0005D568 File Offset: 0x0005B768
		private SqlConnectionFactory()
		{
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0005BF88 File Offset: 0x0005A188
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0005D570 File Offset: 0x0005B770
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection, null);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0005D580 File Offset: 0x0005B780
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlConnectionPoolKey sqlConnectionPoolKey = (SqlConnectionPoolKey)poolKey;
			SessionData sessionData = null;
			SqlConnection sqlConnection = (SqlConnection)owningConnection;
			bool flag = sqlConnection != null && sqlConnection._applyTransientFaultHandling;
			SqlConnectionString sqlConnectionString2 = null;
			if (userOptions != null)
			{
				sqlConnectionString2 = (SqlConnectionString)userOptions;
			}
			else if (sqlConnection != null)
			{
				sqlConnectionString2 = (SqlConnectionString)sqlConnection.UserConnectionOptions;
			}
			if (sqlConnection != null)
			{
				sessionData = sqlConnection._recoverySessionData;
			}
			bool flag2 = false;
			DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
			if (sqlConnectionString.IntegratedSecurity)
			{
				if (pool != null)
				{
					dbConnectionPoolIdentity = pool.Identity;
				}
				else
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
				}
			}
			if (sqlConnectionString.UserInstance)
			{
				flag2 = true;
				string text;
				if (pool == null || (pool != null && pool.Count <= 0))
				{
					SqlInternalConnectionTds sqlInternalConnectionTds = null;
					try
					{
						SqlConnectionString sqlConnectionString3 = new SqlConnectionString(sqlConnectionString, sqlConnectionString.DataSource, true, new bool?(false));
						sqlInternalConnectionTds = new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString3, null, false, null, null, flag);
						text = sqlInternalConnectionTds.InstanceName;
						if (!text.StartsWith("\\\\.\\", StringComparison.Ordinal))
						{
							throw SQL.NonLocalSSEInstance();
						}
						if (pool != null)
						{
							((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName = text;
						}
						goto IL_0113;
					}
					finally
					{
						if (sqlInternalConnectionTds != null)
						{
							sqlInternalConnectionTds.Dispose();
						}
					}
				}
				text = ((SqlConnectionPoolProviderInfo)pool.ProviderInfo).InstanceName;
				IL_0113:
				sqlConnectionString = new SqlConnectionString(sqlConnectionString, text, false, null);
				poolGroupProviderInfo = null;
			}
			return new SqlInternalConnectionTds(dbConnectionPoolIdentity, sqlConnectionString, poolGroupProviderInfo, flag2, sqlConnectionString2, sessionData, flag);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0005D6E0 File Offset: 0x0005B8E0
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				dbConnectionPoolProviderInfo = new SqlConnectionPoolProviderInfo();
			}
			return dbConnectionPoolProviderInfo;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0005D704 File Offset: 0x0005B904
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)connectionOptions;
			DbConnectionPoolGroupOptions dbConnectionPoolGroupOptions = null;
			if (sqlConnectionString.Pooling)
			{
				int num = sqlConnectionString.ConnectTimeout;
				if (0 < num && num < 2147483)
				{
					num *= 1000;
				}
				else if (num >= 2147483)
				{
					num = int.MaxValue;
				}
				dbConnectionPoolGroupOptions = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist);
			}
			return dbConnectionPoolGroupOptions;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0005D773 File Offset: 0x0005B973
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0005D780 File Offset: 0x0005B980
		internal static SqlConnectionString FindSqlConnectionOptions(SqlConnectionPoolKey key)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(key);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(key.ConnectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0005D7BC File Offset: 0x0005B9BC
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0005D7DC File Offset: 0x0005B9DC
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0005D7FC File Offset: 0x0005B9FC
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0005D81C File Offset: 0x0005BA1C
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0005D83C File Offset: 0x0005BA3C
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0005D85C File Offset: 0x0005BA5C
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0005D880 File Offset: 0x0005BA80
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0005D89E File Offset: 0x0005BA9E
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.SqlClient.SqlMetaData.xml");
			cacheMetaDataFactory = true;
			return new SqlMetaDataFactory(manifestResourceStream, internalConnection.ServerVersion, internalConnection.ServerVersion);
		}

		// Token: 0x04000C35 RID: 3125
		private const string _metaDataXml = "MetaDataXml";

		// Token: 0x04000C36 RID: 3126
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();
	}
}
