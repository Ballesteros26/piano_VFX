using System;
using System.Security.Permissions;

namespace System.DirectoryServices
{
	/// <summary>The <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" /> class provides a direct way to specify and obtain provider-specific options for manipulating a directory object. Typically, the options apply to search operations of the underlying directory store. The supported options are provider-specific.</summary>
	// Token: 0x0200000B RID: 11
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class DirectoryEntryConfiguration
	{
		/// <summary>Gets or sets a value that determines if and how referral chasing is pursued.</summary>
		/// <returns>A combination of one or more of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> enumeration members that specifies if and how referral chasing is pursued.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000208C File Offset: 0x0000028C
		public ReferralChasingOption Referral
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a security mask to use with <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" />.</summary>
		/// <returns>A combination of one or more of the <see cref="T:System.DirectoryServices.SecurityMasks" /> enumeration members that specifies the security mask.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000208C File Offset: 0x0000028C
		public SecurityMasks SecurityMasks
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the page size in a paged search.</summary>
		/// <returns>The number of entries in a page.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000208C File Offset: 0x0000028C
		public int PageSize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the port number to use to establish an SSL connection when the password is set or changed.</summary>
		/// <returns>The port number to use to establish an SSL connection when the password is set or changed.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000208C File Offset: 0x0000028C
		public int PasswordPort
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the password encoding method.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.PasswordEncodingMethod" /> enumeration members that indicates the type of password encoding.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000208C File Offset: 0x0000028C
		public PasswordEncodingMethod PasswordEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the host name of the server for the current binding to this directory object.</summary>
		/// <returns>The name of the server.</returns>
		// Token: 0x0600002D RID: 45 RVA: 0x0000208C File Offset: 0x0000028C
		public string GetCurrentServerName()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines if mutual authentication is performed by the SSPI layer.</summary>
		/// <returns>true if mutual authentication has been performed; otherwise, false. </returns>
		// Token: 0x0600002E RID: 46 RVA: 0x0000208C File Offset: 0x0000028C
		public bool IsMutuallyAuthenticated()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the name of a security principal so that when the principal is accessed, its quota information will also be returned.</summary>
		/// <param name="accountName">The account name that is being set to allow queries on its principal name.</param>
		// Token: 0x0600002F RID: 47 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetUserNameQueryQuota(string accountName)
		{
			throw new NotImplementedException();
		}
	}
}
