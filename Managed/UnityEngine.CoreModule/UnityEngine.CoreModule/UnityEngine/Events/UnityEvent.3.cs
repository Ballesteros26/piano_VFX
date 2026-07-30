using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	public class UnityEvent<T0, T1> : UnityEventBase
	{
		// Token: 0x060019B7 RID: 6583 RVA: 0x00029EB6 File Offset: 0x000280B6
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00029EC7 File Offset: 0x000280C7
		public void AddListener(UnityAction<T0, T1> call)
		{
			base.AddCall(UnityEvent<T0, T1>.GetDelegate(call));
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00029C63 File Offset: 0x00027E63
		public void RemoveListener(UnityAction<T0, T1> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00029ED8 File Offset: 0x000280D8
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00029F14 File Offset: 0x00028114
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00029F30 File Offset: 0x00028130
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1> action)
		{
			return new InvokableCall<T0, T1>(action);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00029F48 File Offset: 0x00028148
		public void Invoke(T0 arg0, T1 arg1)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1> invokableCall = list[i] as InvokableCall<T0, T1>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1);
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
							this.m_InvokeArray = new object[2];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040007EB RID: 2027
		private object[] m_InvokeArray = null;
	}
}
