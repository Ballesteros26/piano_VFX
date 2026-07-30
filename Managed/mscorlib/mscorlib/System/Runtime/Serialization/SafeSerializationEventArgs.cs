using System;
using System.Collections.Generic;
using Unity;

namespace System.Runtime.Serialization
{
	/// <summary>Provides data for the <see cref="T:System.Exception.SerializeObjectState" /> event.</summary>
	// Token: 0x020006E2 RID: 1762
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x06004A80 RID: 19072 RVA: 0x0010AD56 File Offset: 0x00108F56
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_serializedStates = new List<object>();
			base..ctor();
			this.m_streamingContext = streamingContext;
		}

		/// <summary>Stores the state of the exception.</summary>
		/// <param name="serializedState">A state object that is serialized with the instance.</param>
		// Token: 0x06004A81 RID: 19073 RVA: 0x0010AD70 File Offset: 0x00108F70
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable.", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0010ADD6 File Offset: 0x00108FD6
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		/// <summary>Gets or sets an object that describes the source and destination of a serialized stream.</summary>
		/// <returns>An object that describes the source and destination of a serialized stream.</returns>
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x0010ADDE File Offset: 0x00108FDE
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal SafeSerializationEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040026F5 RID: 9973
		private StreamingContext m_streamingContext;

		// Token: 0x040026F6 RID: 9974
		private List<object> m_serializedStates;
	}
}
