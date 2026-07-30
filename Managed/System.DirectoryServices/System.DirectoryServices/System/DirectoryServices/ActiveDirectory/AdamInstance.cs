using System;
using System.Net;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> class represents an AD LDS instance server.</summary>
	// Token: 0x02000094 RID: 148
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class AdamInstance : DirectoryServer
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00002644 File Offset: 0x00000844
		internal AdamInstance()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object for this AD LDS instance.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ConfigurationSet" /> object for this <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00003C27 File Offset: 0x00001E27
		public ConfigurationSet ConfigurationSet
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the default partition that this AD LDS instance serves.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the distinguished name of the default partition that this AD LDS instance serves.  If the default partition is not set, this will return null.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">The partition name is not in a valid distinguished name format, or the AD LDS instance does not serve this partition. Applies to set only.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00002644 File Offset: 0x00000844
		public string DefaultPartition
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the host name of the computer that hosts this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the computer host name.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00003C27 File Offset: 0x00001E27
		public string HostName
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the inbound replication connections for this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the inbound replication connections for this AD LDS instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00003C27 File Offset: 0x00001E27
		public override ReplicationConnectionCollection InboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the IP address of the computer that hosts this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the IP address of the computer that hosts this AD LDS instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00003C27 File Offset: 0x00001E27
		public override string IPAddress
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DnsPermission(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the LDAP port number of this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that contains the LDAP port number of this AD LDS instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00004CAC File Offset: 0x00002EAC
		public int LdapPort
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the outbound replication connections for this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the outbound replication connections for this AD LDS instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00003C27 File Offset: 0x00001E27
		public override ReplicationConnectionCollection OutboundConnections
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the roles that this AD LDS instance holds.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRoleCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> members that indicate the roles that this AD LDS instance serves.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00003C27 File Offset: 0x00001E27
		public AdamRoleCollection Roles
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the site of which this AD LDS instance is a member.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the site of which this AD LDS instance is a member.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00003C27 File Offset: 0x00001E27
		public override string SiteName
		{
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the SSL port number of this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that contains the SSL port number of this AD LDS instance.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00004CC8 File Offset: 0x00002EC8
		public int SslPort
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets the synchronization delegate for this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.SyncUpdateCallback" /> delegate that this AD LDS instance will use for synchronization notifications.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00003C27 File Offset: 0x00001E27
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00002644 File Offset: 0x00000844
		public override SyncUpdateCallback SyncFromAllServersCallback
		{
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
			[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Invokes the Knowledge Consistency Checker (KCC) that verifies the replication topology for this AD LDS instance.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004AF RID: 1199 RVA: 0x00002644 File Offset: 0x00000844
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void CheckReplicationConsistency()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Finds all AD LDS instances in the specified context that host the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstanceCollection" /> that contains the AD LDS instances that are found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search. The target of this context must be an AD LDS configuration set.</param>
		/// <param name="partitionName">A <see cref="T:System.String" /> that contains the name of the partition to search for AD LDS instances.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="context" /> or <paramref name="partitionName" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or <paramref name="partitionName" /> parameter is null.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No AD LDS instance was found.</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x00003C27 File Offset: 0x00001E27
		public static AdamInstanceCollection FindAll(DirectoryContext context, string partitionName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Finds a single AD LDS instance in the specified context that hosts the specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that represents the AD LDS instance that is found by the search.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use for the search. The target of this context must be an AD LDS configuration set.</param>
		/// <param name="partitionName">A <see cref="T:System.String" /> that contains the name of the partition to search for an AD LDS instance.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">No AD LDS instance was found.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="context" /> or <paramref name="partitionName" />  parameter is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or <paramref name="partitionName" /> parameter is null.</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00003C27 File Offset: 0x00001E27
		public static AdamInstance FindOne(DirectoryContext context, string partitionName)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns an AD LDS instance for a specified context.</summary>
		/// <returns>A <see cref="M:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object that refers to the AD LDS instance that is found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of the context must be an AD LDS instance.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target that was specified in the <paramref name="context" /> parameter could not be made.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="context" /> parameter is not valid.</exception>
		// Token: 0x060004B2 RID: 1202 RVA: 0x00003C27 File Offset: 0x00001E27
		public static AdamInstance GetAdamInstance(DirectoryContext context)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the replication neighbors for this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this AD LDS instance.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004B3 RID: 1203 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override ReplicationNeighborCollection GetAllReplicationNeighbors()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a list of replication connection failures that are recorded by this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailureCollection" /> object that contains the replication connection failures that are recorded by this AD LDS instance.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004B4 RID: 1204 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationFailureCollection GetReplicationConnectionFailures()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the replication cursor information for a specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursorCollection" /> object that contains the replication cursor information.</returns>
		/// <param name="partition">A <see cref="T:System.String" /> that contains the distinguished name of the partition for which to retrieve the replication cursor information.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partition" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="partition" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004B5 RID: 1205 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ReplicationCursorCollection GetReplicationCursors(string partition)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the replication metadata for a specific Active Directory Domain Services object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> object that contains the replication cursor information.</returns>
		/// <param name="objectPath">A <see cref="T:System.String" /> that contains the path to the Active Directory Domain Services object for which to retrieve the replication metadata.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectPath" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="objectPath" /> is not valid.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004B6 RID: 1206 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the replication neighbors for a specified partition.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this AD LDS instance.</returns>
		/// <param name="partition">A <see cref="T:System.String" /> that contains the distinguished name of the partition for which to retrieve the replication neighbors.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partition" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="partition" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004B7 RID: 1207 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override ReplicationNeighborCollection GetReplicationNeighbors(string partition)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the current and pending replication operations for this AD LDS instance.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> object that contains the current and pending replications operations.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004B8 RID: 1208 RVA: 0x00003C27 File Offset: 0x00001E27
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override ReplicationOperationInformation GetReplicationOperationInformation()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Commits changes to the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> object to the underlying directory store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004B9 RID: 1209 RVA: 0x00002644 File Offset: 0x00000844
		public void Save()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Seizes ownership of the specified role.</summary>
		/// <param name="role">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> members that specifies which role the AD LDS instance should take ownership of.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="role" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004BA RID: 1210 RVA: 0x00002644 File Offset: 0x00000844
		public void SeizeRoleOwnership(AdamRole role)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Synchronizes the specified partition with all other domain controllers.</summary>
		/// <param name="partition">A <see cref="T:System.String" /> that contains the distinguished name of the partition of the domain controller to synchronize.</param>
		/// <param name="options">A combination of one of more of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOptions" /> members.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partition" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="partition" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException">An error occurred in the synchronization operation.</exception>
		// Token: 0x060004BB RID: 1211 RVA: 0x00002644 File Offset: 0x00000844
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Synchronizes the specified partition with the specified domain controller.</summary>
		/// <param name="partition">A <see cref="T:System.String" /> that contains the distinguished name of the partition to synchronize.</param>
		/// <param name="sourceServer">A <see cref="T:System.String" /> that contains the name of the server with which to synchronize.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partition" /> or <paramref name="sourceServer" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="partition" /> or <paramref name="sourceServer" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004BC RID: 1212 RVA: 0x00002644 File Offset: 0x00000844
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void SyncReplicaFromServer(string partition, string sourceServer)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Transfers the specified role to this AD LDS instance.</summary>
		/// <param name="role">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.AdamRole" /> members that specifies which role should be transferred to this AD LDS instance.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="role" /> parameter is not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060004BD RID: 1213 RVA: 0x00002644 File Offset: 0x00000844
		public void TransferRoleOwnership(AdamRole role)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Begins a synchronization of the specified partition.</summary>
		/// <param name="partition">A <see cref="T:System.String" /> that contains the distinguished name of the partition to synchronize.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="partition" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="partition" /> is parameter not valid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060004BE RID: 1214 RVA: 0x00002644 File Offset: 0x00000844
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void TriggerSyncReplicaFromNeighbors(string partition)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
