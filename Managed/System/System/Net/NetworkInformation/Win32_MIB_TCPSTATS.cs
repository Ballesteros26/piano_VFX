using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000671 RID: 1649
	internal struct Win32_MIB_TCPSTATS
	{
		// Token: 0x04002972 RID: 10610
		public uint RtoAlgorithm;

		// Token: 0x04002973 RID: 10611
		public uint RtoMin;

		// Token: 0x04002974 RID: 10612
		public uint RtoMax;

		// Token: 0x04002975 RID: 10613
		public uint MaxConn;

		// Token: 0x04002976 RID: 10614
		public uint ActiveOpens;

		// Token: 0x04002977 RID: 10615
		public uint PassiveOpens;

		// Token: 0x04002978 RID: 10616
		public uint AttemptFails;

		// Token: 0x04002979 RID: 10617
		public uint EstabResets;

		// Token: 0x0400297A RID: 10618
		public uint CurrEstab;

		// Token: 0x0400297B RID: 10619
		public uint InSegs;

		// Token: 0x0400297C RID: 10620
		public uint OutSegs;

		// Token: 0x0400297D RID: 10621
		public uint RetransSegs;

		// Token: 0x0400297E RID: 10622
		public uint InErrs;

		// Token: 0x0400297F RID: 10623
		public uint OutRsts;

		// Token: 0x04002980 RID: 10624
		public uint NumConns;
	}
}
