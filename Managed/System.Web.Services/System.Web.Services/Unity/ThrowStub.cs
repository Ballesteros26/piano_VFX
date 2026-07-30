using System;

namespace Unity
{
	// Token: 0x02000156 RID: 342
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000457C8 File Offset: 0x000439C8
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
