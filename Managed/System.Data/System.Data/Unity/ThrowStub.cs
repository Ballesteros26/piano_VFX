using System;

namespace Unity
{
	// Token: 0x020003DA RID: 986
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
