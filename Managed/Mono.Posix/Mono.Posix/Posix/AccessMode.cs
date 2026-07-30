using System;

namespace Mono.Posix
{
	// Token: 0x02000098 RID: 152
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.AccessModes")]
	public enum AccessMode
	{
		// Token: 0x040004FB RID: 1275
		R_OK = 1,
		// Token: 0x040004FC RID: 1276
		W_OK = 2,
		// Token: 0x040004FD RID: 1277
		X_OK = 4,
		// Token: 0x040004FE RID: 1278
		F_OK = 8
	}
}
