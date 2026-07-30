using System;
using System.Runtime.CompilerServices;

namespace System.Web.Security
{
	/// <summary>Provides event data for the <see cref="E:System.Web.Security.MembershipProvider.ValidatingPassword" /> event of the <see cref="T:System.Web.Security.MembershipProvider" /> class.</summary>
	// Token: 0x02000014 RID: 20
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class ValidatePasswordEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.ValidatePasswordEventArgs" /> class.</summary>
		/// <param name="userName">The membership user name for the current create-user, change-password, or reset-password action.</param>
		/// <param name="password">The new password for the specified membership user.</param>
		/// <param name="isNewUser">true if the event is occurring while a new user is being created; otherwise, false.</param>
		// Token: 0x0600004C RID: 76 RVA: 0x000028BF File Offset: 0x00000ABF
		public ValidatePasswordEventArgs(string userName, string password, bool isNewUser)
		{
			this._userName = userName;
			this._password = password;
			this._isNewUser = isNewUser;
			this._cancel = false;
		}

		/// <summary>Gets the name of the membership user for the current create-user, change-password, or reset-password action.</summary>
		/// <returns>The name of the membership user for the current create-user, change-password, or reset-password action.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000028E3 File Offset: 0x00000AE3
		public string UserName
		{
			get
			{
				return this._userName;
			}
		}

		/// <summary>Gets the password for the current create-user, change-password, or reset-password action.</summary>
		/// <returns>The password for the current create-user, change-password, or reset-password action.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000028EB File Offset: 0x00000AEB
		public string Password
		{
			get
			{
				return this._password;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="E:System.Web.Security.MembershipProvider.ValidatingPassword" /> event is being raised during a call to the <see cref="M:System.Web.Security.MembershipProvider.CreateUser(System.String,System.String,System.String,System.String,System.String,System.Boolean,System.Object,System.Web.Security.MembershipCreateStatus@)" /> method.</summary>
		/// <returns>true if the <see cref="E:System.Web.Security.MembershipProvider.ValidatingPassword" /> event is being raised during a call to the <see cref="M:System.Web.Security.MembershipProvider.CreateUser(System.String,System.String,System.String,System.String,System.String,System.Boolean,System.Object,System.Web.Security.MembershipCreateStatus@)" /> method; otherwise, false.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000028F3 File Offset: 0x00000AF3
		public bool IsNewUser
		{
			get
			{
				return this._isNewUser;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the current create-user, change-password, or reset-password action will be canceled.</summary>
		/// <returns>true if the current create-user, change-password, or reset-password action will be canceled; otherwise, false. The default is false.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028FB File Offset: 0x00000AFB
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002903 File Offset: 0x00000B03
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		/// <summary>Gets or sets an exception that describes the reason for the password-validation failure.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that describes the reason for the password-validation failure.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000290C File Offset: 0x00000B0C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002914 File Offset: 0x00000B14
		public Exception FailureInformation
		{
			get
			{
				return this._failureInformation;
			}
			set
			{
				this._failureInformation = value;
			}
		}

		// Token: 0x04000059 RID: 89
		private string _userName;

		// Token: 0x0400005A RID: 90
		private string _password;

		// Token: 0x0400005B RID: 91
		private bool _isNewUser;

		// Token: 0x0400005C RID: 92
		private bool _cancel;

		// Token: 0x0400005D RID: 93
		private Exception _failureInformation;
	}
}
