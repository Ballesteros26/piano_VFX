using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000670 RID: 1648
	internal class Win32TcpStatistics : TcpStatistics
	{
		// Token: 0x06003464 RID: 13412 RVA: 0x000C33EA File Offset: 0x000C15EA
		public Win32TcpStatistics(Win32_MIB_TCPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000C33F9 File Offset: 0x000C15F9
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.info.PassiveOpens);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000C3407 File Offset: 0x000C1607
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.info.ActiveOpens);
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000C3415 File Offset: 0x000C1615
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.info.NumConns);
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000C3423 File Offset: 0x000C1623
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.info.CurrEstab);
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000C3431 File Offset: 0x000C1631
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.info.InErrs);
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000C343F File Offset: 0x000C163F
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.info.AttemptFails);
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000C344D File Offset: 0x000C164D
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.info.MaxConn);
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000C345B File Offset: 0x000C165B
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMax);
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000C3469 File Offset: 0x000C1669
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMin);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000C3477 File Offset: 0x000C1677
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.info.EstabResets);
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000C3485 File Offset: 0x000C1685
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.info.OutRsts);
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000C3493 File Offset: 0x000C1693
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.info.InSegs);
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000C34A1 File Offset: 0x000C16A1
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.info.RetransSegs);
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000C34AF File Offset: 0x000C16AF
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.info.OutSegs);
			}
		}

		// Token: 0x04002971 RID: 10609
		private Win32_MIB_TCPSTATS info;
	}
}
