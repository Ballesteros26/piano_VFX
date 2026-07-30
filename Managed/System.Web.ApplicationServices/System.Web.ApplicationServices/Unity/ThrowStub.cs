using System;

namespace Unity
{
	// Token: 0x0200001B RID: 27
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00002E54 File Offset: 0x00001054
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
