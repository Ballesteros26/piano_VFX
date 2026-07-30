using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020003C5 RID: 965
	internal sealed class SafeGssNameHandle : SafeHandle
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x000C8534 File Offset: 0x000C6734
		public static SafeGssNameHandle CreateUser(string name)
		{
			Interop.NetSecurityNative.Status status2;
			SafeGssNameHandle safeGssNameHandle;
			Interop.NetSecurityNative.Status status = Interop.NetSecurityNative.ImportUserName(out status2, name, Encoding.UTF8.GetByteCount(name), out safeGssNameHandle);
			if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE)
			{
				safeGssNameHandle.Dispose();
				throw new Interop.NetSecurityNative.GssApiException(status, status2);
			}
			return safeGssNameHandle;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000C856C File Offset: 0x000C676C
		public static SafeGssNameHandle CreatePrincipal(string name)
		{
			Interop.NetSecurityNative.Status status2;
			SafeGssNameHandle safeGssNameHandle;
			Interop.NetSecurityNative.Status status = Interop.NetSecurityNative.ImportPrincipalName(out status2, name, Encoding.UTF8.GetByteCount(name), out safeGssNameHandle);
			if (status != Interop.NetSecurityNative.Status.GSS_S_COMPLETE)
			{
				safeGssNameHandle.Dispose();
				throw new Interop.NetSecurityNative.GssApiException(status, status2);
			}
			return safeGssNameHandle;
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000C85A1 File Offset: 0x000C67A1
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000C85B4 File Offset: 0x000C67B4
		protected override bool ReleaseHandle()
		{
			Interop.NetSecurityNative.Status status;
			int num = (int)Interop.NetSecurityNative.ReleaseName(out status, ref this.handle);
			base.SetHandle(IntPtr.Zero);
			return num == 0;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x000C85DC File Offset: 0x000C67DC
		private SafeGssNameHandle()
			: base(IntPtr.Zero, true)
		{
		}
	}
}
