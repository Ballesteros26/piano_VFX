using System;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Specifies access rights to a memory-mapped file that is not associated with a file on disk.</summary>
	// Token: 0x02000052 RID: 82
	[Flags]
	public enum MemoryMappedFileRights
	{
		/// <summary>The right to read and write to a file with the restriction that write operations will not be seen by other processes.</summary>
		// Token: 0x04000251 RID: 593
		CopyOnWrite = 1,
		/// <summary>The right to add data to a file or remove data from a file.</summary>
		// Token: 0x04000252 RID: 594
		Write = 2,
		/// <summary>The right to open and copy a file as read-only.</summary>
		// Token: 0x04000253 RID: 595
		Read = 4,
		/// <summary>The right to run an application file.</summary>
		// Token: 0x04000254 RID: 596
		Execute = 8,
		/// <summary>The right to delete a file.</summary>
		// Token: 0x04000255 RID: 597
		Delete = 65536,
		/// <summary>The right to open and copy access and audit rules from a file. This does not include the right to read data, file system attributes, or extended file system attributes.</summary>
		// Token: 0x04000256 RID: 598
		ReadPermissions = 131072,
		/// <summary>The right to change the security and audit rules associated with a file.</summary>
		// Token: 0x04000257 RID: 599
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a file.</summary>
		// Token: 0x04000258 RID: 600
		TakeOwnership = 524288,
		/// <summary>The right to open and copy a file, and the right to add data to a file or remove data from a file.</summary>
		// Token: 0x04000259 RID: 601
		ReadWrite = 6,
		/// <summary>The right to open and copy a folder or file as read-only, and to run application files. This right includes the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileRights.Read" /> right and the <see cref="F:System.IO.MemoryMappedFiles.MemoryMappedFileRights.Execute" /> right.</summary>
		// Token: 0x0400025A RID: 602
		ReadExecute = 12,
		/// <summary>The right to open and copy a file, the right to add data to a file or remove data from a file, and the right to run an application file.</summary>
		// Token: 0x0400025B RID: 603
		ReadWriteExecute = 14,
		/// <summary>The right to exert full control over a file, and to modify access control and audit rules. This value represents the right to do anything with a file and is the combination of all rights in this enumeration.</summary>
		// Token: 0x0400025C RID: 604
		FullControl = 983055,
		/// <summary>The right to get or set permissions on a file.</summary>
		// Token: 0x0400025D RID: 605
		AccessSystemSecurity = 16777216
	}
}
