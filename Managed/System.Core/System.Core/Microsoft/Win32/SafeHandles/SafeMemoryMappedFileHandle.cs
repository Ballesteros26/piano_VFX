using System;
using System.IO.MemoryMappedFiles;
using Unity;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a memory-mapped file for sequential access.</summary>
	// Token: 0x02000004 RID: 4
	public sealed class SafeMemoryMappedFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021E6 File Offset: 0x000003E6
		public SafeMemoryMappedFileHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			this.handle = preexistingHandle;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021F6 File Offset: 0x000003F6
		protected override bool ReleaseHandle()
		{
			MemoryMapImpl.CloseMapping(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000220F File Offset: 0x0000040F
		internal SafeMemoryMappedFileHandle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
