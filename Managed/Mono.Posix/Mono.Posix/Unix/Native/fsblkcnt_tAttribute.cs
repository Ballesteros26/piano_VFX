using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000075 RID: 117
	[AttributeUsage(AttributeTargets.Field)]
	internal class fsblkcnt_tAttribute : MapAttribute
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0000E1AE File Offset: 0x0000C3AE
		public fsblkcnt_tAttribute()
			: base("fsblkcnt_t")
		{
		}
	}
}
