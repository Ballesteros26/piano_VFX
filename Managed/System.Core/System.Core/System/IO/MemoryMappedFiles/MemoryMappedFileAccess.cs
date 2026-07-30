using System;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Specifies access capabilities and restrictions for a memory-mapped file or view. </summary>
	// Token: 0x02000050 RID: 80
	[Serializable]
	public enum MemoryMappedFileAccess
	{
		/// <summary>Read and write access to the file.</summary>
		// Token: 0x04000247 RID: 583
		ReadWrite,
		/// <summary>Read-only access to the file.</summary>
		// Token: 0x04000248 RID: 584
		Read,
		/// <summary>Write-only access to file.</summary>
		// Token: 0x04000249 RID: 585
		Write,
		/// <summary>Read and write access to the file, with the restriction that any write operations will not be seen by other processes. </summary>
		// Token: 0x0400024A RID: 586
		CopyOnWrite,
		/// <summary>Read access to the file that can store and run executable code.</summary>
		// Token: 0x0400024B RID: 587
		ReadExecute,
		/// <summary>Read and write access to the file that can can store and run executable code.</summary>
		// Token: 0x0400024C RID: 588
		ReadWriteExecute
	}
}
