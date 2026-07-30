using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007D RID: 125
	[AttributeUsage(AttributeTargets.Field)]
	internal class time_tAttribute : MapAttribute
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0000E216 File Offset: 0x0000C416
		public time_tAttribute()
			: base("time_t")
		{
		}
	}
}
