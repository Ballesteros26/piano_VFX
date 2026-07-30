using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000034 RID: 52
	internal class Level2Map
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00005265 File Offset: 0x00003465
		public Level2Map(byte source, byte replace)
		{
			this.Source = source;
			this.Replace = replace;
		}

		// Token: 0x040003DF RID: 991
		public byte Source;

		// Token: 0x040003E0 RID: 992
		public byte Replace;
	}
}
