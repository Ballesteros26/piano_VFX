using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x0200025C RID: 604
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x060019A1 RID: 6561 RVA: 0x00029C42 File Offset: 0x00027E42
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00029C53 File Offset: 0x00027E53
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00029C63 File Offset: 0x00027E63
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00029C7C File Offset: 0x00027E7C
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[0]);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00029C9C File Offset: 0x00027E9C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00029CB8 File Offset: 0x00027EB8
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00029CD0 File Offset: 0x00027ED0
		public void Invoke()
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall invokableCall = list[i] as InvokableCall;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke();
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
							this.m_InvokeArray = new object[0];
						}
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040007E9 RID: 2025
		private object[] m_InvokeArray = null;
	}
}
