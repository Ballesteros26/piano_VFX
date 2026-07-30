using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000251 RID: 593
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600193D RID: 6461 RVA: 0x0002896C File Offset: 0x00026B6C
		// (remove) Token: 0x0600193E RID: 6462 RVA: 0x000289A4 File Offset: 0x00026BA4
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1> Delegate;

		// Token: 0x0600193F RID: 6463 RVA: 0x000289D9 File Offset: 0x00026BD9
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00028A02 File Offset: 0x00026C02
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00028A14 File Offset: 0x00026C14
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00028A68 File Offset: 0x00026C68
		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00028A94 File Offset: 0x00026C94
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
