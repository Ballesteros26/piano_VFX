using System;
using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryPartition" /> class is an abstract class that represents a directory partition in a domain.</summary>
	// Token: 0x02000035 RID: 53
	public abstract class ActiveDirectoryPartition : IDisposable
	{
		/// <summary>Gets the partition name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the partition name.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Releases all managed and unmanaged resources that are held by the object.</summary>
		// Token: 0x060001B8 RID: 440 RVA: 0x00004060 File Offset: 0x00002260
		public void Dispose()
		{
		}

		/// <summary>Releases the managed resources that are used by the object and, optionally, releases unmanaged resources.</summary>
		/// <param name="disposing">A <see cref="T:System.Boolean" /> value that determines if the managed resources should be released. true if the managed resources are released; false if only the unmanaged resources are released.</param>
		// Token: 0x060001B9 RID: 441 RVA: 0x00004060 File Offset: 0x00002260
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Retrieves a string representation of the current directory partition.</summary>
		/// <returns>Returns a string representation of the current directory partition.</returns>
		// Token: 0x060001BA RID: 442 RVA: 0x0000208C File Offset: 0x0000028C
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</summary>
		/// <returns>Returns a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</returns>
		// Token: 0x060001BB RID: 443
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public abstract DirectoryEntry GetDirectoryEntry();
	}
}
