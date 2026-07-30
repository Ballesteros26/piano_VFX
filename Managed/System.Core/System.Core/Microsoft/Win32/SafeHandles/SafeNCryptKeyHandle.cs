using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a key (NCRYPT_KEY_HANDLE).</summary>
	// Token: 0x02000007 RID: 7
	public sealed class SafeNCryptKeyHandle : SafeNCryptHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeNCryptKeyHandle" /> class.</summary>
		// Token: 0x0600001B RID: 27 RVA: 0x00002288 File Offset: 0x00000488
		public SafeNCryptKeyHandle()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002290 File Offset: 0x00000490
		public SafeNCryptKeyHandle(IntPtr handle, SafeHandle parentHandle)
			: base(handle, parentHandle)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002285 File Offset: 0x00000485
		protected override bool ReleaseNativeHandle()
		{
			return false;
		}
	}
}
