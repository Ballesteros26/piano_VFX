using System;

namespace Mono.Posix
{
	// Token: 0x02000097 RID: 151
	[Flags]
	[CLSCompliant(false)]
	[Obsolete("Use Mono.Unix.Native.WaitOptions")]
	public enum WaitOptions
	{
		// Token: 0x040004F8 RID: 1272
		WNOHANG = 0,
		// Token: 0x040004F9 RID: 1273
		WUNTRACED = 1
	}
}
