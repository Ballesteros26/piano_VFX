using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Drawing2D
{
	// Token: 0x0200014B RID: 331
	[SecurityCritical]
	internal class SafeCustomLineCapHandle : SafeHandle
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x0001F01D File Offset: 0x0001D21D
		internal SafeCustomLineCapHandle(IntPtr h)
			: base(IntPtr.Zero, true)
		{
			base.SetHandle(h);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001F034 File Offset: 0x0001D234
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			int num = 0;
			if (!this.IsInvalid)
			{
				try
				{
					num = GDIPlus.GdipDeleteCustomLineCap(new HandleRef(this, this.handle));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.handle = IntPtr.Zero;
				}
			}
			return num == 0;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0001F098 File Offset: 0x0001D298
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0001F0AA File Offset: 0x0001D2AA
		public static implicit operator IntPtr(SafeCustomLineCapHandle handle)
		{
			if (handle == null)
			{
				return IntPtr.Zero;
			}
			return handle.handle;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001F0BB File Offset: 0x0001D2BB
		public static explicit operator SafeCustomLineCapHandle(IntPtr handle)
		{
			return new SafeCustomLineCapHandle(handle);
		}
	}
}
