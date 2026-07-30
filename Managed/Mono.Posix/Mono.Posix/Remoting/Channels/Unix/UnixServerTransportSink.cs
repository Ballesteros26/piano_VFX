using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000091 RID: 145
	internal class UnixServerTransportSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		public UnixServerTransportSink(IServerChannelSink next)
		{
			this.next_sink = next;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000FF02 File Offset: 0x0000E102
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0000FF0A File Offset: 0x0000E10A
		public IDictionary Properties
		{
			get
			{
				if (this.next_sink != null)
				{
					return this.next_sink.Properties;
				}
				return null;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000FF24 File Offset: 0x0000E124
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream responseStream)
		{
			ClientConnection clientConnection = (ClientConnection)state;
			NetworkStream networkStream = new NetworkStream(clientConnection.Client);
			UnixMessageIO.SendMessageStream(networkStream, responseStream, headers, clientConnection.Buffer);
			networkStream.Flush();
			networkStream.Close();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000FF5E File Offset: 0x0000E15E
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000FF61 File Offset: 0x0000E161
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000FF68 File Offset: 0x0000E168
		internal void InternalProcessMessage(ClientConnection connection, Stream stream)
		{
			ITransportHeaders transportHeaders;
			Stream stream2 = UnixMessageIO.ReceiveMessageStream(stream, out transportHeaders, connection.Buffer);
			ServerChannelSinkStack serverChannelSinkStack = new ServerChannelSinkStack();
			serverChannelSinkStack.Push(this, connection);
			IMessage message;
			ITransportHeaders transportHeaders2;
			Stream stream3;
			ServerProcessing serverProcessing = this.next_sink.ProcessMessage(serverChannelSinkStack, null, transportHeaders, stream2, out message, out transportHeaders2, out stream3);
			if (serverProcessing != ServerProcessing.Complete)
			{
				int num = serverProcessing - ServerProcessing.OneWay;
				return;
			}
			UnixMessageIO.SendMessageStream(stream, stream3, transportHeaders2, connection.Buffer);
			stream.Flush();
		}

		// Token: 0x040004D9 RID: 1241
		private IServerChannelSink next_sink;
	}
}
