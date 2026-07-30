using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	internal class PersistentCall : ISerializationCallbackReceiver
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00028F68 File Offset: 0x00027168
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00028F80 File Offset: 0x00027180
		public string targetAssemblyTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TargetAssemblyTypeName) && this.m_Target != null;
				if (flag)
				{
					this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_Target.GetType().AssemblyQualifiedName);
				}
				return this.m_TargetAssemblyTypeName;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00028FD8 File Offset: 0x000271D8
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00028FF0 File Offset: 0x000271F0
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x00029008 File Offset: 0x00027208
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00029014 File Offset: 0x00027214
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0002902C File Offset: 0x0002722C
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x00029044 File Offset: 0x00027244
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00029050 File Offset: 0x00027250
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.targetAssemblyTypeName) && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00029080 File Offset: 0x00027280
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			bool flag = this.m_CallState == UnityEventCallState.Off || theEvent == null;
			BaseInvokableCall baseInvokableCall;
			if (flag)
			{
				baseInvokableCall = null;
			}
			else
			{
				MethodInfo methodInfo = theEvent.FindMethod(this);
				bool flag2 = methodInfo == null;
				if (flag2)
				{
					baseInvokableCall = null;
				}
				else
				{
					Object @object = (methodInfo.IsStatic ? null : this.target);
					switch (this.m_Mode)
					{
					case PersistentListenerMode.EventDefined:
						baseInvokableCall = theEvent.GetDelegate(@object, methodInfo);
						break;
					case PersistentListenerMode.Void:
						baseInvokableCall = new InvokableCall(@object, methodInfo);
						break;
					case PersistentListenerMode.Object:
						baseInvokableCall = PersistentCall.GetObjectCall(@object, methodInfo, this.m_Arguments);
						break;
					case PersistentListenerMode.Int:
						baseInvokableCall = new CachedInvokableCall<int>(@object, methodInfo, this.m_Arguments.intArgument);
						break;
					case PersistentListenerMode.Float:
						baseInvokableCall = new CachedInvokableCall<float>(@object, methodInfo, this.m_Arguments.floatArgument);
						break;
					case PersistentListenerMode.String:
						baseInvokableCall = new CachedInvokableCall<string>(@object, methodInfo, this.m_Arguments.stringArgument);
						break;
					case PersistentListenerMode.Bool:
						baseInvokableCall = new CachedInvokableCall<bool>(@object, methodInfo, this.m_Arguments.boolArgument);
						break;
					default:
						baseInvokableCall = null;
						break;
					}
				}
			}
			return baseInvokableCall;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00029180 File Offset: 0x00027380
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			bool flag = !string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName);
			if (flag)
			{
				type = Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[] { type });
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			bool flag2 = @object != null && !type.IsAssignableFrom(@object.GetType());
			if (flag2)
			{
				@object = null;
			}
			return constructor.Invoke(new object[] { target, method, @object }) as BaseInvokableCall;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00029261 File Offset: 0x00027461
		public void RegisterPersistentListener(Object ttarget, Type targetType, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(targetType.AssemblyQualifiedName);
			this.m_MethodName = mmethodName;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00029283 File Offset: 0x00027483
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
			this.m_TargetAssemblyTypeName = string.Empty;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000292A3 File Offset: 0x000274A3
		public void OnBeforeSerialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x000292A3 File Offset: 0x000274A3
		public void OnAfterDeserialize()
		{
			this.m_TargetAssemblyTypeName = UnityEventTools.TidyAssemblyTypeName(this.m_TargetAssemblyTypeName);
		}

		// Token: 0x040007DB RID: 2011
		[SerializeField]
		[FormerlySerializedAs("instance")]
		private Object m_Target;

		// Token: 0x040007DC RID: 2012
		[SerializeField]
		private string m_TargetAssemblyTypeName;

		// Token: 0x040007DD RID: 2013
		[SerializeField]
		[FormerlySerializedAs("methodName")]
		private string m_MethodName;

		// Token: 0x040007DE RID: 2014
		[SerializeField]
		[FormerlySerializedAs("mode")]
		private PersistentListenerMode m_Mode = PersistentListenerMode.EventDefined;

		// Token: 0x040007DF RID: 2015
		[FormerlySerializedAs("arguments")]
		[SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x040007E0 RID: 2016
		[SerializeField]
		[FormerlySerializedAs("m_Enabled")]
		[FormerlySerializedAs("enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
