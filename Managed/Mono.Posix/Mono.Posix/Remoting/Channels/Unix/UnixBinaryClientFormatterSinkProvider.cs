using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007F RID: 127
	internal class UnixBinaryClientFormatterSinkProvider : IClientFormatterSinkProvider, IClientChannelSinkProvider
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		public UnixBinaryClientFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		public UnixBinaryClientFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryClientFormatterSinkProvider.allowedProperties);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000E40D File Offset: 0x0000C60D
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0000E415 File Offset: 0x0000C615
		public IClientChannelSinkProvider Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000E420 File Offset: 0x0000C620
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			IClientChannelSink clientChannelSink = null;
			if (this.next != null)
			{
				clientChannelSink = this.next.CreateSink(channel, url, remoteChannelData);
			}
			return new UnixBinaryClientFormatterSink(clientChannelSink)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x0400049B RID: 1179
		private IClientChannelSinkProvider next;

		// Token: 0x0400049C RID: 1180
		private UnixBinaryCore _binaryCore;

		// Token: 0x0400049D RID: 1181
		private static string[] allowedProperties = new string[] { "includeVersions", "strictBinding" };
	}
}
