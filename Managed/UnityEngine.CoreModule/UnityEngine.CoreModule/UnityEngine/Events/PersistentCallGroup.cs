using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000258 RID: 600
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x0600196C RID: 6508 RVA: 0x000292D9 File Offset: 0x000274D9
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x000292F0 File Offset: 0x000274F0
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00029310 File Offset: 0x00027510
		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00029330 File Offset: 0x00027530
		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00029348 File Offset: 0x00027548
		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0002935C File Offset: 0x0002755C
		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0002936C File Offset: 0x0002756C
		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0002937C File Offset: 0x0002757C
		public void Clear()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0002938C File Offset: 0x0002758C
		public void RegisterEventPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000293B8 File Offset: 0x000275B8
		public void RegisterVoidPersistentListener(int index, Object targetObj, Type targetObjType, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000293E4 File Offset: 0x000275E4
		public void RegisterObjectPersistentListener(int index, Object targetObj, Type targetObjType, Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0002941C File Offset: 0x0002761C
		public void RegisterIntPersistentListener(int index, Object targetObj, Type targetObjType, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00029454 File Offset: 0x00027654
		public void RegisterFloatPersistentListener(int index, Object targetObj, Type targetObjType, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0002948C File Offset: 0x0002768C
		public void RegisterStringPersistentListener(int index, Object targetObj, Type targetObjType, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000294C4 File Offset: 0x000276C4
		public void RegisterBoolPersistentListener(int index, Object targetObj, Type targetObjType, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, targetObjType, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000294FC File Offset: 0x000276FC
		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0002951C File Offset: 0x0002771C
		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				bool flag = this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName;
				if (flag)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000295B0 File Offset: 0x000277B0
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				bool flag = !persistentCall.IsValid();
				if (!flag)
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					bool flag2 = runtimeCall != null;
					if (flag2)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x040007E1 RID: 2017
		[FormerlySerializedAs("m_Listeners")]
		[SerializeField]
		private List<PersistentCall> m_Calls;
	}
}
