using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	/// <summary>Represents the permissions that can be granted for file access and operations on memory-mapped files. </summary>
	// Token: 0x02000053 RID: 83
	public class MemoryMappedFileSecurity : ObjectSecurity<MemoryMappedFileRights>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryMappedFiles.MemoryMappedFileSecurity" /> class. </summary>
		// Token: 0x06000185 RID: 389 RVA: 0x000045FB File Offset: 0x000027FB
		public MemoryMappedFileSecurity()
			: base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004605 File Offset: 0x00002805
		[SecuritySafeCritical]
		internal MemoryMappedFileSecurity(SafeMemoryMappedFileHandle safeHandle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, safeHandle, includeSections)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004611 File Offset: 0x00002811
		[SecuritySafeCritical]
		internal void PersistHandle(SafeHandle handle)
		{
			base.Persist(handle);
		}
	}
}
