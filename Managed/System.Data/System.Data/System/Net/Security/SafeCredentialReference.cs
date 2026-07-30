using System;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Security
{
	// Token: 0x0200004C RID: 76
	internal sealed class SafeCredentialReference : CriticalHandleMinusOneIsInvalid
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000EB30 File Offset: 0x0000CD30
		internal static SafeCredentialReference CreateReference(SafeFreeCredentials target)
		{
			SafeCredentialReference safeCredentialReference = new SafeCredentialReference(target);
			if (safeCredentialReference.IsInvalid)
			{
				return null;
			}
			return safeCredentialReference;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EB50 File Offset: 0x0000CD50
		private SafeCredentialReference(SafeFreeCredentials target)
		{
			bool flag = false;
			target.DangerousAddRef(ref flag);
			this.Target = target;
			base.SetHandle(new IntPtr(0));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EB80 File Offset: 0x0000CD80
		protected override bool ReleaseHandle()
		{
			SafeFreeCredentials target = this.Target;
			if (target != null)
			{
				target.DangerousRelease();
			}
			this.Target = null;
			return true;
		}

		// Token: 0x040004D4 RID: 1236
		internal SafeFreeCredentials Target;
	}
}
