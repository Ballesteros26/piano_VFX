using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004A6 RID: 1190
	internal struct EventRecord
	{
		// Token: 0x040028BD RID: 10429
		internal ushort what;

		// Token: 0x040028BE RID: 10430
		internal uint message;

		// Token: 0x040028BF RID: 10431
		internal uint when;

		// Token: 0x040028C0 RID: 10432
		internal QDPoint mouse;

		// Token: 0x040028C1 RID: 10433
		internal ushort modifiers;
	}
}
