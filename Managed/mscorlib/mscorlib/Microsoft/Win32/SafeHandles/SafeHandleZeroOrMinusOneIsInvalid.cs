using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 safe handle implementations in which the value of either 0 or -1 indicates an invalid handle.</summary>
	// Token: 0x020000C3 RID: 195
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" /> class, specifying whether the handle is to be reliably released. </summary>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x06000647 RID: 1607 RVA: 0x00021E9E File Offset: 0x0002009E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle)
			: base(IntPtr.Zero, ownsHandle)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00021EAC File Offset: 0x000200AC
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle.IsNull() || this.handle == new IntPtr(-1);
			}
		}
	}
}
