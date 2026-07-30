using System;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
	/// <summary>Exposes and updates membership user information in the membership data store.</summary>
	// Token: 0x02000017 RID: 23
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class MembershipUser
	{
		/// <summary>Creates a new instance of a <see cref="T:System.Web.Security.MembershipUser" /> object for a class that inherits the <see cref="T:System.Web.Security.MembershipUser" /> class.</summary>
		// Token: 0x0600007D RID: 125 RVA: 0x00002A39 File Offset: 0x00000C39
		protected MembershipUser()
		{
		}

		/// <summary>Creates a new membership user object with the specified property values.</summary>
		/// <param name="providerName">The <see cref="P:System.Web.Security.MembershipUser.ProviderName" /> string for the membership user.</param>
		/// <param name="name">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> string for the membership user.</param>
		/// <param name="providerUserKey">The <see cref="P:System.Web.Security.MembershipUser.ProviderUserKey" /> identifier for the membership user.</param>
		/// <param name="email">The <see cref="P:System.Web.Security.MembershipUser.Email" /> string for the membership user.</param>
		/// <param name="passwordQuestion">The <see cref="P:System.Web.Security.MembershipUser.PasswordQuestion" /> string for the membership user.</param>
		/// <param name="comment">The <see cref="P:System.Web.Security.MembershipUser.Comment" /> string for the membership user.</param>
		/// <param name="isApproved">The <see cref="P:System.Web.Security.MembershipUser.IsApproved" /> value for the membership user.</param>
		/// <param name="isLockedOut">true to lock out the membership user; otherwise, false.</param>
		/// <param name="creationDate">The <see cref="P:System.Web.Security.MembershipUser.CreationDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
		/// <param name="lastLoginDate">The <see cref="P:System.Web.Security.MembershipUser.LastLoginDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
		/// <param name="lastActivityDate">The <see cref="P:System.Web.Security.MembershipUser.LastActivityDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
		/// <param name="lastPasswordChangedDate">The <see cref="P:System.Web.Security.MembershipUser.LastPasswordChangedDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
		/// <param name="lastLockoutDate">The <see cref="P:System.Web.Security.MembershipUser.LastLockoutDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="providerName" /> is null.-or-<paramref name="providerName" /> is not found in the <see cref="P:System.Web.Security.Membership.Providers" /> collection.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The constructor is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, derive your class from the type and then call the default protected constructor, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x0600007E RID: 126 RVA: 0x00002A44 File Offset: 0x00000C44
		public MembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
		{
			this.providerName = providerName;
			this.name = name;
			this.providerUserKey = providerUserKey;
			this.email = email;
			this.passwordQuestion = passwordQuestion;
			this.comment = comment;
			this.isApproved = isApproved;
			this.isLockedOut = isLockedOut;
			this.creationDate = creationDate.ToUniversalTime();
			this.lastLoginDate = lastLoginDate.ToUniversalTime();
			this.lastActivityDate = lastActivityDate.ToUniversalTime();
			this.lastPasswordChangedDate = lastPasswordChangedDate.ToUniversalTime();
			this.lastLockoutDate = lastLockoutDate.ToUniversalTime();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private void UpdateSelf(MembershipUser fromUser)
		{
			try
			{
				this.Comment = fromUser.Comment;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.creationDate = fromUser.CreationDate;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.Email = fromUser.Email;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.IsApproved = fromUser.IsApproved;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.isLockedOut = fromUser.IsLockedOut;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.LastActivityDate = fromUser.LastActivityDate;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.lastLockoutDate = fromUser.LastLockoutDate;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.LastLoginDate = fromUser.LastLoginDate;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.lastPasswordChangedDate = fromUser.LastPasswordChangedDate;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.passwordQuestion = fromUser.PasswordQuestion;
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				this.providerUserKey = fromUser.ProviderUserKey;
			}
			catch (NotSupportedException)
			{
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002C34 File Offset: 0x00000E34
		internal void UpdateUser()
		{
			MembershipUser user = this.Provider.GetUser(this.UserName, false);
			this.UpdateSelf(user);
		}

		/// <summary>Updates the password for the membership user in the membership data store.</summary>
		/// <returns>true if the update was successful; otherwise, false.</returns>
		/// <param name="oldPassword">The current password for the membership user.</param>
		/// <param name="newPassword">The new password for the membership user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oldPassword" /> is an empty string.-or-<paramref name="newPassword" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oldPassword" /> is null.-or-<paramref name="newPassword" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000081 RID: 129 RVA: 0x00002C5B File Offset: 0x00000E5B
		public virtual bool ChangePassword(string oldPassword, string newPassword)
		{
			bool flag = this.Provider.ChangePassword(this.UserName, oldPassword, newPassword);
			this.UpdateUser();
			return flag;
		}

		/// <summary>Updates the password question and answer for the membership user in the membership data store.</summary>
		/// <returns>true if the update was successful; otherwise, false.</returns>
		/// <param name="password">The current password for the membership user.</param>
		/// <param name="newPasswordQuestion">The new password question value for the membership user.</param>
		/// <param name="newPasswordAnswer">The new password answer value for the membership user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="password" /> is an empty string.-or-<paramref name="newPasswordQuestion" /> is an empty string.-or-<paramref name="newPasswordAnswer" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="password" /> is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000082 RID: 130 RVA: 0x00002C76 File Offset: 0x00000E76
		public virtual bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			bool flag = this.Provider.ChangePasswordQuestionAndAnswer(this.UserName, password, newPasswordQuestion, newPasswordAnswer);
			this.UpdateUser();
			return flag;
		}

		/// <summary>Gets the password for the membership user from the membership data store.</summary>
		/// <returns>The password for the membership user.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000083 RID: 131 RVA: 0x00002C92 File Offset: 0x00000E92
		public virtual string GetPassword()
		{
			return this.GetPassword(null);
		}

		/// <summary>Gets the password for the membership user from the membership data store.</summary>
		/// <returns>The password for the membership user.</returns>
		/// <param name="passwordAnswer">The password answer for the membership user.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000084 RID: 132 RVA: 0x00002C9B File Offset: 0x00000E9B
		public virtual string GetPassword(string passwordAnswer)
		{
			return this.Provider.GetPassword(this.UserName, passwordAnswer);
		}

		/// <summary>Resets a user's password to a new, automatically generated password.</summary>
		/// <returns>The new password for the membership user.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000085 RID: 133 RVA: 0x00002CAF File Offset: 0x00000EAF
		public virtual string ResetPassword()
		{
			return this.ResetPassword(null);
		}

		/// <summary>Resets a user's password to a new, automatically generated password.</summary>
		/// <returns>The new password for the membership user.</returns>
		/// <param name="passwordAnswer">The password answer for the membership user.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x06000086 RID: 134 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public virtual string ResetPassword(string passwordAnswer)
		{
			string text = this.Provider.ResetPassword(this.UserName, passwordAnswer);
			this.UpdateUser();
			return text;
		}

		/// <summary>Gets or sets application-specific information for the membership user.</summary>
		/// <returns>Application-specific information for the membership user.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002CD2 File Offset: 0x00000ED2
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002CDA File Offset: 0x00000EDA
		public virtual string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		/// <summary>Gets the date and time when the user was added to the membership data store.</summary>
		/// <returns>The date and time when the user was added to the membership data store. </returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public virtual DateTime CreationDate
		{
			get
			{
				return this.creationDate.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the e-mail address for the membership user.</summary>
		/// <returns>The e-mail address for the membership user.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public virtual string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		/// <summary>Gets or sets whether the membership user can be authenticated.</summary>
		/// <returns>true if the user can be authenticated; otherwise, false.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002D01 File Offset: 0x00000F01
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool IsApproved
		{
			get
			{
				return this.isApproved;
			}
			set
			{
				this.isApproved = value;
			}
		}

		/// <summary>Gets a value indicating whether the membership user is locked out and unable to be validated.</summary>
		/// <returns>true if the membership user is locked out and unable to be validated; otherwise, false.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002D12 File Offset: 0x00000F12
		public virtual bool IsLockedOut
		{
			get
			{
				return this.isLockedOut;
			}
		}

		/// <summary>Gets whether the user is currently online.</summary>
		/// <returns>true if the user is online; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002D1C File Offset: 0x00000F1C
		public virtual bool IsOnline
		{
			get
			{
				IMembershipHelper helper = MembershipProvider.Helper;
				if (helper == null)
				{
					throw new PlatformNotSupportedException("The method is not available.");
				}
				int userIsOnlineTimeWindow = helper.UserIsOnlineTimeWindow;
				return this.LastActivityDate > DateTime.Now - TimeSpan.FromMinutes((double)userIsOnlineTimeWindow);
			}
		}

		/// <summary>Gets or sets the date and time when the membership user was last authenticated or accessed the application.</summary>
		/// <returns>The date and time when the membership user was last authenticated or accessed the application.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002D5E File Offset: 0x00000F5E
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002D6B File Offset: 0x00000F6B
		public virtual DateTime LastActivityDate
		{
			get
			{
				return this.lastActivityDate.ToLocalTime();
			}
			set
			{
				this.lastActivityDate = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the date and time when the user was last authenticated.</summary>
		/// <returns>The date and time when the user was last authenticated.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002D7A File Offset: 0x00000F7A
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002D87 File Offset: 0x00000F87
		public virtual DateTime LastLoginDate
		{
			get
			{
				return this.lastLoginDate.ToLocalTime();
			}
			set
			{
				this.lastLoginDate = value.ToUniversalTime();
			}
		}

		/// <summary>Gets the date and time when the membership user's password was last updated.</summary>
		/// <returns>The date and time when the membership user's password was last updated.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002D96 File Offset: 0x00000F96
		public virtual DateTime LastPasswordChangedDate
		{
			get
			{
				return this.lastPasswordChangedDate.ToLocalTime();
			}
		}

		/// <summary>Gets the most recent date and time that the membership user was locked out.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the most recent date and time that the membership user was locked out.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public virtual DateTime LastLockoutDate
		{
			get
			{
				return this.lastLockoutDate.ToLocalTime();
			}
		}

		/// <summary>Gets the password question for the membership user.</summary>
		/// <returns>The password question for the membership user.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public virtual string PasswordQuestion
		{
			get
			{
				return this.passwordQuestion;
			}
		}

		/// <summary>Gets the name of the membership provider that stores and retrieves user information for the membership user.</summary>
		/// <returns>The name of the membership provider that stores and retrieves user information for the membership user.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public virtual string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		/// <summary>Gets the logon name of the membership user.</summary>
		/// <returns>The logon name of the membership user.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002DC0 File Offset: 0x00000FC0
		public virtual string UserName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the user identifier from the membership data source for the user.</summary>
		/// <returns>The user identifier from the membership data source for the user.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public virtual object ProviderUserKey
		{
			get
			{
				return this.providerUserKey;
			}
		}

		/// <summary>Returns the user name for the membership user.</summary>
		/// <returns>The <see cref="P:System.Web.Security.MembershipUser.UserName" /> for the membership user.</returns>
		// Token: 0x0600009A RID: 154 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public override string ToString()
		{
			return this.UserName;
		}

		/// <summary>Clears the locked-out state of the user so that the membership user can be validated.</summary>
		/// <returns>true if the membership user was successfully unlocked; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
		// Token: 0x0600009B RID: 155 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public virtual bool UnlockUser()
		{
			bool flag = this.Provider.UnlockUser(this.UserName);
			this.UpdateUser();
			return flag;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002DF4 File Offset: 0x00000FF4
		private MembershipProvider Provider
		{
			get
			{
				IMembershipHelper helper = MembershipProvider.Helper;
				if (helper == null)
				{
					throw new PlatformNotSupportedException("The method is not available.");
				}
				MembershipProvider membershipProvider = helper.Providers[this.ProviderName];
				if (membershipProvider == null)
				{
					throw new InvalidOperationException("Membership provider '" + this.ProviderName + "' not found.");
				}
				return membershipProvider;
			}
		}

		// Token: 0x04000062 RID: 98
		private string providerName;

		// Token: 0x04000063 RID: 99
		private string name;

		// Token: 0x04000064 RID: 100
		private object providerUserKey;

		// Token: 0x04000065 RID: 101
		private string email;

		// Token: 0x04000066 RID: 102
		private string passwordQuestion;

		// Token: 0x04000067 RID: 103
		private string comment;

		// Token: 0x04000068 RID: 104
		private bool isApproved;

		// Token: 0x04000069 RID: 105
		private bool isLockedOut;

		// Token: 0x0400006A RID: 106
		private DateTime creationDate;

		// Token: 0x0400006B RID: 107
		private DateTime lastLoginDate;

		// Token: 0x0400006C RID: 108
		private DateTime lastActivityDate;

		// Token: 0x0400006D RID: 109
		private DateTime lastPasswordChangedDate;

		// Token: 0x0400006E RID: 110
		private DateTime lastLockoutDate;
	}
}
