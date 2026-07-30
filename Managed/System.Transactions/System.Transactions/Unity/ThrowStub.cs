using System;

namespace Unity
{
	// Token: 0x02000032 RID: 50
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003303 File Offset: 0x00001503
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
