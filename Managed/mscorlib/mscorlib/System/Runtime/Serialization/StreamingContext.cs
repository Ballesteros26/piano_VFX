using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Describes the source and destination of a given serialized stream, and provides an additional caller-defined context.</summary>
	// Token: 0x020006F3 RID: 1779
	[ComVisible(true)]
	[Serializable]
	public struct StreamingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		// Token: 0x06004B04 RID: 19204 RVA: 0x0010C3BC File Offset: 0x0010A5BC
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state, and some additional information.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		/// <param name="additional">Any additional information to be associated with the <see cref="T:System.Runtime.Serialization.StreamingContext" />. This information is available to any object that implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or any serialization surrogate. Most users do not need to set this parameter. </param>
		// Token: 0x06004B05 RID: 19205 RVA: 0x0010C3C6 File Offset: 0x0010A5C6
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		/// <summary>Gets context specified as part of the additional context.</summary>
		/// <returns>The context specified as part of the additional context.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x0010C3D6 File Offset: 0x0010A5D6
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances contain the same values.</summary>
		/// <returns>true if the specified object is an instance of <see cref="T:System.Runtime.Serialization.StreamingContext" /> and equals the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with the current instance. </param>
		// Token: 0x06004B07 RID: 19207 RVA: 0x0010C3DE File Offset: 0x0010A5DE
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		/// <summary>Returns a hash code of this object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> value that contains the source or destination of the serialization for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</returns>
		// Token: 0x06004B08 RID: 19208 RVA: 0x0010C413 File Offset: 0x0010A613
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		/// <summary>Gets the source or destination of the transmitted data.</summary>
		/// <returns>During serialization, the destination of the transmitted data. During deserialization, the source of the data.</returns>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004B09 RID: 19209 RVA: 0x0010C413 File Offset: 0x0010A613
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x04002723 RID: 10019
		internal object m_additionalContext;

		// Token: 0x04002724 RID: 10020
		internal StreamingContextStates m_state;
	}
}
