using System;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000825 RID: 2085
	internal class ServerContextTerminatorSink : IMessageSink
	{
		// Token: 0x0600536A RID: 21354 RVA: 0x001255C7 File Offset: 0x001237C7
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (msg is IConstructionCallMessage)
			{
				return ActivationServices.CreateInstanceFromMessage((IConstructionCallMessage)msg);
			}
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).SyncObjectProcessMessage(msg);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x001255EE File Offset: 0x001237EE
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			return ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).AsyncObjectProcessMessage(msg, replySink);
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}
	}
}
