using System;
using System.IO;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a pipe handle. </summary>
	// Token: 0x0200000A RID: 10
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	public sealed class SafePipeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> class.</summary>
		/// <param name="preexistingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x06000022 RID: 34 RVA: 0x000021E6 File Offset: 0x000003E6
		public SafePipeHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			this.handle = preexistingHandle;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000229C File Offset: 0x0000049C
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			return MonoIO.Close(this.handle, out monoIOError);
		}
	}
}
