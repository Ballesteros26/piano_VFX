using System;
using System.Security.Permissions;
using Unity;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> class represents an inter-site transport object.</summary>
	// Token: 0x02000092 RID: 146
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectoryInterSiteTransport : IDisposable
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00002644 File Offset: 0x00000844
		internal ActiveDirectoryInterSiteTransport()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a value that indicates whether all site links are bridged.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether to bridge all site links. true if all site links are bridged; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00004C58 File Offset: 0x00002E58
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00002644 File Offset: 0x00000844
		public bool BridgeAllSiteLinks
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the replication schedule is ignored.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether to ignore the replication schedule. true if the replication schedule is ignored; otherwise, false.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00004C74 File Offset: 0x00002E74
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x00002644 File Offset: 0x00000844
		public bool IgnoreReplicationSchedule
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkBridgeCollection" /> object that contains all site link bridges for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkBridgeCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> objects that represent site link bridges.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x00003C27 File Offset: 0x00001E27
		public ReadOnlySiteLinkBridgeCollection SiteLinkBridges
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkCollection" /> object that contains all site links for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects that represent site links.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00003C27 File Offset: 0x00001E27
		public ReadOnlySiteLinkCollection SiteLinks
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that represents the transport type for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object.</summary>
		/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> values that represents the transport type for this object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00004C90 File Offset: 0x00002E90
		public ActiveDirectoryTransportType TransportType
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ActiveDirectoryTransportType.Rpc;
			}
		}

		/// <summary>Releases all resources that are used by the object.</summary>
		// Token: 0x06000498 RID: 1176 RVA: 0x00002644 File Offset: 0x00000844
		public void Dispose()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources that are used by the object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x06000499 RID: 1177 RVA: 0x00002644 File Offset: 0x00000844
		protected virtual void Dispose(bool disposing)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object for a given directory context and transport type.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object for the item found. An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException" /> exception is thrown if an object was not found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
		/// <param name="transport">An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> object that specifies a transport type to find in the search.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">The object was not found.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transport" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		// Token: 0x0600049A RID: 1178 RVA: 0x00003C27 File Offset: 0x00001E27
		public static ActiveDirectoryInterSiteTransport FindByTransportType(DirectoryContext context, ActiveDirectoryTransportType transport)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory entry for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		// Token: 0x0600049B RID: 1179 RVA: 0x00003C27 File Offset: 0x00001E27
		public DirectoryEntry GetDirectoryEntry()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Commits all changes to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryInterSiteTransport" /> object to the underlying directory store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x0600049C RID: 1180 RVA: 0x00002644 File Offset: 0x00000844
		public void Save()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
