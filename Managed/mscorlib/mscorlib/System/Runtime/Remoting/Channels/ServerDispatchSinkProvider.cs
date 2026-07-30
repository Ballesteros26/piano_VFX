using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007B6 RID: 1974
	internal class ServerDispatchSinkProvider : IServerFormatterSinkProvider, IServerChannelSinkProvider
	{
		// Token: 0x06005005 RID: 20485 RVA: 0x00002111 File Offset: 0x00000311
		public ServerDispatchSinkProvider()
		{
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00002111 File Offset: 0x00000311
		public ServerDispatchSinkProvider(IDictionary properties, ICollection providerData)
		{
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06005007 RID: 20487 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x06005008 RID: 20488 RVA: 0x00014B5A File Offset: 0x00012D5A
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0011F015 File Offset: 0x0011D215
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new ServerDispatchSink();
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00002194 File Offset: 0x00000394
		public void GetChannelData(IChannelDataStore channelData)
		{
		}
	}
}
