using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 safe handle implementations in which the value of -1 indicates an invalid handle.</summary>
	// Token: 0x020000C4 RID: 196
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class SafeHandleMinusOneIsInvalid : SafeHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleMinusOneIsInvalid" /> class, specifying whether the handle is to be reliably released. </summary>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x06000649 RID: 1609 RVA: 0x00021ECE File Offset: 0x000200CE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleMinusOneIsInvalid(bool ownsHandle)
			: base(new IntPtr(-1), ownsHandle)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00021EDD File Offset: 0x000200DD
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}
