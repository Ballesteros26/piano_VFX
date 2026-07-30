using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Implements management and administrative functionality for Web Parts personalization. This class cannot be inherited. </summary>
	// Token: 0x020007AF RID: 1967
	public static class PersonalizationAdministration
	{
		/// <summary>Gets or sets the name of the application specified by the provider.</summary>
		/// <returns>The application name. </returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F5C RID: 20316 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public static string ApplicationName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns an instance of the default personalization provider.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProvider" /> of the default provider.</returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004F5D RID: 20317 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationProvider Provider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of personalization providers indexed by name.</summary>
		/// <returns>A read-only <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationProviderCollection" /> that contains the personalization providers available to the application.</returns>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06004F5E RID: 20318 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationProviderCollection Providers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of per-user personalization state information for inactive users, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F5F RID: 20319 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information for inactive users, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="userInactiveSinceDate">The last date personalization information was accessed.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero- or -<paramref name="pageSize" /> is less than or equal to zero.- or -the combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F60 RID: 20320 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of shared personalization state information based on the specified path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains shared personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> is an empty string ("") either before or after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F61 RID: 20321 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of shared personalization state information based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains shared personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F62 RID: 20322 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information based on the user name and page path.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F63 RID: 20323 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of per-user personalization state information based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> that contains per-user personalization state information.</returns>
		/// <param name="pathToMatch">The path of the page to match.</param>
		/// <param name="usernameToMatch">The user name to match that has personalization data associated with the page.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number or records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" /> or <paramref name="usernameToMatch" /> before or after trimming is an empty string ("").- or -<paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.-or-The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or - The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F64 RID: 20324 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of all per-user personalization state information associated with inactive users, based on the specified date.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instances.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F65 RID: 20325 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a subset of all per-user personalization state information associated with inactive users, based on the specified parameters.</summary>
		/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instances.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.- or -The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F66 RID: 20326 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a collection of all personalization state information from the underlying data store for the requested personalization scope.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing state information for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization information to be retrieved.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		// Token: 0x06004F67 RID: 20327 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a subset of all personalization state information from the underlying data store, based on the specified parameters.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> containing state information for the specified scope and parameters.</returns>
		/// <param name="scope">The scope of the personalization information to be retrieved.</param>
		/// <param name="pageIndex">The zero-based index of the page of results to return. </param>
		/// <param name="pageSize">The number of records to return.</param>
		/// <param name="totalRecords">The total number of records available.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.- or -<paramref name="pageSize" /> is less than or equal to zero.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The combination of <paramref name="pageIndex" /> and <paramref name="pageSize" /> exceeds <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		// Token: 0x06004F68 RID: 20328 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope, int pageIndex, int pageSize, out int totalRecords)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a count of the per-user personalization items in the underlying data store for inactive users, based on the parameter specified.</summary>
		/// <returns>The number of personalization items for inactive users.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F69 RID: 20329 RVA: 0x000CB688 File Offset: 0x000C9888
		public static int GetCountOfInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the per-user personalization items in the underlying data store for inactive users, based on the specified parameters.</summary>
		/// <returns>The count of the per-user personalization items in the underlying data store for inactive users.</returns>
		/// <param name="pathToMatch">The path to the page with personalization state items to retrieve.</param>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pathToMatch" />, after trimming, is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F6A RID: 20330 RVA: 0x000CB6A4 File Offset: 0x000C98A4
		public static int GetCountOfInactiveUserState(string pathToMatch, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified scope.</summary>
		/// <returns>The number of personalization state items for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization state items to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F6B RID: 20331 RVA: 0x000CB6C0 File Offset: 0x000C98C0
		public static int GetCountOfState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified parameters.</summary>
		/// <returns>The number of personalization state items for the specified scope.</returns>
		/// <param name="scope">The scope of the personalization state items to retrieve.</param>
		/// <param name="pathToMatch">The path to the page with personalization state items to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or -<paramref name="pathToMatch" />, after trimming, is an empty string ("").- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F6C RID: 20332 RVA: 0x000CB6DC File Offset: 0x000C98DC
		public static int GetCountOfState(PersonalizationScope scope, string pathToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Returns a count of the personalization state items in the underlying data store that exist for the specified user.</summary>
		/// <returns>The number of personalization state items for the specified user.</returns>
		/// <param name="usernameToMatch">The user name associated with the personalization state information to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F6D RID: 20333 RVA: 0x000CB6F8 File Offset: 0x000C98F8
		public static int GetCountOfUserState(string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all personalization data in the underlying data store by deleting all rows associated with the specified scope.</summary>
		/// <returns>The number of items that were reset.</returns>
		/// <param name="scope">The scope associated with the personalization data to be deleted.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scope" /> is not a valid member of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count of affected rows.</exception>
		// Token: 0x06004F6E RID: 20334 RVA: 0x000CB714 File Offset: 0x000C9914
		public static int ResetAllState(PersonalizationScope scope)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user personalization state information in the underlying data store, based on the specified parameter.</summary>
		/// <returns>The number of rows in the underlying data store that were reset.</returns>
		/// <param name="userInactiveSinceDate">The last date a user's personalization information was accessed.</param>
		/// <exception cref="T:System.ArgumentException">The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F6F RID: 20335 RVA: 0x000CB730 File Offset: 0x000C9930
		public static int ResetInactiveUserState(DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state information in the underlying data store for inactive users, based on the specified parameters.</summary>
		/// <returns>The number of rows in the underlying data store that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="userInactiveSinceDate">The last active date to be used in resetting user state personalization items.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider returned a negative number for the count.</exception>
		// Token: 0x06004F70 RID: 20336 RVA: 0x000CB74C File Offset: 0x000C994C
		public static int ResetInactiveUserState(string path, DateTime userInactiveSinceDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets shared state in the underlying data store for the specified path.</summary>
		/// <returns>true if personalization shared state was reset; otherwise, false.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was either a negative number or was greater than one.</exception>
		// Token: 0x06004F71 RID: 20337 RVA: 0x000CB768 File Offset: 0x000C9968
		public static bool ResetSharedState(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Resets shared state in the underlying data store for the specified paths.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="paths">The paths to the pages associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="paths" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paths" /> is a zero-length array.- or -A member of <paramref name="paths" /> is either null or an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F72 RID: 20338 RVA: 0x000CB784 File Offset: 0x000C9984
		public static int ResetSharedState(string[] paths)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets personalization data in the underlying data store, based on the items contained in the collection.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="data">A collection of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" /> objects indicating what data should be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is an empty collection.- or -An element of the collection is null.- or -The path value of a <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instance in the collection is null or an empty string ("").- or -The <paramref name="user" /> value of a <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> instance in the collection is null, an empty string, or contains commas.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F73 RID: 20339 RVA: 0x000CB7A0 File Offset: 0x000C99A0
		public static int ResetState(PersonalizationStateInfoCollection data)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state in the underlying data store for the specified path.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("").- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F74 RID: 20340 RVA: 0x000CB7BC File Offset: 0x000C99BC
		public static int ResetUserState(string path)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets per-user state in the underlying data store for the specified combination of user name and path.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="username">The user name associated with the personalization data to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> or <paramref name="username" /> is an empty string ("") after trimming.- or -<paramref name="username" /> contains commas.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or - The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F75 RID: 20341 RVA: 0x000CB7D8 File Offset: 0x000C99D8
		public static bool ResetUserState(string path, string username)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Resets per-user state in the underlying data store for the page and users specified.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="path">The path to the page associated with the personalization state information to be reset.</param>
		/// <param name="usernames">The user names associated with the personalization data to be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string ("") after trimming.- or -A member of <paramref name="usernames" /> is null, an empty string after trimming, or contains commas.- or -<paramref name="usernames" /> is a zero-length array.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F76 RID: 20342 RVA: 0x000CB7F4 File Offset: 0x000C99F4
		public static int ResetUserState(string path, string[] usernames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Resets all per-user state in the underlying data store for the specified paths.</summary>
		/// <returns>The number of rows that were reset.</returns>
		/// <param name="usernames">An array of user names whose per-user data should be reset.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernames" /> is a zero-length array.- or -A member of <paramref name="usernames" /> is either null, contains commas, or is an empty string ("") after trimming.- or -The provider for a personalization provider defined in configuration is not of the correct type.- or -The length of the string of any parameter is greater than 256 characters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A configuration exception occurred while attempting to create and initialize an instance of one of the configured personalization providers.- or -The default personalization provider defined in configuration could not be found.</exception>
		/// <exception cref="T:System.Web.HttpException">The default provider indicated that the number of deleted rows was a negative number.</exception>
		// Token: 0x06004F77 RID: 20343 RVA: 0x000CB810 File Offset: 0x000C9A10
		public static int ResetUserState(string[] usernames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
