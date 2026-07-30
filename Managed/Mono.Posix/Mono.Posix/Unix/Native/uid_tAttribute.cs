using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007C RID: 124
	[AttributeUsage(AttributeTargets.Field)]
	internal class uid_tAttribute : MapAttribute
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0000E209 File Offset: 0x0000C409
		public uid_tAttribute()
			: base("uid_t")
		{
		}
	}
}
