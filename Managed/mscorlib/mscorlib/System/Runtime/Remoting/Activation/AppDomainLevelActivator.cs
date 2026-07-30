using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007BC RID: 1980
	internal class AppDomainLevelActivator : IActivator
	{
		// Token: 0x0600501F RID: 20511 RVA: 0x0011F44C File Offset: 0x0011D64C
		public AppDomainLevelActivator(string activationUrl, IActivator next)
		{
			this._activationUrl = activationUrl;
			this._next = next;
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06005020 RID: 20512 RVA: 0x00059020 File Offset: 0x00057220
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.AppDomain;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x0011F462 File Offset: 0x0011D662
		// (set) Token: 0x06005022 RID: 20514 RVA: 0x0011F46A File Offset: 0x0011D66A
		public IActivator NextActivator
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0011F474 File Offset: 0x0011D674
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			IActivator activator = (IActivator)RemotingServices.Connect(typeof(IActivator), this._activationUrl);
			ctorCall.Activator = ctorCall.Activator.NextActivator;
			IConstructionReturnMessage constructionReturnMessage;
			try
			{
				constructionReturnMessage = activator.Activate(ctorCall);
			}
			catch (Exception ex)
			{
				return new ConstructionResponse(ex, ctorCall);
			}
			ObjRef objRef = (ObjRef)constructionReturnMessage.ReturnValue;
			if (RemotingServices.GetIdentityForUri(objRef.URI) != null)
			{
				throw new RemotingException("Inconsistent state during activation; there may be two proxies for the same object");
			}
			object obj;
			Identity orCreateClientIdentity = RemotingServices.GetOrCreateClientIdentity(objRef, null, out obj);
			RemotingServices.SetMessageTargetIdentity(ctorCall, orCreateClientIdentity);
			return constructionReturnMessage;
		}

		// Token: 0x04002A6F RID: 10863
		private string _activationUrl;

		// Token: 0x04002A70 RID: 10864
		private IActivator _next;
	}
}
