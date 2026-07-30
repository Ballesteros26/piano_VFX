using System;
using System.Net.Sockets;
using Mono.Unix;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008A RID: 138
	internal class ReusableUnixClient : UnixClient
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		public ReusableUnixClient(string path)
			: base(path)
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000F0A9 File Offset: 0x0000D2A9
		public bool IsAlive
		{
			get
			{
				return !base.Client.Poll(0, SelectMode.SelectRead);
			}
		}
	}
}
