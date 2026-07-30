using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Web.Hosting;
using System.Web.Properties;
using Mono.Data.Sqlite;

namespace System.Web.Security
{
	// Token: 0x020004D3 RID: 1235
	internal class SqliteRoleProvider : RoleProvider
	{
		// Token: 0x06003825 RID: 14373 RVA: 0x00096571 File Offset: 0x00094771
		private DbParameter AddParameter(DbCommand command, string parameterName)
		{
			return this.AddParameter(command, parameterName, null);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x0009657C File Offset: 0x0009477C
		private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			return this.AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x00096588 File Offset: 0x00094788
		private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000965C0 File Offset: 0x000947C0
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("Config", Resources.ErrArgumentNull);
			}
			if (string.IsNullOrEmpty(name))
			{
				name = Resources.RoleProviderDefaultName;
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", Resources.RoleProviderDefaultDescription);
			}
			base.Initialize(name, config);
			this.m_ApplicationName = this.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
			string text = config["connectionStringName"];
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentOutOfRangeException("ConnectionStringName", Resources.ErrArgumentNullOrEmpty);
			}
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
			if (connectionStringSettings == null || string.IsNullOrEmpty(connectionStringSettings.ConnectionString.Trim()))
			{
				throw new ProviderException(Resources.ErrConnectionStringNullOrEmpty);
			}
			this.m_ConnectionString = connectionStringSettings.ConnectionString;
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x0009669C File Offset: 0x0009489C
		// (set) Token: 0x0600382A RID: 14378 RVA: 0x000966A4 File Offset: 0x000948A4
		public override string ApplicationName
		{
			get
			{
				return this.m_ApplicationName;
			}
			set
			{
				this.m_ApplicationName = value;
			}
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000966B0 File Offset: 0x000948B0
		public override void AddUsersToRoles(string[] userNames, string[] roleNames)
		{
			foreach (string text in roleNames)
			{
				if (!this.RoleExists(text))
				{
					throw new ProviderException(string.Format(Resources.ErrRoleNotExist, text));
				}
			}
			foreach (string text2 in userNames)
			{
				foreach (string text3 in roleNames)
				{
					if (this.IsUserInRole(text2, text3))
					{
						throw new ProviderException(string.Format(Resources.ErrUserAlreadyInRole, text2, text3));
					}
				}
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"Username\", \"Rolename\", \"ApplicationName\") Values (@Username, @Rolename, @ApplicationName)", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Username");
					this.AddParameter(sqliteCommand, "@Rolename");
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					SqliteTransaction sqliteTransaction = null;
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						SqliteTransaction sqliteTransaction2;
						sqliteTransaction = (sqliteTransaction2 = sqliteConnection.BeginTransaction());
						try
						{
							foreach (string text4 in userNames)
							{
								foreach (string text5 in roleNames)
								{
									sqliteCommand.Parameters["@Username"].Value = text4;
									sqliteCommand.Parameters["@Rolename"].Value = text5;
									sqliteCommand.ExecuteNonQuery();
								}
							}
							sqliteTransaction.Commit();
						}
						finally
						{
							if (sqliteTransaction2 != null)
							{
								((IDisposable)sqliteTransaction2).Dispose();
							}
						}
					}
					catch (SqliteException)
					{
						try
						{
							sqliteTransaction.Rollback();
						}
						catch (SqliteException)
						{
						}
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00096904 File Offset: 0x00094B04
		public override void CreateRole(string roleName)
		{
			if (this.RoleExists(roleName))
			{
				throw new ProviderException(string.Format(Resources.ErrRoleAlreadyExist, roleName));
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"Rolename\", \"ApplicationName\") Values (@Rolename, @ApplicationName)", "Roles");
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						sqliteCommand.ExecuteNonQuery();
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000969E8 File Offset: 0x00094BE8
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			if (!this.RoleExists(roleName))
			{
				throw new ProviderException(string.Format(Resources.ErrRoleNotExist, roleName));
			}
			if (throwOnPopulatedRole && this.GetUsersInRole(roleName).Length != 0)
			{
				throw new ProviderException(Resources.ErrCantDeletePopulatedRole);
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "Roles");
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					SqliteTransaction sqliteTransaction = null;
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						SqliteTransaction sqliteTransaction2;
						sqliteTransaction = (sqliteTransaction2 = sqliteConnection.BeginTransaction());
						try
						{
							sqliteCommand.ExecuteNonQuery();
							sqliteTransaction.Commit();
						}
						finally
						{
							if (sqliteTransaction2 != null)
							{
								((IDisposable)sqliteTransaction2).Dispose();
							}
						}
					}
					catch (SqliteException)
					{
						try
						{
							sqliteTransaction.Rollback();
						}
						catch (SqliteException)
						{
						}
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x00096B24 File Offset: 0x00094D24
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			List<string> list = new List<string>();
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT \"Username\" FROM \"{0}\" WHERE \"Username\" LIKE @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName ORDER BY \"Username\" ASC", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Username", usernameToMatch);
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							if (sqliteDataReader.HasRows)
							{
								while (sqliteDataReader.Read())
								{
									list.Add(sqliteDataReader.GetString(0));
								}
							}
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00096C40 File Offset: 0x00094E40
		public override string[] GetAllRoles()
		{
			List<string> list = new List<string>();
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT \"Rolename\" FROM \"{0}\" WHERE \"ApplicationName\" = @ApplicationName ORDER BY \"Rolename\" ASC", "Roles");
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							while (sqliteDataReader.Read())
							{
								list.Add(sqliteDataReader.GetString(0));
							}
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00096D38 File Offset: 0x00094F38
		public override string[] GetRolesForUser(string username)
		{
			List<string> list = new List<string>();
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT \"Rolename\" FROM \"{0}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName ORDER BY \"Rolename\" ASC", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Username", username);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							if (sqliteDataReader.HasRows)
							{
								while (sqliteDataReader.Read())
								{
									list.Add(sqliteDataReader.GetString(0));
								}
							}
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00096E44 File Offset: 0x00095044
		public override string[] GetUsersInRole(string roleName)
		{
			List<string> list = new List<string>();
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT \"Username\" FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName ORDER BY \"Username\" ASC", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							if (sqliteDataReader.HasRows)
							{
								while (sqliteDataReader.Read())
								{
									list.Add(sqliteDataReader.GetString(0));
								}
							}
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x00096F50 File Offset: 0x00095150
		public override bool IsUserInRole(string userName, string roleName)
		{
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Username\" = @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Username", userName);
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						int num = 0;
						int.TryParse(sqliteCommand.ExecuteScalar().ToString(), out num);
						if (num > 0)
						{
							return true;
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x00097040 File Offset: 0x00095240
		public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
		{
			foreach (string text in roleNames)
			{
				if (!this.RoleExists(text))
				{
					throw new ProviderException(string.Format(Resources.ErrRoleNotExist, text));
				}
			}
			foreach (string text2 in userNames)
			{
				foreach (string text3 in roleNames)
				{
					if (!this.IsUserInRole(text2, text3))
					{
						throw new ProviderException(string.Format(Resources.ErrUserIsNotInRole, text2, text3));
					}
				}
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Username\" = @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "UsersInRoles");
					this.AddParameter(sqliteCommand, "@Username");
					this.AddParameter(sqliteCommand, "@Rolename");
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					SqliteTransaction sqliteTransaction = null;
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						SqliteTransaction sqliteTransaction2;
						sqliteTransaction = (sqliteTransaction2 = sqliteConnection.BeginTransaction());
						try
						{
							foreach (string text4 in userNames)
							{
								foreach (string text5 in roleNames)
								{
									sqliteCommand.Parameters["@Username"].Value = text4;
									sqliteCommand.Parameters["@Rolename"].Value = text5;
									sqliteCommand.ExecuteNonQuery();
								}
							}
							sqliteTransaction.Commit();
						}
						finally
						{
							if (sqliteTransaction2 != null)
							{
								((IDisposable)sqliteTransaction2).Dispose();
							}
						}
					}
					catch (SqliteException)
					{
						try
						{
							sqliteTransaction.Rollback();
						}
						catch (SqliteException)
						{
						}
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x00097294 File Offset: 0x00095494
		public override bool RoleExists(string roleName)
		{
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "Roles");
					this.AddParameter(sqliteCommand, "@Rolename", roleName);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						int num = 0;
						int.TryParse(sqliteCommand.ExecuteScalar().ToString(), out num);
						if (num > 0)
						{
							return true;
						}
					}
					catch (SqliteException)
					{
						throw new ProviderException(Resources.ErrOperationAborted);
					}
					finally
					{
						if (sqliteConnection != null)
						{
							sqliteConnection.Close();
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x00095EAC File Offset: 0x000940AC
		private string GetConfigValue(string configValue, string defaultValue)
		{
			if (string.IsNullOrEmpty(configValue))
			{
				return defaultValue;
			}
			return configValue;
		}

		// Token: 0x04001E2F RID: 7727
		private const string m_RolesTableName = "Roles";

		// Token: 0x04001E30 RID: 7728
		private const string m_UserInRolesTableName = "UsersInRoles";

		// Token: 0x04001E31 RID: 7729
		private string m_ConnectionString = string.Empty;

		// Token: 0x04001E32 RID: 7730
		private string m_ApplicationName = string.Empty;
	}
}
