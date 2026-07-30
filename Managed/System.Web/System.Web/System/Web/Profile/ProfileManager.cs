using System;
using System.Configuration;
using System.Web.Configuration;
using Unity;

namespace System.Web.Profile
{
	/// <summary>Manages user profile data and settings.</summary>
	// Token: 0x0200050A RID: 1290
	public static class ProfileManager
	{
		/// <summary>Deletes user profile data for which the last activity date and time occurred before the specified date and time.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x06003963 RID: 14691 RVA: 0x0009A8FC File Offset: 0x00098AFC
		public static int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.DeleteInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		/// <summary>Deletes the profile for the specified user name from the data source.</summary>
		/// <returns>true if the user profile was found and deleted; otherwise, false.</returns>
		/// <param name="username">The user name for the profile to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string ("") or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x06003964 RID: 14692 RVA: 0x0009A90A File Offset: 0x00098B0A
		public static bool DeleteProfile(string username)
		{
			return ProfileManager.Provider.DeleteProfiles(new string[] { username }) > 0;
		}

		/// <summary>Deletes profile properties and information for the supplied list of user names.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="usernames">A string array of user names for profiles to be deleted. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="usernames" /> is zero.- or -One of the items in <paramref name="usernames" /> is an empty string ("") or contains a comma.- or -Two or more items in <paramref name="usernames" /> have the same value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernames" /> is null.- or -One of the items in <paramref name="usernames" /> is null.</exception>
		// Token: 0x06003965 RID: 14693 RVA: 0x0009A923 File Offset: 0x00098B23
		public static int DeleteProfiles(string[] usernames)
		{
			return ProfileManager.Provider.DeleteProfiles(usernames);
		}

		/// <summary>Deletes profile properties and information from the data source for the supplied list of profiles.</summary>
		/// <returns>The number of profiles deleted from the data source.</returns>
		/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  that contains profile information for profiles to be deleted.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfoCollection.Count" /> value of zero.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is an empty string ("") or contains a comma.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="profiles" /> is null.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is null.</exception>
		// Token: 0x06003966 RID: 14694 RVA: 0x0009A930 File Offset: 0x00098B30
		public static int DeleteProfiles(ProfileInfoCollection profiles)
		{
			return ProfileManager.Provider.DeleteProfiles(profiles);
		}

		/// <summary>Retrieves profile information for all profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles in which the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").</exception>
		// Token: 0x06003967 RID: 14695 RVA: 0x0009A940 File Offset: 0x00098B40
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			int num = 0;
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves profile information in pages of data for profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
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
		///   <paramref name="usernameToMatch" /> is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than 1.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06003968 RID: 14696 RVA: 0x0009A964 File Offset: 0x00098B64
		public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves all profile information for profiles in which the user name matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").</exception>
		// Token: 0x06003969 RID: 14697 RVA: 0x0009A978 File Offset: 0x00098B78
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch)
		{
			int num = 0;
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves profile information in pages of data for profiles in which the user name matches the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="usernameToMatch">The user name for which to search.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than 1.- or -<paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600396A RID: 14698 RVA: 0x0009A99B File Offset: 0x00098B9B
		public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves all user profile data for profiles in which the last activity date occurred on or before the specified date and time.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x0600396B RID: 14699 RVA: 0x0009A9B0 File Offset: 0x00098BB0
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			int num = 0;
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves a page of <see cref="T:System.Web.Profile.ProfileInfo" /> objects for user profiles in which the last activity date occurred on or before the specified date and time.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		// Token: 0x0600396C RID: 14700 RVA: 0x0009A9D3 File Offset: 0x00098BD3
		public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Retrieves user profile data for profiles in the data source.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		// Token: 0x0600396D RID: 14701 RVA: 0x0009A9E8 File Offset: 0x00098BE8
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption)
		{
			int num = 0;
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, 0, int.MaxValue, out num);
		}

		/// <summary>Retrieves pages of user profile data.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
		// Token: 0x0600396E RID: 14702 RVA: 0x0009AA0A File Offset: 0x00098C0A
		public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			return ProfileManager.Provider.GetAllProfiles(authenticationOption, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Gets the number of profiles in which the last activity date occurred on or before the specified date.</summary>
		/// <returns>The number of profiles in the data source for which the last activity date occurred before the specified date and time.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> object that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
		// Token: 0x0600396F RID: 14703 RVA: 0x0009AA1A File Offset: 0x00098C1A
		public static int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return ProfileManager.Provider.GetNumberOfInactiveProfiles(authenticationOption, userInactiveSinceDate);
		}

		/// <summary>Gets the number of profiles in the data source.</summary>
		/// <returns>The number of profiles in the data source.</returns>
		/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
		// Token: 0x06003970 RID: 14704 RVA: 0x0009AA28 File Offset: 0x00098C28
		public static int GetNumberOfProfiles(ProfileAuthenticationOption authenticationOption)
		{
			int num = 0;
			ProfileManager.Provider.GetAllProfiles(authenticationOption, 0, 1, out num);
			return num;
		}

		/// <summary>Gets or sets the name of the application for which to store and retrieve profile information.</summary>
		/// <returns>The name of the application for which to store and retrieve profile information.</returns>
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x0009AA48 File Offset: 0x00098C48
		// (set) Token: 0x06003972 RID: 14706 RVA: 0x0009AA54 File Offset: 0x00098C54
		public static string ApplicationName
		{
			get
			{
				return ProfileManager.Provider.ApplicationName;
			}
			set
			{
				ProfileManager.Provider.ApplicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the user profile will be automatically saved at the end of the execution of an ASP.NET page.</summary>
		/// <returns>true if the user profile will be automatically saved at the end of the execution of an ASP.NET page; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.AutomaticSaveEnabled" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x0009AA61 File Offset: 0x00098C61
		public static bool AutomaticSaveEnabled
		{
			get
			{
				return ProfileManager.config.AutomaticSaveEnabled;
			}
		}

		/// <summary>Gets a value indicating whether the user profile is enabled for the application.</summary>
		/// <returns>true if the user profile is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x0009AA6D File Offset: 0x00098C6D
		public static bool Enabled
		{
			get
			{
				return ProfileManager.config.Enabled;
			}
		}

		/// <summary>Gets a reference to the default profile provider for the application.</summary>
		/// <returns>The default profile provider for the application.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Provider" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x0009AA79 File Offset: 0x00098C79
		[global::System.MonoTODO("check AspNetHostingPermissionLevel")]
		public static ProfileProvider Provider
		{
			get
			{
				ProfileProvider profileProvider = ProfileManager.Providers[ProfileManager.config.DefaultProvider];
				if (profileProvider == null)
				{
					throw new ConfigurationErrorsException("Provider '" + ProfileManager.config.DefaultProvider + "' was not found");
				}
				return profileProvider;
			}
		}

		/// <summary>Gets a collection of the profile providers for the ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileProviderCollection" /> of the profile providers configured for the ASP.NET application.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x0009AAB4 File Offset: 0x00098CB4
		public static ProfileProviderCollection Providers
		{
			get
			{
				ProfileManager.CheckEnabled();
				if (ProfileManager.providersCollection == null)
				{
					ProfileProviderCollection profileProviderCollection = new ProfileProviderCollection();
					ProvidersHelper.InstantiateProviders(ProfileManager.config.Providers, profileProviderCollection, typeof(ProfileProvider));
					ProfileManager.providersCollection = profileProviderCollection;
				}
				return ProfileManager.providersCollection;
			}
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x0009AAF8 File Offset: 0x00098CF8
		private static void CheckEnabled()
		{
			if (!ProfileManager.Enabled)
			{
				throw new Exception("This feature is not enabled.  To enable it, add <profile enabled=\"true\"> to your configuration file.");
			}
		}

		/// <summary>Adds a profile property programmatically.</summary>
		/// <param name="property">The property settings to be added.</param>
		// Token: 0x06003978 RID: 14712 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static void AddDynamicProfileProperty(ProfilePropertySettings property)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F2B RID: 7979
		private static ProfileSection config = (ProfileSection)WebConfigurationManager.GetSection("system.web/profile");

		// Token: 0x04001F2C RID: 7980
		private static ProfileProviderCollection providersCollection;
	}
}
