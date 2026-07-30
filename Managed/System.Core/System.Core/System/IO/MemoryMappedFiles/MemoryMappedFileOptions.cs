using System;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Provides memory allocation options for memory-mapped files.</summary>
	// Token: 0x02000051 RID: 81
	[Flags]
	[Serializable]
	public enum MemoryMappedFileOptions
	{
		/// <summary>No memory allocation options are applied.</summary>
		// Token: 0x0400024E RID: 590
		None = 0,
		/// <summary>Memory allocation is delayed until a view is created with either the <see cref="M:System.IO.MemoryMappedFiles.MemoryMappedFile.CreateViewAccessor" /> or <see cref="M:System.IO.MemoryMappedFiles.MemoryMappedFile.CreateViewStream" /> method.</summary>
		// Token: 0x0400024F RID: 591
		DelayAllocatePages = 67108864
	}
}
