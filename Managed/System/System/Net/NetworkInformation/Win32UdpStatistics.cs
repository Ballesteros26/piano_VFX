using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000673 RID: 1651
	internal class Win32UdpStatistics : UdpStatistics
	{
		// Token: 0x0600347A RID: 13434 RVA: 0x000C3537 File Offset: 0x000C1737
		public Win32UdpStatistics(Win32_MIB_UDPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000C3546 File Offset: 0x000C1746
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.info.InDatagrams);
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000C3554 File Offset: 0x000C1754
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.info.OutDatagrams);
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000C3562 File Offset: 0x000C1762
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.NoPorts);
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000C3570 File Offset: 0x000C1770
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.info.InErrors);
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000C357E File Offset: 0x000C177E
		public override int UdpListeners
		{
			get
			{
				return this.info.NumAddrs;
			}
		}

		// Token: 0x04002982 RID: 10626
		private Win32_MIB_UDPSTATS info;
	}
}
