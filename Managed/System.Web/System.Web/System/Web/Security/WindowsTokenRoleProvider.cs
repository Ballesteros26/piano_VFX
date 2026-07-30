using System;
using System.Security.Principal;
using Unity;

namespace System.Web.Security
{
	/// <summary>Gets role information for an ASP.NET application from Windows group membership.</summary>
	// Token: 0x020006EC RID: 1772
	public class WindowsTokenRoleProvider : RoleProvider
	{
		/// <summary>Creates an instance of the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class.</summary>
		// Token: 0x06004AED RID: 19181 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WindowsTokenRoleProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.Security.WindowsTokenRoleProvider.ApplicationName" /> property by a caller that does not have <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> ASP.NET hosting permission.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.WindowsTokenRoleProvider.ApplicationName" /> to a string that is longer than 256 characters.</exception>
		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string ApplicationName
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

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <param name="usernames">A string array of user names to be added to the specified roles. </param>
		/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF0 RID: 19184 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <param name="roleName">The name of the role to create.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF1 RID: 19185 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void CreateRole(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <returns>None. The method is not supported by the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class and always throws a <see cref="T:System.Configuration.Provider.ProviderException" />.</returns>
		/// <param name="roleName">The name of the role to delete.</param>
		/// <param name="throwOnPopulatedRole">If true, an exception will be thrown on an attempt to delete a role that contains one or more members.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF2 RID: 19186 RVA: 0x000CA984 File Offset: 0x000C8B84
		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <returns>None. The method is not supported by the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class and always throws a <see cref="T:System.Configuration.Provider.ProviderException" />.</returns>
		/// <param name="roleName">The role to search in.</param>
		/// <param name="usernameToMatch">The user name to find in the role.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF3 RID: 19187 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <returns>None. The method is not supported by the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class and always throws a <see cref="T:System.Configuration.Provider.ProviderException" />.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF4 RID: 19188 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string[] GetAllRoles()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a list of the Windows groups that a user is in.</summary>
		/// <returns>A string array containing the names of all the Windows groups that the specified user is in.</returns>
		/// <param name="username">The user to return the list of Windows groups for in the form DOMAIN\username. </param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Web.UI.Page.User" />. For non-HTTP scenarios, the currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Threading.Thread.CurrentPrincipal" />.-or-<paramref name="username" /> does not match the <see cref="P:System.Security.Principal.WindowsIdentity.Name" /> of the current <see cref="T:System.Security.Principal.WindowsIdentity" />.-or-A failure occurred while retrieving the user's Windows group information.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The trust level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Low" />.</exception>
		// Token: 0x06004AF5 RID: 19189 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string[] GetRolesForUser(string username)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <returns>None. The method is not supported by the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class and always throws a <see cref="T:System.Configuration.Provider.ProviderException" />.</returns>
		/// <param name="roleName">The name of the role to get the list of users for.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF6 RID: 19190 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string[] GetUsersInRole(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified built-in Windows role.</summary>
		/// <returns>true if the specified user is in the specified Windows role; otherwise, false.</returns>
		/// <param name="username">The user name to search for in the form DOMAIN\username.</param>
		/// <param name="role">The Windows role to search in. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Web.UI.Page.User" />. For non-HTTP scenarios, the currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Threading.Thread.CurrentPrincipal" />.-or-<paramref name="username" /> does not match the <see cref="P:System.Security.Principal.WindowsIdentity.Name" /> of the current <see cref="T:System.Security.Principal.WindowsIdentity" />.</exception>
		// Token: 0x06004AF7 RID: 19191 RVA: 0x000CA9A0 File Offset: 0x000C8BA0
		public bool IsUserInRole(string username, WindowsBuiltInRole role)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets a value indicating whether the specified user is in the specified Windows group.</summary>
		/// <returns>true if the specified user name is in the specified Windows group; otherwise, false.</returns>
		/// <param name="username">The user name to search for in the form DOMAIN\username. </param>
		/// <param name="roleName">The Windows group to search in the form DOMAIN\rolename. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="username" /> is null.-or-<paramref name="roleName" /> is null.</exception>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Web.UI.Page.User" />. For non-HTTP scenarios, the currently executing user does not have an authenticated <see cref="T:System.Security.Principal.WindowsIdentity" /> attached to <see cref="P:System.Threading.Thread.CurrentPrincipal" />.-or-<paramref name="username" /> does not match the <see cref="P:System.Security.Principal.WindowsIdentity.Name" /> of the current <see cref="T:System.Security.Principal.WindowsIdentity" />.-or-A failure occurred while retrieving the user's Windows group information.</exception>
		// Token: 0x06004AF8 RID: 19192 RVA: 0x000CA9BC File Offset: 0x000C8BBC
		public override bool IsUserInRole(string username, string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <param name="usernames">A string array of user names to be added to the specified roles. </param>
		/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AF9 RID: 19193 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is not supported by the Windows token role provider.</summary>
		/// <returns>None. The method is not supported by the <see cref="T:System.Web.Security.WindowsTokenRoleProvider" /> class and always throws a <see cref="T:System.Configuration.Provider.ProviderException" />.</returns>
		/// <param name="roleName">The name of the role to search for in the data source.</param>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">An unsupported method was called.</exception>
		// Token: 0x06004AFA RID: 19194 RVA: 0x000CA9D8 File Offset: 0x000C8BD8
		public override bool RoleExists(string roleName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
