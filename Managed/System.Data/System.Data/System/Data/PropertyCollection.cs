using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents a collection of properties that can be added to <see cref="T:System.Data.DataColumn" />, <see cref="T:System.Data.DataSet" />, or <see cref="T:System.Data.DataTable" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class PropertyCollection : Hashtable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		// Token: 0x06000BE4 RID: 3044 RVA: 0x000363A0 File Offset: 0x000345A0
		public PropertyCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x06000BE5 RID: 3045 RVA: 0x000363A8 File Offset: 0x000345A8
		protected PropertyCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Object" />, a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</returns>
		// Token: 0x06000BE6 RID: 3046 RVA: 0x000363B4 File Offset: 0x000345B4
		public override object Clone()
		{
			PropertyCollection propertyCollection = new PropertyCollection();
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				propertyCollection.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return propertyCollection;
		}
	}
}
