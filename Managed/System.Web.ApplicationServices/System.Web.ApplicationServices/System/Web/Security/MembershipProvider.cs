using System;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Runtime.CompilerServices;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Defines the contract that ASP.NET implements to provide membership services using custom membership providers.</summary>
	// Token: 0x02000016 RID: 22
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class MembershipProvider : ProviderBase
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000291D File Offset: 0x00000B1D
		internal static IMembershipHelper Helper
		{
			get
			{
				return MembershipProvider.helper;
			}
		}

		/// <summary>Occurs when a user is created, a password is changed, or a password is reset.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000059 RID: 89 RVA: 0x00002924 File Offset: 0x00000B24
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x00002937 File Offset: 0x00000B37
		public event MembershipValidatePasswordEventHandler ValidatingPassword
		{
			add
			{
				this.events.AddHandler(MembershipProvider.validatingPasswordEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(MembershipProvider.validatingPasswordEvent, value);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000294C File Offset: 0x00000B4C
		static MembershipProvider()
		{
			Type type = Type.GetType("System.Web.Security.MembershipHelper, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false);
			if (type == null)
			{
				return;
			}
			try
			{
				MembershipProvider.helper = Activator.CreateInstance(type) as IMembershipHelper;
			}
			catch
			{
			}
		}

		/// <summary>Processes a request to update the password for a membership user.</summary>
		/// <returns>true if the password was updated successfully; otherwise, false.</returns>
		/// <param name="username">The user to update the password for. </param>
		/// <param name="oldPassword">The current password for the specified user. </param>
		/// <param name="newPassword">The new password for the specified user. </param>
		// Token: 0x0600005D RID: 93
		public abstract bool ChangePassword(string username, string oldPassword, string newPassword);

		/// <summary>Processes a request to update the password question and answer for a membership user.</summary>
		/// <returns>true if the password question and answer are updated successfully; otherwise, false.</returns>
		/// <param name="username">The user to change the password question and answer for. </param>
		/// <param name="password">The password for the specified user. </param>
		/// <param name="newPasswordQuestion">The new password question for the specified user. </param>
		/// <param name="newPasswordAnswer">The new password answer for the specified user. </param>
		// Token: 0x0600005E RID: 94
		public abstract bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

		/// <summary>Adds a new membership user to the data source.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the information for the newly created user.</returns>
		/// <param name="username">The user name for the new user. </param>
		/// <param name="password">The password for the new user. </param>
		/// <param name="email">The e-mail address for the new user.</param>
		/// <param name="passwordQuestion">The password question for the new user.</param>
		/// <param name="passwordAnswer">The password answer for the new user</param>
		/// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
		/// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
		/// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
		// Token: 0x0600005F RID: 95
		public abstract MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);

		/// <summary>Removes a user from the membership data source. </summary>
		/// <returns>true if the user was successfully deleted; otherwise, false.</returns>
		/// <param name="username">The name of the user to delete.</param>
		/// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
		// Token: 0x06000060 RID: 96
		public abstract bool DeleteUser(string username, bool deleteAllRelatedData);

		/// <summary>Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="emailToMatch">The e-mail address to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		// Token: 0x06000061 RID: 97
		public abstract MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>Gets a collection of membership users where the user name contains the specified user name to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		// Token: 0x06000062 RID: 98
		public abstract MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

		/// <summary>Gets a collection of all the users in the data source in pages of data.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		// Token: 0x06000063 RID: 99
		public abstract MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

		/// <summary>Gets the number of users currently accessing the application.</summary>
		/// <returns>The number of users currently accessing the application.</returns>
		// Token: 0x06000064 RID: 100
		public abstract int GetNumberOfUsersOnline();

		/// <summary>Gets the password for the specified user name from the data source.</summary>
		/// <returns>The password for the specified user name.</returns>
		/// <param name="username">The user to retrieve the password for. </param>
		/// <param name="answer">The password answer for the user. </param>
		// Token: 0x06000065 RID: 101
		public abstract string GetPassword(string username, string answer);

		/// <summary>Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.</returns>
		/// <param name="username">The name of the user to get information for. </param>
		/// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
		// Token: 0x06000066 RID: 102
		public abstract MembershipUser GetUser(string username, bool userIsOnline);

		/// <summary>Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.</returns>
		/// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
		/// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
		// Token: 0x06000067 RID: 103
		public abstract MembershipUser GetUser(object providerUserKey, bool userIsOnline);

		/// <summary>Gets the user name associated with the specified e-mail address.</summary>
		/// <returns>The user name associated with the specified e-mail address. If no match is found, return null.</returns>
		/// <param name="email">The e-mail address to search for. </param>
		// Token: 0x06000068 RID: 104
		public abstract string GetUserNameByEmail(string email);

		/// <summary>Resets a user's password to a new, automatically generated password.</summary>
		/// <returns>The new password for the specified user.</returns>
		/// <param name="username">The user to reset the password for. </param>
		/// <param name="answer">The password answer for the specified user. </param>
		// Token: 0x06000069 RID: 105
		public abstract string ResetPassword(string username, string answer);

		/// <summary>Updates information about a user in the data source.</summary>
		/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user. </param>
		// Token: 0x0600006A RID: 106
		public abstract void UpdateUser(MembershipUser user);

		/// <summary>Verifies that the specified user name and password exist in the data source.</summary>
		/// <returns>true if the specified username and password are valid; otherwise, false.</returns>
		/// <param name="username">The name of the user to validate. </param>
		/// <param name="password">The password for the specified user. </param>
		// Token: 0x0600006B RID: 107
		public abstract bool ValidateUser(string username, string password);

		/// <summary>Clears a lock so that the membership user can be validated.</summary>
		/// <returns>true if the membership user was successfully unlocked; otherwise, false.</returns>
		/// <param name="userName">The membership user whose lock status you want to clear.</param>
		// Token: 0x0600006C RID: 108
		public abstract bool UnlockUser(string userName);

		/// <summary>The name of the application using the custom membership provider.</summary>
		/// <returns>The name of the application using the custom membership provider.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109
		// (set) Token: 0x0600006E RID: 110
		public abstract string ApplicationName { get; set; }

		/// <summary>Indicates whether the membership provider is configured to allow users to reset their passwords.</summary>
		/// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006F RID: 111
		public abstract bool EnablePasswordReset { get; }

		/// <summary>Indicates whether the membership provider is configured to allow users to retrieve their passwords.</summary>
		/// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000070 RID: 112
		public abstract bool EnablePasswordRetrieval { get; }

		/// <summary>Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.</summary>
		/// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000071 RID: 113
		public abstract bool RequiresQuestionAndAnswer { get; }

		/// <summary>Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.</summary>
		/// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000072 RID: 114
		public abstract int MaxInvalidPasswordAttempts { get; }

		/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
		/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000073 RID: 115
		public abstract int MinRequiredNonAlphanumericCharacters { get; }

		/// <summary>Gets the minimum length required for a password.</summary>
		/// <returns>The minimum length required for a password. </returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000074 RID: 116
		public abstract int MinRequiredPasswordLength { get; }

		/// <summary>Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.</summary>
		/// <returns>The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000075 RID: 117
		public abstract int PasswordAttemptWindow { get; }

		/// <summary>Gets a value indicating the format for storing passwords in the membership data store.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000076 RID: 118
		public abstract MembershipPasswordFormat PasswordFormat { get; }

		/// <summary>Gets the regular expression used to evaluate a password.</summary>
		/// <returns>A regular expression used to evaluate a password.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000077 RID: 119
		public abstract string PasswordStrengthRegularExpression { get; }

		/// <summary>Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.</summary>
		/// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000078 RID: 120
		public abstract bool RequiresUniqueEmail { get; }

		/// <summary>Raises the <see cref="E:System.Web.Security.MembershipProvider.ValidatingPassword" /> event if an event handler has been defined.</summary>
		/// <param name="e">The <see cref="T:System.Web.Security.ValidatePasswordEventArgs" /> to pass to the <see cref="E:System.Web.Security.MembershipProvider.ValidatingPassword" /> event handler.</param>
		// Token: 0x06000079 RID: 121 RVA: 0x000029B4 File Offset: 0x00000BB4
		protected virtual void OnValidatingPassword(ValidatePasswordEventArgs e)
		{
			MembershipValidatePasswordEventHandler membershipValidatePasswordEventHandler = this.events[MembershipProvider.validatingPasswordEvent] as MembershipValidatePasswordEventHandler;
			if (membershipValidatePasswordEventHandler != null)
			{
				membershipValidatePasswordEventHandler(this, e);
			}
		}

		/// <summary>Decrypts an encrypted password.</summary>
		/// <returns>A byte array that contains the decrypted password.</returns>
		/// <param name="encodedPassword">A byte array that contains the encrypted password to decrypt.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Configuration.MachineKeySection.ValidationKey" /> property or <see cref="P:System.Web.Configuration.MachineKeySection.DecryptionKey" /> property is set to AutoGenerate.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x0600007A RID: 122 RVA: 0x000029E2 File Offset: 0x00000BE2
		protected virtual byte[] DecryptPassword(byte[] encodedPassword)
		{
			if (MembershipProvider.helper == null)
			{
				throw new PlatformNotSupportedException("This method is not available.");
			}
			return MembershipProvider.helper.DecryptPassword(encodedPassword);
		}

		/// <summary>Encrypts a password.</summary>
		/// <returns>A byte array that contains the encrypted password.</returns>
		/// <param name="password">A byte array that contains the password to encrypt.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Configuration.MachineKeySection.ValidationKey" /> property or <see cref="P:System.Web.Configuration.MachineKeySection.DecryptionKey" /> property is set to AutoGenerate.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x0600007B RID: 123 RVA: 0x00002A01 File Offset: 0x00000C01
		protected virtual byte[] EncryptPassword(byte[] password)
		{
			return this.EncryptPassword(password, MembershipPasswordCompatibilityMode.Framework20);
		}

		/// <summary>Encrypts the specified password using the specified password-compatibility mode.</summary>
		/// <returns>A byte array that contains the encrypted password.</returns>
		/// <param name="password">A byte array that contains the password to encrypt.</param>
		/// <param name="legacyPasswordCompatibilityMode">The membership password-compatibility mode.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Configuration.MachineKeySection.ValidationKey" /> property or <see cref="P:System.Web.Configuration.MachineKeySection.DecryptionKey" /> property is set to AutoGenerate.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x0600007C RID: 124 RVA: 0x00002A0B File Offset: 0x00000C0B
		[MonoTODO("Discover what actually is 4.0 password compatibility mode.")]
		protected virtual byte[] EncryptPassword(byte[] password, MembershipPasswordCompatibilityMode legacyPasswordCompatibilityMode)
		{
			if (MembershipProvider.helper == null)
			{
				throw new PlatformNotSupportedException("This method is not available.");
			}
			if (legacyPasswordCompatibilityMode == MembershipPasswordCompatibilityMode.Framework40)
			{
				throw new PlatformNotSupportedException("Framework 4.0 password encryption mode is not supported at this time.");
			}
			return MembershipProvider.helper.EncryptPassword(password);
		}

		// Token: 0x0400005E RID: 94
		private const string HELPER_TYPE_NAME = "System.Web.Security.MembershipHelper, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x0400005F RID: 95
		private static IMembershipHelper helper;

		// Token: 0x04000060 RID: 96
		private static readonly object validatingPasswordEvent = new object();

		// Token: 0x04000061 RID: 97
		private EventHandlerList events = new EventHandlerList();
	}
}
