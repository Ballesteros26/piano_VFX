using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for serializing a reflection primitive within the object graph.</summary>
	// Token: 0x02000158 RID: 344
	public abstract class MemberCodeDomSerializer : CodeDomSerializerBase
	{
		/// <summary>Serializes the given member descriptor on the given value to a statement collection.</summary>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="value">The object to which the member is bound.</param>
		/// <param name="descriptor">The descriptor of the member to serialize.</param>
		/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which <paramref name="descriptor" /> is serialized.</param>
		// Token: 0x06000A79 RID: 2681
		public abstract void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements);

		/// <summary>Determines if the given member should be serialized.</summary>
		/// <returns>true, if the member described by <paramref name="descriptor" /> should be serialized; otherwise, false.</returns>
		/// <param name="manager">The serialization manager to use for serialization.</param>
		/// <param name="value">The object to which the member is bound.</param>
		/// <param name="descriptor">The descriptor of the member to serialize.</param>
		// Token: 0x06000A7A RID: 2682
		public abstract bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor);
	}
}
