using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> class defines a set of domain controllers that are well-connected in terms of speed and cost. A site object consists of a set of one or more IP subnets.</summary>
	// Token: 0x02000040 RID: 64
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySite : IDisposable
	{
		/// <summary>Gets the name of the site.</summary>
		/// <returns>A string value that contains the name of the site.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets all domains in the site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollection" /> object containing all domains in the site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000208C File Offset: 0x0000028C
		public DomainCollection Domains
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a writable collection of subnets in the site.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnetCollection" /> object that contains a writable collection of subnets in the site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySubnetCollection Subnets
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a read-only collection of directory servers in the site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> that contains a read-only collection of directory servers in the site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyDirectoryServerCollection Servers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a read-only collection of sites that are connected through a common site link with this site object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> collection that contains a read-only collection of sites that are connected through a common site link with this site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlySiteCollection AdjacentSites
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a read-only collection of site links that involve this site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkCollection" /> object that contains a read-only collection of site links that this site is in.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlySiteLinkCollection SiteLinks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the directory server that serves as the inter-site topology generator.</summary>
		/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that represents the directory server that serves as the inter-site topology generator.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.NotSupportedException">The transport type is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryServer InterSiteTopologyGenerator
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

		/// <summary>Gets or sets the site options.</summary>
		/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteOptions" /> value that gets or sets the site options.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySiteOptions Options
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

		/// <summary>Gets or sets the location of the site.</summary>
		/// <returns>A string value that gets or sets the location of the site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000208C File Offset: 0x0000028C
		public string Location
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

		/// <summary>Gets a read-only collection of bridgehead servers for this site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> collection that contains a read-only collection of directory servers in this site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyDirectoryServerCollection BridgeheadServers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a collection of directory servers that are designated as preferred bridgehead servers for the SMTP transport.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains a collection of directory servers that are designated as preferred bridgehead servers for the SMTP transport.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryServerCollection PreferredSmtpBridgeheadServers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns a collection of directory servers that are designated as preferred bridgehead servers for the RPC transport.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServerCollection" /> object that contains the directory servers that are designated as preferred bridgehead servers for the RPC transport.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryServerCollection PreferredRpcBridgeheadServers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the default setting for the replication schedule for the site.</summary>
		/// <returns>A read/write <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> that represents the default setting for the replication schedule for the site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchedule IntraSiteReplicationSchedule
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

		/// <summary>Returns a site based on a site name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object for the requested site.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site.</param>
		/// <param name="siteName">The name of the site to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteName" /> in the <paramref name="context" /> specified.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.<paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x06000266 RID: 614 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySite FindByName(DirectoryContext context, string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site.</param>
		/// <param name="siteName">The name for the new site.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.<paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x06000267 RID: 615 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public ActiveDirectorySite(DirectoryContext context, string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the site that this computer is a member of.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that contains the caller's current site.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The caller's computer does not belong to a site.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		// Token: 0x06000268 RID: 616 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySite GetComputerSite()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes any changes to the object to the Active Directory Domain Services store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site object already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.NotSupportedException">The transport type is not supported.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000269 RID: 617 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes the current site.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600026A RID: 618 RVA: 0x0000208C File Offset: 0x0000028C
		public void Delete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the name of the site.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the site.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600026B RID: 619 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600026C RID: 620 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the object.</summary>
		// Token: 0x0600026D RID: 621 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources used by the object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x0600026E RID: 622 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
