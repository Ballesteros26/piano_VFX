using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a safe handle to the Windows registry.</summary>
	// Token: 0x020000C1 RID: 193
	[SecurityCritical]
	public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x00021E29 File Offset: 0x00020029
		[SecurityCritical]
		internal SafeRegistryHandle()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> class. </summary>
		/// <param name="preexistingHandle">An object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release.</param>
		// Token: 0x06000641 RID: 1601 RVA: 0x00021E32 File Offset: 0x00020032
		[SecurityCritical]
		public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00021E80 File Offset: 0x00020080
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeRegistryHandle.RegCloseKey(this.handle) == 0;
		}

		// Token: 0x06000643 RID: 1603
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr hKey);
	}
}
