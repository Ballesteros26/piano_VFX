using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class represents a set of two or more sites that can be scheduled, for replication, to communicate at uniform cost and through a particular transport.</summary>
	// Token: 0x02000042 RID: 66
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySiteLink : IDisposable
	{
		/// <summary>Gets the name of the site link.</summary>
		/// <returns>The name of the site link.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the transport type of the site link.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value indicating the transport type of this site link.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectoryTransportType TransportType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a read/write collection of sites that this site link contains.</summary>
		/// <returns>A writable <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteCollection" /> collection of sites that this site link contains. Sites can be added and deleted from this collection.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySiteCollection Sites
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the site link cost.</summary>
		/// <returns>A cost that is associated with this site link. The default value is 100.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ArgumentException">The cost is less than zero. (applies to set only)</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000208C File Offset: 0x0000028C
		public int Cost
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

		/// <summary>Gets or sets the replication interval between sites.</summary>
		/// <returns>The replication interval between sites.</returns>
		/// <exception cref="T:System.ArgumentException">Invalid <paramref name="ReplicationInterval" /> specified.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000208C File Offset: 0x0000028C
		public TimeSpan ReplicationInterval
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

		/// <summary>Gets or sets the mode for reciprocal replication between sites.</summary>
		/// <returns>true if reciprocal replication is enabled; false if reciprocal replication is disabled.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000208C File Offset: 0x0000028C
		public bool ReciprocalReplicationEnabled
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

		/// <summary>Gets or sets a value indicating whether notifications are enabled.</summary>
		/// <returns>true if notifications are enabled; false if notifications are disabled.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000208C File Offset: 0x0000028C
		public bool NotificationEnabled
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

		/// <summary>Gets or sets the data compression mode of the site link.</summary>
		/// <returns>true if data compression mode is enabled; false if data compression is disabled.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000208C File Offset: 0x0000028C
		public bool DataCompressionEnabled
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

		/// <summary>Gets or sets the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object for the current site link object.</summary>
		/// <returns>Gets or sets the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object for the current site link object. Setting this property changes the replication schedule for the site link.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">An Active Directory Domain Services operation failed. See the exception for details.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySchedule InterSiteReplicationSchedule
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

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and name.</summary>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
		/// <param name="siteLinkName">The name for the site link.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.The target in the <paramref name="context" /> parameter is not a forest,  configuration set, domain controller, or an AD LDS server.<paramref name="siteLinkName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteLinkName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x0600028E RID: 654 RVA: 0x00004B17 File Offset: 0x00002D17
		public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName)
			: this(context, siteLinkName, ActiveDirectoryTransportType.Rpc, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, and transport type.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
		/// <param name="siteLinkName">The name for the site link.</param>
		/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.<paramref name="siteLinkName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> parameter or <paramref name="siteLinkName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
		// Token: 0x0600028F RID: 655 RVA: 0x00004B23 File Offset: 0x00002D23
		public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
			: this(context, siteLinkName, transport, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, transport type, and replication schedule.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object for creating this site link.</param>
		/// <param name="siteLinkName">The name for the site link.</param>
		/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
		/// <param name="schedule">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchedule" /> object that specifies the replication schedule for this site link.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.<paramref name="siteLinkName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or the <paramref name="siteLinkName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
		// Token: 0x06000290 RID: 656 RVA: 0x00002050 File Offset: 0x00000250
		public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport, ActiveDirectorySchedule schedule)
		{
		}

		/// <summary>Returns a site link based on a site link name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object for the requested site link.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site link.</param>
		/// <param name="siteLinkName">The name of the site link to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteLinkName" /> in the <paramref name="context" /> specified.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:The target in the <paramref name="context" /> parameter is not a forest,  configuration set, domain controller, or an AD LDS server.<paramref name="siteLinkName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteLinkName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x06000291 RID: 657 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a site link based on a site link name and transport.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object for the requested site link.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this site link.</param>
		/// <param name="siteLinkName">The name of the site link to find.</param>
		/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies the transport type.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The site could not be found for the given <paramref name="siteLinkName" /> in the <paramref name="context" /> specified.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any one of the following reasons:The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.<paramref name="siteLinkName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="siteLinkName" /> is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transport" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="transport" /> type is not supported.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x06000292 RID: 658 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes any changes to the object to the Active Directory Domain Services store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site object already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000293 RID: 659 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes the current site link.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x06000294 RID: 660 RVA: 0x0000208C File Offset: 0x0000028C
		public void Delete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the site link name.</summary>
		/// <returns>A string that contains the name of the site link.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000295 RID: 661 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> for this object.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this site link.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x06000296 RID: 662 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the object.</summary>
		// Token: 0x06000297 RID: 663 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources used by the object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x06000298 RID: 664 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
