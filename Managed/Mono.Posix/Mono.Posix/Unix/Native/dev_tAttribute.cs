using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.Field)]
	internal class dev_tAttribute : MapAttribute
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0000E194 File Offset: 0x0000C394
		public dev_tAttribute()
			: base("dev_t")
		{
		}
	}
}
