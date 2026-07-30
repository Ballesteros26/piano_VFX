using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007A RID: 122
	[AttributeUsage(AttributeTargets.Field)]
	internal class pid_tAttribute : MapAttribute
	{
		// Token: 0x0600065F RID: 1631 RVA: 0x0000E1EF File Offset: 0x0000C3EF
		public pid_tAttribute()
			: base("pid_t")
		{
		}
	}
}
