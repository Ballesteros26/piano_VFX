using System;

namespace Unity
{
	// Token: 0x020003A9 RID: 937
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0004C349 File Offset: 0x0004A549
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
