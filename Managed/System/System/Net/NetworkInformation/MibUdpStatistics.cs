using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000672 RID: 1650
	internal class MibUdpStatistics : UdpStatistics
	{
		// Token: 0x06003473 RID: 13427 RVA: 0x000C34BD File Offset: 0x000C16BD
		public MibUdpStatistics(StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000C34CC File Offset: 0x000C16CC
		private long Get(string name)
		{
			if (this.dic[name] == null)
			{
				return 0L;
			}
			return long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000C34F5 File Offset: 0x000C16F5
		public override long DatagramsReceived
		{
			get
			{
				return this.Get("InDatagrams");
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000C3502 File Offset: 0x000C1702
		public override long DatagramsSent
		{
			get
			{
				return this.Get("OutDatagrams");
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000C350F File Offset: 0x000C170F
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return this.Get("NoPorts");
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000C351C File Offset: 0x000C171C
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000C3529 File Offset: 0x000C1729
		public override int UdpListeners
		{
			get
			{
				return (int)this.Get("NumAddrs");
			}
		}

		// Token: 0x04002981 RID: 10625
		private StringDictionary dic;
	}
}
