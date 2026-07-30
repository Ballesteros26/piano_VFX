using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200079D RID: 1949
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x06004FB5 RID: 20405 RVA: 0x0011EB83 File Offset: 0x0011CD83
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0011EB94 File Offset: 0x0011CD94
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink crossAppDomainSink;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					crossAppDomainSink = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink2 = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink2;
					crossAppDomainSink = crossAppDomainSink2;
				}
			}
			return crossAppDomainSink;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x0011EC18 File Offset: 0x0011CE18
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0011EC20 File Offset: 0x0011CE20
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes processMessageRes = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out processMessageRes.arrResponse, out processMessageRes.cadMrm);
			}
			catch (Exception ex)
			{
				IMessage message = new MethodResponse(ex, new ErrorMessage());
				processMessageRes.arrResponse = CADSerializer.SerializeMessage(message).GetBuffer();
			}
			return processMessageRes;
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0011EC84 File Offset: 0x0011CE84
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage message = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage cadmethodReturnMessage = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					array2 = CADSerializer.SerializeMessage(msgRequest).GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[] { array2, cadmethodCallMessage });
					array = processMessageRes.arrResponse;
					cadmethodReturnMessage = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					message = CADSerializer.DeserializeMessage(new MemoryStream(array), msgRequest as IMethodCallMessage);
				}
				else
				{
					message = new MethodResponse(msgRequest as IMethodCallMessage, cadmethodReturnMessage);
				}
			}
			catch (Exception ex)
			{
				try
				{
					message = new ReturnMessage(ex, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return message;
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0011ED5C File Offset: 0x0011CF5C
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest asyncRequest = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.SendAsyncMessage(data);
				}
				catch
				{
				}
			}, asyncRequest);
			return null;
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0011ED88 File Offset: 0x0011CF88
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage message = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(message);
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002A58 RID: 10840
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x04002A59 RID: 10841
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04002A5A RID: 10842
		private int _domainID;

		// Token: 0x0200079E RID: 1950
		private struct ProcessMessageRes
		{
			// Token: 0x04002A5B RID: 10843
			public byte[] arrResponse;

			// Token: 0x04002A5C RID: 10844
			public CADMethodReturnMessage cadMrm;
		}
	}
}
