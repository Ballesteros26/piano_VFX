using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000087 RID: 135
	internal class UnixClientTransportSink : IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public UnixClientTransportSink(string url)
		{
			string text;
			this._path = UnixChannel.ParseUnixURL(url, out text);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000ECD5 File Offset: 0x0000CED5
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream requestStream)
		{
			UnixConnection unixConnection = null;
			bool flag = RemotingServices.IsOneWay(((IMethodMessage)msg).MethodBase);
			try
			{
				if (headers == null)
				{
					headers = new TransportHeaders();
				}
				headers["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, headers, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				if (!flag)
				{
					sinkStack.Push(this, unixConnection);
					ThreadPool.QueueUserWorkItem(delegate(object data)
					{
						try
						{
							this.ReadAsyncUnixMessage(data);
						}
						catch
						{
						}
					}, sinkStack);
				}
				else
				{
					unixConnection.Release();
				}
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				if (!flag)
				{
					throw;
				}
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000ED90 File Offset: 0x0000CF90
		private void ReadAsyncUnixMessage(object data)
		{
			IClientChannelSinkStack clientChannelSinkStack = (IClientChannelSinkStack)data;
			UnixConnection unixConnection = (UnixConnection)clientChannelSinkStack.Pop(this);
			try
			{
				if (UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer) != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				ITransportHeaders transportHeaders;
				Stream stream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out transportHeaders, unixConnection.Buffer);
				unixConnection.Release();
				unixConnection = null;
				clientChannelSinkStack.AsyncProcessResponse(transportHeaders, stream);
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				throw;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000EE14 File Offset: 0x0000D014
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000EE1B File Offset: 0x0000D01B
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000EE20 File Offset: 0x0000D020
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			UnixConnection unixConnection = null;
			try
			{
				if (requestHeaders == null)
				{
					requestHeaders = new TransportHeaders();
				}
				requestHeaders["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, requestHeaders, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				if (UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer) != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				responseStream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out responseHeaders, unixConnection.Buffer);
			}
			finally
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
			}
		}

		// Token: 0x040004B3 RID: 1203
		private string _path;
	}
}
