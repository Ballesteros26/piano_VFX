using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Defines the reader, writer, and methods for pre-generated, typed serializers.</summary>
	// Token: 0x02000361 RID: 865
	public abstract class XmlSerializerImplementation
	{
		/// <summary>Gets the XML reader object that is used by the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that is used to read an XML document or data stream.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlSerializationReader Reader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the XML writer object for the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> that is used to write to an XML data stream or document.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlSerializationWriter Writer
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of methods that is used to read a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual Hashtable ReadMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Get the collection of methods that is used to write to a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual Hashtable WriteMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of typed serializers that is found in the assembly.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the typed serializers.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual Hashtable TypedSerializers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that determines whether a type can be serialized.</summary>
		/// <returns>true if the type can be serialized; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		// Token: 0x0600234A RID: 9034 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual bool CanSerialize(Type type)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a serializer for the specified type.</summary>
		/// <returns>An instance of a type derived from the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </returns>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		// Token: 0x0600234B RID: 9035 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual XmlSerializer GetSerializer(Type type)
		{
			throw new NotSupportedException();
		}
	}
}
