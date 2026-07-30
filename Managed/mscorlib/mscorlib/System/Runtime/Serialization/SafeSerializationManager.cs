using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020006E4 RID: 1764
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06004A86 RID: 19078 RVA: 0x0010ADE8 File Offset: 0x00108FE8
		// (remove) Token: 0x06004A87 RID: 19079 RVA: 0x0010AE20 File Offset: 0x00109020
		internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState;

		// Token: 0x06004A88 RID: 19080 RVA: 0x00002111 File Offset: 0x00000311
		internal SafeSerializationManager()
		{
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0010AE58 File Offset: 0x00109058
		[SecurityCritical]
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>;
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x0010AEBE File Offset: 0x001090BE
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0010AECC File Offset: 0x001090CC
		[SecurityCritical]
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x0010AF30 File Offset: 0x00109130
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					((ISafeSerializationData)obj).CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x0010AF88 File Offset: 0x00109188
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x0010AFA8 File Offset: 0x001091A8
		[SecurityCritical]
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = runtimeType.BaseType as RuntimeType;
			}
			while (runtimeType != typeof(object));
			RuntimeType runtimeType2;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				runtimeType2 = runtimeType;
				runtimeType = stack.Pop() as RuntimeType;
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType2);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x0010B06B File Offset: 0x0010926B
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType()).InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x040026F7 RID: 9975
		private IList<object> m_serializedStates;

		// Token: 0x040026F8 RID: 9976
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x040026F9 RID: 9977
		private object m_realObject;

		// Token: 0x040026FA RID: 9978
		private RuntimeType m_realType;

		// Token: 0x040026FC RID: 9980
		private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";
	}
}
