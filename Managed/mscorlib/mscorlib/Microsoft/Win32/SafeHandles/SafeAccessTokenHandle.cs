using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000C7 RID: 199
	[SecurityCritical]
	public sealed class SafeAccessTokenHandle : SafeHandle
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x00021F40 File Offset: 0x00020140
		private SafeAccessTokenHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00021F4E File Offset: 0x0002014E
		public SafeAccessTokenHandle(IntPtr handle)
			: base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00021F63 File Offset: 0x00020163
		public static SafeAccessTokenHandle InvalidHandle
		{
			[SecurityCritical]
			get
			{
				return new SafeAccessTokenHandle(IntPtr.Zero);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00021F6F File Offset: 0x0002016F
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00003B29 File Offset: 0x00001D29
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return true;
		}
	}
}
