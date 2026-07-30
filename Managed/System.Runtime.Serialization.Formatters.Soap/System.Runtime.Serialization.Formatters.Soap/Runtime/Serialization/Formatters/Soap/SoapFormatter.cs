using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Soap
{
	/// <summary>Serializes and deserializes an object, or an entire graph of connected objects, in SOAP format.</summary>
	// Token: 0x0200000B RID: 11
	public sealed class SoapFormatter : IRemotingFormatter, IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> class with default property values.</summary>
		// Token: 0x0600000C RID: 12 RVA: 0x0000208C File Offset: 0x0000028C
		public SoapFormatter()
		{
			this._selector = null;
			this._context = new StreamingContext(StreamingContextStates.All);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> class with the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="selector">The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to use with the new instance of <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />. Can be null. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that holds the source and destination of the serialization. If the <paramref name="context" /> parameter is null, then the <see cref="P:System.Runtime.Serialization.Formatters.Soap.SoapFormatter.Context" /> defaults to <see cref="F:System.Runtime.Serialization.StreamingContextStates.CrossMachine" />. </param>
		// Token: 0x0600000D RID: 13 RVA: 0x000020B9 File Offset: 0x000002B9
		public SoapFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this._selector = selector;
			this._context = context;
		}

		/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
		/// <returns>The top object of the deserialized graph (root).</returns>
		/// <param name="serializationStream">The stream that contains the data to deserialize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializationStream" /> is null. </exception>
		// Token: 0x0600000E RID: 14 RVA: 0x000020DD File Offset: 0x000002DD
		public object Deserialize(Stream serializationStream)
		{
			return this.Deserialize(serializationStream, null);
		}

		/// <summary>Deserializes the stream into an object graph with any headers in that stream being handled by the given <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" />.</summary>
		/// <returns>The top object of the deserialized graph (root).</returns>
		/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
		/// <param name="handler">Delegate to handle any headers found on the stream. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="serializationStream" /> supports seeking, and its length is 0. </exception>
		// Token: 0x0600000F RID: 15 RVA: 0x000020E7 File Offset: 0x000002E7
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return new SoapReader(this._binder, this._selector, this._context).Deserialize(serializationStream, this._topObject);
		}

		/// <summary>Serializes an object or graph of objects with the specified root to the given <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="serializationStream">The stream onto which the formatter puts the data to serialize. </param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializationStream" /> is null. </exception>
		// Token: 0x06000010 RID: 16 RVA: 0x0000210C File Offset: 0x0000030C
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		/// <summary>Serializes an object or graph of objects with the specified root to the given <see cref="T:System.IO.Stream" /> in the SOAP Remote Procedure Call (RPC) format.</summary>
		/// <param name="serializationStream">The stream onto which the formatter puts the data to serialize. </param>
		/// <param name="graph">The object or root of the object graph to serialize. All child objects of this root object are automatically serialized. </param>
		/// <param name="headers">Remoting headers to include in the serialization. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serializationStream" /> is null. </exception>
		// Token: 0x06000011 RID: 17 RVA: 0x00002118 File Offset: 0x00000318
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			if (!serializationStream.CanWrite)
			{
				throw new SerializationException("Can't write in the serialization stream");
			}
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			new SoapWriter(serializationStream, this._selector, this._context, this._topObject).Serialize(graph, headers, this._typeFormat, this._assemblyFormat);
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> that controls type substitution during serialization and deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002187 File Offset: 0x00000387
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this._selector;
			}
			set
			{
				this._selector = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that controls the binding of a serialized object to a type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002190 File Offset: 0x00000390
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002198 File Offset: 0x00000398
		public SerializationBinder Binder
		{
			get
			{
				return this._binder;
			}
			set
			{
				this._binder = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used with this <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" />.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021A1 File Offset: 0x000003A1
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000021A9 File Offset: 0x000003A9
		public StreamingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.ISoapMessage" /> into which the SOAP top object is deserialized.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.ISoapMessage" /> into which the SOAP top object is deserialized.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021B2 File Offset: 0x000003B2
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021BA File Offset: 0x000003BA
		public ISoapMessage TopObject
		{
			get
			{
				return this._topObject;
			}
			set
			{
				this._topObject = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> of automatic deserialization for .NET Framework remoting.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> that represents the current automatic deserialization level.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021C3 File Offset: 0x000003C3
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021CB File Offset: 0x000003CB
		[MonoTODO("Interpret this")]
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this._filterLevel;
			}
			set
			{
				this._filterLevel = value;
			}
		}

		/// <summary>Gets or sets the behavior of the deserializer with regards to finding and loading assemblies.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values that specifies the deserializer behavior.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021DC File Offset: 0x000003DC
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this._assemblyFormat;
			}
			set
			{
				this._assemblyFormat = value;
			}
		}

		/// <summary>Gets or sets the format in which type descriptions are laid out in the serialized stream.</summary>
		/// <returns>The format in which type descriptions are laid out in the serialized stream.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021E5 File Offset: 0x000003E5
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021ED File Offset: 0x000003ED
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this._typeFormat;
			}
			set
			{
				this._typeFormat = value;
			}
		}

		// Token: 0x04000030 RID: 48
		private SerializationBinder _binder;

		// Token: 0x04000031 RID: 49
		private StreamingContext _context;

		// Token: 0x04000032 RID: 50
		private ISurrogateSelector _selector;

		// Token: 0x04000033 RID: 51
		private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

		// Token: 0x04000034 RID: 52
		private FormatterTypeStyle _typeFormat;

		// Token: 0x04000035 RID: 53
		private ISoapMessage _topObject;

		// Token: 0x04000036 RID: 54
		private TypeFilterLevel _filterLevel = TypeFilterLevel.Low;
	}
}
