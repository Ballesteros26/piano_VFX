using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Field)]
	internal class nlink_tAttribute : MapAttribute
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0000E1D5 File Offset: 0x0000C3D5
		public nlink_tAttribute()
			: base("nlink_t")
		{
		}
	}
}
