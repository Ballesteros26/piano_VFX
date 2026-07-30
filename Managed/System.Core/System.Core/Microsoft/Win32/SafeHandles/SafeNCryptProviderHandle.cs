using System;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a key storage provider (NCRYPT_PROV_HANDLE).</summary>
	// Token: 0x02000008 RID: 8
	public sealed class SafeNCryptProviderHandle : SafeNCryptHandle
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002285 File Offset: 0x00000485
		protected override bool ReleaseNativeHandle()
		{
			return false;
		}
	}
}
