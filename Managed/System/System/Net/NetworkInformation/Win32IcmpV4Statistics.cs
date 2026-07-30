using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000642 RID: 1602
	internal class Win32IcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06003327 RID: 13095 RVA: 0x000C03F8 File Offset: 0x000BE5F8
		public Win32IcmpV4Statistics(Win32_MIBICMPINFO info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x000C0418 File Offset: 0x000BE618
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMaskReps);
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000C0426 File Offset: 0x000BE626
		public override long AddressMaskRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMaskReps);
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000C0434 File Offset: 0x000BE634
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.AddrMasks);
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000C0442 File Offset: 0x000BE642
		public override long AddressMaskRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.AddrMasks);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000C0450 File Offset: 0x000BE650
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.DestUnreachs);
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000C045E File Offset: 0x000BE65E
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.DestUnreachs);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000C046C File Offset: 0x000BE66C
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.EchoReps);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C047A File Offset: 0x000BE67A
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.EchoReps);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000C0488 File Offset: 0x000BE688
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Echos);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000C0496 File Offset: 0x000BE696
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Echos);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C04A4 File Offset: 0x000BE6A4
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000C04B2 File Offset: 0x000BE6B2
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000C04C0 File Offset: 0x000BE6C0
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000C04CE File Offset: 0x000BE6CE
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000C04DC File Offset: 0x000BE6DC
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.ParmProbs);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x000C04EA File Offset: 0x000BE6EA
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.ParmProbs);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000C04F8 File Offset: 0x000BE6F8
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Redirects);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000C0506 File Offset: 0x000BE706
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Redirects);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000C0514 File Offset: 0x000BE714
		public override long SourceQuenchesReceived
		{
			get
			{
				return (long)((ulong)this.iin.SrcQuenchs);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000C0522 File Offset: 0x000BE722
		public override long SourceQuenchesSent
		{
			get
			{
				return (long)((ulong)this.iout.SrcQuenchs);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000C0530 File Offset: 0x000BE730
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimeExcds);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000C053E File Offset: 0x000BE73E
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimeExcds);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000C054C File Offset: 0x000BE74C
		public override long TimestampRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.TimestampReps);
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000C055A File Offset: 0x000BE75A
		public override long TimestampRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.TimestampReps);
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000C0568 File Offset: 0x000BE768
		public override long TimestampRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Timestamps);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000C0576 File Offset: 0x000BE776
		public override long TimestampRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Timestamps);
			}
		}

		// Token: 0x040028A3 RID: 10403
		private Win32_MIBICMPSTATS iin;

		// Token: 0x040028A4 RID: 10404
		private Win32_MIBICMPSTATS iout;
	}
}
