using System;

namespace Mono.Unix.Native
{
	// Token: 0x0200005F RID: 95
	[Map("struct linger")]
	[CLSCompliant(false)]
	public struct Linger
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000A8F7 File Offset: 0x00008AF7
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.l_onoff, this.l_linger);
		}

		// Token: 0x04000449 RID: 1097
		public int l_onoff;

		// Token: 0x0400044A RID: 1098
		public int l_linger;
	}
}
