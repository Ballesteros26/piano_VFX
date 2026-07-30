using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Manages serialization processes at run time. This class cannot be inherited.</summary>
	// Token: 0x020006F2 RID: 1778
	public sealed class SerializationObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> class. </summary>
		/// <param name="context">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains information about the current serialization operation.</param>
		// Token: 0x06004B00 RID: 19200 RVA: 0x0010C2FA File Offset: 0x0010A4FA
		public SerializationObjectManager(StreamingContext context)
		{
			this.m_context = context;
			this.m_objectSeenTable = new Hashtable();
		}

		/// <summary>Registers the object upon which events will be raised.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06004B01 RID: 19201 RVA: 0x0010C320 File Offset: 0x0010A520
		[SecurityCritical]
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this.m_objectSeenTable[obj] == null)
			{
				this.m_objectSeenTable[obj] = true;
				serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
				this.AddOnSerialized(obj);
			}
		}

		/// <summary>Invokes the OnSerializing callback event if the type of the object has one; and registers the object for raising the OnSerialized event if the type of the object has one.</summary>
		// Token: 0x06004B02 RID: 19202 RVA: 0x0010C375 File Offset: 0x0010A575
		public void RaiseOnSerializedEvent()
		{
			if (this.m_onSerializedHandler != null)
			{
				this.m_onSerializedHandler(this.m_context);
			}
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x0010C390 File Offset: 0x0010A590
		[SecuritySafeCritical]
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this.m_onSerializedHandler);
		}

		// Token: 0x04002720 RID: 10016
		private Hashtable m_objectSeenTable = new Hashtable();

		// Token: 0x04002721 RID: 10017
		private SerializationEventHandler m_onSerializedHandler;

		// Token: 0x04002722 RID: 10018
		private StreamingContext m_context;
	}
}
