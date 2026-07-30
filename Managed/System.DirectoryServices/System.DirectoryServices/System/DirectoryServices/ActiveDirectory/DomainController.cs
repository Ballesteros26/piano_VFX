using System;
using System.Net;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> class represents a domain controller in an Active Directory domain.</summary>
	// Token: 0x02000055 RID: 85
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class DomainController : DirectoryServer
	{
		/// <summary>Gets the forest that this domain controller is a member of.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" /> object that represents the forest that this domain controller is a member of.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000208C File Offset: 0x0000028C
		public Forest Forest
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current date and time from this domain controller.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the current date and time from this domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime CurrentTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the highest update sequence number that has been committed to this domain controller.</summary>
		/// <returns>The highest update sequence number (USN) that has been committed to this domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000208C File Offset: 0x0000028C
		public long HighestCommittedUsn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the operating system version of this domain controller.</summary>
		/// <returns>The version of the operating system that is in use on this domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000208C File Offset: 0x0000028C
		public string OSVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the roles that this domain controller serves.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRoleCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> members that indicate the roles that this domain controller serves.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectoryRoleCollection Roles
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the domain that this domain controller is a member of.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.Domain" /> object that represents the domain that this domain controller is a member of.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000208C File Offset: 0x0000028C
		public Domain Domain
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the IP address of this domain controller.</summary>
		/// <returns>The Internet protocol (IP) address of this domain controller in string form.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000208C File Offset: 0x0000028C
		public override string IPAddress
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DnsPermission(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the site that this domain controller belongs to.</summary>
		/// <returns>The name of the site that this domain controller belongs to.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">The site name was not found.</exception>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000208C File Offset: 0x0000028C
		public override string SiteName
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the synchronization delegate for this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.SyncUpdateCallback" /> delegate that will be used for synchronization notifications.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000208C File Offset: 0x0000028C
		public override SyncUpdateCallback SyncFromAllServersCallback
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				throw new NotImplementedException();
			}
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the inbound replication connections for this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the inbound replication connections for this domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000208C File Offset: 0x0000028C
		public override ReplicationConnectionCollection InboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the outbound replication connections for this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the outbound replication connections for this domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000208C File Offset: 0x0000028C
		public override ReplicationConnectionCollection OutboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> class.</summary>
		// Token: 0x06000363 RID: 867 RVA: 0x00004B73 File Offset: 0x00002D73
		protected DomainController()
		{
		}

		/// <summary>Causes the object to release all managed and/or unmanaged resources.</summary>
		/// <param name="disposing">Determines if the managed resources should be released. true if the managed resources are released; otherwise, false.</param>
		// Token: 0x06000364 RID: 868 RVA: 0x00004B7B File Offset: 0x00002D7B
		protected override void Dispose(bool disposing)
		{
			base.Dispose();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object for the specified context.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that was found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of this context must be a domain controller.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> could not be made.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> is not valid.</exception>
		// Token: 0x06000365 RID: 869 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainController GetDomainController(DirectoryContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a single domain controller in the specified context.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that was found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No domain controller was found.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06000366 RID: 870 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainController FindOne(DirectoryContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a single domain controller in the specified context and site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that was found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
		/// <param name="siteName">The name of the site to search for a domain controller.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No domain controller was found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
		// Token: 0x06000367 RID: 871 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainController FindOne(DirectoryContext context, string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a single domain controller in the specified context, allowing for additional search options.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that was found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
		/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No domain controller was found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> or <paramref name="flag" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		// Token: 0x06000368 RID: 872 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainController FindOne(DirectoryContext context, LocatorOptions flag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds a single domain controller in the specified context and site, allowing for additional search options.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object that represents the domain controller that was found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
		/// <param name="siteName">The name of the site to search for a domain controller.</param>
		/// <param name="flag">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.LocatorOptions" /> members that defines the type of domain controller to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No domain controller was found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" />, <paramref name="siteName" />, or <paramref name="flag" /> is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
		// Token: 0x06000369 RID: 873 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainController FindOne(DirectoryContext context, string siteName, LocatorOptions flag)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all domain controllers in the specified context.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> object that contains the domain controllers found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search. The target of this <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object is the name of the domain that will be searched for domain controllers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="context" /> is not valid.</exception>
		// Token: 0x0600036A RID: 874 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainControllerCollection FindAll(DirectoryContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finds all domain controllers in the specified context and site.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainControllerCollection" /> object that contains the domain controllers that were found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search.</param>
		/// <param name="siteName">The name of the site to search for domain controllers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteName" /> is null.</exception>
		// Token: 0x0600036B RID: 875 RVA: 0x0000208C File Offset: 0x0000028C
		public static DomainControllerCollection FindAll(DirectoryContext context, string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Promotes this domain controller to a global catalog server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.GlobalCatalog" /> object that represents the global catalog server.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600036C RID: 876 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual GlobalCatalog EnableGlobalCatalog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines if this domain controller is a global catalog server.</summary>
		/// <returns>true if this domain controller is a global catalog server or false otherwise.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600036D RID: 877 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual bool IsGlobalCatalog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes ownership of the specified role to be transferred to this domain controller.</summary>
		/// <param name="role">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> members that specifies which role will be transferred to this domain controller.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="role" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600036E RID: 878 RVA: 0x0000208C File Offset: 0x0000028C
		public void TransferRoleOwnership(ActiveDirectoryRole role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes this domain controller to take ownership of the specified role.</summary>
		/// <param name="role">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryRole" /> members that specifies which role the domain controller should take ownership of.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="role" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600036F RID: 879 RVA: 0x0000208C File Offset: 0x0000028C
		public void SeizeRoleOwnership(ActiveDirectoryRole role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectorySearcher" /> object for the domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectorySearcher" /> object for the domain controller.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x06000370 RID: 880 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual DirectorySearcher GetDirectorySearcher()
		{
			throw new NotImplementedException();
		}

		/// <summary>Uses the Knowledge Consistency Checker (KCC) to verify the replication topology for this domain controller.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000371 RID: 881 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void CheckReplicationConsistency()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the replication cursor information for the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursorCollection" /> that contains the replication cursor information.</returns>
		/// <param name="partition">The distinguished name of the partition for which to retrieve the replication cursor information.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partition" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partition" /> is an empty string.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000372 RID: 882 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override ReplicationCursorCollection GetReplicationCursors(string partition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the current and pending replication operations for this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> object that contains the current and pending replication operations.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000373 RID: 883 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationOperationInformation GetReplicationOperationInformation()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the replication neighbors for the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this object.</returns>
		/// <param name="partition">The distinguished name of the partition for which to retrieve the replication neighbors.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partition" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partition" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000374 RID: 884 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationNeighborCollection GetReplicationNeighbors(string partition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the replication neighbors for this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this domain controller.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationNeighborCollection GetAllReplicationNeighbors()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a list of the replication connection failures recorded by this domain controller.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailureCollection" /> object that contains the replication connection failures that were recorded by this domain controller.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000376 RID: 886 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationFailureCollection GetReplicationConnectionFailures()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the replication metadata for a specific Active Directory Domain Services object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> object that contains the replication metadata for the specified object.</returns>
		/// <param name="objectPath">The path to the object for which to retrieve the replication metadata.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectPath" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="objectPath" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes this domain controller to synchronize the specified partition with the specified domain controller.</summary>
		/// <param name="partition">The distinguished name of the partition with which to synchronize the domain controller.</param>
		/// <param name="sourceServer">The name of the server with which to synchronize the partition.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partition" /> or <paramref name="sourceServer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partition" /> or <paramref name="sourceServer" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000378 RID: 888 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void SyncReplicaFromServer(string partition, string sourceServer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Begins a synchronization of the specified partition.</summary>
		/// <param name="partition">The distinguished name of the partition to synchronize.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partition" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partition" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000379 RID: 889 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void TriggerSyncReplicaFromNeighbors(string partition)
		{
			throw new NotImplementedException();
		}

		/// <summary>Causes this domain controller to synchronize the specified partition with all other domain controllers.</summary>
		/// <param name="partition">The distinguished name of the partition for the domain controller to synchronize.</param>
		/// <param name="options">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOptions" /> members.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="partition" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partition" /> is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException">An error occurred in the synchronization operation.</exception>
		// Token: 0x0600037A RID: 890 RVA: 0x0000208C File Offset: 0x0000028C
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
