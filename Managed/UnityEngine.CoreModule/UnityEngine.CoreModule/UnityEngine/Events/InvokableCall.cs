using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000250 RID: 592
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001936 RID: 6454 RVA: 0x00028838 File Offset: 0x00026A38
		// (remove) Token: 0x06001937 RID: 6455 RVA: 0x00028870 File Offset: 0x00026A70
		[field: DebuggerBrowsable(0)]
		private event UnityAction Delegate;

		// Token: 0x06001938 RID: 6456 RVA: 0x000288A5 File Offset: 0x00026AA5
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction)global::System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000288CE File Offset: 0x00026ACE
		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000288E0 File Offset: 0x00026AE0
		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0002890C File Offset: 0x00026B0C
		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00028938 File Offset: 0x00026B38
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
