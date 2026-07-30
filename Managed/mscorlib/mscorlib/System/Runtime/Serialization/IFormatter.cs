using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides functionality for formatting serialized objects.</summary>
	// Token: 0x020006D1 RID: 1745
	[ComVisible(true)]
	public interface IFormatter
	{
		/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
		/// <returns>The top object of the deserialized graph.</returns>
		/// <param name="serializationStream">The stream that contains the data to deserialize. </param>
		// Token: 0x060049EB RID: 18923
		object Deserialize(Stream serializationStream);

		/// <summary>Serializes an object, or graph of objects with the given root to the provided stream.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on). </param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized. </param>
		// Token: 0x060049EC RID: 18924
		void Serialize(Stream serializationStream, object graph);

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by this formatter.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060049ED RID: 18925
		// (set) Token: 0x060049EE RID: 18926
		ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060049EF RID: 18927
		// (set) Token: 0x060049F0 RID: 18928
		SerializationBinder Binder { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060049F1 RID: 18929
		// (set) Token: 0x060049F2 RID: 18930
		StreamingContext Context { get; set; }
	}
}
