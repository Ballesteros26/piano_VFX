using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> class is an abstract class that represents an Active Directory Domain Services server or AD LDS instance.</summary>
	// Token: 0x02000050 RID: 80
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public abstract class DirectoryServer : IDisposable
	{
		/// <summary>Gets the name of the directory server.</summary>
		/// <returns>The name of the directory server.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the partitions on this directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyStringCollection" /> object that contains the distinguished names of the partitions on this directory server.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000208C File Offset: 0x0000028C
		public ReadOnlyStringCollection Partitions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the IP address of this directory server.</summary>
		/// <returns>The Internet protocol (IP) address of this directory server in string form.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000303 RID: 771
		public abstract string IPAddress
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get;
		}

		/// <summary>Gets the name of the site that this directory server belongs to.</summary>
		/// <returns>The name of the site that this directory server belongs to.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000304 RID: 772
		public abstract string SiteName
		{
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			get;
		}

		/// <summary>Gets or sets the synchronization delegate for this directory server.</summary>
		/// <returns>The delegate that this directory server will use for synchronization notifications.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000305 RID: 773
		// (set) Token: 0x06000306 RID: 774
		public abstract SyncUpdateCallback SyncFromAllServersCallback
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get;
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			set;
		}

		/// <summary>Retrieves the inbound replication connections for this directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the inbound replication connections for this directory server.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000307 RID: 775
		public abstract ReplicationConnectionCollection InboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			get;
		}

		/// <summary>Gets the outbound replication connections for this directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the outbound replication connections for this directory server.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000308 RID: 776
		public abstract ReplicationConnectionCollection OutboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			get;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000208C File Offset: 0x0000028C
		internal DirectoryContext Context
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases all managed and unmanaged resources that are used by the object.</summary>
		// Token: 0x0600030A RID: 778 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases all unmanaged resources and, optionally, all managed resources that are used by the object.</summary>
		/// <param name="disposing">Determines if the managed resources should be released. true if the managed resources are released; false if the managed resources are not released.</param>
		// Token: 0x0600030B RID: 779 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Retrieves the name of the directory server.</summary>
		/// <returns>The name of the server.</returns>
		// Token: 0x0600030C RID: 780 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Moves the directory server to another site within the forest or configuration set.</summary>
		/// <param name="siteName">The name of the site within the domain to which to move the directory server.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600030D RID: 781 RVA: 0x0000208C File Offset: 0x0000028C
		public void MoveToAnotherSite(string siteName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory server.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x0600030E RID: 782 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Uses the Knowledge Consistency Checker (KCC) to verify and recalculate the replication topology for this server.</summary>
		// Token: 0x0600030F RID: 783
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract void CheckReplicationConsistency();

		/// <summary>Retrieves the replication cursor information for the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursorCollection" /> that contains the replication cursor information.</returns>
		/// <param name="partition">The distinguished name of the partition for which to retrieve the replication cursor information.</param>
		// Token: 0x06000310 RID: 784
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract ReplicationCursorCollection GetReplicationCursors(string partition);

		/// <summary>Retrieves the current and pending replication operations for this directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> object that contains the current and pending replication operations.</returns>
		// Token: 0x06000311 RID: 785
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract ReplicationOperationInformation GetReplicationOperationInformation();

		/// <summary>Retrieves the replication neighbors of this directory server for the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this object.</returns>
		/// <param name="partition">The distinguished name of the partition for which to retrieve the replication.</param>
		// Token: 0x06000312 RID: 786
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public abstract ReplicationNeighborCollection GetReplicationNeighbors(string partition);

		/// <summary>Retrieves all of the replication neighbors for this object.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this object.</returns>
		// Token: 0x06000313 RID: 787
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract ReplicationNeighborCollection GetAllReplicationNeighbors();

		/// <summary>Retrieves a collection of the replication connection failures for this directory server.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailureCollection" /> object that contains the replication connection failures for this directory server.</returns>
		// Token: 0x06000314 RID: 788
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract ReplicationFailureCollection GetReplicationConnectionFailures();

		/// <summary>Retrieves the replication metadata for a specific Active Directory Domain Services object.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> object that contains the replication metadata for the specified object.</returns>
		/// <param name="objectPath">The path to the object for which to retrieve the replication metadata.</param>
		// Token: 0x06000315 RID: 789
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public abstract ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath);

		/// <summary>Causes this directory server to synchronize the specified partition with the specified directory server.</summary>
		/// <param name="partition">The distinguished name of the partition to synchronize.</param>
		/// <param name="sourceServer">The name of the server to synchronize the partition with.</param>
		// Token: 0x06000316 RID: 790
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public abstract void SyncReplicaFromServer(string partition, string sourceServer);

		/// <summary>Begins a synchronization of the specified partition.</summary>
		/// <param name="partition">The distinguished name of the partition to synchronize.</param>
		// Token: 0x06000317 RID: 791
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public abstract void TriggerSyncReplicaFromNeighbors(string partition);

		/// <summary>Causes this directory server to synchronize the specified partition with all other directory servers in the same site that hosts the partition.</summary>
		/// <param name="partition">The distinguished name of the partition to synchronize.</param>
		/// <param name="options">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOptions" /> members.</param>
		// Token: 0x06000318 RID: 792
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options);
	}
}
