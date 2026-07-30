using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000077 RID: 119
	[AttributeUsage(AttributeTargets.Field)]
	internal class ino_tAttribute : MapAttribute
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		public ino_tAttribute()
			: base("ino_t")
		{
		}
	}
}
