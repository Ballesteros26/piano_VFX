using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System.Xml.Serialization
{
	/// <summary>Serializes and deserializes objects into and from XML documents. The <see cref="T:System.Xml.Serialization.XmlSerializer" /> enables you to control how objects are encoded into XML.</summary>
	// Token: 0x02000362 RID: 866
	public class XmlSerializer
	{
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x000DAA30 File Offset: 0x000D8C30
		private static XmlSerializerNamespaces DefaultNamespaces
		{
			get
			{
				if (XmlSerializer.defaultNamespaces == null)
				{
					XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
					xmlSerializerNamespaces.AddInternal("xsi", "http://www.w3.org/2001/XMLSchema-instance");
					xmlSerializerNamespaces.AddInternal("xsd", "http://www.w3.org/2001/XMLSchema");
					if (XmlSerializer.defaultNamespaces == null)
					{
						XmlSerializer.defaultNamespaces = xmlSerializerNamespaces;
					}
				}
				return XmlSerializer.defaultNamespaces;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</summary>
		// Token: 0x0600234E RID: 9038 RVA: 0x000020FD File Offset: 0x000002FD
		protected XmlSerializer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of type <see cref="T:System.Object" /> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object" />. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties. </param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
		// Token: 0x0600234F RID: 9039 RVA: 0x000DAA84 File Offset: 0x000D8C84
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace)
			: this(type, overrides, extraTypes, root, defaultNamespace, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize an XML document into object of the specified type. It also specifies the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that represents the XML root element. </param>
		// Token: 0x06002350 RID: 9040 RVA: 0x000DAA94 File Offset: 0x000D8C94
		public XmlSerializer(Type type, XmlRootAttribute root)
			: this(type, null, new Type[0], root, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into object of a specified type. If a property or field returns an array, the <paramref name="extraTypes" /> parameter specifies objects that can be inserted into the array.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize. </param>
		// Token: 0x06002351 RID: 9041 RVA: 0x000DAAA7 File Offset: 0x000D8CA7
		public XmlSerializer(Type type, Type[] extraTypes)
			: this(type, null, extraTypes, null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.</summary>
		/// <param name="type">The type of the object to serialize. </param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" />. </param>
		// Token: 0x06002352 RID: 9042 RVA: 0x000DAAB5 File Offset: 0x000D8CB5
		public XmlSerializer(Type type, XmlAttributeOverrides overrides)
			: this(type, overrides, new Type[0], null, null, null)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class using an object that maps one type to another.</summary>
		/// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x06002353 RID: 9043 RVA: 0x000DAAC8 File Offset: 0x000D8CC8
		public XmlSerializer(XmlTypeMapping xmlTypeMapping)
		{
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(xmlTypeMapping);
			this.mapping = xmlTypeMapping;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		// Token: 0x06002354 RID: 9044 RVA: 0x000DAAE3 File Offset: 0x000D8CE3
		public XmlSerializer(Type type)
			: this(type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Specifies the default namespace for all the XML elements.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize. </param>
		/// <param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
		// Token: 0x06002355 RID: 9045 RVA: 0x000DAAF0 File Offset: 0x000D8CF0
		public XmlSerializer(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.mapping = XmlSerializer.GetKnownMapping(type, defaultNamespace);
			if (this.mapping != null)
			{
				this.primitiveType = type;
				return;
			}
			this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
			if (this.tempAssembly == null)
			{
				TempAssemblyCache tempAssemblyCache = XmlSerializer.cache;
				lock (tempAssemblyCache)
				{
					this.tempAssembly = XmlSerializer.cache[defaultNamespace, type];
					if (this.tempAssembly == null)
					{
						XmlSerializerImplementation xmlSerializerImplementation;
						Assembly assembly = TempAssembly.LoadGeneratedAssembly(type, defaultNamespace, out xmlSerializerImplementation);
						if (assembly == null)
						{
							XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(defaultNamespace);
							this.mapping = xmlReflectionImporter.ImportTypeMapping(type, null, defaultNamespace);
							this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace);
						}
						else
						{
							this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
							this.tempAssembly = new TempAssembly(new XmlMapping[] { this.mapping }, assembly, xmlSerializerImplementation);
						}
					}
					XmlSerializer.cache.Add(defaultNamespace, type, this.tempAssembly);
				}
			}
			if (this.mapping == null)
			{
				this.mapping = XmlReflectionImporter.GetTopLevelMapping(type, defaultNamespace);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of type <see cref="T:System.Object" /> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object" />. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		// Token: 0x06002356 RID: 9046 RVA: 0x000DAC28 File Offset: 0x000D8E28
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location)
			: this(type, overrides, extraTypes, root, defaultNamespace, location, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class that can serialize objects of the specified type into XML document instances, and deserialize XML document instances into objects of the specified type. This overload allows you to supply other types that can be encountered during a serialization or deserialization operation, as well as a default namespace for all XML elements, the class to use as the XML root element, its location, and credentials required for access.</summary>
		/// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can serialize.</param>
		/// <param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides" /> that extends or overrides the behavior of the class specified in the <paramref name="type" /> parameter.</param>
		/// <param name="extraTypes">A <see cref="T:System.Type" /> array of additional object types to serialize.</param>
		/// <param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute" /> that defines the XML root element properties.</param>
		/// <param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param>
		/// <param name="location">The location of the types.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains credentials required to access types.</param>
		// Token: 0x06002357 RID: 9047 RVA: 0x000DAC3C File Offset: 0x000D8E3C
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use a XmlSerializer constructor overload which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public XmlSerializer(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location, Evidence evidence)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter(overrides, defaultNamespace);
			if (extraTypes != null)
			{
				for (int i = 0; i < extraTypes.Length; i++)
				{
					xmlReflectionImporter.IncludeType(extraTypes[i]);
				}
			}
			this.mapping = xmlReflectionImporter.ImportTypeMapping(type, root, defaultNamespace);
			if (location != null || evidence != null)
			{
				this.DemandForUserLocationOrEvidence();
			}
			this.tempAssembly = XmlSerializer.GenerateTempAssembly(this.mapping, type, defaultNamespace, location, evidence);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00002F50 File Offset: 0x00001150
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		private void DemandForUserLocationOrEvidence()
		{
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000DACBD File Offset: 0x000D8EBD
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping)
		{
			return XmlSerializer.GenerateTempAssembly(xmlMapping, null, null);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000DACC7 File Offset: 0x000D8EC7
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace)
		{
			if (xmlMapping == null)
			{
				throw new ArgumentNullException("xmlMapping");
			}
			return new TempAssembly(new XmlMapping[] { xmlMapping }, new Type[] { type }, defaultNamespace, null, null);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000DACF3 File Offset: 0x000D8EF3
		internal static TempAssembly GenerateTempAssembly(XmlMapping xmlMapping, Type type, string defaultNamespace, string location, Evidence evidence)
		{
			return new TempAssembly(new XmlMapping[] { xmlMapping }, new Type[] { type }, defaultNamespace, location, evidence);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		// Token: 0x0600235C RID: 9052 RVA: 0x000DAD12 File Offset: 0x000D8F12
		public void Serialize(TextWriter textWriter, object o)
		{
			this.Serialize(textWriter, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter" /> and references the specified namespaces.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> that contains namespaces for the generated XML document. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x0600235D RID: 9053 RVA: 0x000DAD20 File Offset: 0x000D8F20
		public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(textWriter)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x0600235E RID: 9054 RVA: 0x000DAD4B File Offset: 0x000D8F4B
		public void Serialize(Stream stream, object o)
		{
			this.Serialize(stream, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream" />that references the specified namespaces.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x0600235F RID: 9055 RVA: 0x000DAD58 File Offset: 0x000D8F58
		public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented,
				Indentation = 2
			}, o, namespaces);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002360 RID: 9056 RVA: 0x000DAD84 File Offset: 0x000D8F84
		public void Serialize(XmlWriter xmlWriter, object o)
		{
			this.Serialize(xmlWriter, o, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002361 RID: 9057 RVA: 0x000DAD8F File Offset: 0x000D8F8F
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			this.Serialize(xmlWriter, o, namespaces, null);
		}

		/// <summary>Serializes the specified object and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" /> and references the specified namespaces and encoding style.</summary>
		/// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter" /> used to write the XML document. </param>
		/// <param name="o">The object to serialize. </param>
		/// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> referenced by the object. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002362 RID: 9058 RVA: 0x000DAD9B File Offset: 0x000D8F9B
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
		{
			this.Serialize(xmlWriter, o, namespaces, encodingStyle, null);
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter" />, XML namespaces, and encoding. </summary>
		/// <param name="xmlWriter">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document.</param>
		/// <param name="o">The object to serialize.</param>
		/// <param name="namespaces">An instance of the XmlSerializaerNamespaces that contains namespaces and prefixes to use.</param>
		/// <param name="encodingStyle">The encoding used in the document.</param>
		/// <param name="id">For SOAP encoded messages, the base used to generate id attributes. </param>
		// Token: 0x06002363 RID: 9059 RVA: 0x000DADAC File Offset: 0x000D8FAC
		public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
		{
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.", new object[] { encodingStyle }));
					}
					this.SerializePrimitive(xmlWriter, o, namespaces);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationWriter xmlSerializationWriter = this.CreateWriter();
						xmlSerializationWriter.Init(xmlWriter, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id, this.tempAssembly);
						try
						{
							this.Serialize(o, xmlSerializationWriter);
							goto IL_00B8;
						}
						finally
						{
							xmlSerializationWriter.Dispose();
						}
					}
					this.tempAssembly.InvokeWriter(this.mapping, xmlWriter, o, (namespaces == null || namespaces.Count == 0) ? XmlSerializer.DefaultNamespaces : namespaces, encodingStyle, id);
				}
				IL_00B8:;
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				throw new InvalidOperationException(Res.GetString("There was an error generating the XML document."), innerException);
			}
			xmlWriter.Flush();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> that contains the XML document to deserialize. </param>
		// Token: 0x06002364 RID: 9060 RVA: 0x000DAED0 File Offset: 0x000D90D0
		public object Deserialize(Stream stream)
		{
			return this.Deserialize(new XmlTextReader(stream)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="textReader">The <see cref="T:System.IO.TextReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002365 RID: 9061 RVA: 0x000DAF04 File Offset: 0x000D9104
		public object Deserialize(TextReader textReader)
		{
			return this.Deserialize(new XmlTextReader(textReader)
			{
				WhitespaceHandling = WhitespaceHandling.Significant,
				Normalization = true,
				XmlResolver = null
			}, null);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002366 RID: 9062 RVA: 0x000DAF35 File Offset: 0x000D9135
		public object Deserialize(XmlReader xmlReader)
		{
			return this.Deserialize(xmlReader, null);
		}

		/// <summary>Deserializes an XML document contained by the specified <see cref="T:System.Xml.XmlReader" /> and allows the overriding of events that occur during deserialization.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that contains the document to deserialize.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x06002367 RID: 9063 RVA: 0x000DAF3F File Offset: 0x000D913F
		public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			return this.Deserialize(xmlReader, null, events);
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader" /> and encoding style.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="xmlReader">The <see cref="T:System.xml.XmlReader" /> that contains the XML document to deserialize. </param>
		/// <param name="encodingStyle">The encoding style of the serialized XML. </param>
		/// <exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
		// Token: 0x06002368 RID: 9064 RVA: 0x000DAF4A File Offset: 0x000D914A
		public object Deserialize(XmlReader xmlReader, string encodingStyle)
		{
			return this.Deserialize(xmlReader, encodingStyle, this.events);
		}

		/// <summary>Deserializes the object using the data contained by the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The object being deserialized.</returns>
		/// <param name="xmlReader">An instance of the <see cref="T:System.Xml.XmlReader" /> class used to read the document.</param>
		/// <param name="encodingStyle">The encoding used.</param>
		/// <param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents" /> class. </param>
		// Token: 0x06002369 RID: 9065 RVA: 0x000DAF5C File Offset: 0x000D915C
		public object Deserialize(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
		{
			events.sender = this;
			object obj;
			try
			{
				if (this.primitiveType != null)
				{
					if (encodingStyle != null && encodingStyle.Length > 0)
					{
						throw new InvalidOperationException(Res.GetString("The encoding style '{0}' is not valid for this call because this XmlSerializer instance does not support encoding. Use the SoapReflectionImporter to initialize an XmlSerializer that supports encoding.", new object[] { encodingStyle }));
					}
					obj = this.DeserializePrimitive(xmlReader, events);
				}
				else
				{
					if (this.tempAssembly == null || this.typedSerializer)
					{
						XmlSerializationReader xmlSerializationReader = this.CreateReader();
						xmlSerializationReader.Init(xmlReader, events, encodingStyle, this.tempAssembly);
						try
						{
							return this.Deserialize(xmlSerializationReader);
						}
						finally
						{
							xmlSerializationReader.Dispose();
						}
					}
					obj = this.tempAssembly.InvokeReader(this.mapping, xmlReader, events, encodingStyle);
				}
			}
			catch (Exception innerException)
			{
				if (innerException is ThreadAbortException || innerException is StackOverflowException || innerException is OutOfMemoryException)
				{
					throw;
				}
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				if (xmlReader is IXmlLineInfo)
				{
					IXmlLineInfo xmlLineInfo = (IXmlLineInfo)xmlReader;
					throw new InvalidOperationException(Res.GetString("There is an error in XML document ({0}, {1}).", new object[]
					{
						xmlLineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
						xmlLineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
					}), innerException);
				}
				throw new InvalidOperationException(Res.GetString("There is an error in the XML document."), innerException);
			}
			return obj;
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize a specified XML document.</summary>
		/// <returns>true if this <see cref="T:System.Xml.Serialization.XmlSerializer" /> can deserialize the object that the <see cref="T:System.Xml.XmlReader" /> points to; otherwise, false.</returns>
		/// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader" /> that points to the document to deserialize. </param>
		// Token: 0x0600236A RID: 9066 RVA: 0x000DB0B4 File Offset: 0x000D92B4
		public virtual bool CanDeserialize(XmlReader xmlReader)
		{
			if (this.primitiveType != null)
			{
				TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[this.primitiveType];
				return xmlReader.IsStartElement(typeDesc.DataType.Name, string.Empty);
			}
			return this.tempAssembly != null && this.tempAssembly.CanRead(this.mapping, xmlReader);
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> that maps one type to another. </param>
		// Token: 0x0600236B RID: 9067 RVA: 0x000DB118 File Offset: 0x000D9318
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings)
		{
			return XmlSerializer.FromMappings(mappings, null);
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class from the specified mappings.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the deserialized object.</param>
		// Token: 0x0600236C RID: 9068 RVA: 0x000DB124 File Offset: 0x000D9324
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Type type)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation xmlSerializerImplementation = null;
			if (!(((type == null) ? null : TempAssembly.LoadGeneratedAssembly(type, null, out xmlSerializerImplementation)) == null))
			{
				XmlSerializer[] array = new XmlSerializer[mappings.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[i].Key];
				}
				return array;
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			if (type == null)
			{
				TempAssembly tempAssembly = new TempAssembly(mappings, new Type[] { type }, null, null, null);
				XmlSerializer[] array2 = new XmlSerializer[mappings.Length];
				xmlSerializerImplementation = tempAssembly.Contract;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = (XmlSerializer)xmlSerializerImplementation.TypedSerializers[mappings[j].Key];
					array2[j].SetTempAssembly(tempAssembly, mappings[j]);
				}
				return array2;
			}
			return XmlSerializer.GetSerializersFromCache(mappings, type);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000DB218 File Offset: 0x000D9418
		private static XmlSerializer[] GetSerializersFromCache(XmlMapping[] mappings, Type type)
		{
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			Hashtable hashtable = null;
			Hashtable hashtable2 = XmlSerializer.xmlSerializerTable;
			lock (hashtable2)
			{
				hashtable = XmlSerializer.xmlSerializerTable[type] as Hashtable;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					XmlSerializer.xmlSerializerTable[type] = hashtable;
				}
			}
			hashtable2 = hashtable;
			lock (hashtable2)
			{
				Hashtable hashtable3 = new Hashtable();
				for (int i = 0; i < mappings.Length; i++)
				{
					XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = new XmlSerializer.XmlSerializerMappingKey(mappings[i]);
					array[i] = hashtable[xmlSerializerMappingKey] as XmlSerializer;
					if (array[i] == null)
					{
						hashtable3.Add(xmlSerializerMappingKey, i);
					}
				}
				if (hashtable3.Count > 0)
				{
					XmlMapping[] array2 = new XmlMapping[hashtable3.Count];
					int num = 0;
					foreach (object obj in hashtable3.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey2 = (XmlSerializer.XmlSerializerMappingKey)obj;
						array2[num++] = xmlSerializerMappingKey2.Mapping;
					}
					TempAssembly tempAssembly = new TempAssembly(array2, new Type[] { type }, null, null, null);
					XmlSerializerImplementation contract = tempAssembly.Contract;
					foreach (object obj2 in hashtable3.Keys)
					{
						XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey3 = (XmlSerializer.XmlSerializerMappingKey)obj2;
						num = (int)hashtable3[xmlSerializerMappingKey3];
						array[num] = (XmlSerializer)contract.TypedSerializers[xmlSerializerMappingKey3.Mapping.Key];
						array[num].SetTempAssembly(tempAssembly, xmlSerializerMappingKey3.Mapping);
						hashtable[xmlSerializerMappingKey3] = array[num];
					}
				}
			}
			return array;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class created from mappings of one XML type to another.</summary>
		/// <returns>An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class.</returns>
		/// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to map one type to another.</param>
		/// <param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence" /> class that contains host and assembly data presented to the common language runtime policy system.</param>
		// Token: 0x0600236E RID: 9070 RVA: 0x000DB454 File Offset: 0x000D9654
		[Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of FromMappings which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static XmlSerializer[] FromMappings(XmlMapping[] mappings, Evidence evidence)
		{
			if (mappings == null || mappings.Length == 0)
			{
				return new XmlSerializer[0];
			}
			if (XmlMapping.IsShallow(mappings))
			{
				return new XmlSerializer[0];
			}
			XmlSerializerImplementation contract = new TempAssembly(mappings, new Type[0], null, null, evidence).Contract;
			XmlSerializer[] array = new XmlSerializer[mappings.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (XmlSerializer)contract.TypedSerializers[mappings[i].Key];
			}
			return array;
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that contains serializers for the supplied types and mappings.</returns>
		/// <param name="types">A collection of types.</param>
		/// <param name="mappings">A collection of <see cref="T:System.Xml.Serialization.XmlMapping" /> objects used to convert one type to another.</param>
		// Token: 0x0600236F RID: 9071 RVA: 0x000DB4C8 File Offset: 0x000D96C8
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings)
		{
			return XmlSerializer.GenerateSerializer(types, mappings, new CompilerParameters
			{
				TempFiles = new TempFileCollection(),
				GenerateInMemory = false,
				IncludeDebugInformation = false
			});
		}

		/// <summary>Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings and compiler settings and options. </summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that contains special versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		/// <param name="types">An array of type <see cref="T:System.Type" /> that contains objects used to serialize and deserialize data.</param>
		/// <param name="mappings">An array of type <see cref="T:System.Xml.Serialization.XmlMapping" /> that maps the XML data to the type data.</param>
		/// <param name="parameters">An instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class that represents the parameters used to invoke a compiler.</param>
		// Token: 0x06002370 RID: 9072 RVA: 0x000DB4FC File Offset: 0x000D96FC
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static Assembly GenerateSerializer(Type[] types, XmlMapping[] mappings, CompilerParameters parameters)
		{
			if (types == null || types.Length == 0)
			{
				return null;
			}
			if (mappings == null)
			{
				throw new ArgumentNullException("mappings");
			}
			if (XmlMapping.IsShallow(mappings))
			{
				throw new InvalidOperationException(Res.GetString("This mapping was not crated by reflection importer and cannot be used in this context."));
			}
			Assembly assembly = null;
			foreach (Type type in types)
			{
				if (DynamicAssemblies.IsTypeDynamic(type))
				{
					throw new InvalidOperationException(Res.GetString("Cannot pre-generate serialization assembly for type '{0}'. Pre-generation of serialization assemblies is not supported for dynamic types. Save the assembly and load it from disk to use it with XmlSerialization.", new object[] { type.FullName }));
				}
				if (assembly == null)
				{
					assembly = type.Assembly;
				}
				else if (type.Assembly != assembly)
				{
					throw new ArgumentException(Res.GetString("Cannot pre-generate serializer for multiple assemblies. Type '{0}' does not belong to assembly {1}.", new object[] { type.FullName, assembly.Location }), "types");
				}
			}
			return TempAssembly.GenerateAssembly(mappings, types, null, null, XmlSerializerCompilerParameters.Create(parameters, true), assembly, new Hashtable());
		}

		/// <summary>Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects created from an array of types.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Serialization.XmlSerializer" /> objects.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects. </param>
		// Token: 0x06002371 RID: 9073 RVA: 0x000DB5DC File Offset: 0x000D97DC
		public static XmlSerializer[] FromTypes(Type[] types)
		{
			if (types == null)
			{
				return new XmlSerializer[0];
			}
			XmlReflectionImporter xmlReflectionImporter = new XmlReflectionImporter();
			XmlTypeMapping[] array = new XmlTypeMapping[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = xmlReflectionImporter.ImportTypeMapping(types[i]);
			}
			return XmlSerializer.FromMappings(array);
		}

		/// <summary>Returns the name of the assembly that contains one or more versions of the <see cref="T:System.Xml.Serialization.XmlSerializer" /> especially created to serialize or deserialize the specified type.</summary>
		/// <returns>The name of the assembly that contains an <see cref="T:System.Xml.Serialization.XmlSerializer" /> for the type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are deserializing.</param>
		// Token: 0x06002372 RID: 9074 RVA: 0x000DB622 File Offset: 0x000D9822
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type)
		{
			return XmlSerializer.GetXmlSerializerAssemblyName(type, null);
		}

		/// <summary>Returns the name of the assembly that contains the serializer for the specified type in the specified namespace.</summary>
		/// <returns>The name of the assembly that contains specially built serializers.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> you are interested in.</param>
		/// <param name="defaultNamespace">The namespace of the type.</param>
		// Token: 0x06002373 RID: 9075 RVA: 0x000DB62B File Offset: 0x000D982B
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public static string GetXmlSerializerAssemblyName(Type type, string defaultNamespace)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Compiler.GetTempAssemblyName(type.Assembly.GetName(), defaultNamespace);
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML node of unknown type during deserialization.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06002374 RID: 9076 RVA: 0x000DB652 File Offset: 0x000D9852
		// (remove) Token: 0x06002375 RID: 9077 RVA: 0x000DB670 File Offset: 0x000D9870
		public event XmlNodeEventHandler UnknownNode
		{
			add
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Combine(this.events.OnUnknownNode, value);
			}
			remove
			{
				this.events.OnUnknownNode = (XmlNodeEventHandler)Delegate.Remove(this.events.OnUnknownNode, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML attribute of unknown type during deserialization.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06002376 RID: 9078 RVA: 0x000DB68E File Offset: 0x000D988E
		// (remove) Token: 0x06002377 RID: 9079 RVA: 0x000DB6AC File Offset: 0x000D98AC
		public event XmlAttributeEventHandler UnknownAttribute
		{
			add
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Combine(this.events.OnUnknownAttribute, value);
			}
			remove
			{
				this.events.OnUnknownAttribute = (XmlAttributeEventHandler)Delegate.Remove(this.events.OnUnknownAttribute, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters an XML element of unknown type during deserialization.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06002378 RID: 9080 RVA: 0x000DB6CA File Offset: 0x000D98CA
		// (remove) Token: 0x06002379 RID: 9081 RVA: 0x000DB6E8 File Offset: 0x000D98E8
		public event XmlElementEventHandler UnknownElement
		{
			add
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Combine(this.events.OnUnknownElement, value);
			}
			remove
			{
				this.events.OnUnknownElement = (XmlElementEventHandler)Delegate.Remove(this.events.OnUnknownElement, value);
			}
		}

		/// <summary>Occurs during deserialization of a SOAP-encoded XML stream, when the <see cref="T:System.Xml.Serialization.XmlSerializer" /> encounters a recognized type that is not used or is unreferenced.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600237A RID: 9082 RVA: 0x000DB706 File Offset: 0x000D9906
		// (remove) Token: 0x0600237B RID: 9083 RVA: 0x000DB724 File Offset: 0x000D9924
		public event UnreferencedObjectEventHandler UnreferencedObject
		{
			add
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Combine(this.events.OnUnreferencedObject, value);
			}
			remove
			{
				this.events.OnUnreferencedObject = (UnreferencedObjectEventHandler)Delegate.Remove(this.events.OnUnreferencedObject, value);
			}
		}

		/// <summary>Returns an object used to read the XML document to be serialized.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> used to read the XML document.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600237C RID: 9084 RVA: 0x0000A533 File Offset: 0x00008733
		protected virtual XmlSerializationReader CreateReader()
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes the XML document contained by the specified <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that contains the XML document to deserialize. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600237D RID: 9085 RVA: 0x0000A533 File Offset: 0x00008733
		protected virtual object Deserialize(XmlSerializationReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns a writer used to serialize the object.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> class.</returns>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600237E RID: 9086 RVA: 0x0000A533 File Offset: 0x00008733
		protected virtual XmlSerializationWriter CreateWriter()
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the specified <see cref="T:System.Object" /> and writes the XML document to a file using the specified <see cref="T:System.Xml.Serialization.XmlSerializationWriter" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> to serialize. </param>
		/// <param name="writer">The <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> used to write the XML document. </param>
		/// <exception cref="T:System.NotImplementedException">Any attempt is made to access the method when the method is not overridden in a descendant class. </exception>
		// Token: 0x0600237F RID: 9087 RVA: 0x0000A533 File Offset: 0x00008733
		protected virtual void Serialize(object o, XmlSerializationWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000DB742 File Offset: 0x000D9942
		internal void SetTempAssembly(TempAssembly tempAssembly, XmlMapping mapping)
		{
			this.tempAssembly = tempAssembly;
			this.mapping = mapping;
			this.typedSerializer = true;
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000DB75C File Offset: 0x000D995C
		private static XmlTypeMapping GetKnownMapping(Type type, string ns)
		{
			if (ns != null && ns != string.Empty)
			{
				return null;
			}
			TypeDesc typeDesc = (TypeDesc)TypeScope.PrimtiveTypes[type];
			if (typeDesc == null)
			{
				return null;
			}
			XmlTypeMapping xmlTypeMapping = new XmlTypeMapping(null, new ElementAccessor
			{
				Name = typeDesc.DataType.Name
			});
			xmlTypeMapping.SetKeyInternal(XmlMapping.GenerateKey(type, null, null));
			return xmlTypeMapping;
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000DB7C0 File Offset: 0x000D99C0
		private void SerializePrimitive(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
		{
			XmlSerializationPrimitiveWriter xmlSerializationPrimitiveWriter = new XmlSerializationPrimitiveWriter();
			xmlSerializationPrimitiveWriter.Init(xmlWriter, namespaces, null, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				xmlSerializationPrimitiveWriter.Write_boolean(o);
				return;
			case TypeCode.Char:
				xmlSerializationPrimitiveWriter.Write_char(o);
				return;
			case TypeCode.SByte:
				xmlSerializationPrimitiveWriter.Write_byte(o);
				return;
			case TypeCode.Byte:
				xmlSerializationPrimitiveWriter.Write_unsignedByte(o);
				return;
			case TypeCode.Int16:
				xmlSerializationPrimitiveWriter.Write_short(o);
				return;
			case TypeCode.UInt16:
				xmlSerializationPrimitiveWriter.Write_unsignedShort(o);
				return;
			case TypeCode.Int32:
				xmlSerializationPrimitiveWriter.Write_int(o);
				return;
			case TypeCode.UInt32:
				xmlSerializationPrimitiveWriter.Write_unsignedInt(o);
				return;
			case TypeCode.Int64:
				xmlSerializationPrimitiveWriter.Write_long(o);
				return;
			case TypeCode.UInt64:
				xmlSerializationPrimitiveWriter.Write_unsignedLong(o);
				return;
			case TypeCode.Single:
				xmlSerializationPrimitiveWriter.Write_float(o);
				return;
			case TypeCode.Double:
				xmlSerializationPrimitiveWriter.Write_double(o);
				return;
			case TypeCode.Decimal:
				xmlSerializationPrimitiveWriter.Write_decimal(o);
				return;
			case TypeCode.DateTime:
				xmlSerializationPrimitiveWriter.Write_dateTime(o);
				return;
			case TypeCode.String:
				xmlSerializationPrimitiveWriter.Write_string(o);
				return;
			}
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				xmlSerializationPrimitiveWriter.Write_QName(o);
				return;
			}
			if (this.primitiveType == typeof(byte[]))
			{
				xmlSerializationPrimitiveWriter.Write_base64Binary(o);
				return;
			}
			if (this.primitiveType == typeof(Guid))
			{
				xmlSerializationPrimitiveWriter.Write_guid(o);
				return;
			}
			if (this.primitiveType == typeof(TimeSpan))
			{
				xmlSerializationPrimitiveWriter.Write_TimeSpan(o);
				return;
			}
			throw new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[] { this.primitiveType.FullName }));
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000DB94C File Offset: 0x000D9B4C
		private object DeserializePrimitive(XmlReader xmlReader, XmlDeserializationEvents events)
		{
			XmlSerializationPrimitiveReader xmlSerializationPrimitiveReader = new XmlSerializationPrimitiveReader();
			xmlSerializationPrimitiveReader.Init(xmlReader, events, null, null);
			switch (Type.GetTypeCode(this.primitiveType))
			{
			case TypeCode.Boolean:
				return xmlSerializationPrimitiveReader.Read_boolean();
			case TypeCode.Char:
				return xmlSerializationPrimitiveReader.Read_char();
			case TypeCode.SByte:
				return xmlSerializationPrimitiveReader.Read_byte();
			case TypeCode.Byte:
				return xmlSerializationPrimitiveReader.Read_unsignedByte();
			case TypeCode.Int16:
				return xmlSerializationPrimitiveReader.Read_short();
			case TypeCode.UInt16:
				return xmlSerializationPrimitiveReader.Read_unsignedShort();
			case TypeCode.Int32:
				return xmlSerializationPrimitiveReader.Read_int();
			case TypeCode.UInt32:
				return xmlSerializationPrimitiveReader.Read_unsignedInt();
			case TypeCode.Int64:
				return xmlSerializationPrimitiveReader.Read_long();
			case TypeCode.UInt64:
				return xmlSerializationPrimitiveReader.Read_unsignedLong();
			case TypeCode.Single:
				return xmlSerializationPrimitiveReader.Read_float();
			case TypeCode.Double:
				return xmlSerializationPrimitiveReader.Read_double();
			case TypeCode.Decimal:
				return xmlSerializationPrimitiveReader.Read_decimal();
			case TypeCode.DateTime:
				return xmlSerializationPrimitiveReader.Read_dateTime();
			case TypeCode.String:
				return xmlSerializationPrimitiveReader.Read_string();
			}
			object obj;
			if (this.primitiveType == typeof(XmlQualifiedName))
			{
				obj = xmlSerializationPrimitiveReader.Read_QName();
			}
			else if (this.primitiveType == typeof(byte[]))
			{
				obj = xmlSerializationPrimitiveReader.Read_base64Binary();
			}
			else if (this.primitiveType == typeof(Guid))
			{
				obj = xmlSerializationPrimitiveReader.Read_guid();
			}
			else
			{
				if (!(this.primitiveType == typeof(TimeSpan)) || !LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					throw new InvalidOperationException(Res.GetString("The type {0} was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.", new object[] { this.primitiveType.FullName }));
				}
				obj = xmlSerializationPrimitiveReader.Read_TimeSpan();
			}
			return obj;
		}

		// Token: 0x0400185C RID: 6236
		private TempAssembly tempAssembly;

		// Token: 0x0400185D RID: 6237
		private bool typedSerializer;

		// Token: 0x0400185E RID: 6238
		private Type primitiveType;

		// Token: 0x0400185F RID: 6239
		private XmlMapping mapping;

		// Token: 0x04001860 RID: 6240
		private XmlDeserializationEvents events;

		// Token: 0x04001861 RID: 6241
		private static TempAssemblyCache cache = new TempAssemblyCache();

		// Token: 0x04001862 RID: 6242
		private static volatile XmlSerializerNamespaces defaultNamespaces;

		// Token: 0x04001863 RID: 6243
		private static Hashtable xmlSerializerTable = new Hashtable();

		// Token: 0x02000363 RID: 867
		private class XmlSerializerMappingKey
		{
			// Token: 0x06002385 RID: 9093 RVA: 0x000DBB3B File Offset: 0x000D9D3B
			public XmlSerializerMappingKey(XmlMapping mapping)
			{
				this.Mapping = mapping;
			}

			// Token: 0x06002386 RID: 9094 RVA: 0x000DBB4C File Offset: 0x000D9D4C
			public override bool Equals(object obj)
			{
				XmlSerializer.XmlSerializerMappingKey xmlSerializerMappingKey = obj as XmlSerializer.XmlSerializerMappingKey;
				return xmlSerializerMappingKey != null && !(this.Mapping.Key != xmlSerializerMappingKey.Mapping.Key) && !(this.Mapping.ElementName != xmlSerializerMappingKey.Mapping.ElementName) && !(this.Mapping.Namespace != xmlSerializerMappingKey.Mapping.Namespace) && this.Mapping.IsSoap == xmlSerializerMappingKey.Mapping.IsSoap;
			}

			// Token: 0x06002387 RID: 9095 RVA: 0x000DBBE0 File Offset: 0x000D9DE0
			public override int GetHashCode()
			{
				int num = (this.Mapping.IsSoap ? 0 : 1);
				if (this.Mapping.Key != null)
				{
					num ^= this.Mapping.Key.GetHashCode();
				}
				if (this.Mapping.ElementName != null)
				{
					num ^= this.Mapping.ElementName.GetHashCode();
				}
				if (this.Mapping.Namespace != null)
				{
					num ^= this.Mapping.Namespace.GetHashCode();
				}
				return num;
			}

			// Token: 0x04001864 RID: 6244
			public XmlMapping Mapping;
		}
	}
}
