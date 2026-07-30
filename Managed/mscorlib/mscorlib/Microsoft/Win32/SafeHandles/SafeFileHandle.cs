using System;
using System.IO;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a file handle. </summary>
	// Token: 0x020000BF RID: 191
	[SecurityCritical]
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x00021E29 File Offset: 0x00020029
		private SafeFileHandle()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> class. </summary>
		/// <param name="preexistingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x0600063B RID: 1595 RVA: 0x00021E32 File Offset: 0x00020032
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00021E44 File Offset: 0x00020044
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			MonoIO.Close(this.handle, out monoIOError);
			return monoIOError == MonoIOError.ERROR_SUCCESS;
		}
	}
}
