using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security
{
	/// <summary>Manages user membership in roles for authorization checking in an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x020004CC RID: 1228
	public static class Roles
	{
		/// <summary>Adds the specified users to the specified role.</summary>
		/// <param name="usernames">A string array of user names to add to the specified role. </param>
		/// <param name="roleName">The role to add the specified user names to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-One of the elements in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the elements in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003781 RID: 14209 RVA: 0x000910A5 File Offset: 0x0008F2A5
		public static void AddUsersToRole(string[] usernames, string roleName)
		{
			Roles.Provider.AddUsersToRoles(usernames, new string[] { roleName });
		}

		/// <summary>Adds the specified users to the specified roles.</summary>
		/// <param name="usernames">A string array of user names to add to the specified roles. </param>
		/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-One of the users in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003782 RID: 14210 RVA: 0x000910BC File Offset: 0x0008F2BC
		public static void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			Roles.Provider.AddUsersToRoles(usernames, roleNames);
		}

		/// <summary>Adds the specified user to the specified role.</summary>
		/// <param name="username">The user name to add to the specified role.</param>
		/// <param name="roleName">The role to add the specified user name to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled. -or-User is already assigned to the specified role.</exception>
		// Token: 0x06003783 RID: 14211 RVA: 0x000910CA File Offset: 0x0008F2CA
		public static void AddUserToRole(string username, string roleName)
		{
			Roles.Provider.AddUsersToRoles(new string[] { username }, new string[] { roleName });
		}

		/// <summary>Adds the specified user to the specified roles.</summary>
		/// <param name="username">The user name to add to the specified roles. </param>
		/// <param name="roleNames">A string array of roles to add the specified user name to. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003784 RID: 14212 RVA: 0x000910EA File Offset: 0x0008F2EA
		public static void AddUserToRoles(string username, string[] roleNames)
		{
			Roles.Provider.AddUsersToRoles(new string[] { username }, roleNames);
		}

		/// <summary>Adds a new role to the data source.</summary>
		/// <param name="roleName">The name of the role to create. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.-or-<paramref name="roleName" /> contains a comma.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003785 RID: 14213 RVA: 0x00091101 File Offset: 0x0008F301
		public static void CreateRole(string roleName)
		{
			Roles.Provider.CreateRole(roleName);
		}

		/// <summary>Deletes the cookie where role names are cached.</summary>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003786 RID: 14214 RVA: 0x00091110 File Offset: 0x0008F310
		public static void DeleteCookie()
		{
			if (Roles.CacheRolesInCookie)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					throw new HttpException("Context is null.");
				}
				HttpResponse response = httpContext.Response;
				if (response == null)
				{
					throw new HttpException("Response is null.");
				}
				HttpCookieCollection cookies = response.Cookies;
				cookies.Remove(Roles.CookieName);
				cookies.Add(new HttpCookie(Roles.CookieName, "")
				{
					Expires = new DateTime(1999, 10, 12),
					Path = Roles.CookiePath
				});
			}
		}

		/// <summary>Removes a role from the data source.</summary>
		/// <returns>true if <paramref name="roleName" /> was deleted from the data source; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to delete. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members.-or-Role management is not enabled.</exception>
		// Token: 0x06003787 RID: 14215 RVA: 0x0009118F File Offset: 0x0008F38F
		public static bool DeleteRole(string roleName)
		{
			return Roles.Provider.DeleteRole(roleName, true);
		}

		/// <summary>Removes a role from the data source.</summary>
		/// <returns>true if <paramref name="roleName" /> was deleted from the data source; otherwise; false.</returns>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If true, throws an exception if <paramref name="roleName" /> has one or more members.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">
		///   <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is true.-or-Role management is not enabled.</exception>
		// Token: 0x06003788 RID: 14216 RVA: 0x0009119D File Offset: 0x0008F39D
		public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			return Roles.Provider.DeleteRole(roleName, throwOnPopulatedRole);
		}

		/// <summary>Gets a list of all the roles for the application.</summary>
		/// <returns>A string array containing the names of all the roles stored in the data source for the application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003789 RID: 14217 RVA: 0x000911AB File Offset: 0x0008F3AB
		public static string[] GetAllRoles()
		{
			return Roles.Provider.GetAllRoles();
		}

		/// <summary>Gets a list of the roles that the currently logged-on user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the currently logged-on user is in.</returns>
		/// <exception cref="T:System.ArgumentNullException">There is no current logged-on user.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600378A RID: 14218 RVA: 0x000911B7 File Offset: 0x0008F3B7
		public static string[] GetRolesForUser()
		{
			return Roles.Provider.GetRolesForUser(Roles.CurrentUser);
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000911C8 File Offset: 0x0008F3C8
		private static string CurrentUser
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.User != null)
				{
					return HttpContext.Current.User.Identity.Name;
				}
				return Thread.CurrentPrincipal.Identity.Name;
			}
		}

		/// <summary>Gets a list of the roles that a user is in.</summary>
		/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
		/// <param name="username">The user to return a list of roles for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="username" /> contains a comma (,). </exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600378C RID: 14220 RVA: 0x00091201 File Offset: 0x0008F401
		public static string[] GetRolesForUser(string username)
		{
			return Roles.Provider.GetRolesForUser(username);
		}

		/// <summary>Gets a list of users in the specified role.</summary>
		/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
		/// <param name="roleName">The role to get the list of users for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600378D RID: 14221 RVA: 0x0009120E File Offset: 0x0008F40E
		public static string[] GetUsersInRole(string roleName)
		{
			return Roles.Provider.GetUsersInRole(roleName);
		}

		/// <summary>Gets a value indicating whether the currently logged-on user is in the specified role.</summary>
		/// <returns>true if the currently logged-on user is in the specified role; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-There is no current logged-on user.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600378E RID: 14222 RVA: 0x0009121B File Offset: 0x0008F41B
		public static bool IsUserInRole(string roleName)
		{
			return Roles.IsUserInRole(Roles.CurrentUser, roleName);
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified role.</summary>
		/// <returns>true if the specified user is in the specified role; otherwise, false.</returns>
		/// <param name="username">The name of the user to search for. </param>
		/// <param name="roleName">The name of the role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x0600378F RID: 14223 RVA: 0x00091228 File Offset: 0x0008F428
		public static bool IsUserInRole(string username, string roleName)
		{
			return !string.IsNullOrEmpty(username) && Roles.Provider.IsUserInRole(username, roleName);
		}

		/// <summary>Removes the specified user from the specified role.</summary>
		/// <param name="username">The user to remove from the specified role.</param>
		/// <param name="roleName">The role to remove the specified user from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,)<paramref name="username" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003790 RID: 14224 RVA: 0x00091240 File Offset: 0x0008F440
		public static void RemoveUserFromRole(string username, string roleName)
		{
			Roles.Provider.RemoveUsersFromRoles(new string[] { username }, new string[] { roleName });
		}

		/// <summary>Removes the specified user from the specified roles.</summary>
		/// <param name="username">The user to remove from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified user from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is null.-or-<paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-<paramref name="username" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003791 RID: 14225 RVA: 0x00091260 File Offset: 0x0008F460
		public static void RemoveUserFromRoles(string username, string[] roleNames)
		{
			Roles.Provider.RemoveUsersFromRoles(new string[] { username }, roleNames);
		}

		/// <summary>Removes the specified users from the specified role.</summary>
		/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
		/// <param name="roleName">The name of the role to remove the specified users from. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null.-or-One of the user names in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the user names in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003792 RID: 14226 RVA: 0x00091277 File Offset: 0x0008F477
		public static void RemoveUsersFromRole(string[] usernames, string roleName)
		{
			Roles.Provider.RemoveUsersFromRoles(usernames, new string[] { roleName });
		}

		/// <summary>Removes the specified user names from the specified roles.</summary>
		/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
		/// <param name="roleNames">A string array of role names to remove the specified users from. </param>
		/// <exception cref="T:System.ArgumentNullException">One of the roles specified in <paramref name="roleNames" /> is null.-or-One of the users specified in <paramref name="usernames" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">One of the roles specified in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users specified in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-<paramref name="roleNames" /> contains a duplicate element.-or-<paramref name="usernames" /> contains a duplicate element.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003793 RID: 14227 RVA: 0x0009128E File Offset: 0x0008F48E
		public static void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			Roles.Provider.RemoveUsersFromRoles(usernames, roleNames);
		}

		/// <summary>Gets a value indicating whether the specified role name already exists in the role data source.</summary>
		/// <returns>true if the role name already exists in the data source; otherwise, false.</returns>
		/// <param name="roleName">The name of the role to search for in the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003794 RID: 14228 RVA: 0x0009129C File Offset: 0x0008F49C
		public static bool RoleExists(string roleName)
		{
			return Roles.Provider.RoleExists(roleName);
		}

		/// <summary>Gets a list of users in a specified role where the user name contains the specified user name to match.</summary>
		/// <returns>A string array containing the names of all the users whose user name matches <paramref name="usernameToMatch" /> and who are members of the specified role.</returns>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="roleName" /> is null (Nothing in Visual Basic).-or-<paramref name="usernameToMatch" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="roleName" /> is an empty string or contains a comma (,).-or-<paramref name="usernameToMatch" /> is an empty string.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x06003795 RID: 14229 RVA: 0x000912A9 File Offset: 0x0008F4A9
		public static string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			return Roles.Provider.FindUsersInRole(roleName, usernameToMatch);
		}

		/// <summary>Gets or sets the name of the application to store and retrieve role information for.</summary>
		/// <returns>The name of the application to store and retrieve role information for.</returns>
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000912B7 File Offset: 0x0008F4B7
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x000912C3 File Offset: 0x0008F4C3
		public static string ApplicationName
		{
			get
			{
				return Roles.Provider.ApplicationName;
			}
			set
			{
				Roles.Provider.ApplicationName = value;
			}
		}

		/// <summary>Gets a value indicating whether the current user's roles are cached in a cookie.</summary>
		/// <returns>true if the current user's roles are cached in a cookie; otherwise, false. The default is true.</returns>
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000912D0 File Offset: 0x0008F4D0
		public static bool CacheRolesInCookie
		{
			get
			{
				return Roles.config.CacheRolesInCookie;
			}
		}

		/// <summary>Gets the name of the cookie where role names are cached.</summary>
		/// <returns>The name of the cookie where role names are cached. The default is .ASPXROLES.</returns>
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000912DC File Offset: 0x0008F4DC
		public static string CookieName
		{
			get
			{
				return Roles.config.CookieName;
			}
		}

		/// <summary>Gets the path for the cached role names cookie.</summary>
		/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000912E8 File Offset: 0x0008F4E8
		public static string CookiePath
		{
			get
			{
				return Roles.config.CookiePath;
			}
		}

		/// <summary>Gets a value that indicates how role names cached in a cookie are protected.</summary>
		/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> enumeration values indicating how role names that are cached in a cookie are protected. The default is All.</returns>
		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000912F4 File Offset: 0x0008F4F4
		public static CookieProtection CookieProtectionValue
		{
			get
			{
				return Roles.config.CookieProtection;
			}
		}

		/// <summary>Gets a value indicating whether the role names cookie requires SSL in order to be returned to the server.</summary>
		/// <returns>true if SSL is required to return the role names cookie to the server; otherwise, false. The default is false.</returns>
		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600379C RID: 14236 RVA: 0x00091300 File Offset: 0x0008F500
		public static bool CookieRequireSSL
		{
			get
			{
				return Roles.config.CookieRequireSSL;
			}
		}

		/// <summary>Indicates whether the role names cookie expiration date and time will be reset periodically.</summary>
		/// <returns>true if the role names cookie expiration date and time will be reset periodically; otherwise, false. The default is true.</returns>
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x0009130C File Offset: 0x0008F50C
		public static bool CookieSlidingExpiration
		{
			get
			{
				return Roles.config.CookieSlidingExpiration;
			}
		}

		/// <summary>Gets the number of minutes before the roles cookie expires.</summary>
		/// <returns>An integer specifying the number of minutes before the roles cookie expires. The default is 30 minutes.</returns>
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x00091318 File Offset: 0x0008F518
		public static int CookieTimeout
		{
			get
			{
				return (int)Roles.config.CookieTimeout.TotalMinutes;
			}
		}

		/// <summary>Gets a value indicating whether the role-names cookie is session-based or persistent.</summary>
		/// <returns>true if the role-names cookie is a persistent cookie; otherwise false. The default is false.</returns>
		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x00091338 File Offset: 0x0008F538
		public static bool CreatePersistentCookie
		{
			get
			{
				return Roles.config.CreatePersistentCookie;
			}
		}

		/// <summary>Gets the value of the domain of the role-names cookie.</summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the role names cookie.</returns>
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x00091344 File Offset: 0x0008F544
		public static string Domain
		{
			get
			{
				return Roles.config.Domain;
			}
		}

		/// <summary>Gets or sets a value indicating whether role management is enabled for the current Web application.</summary>
		/// <returns>true if role management is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x00091350 File Offset: 0x0008F550
		// (set) Token: 0x060037A2 RID: 14242 RVA: 0x0009135C File Offset: 0x0008F55C
		public static bool Enabled
		{
			get
			{
				return Roles.config.Enabled;
			}
			set
			{
				Roles.config.Enabled = value;
			}
		}

		/// <summary>Gets the maximum number of role names to be cached for a user.</summary>
		/// <returns>The maximum number of role names to be cached for a user. The default is 25.</returns>
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x00091369 File Offset: 0x0008F569
		public static int MaxCachedResults
		{
			get
			{
				return Roles.config.MaxCachedResults;
			}
		}

		/// <summary>Gets the default role provider for the application.</summary>
		/// <returns>The default role provider for the application, which is exposed as a class that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x00091375 File Offset: 0x0008F575
		public static RoleProvider Provider
		{
			get
			{
				RoleProvider roleProvider = Roles.Providers[Roles.config.DefaultProvider];
				if (roleProvider == null)
				{
					throw new ConfigurationErrorsException("Default Role Provider could not be found: Cannot instantiate provider: '" + Roles.config.DefaultProvider + "'.");
				}
				return roleProvider;
			}
		}

		/// <summary>Gets a collection of the role providers for the ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.Web.Security.RoleProviderCollection" /> that contains the role providers configured for the ASP.NET application.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000913B0 File Offset: 0x0008F5B0
		public static RoleProviderCollection Providers
		{
			get
			{
				Roles.CheckEnabled();
				if (Roles.providersCollection == null)
				{
					RoleProviderCollection roleProviderCollection = new RoleProviderCollection();
					ProvidersHelper.InstantiateProviders(Roles.config.Providers, roleProviderCollection, typeof(RoleProvider));
					Roles.providersCollection = roleProviderCollection;
				}
				return Roles.providersCollection;
			}
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000913F4 File Offset: 0x0008F5F4
		private static void CheckEnabled()
		{
			if (!Roles.Enabled)
			{
				throw new ProviderException("This feature is not enabled.  To enable it, add <roleManager enabled=\"true\"> to your configuration file.");
			}
		}

		// Token: 0x04001DFA RID: 7674
		private static RoleManagerSection config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");

		// Token: 0x04001DFB RID: 7675
		private static RoleProviderCollection providersCollection;
	}
}
