using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000082 RID: 130
	internal class UnixBinaryServerFormatterSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public UnixBinaryServerFormatterSink(IServerChannelSink nextSink, IChannelReceiver receiver)
		{
			this.next_sink = nextSink;
			this.receiver = receiver;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000E6CD File Offset: 0x0000C8CD
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0000E6D5 File Offset: 0x0000C8D5
		internal UnixBinaryCore BinaryCore
		{
			get
			{
				return this._binaryCore;
			}
			set
			{
				this._binaryCore = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage message, ITransportHeaders headers, Stream stream)
		{
			ITransportHeaders transportHeaders = new TransportHeaders();
			if (sinkStack != null)
			{
				stream = sinkStack.GetResponseStream(message, transportHeaders);
			}
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, message, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			sinkStack.AsyncProcessResponse(message, transportHeaders, stream);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000E747 File Offset: 0x0000C947
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000E74C File Offset: 0x0000C94C
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			sinkStack.Push(this, null);
			ServerProcessing serverProcessing;
			try
			{
				string text = (string)requestHeaders["__RequestUri"];
				string text2;
				this.receiver.Parse(text, out text2);
				if (text2 == null)
				{
					text2 = text;
				}
				MethodCallHeaderHandler methodCallHeaderHandler = new MethodCallHeaderHandler(text2);
				requestMsg = (IMessage)this._binaryCore.Deserializer.Deserialize(requestStream, new HeaderHandler(methodCallHeaderHandler.HandleHeaders));
				serverProcessing = this.next_sink.ProcessMessage(sinkStack, requestMsg, requestHeaders, null, out responseMsg, out responseHeaders, out responseStream);
			}
			catch (Exception ex)
			{
				responseMsg = new ReturnMessage(ex, (IMethodCallMessage)requestMsg);
				serverProcessing = ServerProcessing.Complete;
				responseHeaders = null;
				responseStream = null;
			}
			if (serverProcessing == ServerProcessing.Complete)
			{
				for (int i = 0; i < 3; i++)
				{
					responseStream = null;
					responseHeaders = new TransportHeaders();
					if (sinkStack != null)
					{
						responseStream = sinkStack.GetResponseStream(responseMsg, responseHeaders);
					}
					if (responseStream == null)
					{
						responseStream = new MemoryStream();
					}
					try
					{
						this._binaryCore.Serializer.Serialize(responseStream, responseMsg);
						break;
					}
					catch (Exception ex2)
					{
						if (i == 2)
						{
							throw ex2;
						}
						responseMsg = new ReturnMessage(ex2, (IMethodCallMessage)requestMsg);
					}
				}
				if (responseStream is MemoryStream)
				{
					responseStream.Position = 0L;
				}
				sinkStack.Pop(this);
			}
			return serverProcessing;
		}

		// Token: 0x040004A5 RID: 1189
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x040004A6 RID: 1190
		private IServerChannelSink next_sink;

		// Token: 0x040004A7 RID: 1191
		private IChannelReceiver receiver;
	}
}
