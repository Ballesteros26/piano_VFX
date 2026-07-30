using System;

namespace Unity
{
	// Token: 0x02000693 RID: 1683
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06004367 RID: 17255 RVA: 0x0016FC20 File Offset: 0x0016DE20
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
