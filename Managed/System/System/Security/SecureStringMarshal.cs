using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x0200036D RID: 877
	public static class SecureStringMarshal
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x0006BE40 File Offset: 0x0006A040
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemAnsi(s);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0006BE48 File Offset: 0x0006A048
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocAnsi(s);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0006BE50 File Offset: 0x0006A050
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToCoTaskMemUnicode(s);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0006BE58 File Offset: 0x0006A058
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			return Marshal.SecureStringToGlobalAllocUnicode(s);
		}
	}
}
