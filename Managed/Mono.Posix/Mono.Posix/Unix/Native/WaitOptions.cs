using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200003B RID: 59
	[Flags]
	[Map]
	public enum WaitOptions
	{
		// Token: 0x0400020B RID: 523
		WNOHANG = 1,
		// Token: 0x0400020C RID: 524
		WUNTRACED = 2
	}
}
