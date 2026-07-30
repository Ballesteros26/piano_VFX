using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000253 RID: 595
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600194B RID: 6475 RVA: 0x00028C34 File Offset: 0x00026E34
		// (remove) Token: 0x0600194C RID: 6476 RVA: 0x00028C6C File Offset: 0x00026E6C
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x0600194D RID: 6477 RVA: 0x00028CA1 File Offset: 0x00026EA1
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00028CC9 File Offset: 0x00026EC9
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00028CDC File Offset: 0x00026EDC
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00028D50 File Offset: 0x00026F50
		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00028D7C File Offset: 0x00026F7C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
