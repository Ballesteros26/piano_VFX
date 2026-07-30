using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200025A RID: 602
	[UsedByNativeCode]
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x000297B8 File Offset: 0x000279B8
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00002EC3 File Offset: 0x000010C3
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000297DF File Offset: 0x000279DF
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000297EC File Offset: 0x000279EC
		protected MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return this.FindMethod_Impl(name, targetObj.GetType());
		}

		// Token: 0x0600198A RID: 6538
		protected abstract MethodInfo FindMethod_Impl(string name, Type targetObjType);

		// Token: 0x0600198B RID: 6539
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x0600198C RID: 6540 RVA: 0x0002980C File Offset: 0x00027A0C
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type type2 = ((call.target != null) ? call.target.GetType() : Type.GetType(call.targetAssemblyTypeName, false));
			return this.FindMethod(call.methodName, type2, call.mode, type);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0002989C File Offset: 0x00027A9C
		internal MethodInfo FindMethod(string name, Type listenerType, PersistentListenerMode mode, Type argumentType)
		{
			MethodInfo methodInfo;
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				methodInfo = this.FindMethod_Impl(name, listenerType);
				break;
			case PersistentListenerMode.Void:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[0]);
				break;
			case PersistentListenerMode.Object:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { argumentType ?? typeof(Object) });
				break;
			case PersistentListenerMode.Int:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(int) });
				break;
			case PersistentListenerMode.Float:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(float) });
				break;
			case PersistentListenerMode.String:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(string) });
				break;
			case PersistentListenerMode.Bool:
				methodInfo = UnityEventBase.GetValidMethodInfo(listenerType, name, new Type[] { typeof(bool) });
				break;
			default:
				methodInfo = null;
				break;
			}
			return methodInfo;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00029990 File Offset: 0x00027B90
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000299B0 File Offset: 0x00027BB0
		public Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.target : null;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000299DC File Offset: 0x00027BDC
		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener != null) ? listener.methodName : string.Empty;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00029A0B File Offset: 0x00027C0B
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00029A24 File Offset: 0x00027C24
		private void RebuildPersistentCallsIfNeeded()
		{
			bool callsDirty = this.m_CallsDirty;
			if (callsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00029A58 File Offset: 0x00027C58
		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			bool flag = listener != null;
			if (flag)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00029A8A File Offset: 0x00027C8A
		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00029AA1 File Offset: 0x00027CA1
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00029AB1 File Offset: 0x00027CB1
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00029AC2 File Offset: 0x00027CC2
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00029AD4 File Offset: 0x00027CD4
		internal List<BaseInvokableCall> PrepareInvoke()
		{
			this.RebuildPersistentCallsIfNeeded();
			return this.m_Calls.PrepareInvoke();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00029AF8 File Offset: 0x00027CF8
		protected void Invoke(object[] parameters)
		{
			List<BaseInvokableCall> list = this.PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].Invoke(parameters);
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00029B30 File Offset: 0x00027D30
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00029B60 File Offset: 0x00027D60
		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			return UnityEventBase.GetValidMethodInfo(obj.GetType(), functionName, argumentTypes);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00029B80 File Offset: 0x00027D80
		public static MethodInfo GetValidMethodInfo(Type objectType, string functionName, Type[] argumentTypes)
		{
			while (objectType != typeof(object) && objectType != null)
			{
				MethodInfo method = objectType.GetMethod(functionName, 60, null, argumentTypes, null);
				bool flag = method != null;
				if (flag)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag2 = true;
					int num = 0;
					foreach (ParameterInfo parameterInfo in parameters)
					{
						Type type = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag2 = type.IsPrimitive == parameterType.IsPrimitive;
						bool flag3 = !flag2;
						if (flag3)
						{
							break;
						}
						num++;
					}
					bool flag4 = flag2;
					if (flag4)
					{
						return method;
					}
				}
				objectType = objectType.BaseType;
			}
			return null;
		}

		// Token: 0x040007E6 RID: 2022
		private InvokableCallList m_Calls;

		// Token: 0x040007E7 RID: 2023
		[SerializeField]
		[FormerlySerializedAs("m_PersistentListeners")]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x040007E8 RID: 2024
		private bool m_CallsDirty = true;
	}
}
