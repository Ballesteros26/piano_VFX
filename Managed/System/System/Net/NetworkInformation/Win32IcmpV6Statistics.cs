using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000647 RID: 1607
	internal class Win32IcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06003365 RID: 13157 RVA: 0x000C075C File Offset: 0x000BE95C
		public Win32IcmpV6Statistics(Win32_MIB_ICMP_EX info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000C077C File Offset: 0x000BE97C
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[1]);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000C078C File Offset: 0x000BE98C
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[1]);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000C079C File Offset: 0x000BE99C
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[129]);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000C07B0 File Offset: 0x000BE9B0
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[129]);
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000C07C4 File Offset: 0x000BE9C4
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[128]);
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000C07D8 File Offset: 0x000BE9D8
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[128]);
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600336C RID: 13164 RVA: 0x000C07EC File Offset: 0x000BE9EC
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000C07FA File Offset: 0x000BE9FA
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000C0808 File Offset: 0x000BEA08
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[130]);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000C081C File Offset: 0x000BEA1C
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[130]);
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000C0830 File Offset: 0x000BEA30
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[132]);
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000C0844 File Offset: 0x000BEA44
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[132]);
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003372 RID: 13170 RVA: 0x000C0858 File Offset: 0x000BEA58
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[131]);
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000C086C File Offset: 0x000BEA6C
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[131]);
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000C0880 File Offset: 0x000BEA80
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000C088E File Offset: 0x000BEA8E
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000C089C File Offset: 0x000BEA9C
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[136]);
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000C08B0 File Offset: 0x000BEAB0
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[136]);
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000C08C4 File Offset: 0x000BEAC4
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[135]);
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000C08D8 File Offset: 0x000BEAD8
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[135]);
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000C08EC File Offset: 0x000BEAEC
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[2]);
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000C08FC File Offset: 0x000BEAFC
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[2]);
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000C090C File Offset: 0x000BEB0C
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[4]);
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000C091C File Offset: 0x000BEB1C
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[4]);
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000C092C File Offset: 0x000BEB2C
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[137]);
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000C0940 File Offset: 0x000BEB40
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[137]);
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x000C0954 File Offset: 0x000BEB54
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[134]);
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000C0968 File Offset: 0x000BEB68
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[134]);
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003382 RID: 13186 RVA: 0x000C097C File Offset: 0x000BEB7C
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[133]);
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000C0990 File Offset: 0x000BEB90
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[133]);
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000C09A4 File Offset: 0x000BEBA4
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[3]);
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000C09B4 File Offset: 0x000BEBB4
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[3]);
			}
		}

		// Token: 0x040028C4 RID: 10436
		private Win32_MIBICMPSTATS_EX iin;

		// Token: 0x040028C5 RID: 10437
		private Win32_MIBICMPSTATS_EX iout;
	}
}
