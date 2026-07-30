using System;
using Unity;

namespace System.Web.Security
{
	/// <summary>Exposes and updates membership user information stored in an Active Directory data store.</summary>
	// Token: 0x020006E9 RID: 1769
	[Serializable]
	public class ActiveDirectoryMembershipUser : MembershipUser
	{
		/// <summary>Initializes a new instance of an <see cref="T:System.Web.Security.ActiveDirectoryMembershipUser" /> object for a class that inherits the <see cref="T:System.Web.Security.ActiveDirectoryMembershipUser" /> class.</summary>
		// Token: 0x06004ADA RID: 19162 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ActiveDirectoryMembershipUser()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.ActiveDirectoryMembershipUser" /> class with the specified property values.</summary>
		/// <param name="providerName">The <see cref="P:System.Web.Security.MembershipUser.ProviderName" /> for the membership user.</param>
		/// <param name="name">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> for the membership user.</param>
		/// <param name="providerUserKey">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.ProviderUserKey" /> for the membership user.</param>
		/// <param name="email">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.Email" /> address of the membership user.</param>
		/// <param name="passwordQuestion">The <see cref="P:System.Web.Security.MembershipUser.PasswordQuestion" /> for the membership user.</param>
		/// <param name="comment">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.Comment" /> for the membership user.</param>
		/// <param name="isApproved">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.IsApproved" /> value for the membership user.</param>
		/// <param name="isLockedOut">The <see cref="P:System.Web.Security.MembershipUser.IsLockedOut" /> value for the membership user.</param>
		/// <param name="creationDate">The <see cref="P:System.Web.Security.MembershipUser.CreationDate" /> for the membership user.</param>
		/// <param name="lastLoginDate">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.LastLoginDate" /> for the membership user. This parameter is not used.</param>
		/// <param name="lastActivityDate">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipUser.LastActivityDate" /> for the membership user. This parameter is not used.</param>
		/// <param name="lastPasswordChangedDate">The <see cref="P:System.Web.Security.MembershipUser.LastPasswordChangedDate" /> for the membership user.</param>
		/// <param name="lastLockoutDate">The <see cref="P:System.Web.Security.MembershipUser.LastLockoutDate" /> for the membership user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerUserKey" /> is not a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.- or -<paramref name="providerName" /> is null and a provider is not set in the application's configuration file.</exception>
		// Token: 0x06004ADB RID: 19163 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ActiveDirectoryMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
