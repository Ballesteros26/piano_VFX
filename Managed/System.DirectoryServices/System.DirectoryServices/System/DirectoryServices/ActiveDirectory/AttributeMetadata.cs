using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> class is used to contain replication metadata for an Active Directory Domain Services attribute.</summary>
	// Token: 0x0200004C RID: 76
	public class AttributeMetadata
	{
		/// <summary>Gets the name of the attribute.</summary>
		/// <returns>The LDAP display name of the attribute.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000208C File Offset: 0x0000028C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the version number of this attribute.</summary>
		/// <returns>The version number of this attribute. Each originating modification of the attribute increases this value by one.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000208C File Offset: 0x0000028C
		public int Version
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time at which the last originating change was made to this attribute.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the last originating change time for this attribute.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000208C File Offset: 0x0000028C
		public DateTime LastOriginatingChangeTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the invocation identifier of the server on which the last change was made to this attribute.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains the identifier.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000208C File Offset: 0x0000028C
		public Guid LastOriginatingInvocationId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the update sequence number (USN) on the originating server at which the last change to this attribute was made.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the USN.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000208C File Offset: 0x0000028C
		public long OriginatingChangeUsn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the update sequence number (USN) on the destination server at which the last change to this attribute was applied.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the USN.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000208C File Offset: 0x0000028C
		public long LocalChangeUsn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the originating server.</summary>
		/// <returns>The distinguished name of the originating server.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000208C File Offset: 0x0000028C
		public string OriginatingServer
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
