using System;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> class represents an Active Directory domain.</summary>
	// Token: 0x02000052 RID: 82
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class Domain : ActiveDirectoryPartition
	{
		/// <summary>Gets the forest that this domain is a member of.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that this domain is a member of.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000208C File Offset: 0x0000028C
		public Forest Forest
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the domain controllers in this domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> object that contains the domain controllers in this domain.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainControllerCollection DomainControllers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the domains that are children of this domain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> object that contains the child domains.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainCollection Children
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the mode that this domain is operating in.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> values that indicates the mode that this domain is operating in.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainMode DomainMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the parent domain of this domain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the parent domain of this domain. null if this domain has no parent domain.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000208C File Offset: 0x0000028C
		public Domain Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that holds the primary domain controller (PDC) for this domain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the PDC emulator role for this domain.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController PdcRoleOwner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the RID master role holder for this domain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the relative identifier (RID) master role for this domain.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController RidRoleOwner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the infrastructure role owner for this domain.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that holds the infrastructure owner role.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController InfrastructureRoleOwner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object for the specified context.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain for the specified context.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The type of the context must be a domain or directory server.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> could not be made.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06000332 RID: 818 RVA: 0x0000208C File Offset: 0x0000028C
		public static Domain GetDomain(DirectoryContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain to which the local computer is joined.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain to which the local machine is joined.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the domain could not be made.</exception>
		// Token: 0x06000333 RID: 819 RVA: 0x0000208C File Offset: 0x0000028C
		public static Domain GetComputerDomain()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the mode of operation for the domain.</summary>
		/// <param name="domainMode">An <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> enumeration value that specifies the new operation level for the domain.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">Based on the current operating mode of the domain, the value specified for <paramref name="domainMode" /> is not valid.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="domainMode" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.DomainMode" /> enumeration value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000334 RID: 820 RVA: 0x0000208C File Offset: 0x0000028C
		public void RaiseDomainFunctionality(DomainMode domainMode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds any domain controller in this domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> that represents the domain controller that is found by this method.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000335 RID: 821 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController FindDomainController()
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a domain controller in this domain and in the specified site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is found by this method.</returns>
		/// <param name="siteName">The name of the site to search for the domain controller.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000336 RID: 822 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController FindDomainController(string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a domain controller in this domain that meets the specified criteria.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> that represents the domain controller that is found by this method.</returns>
		/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000337 RID: 823 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController FindDomainController(LocatorOptions flag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a domain controller in this domain and in the specified site that meets the specified criteria.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that is found by this method.</returns>
		/// <param name="siteName">The name of the site to search for the domain controller.</param>
		/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A domain controller cannot be located.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.<paramref name="siteName" /> is an empty string.<paramref name="flag" /> contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000338 RID: 824 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainController FindDomainController(string siteName, LocatorOptions flag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all of the domain controllers in this domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000339 RID: 825 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainControllerCollection FindAllDomainControllers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all of the domain controllers in this domain that are also in the specified site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
		/// <param name="siteName">The name of the site to search for the domain controllers.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600033A RID: 826 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainControllerCollection FindAllDomainControllers(string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all of the discoverable domain controllers in this domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600033B RID: 827 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainControllerCollection FindAllDiscoverableDomainControllers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all of the discoverable domain controllers in this domain that are also in the specified site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> that contains the domain controller objects that were found by this method.</returns>
		/// <param name="siteName">The name of the site to search for the domain controllers.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600033C RID: 828 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainControllerCollection FindAllDiscoverableDomainControllers(string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the default naming context of the domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the domain.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600033D RID: 829 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves all of the trust relationships for this domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformationCollection" /> object that contains all of the trust relationships for this domain.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600033E RID: 830 RVA: 0x0000208C File Offset: 0x0000028C
		public TrustRelationshipInformationCollection GetAllTrustRelationships()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the trust relationship between this domain and the specified domain.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object that represents the trust relationship between this domain and the specified domain.</returns>
		/// <param name="targetDomainName">The DNS name of the domain with which the trust relationship exists.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600033F RID: 831 RVA: 0x0000208C File Offset: 0x0000028C
		public TrustRelationshipInformation GetTrustRelationship(string targetDomainName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines the authentication type of an inbound trust.</summary>
		/// <returns>true if the authentication of the trust is selective; false if the authentication is domain-wide.</returns>
		/// <param name="targetDomainName">The DNS name of the domain which with the trust exists.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The call to LsaQueryTrustedDomainInfoByName failed. For more information, see the topic LsaQueryTrustedDomainInfoByName in the MSDN Library at http://msdn.microsoft.com/library.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000340 RID: 832 RVA: 0x0000208C File Offset: 0x0000028C
		public bool GetSelectiveAuthenticationStatus(string targetDomainName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Enables or disables selective authentication for an inbound trust.</summary>
		/// <param name="targetDomainName">The DNS name of the domain with which the inbound trust exists.</param>
		/// <param name="enable">true if selective authentication is enabled; otherwise, false.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain controller that is specified by <paramref name="targetDomainName" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000341 RID: 833 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetSelectiveAuthenticationStatus(string targetDomainName, bool enable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines the SID filtering status of a trust.</summary>
		/// <returns>true if SID filtering is enabled; otherwise, false.</returns>
		/// <param name="targetDomainName">The DNS name of the domain which with the trust relationship exists.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> that is specified by the <paramref name="targetForestName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000342 RID: 834 RVA: 0x0000208C File Offset: 0x0000028C
		public bool GetSidFilteringStatus(string targetDomainName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the SID filtering state for the specified domain.</summary>
		/// <param name="targetDomainName">The DNS name of the domain with which the trust exists.</param>
		/// <param name="enable">true if SID filtering must be enabled; otherwise, false.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x06000343 RID: 835 RVA: 0x0000208C File Offset: 0x0000028C
		public void SetSidFilteringStatus(string targetDomainName, bool enable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes the local side of a trust relationship.</summary>
		/// <param name="targetDomainName">The DNS name of the domain that the trust exists with.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> that is specified by the <paramref name="targetDomainName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x0000208C File Offset: 0x0000028C
		public void DeleteLocalSideOfTrustRelationship(string targetDomainName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes both sides of a trust relationship.</summary>
		/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain that the trust exists with.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the specified <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomain" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000345 RID: 837 RVA: 0x0000208C File Offset: 0x0000028C
		public void DeleteTrustRelationship(Domain targetDomain)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies that a previously established outbound trust with the specified domain is valid.</summary>
		/// <param name="targetDomainName">The DNS name of the domain with which the trust exists.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no outbound trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000346 RID: 838 RVA: 0x0000208C File Offset: 0x0000028C
		public void VerifyOutboundTrustRelationship(string targetDomainName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies that a previously established trust with the specified domain is valid.</summary>
		/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
		/// <param name="direction">A <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the direction, relative to this domain, of the trust.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomain" /> parameter, or the target domain does not have the trust direction that is specified by the <paramref name="direction" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomain" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x0000208C File Offset: 0x0000028C
		public void VerifyTrustRelationship(Domain targetDomain, TrustDirection direction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates the local side of a trust relationship with the specified domain.</summary>
		/// <param name="targetDomainName">The DNS name of the domain that the trust is created with.</param>
		/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this domain.</param>
		/// <param name="trustPassword">The password for the trust. See remarks below.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> or <paramref name="trustPassword" /> is empty.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> or <paramref name="trustPassword" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000348 RID: 840 RVA: 0x0000208C File Offset: 0x0000028C
		public void CreateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection direction, string trustPassword)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates both sides of a trust relationship with the specified domain.</summary>
		/// <param name="targetDomain">A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain that the trust is being created with.</param>
		/// <param name="direction">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> members that determines the direction of the trust, relative to this domain.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The trust relationship already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomain" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000349 RID: 841 RVA: 0x0000208C File Offset: 0x0000028C
		public void CreateTrustRelationship(Domain targetDomain, TrustDirection direction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the password for the local side of a trust relationship.</summary>
		/// <param name="targetDomainName">The DNS name of the domain with which a trust exists.</param>
		/// <param name="newTrustPassword">The new password for the trust.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by <paramref name="targetDomainName" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600034A RID: 842 RVA: 0x0000208C File Offset: 0x0000028C
		public void UpdateLocalSideOfTrustRelationship(string targetDomainName, string newTrustPassword)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the password and trust direction for the local side of a trust relationship.</summary>
		/// <param name="targetDomainName">The DNS name of the domain with which a trust exists.</param>
		/// <param name="newTrustDirection">An <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value for the new trust direction for the trust relationship.</param>
		/// <param name="newTrustPassword">The new password for the trust.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomainName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomainName" /> or <paramref name="newTrustPassword" /> are null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="newTrustDirection" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600034B RID: 843 RVA: 0x0000208C File Offset: 0x0000028C
		public void UpdateLocalSideOfTrustRelationship(string targetDomainName, TrustDirection newTrustDirection, string newTrustPassword)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the trust direction for an existing trust relationship. The trust directions are updated on both sides of the trust.</summary>
		/// <param name="targetDomain">An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
		/// <param name="newTrustDirection">An <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value that specifies the new trust direction for the trust relationship.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the domain that is specified by the <paramref name="targetDomain" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomain" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="newTrustDirection" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.TrustDirection" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600034C RID: 844 RVA: 0x0000208C File Offset: 0x0000028C
		public void UpdateTrustRelationship(Domain targetDomain, TrustDirection newTrustDirection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Repairs a trust relationship.</summary>
		/// <param name="targetDomain">An <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain with which the trust exists.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">There is no trust relationship with the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> that is specified by <paramref name="targetDomain" />.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target or source server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetDomain" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600034D RID: 845 RVA: 0x0000208C File Offset: 0x0000028C
		public void RepairTrustRelationship(Domain targetDomain)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object for the current user credentials in effect for the security context under which the application is running.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain for the specified user credentials in effect for the security context under which the application is running.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the current context could not be made.</exception>
		// Token: 0x0600034E RID: 846 RVA: 0x0000208C File Offset: 0x0000028C
		public static Domain GetCurrentDomain()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00004B58 File Offset: 0x00002D58
		public int DomainModeLevel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00002644 File Offset: 0x00000844
		public void RaiseDomainFunctionalityLevel(int domainMode)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
