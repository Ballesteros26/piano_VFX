using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a reference in a discovery document to a service description.</summary>
	// Token: 0x0200009B RID: 155
	[XmlRoot("contractRef", Namespace = "http://schemas.xmlsoap.org/disco/scl/")]
	public class ContractReference : DiscoveryReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using default values.</summary>
		// Token: 0x060003EF RID: 1007 RVA: 0x000123CF File Offset: 0x000105CF
		public ContractReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using the supplied reference to a Service Description.</summary>
		/// <param name="href">The URL for a Sevice Descritpion. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property value. </param>
		// Token: 0x060003F0 RID: 1008 RVA: 0x000123D7 File Offset: 0x000105D7
		public ContractReference(string href)
		{
			this.Ref = href;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using the supplied references to a service description and a XML Web service implementing the service description.</summary>
		/// <param name="href">The URL for a service description. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property value. </param>
		/// <param name="docRef">The URL for a XML Web service implementing the service description at <paramref name="href" />. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.DocRef" /> property value. </param>
		// Token: 0x060003F1 RID: 1009 RVA: 0x000123E6 File Offset: 0x000105E6
		public ContractReference(string href, string docRef)
		{
			this.Ref = href;
			this.DocRef = docRef;
		}

		/// <summary>Gets or sets the URL to the referenced service description.</summary>
		/// <returns>The URL to the referenced service description.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000123FC File Offset: 0x000105FC
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00012404 File Offset: 0x00010604
		[XmlAttribute("ref")]
		public string Ref
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		/// <summary>Gets and sets the URL for a XML Web service implementing the service description referenced in the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property.</summary>
		/// <returns>The URL for a XML Web service implementing the Service Description referenced in the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001240D File Offset: 0x0001060D
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00012415 File Offset: 0x00010615
		[XmlAttribute("docRef")]
		public string DocRef
		{
			get
			{
				return this.docRef;
			}
			set
			{
				this.docRef = value;
			}
		}

		/// <summary>Gets or sets the URL for the referenced Service Description.</summary>
		/// <returns>The URL for the referenced service description.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001241E File Offset: 0x0001061E
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00012426 File Offset: 0x00010626
		[XmlIgnore]
		public override string Url
		{
			get
			{
				return this.Ref;
			}
			set
			{
				this.Ref = value;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00012430 File Offset: 0x00010630
		internal override void LoadExternals(Hashtable loadedExternals)
		{
			ServiceDescription serviceDescription = null;
			try
			{
				serviceDescription = this.Contract;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				base.ClientProtocol.Errors[this.Url] = ex;
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "LoadExternals", ex);
				}
			}
			if (serviceDescription != null)
			{
				foreach (object obj in this.Contract.Types.Schemas)
				{
					SchemaReference.LoadExternals((XmlSchema)obj, this.Url, base.ClientProtocol, loadedExternals);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object representing the service description.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> object representing the service description.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null. </exception>
		/// <exception cref="T:System.Exception">The <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property of <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> does not contain a discovery document with an URL of <see cref="P:System.Web.Services.Discovery.ContractReference.Url" />. </exception>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00012500 File Offset: 0x00010700
		[XmlIgnore]
		public ServiceDescription Contract
		{
			get
			{
				if (base.ClientProtocol == null)
				{
					throw new InvalidOperationException(Res.GetString("WebMissingClientProtocol"));
				}
				object obj = base.ClientProtocol.Documents[this.Url];
				if (obj == null)
				{
					base.Resolve();
					obj = base.ClientProtocol.Documents[this.Url];
				}
				ServiceDescription serviceDescription = obj as ServiceDescription;
				if (serviceDescription == null)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidDocType", new object[]
					{
						typeof(ServiceDescription).FullName,
						(obj == null) ? string.Empty : obj.GetType().FullName,
						this.Url
					}));
				}
				return serviceDescription;
			}
		}

		/// <summary>Gets the name of the file to use by default when saving the referenced service description.</summary>
		/// <returns>Name of the default file to use when saving the referenced service description to a file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000125B0 File Offset: 0x000107B0
		[XmlIgnore]
		public override string DefaultFilename
		{
			get
			{
				string text = DiscoveryReference.MakeValidFilename(this.Contract.Name);
				if (text == null || text.Length == 0)
				{
					text = DiscoveryReference.FilenameFromUrl(this.Url);
				}
				return Path.ChangeExtension(text, ".wsdl");
			}
		}

		/// <summary>Writes the passed-in service description into the passed-in <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="document">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to write into <paramref name="stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized <see cref="T:System.Web.Services.Description.ServiceDescription" /> is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003FB RID: 1019 RVA: 0x000125F0 File Offset: 0x000107F0
		public override void WriteDocument(object document, Stream stream)
		{
			((ServiceDescription)document).Write(new StreamWriter(stream, new UTF8Encoding(false)));
		}

		/// <summary>Reads the service description from the passed <see cref="T:System.IO.Stream" /> and returns the service description.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> containing the contents of the referenced service description.</returns>
		/// <param name="stream">
		///   <see cref="T:System.IO.Stream" /> containing the service description. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003FC RID: 1020 RVA: 0x00012609 File Offset: 0x00010809
		public override object ReadDocument(Stream stream)
		{
			return ServiceDescription.Read(stream, true);
		}

		/// <summary>Resolves whether the the referenced document is valid.</summary>
		/// <param name="contentType">The MIME content type of <paramref name="stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> containing the referenced document. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null.-or- The service description could not be downloaded and verified successfully. </exception>
		// Token: 0x060003FD RID: 1021 RVA: 0x00012614 File Offset: 0x00010814
		protected internal override void Resolve(string contentType, Stream stream)
		{
			if (ContentType.IsHtml(contentType))
			{
				throw new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[] { contentType }), contentType);
			}
			ServiceDescription serviceDescription = base.ClientProtocol.Documents[this.Url] as ServiceDescription;
			if (serviceDescription == null)
			{
				serviceDescription = ServiceDescription.Read(stream, true);
				serviceDescription.RetrievalUrl = this.Url;
				base.ClientProtocol.Documents[this.Url] = serviceDescription;
			}
			base.ClientProtocol.References[this.Url] = this;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in serviceDescription.Imports)
			{
				Import import = (Import)obj;
				if (import.Location != null)
				{
					arrayList.Add(import.Location);
				}
			}
			foreach (object obj2 in serviceDescription.Types.Schemas)
			{
				foreach (XmlSchemaObject xmlSchemaObject in ((XmlSchema)obj2).Includes)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
					if (xmlSchemaExternal.SchemaLocation != null && xmlSchemaExternal.SchemaLocation.Length > 0)
					{
						arrayList.Add(xmlSchemaExternal.SchemaLocation);
					}
				}
			}
			foreach (object obj3 in arrayList)
			{
				string text = (string)obj3;
				string text2 = DiscoveryReference.UriToString(this.Url, text);
				if (base.ClientProtocol.Documents[text2] == null)
				{
					string text3 = text2;
					try
					{
						stream = base.ClientProtocol.Download(ref text2, ref contentType);
						try
						{
							if (base.ClientProtocol.Documents[text2] == null)
							{
								XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
								xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
								xmlTextReader.XmlResolver = null;
								xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
								if (ServiceDescription.CanRead(xmlTextReader))
								{
									ServiceDescription serviceDescription2 = ServiceDescription.Read(xmlTextReader, true);
									serviceDescription2.RetrievalUrl = text2;
									base.ClientProtocol.Documents[text2] = serviceDescription2;
									ContractReference contractReference = new ContractReference(text2, null);
									contractReference.ClientProtocol = base.ClientProtocol;
									try
									{
										contractReference.Resolve(contentType, stream);
										continue;
									}
									catch (Exception ex)
									{
										if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
										{
											throw;
										}
										contractReference.Url = text3;
										if (Tracing.On)
										{
											Tracing.ExceptionCatch(TraceEventType.Warning, this, "Resolve", ex);
										}
										continue;
									}
								}
								if (xmlTextReader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
								{
									base.ClientProtocol.Documents[text2] = XmlSchema.Read(xmlTextReader, null);
									SchemaReference schemaReference = new SchemaReference(text2);
									schemaReference.ClientProtocol = base.ClientProtocol;
									try
									{
										schemaReference.Resolve(contentType, stream);
									}
									catch (Exception ex2)
									{
										if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
										{
											throw;
										}
										schemaReference.Url = text3;
										if (Tracing.On)
										{
											Tracing.ExceptionCatch(TraceEventType.Warning, this, "Resolve", ex2);
										}
									}
								}
							}
						}
						finally
						{
							stream.Close();
						}
					}
					catch (Exception ex3)
					{
						if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
						{
							throw;
						}
						throw new InvalidDocumentContentsException(Res.GetString("TheWSDLDocumentContainsLinksThatCouldNotBeResolved", new object[] { text2 }), ex3);
					}
				}
			}
		}

		/// <summary>XML namespace for service description references in discovery documents.</summary>
		// Token: 0x04000323 RID: 803
		public const string Namespace = "http://schemas.xmlsoap.org/disco/scl/";

		// Token: 0x04000324 RID: 804
		private string docRef;

		// Token: 0x04000325 RID: 805
		private string reference;
	}
}
