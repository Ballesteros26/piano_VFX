using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000828 RID: 2088
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06005376 RID: 21366 RVA: 0x001256E4 File Offset: 0x001238E4
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0012570A File Offset: 0x0012390A
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0012573C File Offset: 0x0012393C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] array = new object[] { msg, replySink };
			ThreadPool.QueueUserWorkItem(delegate(object data)
			{
				try
				{
					this.ExecuteAsyncMessage(data);
				}
				catch
				{
				}
			}, array);
			return null;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0012576C File Offset: 0x0012396C
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage message;
			if (this._rp != null)
			{
				message = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				message = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(message);
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x0000A42E File Offset: 0x0000862E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x001257C4 File Offset: 0x001239C4
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Cannot cast argument ",
						parameterInfo.Position,
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x04002B65 RID: 11109
		private MarshalByRefObject _target;

		// Token: 0x04002B66 RID: 11110
		private RealProxy _rp;
	}
}
