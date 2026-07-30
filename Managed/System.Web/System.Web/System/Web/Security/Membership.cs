using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Validates user credentials and manages user settings. This class cannot be inherited.</summary>
	// Token: 0x020004C4 RID: 1220
	public static class Membership
	{
		// Token: 0x060036D7 RID: 14039 RVA: 0x0008FD64 File Offset: 0x0008DF64
		static Membership()
		{
			MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
			Membership.providers = new MembershipProviderCollection();
			ProvidersHelper.InstantiateProviders(membershipSection.Providers, Membership.providers, typeof(MembershipProvider));
			Membership.provider = Membership.providers[membershipSection.DefaultProvider];
			Membership.onlineTimeWindow = (int)membershipSection.UserIsOnlineTimeWindow.TotalMinutes;
			Membership.hashAlgorithmType = membershipSection.HashAlgorithmType;
			if (string.IsNullOrEmpty(Membership.hashAlgorithmType))
			{
				MachineKeySection machineKeySection = WebConfigurationManager.GetSection("system.web/machineKey") as MachineKeySection;
				Membership.hashAlgorithmType = new MachineKeyValidationConverter().ConvertTo(null, null, machineKeySection.Validation, typeof(string)) as string;
			}
			if (string.IsNullOrEmpty(Membership.hashAlgorithmType))
			{
				Membership.hashAlgorithmType = "SHA1";
			}
		}

		/// <summary>Adds a new user to the data store.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user.</returns>
		/// <param name="username">The user name for the new user. </param>
		/// <param name="password">The password for the new user. </param>
		/// <exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> property for a <see cref="T:System.Web.Security.MembershipCreateStatus" /> value. </exception>
		// Token: 0x060036D8 RID: 14040 RVA: 0x0008FE36 File Offset: 0x0008E036
		public static MembershipUser CreateUser(string username, string password)
		{
			return Membership.CreateUser(username, password, null);
		}

		/// <summary>Adds a new user with a specified e-mail address to the data store.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user.</returns>
		/// <param name="username">The user name for the new user. </param>
		/// <param name="password">The password for the new user. </param>
		/// <param name="email">The e-mail address for the new user. </param>
		/// <exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> property for a <see cref="T:System.Web.Security.MembershipCreateStatus" /> value. </exception>
		// Token: 0x060036D9 RID: 14041 RVA: 0x0008FE40 File Offset: 0x0008E040
		public static MembershipUser CreateUser(string username, string password, string email)
		{
			MembershipCreateStatus membershipCreateStatus;
			MembershipUser membershipUser = Membership.CreateUser(username, password, email, null, null, true, out membershipCreateStatus);
			if (membershipUser == null)
			{
				throw new MembershipCreateUserException(membershipCreateStatus);
			}
			return membershipUser;
		}

		/// <summary>Adds a new user with specified property values to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns null.</returns>
		/// <param name="username">The user name for the new user. </param>
		/// <param name="password">The password for the new user. </param>
		/// <param name="email">The e-mail address for the new user. </param>
		/// <param name="passwordQuestion">The password-question value for the membership user.</param>
		/// <param name="passwordAnswer">The password-answer value for the membership user.</param>
		/// <param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param>
		/// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> indicating that the user was created successfully or the reason that creation failed. </param>
		// Token: 0x060036DA RID: 14042 RVA: 0x0008FE64 File Offset: 0x0008E064
		public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
		{
			return Membership.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, null, out status);
		}

		/// <summary>Adds a new user with specified property values and a unique identifier to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns null.</returns>
		/// <param name="username">The user name for the new user.</param>
		/// <param name="password">The password for the new user.</param>
		/// <param name="email">The e-mail address for the new user.</param>
		/// <param name="passwordQuestion">The password-question value for the membership user.</param>
		/// <param name="passwordAnswer">The password-answer value for the membership user.</param>
		/// <param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param>
		/// <param name="providerUserKey">The user identifier for the user that should be stored in the membership data store.</param>
		/// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> indicating that the user was created successfully or the reason creation failed.</param>
		// Token: 0x060036DB RID: 14043 RVA: 0x0008FE78 File Offset: 0x0008E078
		public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			if (string.IsNullOrEmpty(username))
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}
			if (string.IsNullOrEmpty(password))
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			return Membership.Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
		}

		/// <summary>Deletes a user and any related user data from the database.</summary>
		/// <returns>true if the user was deleted; otherwise, false.</returns>
		/// <param name="username">The name of the user to delete. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string or contains a comma (,). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x060036DC RID: 14044 RVA: 0x0008FEB7 File Offset: 0x0008E0B7
		public static bool DeleteUser(string username)
		{
			return Membership.Provider.DeleteUser(username, true);
		}

		/// <summary>Deletes a user from the database.</summary>
		/// <returns>true if the user was deleted; otherwise, false.</returns>
		/// <param name="username">The name of the user to delete.</param>
		/// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> is an empty string or contains a comma (,). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x060036DD RID: 14045 RVA: 0x0008FEC5 File Offset: 0x0008E0C5
		public static bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			return Membership.Provider.DeleteUser(username, deleteAllRelatedData);
		}

		/// <summary>Generates a random password of the specified length.</summary>
		/// <returns>A random password of the specified length.</returns>
		/// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters. </param>
		/// <param name="numberOfNonAlphanumericCharacters">The minimum number of non-alphanumeric characters (such as @, #, !, %, &amp;, and so on) in the generated password.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="length" /> is less than 1 or greater than 128 -or-<paramref name="numberOfNonAlphanumericCharacters" /> is less than 0 or greater than <paramref name="length" />. </exception>
		// Token: 0x060036DE RID: 14046 RVA: 0x0008FED4 File Offset: 0x0008E0D4
		public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
		{
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[length];
			int num = 0;
			randomNumberGenerator.GetBytes(array);
			for (int i = 0; i < length; i++)
			{
				array[i] = array[i] % 93 + 33;
				if ((array[i] >= 33 && array[i] <= 47) || (array[i] >= 58 && array[i] <= 64) || (array[i] >= 91 && array[i] <= 96) || (array[i] >= 123 && array[i] <= 126))
				{
					num++;
				}
				if (array[i] == 34 || array[i] == 39)
				{
					byte[] array2 = array;
					int num2 = i;
					array2[num2] += 1;
				}
				else if (array[i] == 96)
				{
					byte[] array3 = array;
					int num3 = i;
					array3[num3] -= 1;
				}
			}
			if (num < numberOfNonAlphanumericCharacters)
			{
				int i = 0;
				while (i < length && num != numberOfNonAlphanumericCharacters)
				{
					if (array[i] >= 48 && array[i] <= 57)
					{
						array[i] = array[i] - 48 + 33;
						num++;
					}
					else if (array[i] >= 65 && array[i] <= 90)
					{
						array[i] = (array[i] - 65) % 13 + 33;
						num++;
					}
					else if (array[i] >= 97 && array[i] <= 122)
					{
						array[i] = (array[i] - 97) % 13 + 33;
						num++;
					}
					if (array[i] == 34 || array[i] == 39)
					{
						byte[] array4 = array;
						int num4 = i;
						array4[num4] += 1;
					}
					else if (array[i] == 96)
					{
						byte[] array5 = array;
						int num5 = i;
						array5[num5] -= 1;
					}
					i++;
				}
			}
			return Encoding.ASCII.GetString(array);
		}

		/// <summary>Gets a collection of all the users in the database.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all of the users in the database.</returns>
		// Token: 0x060036DF RID: 14047 RVA: 0x00090044 File Offset: 0x0008E244
		public static MembershipUserCollection GetAllUsers()
		{
			int num;
			return Membership.GetAllUsers(0, int.MaxValue, out num);
		}

		/// <summary>Gets a collection of all the users in the database in pages of data.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all the users in the database for the configured applicationName.</returns>
		/// <param name="pageIndex">The index of the page of results to return. Use 0 to indicate the first page.</param>
		/// <param name="pageSize">The size of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="totalRecords">The total number of users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.-or-<paramref name="pageSize" /> is less than 1.</exception>
		// Token: 0x060036E0 RID: 14048 RVA: 0x0009005E File Offset: 0x0008E25E
		public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			return Membership.Provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Gets the number of users currently accessing an application.</summary>
		/// <returns>The number of users currently accessing an application.</returns>
		// Token: 0x060036E1 RID: 14049 RVA: 0x0009006D File Offset: 0x0008E26D
		public static int GetNumberOfUsersOnline()
		{
			return Membership.Provider.GetNumberOfUsersOnline();
		}

		/// <summary>Gets the information from the data source and updates the last-activity date/time stamp for the current logged-on membership user.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the current logged-on user.</returns>
		/// <exception cref="T:System.ArgumentException">No membership user is currently logged in.</exception>
		// Token: 0x060036E2 RID: 14050 RVA: 0x00090079 File Offset: 0x0008E279
		public static MembershipUser GetUser()
		{
			return Membership.GetUser(HttpContext.Current.User.Identity.Name, true);
		}

		/// <summary>Gets the information from the data source for the current logged-on membership user. Updates the last-activity date/time stamp for the current logged-on membership user, if specified.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the current logged-on user.</returns>
		/// <param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user. </param>
		/// <exception cref="T:System.ArgumentException">No membership user is currently logged in.</exception>
		// Token: 0x060036E3 RID: 14051 RVA: 0x00090095 File Offset: 0x0008E295
		public static MembershipUser GetUser(bool userIsOnline)
		{
			return Membership.GetUser(HttpContext.Current.User.Identity.Name, userIsOnline);
		}

		/// <summary>Gets the information from the data source for the specified membership user.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If the <paramref name="username" /> parameter does not correspond to an existing user, this method returns null.</returns>
		/// <param name="username">The name of the user to retrieve.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma (,). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x060036E4 RID: 14052 RVA: 0x000900B1 File Offset: 0x0008E2B1
		public static MembershipUser GetUser(string username)
		{
			return Membership.GetUser(username, false);
		}

		/// <summary>Gets the information from the data source for the specified membership user. Updates the last-activity date/time stamp for the user, if specified.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If the <paramref name="username" /> parameter does not correspond to an existing user, this method returns null.</returns>
		/// <param name="username">The name of the user to retrieve. </param>
		/// <param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma (,). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		// Token: 0x060036E5 RID: 14053 RVA: 0x000900BA File Offset: 0x0008E2BA
		public static MembershipUser GetUser(string username, bool userIsOnline)
		{
			return Membership.Provider.GetUser(username, userIsOnline);
		}

		/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier.</returns>
		/// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerUserKey" /> is null. </exception>
		// Token: 0x060036E6 RID: 14054 RVA: 0x000900C8 File Offset: 0x0008E2C8
		public static MembershipUser GetUser(object providerUserKey)
		{
			return Membership.GetUser(providerUserKey, false);
		}

		/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier. Updates the last-activity date/time stamp for the user, if specified.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier.</returns>
		/// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param>
		/// <param name="userIsOnline">If true, updates the last-activity date/time stamp for the specified user.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerUserKey" /> is null. </exception>
		// Token: 0x060036E7 RID: 14055 RVA: 0x000900D1 File Offset: 0x0008E2D1
		public static MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			return Membership.Provider.GetUser(providerUserKey, userIsOnline);
		}

		/// <summary>Gets a user name where the e-mail address for the user matches the specified e-mail address.</summary>
		/// <returns>The user name where the e-mail address for the user matches the specified e-mail address. If no match is found, null is returned.</returns>
		/// <param name="emailToMatch">The e-mail address to search for. </param>
		// Token: 0x060036E8 RID: 14056 RVA: 0x000900DF File Offset: 0x0008E2DF
		public static string GetUserNameByEmail(string emailToMatch)
		{
			return Membership.Provider.GetUserNameByEmail(emailToMatch);
		}

		/// <summary>Updates the database with the information for the specified user.</summary>
		/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to be updated and the updated information for the user. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="user" /> is null.</exception>
		// Token: 0x060036E9 RID: 14057 RVA: 0x000900EC File Offset: 0x0008E2EC
		public static void UpdateUser(MembershipUser user)
		{
			Membership.Provider.UpdateUser(user);
		}

		/// <summary>Verifies that the supplied user name and password are valid.</summary>
		/// <returns>true if the supplied user name and password are valid; otherwise, false.</returns>
		/// <param name="username">The name of the user to be validated. </param>
		/// <param name="password">The password for the specified user. </param>
		// Token: 0x060036EA RID: 14058 RVA: 0x000900F9 File Offset: 0x0008E2F9
		public static bool ValidateUser(string username, string password)
		{
			return Membership.Provider.ValidateUser(username, password);
		}

		/// <summary>Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains all users that match the <paramref name="emailToMatch" /> parameter.Leading and trailing spaces are trimmed from the <paramref name="emailToMatch" /> parameter value.</returns>
		/// <param name="emailToMatch">The e-mail address to search for.</param>
		// Token: 0x060036EB RID: 14059 RVA: 0x00090108 File Offset: 0x0008E308
		public static MembershipUserCollection FindUsersByEmail(string emailToMatch)
		{
			int num;
			return Membership.Provider.FindUsersByEmail(emailToMatch, 0, int.MaxValue, out num);
		}

		/// <summary>Gets a collection of membership users, in a page of data, where the e-mail address contains the specified e-mail address to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
		/// <param name="emailToMatch">The e-mail address to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pageIndex" /> is less than zero.-or-<paramref name="pageSize" /> is less than 1.</exception>
		// Token: 0x060036EC RID: 14060 RVA: 0x00090128 File Offset: 0x0008E328
		public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return Membership.Provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Gets a collection of membership users where the user name contains the specified user name to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains all users that match the <paramref name="usernameToMatch" /> parameter.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch" /> parameter value.</returns>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		// Token: 0x060036ED RID: 14061 RVA: 0x00090138 File Offset: 0x0008E338
		public static MembershipUserCollection FindUsersByName(string usernameToMatch)
		{
			int num;
			return Membership.Provider.FindUsersByName(usernameToMatch, 0, int.MaxValue, out num);
		}

		/// <summary>Gets a collection of membership users, in a page of data, where the user name contains the specified user name to match.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch" /> parameter value.</returns>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
		/// <param name="pageSize">The size of the page of results to return.</param>
		/// <param name="totalRecords">The total number of matched users.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usernameToMatch" /> is an empty string.-or-<paramref name="pageIndex" /> is less than zero.-or-<paramref name="pageSize" /> is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="usernameToMatch" /> is null.</exception>
		// Token: 0x060036EE RID: 14062 RVA: 0x00090158 File Offset: 0x0008E358
		public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return Membership.Provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x00090168 File Offset: 0x0008E368
		// (set) Token: 0x060036F0 RID: 14064 RVA: 0x00090174 File Offset: 0x0008E374
		public static string ApplicationName
		{
			get
			{
				return Membership.Provider.ApplicationName;
			}
			set
			{
				Membership.Provider.ApplicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the current membership provider is configured to allow users to reset their passwords.</summary>
		/// <returns>true if the membership provider supports password reset; otherwise, false.</returns>
		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x00090181 File Offset: 0x0008E381
		public static bool EnablePasswordReset
		{
			get
			{
				return Membership.Provider.EnablePasswordReset;
			}
		}

		/// <summary>Gets a value indicating whether the current membership provider is configured to allow users to retrieve their passwords.</summary>
		/// <returns>true if the membership provider supports password retrieval; otherwise, false.</returns>
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x0009018D File Offset: 0x0008E38D
		public static bool EnablePasswordRetrieval
		{
			get
			{
				return Membership.Provider.EnablePasswordRetrieval;
			}
		}

		/// <summary>The identifier of the algorithm used to hash passwords.</summary>
		/// <returns>The identifier of the algorithm used to hash passwords, or blank to use the default hash algorithm.</returns>
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x00090199 File Offset: 0x0008E399
		public static string HashAlgorithmType
		{
			get
			{
				return Membership.hashAlgorithmType;
			}
		}

		/// <summary>Gets a value indicating whether the default membership provider requires the user to answer a password question for password reset and retrieval.</summary>
		/// <returns>true if a password answer is required for password reset and retrieval; otherwise, false.</returns>
		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000901A0 File Offset: 0x0008E3A0
		public static bool RequiresQuestionAndAnswer
		{
			get
			{
				return Membership.Provider.RequiresQuestionAndAnswer;
			}
		}

		/// <summary>Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.</summary>
		/// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000901AC File Offset: 0x0008E3AC
		public static int MaxInvalidPasswordAttempts
		{
			get
			{
				return Membership.Provider.MaxInvalidPasswordAttempts;
			}
		}

		/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
		/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000901B8 File Offset: 0x0008E3B8
		public static int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return Membership.Provider.MinRequiredNonAlphanumericCharacters;
			}
		}

		/// <summary>Gets the minimum length required for a password.</summary>
		/// <returns>The minimum length required for a password. </returns>
		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000901C4 File Offset: 0x0008E3C4
		public static int MinRequiredPasswordLength
		{
			get
			{
				return Membership.Provider.MinRequiredPasswordLength;
			}
		}

		/// <summary>Gets the time window between which consecutive failed attempts to provide a valid password or password answer are tracked.</summary>
		/// <returns>The time window, in minutes, during which consecutive failed attempts to provide a valid password or password answer are tracked. The default is 10 minutes. If the interval between the current failed attempt and the last failed attempt is greater than the <see cref="P:System.Web.Security.Membership.PasswordAttemptWindow" /> property setting, each failed attempt is treated as if it were the first failed attempt.</returns>
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000901D0 File Offset: 0x0008E3D0
		public static int PasswordAttemptWindow
		{
			get
			{
				return Membership.Provider.PasswordAttemptWindow;
			}
		}

		/// <summary>Gets the regular expression used to evaluate a password.</summary>
		/// <returns>A regular expression used to evaluate a password.</returns>
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000901DC File Offset: 0x0008E3DC
		public static string PasswordStrengthRegularExpression
		{
			get
			{
				return Membership.Provider.PasswordStrengthRegularExpression;
			}
		}

		/// <summary>Gets a reference to the default membership provider for the application.</summary>
		/// <returns>The default membership provider for the application exposed using the <see cref="T:System.Web.Security.MembershipProvider" /> abstract base class.</returns>
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000901E8 File Offset: 0x0008E3E8
		public static MembershipProvider Provider
		{
			get
			{
				return Membership.provider;
			}
		}

		/// <summary>Gets a collection of the membership providers for the ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipProviderCollection" /> of the membership providers configured for the ASP.NET application.</returns>
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000901EF File Offset: 0x0008E3EF
		public static MembershipProviderCollection Providers
		{
			get
			{
				return Membership.providers;
			}
		}

		/// <summary>Specifies the number of minutes after the last-activity date/time stamp for a user during which the user is considered online.</summary>
		/// <returns>The number of minutes after the last-activity date/time stamp for a user during which the user is considered online.</returns>
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000901F6 File Offset: 0x0008E3F6
		public static int UserIsOnlineTimeWindow
		{
			get
			{
				return Membership.onlineTimeWindow;
			}
		}

		/// <summary>Occurs when a user is created, a password is changed, or a password is reset.</summary>
		// Token: 0x14000105 RID: 261
		// (add) Token: 0x060036FD RID: 14077 RVA: 0x000901FD File Offset: 0x0008E3FD
		// (remove) Token: 0x060036FE RID: 14078 RVA: 0x0009020A File Offset: 0x0008E40A
		public static event MembershipValidatePasswordEventHandler ValidatingPassword
		{
			add
			{
				Membership.Provider.ValidatingPassword += value;
			}
			remove
			{
				Membership.Provider.ValidatingPassword -= value;
			}
		}

		// Token: 0x04001DE0 RID: 7648
		private static MembershipProviderCollection providers;

		// Token: 0x04001DE1 RID: 7649
		private static MembershipProvider provider;

		// Token: 0x04001DE2 RID: 7650
		private static int onlineTimeWindow;

		// Token: 0x04001DE3 RID: 7651
		private static string hashAlgorithmType;
	}
}
