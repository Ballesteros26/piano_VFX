using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200007B RID: 123
	[AttributeUsage(AttributeTargets.Field)]
	internal class suseconds_tAttribute : MapAttribute
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		public suseconds_tAttribute()
			: base("suseconds_t")
		{
		}
	}
}
