using System;

namespace Mono.AppleTls
{
	// Token: 0x020000C9 RID: 201
	// (Invoke) Token: 0x06000485 RID: 1157
	internal delegate SslStatus SslWriteFunc(IntPtr connection, IntPtr data, ref IntPtr dataLength);
}
