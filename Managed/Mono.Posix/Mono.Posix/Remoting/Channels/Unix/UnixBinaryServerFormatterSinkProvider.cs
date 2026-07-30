using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000084 RID: 132
	internal class UnixBinaryServerFormatterSinkProvider : IServerFormatterSinkProvider, IServerChannelSinkProvider
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0000E8AF File Offset: 0x0000CAAF
		public UnixBinaryServerFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000E8C2 File Offset: 0x0000CAC2
		public UnixBinaryServerFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryServerFormatterSinkProvider.AllowedProperties);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000E8DC File Offset: 0x0000CADC
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public IServerChannelSinkProvider Next
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

		// Token: 0x0600068D RID: 1677 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			IServerChannelSink serverChannelSink = null;
			if (this.next != null)
			{
				serverChannelSink = this.next.CreateSink(channel);
			}
			return new UnixBinaryServerFormatterSink(serverChannelSink, channel)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000E927 File Offset: 0x0000CB27
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x040004A9 RID: 1193
		private IServerChannelSinkProvider next;

		// Token: 0x040004AA RID: 1194
		private UnixBinaryCore _binaryCore;

		// Token: 0x040004AB RID: 1195
		internal static string[] AllowedProperties = new string[] { "includeVersions", "strictBinding" };
	}
}
