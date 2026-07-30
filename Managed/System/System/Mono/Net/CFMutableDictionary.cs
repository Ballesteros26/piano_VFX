using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000055 RID: 85
	internal class CFMutableDictionary : CFDictionary
	{
		// Token: 0x06000173 RID: 371 RVA: 0x000049B7 File Offset: 0x00002BB7
		public CFMutableDictionary(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000049C1 File Offset: 0x00002BC1
		public void SetValue(IntPtr key, IntPtr val)
		{
			CFMutableDictionary.CFDictionarySetValue(base.Handle, key, val);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000049D0 File Offset: 0x00002BD0
		public static CFMutableDictionary Create()
		{
			IntPtr intPtr = CFMutableDictionary.CFDictionaryCreateMutable(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			return new CFMutableDictionary(intPtr, true);
		}

		// Token: 0x06000176 RID: 374
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFDictionarySetValue(IntPtr handle, IntPtr key, IntPtr val);

		// Token: 0x06000177 RID: 375
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateMutable(IntPtr allocator, IntPtr capacity, IntPtr keyCallback, IntPtr valueCallbacks);
	}
}
