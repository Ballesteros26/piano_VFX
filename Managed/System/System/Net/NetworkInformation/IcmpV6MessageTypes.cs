using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000646 RID: 1606
	internal class IcmpV6MessageTypes
	{
		// Token: 0x040028B5 RID: 10421
		public const int DestinationUnreachable = 1;

		// Token: 0x040028B6 RID: 10422
		public const int PacketTooBig = 2;

		// Token: 0x040028B7 RID: 10423
		public const int TimeExceeded = 3;

		// Token: 0x040028B8 RID: 10424
		public const int ParameterProblem = 4;

		// Token: 0x040028B9 RID: 10425
		public const int EchoRequest = 128;

		// Token: 0x040028BA RID: 10426
		public const int EchoReply = 129;

		// Token: 0x040028BB RID: 10427
		public const int GroupMembershipQuery = 130;

		// Token: 0x040028BC RID: 10428
		public const int GroupMembershipReport = 131;

		// Token: 0x040028BD RID: 10429
		public const int GroupMembershipReduction = 132;

		// Token: 0x040028BE RID: 10430
		public const int RouterSolicitation = 133;

		// Token: 0x040028BF RID: 10431
		public const int RouterAdvertisement = 134;

		// Token: 0x040028C0 RID: 10432
		public const int NeighborSolicitation = 135;

		// Token: 0x040028C1 RID: 10433
		public const int NeighborAdvertisement = 136;

		// Token: 0x040028C2 RID: 10434
		public const int Redirect = 137;

		// Token: 0x040028C3 RID: 10435
		public const int RouterRenumbering = 138;
	}
}
