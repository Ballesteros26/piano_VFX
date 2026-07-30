using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x02000760 RID: 1888
	internal class SingletonIdentity : ServerIdentity
	{
		// Token: 0x06004E1A RID: 19994 RVA: 0x0011A41B File Offset: 0x0011861B
		public SingletonIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri, context, objectType)
		{
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x0011A428 File Offset: 0x00118628
		public MarshalByRefObject GetServerObject()
		{
			if (this._serverObject != null)
			{
				return this._serverObject;
			}
			lock (this)
			{
				if (this._serverObject == null)
				{
					MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._objectType, true);
					base.AttachServerObject(marshalByRefObject, Context.DefaultContext);
					base.StartTrackingLifetime((ILease)marshalByRefObject.InitializeLifetimeService());
				}
			}
			return this._serverObject;
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x0011A4AC File Offset: 0x001186AC
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			MarshalByRefObject serverObject = this.GetServerObject();
			if (this._serverSink == null)
			{
				this._serverSink = this._context.CreateServerObjectSinkChain(serverObject, false);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x0011A4E8 File Offset: 0x001186E8
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			MarshalByRefObject serverObject = this.GetServerObject();
			if (this._serverSink == null)
			{
				this._serverSink = this._context.CreateServerObjectSinkChain(serverObject, false);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}
	}
}
