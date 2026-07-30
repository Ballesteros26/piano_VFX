using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.Field)]
	internal class blkcnt_tAttribute : MapAttribute
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0000E17A File Offset: 0x0000C37A
		public blkcnt_tAttribute()
			: base("blkcnt_t")
		{
		}
	}
}
