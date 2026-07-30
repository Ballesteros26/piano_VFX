using System;

namespace Unity
{
	// Token: 0x02000156 RID: 342
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x000213C0 File Offset: 0x0001F5C0
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
