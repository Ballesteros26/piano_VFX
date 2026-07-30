using System;

namespace Unity
{
	// Token: 0x02000B60 RID: 2912
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06006603 RID: 26115 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
