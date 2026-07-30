using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000090 RID: 144
	internal class ClientConnection
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public ClientConnection(UnixServerChannel serverChannel, Socket client, UnixServerTransportSink sink)
		{
			this._serverChannel = serverChannel;
			this._client = client;
			this._sink = sink;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000FE1D File Offset: 0x0000E01D
		public Socket Client
		{
			get
			{
				return this._client;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000FE25 File Offset: 0x0000E025
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000FE30 File Offset: 0x0000E030
		public void ProcessMessages()
		{
			byte[] array = new byte[256];
			this._stream = new BufferedStream(new NetworkStream(this._client));
			try
			{
				bool flag = false;
				while (!flag)
				{
					MessageStatus messageStatus = UnixMessageIO.ReceiveMessageStatus(this._stream, array);
					if (messageStatus != MessageStatus.MethodMessage)
					{
						if (messageStatus == MessageStatus.CancelSignal || messageStatus == MessageStatus.Unknown)
						{
							flag = true;
						}
					}
					else
					{
						this._sink.InternalProcessMessage(this, this._stream);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					this._serverChannel.ReleaseConnection(Thread.CurrentThread);
					this._stream.Close();
					this._client.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public bool IsLocal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040004D4 RID: 1236
		private Socket _client;

		// Token: 0x040004D5 RID: 1237
		private UnixServerTransportSink _sink;

		// Token: 0x040004D6 RID: 1238
		private Stream _stream;

		// Token: 0x040004D7 RID: 1239
		private UnixServerChannel _serverChannel;

		// Token: 0x040004D8 RID: 1240
		private byte[] _buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
	}
}
