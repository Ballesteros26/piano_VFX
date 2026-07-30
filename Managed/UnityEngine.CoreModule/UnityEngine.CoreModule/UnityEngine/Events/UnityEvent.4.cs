using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine.Events
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x0002A011 File Offset: 0x00028211
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0002A022 File Offset: 0x00028222
		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2>.GetDelegate(call));
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00029C63 File Offset: 0x00027E63
		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0002A034 File Offset: 0x00028234
		protected override MethodInfo FindMethod_Impl(string name, Type targetObjType)
		{
			return UnityEventBase.GetValidMethodInfo(targetObjType, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0002A07C File Offset: 0x0002827C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0002A098 File Offset: 0x00028298
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0002A0B0 File Offset: 0x000282B0
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			List<BaseInvokableCall> list = base.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2> invokableCall = list[i] as InvokableCall<T0, T1, T2>;
				bool flag = invokableCall != null;
				if (flag)
				{
					invokableCall.Invoke(arg0, arg1, arg2);
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
							this.m_InvokeArray = new object[3];
						}
						this.m_InvokeArray[0] = arg0;
						this.m_InvokeArray[1] = arg1;
						this.m_InvokeArray[2] = arg2;
						baseInvokableCall.Invoke(this.m_InvokeArray);
					}
				}
			}
		}

		// Token: 0x040007EC RID: 2028
		private object[] m_InvokeArray = null;
	}
}
