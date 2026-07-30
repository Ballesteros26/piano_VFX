using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000761 RID: 1889
	internal class SingleCallIdentity : ServerIdentity
	{
		// Token: 0x06004E1E RID: 19998 RVA: 0x0011A41B File Offset: 0x0011861B
		public SingleCallIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri, context, objectType)
		{
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x0011A524 File Offset: 0x00118724
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			if (marshalByRefObject.ObjectIdentity == null)
			{
				marshalByRefObject.ObjectIdentity = this;
			}
			IMessage message = this._context.CreateServerObjectSinkChain(marshalByRefObject, false).SyncProcessMessage(msg);
			if (marshalByRefObject is IDisposable)
			{
				((IDisposable)marshalByRefObject).Dispose();
			}
			return message;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x0011A578 File Offset: 0x00118778
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
			IMessageSink messageSink = this._context.CreateServerObjectSinkChain(marshalByRefObject, false);
			if (marshalByRefObject is IDisposable)
			{
				replySink = new DisposerReplySink(replySink, (IDisposable)marshalByRefObject);
			}
			return messageSink.AsyncProcessMessage(msg, replySink);
		}
	}
}
