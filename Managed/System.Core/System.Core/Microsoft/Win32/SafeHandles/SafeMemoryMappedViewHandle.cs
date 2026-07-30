using System;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using Unity;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a view of a block of unmanaged memory for random access. </summary>
	// Token: 0x02000005 RID: 5
	public sealed class SafeMemoryMappedViewHandle : SafeBuffer
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002216 File Offset: 0x00000416
		internal SafeMemoryMappedViewHandle(IntPtr mmap_handle, IntPtr base_address, long size)
			: base(true)
		{
			this.mmap_handle = mmap_handle;
			this.handle = base_address;
			base.Initialize((ulong)size);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002234 File Offset: 0x00000434
		internal void Flush()
		{
			MemoryMapImpl.Flush(this.mmap_handle);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002241 File Offset: 0x00000441
		protected override bool ReleaseHandle()
		{
			if (this.handle != (IntPtr)(-1))
			{
				return MemoryMapImpl.Unmap(this.mmap_handle);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000220F File Offset: 0x0000040F
		internal SafeMemoryMappedViewHandle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040001CA RID: 458
		private IntPtr mmap_handle;
	}
}
