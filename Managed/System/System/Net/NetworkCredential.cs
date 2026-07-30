using System;
using System.Security;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x0200045D RID: 1117
	public class NetworkCredential : ICredentials, ICredentialsByHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x060020D6 RID: 8406 RVA: 0x0007D255 File Offset: 0x0007B455
		public NetworkCredential()
			: this(string.Empty, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		// Token: 0x060020D7 RID: 8407 RVA: 0x0007F8F9 File Offset: 0x0007DAF9
		public NetworkCredential(string userName, string password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x060020D8 RID: 8408 RVA: 0x0007F908 File Offset: 0x0007DB08
		public NetworkCredential(string userName, SecureString password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		/// <param name="domain">The domain associated with these credentials. </param>
		// Token: 0x060020D9 RID: 8409 RVA: 0x0007F917 File Offset: 0x0007DB17
		public NetworkCredential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x060020DA RID: 8410 RVA: 0x0007F934 File Offset: 0x0007DB34
		public NetworkCredential(string userName, SecureString password, string domain)
		{
			this.UserName = userName;
			this.SecurePassword = password;
			this.Domain = domain;
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0007F951 File Offset: 0x0007DB51
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x0007F959 File Offset: 0x0007DB59
		public string UserName
		{
			get
			{
				return this.InternalGetUserName();
			}
			set
			{
				if (value == null)
				{
					this.m_userName = string.Empty;
					return;
				}
				this.m_userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was initialized with the <paramref name="password" /> parameter set to null, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0007F971 File Offset: 0x0007DB71
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0007F979 File Offset: 0x0007DB79
		public string Password
		{
			get
			{
				return this.InternalGetPassword();
			}
			set
			{
				this.m_password = UnsafeNclNativeMethods.SecureStringHelper.CreateSecureString(value);
			}
		}

		/// <summary>Gets or sets the password as a <see cref="T:System.Security.SecureString" /> instance.</summary>
		/// <returns>The password for the user name associated with the credentials.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0007F987 File Offset: 0x0007DB87
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x0007F994 File Offset: 0x0007DB94
		public SecureString SecurePassword
		{
			get
			{
				return this.InternalGetSecurePassword().Copy();
			}
			set
			{
				if (value == null)
				{
					this.m_password = new SecureString();
					return;
				}
				this.m_password = value.Copy();
			}
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0007F9B1 File Offset: 0x0007DBB1
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0007F9B9 File Offset: 0x0007DBB9
		public string Domain
		{
			get
			{
				return this.InternalGetDomain();
			}
			set
			{
				if (value == null)
				{
					this.m_domain = string.Empty;
					return;
				}
				this.m_domain = value;
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0007F9D1 File Offset: 0x0007DBD1
		internal string InternalGetUserName()
		{
			return this.m_userName;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0007F9D9 File Offset: 0x0007DBD9
		internal string InternalGetPassword()
		{
			return UnsafeNclNativeMethods.SecureStringHelper.CreateString(this.m_password);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0007F9E6 File Offset: 0x0007DBE6
		internal SecureString InternalGetSecurePassword()
		{
			return this.m_password;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0007F9EE File Offset: 0x0007DBEE
		internal string InternalGetDomain()
		{
			return this.m_domain;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
		internal string InternalGetDomainUserName()
		{
			string text = this.InternalGetDomain();
			if (text.Length != 0)
			{
				text += "\\";
			}
			return text + this.InternalGetUserName();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		/// <param name="uri">The URI that the client provides authentication for. </param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x060020E8 RID: 8424 RVA: 0x00002068 File Offset: 0x00000268
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified host, port, and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or null if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		/// <param name="host">The host computer that authenticates the client.</param>
		/// <param name="port">The port on the <paramref name="host" /> that the client communicates with.</param>
		/// <param name="authenticationType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x060020E9 RID: 8425 RVA: 0x00002068 File Offset: 0x00000268
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return this;
		}

		// Token: 0x04001DEE RID: 7662
		private string m_domain;

		// Token: 0x04001DEF RID: 7663
		private string m_userName;

		// Token: 0x04001DF0 RID: 7664
		private SecureString m_password;
	}
}
