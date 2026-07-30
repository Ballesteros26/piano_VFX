using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020007B5 RID: 1973
	internal class ServerDispatchSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x0000A42E File Offset: 0x0000862E
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x0000A42E File Offset: 0x0000862E
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0000A42E File Offset: 0x0000862E
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0011F002 File Offset: 0x0011D202
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}
	}
}
