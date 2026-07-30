using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000086 RID: 134
	public class UnixClientChannel : IChannelSender, IChannel
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0000EAF9 File Offset: 0x0000CCF9
		public UnixClientChannel()
		{
			this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
			this._sinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public UnixClientChannel(IDictionary properties, IClientChannelSinkProvider sinkProvider)
		{
			object obj = properties["name"];
			if (obj != null)
			{
				this.name = obj as string;
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this.priority = Convert.ToInt32(obj);
			}
			if (sinkProvider != null)
			{
				this._sinkProvider = sinkProvider;
				IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
				while (clientChannelSinkProvider.Next != null)
				{
					clientChannelSinkProvider = clientChannelSinkProvider.Next;
				}
				clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
				return;
			}
			this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
			this._sinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public UnixClientChannel(string name, IClientChannelSinkProvider sinkProvider)
		{
			this.name = name;
			this._sinkProvider = sinkProvider;
			IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
			while (clientChannelSinkProvider.Next != null)
			{
				clientChannelSinkProvider = clientChannelSinkProvider.Next;
			}
			clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000EC21 File Offset: 0x0000CE21
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000EC29 File Offset: 0x0000CE29
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			if (url != null && this.Parse(url, out objectURI) != null)
			{
				return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
			}
			if (remoteChannelData != null)
			{
				IChannelDataStore channelDataStore = remoteChannelData as IChannelDataStore;
				if (channelDataStore == null || channelDataStore.ChannelUris.Length == 0)
				{
					objectURI = null;
					return null;
				}
				url = channelDataStore.ChannelUris[0];
			}
			if (this.Parse(url, out objectURI) == null)
			{
				return null;
			}
			return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x040004B0 RID: 1200
		private int priority = 1;

		// Token: 0x040004B1 RID: 1201
		private string name = "unix";

		// Token: 0x040004B2 RID: 1202
		private IClientChannelSinkProvider _sinkProvider;
	}
}
