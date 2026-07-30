using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Field)]
	internal class off_tAttribute : MapAttribute
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public off_tAttribute()
			: base("off_t")
		{
		}
	}
}
