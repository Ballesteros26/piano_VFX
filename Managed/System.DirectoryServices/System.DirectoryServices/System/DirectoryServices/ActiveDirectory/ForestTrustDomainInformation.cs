using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> class contains information about a <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object and is contained in a <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInfoCollection" /> object.</summary>
	// Token: 0x0200005D RID: 93
	public class ForestTrustDomainInformation
	{
		/// <summary>Gets the DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
		/// <returns>A string that contains the DNS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000208C File Offset: 0x0000028C
		public string DnsName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the NetBIOS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
		/// <returns>A string that contains the NetBIOS name of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000208C File Offset: 0x0000028C
		public string NetBiosName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the SID of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
		/// <returns>A string that contains the SID of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000208C File Offset: 0x0000028C
		public string DomainSid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the status of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainStatus" /> values that represents the status of the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="Status" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainStatus" /> enumeration value.</exception>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000208C File Offset: 0x0000028C
		public ForestTrustDomainStatus Status
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
	}
}
