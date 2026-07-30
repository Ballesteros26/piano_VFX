using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;

namespace System.Web.Security
{
	// Token: 0x020004B9 RID: 1209
	internal static class AspNetDBSchemaChecker
	{
		// Token: 0x06003679 RID: 13945 RVA: 0x0008E8C8 File Offset: 0x0008CAC8
		private static DbConnection CreateConnection(DbProviderFactory factory, string connStr)
		{
			DbConnection dbConnection = factory.CreateConnection();
			dbConnection.ConnectionString = connStr;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x0008E8E0 File Offset: 0x0008CAE0
		public static bool CheckMembershipSchemaVersion(DbProviderFactory factory, string connStr, string feature, string compatibleVersion)
		{
			bool flag;
			using (DbConnection dbConnection = AspNetDBSchemaChecker.CreateConnection(factory, connStr))
			{
				DbCommand dbCommand = factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "aspnet_CheckSchemaVersion";
				dbCommand.CommandType = CommandType.StoredProcedure;
				AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@Feature", ParameterDirection.Input, feature);
				AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@CompatibleSchemaVersion", ParameterDirection.Input, compatibleVersion);
				DbParameter dbParameter = AspNetDBSchemaChecker.AddParameter(factory, dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, null);
				try
				{
					dbCommand.ExecuteNonQuery();
				}
				catch (Exception)
				{
					throw new ProviderException("ASP.NET Membership schema not installed.");
				}
				if ((int)(dbParameter.Value ?? (-1)) == 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x0008E9A0 File Offset: 0x0008CBA0
		private static DbParameter AddParameter(DbProviderFactory factory, DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}
	}
}
