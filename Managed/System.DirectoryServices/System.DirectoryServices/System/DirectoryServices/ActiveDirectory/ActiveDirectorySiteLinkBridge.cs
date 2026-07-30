using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class represents a set of site links that communicate through a transport.</summary>
	// Token: 0x02000043 RID: 67
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySiteLinkBridge : IDisposable
	{
		/// <summary>Gets the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the current site link bridge object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a collection of site link objects that are associated with the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkCollection" /> object that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects that are associated with the current site link bridge object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySiteLinkCollection SiteLinks
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the transport type for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that represents the transport type that is used by the current site link bridge object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectoryTransportType TransportType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and name.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
		/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:The <paramref name="context" /> parameter does not refer to a valid forest, configuration set,  domain controller, or AD LDS server.The <paramref name="bridgeName" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter or the <paramref name="bridgeName" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x0600029C RID: 668 RVA: 0x00004B2F File Offset: 0x00002D2F
		public ActiveDirectorySiteLinkBridge(DirectoryContext context, string bridgeName)
			: this(context, bridgeName, ActiveDirectoryTransportType.Rpc)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> class using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, name, and transport type.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
		/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</param>
		/// <param name="transport">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that specifies the transport type to be used.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:The <paramref name="context" /> parameter does not refer to a valid forest, configuration set, domain controller, or AD LDS server.The <paramref name="bridgeName" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter or the <paramref name="bridgeName" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="transport" /> is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		/// <exception cref="T:System.NotSupportedException">The transport type specified in the <paramref name="transport" /> parameter is not supported.</exception>
		// Token: 0x0600029D RID: 669 RVA: 0x00002050 File Offset: 0x00000250
		public ActiveDirectorySiteLinkBridge(DirectoryContext context, string bridgeName, ActiveDirectoryTransportType transport)
		{
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object that matches a given directory context and name for the RPC transport protocol only.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object. null if the object was not found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
		/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site link bridge could not be found for the given <paramref name="bridgeName" /> parameter. </exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons.The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.The <paramref name="bridgeName" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or the <paramref name="bridgeName" /> parameter is null.</exception>
		// Token: 0x0600029E RID: 670 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySiteLinkBridge FindByName(DirectoryContext context, string bridgeName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object that matches a given directory context, name, and transport type.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object. null if the object was not found.</returns>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for the search.</param>
		/// <param name="bridgeName">A <see cref="T:System.String" /> that specifies the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for.</param>
		/// <param name="transport">A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value that specifies the transport type of the object to search for.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site link bridge could not be found for the given <paramref name="bridgeName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or an AD LDS server.The <paramref name="bridgeName" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> or the <paramref name="bridgeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="transport" /> parameter is not a valid <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> value.</exception>
		// Token: 0x0600029F RID: 671 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySiteLinkBridge FindByName(DirectoryContext context, string bridgeName, ActiveDirectoryTransportType transport)
		{
			throw new NotImplementedException();
		}

		/// <summary>Commits all changes to the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to the underlying directory store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The site link bridge object already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060002A0 RID: 672 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes the site link bridge.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060002A1 RID: 673 RVA: 0x0000208C File Offset: 0x0000028C
		public void Delete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the current <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060002A2 RID: 674 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory entry for the site link bridge object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060002A3 RID: 675 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</summary>
		// Token: 0x060002A4 RID: 676 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x060002A5 RID: 677 RVA: 0x0000208C File Offset: 0x0000028C
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}
	}
}
