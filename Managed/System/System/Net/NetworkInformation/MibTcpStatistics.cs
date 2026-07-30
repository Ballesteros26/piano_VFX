using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200066F RID: 1647
	internal class MibTcpStatistics : TcpStatistics
	{
		// Token: 0x06003454 RID: 13396 RVA: 0x000C32FC File Offset: 0x000C14FC
		public MibTcpStatistics(StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000C330B File Offset: 0x000C150B
		private long Get(string name)
		{
			if (this.dic[name] == null)
			{
				return 0L;
			}
			return long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000C3334 File Offset: 0x000C1534
		public override long ConnectionsAccepted
		{
			get
			{
				return this.Get("PassiveOpens");
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000C3341 File Offset: 0x000C1541
		public override long ConnectionsInitiated
		{
			get
			{
				return this.Get("ActiveOpens");
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000C334E File Offset: 0x000C154E
		public override long CumulativeConnections
		{
			get
			{
				return this.Get("NumConns");
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000C335B File Offset: 0x000C155B
		public override long CurrentConnections
		{
			get
			{
				return this.Get("CurrEstab");
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000C3368 File Offset: 0x000C1568
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrs");
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000C3375 File Offset: 0x000C1575
		public override long FailedConnectionAttempts
		{
			get
			{
				return this.Get("AttemptFails");
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000C3382 File Offset: 0x000C1582
		public override long MaximumConnections
		{
			get
			{
				return this.Get("MaxConn");
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000C338F File Offset: 0x000C158F
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMax");
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000C339C File Offset: 0x000C159C
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMin");
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000C33A9 File Offset: 0x000C15A9
		public override long ResetConnections
		{
			get
			{
				return this.Get("EstabResets");
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000C33B6 File Offset: 0x000C15B6
		public override long ResetsSent
		{
			get
			{
				return this.Get("OutRsts");
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000C33C3 File Offset: 0x000C15C3
		public override long SegmentsReceived
		{
			get
			{
				return this.Get("InSegs");
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000C33D0 File Offset: 0x000C15D0
		public override long SegmentsResent
		{
			get
			{
				return this.Get("RetransSegs");
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000C33DD File Offset: 0x000C15DD
		public override long SegmentsSent
		{
			get
			{
				return this.Get("OutSegs");
			}
		}

		// Token: 0x04002970 RID: 10608
		private StringDictionary dic;
	}
}
