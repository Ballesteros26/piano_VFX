using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>An in-memory representation of an XML Schema as specified in the World Wide Web Consortium (W3C) XML Schema Part 1: Structures and XML Schema Part 2: Datatypes specifications.</summary>
	// Token: 0x02000434 RID: 1076
	[XmlRoot("schema", Namespace = "http://www.w3.org/2001/XMLSchema")]
	public class XmlSchema : XmlSchemaObject
	{
		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <param name="reader">The TextReader containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x06002A77 RID: 10871 RVA: 0x0010412F File Offset: 0x0010232F
		public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(reader), validationEventHandler);
		}

		/// <summary>Reads an XML Schema  from the supplied stream.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <param name="stream">The supplied data stream. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about XML Schema syntax errors. </param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x06002A78 RID: 10872 RVA: 0x0010413D File Offset: 0x0010233D
		public static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			return XmlSchema.Read(new XmlTextReader(stream), validationEventHandler);
		}

		/// <summary>Reads an XML Schema from the supplied <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XML Schema.</returns>
		/// <param name="reader">The XmlReader containing the XML Schema to read. </param>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema syntax errors. </param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An <see cref="T:System.Xml.Schema.XmlSchemaException" /> is raised if no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> is specified.</exception>
		// Token: 0x06002A79 RID: 10873 RVA: 0x0010414C File Offset: 0x0010234C
		public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlNameTable xmlNameTable = reader.NameTable;
			Parser parser = new Parser(SchemaType.XSD, xmlNameTable, new SchemaNames(xmlNameTable), validationEventHandler);
			try
			{
				parser.Parse(reader, null);
			}
			catch (XmlSchemaException ex)
			{
				if (validationEventHandler != null)
				{
					validationEventHandler(null, new ValidationEventArgs(ex));
					return null;
				}
				throw ex;
			}
			return parser.XmlSchema;
		}

		/// <summary>Writes the XML Schema to the supplied data stream.</summary>
		/// <param name="stream">The supplied data stream. </param>
		// Token: 0x06002A7A RID: 10874 RVA: 0x001041AC File Offset: 0x001023AC
		public void Write(Stream stream)
		{
			this.Write(stream, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.Stream" /> using the <see cref="T:System.Xml.XmlNamespaceManager" /> specified.</summary>
		/// <param name="stream">The supplied data stream. </param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />.</param>
		// Token: 0x06002A7B RID: 10875 RVA: 0x001041B8 File Offset: 0x001023B8
		public void Write(Stream stream, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		// Token: 0x06002A7C RID: 10876 RVA: 0x001041DC File Offset: 0x001023DC
		public void Write(TextWriter writer)
		{
			this.Write(writer, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />. </param>
		// Token: 0x06002A7D RID: 10877 RVA: 0x001041E8 File Offset: 0x001023E8
		public void Write(TextWriter writer, XmlNamespaceManager namespaceManager)
		{
			this.Write(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, namespaceManager);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="writer" /> parameter is null.</exception>
		// Token: 0x06002A7E RID: 10878 RVA: 0x0010420B File Offset: 0x0010240B
		public void Write(XmlWriter writer)
		{
			this.Write(writer, null);
		}

		/// <summary>Writes the XML Schema to the supplied <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to.</param>
		/// <param name="namespaceManager">The <see cref="T:System.Xml.XmlNamespaceManager" />. </param>
		// Token: 0x06002A7F RID: 10879 RVA: 0x00104218 File Offset: 0x00102418
		public void Write(XmlWriter writer, XmlNamespaceManager namespaceManager)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlSchema));
			XmlSerializerNamespaces xmlSerializerNamespaces;
			if (namespaceManager != null)
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				bool flag = false;
				if (base.Namespaces != null)
				{
					flag = base.Namespaces.Namespaces["xs"] != null || base.Namespaces.Namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema");
				}
				if (!flag && namespaceManager.LookupPrefix("http://www.w3.org/2001/XMLSchema") == null && namespaceManager.LookupNamespace("xs") == null)
				{
					xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				using (IEnumerator enumerator = namespaceManager.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						string text = (string)obj;
						if (text != "xml" && text != "xmlns")
						{
							xmlSerializerNamespaces.Add(text, namespaceManager.LookupNamespace(text));
						}
					}
					goto IL_017B;
				}
			}
			if (base.Namespaces != null && base.Namespaces.Count > 0)
			{
				Hashtable namespaces = base.Namespaces.Namespaces;
				if (namespaces["xs"] == null && !namespaces.ContainsValue("http://www.w3.org/2001/XMLSchema"))
				{
					namespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				}
				xmlSerializerNamespaces = base.Namespaces;
			}
			else
			{
				xmlSerializerNamespaces = new XmlSerializerNamespaces();
				xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
				if (this.targetNs != null && this.targetNs.Length != 0)
				{
					xmlSerializerNamespaces.Add("tns", this.targetNs);
				}
			}
			IL_017B:
			xmlSerializer.Serialize(writer, this, xmlSerializerNamespaces);
		}

		/// <summary>Compiles the XML Schema Object Model (SOM) into schema information for validation. Used to check the syntactic and semantic structure of the programmatically built SOM. Semantic validation checking is performed during compilation.</summary>
		/// <param name="validationEventHandler">The validation event handler that receives information about XML Schema validation errors. </param>
		// Token: 0x06002A80 RID: 10880 RVA: 0x001043BC File Offset: 0x001025BC
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler)
		{
			SchemaInfo schemaInfo = new SchemaInfo();
			schemaInfo.SchemaType = SchemaType.XSD;
			this.CompileSchema(null, XmlReaderSection.CreateDefaultResolver(), schemaInfo, null, validationEventHandler, this.NameTable, false);
		}

		/// <summary>Compiles the XML Schema Object Model (SOM) into schema information for validation. Used to check the syntactic and semantic structure of the programmatically built SOM. Semantic validation checking is performed during compilation.</summary>
		/// <param name="validationEventHandler">The validation event handler that receives information about the XML Schema validation errors. </param>
		/// <param name="resolver">The XmlResolver used to resolve namespaces referenced in include and import elements. </param>
		// Token: 0x06002A81 RID: 10881 RVA: 0x001043F0 File Offset: 0x001025F0
		[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void Compile(ValidationEventHandler validationEventHandler, XmlResolver resolver)
		{
			this.CompileSchema(null, resolver, new SchemaInfo
			{
				SchemaType = SchemaType.XSD
			}, null, validationEventHandler, this.NameTable, false);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00104420 File Offset: 0x00102620
		internal bool CompileSchema(XmlSchemaCollection xsc, XmlResolver resolver, SchemaInfo schemaInfo, string ns, ValidationEventHandler validationEventHandler, XmlNameTable nameTable, bool CompileContentModel)
		{
			bool flag2;
			lock (this)
			{
				if (!new SchemaCollectionPreprocessor(nameTable, null, validationEventHandler)
				{
					XmlResolver = resolver
				}.Execute(this, ns, true, xsc))
				{
					flag2 = false;
				}
				else
				{
					SchemaCollectionCompiler schemaCollectionCompiler = new SchemaCollectionCompiler(nameTable, validationEventHandler);
					this.isCompiled = schemaCollectionCompiler.Execute(this, schemaInfo, CompileContentModel);
					this.SetIsCompiled(this.isCompiled);
					flag2 = this.isCompiled;
				}
			}
			return flag2;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x001044A4 File Offset: 0x001026A4
		internal void CompileSchemaInSet(XmlNameTable nameTable, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			Compiler compiler = new Compiler(nameTable, eventHandler, null, compilationSettings);
			compiler.Prepare(this, true);
			this.isCompiledBySet = compiler.Compile();
		}

		/// <summary>Gets or sets the form for attributes declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if attributes from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x001044CF File Offset: 0x001026CF
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x001044D7 File Offset: 0x001026D7
		[XmlAttribute("attributeFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm AttributeFormDefault
		{
			get
			{
				return this.attributeFormDefault;
			}
			set
			{
				this.attributeFormDefault = value;
			}
		}

		/// <summary>Gets or sets the blockDefault attribute which sets the default value of the block attribute on element and complex types in the targetNamespace of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x001044E0 File Offset: 0x001026E0
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x001044E8 File Offset: 0x001026E8
		[XmlAttribute("blockDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod BlockDefault
		{
			get
			{
				return this.blockDefault;
			}
			set
			{
				this.blockDefault = value;
			}
		}

		/// <summary>Gets or sets the finalDefault attribute which sets the default value of the final attribute on elements and complex types in the target namespace of the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaDerivationMethod" /> value representing the different methods for preventing derivation. The default value is XmlSchemaDerivationMethod.None.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x001044F1 File Offset: 0x001026F1
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x001044F9 File Offset: 0x001026F9
		[XmlAttribute("finalDefault")]
		[DefaultValue(XmlSchemaDerivationMethod.None)]
		public XmlSchemaDerivationMethod FinalDefault
		{
			get
			{
				return this.finalDefault;
			}
			set
			{
				this.finalDefault = value;
			}
		}

		/// <summary>Gets or sets the form for elements declared in the target namespace of the schema.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaForm" /> value that indicates if elements from the target namespace are required to be qualified with the namespace prefix. The default is <see cref="F:System.Xml.Schema.XmlSchemaForm.None" />.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00104502 File Offset: 0x00102702
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x0010450A File Offset: 0x0010270A
		[XmlAttribute("elementFormDefault")]
		[DefaultValue(XmlSchemaForm.None)]
		public XmlSchemaForm ElementFormDefault
		{
			get
			{
				return this.elementFormDefault;
			}
			set
			{
				this.elementFormDefault = value;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the schema target namespace.</summary>
		/// <returns>The schema target namespace.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x00104513 File Offset: 0x00102713
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x0010451B File Offset: 0x0010271B
		[XmlAttribute("targetNamespace", DataType = "anyURI")]
		public string TargetNamespace
		{
			get
			{
				return this.targetNs;
			}
			set
			{
				this.targetNs = value;
			}
		}

		/// <summary>Gets or sets the version of the schema.</summary>
		/// <returns>The version of the schema. The default value is String.Empty.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x00104524 File Offset: 0x00102724
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x0010452C File Offset: 0x0010272C
		[XmlAttribute("version", DataType = "token")]
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Gets the collection of included and imported schemas.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of the included and imported schemas.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00104535 File Offset: 0x00102735
		[XmlElement("include", typeof(XmlSchemaInclude))]
		[XmlElement("import", typeof(XmlSchemaImport))]
		[XmlElement("redefine", typeof(XmlSchemaRedefine))]
		public XmlSchemaObjectCollection Includes
		{
			get
			{
				return this.includes;
			}
		}

		/// <summary>Gets the collection of schema elements in the schema and is used to add new element types at the schema element level.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of schema elements in the schema.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x0010453D File Offset: 0x0010273D
		[XmlElement("annotation", typeof(XmlSchemaAnnotation))]
		[XmlElement("attribute", typeof(XmlSchemaAttribute))]
		[XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroup))]
		[XmlElement("simpleType", typeof(XmlSchemaSimpleType))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("group", typeof(XmlSchemaGroup))]
		[XmlElement("notation", typeof(XmlSchemaNotation))]
		[XmlElement("complexType", typeof(XmlSchemaComplexType))]
		public XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Indicates if the schema has been compiled.</summary>
		/// <returns>true if schema has been compiled, otherwise, false. The default value is false.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00104545 File Offset: 0x00102745
		[XmlIgnore]
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled || this.isCompiledBySet;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x00104557 File Offset: 0x00102757
		// (set) Token: 0x06002A94 RID: 10900 RVA: 0x0010455F File Offset: 0x0010275F
		[XmlIgnore]
		internal bool IsCompiledBySet
		{
			get
			{
				return this.isCompiledBySet;
			}
			set
			{
				this.isCompiledBySet = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x00104568 File Offset: 0x00102768
		// (set) Token: 0x06002A96 RID: 10902 RVA: 0x00104570 File Offset: 0x00102770
		[XmlIgnore]
		internal bool IsPreprocessed
		{
			get
			{
				return this.isPreprocessed;
			}
			set
			{
				this.isPreprocessed = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x00104579 File Offset: 0x00102779
		// (set) Token: 0x06002A98 RID: 10904 RVA: 0x00104581 File Offset: 0x00102781
		[XmlIgnore]
		internal bool IsRedefined
		{
			get
			{
				return this.isRedefined;
			}
			set
			{
				this.isRedefined = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the attributes in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the attributes in the schema.</returns>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x0010458A File Offset: 0x0010278A
		[XmlIgnore]
		public XmlSchemaObjectTable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the global attribute groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the global attribute groups in the schema.</returns>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x001045A5 File Offset: 0x001027A5
		[XmlIgnore]
		public XmlSchemaObjectTable AttributeGroups
		{
			get
			{
				if (this.attributeGroups == null)
				{
					this.attributeGroups = new XmlSchemaObjectTable();
				}
				return this.attributeGroups;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all schema types in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectCollection" /> of all schema types in the schema.</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x001045C0 File Offset: 0x001027C0
		[XmlIgnore]
		public XmlSchemaObjectTable SchemaTypes
		{
			get
			{
				if (this.types == null)
				{
					this.types = new XmlSchemaObjectTable();
				}
				return this.types;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all the elements in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the elements in the schema.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x001045DB File Offset: 0x001027DB
		[XmlIgnore]
		public XmlSchemaObjectTable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		/// <summary>Gets or sets the string ID.</summary>
		/// <returns>The ID of the string. The default value is String.Empty.</returns>
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x001045F6 File Offset: 0x001027F6
		// (set) Token: 0x06002A9E RID: 10910 RVA: 0x001045FE File Offset: 0x001027FE
		[XmlAttribute("id", DataType = "ID")]
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		/// <summary>Gets and sets the qualified attributes which do not belong to the schema target namespace.</summary>
		/// <returns>An array of qualified <see cref="T:System.Xml.XmlAttribute" /> objects that do not belong to the schema target namespace.</returns>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x00104607 File Offset: 0x00102807
		// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x0010460F File Offset: 0x0010280F
		[XmlAnyAttribute]
		public XmlAttribute[] UnhandledAttributes
		{
			get
			{
				return this.moreAttributes;
			}
			set
			{
				this.moreAttributes = value;
			}
		}

		/// <summary>Gets the post-schema-compilation value of all the groups in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all the groups in the schema.</returns>
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002AA1 RID: 10913 RVA: 0x00104618 File Offset: 0x00102818
		[XmlIgnore]
		public XmlSchemaObjectTable Groups
		{
			get
			{
				return this.groups;
			}
		}

		/// <summary>Gets the post-schema-compilation value for all notations in the schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaObjectTable" /> collection of all notations in the schema.</returns>
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x00104620 File Offset: 0x00102820
		[XmlIgnore]
		public XmlSchemaObjectTable Notations
		{
			get
			{
				return this.notations;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x00104628 File Offset: 0x00102828
		[XmlIgnore]
		internal XmlSchemaObjectTable IdentityConstraints
		{
			get
			{
				return this.identityConstraints;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x00104630 File Offset: 0x00102830
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x00104638 File Offset: 0x00102838
		[XmlIgnore]
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00104641 File Offset: 0x00102841
		[XmlIgnore]
		internal int SchemaId
		{
			get
			{
				if (this.schemaId == -1)
				{
					this.schemaId = Interlocked.Increment(ref XmlSchema.globalIdCounter);
				}
				return this.schemaId;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x00104662 File Offset: 0x00102862
		// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x0010466A File Offset: 0x0010286A
		[XmlIgnore]
		internal bool IsChameleon
		{
			get
			{
				return this.isChameleon;
			}
			set
			{
				this.isChameleon = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x00104673 File Offset: 0x00102873
		[XmlIgnore]
		internal Hashtable Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x0010467B File Offset: 0x0010287B
		[XmlIgnore]
		internal XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument();
				}
				return this.document;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x00104696 File Offset: 0x00102896
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x0010469E File Offset: 0x0010289E
		[XmlIgnore]
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x001046A8 File Offset: 0x001028A8
		internal new XmlSchema Clone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.includes = this.includes;
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.items = this.items;
			xmlSchema.BaseUri = this.BaseUri;
			SchemaCollectionCompiler.Cleanup(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00104738 File Offset: 0x00102938
		internal XmlSchema DeepClone()
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.attributeFormDefault = this.attributeFormDefault;
			xmlSchema.elementFormDefault = this.elementFormDefault;
			xmlSchema.blockDefault = this.blockDefault;
			xmlSchema.finalDefault = this.finalDefault;
			xmlSchema.targetNs = this.targetNs;
			xmlSchema.version = this.version;
			xmlSchema.isPreprocessed = this.isPreprocessed;
			for (int i = 0; i < this.items.Count; i++)
			{
				XmlSchemaComplexType xmlSchemaComplexType;
				XmlSchemaObject xmlSchemaObject;
				XmlSchemaElement xmlSchemaElement;
				XmlSchemaGroup xmlSchemaGroup;
				if ((xmlSchemaComplexType = this.items[i] as XmlSchemaComplexType) != null)
				{
					xmlSchemaObject = xmlSchemaComplexType.Clone(this);
				}
				else if ((xmlSchemaElement = this.items[i] as XmlSchemaElement) != null)
				{
					xmlSchemaObject = xmlSchemaElement.Clone(this);
				}
				else if ((xmlSchemaGroup = this.items[i] as XmlSchemaGroup) != null)
				{
					xmlSchemaObject = xmlSchemaGroup.Clone(this);
				}
				else
				{
					xmlSchemaObject = this.items[i].Clone();
				}
				xmlSchema.Items.Add(xmlSchemaObject);
			}
			for (int j = 0; j < this.includes.Count; j++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)this.includes[j].Clone();
				xmlSchema.Includes.Add(xmlSchemaExternal);
			}
			xmlSchema.Namespaces = base.Namespaces;
			xmlSchema.BaseUri = this.BaseUri;
			return xmlSchema;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x00104895 File Offset: 0x00102A95
		// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x0010489D File Offset: 0x00102A9D
		[XmlIgnore]
		internal override string IdAttribute
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x001048A6 File Offset: 0x00102AA6
		internal void SetIsCompiled(bool isCompiled)
		{
			this.isCompiled = isCompiled;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0010460F File Offset: 0x0010280F
		internal override void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
			this.moreAttributes = moreAttributes;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x001048AF File Offset: 0x00102AAF
		internal override void AddAnnotation(XmlSchemaAnnotation annotation)
		{
			this.items.Add(annotation);
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x001048BE File Offset: 0x00102ABE
		internal XmlNameTable NameTable
		{
			get
			{
				if (this.nameTable == null)
				{
					this.nameTable = new NameTable();
				}
				return this.nameTable;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x001048D9 File Offset: 0x00102AD9
		internal ArrayList ImportedSchemas
		{
			get
			{
				if (this.importedSchemas == null)
				{
					this.importedSchemas = new ArrayList();
				}
				return this.importedSchemas;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x001048F4 File Offset: 0x00102AF4
		internal ArrayList ImportedNamespaces
		{
			get
			{
				if (this.importedNamespaces == null)
				{
					this.importedNamespaces = new ArrayList();
				}
				return this.importedNamespaces;
			}
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x00104910 File Offset: 0x00102B10
		internal void GetExternalSchemasList(IList extList, XmlSchema schema)
		{
			if (extList.Contains(schema))
			{
				return;
			}
			extList.Add(schema);
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)schema.Includes[i];
				if (xmlSchemaExternal.Schema != null)
				{
					this.GetExternalSchemasList(extList, xmlSchemaExternal.Schema);
				}
			}
		}

		/// <summary>The XML schema namespace. This field is constant.</summary>
		// Token: 0x04001CFD RID: 7421
		public const string Namespace = "http://www.w3.org/2001/XMLSchema";

		/// <summary>The XML schema instance namespace. This field is constant. </summary>
		// Token: 0x04001CFE RID: 7422
		public const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04001CFF RID: 7423
		private XmlSchemaForm attributeFormDefault;

		// Token: 0x04001D00 RID: 7424
		private XmlSchemaForm elementFormDefault;

		// Token: 0x04001D01 RID: 7425
		private XmlSchemaDerivationMethod blockDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04001D02 RID: 7426
		private XmlSchemaDerivationMethod finalDefault = XmlSchemaDerivationMethod.None;

		// Token: 0x04001D03 RID: 7427
		private string targetNs;

		// Token: 0x04001D04 RID: 7428
		private string version;

		// Token: 0x04001D05 RID: 7429
		private XmlSchemaObjectCollection includes = new XmlSchemaObjectCollection();

		// Token: 0x04001D06 RID: 7430
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();

		// Token: 0x04001D07 RID: 7431
		private string id;

		// Token: 0x04001D08 RID: 7432
		private XmlAttribute[] moreAttributes;

		// Token: 0x04001D09 RID: 7433
		private bool isCompiled;

		// Token: 0x04001D0A RID: 7434
		private bool isCompiledBySet;

		// Token: 0x04001D0B RID: 7435
		private bool isPreprocessed;

		// Token: 0x04001D0C RID: 7436
		private bool isRedefined;

		// Token: 0x04001D0D RID: 7437
		private int errorCount;

		// Token: 0x04001D0E RID: 7438
		private XmlSchemaObjectTable attributes;

		// Token: 0x04001D0F RID: 7439
		private XmlSchemaObjectTable attributeGroups = new XmlSchemaObjectTable();

		// Token: 0x04001D10 RID: 7440
		private XmlSchemaObjectTable elements = new XmlSchemaObjectTable();

		// Token: 0x04001D11 RID: 7441
		private XmlSchemaObjectTable types = new XmlSchemaObjectTable();

		// Token: 0x04001D12 RID: 7442
		private XmlSchemaObjectTable groups = new XmlSchemaObjectTable();

		// Token: 0x04001D13 RID: 7443
		private XmlSchemaObjectTable notations = new XmlSchemaObjectTable();

		// Token: 0x04001D14 RID: 7444
		private XmlSchemaObjectTable identityConstraints = new XmlSchemaObjectTable();

		// Token: 0x04001D15 RID: 7445
		private static int globalIdCounter = -1;

		// Token: 0x04001D16 RID: 7446
		private ArrayList importedSchemas;

		// Token: 0x04001D17 RID: 7447
		private ArrayList importedNamespaces;

		// Token: 0x04001D18 RID: 7448
		private int schemaId = -1;

		// Token: 0x04001D19 RID: 7449
		private Uri baseUri;

		// Token: 0x04001D1A RID: 7450
		private bool isChameleon;

		// Token: 0x04001D1B RID: 7451
		private Hashtable ids = new Hashtable();

		// Token: 0x04001D1C RID: 7452
		private XmlDocument document;

		// Token: 0x04001D1D RID: 7453
		private XmlNameTable nameTable;
	}
}
