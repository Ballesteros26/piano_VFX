using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000056 RID: 86
	internal class CFUrl : CFObject
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFUrl(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000179 RID: 377
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFURLCreateWithString(IntPtr allocator, IntPtr str, IntPtr baseURL);

		// Token: 0x0600017A RID: 378 RVA: 0x00004A04 File Offset: 0x00002C04
		public static CFUrl Create(string absolute)
		{
			if (string.IsNullOrEmpty(absolute))
			{
				return null;
			}
			CFString cfstring = CFString.Create(absolute);
			IntPtr intPtr = CFUrl.CFURLCreateWithString(IntPtr.Zero, cfstring.Handle, IntPtr.Zero);
			cfstring.Dispose();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFUrl(intPtr, true);
		}
	}
}
