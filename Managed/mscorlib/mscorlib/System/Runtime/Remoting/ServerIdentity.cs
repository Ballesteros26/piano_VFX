using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x0200075E RID: 1886
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x06004E07 RID: 19975 RVA: 0x0011A1A6 File Offset: 0x001183A6
		public ServerIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x0011A1BD File Offset: 0x001183BD
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x0011A1C5 File Offset: 0x001183C5
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x0011A1F9 File Offset: 0x001183F9
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x0011A204 File Offset: 0x00118404
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x0011A29C File Offset: 0x0011849C
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
					return;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x0011A302 File Offset: 0x00118502
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x0011A30A File Offset: 0x0011850A
		// (set) Token: 0x06004E0F RID: 19983 RVA: 0x0011A312 File Offset: 0x00118512
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x06004E10 RID: 19984
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x06004E11 RID: 19985
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x06004E12 RID: 19986 RVA: 0x0011A31B File Offset: 0x0011851B
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				object serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x040029CF RID: 10703
		protected Type _objectType;

		// Token: 0x040029D0 RID: 10704
		protected MarshalByRefObject _serverObject;

		// Token: 0x040029D1 RID: 10705
		protected IMessageSink _serverSink;

		// Token: 0x040029D2 RID: 10706
		protected Context _context;

		// Token: 0x040029D3 RID: 10707
		protected Lease _lease;
	}
}
