using System;

namespace System.Xml.Schema
{
	// Token: 0x02000399 RID: 921
	internal struct Position
	{
		// Token: 0x06002521 RID: 9505 RVA: 0x000E04A0 File Offset: 0x000DE6A0
		public Position(int symbol, object particle)
		{
			this.symbol = symbol;
			this.particle = particle;
		}

		// Token: 0x04001928 RID: 6440
		public int symbol;

		// Token: 0x04001929 RID: 6441
		public object particle;
	}
}
