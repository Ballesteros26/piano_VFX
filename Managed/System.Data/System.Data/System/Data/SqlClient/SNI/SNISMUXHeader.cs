using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000242 RID: 578
	internal class SNISMUXHeader
	{
		// Token: 0x0400126A RID: 4714
		public const int HEADER_LENGTH = 16;

		// Token: 0x0400126B RID: 4715
		public byte SMID;

		// Token: 0x0400126C RID: 4716
		public byte flags;

		// Token: 0x0400126D RID: 4717
		public ushort sessionId;

		// Token: 0x0400126E RID: 4718
		public uint length;

		// Token: 0x0400126F RID: 4719
		public uint sequenceNumber;

		// Token: 0x04001270 RID: 4720
		public uint highwater;
	}
}
