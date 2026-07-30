using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages storage of role membership information for an ASP.NET application in a SQL Server database.</summary>
	// Token: 0x020004D0 RID: 1232
	public class SqlRoleProvider : RoleProvider
	{
		// Token: 0x060037EB RID: 14315 RVA: 0x000934A0 File Offset: 0x000916A0
		private DbConnection CreateConnection()
		{
			if (!this.schemaIsOk && !(this.schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(this.factory, this.connectionString.ConnectionString, "role manager", "1")))
			{
				throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
			}
			DbConnection dbConnection = this.factory.CreateConnection();
			dbConnection.ConnectionString = this.connectionString.ConnectionString;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x0009350D File Offset: 0x0009170D
		private static void AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			SqlRoleProvider.AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x0009351C File Offset: 0x0009171C
		private static DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x00093554 File Offset: 0x00091754
		private static DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, DbType type, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			dbParameter.DbType = type;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		/// <summary>Adds the specified user names to each of the specified roles.</summary>
		/// <param name="usernames">A string array of user names to be added to the specified roles.</param>
		/// <param name="roleNames">A string array of role names to add the specified user names to.</param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-One of the users in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma.-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma.-or-One of the roles in <paramref name="roleNames" /> is longer than 256 characters.-or-One of the users in <paramref name="usernames" /> is longer than 256 characters.-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">One or more of the specified role names was not found.-or- One or more of the specified user names was not found.-or- One or more of the specified user names is already associated with one or more of the specified role names.-or- An unknown error occurred while communicating with the database.</exception>
		// Token: 0x060037EF RID: 14319 RVA: 0x00093594 File Offset: 0x00091794
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in usernames)
			{
				if (text == null)
				{
					throw new ArgumentNullException("null element in usernames array");
				}
				if (hashtable.ContainsKey(text))
				{
					throw new ArgumentException("duplicate element in usernames array");
				}
				if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in usernames array in illegal format");
				}
				hashtable.Add(text, text);
			}
			hashtable = new Hashtable();
			foreach (string text2 in roleNames)
			{
				if (text2 == null)
				{
					throw new ArgumentNullException("null element in rolenames array");
				}
				if (hashtable.ContainsKey(text2))
				{
					throw new ArgumentException("duplicate element in rolenames array");
				}
				if (text2.Length == 0 || text2.Length > 256 || text2.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in rolenames array in illegal format");
				}
				hashtable.Add(text2, text2);
			}
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_AddUsersToRoles";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@RoleNames", string.Join(",", roleNames));
				SqlRoleProvider.AddParameter(dbCommand, "@UserNames", string.Join(",", usernames));
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				SqlRoleProvider.AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				int num = (int)dbParameter.Value;
				if (num != 0)
				{
					if (num == 2)
					{
						throw new ProviderException("One or more of the specified user/role names was not found.");
					}
					if (num == 3)
					{
						throw new ProviderException("One or more of the specified user names is already associated with one or more of the specified role names.");
					}
					throw new ProviderException("Failed to create new user/role association.");
				}
			}
		}

		/// <summary>Adds a new role to the role database.</summary>
		/// <param name="roleName">The name of the role to create. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="roleName" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> already exists in the database.-or- An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F0 RID: 14320 RVA: 0x00093780 File Offset: 0x00091980
		public override void CreateRole(string roleName)
		{
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
			{
				throw new ArgumentException("rolename is in invalid format");
			}
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_Roles_CreateRole";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				if ((int)dbParameter.Value == 1)
				{
					throw new ProviderException(roleName + " already exists in the database");
				}
			}
		}

		/// <summary>Removes a role from the role database.</summary>
		/// <returns>true if the role was successfully deleted; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If true, throws an exception if <paramref name="roleName" /> has one or more members.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="roleName" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is true.-or- An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F1 RID: 14321 RVA: 0x00093860 File Offset: 0x00091A60
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
			{
				throw new ArgumentException("rolename is in invalid format");
			}
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_Roles_DeleteRole";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				SqlRoleProvider.AddParameter(dbCommand, "@DeleteOnlyIfRoleIsEmpty", throwOnPopulatedRole);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				int num = (int)dbParameter.Value;
				if (num == 0)
				{
					flag = true;
				}
				else if (num == 1)
				{
					flag = false;
				}
				else
				{
					if (num == 2 && throwOnPopulatedRole)
					{
						throw new ProviderException(roleName + " is not empty");
					}
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Gets an array of user names in a role where the user name contains the specified user name to match.</summary>
		/// <returns>A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch" /> and the user is a member of the specified role.</returns>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).-or-<paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="usernameToMatch" /> is an empty string.-or-<paramref name="roleName" /> is longer than 256 characters.-or-<paramref name="usernameToMatch" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> was not found in the database.-or- An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F2 RID: 14322 RVA: 0x00093968 File Offset: 0x00091B68
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			if (roleName == null)
			{
				throw new ArgumentNullException("roleName");
			}
			if (usernameToMatch == null)
			{
				throw new ArgumentNullException("usernameToMatch");
			}
			if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
			{
				throw new ArgumentException("roleName is in invalid format");
			}
			if (usernameToMatch.Length == 0 || usernameToMatch.Length > 256)
			{
				throw new ArgumentException("usernameToMatch is in invalid format");
			}
			string[] array;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_FindUsersInRole";
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				SqlRoleProvider.AddParameter(dbCommand, "@UsernameToMatch", usernameToMatch);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				while (dbDataReader.Read())
				{
					arrayList.Add(dbDataReader.GetString(0));
				}
				dbDataReader.Close();
				array = (string[])arrayList.ToArray(typeof(string));
			}
			return array;
		}

		/// <summary>Gets a list of all the roles for the application.</summary>
		/// <returns>A string array containing the names of all the roles stored in the database for a particular application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database.</exception>
		// Token: 0x060037F3 RID: 14323 RVA: 0x00093A90 File Offset: 0x00091C90
		public override string[] GetAllRoles()
		{
			string[] array;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_Roles_GetAllRoles";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				while (dbDataReader.Read())
				{
					arrayList.Add(dbDataReader.GetString(0));
				}
				dbDataReader.Close();
				array = (string[])arrayList.ToArray(typeof(string));
			}
			return array;
		}

		/// <summary>Gets a list of the roles that a user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
		/// <param name="username">The user to return a list of roles for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma.-or-<paramref name="username" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F4 RID: 14324 RVA: 0x00093B38 File Offset: 0x00091D38
		public override string[] GetRolesForUser(string username)
		{
			string[] array;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_GetRolesForUser";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@UserName", username);
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				while (dbDataReader.Read())
				{
					arrayList.Add(dbDataReader.GetString(0));
				}
				dbDataReader.Close();
				array = (string[])arrayList.ToArray(typeof(string));
			}
			return array;
		}

		/// <summary>Gets a list of users in the specified role.</summary>
		/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
		/// <param name="roleName">The name of the role to get the list of users for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="roleName" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="rolename" /> was not found in the database.-or- An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F5 RID: 14325 RVA: 0x00093BEC File Offset: 0x00091DEC
		public override string[] GetUsersInRole(string roleName)
		{
			string[] array;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_GetUsersInRoles";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				DbDataReader dbDataReader = dbCommand.ExecuteReader();
				ArrayList arrayList = new ArrayList();
				while (dbDataReader.Read())
				{
					arrayList.Add(dbDataReader.GetString(0));
				}
				dbDataReader.Close();
				array = (string[])arrayList.ToArray(typeof(string));
			}
			return array;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x00093CA0 File Offset: 0x00091EA0
		private string GetStringConfigValue(NameValueCollection config, string name, string def)
		{
			string text = def;
			string text2 = config[name];
			if (text2 != null)
			{
				text = text2;
			}
			return text;
		}

		/// <summary>Initializes the SQL Server role provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Security.SqlRoleProvider" /> instance to initialize. </param>
		/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the role provider. </param>
		/// <exception cref="T:System.Web.HttpException">The ASP.NET application is not running at <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />  trust or higher. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="config" /> is null. </exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The connectionStringName attribute is empty or does not exist in the application configuration file for this <see cref="T:System.Web.Security.SqlRoleProvider" /> instance.-or-The applicationName attribute exceeds 256 characters.-or-The application configuration file for this <see cref="T:System.Web.Security.SqlRoleProvider" /> instance contains an unrecognized attribute. </exception>
		// Token: 0x060037F7 RID: 14327 RVA: 0x00093CC0 File Offset: 0x00091EC0
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			base.Initialize(name, config);
			this.applicationName = this.GetStringConfigValue(config, "applicationName", "/");
			string text = config["connectionStringName"];
			if (this.applicationName.Length > 256)
			{
				throw new ProviderException("The ApplicationName attribute must be 256 characters long or less.");
			}
			if (text == null || text.Length == 0)
			{
				throw new ProviderException("The ConnectionStringName attribute must be present and non-zero length.");
			}
			this.connectionString = WebConfigurationManager.ConnectionStrings[text];
			if (this.connectionString == null)
			{
				throw new ProviderException(string.Format("The connection name '{0}' was not found in the applications configuration or the connection string is empty.", text));
			}
			this.factory = (string.IsNullOrEmpty(this.connectionString.ProviderName) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(this.connectionString.ProviderName));
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified role.</summary>
		/// <returns>true if the specified user name is in the specified role; otherwise, false.</returns>
		/// <param name="username">The user name to search for. </param>
		/// <param name="roleName">The role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="username" /> is contains a comma.-or-<paramref name="roleName" /> is longer than 256 characters.-or-<paramref name="username" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F8 RID: 14328 RVA: 0x00093D94 File Offset: 0x00091F94
		public override bool IsUserInRole(string username, string roleName)
		{
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_IsUserInRole";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				SqlRoleProvider.AddParameter(dbCommand, "@UserName", username);
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				if ((int)dbParameter.Value == 1)
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

		/// <summary>Removes the specified user names from the specified roles.</summary>
		/// <param name="usernames">A string array of user names to be removed from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified user names from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-One of the users in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma.-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma.-or-One of the roles in <paramref name="roleNames" /> is longer than 256 characters.-or-One of the users in <paramref name="usernames" /> is longer than 256 characters.-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">One or more of the specified user names was not found.-or- One or more of the specified role names was not found.-or- One or more of the specified user names is not associated with one or more of the specified role names.-or- An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037F9 RID: 14329 RVA: 0x00093E3C File Offset: 0x0009203C
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string text in usernames)
			{
				if (text == null)
				{
					throw new ArgumentNullException("null element in usernames array");
				}
				if (hashtable.ContainsKey(text))
				{
					throw new ArgumentException("duplicate element in usernames array");
				}
				if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in usernames array in illegal format");
				}
				hashtable.Add(text, text);
			}
			hashtable = new Hashtable();
			foreach (string text2 in roleNames)
			{
				if (text2 == null)
				{
					throw new ArgumentNullException("null element in rolenames array");
				}
				if (hashtable.ContainsKey(text2))
				{
					throw new ArgumentException("duplicate element in rolenames array");
				}
				if (text2.Length == 0 || text2.Length > 256 || text2.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in rolenames array in illegal format");
				}
				hashtable.Add(text2, text2);
			}
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_UsersInRoles_RemoveUsersFromRoles";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@UserNames", string.Join(",", usernames));
				SqlRoleProvider.AddParameter(dbCommand, "@RoleNames", string.Join(",", roleNames));
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				int num = (int)dbParameter.Value;
				if (num != 0)
				{
					if (num == 1)
					{
						throw new ProviderException("One or more of the specified user names was not found.");
					}
					if (num == 2)
					{
						throw new ProviderException("One or more of the specified role names was not found.");
					}
					if (num == 3)
					{
						throw new ProviderException("One or more of the specified user names is not associated with one or more of the specified role names.");
					}
					throw new ProviderException("Failed to remove users from roles");
				}
			}
		}

		/// <summary>Gets a value indicating whether the specified role name already exists in the role database.</summary>
		/// <returns>true if the role name already exists in the database; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search for in the database. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma.-or-<paramref name="roleName" /> is longer than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
		// Token: 0x060037FA RID: 14330 RVA: 0x00094024 File Offset: 0x00092224
		public override bool RoleExists(string roleName)
		{
			bool flag;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.CommandText = "dbo.aspnet_Roles_RoleExists";
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				SqlRoleProvider.AddParameter(dbCommand, "@ApplicationName", this.ApplicationName);
				SqlRoleProvider.AddParameter(dbCommand, "@RoleName", roleName);
				DbParameter dbParameter = SqlRoleProvider.AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
				dbCommand.ExecuteNonQuery();
				if ((int)dbParameter.Value == 1)
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

		/// <summary>Gets or sets the name of the application for which to store and retrieve role information.</summary>
		/// <returns>The name of the application for which to store and retrieve role information. The default is the <see cref="P:System.Web.HttpRequest.ApplicationPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.Security.SqlRoleProvider.ApplicationName" /> property by a caller that does not have <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> ASP.NET hosting permission.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.SqlRoleProvider.ApplicationName" /> to a string that is longer than 256 characters.</exception>
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000940C0 File Offset: 0x000922C0
		// (set) Token: 0x060037FC RID: 14332 RVA: 0x000940C8 File Offset: 0x000922C8
		public override string ApplicationName
		{
			get
			{
				return this.applicationName;
			}
			set
			{
				this.applicationName = value;
			}
		}

		// Token: 0x04001E18 RID: 7704
		private string applicationName;

		// Token: 0x04001E19 RID: 7705
		private bool schemaIsOk;

		// Token: 0x04001E1A RID: 7706
		private ConnectionStringSettings connectionString;

		// Token: 0x04001E1B RID: 7707
		private DbProviderFactory factory;
	}
}
