using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075F RID: 1887
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x06004E13 RID: 19987 RVA: 0x0011A34A File Offset: 0x0011854A
		public ClientActivatedIdentity(string objectUri, Type objectType)
			: base(objectUri, null, objectType)
		{
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x0011A355 File Offset: 0x00118555
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x0011A35D File Offset: 0x0011855D
		public MarshalByRefObject GetClientProxy()
		{
			return this._targetThis;
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x0011A365 File Offset: 0x00118565
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x0011A36E File Offset: 0x0011856E
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x0011A37C File Offset: 0x0011857C
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x0011A3CC File Offset: 0x001185CC
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain(flag ? this._targetThis : this._serverObject, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x040029D4 RID: 10708
		private MarshalByRefObject _targetThis;
	}
}
