using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class represents a subnet in a <see cref="T:System.DirectoryServices.ActiveDirectory.Forest" />.</summary>
	// Token: 0x02000046 RID: 70
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public class ActiveDirectorySubnet : IDisposable
	{
		/// <summary>Gets the subnet name of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
		/// <returns>The name of the subnet.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the site that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object is a member of.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object for the site that the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object is a member of.</returns>
		/// <exception cref="T:System.InvalidOperationException">Applies to set only. The specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object does not exist. If it was newly created, it must be committed to the directory store before assigning it to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000208C File Offset: 0x0000028C
		public ActiveDirectorySite Site
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

		/// <summary>Gets or sets the location description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
		/// <returns>The location description of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</returns>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000208C File Offset: 0x0000028C
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

		/// <summary>Returns a subnet that is based on a subnet name.</summary>
		/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> for the requested subnet.</returns>
		/// <param name="context">An <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that is valid for this subnet.</param>
		/// <param name="subnetName">The name of the subnet to find.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">In the <paramref name="context" /> parameter that was specified, the site could not be found for the given <paramref name="subnetName" /> parameter.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:The target in the <paramref name="context" /> parameter is not a forest, configuration set, domain controller, or AD LDS server.<paramref name="subnetName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="subnetName" /> is null.</exception>
		// Token: 0x060002BB RID: 699 RVA: 0x0000208C File Offset: 0x0000028C
		public static ActiveDirectorySubnet FindByName(DirectoryContext context, string subnetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object and subnet name.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</param>
		/// <param name="subnetName">A <see cref="T:System.String" /> that specifies the name of the subnet.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">
		///   <paramref name="context" /> specifies a configuration set, but no AD LDS instance was found.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:<paramref name="context" /> does not refer a valid forest, configuration set, domain controller, or AD LDS server.<paramref name="subnetName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" /> or <paramref name="subnetName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x060002BC RID: 700 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public ActiveDirectorySubnet(DirectoryContext context, string subnetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> class, using the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object, subnet name, and site name.</summary>
		/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that specifies the context for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</param>
		/// <param name="subnetName">A <see cref="T:System.String" /> that specifies the name of the subnet.</param>
		/// <param name="siteName">A <see cref="T:System.String" /> that specifies the name of the site that contains the subnet.</param>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">
		///   <paramref name="context" /> specifies a configuration set, but no AD LDS instance was found.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ArgumentException">This exception will occur for any of the following reasons:<paramref name="context" /> does not refer to a valid forest, configuration set, domain controller, or AD LDS server.<paramref name="subnetName" /> or <paramref name="siteName" /> is an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="context" />, <paramref name="subnetName" />, or <paramref name="siteName" /> is null.</exception>
		/// <exception cref="T:System.Security.Authentication.AuthenticationException">The credentials that were supplied are not valid.</exception>
		// Token: 0x060002BD RID: 701 RVA: 0x00004B3A File Offset: 0x00002D3A
		public ActiveDirectorySubnet(DirectoryContext context, string subnetName, string siteName)
			: this(context, subnetName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes any changes to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object to the Active Directory Domain Services store.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectExistsException">The subnet object already exists.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060002BE RID: 702 RVA: 0x0000208C File Offset: 0x0000028C
		public void Save()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deletes the subnet that is represented by this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
		/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryServerDownException">The target server is either busy or unavailable.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The specified account does not have permission to perform this operation.</exception>
		// Token: 0x060002BF RID: 703 RVA: 0x0000208C File Offset: 0x0000028C
		public void Delete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the name of the subnet.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of the subnet.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060002C0 RID: 704 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
		/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object for this <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
		// Token: 0x060002C1 RID: 705 RVA: 0x0000208C File Offset: 0x0000028C
		public DirectoryEntry GetDirectoryEntry()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object.</summary>
		// Token: 0x060002C2 RID: 706 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySubnet" /> object and optionally releases the managed resources.</summary>
		/// <param name="disposing">true if the managed resources should be released; false if only the unmanaged resources should be released.</param>
		// Token: 0x060002C3 RID: 707 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
