using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the webServices element in the configuration file. This element controls the settings of XML Web services.</summary>
	// Token: 0x0200014A RID: 330
	public sealed class WebServicesSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> class.</summary>
		// Token: 0x06000A10 RID: 2576 RVA: 0x00043F60 File Offset: 0x00042160
		public WebServicesSection()
		{
			this.properties.Add(this.conformanceWarnings);
			this.properties.Add(this.protocols);
			this.properties.Add(this.serviceDescriptionFormatExtensionTypes);
			this.properties.Add(this.soapEnvelopeProcessing);
			this.properties.Add(this.soapExtensionImporterTypes);
			this.properties.Add(this.soapExtensionReflectorTypes);
			this.properties.Add(this.soapExtensionTypes);
			this.properties.Add(this.soapTransportImporterTypes);
			this.properties.Add(this.wsdlHelpGenerator);
			this.properties.Add(this.soapServerProtocolFactoryType);
			this.properties.Add(this.diagnostics);
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00044388 File Offset: 0x00042588
		private static object ClassSyncObject
		{
			get
			{
				if (WebServicesSection.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref WebServicesSection.classSyncObject, obj, null);
				}
				return WebServicesSection.classSyncObject;
			}
		}

		/// <summary>Gets the collection of conformance warnings for the Web Service. This property corresponds to the configurationWarnings element in the configuration file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WsiProfilesElementCollection" /> object that represents the collection of conformance warnings for the Web Service.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000443B4 File Offset: 0x000425B4
		[ConfigurationProperty("conformanceWarnings")]
		public WsiProfilesElementCollection ConformanceWarnings
		{
			get
			{
				return (WsiProfilesElementCollection)base[this.conformanceWarnings];
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000443C8 File Offset: 0x000425C8
		internal WsiProfiles EnabledConformanceWarnings
		{
			get
			{
				WsiProfiles wsiProfiles = WsiProfiles.None;
				foreach (object obj in this.ConformanceWarnings)
				{
					WsiProfilesElement wsiProfilesElement = (WsiProfilesElement)obj;
					wsiProfiles |= wsiProfilesElement.Name;
				}
				return wsiProfiles;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object that represents the current section.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00044428 File Offset: 0x00042628
		public static WebServicesSection Current
		{
			get
			{
				WebServicesSection webServicesSection = null;
				if (Thread.GetDomain().GetData(".appDomain") != null)
				{
					webServicesSection = WebServicesSection.GetConfigFromHttpContext();
				}
				if (webServicesSection == null)
				{
					webServicesSection = (WebServicesSection)PrivilegedConfigurationManager.GetSection("system.web/webServices");
				}
				return webServicesSection;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00044464 File Offset: 0x00042664
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static WebServicesSection GetConfigFromHttpContext()
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			HttpContext httpContext = HttpContext.Current;
			if (httpContext != null)
			{
				return (WebServicesSection)httpContext.GetSection("system.web/webServices");
			}
			return null;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00044494 File Offset: 0x00042694
		internal XmlSerializer DiscoveryDocumentSerializer
		{
			get
			{
				if (this.discoveryDocumentSerializer == null)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.discoveryDocumentSerializer == null)
						{
							XmlAttributeOverrides xmlAttributeOverrides = new XmlAttributeOverrides();
							XmlAttributes xmlAttributes = new XmlAttributes();
							foreach (Type type in this.DiscoveryReferenceTypes)
							{
								object[] customAttributes = type.GetCustomAttributes(typeof(XmlRootAttribute), false);
								if (customAttributes.Length == 0)
								{
									throw new InvalidOperationException(Res.GetString("WebMissingCustomAttribute", new object[] { type.FullName, "XmlRoot" }));
								}
								string elementName = ((XmlRootAttribute)customAttributes[0]).ElementName;
								string @namespace = ((XmlRootAttribute)customAttributes[0]).Namespace;
								XmlElementAttribute xmlElementAttribute = new XmlElementAttribute(elementName, type);
								xmlElementAttribute.Namespace = @namespace;
								xmlAttributes.XmlElements.Add(xmlElementAttribute);
							}
							xmlAttributeOverrides.Add(typeof(DiscoveryDocument), "References", xmlAttributes);
							this.discoveryDocumentSerializer = new DiscoveryDocumentSerializer();
						}
					}
				}
				return this.discoveryDocumentSerializer;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000445C4 File Offset: 0x000427C4
		internal Type[] DiscoveryReferenceTypes
		{
			get
			{
				return this.discoveryReferenceTypes;
			}
		}

		/// <summary>Gets one of the <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> values that indicates the Web service protocol.</summary>
		/// <returns>One of the <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> values.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000445CC File Offset: 0x000427CC
		public WebServiceProtocols EnabledProtocols
		{
			get
			{
				if (this.enabledProtocols == WebServiceProtocols.Unknown)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.enabledProtocols == WebServiceProtocols.Unknown)
						{
							WebServiceProtocols webServiceProtocols = WebServiceProtocols.Unknown;
							foreach (object obj2 in this.Protocols)
							{
								ProtocolElement protocolElement = (ProtocolElement)obj2;
								webServiceProtocols |= protocolElement.Name;
							}
							this.enabledProtocols = webServiceProtocols;
						}
					}
				}
				return this.enabledProtocols;
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00044674 File Offset: 0x00042874
		internal Type[] GetAllFormatExtensionTypes()
		{
			if (this.ServiceDescriptionFormatExtensionTypes.Count == 0)
			{
				return this.defaultFormatTypes;
			}
			Type[] array = new Type[this.defaultFormatTypes.Length + this.ServiceDescriptionFormatExtensionTypes.Count];
			Array.Copy(this.defaultFormatTypes, array, this.defaultFormatTypes.Length);
			for (int i = 0; i < this.ServiceDescriptionFormatExtensionTypes.Count; i++)
			{
				array[i + this.defaultFormatTypes.Length] = this.ServiceDescriptionFormatExtensionTypes[i].Type;
			}
			return array;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000446F8 File Offset: 0x000428F8
		private static XmlFormatExtensionPointAttribute GetExtensionPointAttribute(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(XmlFormatExtensionPointAttribute), false);
			if (customAttributes.Length == 0)
			{
				throw new ArgumentException(Res.GetString("TheSyntaxOfTypeMayNotBeExtended1", new object[] { type.FullName }), "type");
			}
			return (XmlFormatExtensionPointAttribute)customAttributes[0];
		}

		/// <summary>Retrieves the specified configuration section.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object that represents the section being retrieved.</returns>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> object that represents the section to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="config" /> is null.</exception>
		// Token: 0x06000A1B RID: 2587 RVA: 0x00044747 File Offset: 0x00042947
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		public static WebServicesSection GetSection(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return (WebServicesSection)config.GetSection("system.web/webServices");
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00044768 File Offset: 0x00042968
		protected override void InitializeDefault()
		{
			this.ConformanceWarnings.SetDefaults();
			this.Protocols.SetDefaults();
			if (Thread.GetDomain().GetData(".appDomain") != null)
			{
				this.WsdlHelpGenerator.SetDefaults();
			}
			this.SoapServerProtocolFactoryType.Type = typeof(SoapServerProtocolFactory);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000447BC File Offset: 0x000429BC
		internal static void LoadXmlFormatExtensions(Type[] extensionTypes, XmlAttributeOverrides overrides, XmlSerializerNamespaces namespaces)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add(typeof(ServiceDescription), new XmlAttributes());
			hashtable.Add(typeof(Import), new XmlAttributes());
			hashtable.Add(typeof(Port), new XmlAttributes());
			hashtable.Add(typeof(Service), new XmlAttributes());
			hashtable.Add(typeof(FaultBinding), new XmlAttributes());
			hashtable.Add(typeof(InputBinding), new XmlAttributes());
			hashtable.Add(typeof(OutputBinding), new XmlAttributes());
			hashtable.Add(typeof(OperationBinding), new XmlAttributes());
			hashtable.Add(typeof(Binding), new XmlAttributes());
			hashtable.Add(typeof(OperationFault), new XmlAttributes());
			hashtable.Add(typeof(OperationInput), new XmlAttributes());
			hashtable.Add(typeof(OperationOutput), new XmlAttributes());
			hashtable.Add(typeof(Operation), new XmlAttributes());
			hashtable.Add(typeof(PortType), new XmlAttributes());
			hashtable.Add(typeof(Message), new XmlAttributes());
			hashtable.Add(typeof(MessagePart), new XmlAttributes());
			hashtable.Add(typeof(Types), new XmlAttributes());
			Hashtable hashtable2 = new Hashtable();
			foreach (Type type in extensionTypes)
			{
				if (hashtable2[type] == null)
				{
					hashtable2.Add(type, type);
					object[] array = type.GetCustomAttributes(typeof(XmlFormatExtensionAttribute), false);
					if (array.Length == 0)
					{
						throw new ArgumentException(Res.GetString("RequiredXmlFormatExtensionAttributeIsMissing1", new object[] { type.FullName }), "extensionTypes");
					}
					XmlFormatExtensionAttribute xmlFormatExtensionAttribute = (XmlFormatExtensionAttribute)array[0];
					foreach (Type type2 in xmlFormatExtensionAttribute.ExtensionPoints)
					{
						XmlAttributes xmlAttributes = (XmlAttributes)hashtable[type2];
						if (xmlAttributes == null)
						{
							xmlAttributes = new XmlAttributes();
							hashtable.Add(type2, xmlAttributes);
						}
						XmlElementAttribute xmlElementAttribute = new XmlElementAttribute(xmlFormatExtensionAttribute.ElementName, type);
						xmlElementAttribute.Namespace = xmlFormatExtensionAttribute.Namespace;
						xmlAttributes.XmlElements.Add(xmlElementAttribute);
					}
					array = type.GetCustomAttributes(typeof(XmlFormatExtensionPrefixAttribute), false);
					string[] array2 = new string[array.Length];
					Hashtable hashtable3 = new Hashtable();
					for (int k = 0; k < array.Length; k++)
					{
						XmlFormatExtensionPrefixAttribute xmlFormatExtensionPrefixAttribute = (XmlFormatExtensionPrefixAttribute)array[k];
						array2[k] = xmlFormatExtensionPrefixAttribute.Prefix;
						hashtable3.Add(xmlFormatExtensionPrefixAttribute.Prefix, xmlFormatExtensionPrefixAttribute.Namespace);
					}
					Array.Sort(array2, InvariantComparer.Default);
					for (int l = 0; l < array2.Length; l++)
					{
						namespaces.Add(array2[l], (string)hashtable3[array2[l]]);
					}
				}
			}
			foreach (object obj in hashtable.Keys)
			{
				Type type3 = (Type)obj;
				XmlFormatExtensionPointAttribute extensionPointAttribute = WebServicesSection.GetExtensionPointAttribute(type3);
				XmlAttributes xmlAttributes2 = (XmlAttributes)hashtable[type3];
				if (extensionPointAttribute.AllowElements)
				{
					xmlAttributes2.XmlAnyElements.Add(new XmlAnyElementAttribute());
				}
				overrides.Add(type3, extensionPointAttribute.MemberName, xmlAttributes2);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00044B5C File Offset: 0x00042D5C
		internal Type[] MimeImporterTypes
		{
			get
			{
				return this.mimeImporterTypes;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00044B64 File Offset: 0x00042D64
		internal Type[] MimeReflectorTypes
		{
			get
			{
				return this.mimeReflectorTypes;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00044B6C File Offset: 0x00042D6C
		internal Type[] ParameterReaderTypes
		{
			get
			{
				return this.parameterReaderTypes;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00044B74 File Offset: 0x00042D74
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00044B7C File Offset: 0x00042D7C
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00044C3C File Offset: 0x00042E3C
		internal Type[] ProtocolImporterTypes
		{
			get
			{
				if (this.protocolImporterTypes.Length == 0)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.protocolImporterTypes.Length == 0)
						{
							WebServiceProtocols webServiceProtocols = this.EnabledProtocols;
							List<Type> list = new List<Type>();
							if ((webServiceProtocols & WebServiceProtocols.HttpSoap) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(SoapProtocolImporter));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpSoap12) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(Soap12ProtocolImporter));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpGet) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(HttpGetProtocolImporter));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpPost) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(HttpPostProtocolImporter));
							}
							this.protocolImporterTypes = list.ToArray();
						}
					}
				}
				return this.protocolImporterTypes;
			}
			set
			{
				this.protocolImporterTypes = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00044C48 File Offset: 0x00042E48
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00044D08 File Offset: 0x00042F08
		internal Type[] ProtocolReflectorTypes
		{
			get
			{
				if (this.protocolReflectorTypes.Length == 0)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.protocolReflectorTypes.Length == 0)
						{
							WebServiceProtocols webServiceProtocols = this.EnabledProtocols;
							List<Type> list = new List<Type>();
							if ((webServiceProtocols & WebServiceProtocols.HttpSoap) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(SoapProtocolReflector));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpSoap12) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(Soap12ProtocolReflector));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpGet) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(HttpGetProtocolReflector));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpPost) != WebServiceProtocols.Unknown)
							{
								list.Add(typeof(HttpPostProtocolReflector));
							}
							this.protocolReflectorTypes = list.ToArray();
						}
					}
				}
				return this.protocolReflectorTypes;
			}
			set
			{
				this.protocolReflectorTypes = value;
			}
		}

		/// <summary>Gets the transmission protocol that is used to decrypt data sent from a client browser in an HTTP request.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> object that represents the transmission protocol that is used to decrypt data sent from a client browser in an HTTP request.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00044D11 File Offset: 0x00042F11
		[ConfigurationProperty("protocols")]
		public ProtocolElementCollection Protocols
		{
			get
			{
				return (ProtocolElementCollection)base[this.protocols];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> for the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.SoapEnvelopeProcessingElement" /> for the current configuration file.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00044D24 File Offset: 0x00042F24
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00044D37 File Offset: 0x00042F37
		[ConfigurationProperty("soapEnvelopeProcessing")]
		public SoapEnvelopeProcessingElement SoapEnvelopeProcessing
		{
			get
			{
				return (SoapEnvelopeProcessingElement)base[this.soapEnvelopeProcessing];
			}
			set
			{
				base[this.soapEnvelopeProcessing] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> for the <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> for the current configuration file.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00044D46 File Offset: 0x00042F46
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00044D59 File Offset: 0x00042F59
		public DiagnosticsElement Diagnostics
		{
			get
			{
				return (DiagnosticsElement)base[this.diagnostics];
			}
			set
			{
				base[this.diagnostics] = value;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00044D68 File Offset: 0x00042F68
		protected override void Reset(ConfigurationElement parentElement)
		{
			this.serverProtocolFactories = null;
			this.enabledProtocols = WebServiceProtocols.Unknown;
			if (parentElement != null)
			{
				WebServicesSection webServicesSection = (WebServicesSection)parentElement;
				this.discoveryDocumentSerializer = webServicesSection.discoveryDocumentSerializer;
			}
			base.Reset(parentElement);
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00044DA0 File Offset: 0x00042FA0
		internal Type[] ReturnWriterTypes
		{
			get
			{
				return this.returnWriterTypes;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00044DA8 File Offset: 0x00042FA8
		internal ServerProtocolFactory[] ServerProtocolFactories
		{
			get
			{
				if (this.serverProtocolFactories == null)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.serverProtocolFactories == null)
						{
							WebServiceProtocols webServiceProtocols = this.EnabledProtocols;
							List<ServerProtocolFactory> list = new List<ServerProtocolFactory>();
							if ((webServiceProtocols & WebServiceProtocols.AnyHttpSoap) != WebServiceProtocols.Unknown)
							{
								list.Add((ServerProtocolFactory)Activator.CreateInstance(this.SoapServerProtocolFactory));
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpPost) != WebServiceProtocols.Unknown)
							{
								list.Add(new HttpPostServerProtocolFactory());
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpPostLocalhost) != WebServiceProtocols.Unknown)
							{
								list.Add(new HttpPostLocalhostServerProtocolFactory());
							}
							if ((webServiceProtocols & WebServiceProtocols.HttpGet) != WebServiceProtocols.Unknown)
							{
								list.Add(new HttpGetServerProtocolFactory());
							}
							if ((webServiceProtocols & WebServiceProtocols.Documentation) != WebServiceProtocols.Unknown)
							{
								list.Add(new DiscoveryServerProtocolFactory());
								list.Add(new DocumentationServerProtocolFactory());
							}
							this.serverProtocolFactories = list.ToArray();
						}
					}
				}
				return this.serverProtocolFactories;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00044E7C File Offset: 0x0004307C
		internal bool ServiceDescriptionExtended
		{
			get
			{
				return this.ServiceDescriptionFormatExtensionTypes.Count > 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the service description format extension to run within the scope of the configuration file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the service description format extension to run within the scope of the configuration file.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00044E8C File Offset: 0x0004308C
		[ConfigurationProperty("serviceDescriptionFormatExtensionTypes")]
		public TypeElementCollection ServiceDescriptionFormatExtensionTypes
		{
			get
			{
				return (TypeElementCollection)base[this.serviceDescriptionFormatExtensionTypes];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description for an XML Web service within the scope of the configuration file is accessed.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description for an XML Web service within the scope of the configuration file is accessed.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00044E9F File Offset: 0x0004309F
		[ConfigurationProperty("soapExtensionImporterTypes")]
		public TypeElementCollection SoapExtensionImporterTypes
		{
			get
			{
				return (TypeElementCollection)base[this.soapExtensionImporterTypes];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description is generated for all XML Web services within the scope of the configuration file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> that specifies the SOAP extensions to run when a service description is generated for all XML Web services within the scope of the configuration file.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00044EB2 File Offset: 0x000430B2
		[ConfigurationProperty("soapExtensionReflectorTypes")]
		public TypeElementCollection SoapExtensionReflectorTypes
		{
			get
			{
				return (TypeElementCollection)base[this.soapExtensionReflectorTypes];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElementCollection" /> that specifies the SOAP extensions to run with all XML Web services within the scope of the configuration file.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElementCollection" /> that specifies the SOAP extensions to run with all XML Web services within the scope of the configuration file.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00044EC5 File Offset: 0x000430C5
		[ConfigurationProperty("soapExtensionTypes")]
		public SoapExtensionTypeElementCollection SoapExtensionTypes
		{
			get
			{
				return (SoapExtensionTypeElementCollection)base[this.soapExtensionTypes];
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.TypeElement" /> object that corresponds to the protocol used to call the Web service.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElement" /> object that corresponds to the protocol used to call the Web service.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00044ED8 File Offset: 0x000430D8
		[ConfigurationProperty("soapServerProtocolFactory")]
		public TypeElement SoapServerProtocolFactoryType
		{
			get
			{
				return (TypeElement)base[this.soapServerProtocolFactoryType];
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00044EEC File Offset: 0x000430EC
		internal Type SoapServerProtocolFactory
		{
			get
			{
				if (this.soapServerProtocolFactory == null)
				{
					object obj = WebServicesSection.ClassSyncObject;
					lock (obj)
					{
						if (this.soapServerProtocolFactory == null)
						{
							this.soapServerProtocolFactory = this.SoapServerProtocolFactoryType.Type;
						}
					}
				}
				return this.soapServerProtocolFactory;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> object that represents the SoapTransportImporterTypes configuration element.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.TypeElementCollection" /> object that represents the SoapTransportImporterTypes configuration element.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00044F58 File Offset: 0x00043158
		[ConfigurationProperty("soapTransportImporterTypes")]
		public TypeElementCollection SoapTransportImporterTypes
		{
			get
			{
				return (TypeElementCollection)base[this.soapTransportImporterTypes];
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00044F6C File Offset: 0x0004316C
		internal Type[] SoapTransportImporters
		{
			get
			{
				Type[] array = new Type[1 + this.SoapTransportImporterTypes.Count];
				array[0] = typeof(SoapHttpTransportImporter);
				for (int i = 0; i < this.SoapTransportImporterTypes.Count; i++)
				{
					array[i + 1] = this.SoapTransportImporterTypes[i].Type;
				}
				return array;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00044FC8 File Offset: 0x000431C8
		private void TurnOnGetAndPost()
		{
			bool flag = (this.EnabledProtocols & WebServiceProtocols.HttpPost) == WebServiceProtocols.Unknown;
			bool flag2 = (this.EnabledProtocols & WebServiceProtocols.HttpGet) == WebServiceProtocols.Unknown;
			if (!flag2 && !flag)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this.ProtocolImporterTypes);
			ArrayList arrayList2 = new ArrayList(this.ProtocolReflectorTypes);
			if (flag)
			{
				arrayList.Add(typeof(HttpPostProtocolImporter));
				arrayList2.Add(typeof(HttpPostProtocolReflector));
			}
			if (flag2)
			{
				arrayList.Add(typeof(HttpGetProtocolImporter));
				arrayList2.Add(typeof(HttpGetProtocolReflector));
			}
			this.ProtocolImporterTypes = (Type[])arrayList.ToArray(typeof(Type));
			this.ProtocolReflectorTypes = (Type[])arrayList2.ToArray(typeof(Type));
			this.enabledProtocols |= WebServiceProtocols.HttpGet | WebServiceProtocols.HttpPost;
		}

		/// <summary>Gets the Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX page.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Configuration.WsdlHelpGeneratorElement" /> object that specifies the XML Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX XML Web service page.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0004509A File Offset: 0x0004329A
		[ConfigurationProperty("wsdlHelpGenerator")]
		public WsdlHelpGeneratorElement WsdlHelpGenerator
		{
			get
			{
				return (WsdlHelpGeneratorElement)base[this.wsdlHelpGenerator];
			}
		}

		// Token: 0x040005B3 RID: 1459
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040005B4 RID: 1460
		private static object classSyncObject;

		// Token: 0x040005B5 RID: 1461
		private const string SectionName = "system.web/webServices";

		// Token: 0x040005B6 RID: 1462
		private readonly ConfigurationProperty conformanceWarnings = new ConfigurationProperty("conformanceWarnings", typeof(WsiProfilesElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005B7 RID: 1463
		private readonly ConfigurationProperty protocols = new ConfigurationProperty("protocols", typeof(ProtocolElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005B8 RID: 1464
		private readonly ConfigurationProperty serviceDescriptionFormatExtensionTypes = new ConfigurationProperty("serviceDescriptionFormatExtensionTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005B9 RID: 1465
		private readonly ConfigurationProperty soapEnvelopeProcessing = new ConfigurationProperty("soapEnvelopeProcessing", typeof(SoapEnvelopeProcessingElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BA RID: 1466
		private readonly ConfigurationProperty soapExtensionImporterTypes = new ConfigurationProperty("soapExtensionImporterTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BB RID: 1467
		private readonly ConfigurationProperty soapExtensionReflectorTypes = new ConfigurationProperty("soapExtensionReflectorTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BC RID: 1468
		private readonly ConfigurationProperty soapExtensionTypes = new ConfigurationProperty("soapExtensionTypes", typeof(SoapExtensionTypeElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BD RID: 1469
		private readonly ConfigurationProperty soapTransportImporterTypes = new ConfigurationProperty("soapTransportImporterTypes", typeof(TypeElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BE RID: 1470
		private readonly ConfigurationProperty wsdlHelpGenerator = new ConfigurationProperty("wsdlHelpGenerator", typeof(WsdlHelpGeneratorElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005BF RID: 1471
		private readonly ConfigurationProperty soapServerProtocolFactoryType = new ConfigurationProperty("soapServerProtocolFactory", typeof(TypeElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005C0 RID: 1472
		private readonly ConfigurationProperty diagnostics = new ConfigurationProperty("diagnostics", typeof(DiagnosticsElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x040005C1 RID: 1473
		private Type[] defaultFormatTypes = new Type[]
		{
			typeof(HttpAddressBinding),
			typeof(HttpBinding),
			typeof(HttpOperationBinding),
			typeof(HttpUrlEncodedBinding),
			typeof(HttpUrlReplacementBinding),
			typeof(MimeContentBinding),
			typeof(MimeXmlBinding),
			typeof(MimeMultipartRelatedBinding),
			typeof(MimeTextBinding),
			typeof(global::System.Web.Services.Description.SoapBinding),
			typeof(SoapOperationBinding),
			typeof(SoapBodyBinding),
			typeof(SoapFaultBinding),
			typeof(SoapHeaderBinding),
			typeof(SoapAddressBinding),
			typeof(Soap12Binding),
			typeof(Soap12OperationBinding),
			typeof(Soap12BodyBinding),
			typeof(Soap12FaultBinding),
			typeof(Soap12HeaderBinding),
			typeof(Soap12AddressBinding)
		};

		// Token: 0x040005C2 RID: 1474
		private Type[] discoveryReferenceTypes = new Type[]
		{
			typeof(DiscoveryDocumentReference),
			typeof(ContractReference),
			typeof(SchemaReference),
			typeof(global::System.Web.Services.Discovery.SoapBinding)
		};

		// Token: 0x040005C3 RID: 1475
		private XmlSerializer discoveryDocumentSerializer;

		// Token: 0x040005C4 RID: 1476
		private WebServiceProtocols enabledProtocols;

		// Token: 0x040005C5 RID: 1477
		private Type[] mimeImporterTypes = new Type[]
		{
			typeof(MimeXmlImporter),
			typeof(MimeFormImporter),
			typeof(MimeTextImporter)
		};

		// Token: 0x040005C6 RID: 1478
		private Type[] mimeReflectorTypes = new Type[]
		{
			typeof(MimeXmlReflector),
			typeof(MimeFormReflector)
		};

		// Token: 0x040005C7 RID: 1479
		private Type[] parameterReaderTypes = new Type[]
		{
			typeof(UrlParameterReader),
			typeof(HtmlFormParameterReader)
		};

		// Token: 0x040005C8 RID: 1480
		private Type[] protocolImporterTypes = new Type[0];

		// Token: 0x040005C9 RID: 1481
		private Type[] protocolReflectorTypes = new Type[0];

		// Token: 0x040005CA RID: 1482
		private Type[] returnWriterTypes = new Type[] { typeof(XmlReturnWriter) };

		// Token: 0x040005CB RID: 1483
		private ServerProtocolFactory[] serverProtocolFactories;

		// Token: 0x040005CC RID: 1484
		private Type soapServerProtocolFactory;
	}
}
