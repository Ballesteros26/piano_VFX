using System;
using System.Reflection;

namespace System.Web
{
	// Token: 0x020000C8 RID: 200
	internal sealed class NoParamsInvoker
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001CE68 File Offset: 0x0001B068
		public NoParamsInvoker(object o, MethodInfo method)
		{
			if (method.IsStatic)
			{
				this.real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), method);
			}
			else
			{
				this.real = (NoParamsDelegate)Delegate.CreateDelegate(typeof(NoParamsDelegate), o, method);
			}
			this.faked = new EventHandler(this.InvokeNoParams);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001CECE File Offset: 0x0001B0CE
		private void InvokeNoParams(object o, EventArgs args)
		{
			this.real();
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001CEDB File Offset: 0x0001B0DB
		public EventHandler FakeDelegate
		{
			get
			{
				return this.faked;
			}
		}

		// Token: 0x0400106F RID: 4207
		private EventHandler faked;

		// Token: 0x04001070 RID: 4208
		private NoParamsDelegate real;
	}
}
