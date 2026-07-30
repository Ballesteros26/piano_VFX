using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000254 RID: 596
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001952 RID: 6482 RVA: 0x00028DB0 File Offset: 0x00026FB0
		// (remove) Token: 0x06001953 RID: 6483 RVA: 0x00028DE8 File Offset: 0x00026FE8
		[field: DebuggerBrowsable(0)]
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		// Token: 0x06001954 RID: 6484 RVA: 0x00028E1D File Offset: 0x0002701D
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00028E45 File Offset: 0x00027045
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate += action;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00028E58 File Offset: 0x00027058
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00028EE0 File Offset: 0x000270E0
		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00028F10 File Offset: 0x00027110
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
