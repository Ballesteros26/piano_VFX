using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 critical handle implementations in which the value of -1 indicates an invalid handle.</summary>
	// Token: 0x020000C6 RID: 198
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.CriticalHandleMinusOneIsInvalid" /> class.</summary>
		// Token: 0x0600064D RID: 1613 RVA: 0x00021F1F File Offset: 0x0002011F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleMinusOneIsInvalid()
			: base(new IntPtr(-1))
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00021F2D File Offset: 0x0002012D
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
