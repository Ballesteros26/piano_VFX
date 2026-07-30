using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000633 RID: 1587
	internal struct Win32_MIB_IPSTATS
	{
		// Token: 0x04002875 RID: 10357
		public int Forwarding;

		// Token: 0x04002876 RID: 10358
		public int DefaultTTL;

		// Token: 0x04002877 RID: 10359
		public uint InReceives;

		// Token: 0x04002878 RID: 10360
		public uint InHdrErrors;

		// Token: 0x04002879 RID: 10361
		public uint InAddrErrors;

		// Token: 0x0400287A RID: 10362
		public uint ForwDatagrams;

		// Token: 0x0400287B RID: 10363
		public uint InUnknownProtos;

		// Token: 0x0400287C RID: 10364
		public uint InDiscards;

		// Token: 0x0400287D RID: 10365
		public uint InDelivers;

		// Token: 0x0400287E RID: 10366
		public uint OutRequests;

		// Token: 0x0400287F RID: 10367
		public uint RoutingDiscards;

		// Token: 0x04002880 RID: 10368
		public uint OutDiscards;

		// Token: 0x04002881 RID: 10369
		public uint OutNoRoutes;

		// Token: 0x04002882 RID: 10370
		public uint ReasmTimeout;

		// Token: 0x04002883 RID: 10371
		public uint ReasmReqds;

		// Token: 0x04002884 RID: 10372
		public uint ReasmOks;

		// Token: 0x04002885 RID: 10373
		public uint ReasmFails;

		// Token: 0x04002886 RID: 10374
		public uint FragOks;

		// Token: 0x04002887 RID: 10375
		public uint FragFails;

		// Token: 0x04002888 RID: 10376
		public uint FragCreates;

		// Token: 0x04002889 RID: 10377
		public int NumIf;

		// Token: 0x0400288A RID: 10378
		public int NumAddr;

		// Token: 0x0400288B RID: 10379
		public int NumRoutes;
	}
}
