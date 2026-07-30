using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000641 RID: 1601
	internal class MibIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000C026E File Offset: 0x000BE46E
		public MibIcmpV4Statistics(StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000C027D File Offset: 0x000BE47D
		private long Get(string name)
		{
			if (this.dic[name] == null)
			{
				return 0L;
			}
			return long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000C02A6 File Offset: 0x000BE4A6
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return this.Get("InAddrMaskReps");
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000C02B3 File Offset: 0x000BE4B3
		public override long AddressMaskRepliesSent
		{
			get
			{
				return this.Get("OutAddrMaskReps");
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000C02C0 File Offset: 0x000BE4C0
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return this.Get("InAddrMasks");
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000C02CD File Offset: 0x000BE4CD
		public override long AddressMaskRequestsSent
		{
			get
			{
				return this.Get("OutAddrMasks");
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000C02DA File Offset: 0x000BE4DA
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000C02E7 File Offset: 0x000BE4E7
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000C02F4 File Offset: 0x000BE4F4
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReps");
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000C0301 File Offset: 0x000BE501
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReps");
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000C030E File Offset: 0x000BE50E
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000C031B File Offset: 0x000BE51B
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000C0328 File Offset: 0x000BE528
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000C0335 File Offset: 0x000BE535
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000C0342 File Offset: 0x000BE542
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x000C034F File Offset: 0x000BE54F
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x000C035C File Offset: 0x000BE55C
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProbs");
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000C0369 File Offset: 0x000BE569
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProbs");
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x000C0376 File Offset: 0x000BE576
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000C0383 File Offset: 0x000BE583
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000C0390 File Offset: 0x000BE590
		public override long SourceQuenchesReceived
		{
			get
			{
				return this.Get("InSrcQuenchs");
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000C039D File Offset: 0x000BE59D
		public override long SourceQuenchesSent
		{
			get
			{
				return this.Get("OutSrcQuenchs");
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000C03AA File Offset: 0x000BE5AA
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000C03B7 File Offset: 0x000BE5B7
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003323 RID: 13091 RVA: 0x000C03C4 File Offset: 0x000BE5C4
		public override long TimestampRepliesReceived
		{
			get
			{
				return this.Get("InTimestampReps");
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000C03D1 File Offset: 0x000BE5D1
		public override long TimestampRepliesSent
		{
			get
			{
				return this.Get("OutTimestampReps");
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003325 RID: 13093 RVA: 0x000C03DE File Offset: 0x000BE5DE
		public override long TimestampRequestsReceived
		{
			get
			{
				return this.Get("InTimestamps");
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x000C03EB File Offset: 0x000BE5EB
		public override long TimestampRequestsSent
		{
			get
			{
				return this.Get("OutTimestamps");
			}
		}

		// Token: 0x040028A2 RID: 10402
		private StringDictionary dic;
	}
}
