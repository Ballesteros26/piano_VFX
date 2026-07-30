using System;

namespace Unity
{
	// Token: 0x020007D9 RID: 2009
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x0000F3CE File Offset: 0x0000D5CE
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
