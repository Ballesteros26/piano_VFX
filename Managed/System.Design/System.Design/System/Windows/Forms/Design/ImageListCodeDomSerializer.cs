using System;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	/// <summary>Serializes string dictionaries.</summary>
	// Token: 0x0200002C RID: 44
	public class ImageListCodeDomSerializer : CodeDomSerializer
	{
		/// <summary>Deserializes the specified serialized Code Document Object Model (CodeDOM) object into an object.</summary>
		/// <returns>The deserialized CodeDOM object.</returns>
		/// <param name="manager">A serialization manager interface that is used during the deserialization process.</param>
		/// <param name="codeObject">A serialized CodeDOM object to deserialize.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="manager" /> or <paramref name="codeObject" /> is null.</exception>
		// Token: 0x06000176 RID: 374 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the specified object into a Code Document Object Model (CodeDOM) object.</summary>
		/// <returns>A CodeDOM object representing the object that has been serialized.</returns>
		/// <param name="manager">The serialization manager to use during serialization.</param>
		/// <param name="value">The object to serialize.</param>
		// Token: 0x06000177 RID: 375 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			throw new NotImplementedException();
		}
	}
}
