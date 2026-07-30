using System;

namespace Unity
{
	// Token: 0x020001D5 RID: 469
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x000168CF File Offset: 0x00014ACF
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
