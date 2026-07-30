using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000252 RID: 594
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001944 RID: 6468 RVA: 0x00028AC8 File Offset: 0x00026CC8
		// (remove) Token: 0x06001945 RID: 6469 RVA: 0x00028B00 File Offset: 0x00026D00
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2> Delegate;

		// Token: 0x06001946 RID: 6470 RVA: 0x00028B35 File Offset: 0x00026D35
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00028B5D File Offset: 0x00026D5D
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00028B70 File Offset: 0x00026D70
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00028BD4 File Offset: 0x00026DD4
		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00028C00 File Offset: 0x00026E00
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
