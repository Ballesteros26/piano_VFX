using System;
using System.Runtime.InteropServices;

namespace System.Net.Security
{
	// Token: 0x02000049 RID: 73
	internal abstract class SafeDeleteContext : SafeHandle
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000EA30 File Offset: 0x0000CC30
		protected SafeDeleteContext(SafeFreeCredentials credential)
			: base(IntPtr.Zero, true)
		{
			bool flag = false;
			this._credential = credential;
			this._credential.DangerousAddRef(ref flag);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000EA5F File Offset: 0x0000CC5F
		public override bool IsInvalid
		{
			get
			{
				return this._credential == null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EA6A File Offset: 0x0000CC6A
		protected override bool ReleaseHandle()
		{
			this._credential.DangerousRelease();
			this._credential = null;
			return true;
		}

		// Token: 0x040004D0 RID: 1232
		private SafeFreeCredentials _credential;
	}
}
