using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200061C RID: 1564
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x060031E5 RID: 12773 RVA: 0x000BE440 File Offset: 0x000BC640
		public SystemTcpConnectionInformation(IPEndPoint local, IPEndPoint remote, TcpState state)
		{
			this.localEndPoint = local;
			this.remoteEndPoint = remote;
			this.state = state;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000BE45D File Offset: 0x000BC65D
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000BE465 File Offset: 0x000BC665
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000BE46D File Offset: 0x000BC66D
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x04002820 RID: 10272
		private IPEndPoint localEndPoint;

		// Token: 0x04002821 RID: 10273
		private IPEndPoint remoteEndPoint;

		// Token: 0x04002822 RID: 10274
		private TcpState state;
	}
}
