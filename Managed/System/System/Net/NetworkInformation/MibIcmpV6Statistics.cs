using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000645 RID: 1605
	internal class MibIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06003342 RID: 13122 RVA: 0x000C0584 File Offset: 0x000BE784
		public MibIcmpV6Statistics(StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000C0593 File Offset: 0x000BE793
		private long Get(string name)
		{
			if (this.dic[name] == null)
			{
				return 0L;
			}
			return long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000C05BC File Offset: 0x000BE7BC
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000C05C9 File Offset: 0x000BE7C9
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000C05D6 File Offset: 0x000BE7D6
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReplies");
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000C05E3 File Offset: 0x000BE7E3
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReplies");
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000C05F0 File Offset: 0x000BE7F0
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000C05FD File Offset: 0x000BE7FD
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000C060A File Offset: 0x000BE80A
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000C0617 File Offset: 0x000BE817
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000C0624 File Offset: 0x000BE824
		public override long MembershipQueriesReceived
		{
			get
			{
				return this.Get("InGroupMembQueries");
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000C0631 File Offset: 0x000BE831
		public override long MembershipQueriesSent
		{
			get
			{
				return this.Get("OutGroupMembQueries");
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000C063E File Offset: 0x000BE83E
		public override long MembershipReductionsReceived
		{
			get
			{
				return this.Get("InGroupMembReductiions");
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000C064B File Offset: 0x000BE84B
		public override long MembershipReductionsSent
		{
			get
			{
				return this.Get("OutGroupMembReductiions");
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x000C0658 File Offset: 0x000BE858
		public override long MembershipReportsReceived
		{
			get
			{
				return this.Get("InGroupMembRespons");
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000C0665 File Offset: 0x000BE865
		public override long MembershipReportsSent
		{
			get
			{
				return this.Get("OutGroupMembRespons");
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000C0672 File Offset: 0x000BE872
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000C067F File Offset: 0x000BE87F
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000C068C File Offset: 0x000BE88C
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return this.Get("InNeighborAdvertisements");
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000C0699 File Offset: 0x000BE899
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return this.Get("OutNeighborAdvertisements");
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000C06A6 File Offset: 0x000BE8A6
		public override long NeighborSolicitsReceived
		{
			get
			{
				return this.Get("InNeighborSolicits");
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000C06B3 File Offset: 0x000BE8B3
		public override long NeighborSolicitsSent
		{
			get
			{
				return this.Get("OutNeighborSolicits");
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000C06C0 File Offset: 0x000BE8C0
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return this.Get("InPktTooBigs");
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000C06CD File Offset: 0x000BE8CD
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return this.Get("OutPktTooBigs");
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000C06DA File Offset: 0x000BE8DA
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProblems");
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000C06E7 File Offset: 0x000BE8E7
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProblems");
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000C06F4 File Offset: 0x000BE8F4
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000C0701 File Offset: 0x000BE901
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x000C070E File Offset: 0x000BE90E
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return this.Get("InRouterAdvertisements");
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000C071B File Offset: 0x000BE91B
		public override long RouterAdvertisementsSent
		{
			get
			{
				return this.Get("OutRouterAdvertisements");
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000C0728 File Offset: 0x000BE928
		public override long RouterSolicitsReceived
		{
			get
			{
				return this.Get("InRouterSolicits");
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000C0735 File Offset: 0x000BE935
		public override long RouterSolicitsSent
		{
			get
			{
				return this.Get("OutRouterSolicits");
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000C0742 File Offset: 0x000BE942
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000C074F File Offset: 0x000BE94F
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x040028B4 RID: 10420
		private StringDictionary dic;
	}
}
