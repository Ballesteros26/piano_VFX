using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a wait handle. </summary>
	// Token: 0x020000C2 RID: 194
	[SecurityCritical]
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x00021E29 File Offset: 0x00020029
		private SafeWaitHandle()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> class. </summary>
		/// <param name="existingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x06000645 RID: 1605 RVA: 0x00021E32 File Offset: 0x00020032
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00021E90 File Offset: 0x00020090
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			NativeEventCalls.CloseEvent_internal(this.handle);
			return true;
		}
	}
}
