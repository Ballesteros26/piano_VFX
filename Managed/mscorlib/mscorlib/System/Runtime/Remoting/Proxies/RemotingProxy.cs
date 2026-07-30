using System;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000770 RID: 1904
	internal class RemotingProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06004E93 RID: 20115 RVA: 0x0011BC92 File Offset: 0x00119E92
		internal RemotingProxy(Type type, ClientIdentity identity)
			: base(type, identity)
		{
			this._sink = identity.ChannelSink;
			this._hasEnvoySink = false;
			this._targetUri = identity.TargetUri;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0011BCBB File Offset: 0x00119EBB
		internal RemotingProxy(Type type, string activationUrl, object[] activationAttributes)
			: base(type)
		{
			this._hasEnvoySink = false;
			this._ctorCall = ActivationServices.CreateConstructionCall(type, activationUrl, activationAttributes);
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0011BCDC File Offset: 0x00119EDC
		public override IMessage Invoke(IMessage request)
		{
			IMethodCallMessage methodCallMessage = request as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetHashCodeMethod)
				{
					return new MethodResponse(base.ObjectIdentity.GetHashCode(), null, null, methodCallMessage);
				}
				if (methodCallMessage.MethodBase == RemotingProxy._cache_GetTypeMethod)
				{
					return new MethodResponse(base.GetProxiedType(), null, null, methodCallMessage);
				}
			}
			IInternalMessage internalMessage = request as IInternalMessage;
			if (internalMessage != null)
			{
				if (internalMessage.Uri == null)
				{
					internalMessage.Uri = this._targetUri;
				}
				internalMessage.TargetIdentity = this._objectIdentity;
			}
			this._objectIdentity.NotifyClientDynamicSinks(true, request, true, false);
			IMessageSink messageSink;
			if (Thread.CurrentContext.HasExitSinks && !this._hasEnvoySink)
			{
				messageSink = Thread.CurrentContext.GetClientContextSinkChain();
			}
			else
			{
				messageSink = this._sink;
			}
			MonoMethodMessage monoMethodMessage = request as MonoMethodMessage;
			IMessage message;
			if (monoMethodMessage == null || monoMethodMessage.CallType == CallType.Sync)
			{
				message = messageSink.SyncProcessMessage(request);
			}
			else
			{
				AsyncResult asyncResult = monoMethodMessage.AsyncResult;
				IMessageCtrl messageCtrl = messageSink.AsyncProcessMessage(request, asyncResult);
				if (asyncResult != null)
				{
					asyncResult.SetMessageCtrl(messageCtrl);
				}
				message = new ReturnMessage(null, new object[0], 0, null, monoMethodMessage);
			}
			this._objectIdentity.NotifyClientDynamicSinks(false, request, true, false);
			return message;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0011BE04 File Offset: 0x0011A004
		internal void AttachIdentity(Identity identity)
		{
			this._objectIdentity = identity;
			if (identity is ClientActivatedIdentity)
			{
				ClientActivatedIdentity clientActivatedIdentity = (ClientActivatedIdentity)identity;
				this._targetContext = clientActivatedIdentity.Context;
				base.AttachServer(clientActivatedIdentity.GetServerObject());
				clientActivatedIdentity.SetClientProxy((MarshalByRefObject)this.GetTransparentProxy());
			}
			if (identity is ClientIdentity)
			{
				((ClientIdentity)identity).ClientProxy = (MarshalByRefObject)this.GetTransparentProxy();
				this._targetUri = ((ClientIdentity)identity).TargetUri;
			}
			else
			{
				this._targetUri = identity.ObjectUri;
			}
			if (this._objectIdentity.EnvoySink != null)
			{
				this._sink = this._objectIdentity.EnvoySink;
				this._hasEnvoySink = true;
			}
			else
			{
				this._sink = this._objectIdentity.ChannelSink;
			}
			this._ctorCall = null;
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0011BECC File Offset: 0x0011A0CC
		internal IMessage ActivateRemoteObject(IMethodMessage request)
		{
			if (this._ctorCall == null)
			{
				return new ConstructionResponse(this, null, (IMethodCallMessage)request);
			}
			this._ctorCall.CopyFrom(request);
			return ActivationServices.Activate(this, this._ctorCall);
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x0011BEFC File Offset: 0x0011A0FC
		// (set) Token: 0x06004E99 RID: 20121 RVA: 0x00014B5A File Offset: 0x00012D5A
		public string TypeName
		{
			get
			{
				if (this._objectIdentity is ClientIdentity)
				{
					ObjRef objRef = this._objectIdentity.CreateObjRef(null);
					if (objRef.TypeInfo != null)
					{
						return objRef.TypeInfo.TypeName;
					}
				}
				return base.GetProxiedType().AssemblyQualifiedName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0011BF44 File Offset: 0x0011A144
		public bool CanCastTo(Type fromType, object o)
		{
			if (this._objectIdentity is ClientIdentity)
			{
				ObjRef objRef = this._objectIdentity.CreateObjRef(null);
				if (objRef.IsReferenceToWellKnow && (fromType.IsInterface || base.GetProxiedType() == typeof(MarshalByRefObject)))
				{
					return true;
				}
				if (objRef.TypeInfo != null)
				{
					return objRef.TypeInfo.CanCastTo(fromType, o);
				}
			}
			return fromType.IsAssignableFrom(base.GetProxiedType());
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0011BFB8 File Offset: 0x0011A1B8
		~RemotingProxy()
		{
			if (this._objectIdentity != null && !(this._objectIdentity is ClientActivatedIdentity))
			{
				RemotingServices.DisposeIdentity(this._objectIdentity);
			}
		}

		// Token: 0x040029F8 RID: 10744
		private static MethodInfo _cache_GetTypeMethod = typeof(object).GetMethod("GetType");

		// Token: 0x040029F9 RID: 10745
		private static MethodInfo _cache_GetHashCodeMethod = typeof(object).GetMethod("GetHashCode");

		// Token: 0x040029FA RID: 10746
		private IMessageSink _sink;

		// Token: 0x040029FB RID: 10747
		private bool _hasEnvoySink;

		// Token: 0x040029FC RID: 10748
		private ConstructionCall _ctorCall;
	}
}
