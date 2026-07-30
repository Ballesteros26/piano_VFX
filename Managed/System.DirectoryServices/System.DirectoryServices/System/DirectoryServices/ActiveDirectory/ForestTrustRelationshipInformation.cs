using System;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> class contains information about a trust relationship between two <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> objects.</summary>
	// Token: 0x02000061 RID: 97
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ForestTrustRelationshipInformation : TrustRelationshipInformation
	{
		/// <summary>Gets the top-level names in the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> object.</summary>
		/// <returns>A read-only <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollection" /> object that contains the top-level names in the current object.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000208C File Offset: 0x0000028C
		public TopLevelNameCollection TopLevelNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the excluded top-level names in the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> object.</summary>
		/// <returns>A read/write <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the excluded top-level names in the current object.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000208C File Offset: 0x0000028C
		public StringCollection ExcludedTopLevelNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the trusted domain information for this <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> object.</summary>
		/// <returns>A read-only <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInfoCollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustDomainInformation" /> object  for the current object.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000208C File Offset: 0x0000028C
		public ForestTrustDomainInfoCollection TrustedDomainInformation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Commits any changes to the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipInformation" /> properties to the Active Directory Domain Services store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionException">A collision occurred with an existing trust relationship. </exception>
		// Token: 0x060003CA RID: 970 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}
	}
}
