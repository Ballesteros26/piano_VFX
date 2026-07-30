using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007E RID: 126
	internal class UnixBinaryClientFormatterSink : IClientFormatterSink, IMessageSink, IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0000E223 File Offset: 0x0000C423
		public UnixBinaryClientFormatterSink(IClientChannelSink nextSink)
		{
			this._nextInChain = nextSink;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000E23D File Offset: 0x0000C43D
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000E245 File Offset: 0x0000C445
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000E24E File Offset: 0x0000C44E
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return this._nextInChain;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000E256 File Offset: 0x0000C456
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000E259 File Offset: 0x0000C459
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000E25C File Offset: 0x0000C45C
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException("UnixBinaryClientFormatterSink must be the first sink in the IClientChannelSink chain");
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000E268 File Offset: 0x0000C468
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			IMessage message = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(stream, null, (IMethodCallMessage)state);
			sinkStack.DispatchReplyMessage(message);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000E29B File Offset: 0x0000C49B
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000E2A2 File Offset: 0x0000C4A2
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ITransportHeaders transportHeaders = new TransportHeaders();
			Stream stream = this._nextInChain.GetRequestStream(msg, transportHeaders);
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, msg, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			ClientChannelSinkStack clientChannelSinkStack = new ClientChannelSinkStack(replySink);
			clientChannelSinkStack.Push(this, msg);
			this._nextInChain.AsyncProcessRequest(clientChannelSinkStack, msg, transportHeaders, stream);
			return null;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000E318 File Offset: 0x0000C518
		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message;
			try
			{
				ITransportHeaders transportHeaders = new TransportHeaders();
				transportHeaders["__RequestUri"] = ((IMethodCallMessage)msg).Uri;
				transportHeaders["Content-Type"] = "application/octet-stream";
				Stream stream = this._nextInChain.GetRequestStream(msg, transportHeaders);
				if (stream == null)
				{
					stream = new MemoryStream();
				}
				this._binaryCore.Serializer.Serialize(stream, msg, null);
				if (stream is MemoryStream)
				{
					stream.Position = 0L;
				}
				ITransportHeaders transportHeaders2;
				Stream stream2;
				this._nextInChain.ProcessMessage(msg, transportHeaders, stream, out transportHeaders2, out stream2);
				message = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(stream2, null, (IMethodCallMessage)msg);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			return message;
		}

		// Token: 0x04000499 RID: 1177
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x0400049A RID: 1178
		private IClientChannelSink _nextInChain;
	}
}
