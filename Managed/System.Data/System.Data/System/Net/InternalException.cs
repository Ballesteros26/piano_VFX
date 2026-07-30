using System;

namespace System.Net
{
	// Token: 0x0200003D RID: 61
	internal class InternalException : Exception
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		internal InternalException()
		{
			NetEventSource.Fail(this, "InternalException thrown.", ".ctor");
		}
	}
}
