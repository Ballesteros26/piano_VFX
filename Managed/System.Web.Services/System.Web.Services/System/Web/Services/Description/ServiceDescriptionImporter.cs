using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;

namespace System.Web.Services.Description
{
	/// <summary>Exposes a means of generating client proxy classes for XML Web services.</summary>
	// Token: 0x0200010F RID: 271
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ServiceDescriptionImporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> class.</summary>
		// Token: 0x0600076E RID: 1902 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		public ServiceDescriptionImporter()
		{
			Type[] protocolImporterTypes = WebServicesSection.Current.ProtocolImporterTypes;
			this.importers = new ProtocolImporter[protocolImporterTypes.Length];
			for (int i = 0; i < this.importers.Length; i++)
			{
				this.importers[i] = (ProtocolImporter)Activator.CreateInstance(protocolImporterTypes[i]);
				this.importers[i].Initialize(this);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001D69C File Offset: 0x0001B89C
		internal ServiceDescriptionImporter(CodeCompileUnit codeCompileUnit)
			: this()
		{
			this.codeCompileUnit = codeCompileUnit;
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances to be imported.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> instance that contains the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances slated to be imported by the <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001D6AB File Offset: 0x0001B8AB
		public ServiceDescriptionCollection ServiceDescriptions
		{
			get
			{
				return this.serviceDescriptions;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSchemas" /> used by the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> property.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> object that contains the XML schemas used by the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instances in the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> collection.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001D6B3 File Offset: 0x0001B8B3
		public XmlSchemas Schemas
		{
			get
			{
				return this.schemas;
			}
		}

		/// <summary>Gets or sets a value that determines the style of code (client or server) that is generated when the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values are imported.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> values. The default is <see cref="F:System.Web.Services.Description.ServiceDescriptionImportStyle.Client" />.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001D6BB File Offset: 0x0001B8BB
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001D6C3 File Offset: 0x0001B8C3
		public ServiceDescriptionImportStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		/// <summary>Gets or sets various options for code generation.</summary>
		/// <returns>A member or combination of members of the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		[ComVisible(false)]
		public CodeGenerationOptions CodeGenerationOptions
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001D6DD File Offset: 0x0001B8DD
		internal CodeCompileUnit CodeCompileUnit
		{
			get
			{
				return this.codeCompileUnit;
			}
		}

		/// <summary>Gets or sets the code generator used by the service description importer.</summary>
		/// <returns>The <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface used by the service description importer to generate proxy code.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0001D6E5 File Offset: 0x0001B8E5
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0001D700 File Offset: 0x0001B900
		[ComVisible(false)]
		public CodeDomProvider CodeGenerator
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
			set
			{
				this.codeProvider = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001D709 File Offset: 0x0001B909
		internal List<Type> Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new List<Type>();
				}
				return this.extensions;
			}
		}

		/// <summary>Gets or sets the protocol used to access the described XML Web services.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the case-insensitive name of the protocol to be imported.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001D724 File Offset: 0x0001B924
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001D73A File Offset: 0x0001B93A
		public string ProtocolName
		{
			get
			{
				if (this.protocolName != null)
				{
					return this.protocolName;
				}
				return string.Empty;
			}
			set
			{
				this.protocolName = value;
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001D744 File Offset: 0x0001B944
		private ProtocolImporter FindImporterByName(string protocolName)
		{
			for (int i = 0; i < this.importers.Length; i++)
			{
				ProtocolImporter protocolImporter = this.importers[i];
				if (string.Compare(this.ProtocolName, protocolImporter.ProtocolName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return protocolImporter;
				}
			}
			throw new ArgumentException(Res.GetString("ProtocolWithNameIsNotRecognized1", new object[] { protocolName }), "protocolName");
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001D7A1 File Offset: 0x0001B9A1
		internal XmlSchemas AllSchemas
		{
			get
			{
				return this.allSchemas;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001D7A9 File Offset: 0x0001B9A9
		internal XmlSchemas AbstractSchemas
		{
			get
			{
				return this.abstractSchemas;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001D7B1 File Offset: 0x0001B9B1
		internal XmlSchemas ConcreteSchemas
		{
			get
			{
				return this.concreteSchemas;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the collection of <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values to be imported.</summary>
		/// <param name="serviceDescription">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance to add to the collection </param>
		/// <param name="appSettingUrlKey">Sets the initial value of the Url property of the proxy class to be generated from the instance represented by the <paramref name="serviceDescription" /> parameter. Specifies that it should be generated from the web.config file's &lt;appsetting&gt; section. </param>
		/// <param name="appSettingBaseUrl">Sets the initial value of the Url property of the proxy class to be generated from the instance represented by the <paramref name="serviceDescription" /> parameter. Specifies that it should be constructed from a combination of the value of this parameter and the URL specified by the location attribute in the WSDL document. </param>
		// Token: 0x06000780 RID: 1920 RVA: 0x0001D7B9 File Offset: 0x0001B9B9
		public void AddServiceDescription(ServiceDescription serviceDescription, string appSettingUrlKey, string appSettingBaseUrl)
		{
			if (serviceDescription == null)
			{
				throw new ArgumentNullException("serviceDescription");
			}
			serviceDescription.AppSettingUrlKey = appSettingUrlKey;
			serviceDescription.AppSettingBaseUrl = appSettingBaseUrl;
			this.ServiceDescriptions.Add(serviceDescription);
		}

		/// <summary>Imports the specified <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values, that generates code as specified by the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> value that describes any error that occurred; or 0 if no error occurred.</returns>
		/// <param name="codeNamespace">The namespace into which the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> values are imported. </param>
		/// <param name="codeCompileUnit">The <see cref="T:System.CodeDom.CodeCompileUnit" /> instance in which the code that represents the <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.ServiceDescriptions" /> value is generated. </param>
		// Token: 0x06000781 RID: 1921 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		public ServiceDescriptionImportWarnings Import(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
		{
			if (codeCompileUnit != null)
			{
				codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.Web.Services.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.EnterpriseServices.dll");
			}
			return this.Import(codeNamespace, new ImportContext(new CodeIdentifiers(), false), new Hashtable(), new StringCollection());
		}

		/// <summary>Compiles a collection of Web references to produce a client proxy or a server stub.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> of compiler warnings.</returns>
		/// <param name="webReferences">A <see cref="T:System.Web.Services.Description.WebReferenceCollection" /> of Web references to compile.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that specifies the code provider.</param>
		/// <param name="codeCompileUnit">A <see cref="T:System.CodeDom.CodeCompileUnit" /> that specifies the unit into which code is compiled.</param>
		/// <param name="options">A <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> that specifies code generation options.</param>
		// Token: 0x06000782 RID: 1922 RVA: 0x0001D854 File Offset: 0x0001BA54
		public static StringCollection GenerateWebReferences(WebReferenceCollection webReferences, CodeDomProvider codeProvider, CodeCompileUnit codeCompileUnit, WebReferenceOptions options)
		{
			if (codeCompileUnit != null)
			{
				codeCompileUnit.ReferencedAssemblies.Add("System.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.Xml.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.Web.Services.dll");
				codeCompileUnit.ReferencedAssemblies.Add("System.EnterpriseServices.dll");
			}
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			foreach (object obj in webReferences)
			{
				WebReference webReference = (WebReference)obj;
				ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter(codeCompileUnit);
				XmlSchemas xmlSchemas = new XmlSchemas();
				ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
				foreach (object obj2 in webReference.Documents)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					ServiceDescriptionImporter.AddDocument((string)dictionaryEntry.Key, dictionaryEntry.Value, xmlSchemas, serviceDescriptionCollection, webReference.ValidationWarnings);
				}
				serviceDescriptionImporter.Schemas.Add(xmlSchemas);
				foreach (object obj3 in serviceDescriptionCollection)
				{
					ServiceDescription serviceDescription = (ServiceDescription)obj3;
					serviceDescriptionImporter.AddServiceDescription(serviceDescription, webReference.AppSettingUrlKey, webReference.AppSettingBaseUrl);
				}
				serviceDescriptionImporter.CodeGenerator = codeProvider;
				serviceDescriptionImporter.ProtocolName = webReference.ProtocolName;
				serviceDescriptionImporter.Style = options.Style;
				serviceDescriptionImporter.CodeGenerationOptions = options.CodeGenerationOptions;
				foreach (string text in options.SchemaImporterExtensions)
				{
					serviceDescriptionImporter.Extensions.Add(Type.GetType(text, true));
				}
				ImportContext importContext = ServiceDescriptionImporter.Context(webReference.ProxyCode, hashtable, options.Verbose);
				webReference.Warnings = serviceDescriptionImporter.Import(webReference.ProxyCode, importContext, hashtable2, webReference.ValidationWarnings);
				if (webReference.ValidationWarnings.Count != 0)
				{
					webReference.Warnings |= ServiceDescriptionImportWarnings.SchemaValidation;
				}
			}
			StringCollection stringCollection = new StringCollection();
			if (options.Verbose)
			{
				foreach (object obj4 in hashtable.Values)
				{
					foreach (string text2 in ((ImportContext)obj4).Warnings)
					{
						stringCollection.Add(text2);
					}
				}
			}
			return stringCollection;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		internal static ImportContext Context(CodeNamespace ns, Hashtable namespaces, bool verbose)
		{
			if (namespaces[ns.Name] == null)
			{
				namespaces[ns.Name] = new ImportContext(new CodeIdentifiers(), true);
			}
			return (ImportContext)namespaces[ns.Name];
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		internal static void AddDocument(string path, object document, XmlSchemas schemas, ServiceDescriptionCollection descriptions, StringCollection warnings)
		{
			ServiceDescription serviceDescription = document as ServiceDescription;
			if (serviceDescription != null)
			{
				descriptions.Add(serviceDescription);
				return;
			}
			XmlSchema xmlSchema = document as XmlSchema;
			if (xmlSchema != null)
			{
				schemas.Add(xmlSchema);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001DC18 File Offset: 0x0001BE18
		private void FindUse(MessagePart part, out bool isEncoded, out bool isLiteral)
		{
			isEncoded = false;
			isLiteral = false;
			string name = part.Message.Name;
			Operation operation = null;
			ServiceDescription serviceDescription = part.Message.ServiceDescription;
			foreach (object obj in serviceDescription.PortTypes)
			{
				foreach (object obj2 in ((PortType)obj).Operations)
				{
					Operation operation2 = (Operation)obj2;
					using (IEnumerator enumerator3 = operation2.Messages.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (((OperationMessage)enumerator3.Current).Message.Equals(new XmlQualifiedName(part.Message.Name, serviceDescription.TargetNamespace)))
							{
								operation = operation2;
								this.FindUse(operation, serviceDescription, name, ref isEncoded, ref isLiteral);
							}
						}
					}
				}
			}
			if (operation == null)
			{
				this.FindUse(null, serviceDescription, name, ref isEncoded, ref isLiteral);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001DD64 File Offset: 0x0001BF64
		private void FindUse(Operation operation, ServiceDescription description, string messageName, ref bool isEncoded, ref bool isLiteral)
		{
			string targetNamespace = description.TargetNamespace;
			foreach (object obj in description.Bindings)
			{
				Binding binding = (Binding)obj;
				if (operation == null || new XmlQualifiedName(operation.PortType.Name, targetNamespace).Equals(binding.Type))
				{
					foreach (object obj2 in binding.Operations)
					{
						OperationBinding operationBinding = (OperationBinding)obj2;
						if (operationBinding.Input != null)
						{
							foreach (object obj3 in operationBinding.Input.Extensions)
							{
								if (operation != null)
								{
									SoapBodyBinding soapBodyBinding = obj3 as SoapBodyBinding;
									if (soapBodyBinding != null && operation.IsBoundBy(operationBinding))
									{
										if (soapBodyBinding.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapBodyBinding.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
								else
								{
									SoapHeaderBinding soapHeaderBinding = obj3 as SoapHeaderBinding;
									if (soapHeaderBinding != null && soapHeaderBinding.Message.Name == messageName)
									{
										if (soapHeaderBinding.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapHeaderBinding.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
							}
						}
						if (operationBinding.Output != null)
						{
							foreach (object obj4 in operationBinding.Output.Extensions)
							{
								if (operation != null)
								{
									if (operation.IsBoundBy(operationBinding))
									{
										SoapBodyBinding soapBodyBinding2 = obj4 as SoapBodyBinding;
										if (soapBodyBinding2 != null)
										{
											if (soapBodyBinding2.Use == SoapBindingUse.Encoded)
											{
												isEncoded = true;
											}
											else if (soapBodyBinding2.Use == SoapBindingUse.Literal)
											{
												isLiteral = true;
											}
										}
										else if (obj4 is MimeXmlBinding)
										{
											isLiteral = true;
										}
									}
								}
								else
								{
									SoapHeaderBinding soapHeaderBinding2 = obj4 as SoapHeaderBinding;
									if (soapHeaderBinding2 != null && soapHeaderBinding2.Message.Name == messageName)
									{
										if (soapHeaderBinding2.Use == SoapBindingUse.Encoded)
										{
											isEncoded = true;
										}
										else if (soapHeaderBinding2.Use == SoapBindingUse.Literal)
										{
											isLiteral = true;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001E020 File Offset: 0x0001C220
		private void AddImport(XmlSchema schema, Hashtable imports)
		{
			if (schema == null || imports[schema] != null)
			{
				return;
			}
			imports.Add(schema, schema);
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport)
				{
					XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)xmlSchemaExternal;
					foreach (object obj in this.allSchemas.GetSchemas(xmlSchemaImport.Namespace))
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						this.AddImport(xmlSchema, imports);
					}
				}
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		private ServiceDescriptionImportWarnings Import(CodeNamespace codeNamespace, ImportContext importContext, Hashtable exportContext, StringCollection warnings)
		{
			this.allSchemas = new XmlSchemas();
			foreach (object obj in this.schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				this.allSchemas.Add(xmlSchema);
			}
			foreach (object obj2 in this.serviceDescriptions)
			{
				foreach (object obj3 in ((ServiceDescription)obj2).Types.Schemas)
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj3;
					this.allSchemas.Add(xmlSchema2);
				}
			}
			Hashtable hashtable = new Hashtable();
			if (!this.allSchemas.Contains("http://schemas.xmlsoap.org/wsdl/"))
			{
				this.allSchemas.AddReference(ServiceDescription.Schema);
				hashtable[ServiceDescription.Schema] = ServiceDescription.Schema;
			}
			if (!this.allSchemas.Contains("http://schemas.xmlsoap.org/soap/encoding/"))
			{
				this.allSchemas.AddReference(ServiceDescription.SoapEncodingSchema);
				hashtable[ServiceDescription.SoapEncodingSchema] = ServiceDescription.SoapEncodingSchema;
			}
			this.allSchemas.Compile(null, false);
			foreach (object obj4 in this.serviceDescriptions)
			{
				foreach (object obj5 in ((ServiceDescription)obj4).Messages)
				{
					Message message = (Message)obj5;
					foreach (object obj6 in message.Parts)
					{
						MessagePart messagePart = (MessagePart)obj6;
						bool flag;
						bool flag2;
						this.FindUse(messagePart, out flag, out flag2);
						if (messagePart.Element != null && !messagePart.Element.IsEmpty)
						{
							if (flag)
							{
								throw new InvalidOperationException(Res.GetString("CanTSpecifyElementOnEncodedMessagePartsPart", new object[] { messagePart.Name, message.Name }));
							}
							XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.allSchemas.Find(messagePart.Element, typeof(XmlSchemaElement));
							if (xmlSchemaElement != null)
							{
								ServiceDescriptionImporter.AddSchema(xmlSchemaElement.Parent as XmlSchema, flag, flag2, this.abstractSchemas, this.concreteSchemas, hashtable);
								if (xmlSchemaElement.SchemaTypeName != null && !xmlSchemaElement.SchemaTypeName.IsEmpty)
								{
									XmlSchemaType xmlSchemaType = (XmlSchemaType)this.allSchemas.Find(xmlSchemaElement.SchemaTypeName, typeof(XmlSchemaType));
									if (xmlSchemaType != null)
									{
										ServiceDescriptionImporter.AddSchema(xmlSchemaType.Parent as XmlSchema, flag, flag2, this.abstractSchemas, this.concreteSchemas, hashtable);
									}
								}
							}
						}
						if (messagePart.Type != null && !messagePart.Type.IsEmpty)
						{
							XmlSchemaType xmlSchemaType2 = (XmlSchemaType)this.allSchemas.Find(messagePart.Type, typeof(XmlSchemaType));
							if (xmlSchemaType2 != null)
							{
								ServiceDescriptionImporter.AddSchema(xmlSchemaType2.Parent as XmlSchema, flag, flag2, this.abstractSchemas, this.concreteSchemas, hashtable);
							}
						}
					}
				}
			}
			Hashtable hashtable2;
			foreach (XmlSchemas xmlSchemas in new XmlSchemas[] { this.abstractSchemas, this.concreteSchemas })
			{
				hashtable2 = new Hashtable();
				foreach (object obj7 in xmlSchemas)
				{
					XmlSchema xmlSchema3 = (XmlSchema)obj7;
					this.AddImport(xmlSchema3, hashtable2);
				}
				foreach (object obj8 in hashtable2.Keys)
				{
					XmlSchema xmlSchema4 = (XmlSchema)obj8;
					if (hashtable[xmlSchema4] == null && !xmlSchemas.Contains(xmlSchema4))
					{
						xmlSchemas.Add(xmlSchema4);
					}
				}
			}
			hashtable2 = new Hashtable();
			foreach (object obj9 in this.allSchemas)
			{
				XmlSchema xmlSchema5 = (XmlSchema)obj9;
				if (!this.abstractSchemas.Contains(xmlSchema5) && !this.concreteSchemas.Contains(xmlSchema5))
				{
					this.AddImport(xmlSchema5, hashtable2);
				}
			}
			foreach (object obj10 in hashtable2.Keys)
			{
				XmlSchema xmlSchema6 = (XmlSchema)obj10;
				if (hashtable[xmlSchema6] == null)
				{
					if (!this.abstractSchemas.Contains(xmlSchema6))
					{
						this.abstractSchemas.Add(xmlSchema6);
					}
					if (!this.concreteSchemas.Contains(xmlSchema6))
					{
						this.concreteSchemas.Add(xmlSchema6);
					}
				}
			}
			if (this.abstractSchemas.Count > 0)
			{
				foreach (object obj11 in hashtable.Values)
				{
					XmlSchema xmlSchema7 = (XmlSchema)obj11;
					this.abstractSchemas.AddReference(xmlSchema7);
				}
				foreach (string text in SchemaCompiler.Compile(this.abstractSchemas))
				{
					warnings.Add(text);
				}
			}
			if (this.concreteSchemas.Count > 0)
			{
				foreach (object obj12 in hashtable.Values)
				{
					XmlSchema xmlSchema8 = (XmlSchema)obj12;
					this.concreteSchemas.AddReference(xmlSchema8);
				}
				foreach (string text2 in SchemaCompiler.Compile(this.concreteSchemas))
				{
					warnings.Add(text2);
				}
			}
			if (this.ProtocolName.Length > 0)
			{
				ProtocolImporter protocolImporter = this.FindImporterByName(this.ProtocolName);
				if (protocolImporter.GenerateCode(codeNamespace, importContext, exportContext))
				{
					return protocolImporter.Warnings;
				}
			}
			else
			{
				for (int j = 0; j < this.importers.Length; j++)
				{
					ProtocolImporter protocolImporter2 = this.importers[j];
					if (protocolImporter2.GenerateCode(codeNamespace, importContext, exportContext))
					{
						return protocolImporter2.Warnings;
					}
				}
			}
			return ServiceDescriptionImportWarnings.NoCodeGenerated;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001E938 File Offset: 0x0001CB38
		private static void AddSchema(XmlSchema schema, bool isEncoded, bool isLiteral, XmlSchemas abstractSchemas, XmlSchemas concreteSchemas, Hashtable references)
		{
			if (schema != null)
			{
				if (isEncoded && !abstractSchemas.Contains(schema))
				{
					if (references.Contains(schema))
					{
						abstractSchemas.AddReference(schema);
					}
					else
					{
						abstractSchemas.Add(schema);
					}
				}
				if (isLiteral && !concreteSchemas.Contains(schema))
				{
					if (references.Contains(schema))
					{
						concreteSchemas.AddReference(schema);
						return;
					}
					concreteSchemas.Add(schema);
				}
			}
		}

		// Token: 0x0400042F RID: 1071
		private ServiceDescriptionImportStyle style;

		// Token: 0x04000430 RID: 1072
		private ServiceDescriptionCollection serviceDescriptions = new ServiceDescriptionCollection();

		// Token: 0x04000431 RID: 1073
		private XmlSchemas schemas = new XmlSchemas();

		// Token: 0x04000432 RID: 1074
		private XmlSchemas allSchemas = new XmlSchemas();

		// Token: 0x04000433 RID: 1075
		private string protocolName;

		// Token: 0x04000434 RID: 1076
		private CodeGenerationOptions options = CodeGenerationOptions.GenerateOldAsync;

		// Token: 0x04000435 RID: 1077
		private CodeCompileUnit codeCompileUnit;

		// Token: 0x04000436 RID: 1078
		private CodeDomProvider codeProvider;

		// Token: 0x04000437 RID: 1079
		private ProtocolImporter[] importers;

		// Token: 0x04000438 RID: 1080
		private XmlSchemas abstractSchemas = new XmlSchemas();

		// Token: 0x04000439 RID: 1081
		private XmlSchemas concreteSchemas = new XmlSchemas();

		// Token: 0x0400043A RID: 1082
		private List<Type> extensions;
	}
}
