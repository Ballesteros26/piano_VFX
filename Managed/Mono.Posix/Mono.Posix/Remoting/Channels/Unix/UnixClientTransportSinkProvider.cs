using System;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000088 RID: 136
	internal class UnixClientTransportSinkProvider : IClientChannelSinkProvider
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000EF00 File Offset: 0x0000D100
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0000EF03 File Offset: 0x0000D103
		public IClientChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000EF05 File Offset: 0x0000D105
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			return new UnixClientTransportSink(url);
		}
	}
}
