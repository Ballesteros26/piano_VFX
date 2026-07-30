using System;

namespace Unity
{
	// Token: 0x02000090 RID: 144
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000B484 File Offset: 0x00009684
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
