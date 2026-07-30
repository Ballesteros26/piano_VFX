using System;
using Unity;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000E1 RID: 225
	public sealed class SafeX509ChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		internal SafeX509ChainHandle(IntPtr handle)
			: base(true)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO]
		protected override bool ReleaseHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal SafeX509ChainHandle()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
