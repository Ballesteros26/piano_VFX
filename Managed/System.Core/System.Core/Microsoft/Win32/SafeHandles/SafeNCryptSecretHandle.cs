using System;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle that represents a secret agreement value (NCRYPT_SECRET_HANDLE).</summary>
	// Token: 0x02000009 RID: 9
	public sealed class SafeNCryptSecretHandle : SafeNCryptHandle
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002285 File Offset: 0x00000485
		protected override bool ReleaseNativeHandle()
		{
			return false;
		}
	}
}
