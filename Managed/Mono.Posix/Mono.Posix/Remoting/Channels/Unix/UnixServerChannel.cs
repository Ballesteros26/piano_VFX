using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008F RID: 143
	public class UnixServerChannel : IChannelReceiver, IChannel
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		private void Init(IServerChannelSinkProvider serverSinkProvider)
		{
			if (serverSinkProvider == null)
			{
				serverSinkProvider = new UnixBinaryServerFormatterSinkProvider();
			}
			this.channel_data = new ChannelDataStore(null);
			for (IServerChannelSinkProvider serverChannelSinkProvider = serverSinkProvider; serverChannelSinkProvider != null; serverChannelSinkProvider = serverChannelSinkProvider.Next)
			{
				serverChannelSinkProvider.GetChannelData(this.channel_data);
			}
			IServerChannelSink serverChannelSink = ChannelServices.CreateServerChannelSinkChain(serverSinkProvider, this);
			this.sink = new UnixServerTransportSink(serverChannelSink);
			this.StartListening(null);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public UnixServerChannel(string path)
		{
			this.path = path;
			this.Init(null);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000F948 File Offset: 0x0000DB48
		public UnixServerChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)
		{
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (!(text == "path"))
				{
					if (!(text == "priority"))
					{
						if (text == "supressChannelData")
						{
							this.supressChannelData = Convert.ToBoolean(dictionaryEntry.Value);
						}
					}
					else
					{
						this.priority = Convert.ToInt32(dictionaryEntry.Value);
					}
				}
				else
				{
					this.path = dictionaryEntry.Value as string;
				}
			}
			this.Init(serverSinkProvider);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public UnixServerChannel(string name, string path, IServerChannelSinkProvider serverSinkProvider)
		{
			this.name = name;
			this.path = path;
			this.Init(serverSinkProvider);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public UnixServerChannel(string name, string path)
		{
			this.name = name;
			this.path = path;
			this.Init(null);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000FAD9 File Offset: 0x0000DCD9
		public object ChannelData
		{
			get
			{
				if (this.supressChannelData)
				{
					return null;
				}
				return this.channel_data;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000FAFB File Offset: 0x0000DCFB
		public string GetChannelUri()
		{
			return "unix://" + this.path;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000FB10 File Offset: 0x0000DD10
		public string[] GetUrlsForUri(string uri)
		{
			if (!uri.StartsWith("/"))
			{
				uri = "/" + uri;
			}
			string[] channelUris = this.channel_data.ChannelUris;
			string[] array = new string[channelUris.Length];
			for (int i = 0; i < channelUris.Length; i++)
			{
				array[i] = channelUris[i] + "?" + uri;
			}
			return array;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000FB78 File Offset: 0x0000DD78
		private void WaitForConnections()
		{
			try
			{
				for (;;)
				{
					Socket socket = this.listener.AcceptSocket();
					this.CreateListenerConnection(socket);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		internal void CreateListenerConnection(Socket client)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				if (this._activeConnections.Count >= this._maxConcurrentConnections)
				{
					Monitor.Wait(this._activeConnections);
				}
				if (this.server_thread != null)
				{
					Thread thread = new Thread(new ThreadStart(new ClientConnection(this, client, this.sink).ProcessMessages));
					thread.Start();
					thread.IsBackground = true;
					this._activeConnections.Add(thread);
				}
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		internal void ReleaseConnection(Thread thread)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this._activeConnections.Remove(thread);
				Monitor.Pulse(this._activeConnections);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
		public void StartListening(object data)
		{
			this.listener = new UnixListener(this.path);
			Syscall.chmod(this.path, FilePermissions.DEFFILEMODE);
			if (this.server_thread == null)
			{
				this.listener.Start();
				string[] array = new string[1];
				array = new string[] { this.GetChannelUri() };
				this.channel_data.ChannelUris = array;
				this.server_thread = new Thread(new ThreadStart(this.WaitForConnections));
				this.server_thread.IsBackground = true;
				this.server_thread.Start();
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000FD34 File Offset: 0x0000DF34
		public void StopListening(object data)
		{
			if (this.server_thread == null)
			{
				return;
			}
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this.server_thread.Abort();
				this.server_thread = null;
				this.listener.Stop();
				foreach (object obj in this._activeConnections)
				{
					((Thread)obj).Abort();
				}
				this._activeConnections.Clear();
				Monitor.PulseAll(this._activeConnections);
			}
		}

		// Token: 0x040004CA RID: 1226
		private string path;

		// Token: 0x040004CB RID: 1227
		private string name = "unix";

		// Token: 0x040004CC RID: 1228
		private int priority = 1;

		// Token: 0x040004CD RID: 1229
		private bool supressChannelData;

		// Token: 0x040004CE RID: 1230
		private Thread server_thread;

		// Token: 0x040004CF RID: 1231
		private UnixListener listener;

		// Token: 0x040004D0 RID: 1232
		private UnixServerTransportSink sink;

		// Token: 0x040004D1 RID: 1233
		private ChannelDataStore channel_data;

		// Token: 0x040004D2 RID: 1234
		private int _maxConcurrentConnections = 100;

		// Token: 0x040004D3 RID: 1235
		private ArrayList _activeConnections = new ArrayList();
	}
}
