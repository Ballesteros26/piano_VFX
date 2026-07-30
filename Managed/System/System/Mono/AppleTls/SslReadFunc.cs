using System;

namespace Mono.AppleTls
{
	// Token: 0x020000C8 RID: 200
	// (Invoke) Token: 0x06000481 RID: 1153
	internal delegate SslStatus SslReadFunc(IntPtr connection, IntPtr data, ref IntPtr dataLength);
}
