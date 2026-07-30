using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000628 RID: 1576
	internal class UnixIPGlobalProperties : CommonUnixIPGlobalProperties
	{
		// Token: 0x0600322C RID: 12844 RVA: 0x00004239 File Offset: 0x00002439
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00004239 File Offset: 0x00002439
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00004239 File Offset: 0x00002439
		public override IPEndPoint[] GetActiveUdpListeners()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x00004239 File Offset: 0x00002439
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00004239 File Offset: 0x00002439
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x00004239 File Offset: 0x00002439
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00004239 File Offset: 0x00002439
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00004239 File Offset: 0x00002439
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x00004239 File Offset: 0x00002439
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x00004239 File Offset: 0x00002439
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00004239 File Offset: 0x00002439
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			throw new NotImplementedException();
		}
	}
}
