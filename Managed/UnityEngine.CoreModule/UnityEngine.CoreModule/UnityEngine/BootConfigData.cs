using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A6 RID: 166
	[NativeHeader("Runtime/Export/Bootstrap/BootConfig.bindings.h")]
	internal class BootConfigData
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000051F7 File Offset: 0x000033F7
		public void AddKey(string key)
		{
			this.Append(key, null);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00005204 File Offset: 0x00003404
		public string Get(string key)
		{
			return this.GetValue(key, 0);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00005220 File Offset: 0x00003420
		public string Get(string key, int index)
		{
			return this.GetValue(key, index);
		}

		// Token: 0x06000290 RID: 656
		[MethodImpl(4096)]
		public extern void Append(string key, string value);

		// Token: 0x06000291 RID: 657
		[MethodImpl(4096)]
		public extern void Set(string key, string value);

		// Token: 0x06000292 RID: 658
		[MethodImpl(4096)]
		private extern string GetValue(string key, int index);

		// Token: 0x06000293 RID: 659 RVA: 0x0000523C File Offset: 0x0000343C
		[RequiredByNativeCode]
		private static BootConfigData WrapBootConfigData(IntPtr nativeHandle)
		{
			return new BootConfigData(nativeHandle);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00005254 File Offset: 0x00003454
		private BootConfigData(IntPtr nativeHandle)
		{
			bool flag = nativeHandle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("native handle can not be null");
			}
			this.m_Ptr = nativeHandle;
		}

		// Token: 0x040001F1 RID: 497
		private IntPtr m_Ptr;
	}
}
