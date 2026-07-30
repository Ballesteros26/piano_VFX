using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020003C6 RID: 966
	internal class SafeGssCredHandle : SafeHandle
	{
		// Token: 0x06002E51 RID: 11857 RVA: 0x000C85EC File Offset: 0x000C67EC
		public static SafeGssCredHandle Create(string username, string password, bool isNtlmOnly)
		{
			if (string.IsNullOrEmpty(username))
			{
				return new SafeGssCredHandle();
			}
			SafeGssCredHandle safeGssCredHandle = null;
			using (SafeGssNameHandle safeGssNameHandle = SafeGssNameHandle.CreateUser(username))
			{
				Interop.NetSecurityNative.Status status2;
				Interop.NetSecurityNative.Status status;
				if (string.IsNullOrEmpty(password))
				{
					status = Interop.NetSecurityNative.InitiateCredSpNego(out status2, safeGssNameHandle, out safeGssCredHandle);
				}
				else
				{
					status = Interop.NetSecurityNative.InitiateCredWithPassword(out status2, isNtlmOnly, safeGssNameHandle, password, Encoding.UTF8.GetByteCount(password), out safeGssCredHandle);
				}
				if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE)
				{
					safeGssCredHandle.Dispose();
					throw new Interop.NetSecurityNative.GssApiException(status, status2);
				}
			}
			return safeGssCredHandle;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000C85DC File Offset: 0x000C67DC
		private SafeGssCredHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000C85A1 File Offset: 0x000C67A1
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000C866C File Offset: 0x000C686C
		protected override bool ReleaseHandle()
		{
			Interop.NetSecurityNative.Status status;
			int num = (int)Interop.NetSecurityNative.ReleaseCred(out status, ref this.handle);
			base.SetHandle(IntPtr.Zero);
			return num == 0;
		}
	}
}
