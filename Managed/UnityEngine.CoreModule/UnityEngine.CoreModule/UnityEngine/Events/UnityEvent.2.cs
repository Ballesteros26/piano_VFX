using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	public class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x060019AC RID: 6572 RVA: 0x00029D78 File Offset: 0x00027F78
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00029D89 File Offset: 0x00027F89
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00029C63 File Offset: 0x00027E63
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00029D9C File Offset: 0x00027F9C
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[] { typeof(T0) });
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00029DC8 File Offset: 0x00027FC8
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00029DE4 File Offset: 0x00027FE4
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00029DFC File Offset: 0x00027FFC
		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0> invokableCall = list[i] as InvokableCall<T0>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0);
				}
				else
				{
					InvokableCall invokableCall2 = list[i] as InvokableCall;
					bool flag2 = invokableCall2 != null;
					if (flag2)
					{
						invokableCall2.Invoke();
					}
					else
					{
						BaseInvokableCall baseInvokableCall = list[i];
						bool flag3 = this.m_InvokeArray == null;
						if (flag3)
						{
							this.m_InvokeArray = new object[1];
						}
						this.m_InvokeArray[0] = arg0;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040007EA RID: 2026
		private object[] m_InvokeArray = null;
	}
}
