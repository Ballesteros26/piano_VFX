using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.Profile
{
	/// <summary>Manages storage of profile information for an ASP.NET application in a SQL Server database.</summary>
	// Token: 0x02000513 RID: 1299
	public class SqlProfileProvider : ProfileProvider
	{
		/// <summary>Deletes user profile data for profiles in which the last activity date occurred before the specified date and time.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x060039A0 RID: 14752 RVA: 0x0009AED4 File Offset: 0x000990D4
		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int returnValue;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_DeleteInactiveProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
				DbParameter dbParameter = this.AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
				dbCommand.ExecuteNonQuery();
				returnValue = SqlProfileProvider.GetReturnValue(dbParameter);
			}
			return returnValue;
		}

		/// <summary>Deletes profile properties and information for the supplied list of profiles from the data source.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  that contains profile information for profiles to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfoCollection.Count" /> value of zero.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is an empty string (""), exceeds a length of 256 characters, or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="profiles" /> is null.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is null.</exception>
		// Token: 0x060039A1 RID: 14753 RVA: 0x0009AF7C File Offset: 0x0009917C
		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			if (profiles == null)
			{
				throw new ArgumentNullException("prfoles");
			}
			if (profiles.Count == 0)
			{
				throw new ArgumentException("prfoles");
			}
			string[] array = new string[profiles.Count];
			int num = 0;
			foreach (object obj in profiles)
			{
				ProfileInfo profileInfo = (ProfileInfo)obj;
				if (profileInfo.UserName == null)
				{
					throw new ArgumentNullException("element in profiles collection is null");
				}
				if (profileInfo.UserName.Length == 0 || profileInfo.UserName.Length > 256 || profileInfo.UserName.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in profiles collection in illegal format");
				}
				array[num++] = profileInfo.UserName;
			}
			return this.DeleteProfilesInternal(array);
		}

		/// <summary>Deletes profile properties and information from the data source for the supplied list of user names.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="usernames">A string array of user names for profiles to be deleted. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="usernames" /> is zero.- or -One of the items in <paramref name="usernames" /> is an empty string (""), exceeds a length of 256 characters, or contains a comma.- or -Two or more items in <paramref name="usernames" /> have the same value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernames" /> is null.- or -One of the items in <paramref name="usernames" /> is null.</exception>
		// Token: 0x060039A2 RID: 14754 RVA: 0x0009B05C File Offset: 0x0009925C
		public override int DeleteProfiles(string[] usernames)
		{
			if (usernames == null)
			{
				throw new ArgumentNullException("usernames");
			}
			Hashtable hashtable = new Hashtable();
			foreach (string text in usernames)
			{
				if (text == null)
				{
					throw new ArgumentNullException("element in usernames array is null");
				}
				if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
				{
					throw new ArgumentException("element in usernames array in illegal format");
				}
				if (hashtable.ContainsKey(text))
				{
					throw new ArgumentException("duplicate element in usernames array");
				}
				hashtable.Add(text, text);
			}
			return this.DeleteProfilesInternal(usernames);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0009B0F0 File Offset: 0x000992F0
		private int DeleteProfilesInternal(string[] usernames)
		{
			int returnValue;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_DeleteProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "UserNames", string.Join(",", usernames));
				DbParameter dbParameter = this.AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
				dbCommand.ExecuteNonQuery();
				returnValue = SqlProfileProvider.GetReturnValue(dbParameter);
			}
			return returnValue;
		}

		/// <summary>Retrieves profile information for profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("") or exceeds 256 characters.- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060039A4 RID: 14756 RVA: 0x0009B188 File Offset: 0x00099388
		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			this.CheckParam("usernameToMatch", usernameToMatch, 256);
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex is less than zero");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException("pageIndex is less than one");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			ProfileInfoCollection profileInfoCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "PageIndex", pageIndex);
				this.AddParameter(dbCommand, "PageSize", pageSize);
				this.AddParameter(dbCommand, "UserNameToMatch", usernameToMatch);
				this.AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					profileInfoCollection = this.BuildProfileInfoCollection(dbDataReader, out totalRecords);
				}
			}
			return profileInfoCollection;
		}

		/// <summary>Retrieves profile information for profiles in which the user name matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("") or exceeds 256 characters.- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060039A5 RID: 14757 RVA: 0x0009B2C4 File Offset: 0x000994C4
		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			this.CheckParam("usernameToMatch", usernameToMatch, 256);
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex is less than zero");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException("pageIndex is less than one");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			ProfileInfoCollection profileInfoCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "PageIndex", pageIndex);
				this.AddParameter(dbCommand, "PageSize", pageSize);
				this.AddParameter(dbCommand, "UserNameToMatch", usernameToMatch);
				this.AddParameter(dbCommand, "InactiveSinceDate", null);
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					profileInfoCollection = this.BuildProfileInfoCollection(dbDataReader, out totalRecords);
				}
			}
			return profileInfoCollection;
		}

		/// <summary>Retrieves user profile data for profiles in which the last activity date occurred on or before the specified date and time.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060039A6 RID: 14758 RVA: 0x0009B3F8 File Offset: 0x000995F8
		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex is less than zero");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException("pageIndex is less than one");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			ProfileInfoCollection profileInfoCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "PageIndex", pageIndex);
				this.AddParameter(dbCommand, "PageSize", pageSize);
				this.AddParameter(dbCommand, "UserNameToMatch", null);
				this.AddParameter(dbCommand, "InactiveSinceDate", null);
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					profileInfoCollection = this.BuildProfileInfoCollection(dbDataReader, out totalRecords);
				}
			}
			return profileInfoCollection;
		}

		/// <summary>Retrieves user profile data for profiles in the data source.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than one.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060039A7 RID: 14759 RVA: 0x0009B518 File Offset: 0x00099718
		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			if (pageIndex < 0)
			{
				throw new ArgumentException("pageIndex is less than zero");
			}
			if (pageSize < 1)
			{
				throw new ArgumentException("pageIndex is less than one");
			}
			if (pageIndex * pageSize + pageSize - 1 > 2147483647)
			{
				throw new ArgumentException("pageIndex and pageSize are too large");
			}
			ProfileInfoCollection profileInfoCollection;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "PageIndex", pageIndex);
				this.AddParameter(dbCommand, "PageSize", pageSize);
				this.AddParameter(dbCommand, "UserNameToMatch", null);
				this.AddParameter(dbCommand, "InactiveSinceDate", null);
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					profileInfoCollection = this.BuildProfileInfoCollection(dbDataReader, out totalRecords);
				}
			}
			return profileInfoCollection;
		}

		/// <summary>Gets the number of profiles in the data source where the last activity date occurred on or before the specified <paramref name="userInactiveSinceDate" />.</summary>
		/// <returns>The number of profiles in the data source for which the last activity date occurred before the specified date and time.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x060039A8 RID: 14760 RVA: 0x0009B634 File Offset: 0x00099834
		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int num2;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetNumberOfInactiveProfiles";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
				this.AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
				int num = 0;
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					if (dbDataReader.Read())
					{
						num = dbDataReader.GetInt32(0);
					}
				}
				num2 = num;
			}
			return num2;
		}

		/// <summary>Retrieves profile property information and values from a SQL Server profile database.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing profile property information and values.</returns>
		/// <param name="sc">The <see cref="T:System.Configuration.SettingsContext" /> that contains user profile information.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing profile information for the properties to be retrieved.</param>
		// Token: 0x060039A9 RID: 14761 RVA: 0x0009B6F8 File Offset: 0x000998F8
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext sc, SettingsPropertyCollection properties)
		{
			SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
			if (properties.Count == 0)
			{
				return settingsPropertyValueCollection;
			}
			foreach (object obj in properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.SerializeAs == SettingsSerializeAs.ProviderSpecific)
				{
					if (settingsProperty.PropertyType.IsPrimitive || settingsProperty.PropertyType == typeof(string))
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.String;
					}
					else
					{
						settingsProperty.SerializeAs = SettingsSerializeAs.Xml;
					}
				}
				settingsPropertyValueCollection.Add(new SettingsPropertyValue(settingsProperty));
			}
			string text = (string)sc["UserName"];
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_GetProperties";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "UserName", text);
				this.AddParameter(dbCommand, "CurrentTimeUtc", DateTime.UtcNow);
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					if (dbDataReader.Read())
					{
						string @string = dbDataReader.GetString(0);
						string string2 = dbDataReader.GetString(1);
						int num = (int)dbDataReader.GetBytes(2, 0L, null, 0, 0);
						byte[] array = new byte[num];
						dbDataReader.GetBytes(2, 0L, array, 0, num);
						this.DecodeProfileData(@string, string2, array, settingsPropertyValueCollection);
					}
				}
			}
			return settingsPropertyValueCollection;
		}

		/// <summary>Updates the SQL Server profile database with the specified property values.</summary>
		/// <param name="sc">The <see cref="T:System.Configuration.SettingsContext" /> that contains user profile information.</param>
		/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing profile information and values for the properties to be updated.</param>
		// Token: 0x060039AA RID: 14762 RVA: 0x0009B8B0 File Offset: 0x00099AB0
		public override void SetPropertyValues(SettingsContext sc, SettingsPropertyValueCollection properties)
		{
			string text = (string)sc["UserName"];
			bool flag = !(bool)sc["IsAuthenticated"];
			string empty = string.Empty;
			string empty2 = string.Empty;
			byte[] array = null;
			this.EncodeProfileData(ref empty, ref empty2, ref array, properties, !flag);
			using (DbConnection dbConnection = this.CreateConnection())
			{
				DbCommand dbCommand = this.factory.CreateCommand();
				dbCommand.Connection = dbConnection;
				dbCommand.CommandType = CommandType.StoredProcedure;
				dbCommand.CommandText = "aspnet_Profile_SetProperties";
				this.AddParameter(dbCommand, "ApplicationName", this.ApplicationName);
				this.AddParameter(dbCommand, "PropertyNames", empty);
				this.AddParameter(dbCommand, "PropertyValuesString", empty2);
				this.AddParameter(dbCommand, "PropertyValuesBinary", array);
				this.AddParameter(dbCommand, "UserName", text);
				this.AddParameter(dbCommand, "IsUserAnonymous", flag);
				this.AddParameter(dbCommand, "CurrentTimeUtc", DateTime.UtcNow);
				this.AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
				dbCommand.ExecuteNonQuery();
			}
		}

		/// <summary>Initializes the SQL Server profile provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance to initialize. </param>
		/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the profile provider. </param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The connectionStringName attribute is an empty string ("") or is not specified in the application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance.- or - The value of the connection string specified in the connectionStringName attribute value is empty or the specified connectionStringName value does not exist in the application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance.- or - The applicationName attribute value exceeds 256 characters.- or - The application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance contains an unrecognized attribute. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="config" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The current trust level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Low" />.</exception>
		// Token: 0x060039AB RID: 14763 RVA: 0x0009B9E4 File Offset: 0x00099BE4
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
			this.factory = ((this.connectionString == null || string.IsNullOrEmpty(this.connectionString.ProviderName)) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(this.connectionString.ProviderName));
		}

		/// <summary>Gets or sets the name of the application for which to store and retrieve profile information.</summary>
		/// <returns>The name of the application for which to store and retrieve profile information. The default is the <see cref="P:System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath" /> value.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.Profile.SqlProfileProvider.ApplicationName" /> property by a caller that does not have <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> ASP.NET hosting permission.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Profile.SqlProfileProvider.ApplicationName" /> property to a string that is longer than 256 characters.</exception>
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x0009BAA5 File Offset: 0x00099CA5
		// (set) Token: 0x060039AD RID: 14765 RVA: 0x0009BAAD File Offset: 0x00099CAD
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

		// Token: 0x060039AE RID: 14766 RVA: 0x0009BAB8 File Offset: 0x00099CB8
		private DbConnection CreateConnection()
		{
			if (!this.schemaIsOk && !(this.schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(this.factory, this.connectionString.ConnectionString, "profile", "1")))
			{
				throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
			}
			DbConnection dbConnection = this.factory.CreateConnection();
			dbConnection.ConnectionString = this.connectionString.ConnectionString;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0009BB25 File Offset: 0x00099D25
		private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
		{
			return this.AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0009BB34 File Offset: 0x00099D34
		private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
		{
			DbParameter dbParameter = command.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
			dbParameter.Direction = direction;
			command.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0009BB6C File Offset: 0x00099D6C
		private void CheckParam(string pName, string p, int length)
		{
			if (p == null)
			{
				throw new ArgumentNullException(pName);
			}
			if (p.Length == 0 || p.Length > length || p.IndexOf(',') != -1)
			{
				throw new ArgumentException("invalid format for " + pName);
			}
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0009BBA8 File Offset: 0x00099DA8
		private static int GetReturnValue(DbParameter returnValue)
		{
			object value = returnValue.Value;
			if (!(value is int))
			{
				return -1;
			}
			return (int)value;
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x0009BBCC File Offset: 0x00099DCC
		private ProfileInfo ReadProfileInfo(DbDataReader reader)
		{
			ProfileInfo profileInfo = null;
			try
			{
				string @string = reader.GetString(0);
				bool boolean = reader.GetBoolean(1);
				DateTime dateTime = reader.GetDateTime(2);
				DateTime dateTime2 = reader.GetDateTime(3);
				int @int = reader.GetInt32(4);
				profileInfo = new ProfileInfo(@string, boolean, dateTime2, dateTime, @int);
			}
			catch
			{
			}
			return profileInfo;
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0009BC24 File Offset: 0x00099E24
		private ProfileInfoCollection BuildProfileInfoCollection(DbDataReader reader, out int totalRecords)
		{
			ProfileInfoCollection profileInfoCollection = new ProfileInfoCollection();
			while (reader.Read())
			{
				ProfileInfo profileInfo = this.ReadProfileInfo(reader);
				if (profileInfo != null)
				{
					profileInfoCollection.Add(profileInfo);
				}
			}
			totalRecords = 0;
			if (reader.NextResult() && reader.Read())
			{
				totalRecords = reader.GetInt32(0);
			}
			return profileInfoCollection;
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0009BC70 File Offset: 0x00099E70
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

		// Token: 0x060039B6 RID: 14774 RVA: 0x0009BC90 File Offset: 0x00099E90
		private void DecodeProfileData(string allnames, string values, byte[] buf, SettingsPropertyValueCollection properties)
		{
			if (allnames == null || values == null || buf == null || properties == null)
			{
				return;
			}
			string[] array = allnames.Split(new char[] { ':' });
			for (int i = 0; i < array.Length; i += 4)
			{
				string text = array[i];
				SettingsPropertyValue settingsPropertyValue = properties[text];
				if (settingsPropertyValue != null)
				{
					int num = int.Parse(array[i + 2], Helpers.InvariantCulture);
					int num2 = int.Parse(array[i + 3], Helpers.InvariantCulture);
					if (num2 == -1 && !settingsPropertyValue.Property.PropertyType.IsValueType)
					{
						settingsPropertyValue.PropertyValue = null;
						settingsPropertyValue.IsDirty = false;
						settingsPropertyValue.Deserialized = true;
					}
					else if (array[i + 1] == "S" && num >= 0 && num2 > 0 && values.Length >= num + num2)
					{
						settingsPropertyValue.SerializedValue = values.Substring(num, num2);
					}
					else if (array[i + 1] == "B" && num >= 0 && num2 > 0 && buf.Length >= num + num2)
					{
						byte[] array2 = new byte[num2];
						Buffer.BlockCopy(buf, num, array2, 0, num2);
						settingsPropertyValue.SerializedValue = array2;
					}
				}
			}
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0009BDB4 File Offset: 0x00099FB4
		private void EncodeProfileData(ref string allNames, ref string allValues, ref byte[] buf, SettingsPropertyValueCollection properties, bool userIsAuthenticated)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				foreach (object obj in properties)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
					if ((userIsAuthenticated || (bool)settingsPropertyValue.Property.Attributes["AllowAnonymous"]) && (settingsPropertyValue.IsDirty || !settingsPropertyValue.UsingDefaultValue))
					{
						int num = 0;
						string text = null;
						int num2;
						if (settingsPropertyValue.Deserialized && settingsPropertyValue.PropertyValue == null)
						{
							num2 = -1;
						}
						else
						{
							object serializedValue = settingsPropertyValue.SerializedValue;
							if (serializedValue == null)
							{
								num2 = -1;
							}
							else if (serializedValue is string)
							{
								text = (string)serializedValue;
								num2 = text.Length;
								num = stringBuilder2.Length;
							}
							else
							{
								byte[] array = (byte[])serializedValue;
								num = (int)memoryStream.Position;
								memoryStream.Write(array, 0, array.Length);
								memoryStream.Position = (long)(num + array.Length);
								num2 = array.Length;
							}
						}
						stringBuilder.Append(string.Concat(new string[]
						{
							settingsPropertyValue.Name,
							":",
							(text != null) ? "S" : "B",
							":",
							num.ToString(Helpers.InvariantCulture),
							":",
							num2.ToString(Helpers.InvariantCulture),
							":"
						}));
						if (text != null)
						{
							stringBuilder2.Append(text);
						}
					}
				}
				buf = memoryStream.ToArray();
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			allNames = stringBuilder.ToString();
			allValues = stringBuilder2.ToString();
		}

		// Token: 0x04001F38 RID: 7992
		private ConnectionStringSettings connectionString;

		// Token: 0x04001F39 RID: 7993
		private DbProviderFactory factory;

		// Token: 0x04001F3A RID: 7994
		private string applicationName;

		// Token: 0x04001F3B RID: 7995
		private bool schemaIsOk;
	}
}
