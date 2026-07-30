using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000644 RID: 1604
	internal struct Win32_MIBICMPSTATS
	{
		// Token: 0x040028A7 RID: 10407
		public uint Msgs;

		// Token: 0x040028A8 RID: 10408
		public uint Errors;

		// Token: 0x040028A9 RID: 10409
		public uint DestUnreachs;

		// Token: 0x040028AA RID: 10410
		public uint TimeExcds;

		// Token: 0x040028AB RID: 10411
		public uint ParmProbs;

		// Token: 0x040028AC RID: 10412
		public uint SrcQuenchs;

		// Token: 0x040028AD RID: 10413
		public uint Redirects;

		// Token: 0x040028AE RID: 10414
		public uint Echos;

		// Token: 0x040028AF RID: 10415
		public uint EchoReps;

		// Token: 0x040028B0 RID: 10416
		public uint Timestamps;

		// Token: 0x040028B1 RID: 10417
		public uint TimestampReps;

		// Token: 0x040028B2 RID: 10418
		public uint AddrMasks;

		// Token: 0x040028B3 RID: 10419
		public uint AddrMaskReps;
	}
}
