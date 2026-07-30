using System;
using System.Collections;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>Contains a cache of XML Schema definition language (XSD) and XML-Data Reduced (XDR) schemas. The <see cref="T:System.Xml.Schema.XmlSchemaCollection" /> class class is obsolete. Use <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instead.</summary>
	// Token: 0x0200043F RID: 1087
	[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XmlSchemaCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the XmlSchemaCollection class.</summary>
		// Token: 0x06002B27 RID: 11047 RVA: 0x001050A1 File Offset: 0x001032A1
		public XmlSchemaCollection()
			: this(new NameTable())
		{
		}

		/// <summary>Initializes a new instance of the XmlSchemaCollection class with the specified <see cref="T:System.Xml.XmlNameTable" />. The XmlNameTable is used when loading schemas.</summary>
		/// <param name="nametable">The XmlNameTable to use. </param>
		// Token: 0x06002B28 RID: 11048 RVA: 0x001050B0 File Offset: 0x001032B0
		public XmlSchemaCollection(XmlNameTable nametable)
		{
			if (nametable == null)
			{
				throw new ArgumentNullException("nametable");
			}
			this.nameTable = nametable;
			this.collection = Hashtable.Synchronized(new Hashtable());
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.isThreadSafe = true;
			if (this.isThreadSafe)
			{
				this.wLock = new ReaderWriterLock();
			}
		}

		/// <summary>Gets the number of namespaces defined in this collection.</summary>
		/// <returns>The number of namespaces defined in this collection.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x0010511B File Offset: 0x0010331B
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		/// <summary>Gets the default XmlNameTable used by the XmlSchemaCollection when loading new schemas.</summary>
		/// <returns>An XmlNameTable.</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x00105128 File Offset: 0x00103328
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		/// <summary>Sets an event handler for receiving information about the XDR and XML schema validation errors.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06002B2B RID: 11051 RVA: 0x00105130 File Offset: 0x00103330
		// (remove) Token: 0x06002B2C RID: 11052 RVA: 0x00105149 File Offset: 0x00103349
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.validationEventHandler = (ValidationEventHandler)Delegate.Combine(this.validationEventHandler, value);
			}
			remove
			{
				this.validationEventHandler = (ValidationEventHandler)Delegate.Remove(this.validationEventHandler, value);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x00105162 File Offset: 0x00103362
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Adds the schema located by the given URL into the schema collection.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema. </returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="uri">The URL that specifies the schema to load. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06002B2E RID: 11054 RVA: 0x0010516C File Offset: 0x0010336C
		public XmlSchema Add(string ns, string uri)
		{
			if (uri == null || uri.Length == 0)
			{
				throw new ArgumentNullException("uri");
			}
			XmlTextReader xmlTextReader = new XmlTextReader(uri, this.nameTable);
			xmlTextReader.XmlResolver = this.xmlResolver;
			XmlSchema xmlSchema = null;
			try
			{
				xmlSchema = this.Add(ns, xmlTextReader, this.xmlResolver);
				while (xmlTextReader.Read())
				{
				}
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlSchema;
		}

		/// <summary>Adds the schema contained in the <see cref="T:System.Xml.XmlReader" /> to the schema collection.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="reader">
		///   <see cref="T:System.Xml.XmlReader" /> containing the schema to add. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06002B2F RID: 11055 RVA: 0x001051DC File Offset: 0x001033DC
		public XmlSchema Add(string ns, XmlReader reader)
		{
			return this.Add(ns, reader, this.xmlResolver);
		}

		/// <summary>Adds the schema contained in the <see cref="T:System.Xml.XmlReader" /> to the schema collection. The specified <see cref="T:System.Xml.XmlResolver" /> is used to resolve any external resources.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> added to the schema collection; null if the schema being added is an XDR schema or if there are compilation errors in the schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the targetNamespace. </param>
		/// <param name="reader">
		///   <see cref="T:System.Xml.XmlReader" /> containing the schema to add. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces referenced in include and import elements or x-schema attribute (XDR schemas). If this is null, external references are not resolved. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06002B30 RID: 11056 RVA: 0x001051EC File Offset: 0x001033EC
		public XmlSchema Add(string ns, XmlReader reader, XmlResolver resolver)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			XmlNameTable xmlNameTable = reader.NameTable;
			SchemaInfo schemaInfo = new SchemaInfo();
			Parser parser = new Parser(SchemaType.None, xmlNameTable, this.GetSchemaNames(xmlNameTable), this.validationEventHandler);
			parser.XmlResolver = resolver;
			SchemaType schemaType;
			try
			{
				schemaType = parser.Parse(reader, ns);
			}
			catch (XmlSchemaException ex)
			{
				this.SendValidationEvent(ex);
				return null;
			}
			if (schemaType == SchemaType.XSD)
			{
				schemaInfo.SchemaType = SchemaType.XSD;
				return this.Add(ns, schemaInfo, parser.XmlSchema, true, resolver);
			}
			SchemaInfo xdrSchema = parser.XdrSchema;
			return this.Add(ns, parser.XdrSchema, null, true, resolver);
		}

		/// <summary>Adds the <see cref="T:System.Xml.Schema.XmlSchema" /> to the collection.</summary>
		/// <returns>The XmlSchema object.</returns>
		/// <param name="schema">The XmlSchema to add to the collection. </param>
		// Token: 0x06002B31 RID: 11057 RVA: 0x00105290 File Offset: 0x00103490
		public XmlSchema Add(XmlSchema schema)
		{
			return this.Add(schema, this.xmlResolver);
		}

		/// <summary>Adds the <see cref="T:System.Xml.Schema.XmlSchema" /> to the collection. The specified <see cref="T:System.Xml.XmlResolver" /> is used to resolve any external references.</summary>
		/// <returns>The XmlSchema added to the schema collection.</returns>
		/// <param name="schema">The XmlSchema to add to the collection. </param>
		/// <param name="resolver">The <see cref="T:System.Xml.XmlResolver" /> used to resolve namespaces referenced in include and import elements. If this is null, external references are not resolved. </param>
		/// <exception cref="T:System.Xml.XmlException">The schema is not a valid schema. </exception>
		// Token: 0x06002B32 RID: 11058 RVA: 0x001052A0 File Offset: 0x001034A0
		public XmlSchema Add(XmlSchema schema, XmlResolver resolver)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			SchemaInfo schemaInfo = new SchemaInfo();
			schemaInfo.SchemaType = SchemaType.XSD;
			return this.Add(schema.TargetNamespace, schemaInfo, schema, true, resolver);
		}

		/// <summary>Adds all the namespaces defined in the given collection (including their associated schemas) to this collection.</summary>
		/// <param name="schema">The XmlSchemaCollection you want to add to this collection. </param>
		// Token: 0x06002B33 RID: 11059 RVA: 0x001052D8 File Offset: 0x001034D8
		public void Add(XmlSchemaCollection schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (this == schema)
			{
				return;
			}
			IDictionaryEnumerator enumerator = schema.collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)enumerator.Value;
				this.Add(xmlSchemaCollectionNode.NamespaceURI, xmlSchemaCollectionNode);
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchema" /> associated with the given namespace URI.</summary>
		/// <returns>The XmlSchema associated with the namespace URI; null if there is no loaded schema associated with the given namespace or if the namespace is associated with an XDR schema.</returns>
		/// <param name="ns">The namespace URI associated with the schema you want to return. This will typically be the targetNamespace of the schema. </param>
		// Token: 0x17000929 RID: 2345
		public XmlSchema this[string ns]
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
				if (xmlSchemaCollectionNode == null)
				{
					return null;
				}
				return xmlSchemaCollectionNode.Schema;
			}
		}

		/// <summary>Gets a value indicating whether the targetNamespace of the specified <see cref="T:System.Xml.Schema.XmlSchema" /> is in the collection.</summary>
		/// <returns>true if there is a schema in the collection with the same targetNamespace; otherwise, false.</returns>
		/// <param name="schema">The XmlSchema object. </param>
		// Token: 0x06002B35 RID: 11061 RVA: 0x0010535C File Offset: 0x0010355C
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this[schema.TargetNamespace] != null;
		}

		/// <summary>Gets a value indicating whether a schema with the specified namespace is in the collection.</summary>
		/// <returns>true if a schema with the specified namespace is in the collection; otherwise, false.</returns>
		/// <param name="ns">The namespace URI associated with the schema. For XML Schemas, this will typically be the target namespace. </param>
		// Token: 0x06002B36 RID: 11062 RVA: 0x0010537B File Offset: 0x0010357B
		public bool Contains(string ns)
		{
			return this.collection[(ns != null) ? ns : string.Empty] != null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.GetEnumerator" />.</summary>
		/// <returns>Returns the <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06002B37 RID: 11063 RVA: 0x00105396 File Offset: 0x00103596
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		/// <summary>Provides support for the "for each" style iteration over the collection of schemas.</summary>
		/// <returns>An enumerator for iterating over all schemas in the current collection.</returns>
		// Token: 0x06002B38 RID: 11064 RVA: 0x00105396 File Offset: 0x00103596
		public XmlSchemaCollectionEnumerator GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollection.CopyTo(System.Xml.Schema.XmlSchema[],System.Int32)" />.</summary>
		/// <param name="array">The array to copy the objects to. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying will begin. </param>
		// Token: 0x06002B39 RID: 11065 RVA: 0x001053A4 File Offset: 0x001035A4
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XmlSchemaCollectionEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (index == array.Length && array.IsFixedSize)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				array.SetValue(enumerator.Current, index++);
			}
		}

		/// <summary>Copies all the XmlSchema objects from this collection into the given array starting at the given index.</summary>
		/// <param name="array">The array to copy the objects to. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying will begin. </param>
		// Token: 0x06002B3A RID: 11066 RVA: 0x00105410 File Offset: 0x00103610
		public void CopyTo(XmlSchema[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XmlSchemaCollectionEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current != null)
				{
					if (index == array.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					array[index++] = enumerator.Current;
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>Returns true if the collection is synchronized, otherwise false.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x00003242 File Offset: 0x00001442
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>Returns a <see cref="T:System.Collections.ICollection.SyncRoot" /> object that can be used to synchronize access to the collection.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollection.Count" />.</summary>
		/// <returns>Returns the count of the items in the collection.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x0010511B File Offset: 0x0010331B
		int ICollection.Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00105474 File Offset: 0x00103674
		internal SchemaInfo GetSchemaInfo(string ns)
		{
			XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
			if (xmlSchemaCollectionNode == null)
			{
				return null;
			}
			return xmlSchemaCollectionNode.SchemaInfo;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x001054A8 File Offset: 0x001036A8
		internal SchemaNames GetSchemaNames(XmlNameTable nt)
		{
			if (this.nameTable != nt)
			{
				return new SchemaNames(nt);
			}
			if (this.schemaNames == null)
			{
				this.schemaNames = new SchemaNames(this.nameTable);
			}
			return this.schemaNames;
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x001054D9 File Offset: 0x001036D9
		internal XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile)
		{
			return this.Add(ns, schemaInfo, schema, compile, this.xmlResolver);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x001054EC File Offset: 0x001036EC
		private XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile, XmlResolver resolver)
		{
			int num = 0;
			if (schema != null)
			{
				if (schema.ErrorCount == 0 && compile)
				{
					if (!schema.CompileSchema(this, resolver, schemaInfo, ns, this.validationEventHandler, this.nameTable, true))
					{
						num = 1;
					}
					ns = ((schema.TargetNamespace == null) ? string.Empty : schema.TargetNamespace);
				}
				num += schema.ErrorCount;
			}
			else
			{
				num += schemaInfo.ErrorCount;
				ns = this.NameTable.Add(ns);
			}
			if (num == 0)
			{
				this.Add(ns, new XmlSchemaCollectionNode
				{
					NamespaceURI = ns,
					SchemaInfo = schemaInfo,
					Schema = schema
				});
				return schema;
			}
			return null;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0010558C File Offset: 0x0010378C
		private void Add(string ns, XmlSchemaCollectionNode node)
		{
			if (this.isThreadSafe)
			{
				this.wLock.AcquireWriterLock(this.timeout);
			}
			try
			{
				if (this.collection[ns] != null)
				{
					this.collection.Remove(ns);
				}
				this.collection.Add(ns, node);
			}
			finally
			{
				if (this.isThreadSafe)
				{
					this.wLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00105600 File Offset: 0x00103800
		private void SendValidationEvent(XmlSchemaException e)
		{
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e));
				return;
			}
			throw e;
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x0010561E File Offset: 0x0010381E
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x00105626 File Offset: 0x00103826
		internal ValidationEventHandler EventHandler
		{
			get
			{
				return this.validationEventHandler;
			}
			set
			{
				this.validationEventHandler = value;
			}
		}

		// Token: 0x04001D42 RID: 7490
		private Hashtable collection;

		// Token: 0x04001D43 RID: 7491
		private XmlNameTable nameTable;

		// Token: 0x04001D44 RID: 7492
		private SchemaNames schemaNames;

		// Token: 0x04001D45 RID: 7493
		private ReaderWriterLock wLock;

		// Token: 0x04001D46 RID: 7494
		private int timeout = -1;

		// Token: 0x04001D47 RID: 7495
		private bool isThreadSafe = true;

		// Token: 0x04001D48 RID: 7496
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001D49 RID: 7497
		private XmlResolver xmlResolver;
	}
}
