using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Web.Hosting;
using System.Web.Properties;
using System.Web.Util;
using Mono.Data.Sqlite;

namespace System.Web.Profile
{
	// Token: 0x02000514 RID: 1300
	internal class SqliteProfileProvider : ProfileProvider
	{
		// Token: 0x060039B9 RID: 14777 RVA: 0x0009BFAC File Offset: 0x0009A1AC
		private DbParameter AddParameter(DbCommand command, string parameterName)
		{
			return this.AddParameter(command, parameterName, null);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0009BFB7 File Offset: 0x0009A1B7
		private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			return this.AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0009BFC4 File Offset: 0x0009A1C4
		private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0009BFFC File Offset: 0x0009A1FC
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("Config", Resources.ErrArgumentNull);
			}
			if (string.IsNullOrEmpty(name))
			{
				name = Resources.ProfileProviderDefaultName;
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", Resources.ProfileProviderDefaultDescription);
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

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x0009C0D8 File Offset: 0x0009A2D8
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x0009C0E0 File Offset: 0x0009A2E0
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

		// Token: 0x060039BF RID: 14783 RVA: 0x0009C0E9 File Offset: 0x0009A2E9
		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			throw new Exception("DeleteInactiveProfiles: The method or operation is not implemented.");
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0009C0F5 File Offset: 0x0009A2F5
		public override int DeleteProfiles(string[] usernames)
		{
			throw new Exception("DeleteProfiles1: The method or operation is not implemented.");
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0009C101 File Offset: 0x0009A301
		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			throw new Exception("DeleteProfiles2: The method or operation is not implemented.");
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x0009C10D File Offset: 0x0009A30D
		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("FindInactiveProfilesByUserName: The method or operation is not implemented.");
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0009C119 File Offset: 0x0009A319
		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("FindProfilesByUserName: The method or operation is not implemented.");
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0009C125 File Offset: 0x0009A325
		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("GetAllInactiveProfiles: The method or operation is not implemented.");
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x0009C131 File Offset: 0x0009A331
		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("GetAllProfiles: The method or operation is not implemented.");
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0009C13D File Offset: 0x0009A33D
		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			throw new Exception("GetNumberOfInactiveProfiles: The method or operation is not implemented.");
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0009C14C File Offset: 0x0009A34C
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			string text = (string)context["UserName"];
			bool flag = (bool)context["IsAuthenticated"];
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT \"Name\", \"ValueString\", \"ValueBinary\" FROM \"{0}\" WHERE \"Profile\" = (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated)", "ProfileData", "Profiles");
					this.AddParameter(sqliteCommand, "@Username", text);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					this.AddParameter(sqliteCommand, "@IsAuthenticated", !flag);
					try
					{
						sqliteConnection.Open();
						sqliteCommand.Prepare();
						using (SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							while (sqliteDataReader.Read())
							{
								object obj = null;
								if (!sqliteDataReader.IsDBNull(1))
								{
									obj = sqliteDataReader.GetValue(1);
								}
								else if (!sqliteDataReader.IsDBNull(2))
								{
									obj = sqliteDataReader.GetValue(2);
								}
								dictionary.Add(sqliteDataReader.GetString(0), obj);
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
			foreach (object obj2 in collection)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj2;
				if (settingsProperty.SerializeAs == SettingsSerializeAs.ProviderSpecific)
				{
					if (settingsProperty.PropertyType.IsPrimitive || settingsProperty.PropertyType.Equals(typeof(string)))
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.String;
					}
					else
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.Xml;
					}
				}
				SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
				if (dictionary.ContainsKey(settingsProperty.Name) && dictionary[settingsProperty.Name] != null)
				{
					if (settingsProperty.SerializeAs == SettingsSerializeAs.String)
					{
						settingsPropertyValue.PropertyValue = this.m_serializationHelper.DeserializeFromBase64((string)dictionary[settingsProperty.Name]);
					}
					else if (settingsProperty.SerializeAs == SettingsSerializeAs.Xml)
					{
						settingsPropertyValue.PropertyValue = this.m_serializationHelper.DeserializeFromXml((string)dictionary[settingsProperty.Name]);
					}
					else if (settingsProperty.SerializeAs == SettingsSerializeAs.Binary)
					{
						settingsPropertyValue.PropertyValue = this.m_serializationHelper.DeserializeFromBinary((byte[])dictionary[settingsProperty.Name]);
					}
				}
				settingsPropertyValue.IsDirty = false;
				settingsPropertyValueCollection.Add(settingsPropertyValue);
			}
			this.UpdateActivityDates(text, flag, true);
			return settingsPropertyValueCollection;
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0009C47C File Offset: 0x0009A67C
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			string text = (string)context["UserName"];
			bool flag = (bool)context["IsAuthenticated"];
			if (collection.Count < 1)
			{
				return;
			}
			if (!this.ProfileExists(text))
			{
				this.CreateProfileForUser(text, flag);
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					using (SqliteCommand sqliteCommand2 = sqliteConnection.CreateCommand())
					{
						sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Name\" = @Name AND \"Profile\" = (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated)", "ProfileData", "Profiles");
						this.AddParameter(sqliteCommand, "@Name");
						this.AddParameter(sqliteCommand, "@Username", text);
						this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
						this.AddParameter(sqliteCommand, "@IsAuthenticated", !flag);
						sqliteCommand2.CommandText = string.Format("INSERT INTO \"{0}\" (\"pId\", \"Profile\", \"Name\", \"ValueString\", \"ValueBinary\") VALUES (@pId, (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated), @Name, @ValueString, @ValueBinary)", "ProfileData", "Profiles");
						this.AddParameter(sqliteCommand2, "@pId");
						this.AddParameter(sqliteCommand2, "@Name");
						this.AddParameter(sqliteCommand2, "@ValueString");
						sqliteCommand2.Parameters["@ValueString"].IsNullable = true;
						this.AddParameter(sqliteCommand2, "@ValueBinary");
						sqliteCommand2.Parameters["@ValueBinary"].IsNullable = true;
						this.AddParameter(sqliteCommand2, "@Username", text);
						this.AddParameter(sqliteCommand2, "@ApplicationName", this.m_ApplicationName);
						this.AddParameter(sqliteCommand2, "@IsAuthenticated", !flag);
						SqliteTransaction sqliteTransaction = null;
						try
						{
							sqliteConnection.Open();
							sqliteCommand.Prepare();
							sqliteCommand2.Prepare();
							SqliteTransaction sqliteTransaction2;
							sqliteTransaction = (sqliteTransaction2 = sqliteConnection.BeginTransaction());
							try
							{
								foreach (object obj in collection)
								{
									SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
									if (settingsPropertyValue.IsDirty)
									{
										sqliteCommand.Parameters["@Name"].Value = settingsPropertyValue.Name;
										sqliteCommand2.Parameters["@pId"].Value = Guid.NewGuid().ToString();
										sqliteCommand2.Parameters["@Name"].Value = settingsPropertyValue.Name;
										if (settingsPropertyValue.Property.SerializeAs == SettingsSerializeAs.String)
										{
											sqliteCommand2.Parameters["@ValueString"].Value = this.m_serializationHelper.SerializeToBase64(settingsPropertyValue.PropertyValue);
											sqliteCommand2.Parameters["@ValueBinary"].Value = DBNull.Value;
										}
										else if (settingsPropertyValue.Property.SerializeAs == SettingsSerializeAs.Xml)
										{
											settingsPropertyValue.SerializedValue = this.m_serializationHelper.SerializeToXml(settingsPropertyValue.PropertyValue);
											sqliteCommand2.Parameters["@ValueString"].Value = settingsPropertyValue.SerializedValue;
											sqliteCommand2.Parameters["@ValueBinary"].Value = DBNull.Value;
										}
										else if (settingsPropertyValue.Property.SerializeAs == SettingsSerializeAs.Binary)
										{
											settingsPropertyValue.SerializedValue = this.m_serializationHelper.SerializeToBinary(settingsPropertyValue.PropertyValue);
											sqliteCommand2.Parameters["@ValueString"].Value = DBNull.Value;
											sqliteCommand2.Parameters["@ValueBinary"].Value = settingsPropertyValue.SerializedValue;
										}
										sqliteCommand.ExecuteNonQuery();
										sqliteCommand2.ExecuteNonQuery();
									}
								}
								this.UpdateActivityDates(text, flag, false);
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
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0009C918 File Offset: 0x0009AB18
		private void CreateProfileForUser(string username, bool isAuthenticated)
		{
			if (this.ProfileExists(username))
			{
				throw new ProviderException(string.Format(Resources.ErrProfileAlreadyExist, username));
			}
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"pId\", \"Username\", \"ApplicationName\", \"IsAnonymous\", \"LastActivityDate\", \"LastUpdatedDate\") Values (@pId, @Username, @ApplicationName, @IsAuthenticated, @LastActivityDate, @LastUpdatedDate)", "Profiles");
					this.AddParameter(sqliteCommand, "@pId", Guid.NewGuid().ToString());
					this.AddParameter(sqliteCommand, "@Username", username);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					this.AddParameter(sqliteCommand, "@IsAuthenticated", !isAuthenticated);
					this.AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
					this.AddParameter(sqliteCommand, "@LastUpdatedDate", DateTime.Now);
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

		// Token: 0x060039CA RID: 14794 RVA: 0x0009CA60 File Offset: 0x0009AC60
		private bool ProfileExists(string username)
		{
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName", "Profiles");
					this.AddParameter(sqliteCommand, "@Username", username);
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

		// Token: 0x060039CB RID: 14795 RVA: 0x0009CB44 File Offset: 0x0009AD44
		private void UpdateActivityDates(string username, bool isAuthenticated, bool activityOnly)
		{
			using (SqliteConnection sqliteConnection = new SqliteConnection(this.m_ConnectionString))
			{
				using (SqliteCommand sqliteCommand = sqliteConnection.CreateCommand())
				{
					if (activityOnly)
					{
						sqliteCommand.CommandText = string.Format("UPDATE \"{0}\" SET \"LastActivityDate\" = @LastActivityDate WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated", "Profiles");
						this.AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
					}
					else
					{
						sqliteCommand.CommandText = string.Format("UPDATE \"{0}\" SET \"LastActivityDate\" = @LastActivityDate, \"LastUpdatedDate\" = @LastUpdatedDate WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated", "Profiles");
						this.AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
						this.AddParameter(sqliteCommand, "@LastUpdatedDate", DateTime.Now);
					}
					this.AddParameter(sqliteCommand, "@Username", username);
					this.AddParameter(sqliteCommand, "@ApplicationName", this.m_ApplicationName);
					this.AddParameter(sqliteCommand, "@IsAuthenticated", !isAuthenticated);
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

		// Token: 0x060039CC RID: 14796 RVA: 0x00095EAC File Offset: 0x000940AC
		private string GetConfigValue(string configValue, string defaultValue)
		{
			if (string.IsNullOrEmpty(configValue))
			{
				return defaultValue;
			}
			return configValue;
		}

		// Token: 0x04001F3C RID: 7996
		private const string m_ProfilesTableName = "Profiles";

		// Token: 0x04001F3D RID: 7997
		private const string m_ProfileDataTableName = "ProfileData";

		// Token: 0x04001F3E RID: 7998
		private string m_ConnectionString = string.Empty;

		// Token: 0x04001F3F RID: 7999
		private SerializationHelper m_serializationHelper = new SerializationHelper();

		// Token: 0x04001F40 RID: 8000
		private string m_ApplicationName = string.Empty;
	}
}
