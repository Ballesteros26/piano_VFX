using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000255 RID: 597
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x00028F44 File Offset: 0x00027144
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument)
			: base(target, theFunction)
		{
			this.m_Arg1 = argument;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00028F57 File Offset: 0x00027157
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00028F57 File Offset: 0x00027157
		public override void Invoke(T arg0)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x040007D6 RID: 2006
		private readonly T m_Arg1;
	}
}
