using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020007BA RID: 1978
	internal class ActivationServices
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x0011F0A0 File Offset: 0x0011D2A0
		private static IActivator ConstructionActivator
		{
			get
			{
				if (ActivationServices._constructionActivator == null)
				{
					ActivationServices._constructionActivator = new ConstructionLevelActivator();
				}
				return ActivationServices._constructionActivator;
			}
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0011F0B8 File Offset: 0x0011D2B8
		public static IMessage Activate(RemotingProxy proxy, ConstructionCall ctorCall)
		{
			ctorCall.SourceProxy = proxy;
			IMessage message;
			if (Thread.CurrentContext.HasExitSinks && !ctorCall.IsContextOk)
			{
				message = Thread.CurrentContext.GetClientContextSinkChain().SyncProcessMessage(ctorCall);
			}
			else
			{
				message = ActivationServices.RemoteActivate(ctorCall);
			}
			if (message is IConstructionReturnMessage && ((IConstructionReturnMessage)message).Exception == null && proxy.ObjectIdentity == null)
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(ctorCall);
				proxy.AttachIdentity(messageTargetIdentity);
			}
			return message;
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0011F128 File Offset: 0x0011D328
		public static IMessage RemoteActivate(IConstructionCallMessage ctorCall)
		{
			IMessage message;
			try
			{
				message = ctorCall.Activator.Activate(ctorCall);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, ctorCall);
			}
			return message;
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0011F160 File Offset: 0x0011D360
		public static object CreateProxyFromAttributes(Type type, object[] activationAttributes)
		{
			string text = null;
			foreach (object obj in activationAttributes)
			{
				if (!(obj is IContextAttribute))
				{
					throw new RemotingException("Activation attribute does not implement the IContextAttribute interface");
				}
				if (obj is UrlAttribute)
				{
					text = ((UrlAttribute)obj).UrlValue;
				}
			}
			if (text != null)
			{
				return RemotingServices.CreateClientProxy(type, text, activationAttributes);
			}
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, activationAttributes);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, activationAttributes);
			}
			return null;
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0011F1E0 File Offset: 0x0011D3E0
		public static ConstructionCall CreateConstructionCall(Type type, string activationUrl, object[] activationAttributes)
		{
			ConstructionCall constructionCall = new ConstructionCall(type);
			if (!type.IsContextful)
			{
				constructionCall.Activator = new AppDomainLevelActivator(activationUrl, ActivationServices.ConstructionActivator);
				constructionCall.IsContextOk = false;
				return constructionCall;
			}
			IActivator activator = ActivationServices.ConstructionActivator;
			activator = new ContextLevelActivator(activator);
			ArrayList arrayList = new ArrayList();
			if (activationAttributes != null)
			{
				arrayList.AddRange(activationAttributes);
			}
			bool flag = activationUrl == ChannelServices.CrossContextUrl;
			Context currentContext = Thread.CurrentContext;
			if (flag)
			{
				using (IEnumerator enumerator = arrayList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((IContextAttribute)enumerator.Current).IsContextOK(currentContext, constructionCall))
						{
							flag = false;
							break;
						}
					}
				}
			}
			foreach (object obj in type.GetCustomAttributes(true))
			{
				if (obj is IContextAttribute)
				{
					flag = flag && ((IContextAttribute)obj).IsContextOK(currentContext, constructionCall);
					arrayList.Add(obj);
				}
			}
			if (!flag)
			{
				constructionCall.SetActivationAttributes(arrayList.ToArray());
				foreach (object obj2 in arrayList)
				{
					((IContextAttribute)obj2).GetPropertiesForNewContext(constructionCall);
				}
			}
			if (activationUrl != ChannelServices.CrossContextUrl)
			{
				activator = new AppDomainLevelActivator(activationUrl, activator);
			}
			constructionCall.Activator = activator;
			constructionCall.IsContextOk = flag;
			return constructionCall;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0011F364 File Offset: 0x0011D564
		public static IMessage CreateInstanceFromMessage(IConstructionCallMessage ctorCall)
		{
			object obj = ActivationServices.AllocateUninitializedClassInstance(ctorCall.ActivationType);
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(ctorCall);
			serverIdentity.AttachServerObject((MarshalByRefObject)obj, Thread.CurrentContext);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (ctorCall.ActivationType.IsContextful && constructionCall != null && constructionCall.SourceProxy != null)
			{
				constructionCall.SourceProxy.AttachIdentity(serverIdentity);
				RemotingServices.InternalExecuteMessage((MarshalByRefObject)constructionCall.SourceProxy.GetTransparentProxy(), ctorCall);
			}
			else
			{
				ctorCall.MethodBase.Invoke(obj, ctorCall.Args);
			}
			return new ConstructionResponse(obj, null, ctorCall);
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0011F3FC File Offset: 0x0011D5FC
		public static object CreateProxyForType(Type type)
		{
			ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfiguration.IsRemotelyActivatedClientType(type);
			if (activatedClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(activatedClientTypeEntry, null);
			}
			WellKnownClientTypeEntry wellKnownClientTypeEntry = RemotingConfiguration.IsWellKnownClientType(type);
			if (wellKnownClientTypeEntry != null)
			{
				return RemotingServices.CreateClientProxy(wellKnownClientTypeEntry);
			}
			if (type.IsContextful)
			{
				return RemotingServices.CreateClientProxyForContextBound(type, null);
			}
			if (type.IsCOMObject)
			{
				return RemotingServices.CreateClientProxyForComInterop(type);
			}
			return null;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00002194 File Offset: 0x00000394
		internal static void PushActivationAttributes(Type serverType, object[] attributes)
		{
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00002194 File Offset: 0x00000394
		internal static void PopActivationAttributes(Type serverType)
		{
		}

		// Token: 0x0600501C RID: 20508
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object AllocateUninitializedClassInstance(Type type);

		// Token: 0x0600501D RID: 20509
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableProxyActivation(Type type, bool enable);

		// Token: 0x04002A68 RID: 10856
		private static IActivator _constructionActivator;
	}
}
