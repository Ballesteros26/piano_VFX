using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000054 RID: 84
	internal class CFDictionary : CFObject
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00004854 File Offset: 0x00002A54
		static CFDictionary()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFDictionary.KeyCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryKeyCallBacks");
				CFDictionary.ValueCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryValueCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000043D8 File Offset: 0x000025D8
		public CFDictionary(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000048B8 File Offset: 0x00002AB8
		public static CFDictionary FromObjectAndKey(IntPtr obj, IntPtr key)
		{
			return new CFDictionary(CFDictionary.CFDictionaryCreate(IntPtr.Zero, new IntPtr[] { key }, new IntPtr[] { obj }, (IntPtr)1, CFDictionary.KeyCallbacks, CFDictionary.ValueCallbacks), true);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000048F0 File Offset: 0x00002AF0
		public static CFDictionary FromKeysAndObjects(IList<Tuple<IntPtr, IntPtr>> items)
		{
			IntPtr[] array = new IntPtr[items.Count];
			IntPtr[] array2 = new IntPtr[items.Count];
			for (int i = 0; i < items.Count; i++)
			{
				array[i] = items[i].Item1;
				array2[i] = items[i].Item2;
			}
			return new CFDictionary(CFDictionary.CFDictionaryCreate(IntPtr.Zero, array, array2, (IntPtr)items.Count, CFDictionary.KeyCallbacks, CFDictionary.ValueCallbacks), true);
		}

		// Token: 0x0600016B RID: 363
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreate(IntPtr allocator, IntPtr[] keys, IntPtr[] vals, IntPtr len, IntPtr keyCallbacks, IntPtr valCallbacks);

		// Token: 0x0600016C RID: 364
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryGetValue(IntPtr handle, IntPtr key);

		// Token: 0x0600016D RID: 365
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateCopy(IntPtr allocator, IntPtr handle);

		// Token: 0x0600016E RID: 366 RVA: 0x0000496B File Offset: 0x00002B6B
		public CFDictionary Copy()
		{
			return new CFDictionary(CFDictionary.CFDictionaryCreateCopy(IntPtr.Zero, base.Handle), true);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004983 File Offset: 0x00002B83
		public CFMutableDictionary MutableCopy()
		{
			return new CFMutableDictionary(CFDictionary.CFDictionaryCreateMutableCopy(IntPtr.Zero, IntPtr.Zero, base.Handle), true);
		}

		// Token: 0x06000170 RID: 368
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryCreateMutableCopy(IntPtr allocator, IntPtr capacity, IntPtr theDict);

		// Token: 0x06000171 RID: 369 RVA: 0x000049A0 File Offset: 0x00002BA0
		public IntPtr GetValue(IntPtr key)
		{
			return CFDictionary.CFDictionaryGetValue(base.Handle, key);
		}

		// Token: 0x1700002B RID: 43
		public IntPtr this[IntPtr key]
		{
			get
			{
				return this.GetValue(key);
			}
		}

		// Token: 0x0400074E RID: 1870
		private static readonly IntPtr KeyCallbacks;

		// Token: 0x0400074F RID: 1871
		private static readonly IntPtr ValueCallbacks;
	}
}
