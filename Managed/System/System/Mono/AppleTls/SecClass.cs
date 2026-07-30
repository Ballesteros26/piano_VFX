using System;
using Mono.Net;

namespace Mono.AppleTls
{
	// Token: 0x020000BD RID: 189
	internal static class SecClass
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		static SecClass()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				SecClass.Identity = CFObject.GetIntPtr(intPtr, "kSecClassIdentity");
				SecClass.Certificate = CFObject.GetIntPtr(intPtr, "kSecClassCertificate");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000E628 File Offset: 0x0000C828
		public static IntPtr FromSecKind(SecKind secKind)
		{
			if (secKind == SecKind.Identity)
			{
				return SecClass.Identity;
			}
			if (secKind != SecKind.Certificate)
			{
				throw new ArgumentException("secKind");
			}
			return SecClass.Certificate;
		}

		// Token: 0x04000ADF RID: 2783
		public static readonly IntPtr Identity;

		// Token: 0x04000AE0 RID: 2784
		public static readonly IntPtr Certificate;
	}
}
