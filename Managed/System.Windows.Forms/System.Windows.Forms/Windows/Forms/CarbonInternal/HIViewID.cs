using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004B8 RID: 1208
	internal struct HIViewID
	{
		// Token: 0x06004C1B RID: 19483 RVA: 0x0012EED8 File Offset: 0x0012D0D8
		public HIViewID(uint type, uint id)
		{
			this.type = type;
			this.id = id;
		}

		// Token: 0x0400296E RID: 10606
		public uint type;

		// Token: 0x0400296F RID: 10607
		public uint id;
	}
}
