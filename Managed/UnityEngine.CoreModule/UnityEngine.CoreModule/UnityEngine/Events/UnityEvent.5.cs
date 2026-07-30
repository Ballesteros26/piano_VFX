using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000264 RID: 612
	[Serializable]
	public class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		// Token: 0x060019CD RID: 6605 RVA: 0x0002A18B File Offset: 0x0002838B
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0002A19C File Offset: 0x0002839C
		public void AddListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2, T3>.GetDelegate(call));
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00029C63 File Offset: 0x00027E63
		public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0002A1AC File Offset: 0x000283AC
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0002A200 File Offset: 0x00028400
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0002A21C File Offset: 0x0002841C
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2, T3> action)
		{
			return new InvokableCall<T0, T1, T2, T3>(action);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0002A234 File Offset: 0x00028434
		public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2, T3> invokableCall = list[i] as InvokableCall<T0, T1, T2, T3>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1, arg2, arg3);
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
							this.m_InvokeArray = new object[4];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						this.m_InvokeArray[2] = arg2;
						this.m_InvokeArray[3] = arg3;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040007ED RID: 2029
		private object[] m_InvokeArray = null;
	}
}
