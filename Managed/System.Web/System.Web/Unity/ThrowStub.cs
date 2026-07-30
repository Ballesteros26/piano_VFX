using System;

namespace Unity
{
	// Token: 0x020007C2 RID: 1986
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x0001809D File Offset: 0x0001629D
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
