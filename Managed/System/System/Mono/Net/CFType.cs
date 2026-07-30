using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x0200004C RID: 76
	internal class CFType
	{
		// Token: 0x06000128 RID: 296
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", EntryPoint = "CFGetTypeID")]
		public static extern IntPtr GetTypeID(IntPtr typeRef);
	}
}
