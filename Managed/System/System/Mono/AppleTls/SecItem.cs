using System;
using System.Runtime.InteropServices;
using Mono.Net;

namespace Mono.AppleTls
{
	// Token: 0x020000BC RID: 188
	internal class SecItem
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000E560 File Offset: 0x0000C760
		static SecItem()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/Security.framework/Security", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				SecItem.ReturnRef = CFObject.GetIntPtr(intPtr, "kSecReturnRef");
				SecItem.MatchSearchList = CFObject.GetIntPtr(intPtr, "kSecMatchSearchList");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x06000475 RID: 1141
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		internal static extern SecStatusCode SecItemCopyMatching(IntPtr query, out IntPtr result);

		// Token: 0x04000ADD RID: 2781
		public static readonly IntPtr ReturnRef;

		// Token: 0x04000ADE RID: 2782
		public static readonly IntPtr MatchSearchList;
	}
}
