using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020004C0 RID: 1216
	internal struct MouseTrackingRegionID
	{
		// Token: 0x06004C1E RID: 19486 RVA: 0x0012EF08 File Offset: 0x0012D108
		public MouseTrackingRegionID(uint signature, uint id)
		{
			this.signature = signature;
			this.id = id;
		}

		// Token: 0x0400298F RID: 10639
		public uint signature;

		// Token: 0x04002990 RID: 10640
		public uint id;
	}
}
