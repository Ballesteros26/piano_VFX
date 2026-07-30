using System;

namespace System.Net
{
	// Token: 0x0200053E RID: 1342
	internal class NetConfig : ICloneable
	{
		// Token: 0x06002989 RID: 10633 RVA: 0x000A0936 File Offset: 0x0009EB36
		internal NetConfig()
		{
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000A0946 File Offset: 0x0009EB46
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0400229C RID: 8860
		internal bool ipv6Enabled;

		// Token: 0x0400229D RID: 8861
		internal int MaxResponseHeadersLength = 64;
	}
}
