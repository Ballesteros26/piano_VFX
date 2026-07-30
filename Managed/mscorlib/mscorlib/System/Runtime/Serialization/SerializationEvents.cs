using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x020006EB RID: 1771
	internal class SerializationEvents
	{
		// Token: 0x06004A9A RID: 19098 RVA: 0x0010B0D4 File Offset: 0x001092D4
		private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			Type type = t;
			while (type != null && type != typeof(object))
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.IsDefined(attribute, false))
					{
						list.Add(methodInfo);
					}
				}
				type = type.BaseType;
			}
			list.Reverse();
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0010B150 File Offset: 0x00109350
		internal SerializationEvents(Type t)
		{
			this.m_OnSerializingMethods = this.GetMethodsWithAttribute(typeof(OnSerializingAttribute), t);
			this.m_OnSerializedMethods = this.GetMethodsWithAttribute(typeof(OnSerializedAttribute), t);
			this.m_OnDeserializingMethods = this.GetMethodsWithAttribute(typeof(OnDeserializingAttribute), t);
			this.m_OnDeserializedMethods = this.GetMethodsWithAttribute(typeof(OnDeserializedAttribute), t);
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x0010B1BF File Offset: 0x001093BF
		internal bool HasOnSerializingEvents
		{
			get
			{
				return this.m_OnSerializingMethods != null || this.m_OnSerializedMethods != null;
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0010B1D4 File Offset: 0x001093D4
		[SecuritySafeCritical]
		internal void InvokeOnSerializing(object obj, StreamingContext context)
		{
			if (this.m_OnSerializingMethods != null)
			{
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnSerializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0010B25C File Offset: 0x0010945C
		[SecuritySafeCritical]
		internal void InvokeOnDeserializing(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializingMethods != null)
			{
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0010B2E4 File Offset: 0x001094E4
		[SecuritySafeCritical]
		internal void InvokeOnDeserialized(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0010B36C File Offset: 0x0010956C
		[SecurityCritical]
		internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnSerializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnSerializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0010B3EC File Offset: 0x001095EC
		[SecurityCritical]
		internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x040026FE RID: 9982
		private List<MethodInfo> m_OnSerializingMethods;

		// Token: 0x040026FF RID: 9983
		private List<MethodInfo> m_OnSerializedMethods;

		// Token: 0x04002700 RID: 9984
		private List<MethodInfo> m_OnDeserializingMethods;

		// Token: 0x04002701 RID: 9985
		private List<MethodInfo> m_OnDeserializedMethods;
	}
}
