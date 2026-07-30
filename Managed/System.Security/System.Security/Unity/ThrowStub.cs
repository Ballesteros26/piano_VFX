using System;

namespace Unity
{
	// Token: 0x02000093 RID: 147
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x00012407 File Offset: 0x00010607
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
