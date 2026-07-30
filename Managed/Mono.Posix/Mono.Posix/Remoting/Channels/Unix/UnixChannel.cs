using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000085 RID: 133
	public class UnixChannel : IChannelReceiver, IChannel, IChannelSender
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x0000E946 File Offset: 0x0000CB46
		public UnixChannel()
			: this(null)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000E950 File Offset: 0x0000CB50
		public UnixChannel(string path)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = path;
			this.Init(hashtable, null, null);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000E990 File Offset: 0x0000CB90
		private void Init(IDictionary properties, IClientChannelSinkProvider clientSink, IServerChannelSinkProvider serverSink)
		{
			this._clientChannel = new UnixClientChannel(properties, clientSink);
			if (properties["path"] != null)
			{
				this._serverChannel = new UnixServerChannel(properties, serverSink);
			}
			object obj = properties["name"];
			if (obj != null)
			{
				this._name = obj as string;
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this._priority = Convert.ToInt32(obj);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000E9FA File Offset: 0x0000CBFA
		public UnixChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			this.Init(properties, clientSinkProvider, serverSinkProvider);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000EA1D File Offset: 0x0000CC1D
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			return this._clientChannel.CreateMessageSink(url, remoteChannelData, out objectURI);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000EA2D File Offset: 0x0000CC2D
		public string ChannelName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000EA35 File Offset: 0x0000CC35
		public int ChannelPriority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000EA3D File Offset: 0x0000CC3D
		public void StartListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StartListening(data);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000EA53 File Offset: 0x0000CC53
		public void StopListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StopListening(data);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000EA69 File Offset: 0x0000CC69
		public string[] GetUrlsForUri(string uri)
		{
			if (this._serverChannel != null)
			{
				return this._serverChannel.GetUrlsForUri(uri);
			}
			return null;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public object ChannelData
		{
			get
			{
				if (this._serverChannel != null)
				{
					return this._serverChannel.ChannelData;
				}
				return null;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000EA98 File Offset: 0x0000CC98
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		internal static string ParseUnixURL(string url, out string objectURI)
		{
			objectURI = null;
			if (!url.StartsWith("unix://"))
			{
				return null;
			}
			int num = url.IndexOf('?');
			if (num == -1)
			{
				return url.Substring(7);
			}
			objectURI = url.Substring(num + 1);
			if (objectURI.Length == 0)
			{
				objectURI = null;
			}
			return url.Substring(7, num - 7);
		}

		// Token: 0x040004AC RID: 1196
		private UnixClientChannel _clientChannel;

		// Token: 0x040004AD RID: 1197
		private UnixServerChannel _serverChannel;

		// Token: 0x040004AE RID: 1198
		private string _name;

		// Token: 0x040004AF RID: 1199
		private int _priority;
	}
}
