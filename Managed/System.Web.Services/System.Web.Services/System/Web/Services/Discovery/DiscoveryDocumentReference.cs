using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a reference to a discovery document. This class cannot be inherited.</summary>
	// Token: 0x020000A6 RID: 166
	[XmlRoot("discoveryRef", Namespace = "http://schemas.xmlsoap.org/disco/")]
	public sealed class DiscoveryDocumentReference : DiscoveryReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> class.</summary>
		// Token: 0x06000449 RID: 1097 RVA: 0x000123CF File Offset: 0x000105CF
		public DiscoveryDocumentReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> class, setting the <see cref="P:System.Web.Services.Discovery.DiscoveryDocumentReference.Ref" /> property to the value of <paramref name="href" />.</summary>
		/// <param name="href">Reference to a discovery document. The <see cref="P:System.Web.Services.Discovery.DiscoveryDocumentReference.Ref" /> property is set to the value of <paramref name="href" />. </param>
		// Token: 0x0600044A RID: 1098 RVA: 0x00013BE3 File Offset: 0x00011DE3
		public DiscoveryDocumentReference(string href)
		{
			this.Ref = href;
		}

		/// <summary>Gets or sets the reference to a discovery document.</summary>
		/// <returns>Reference to a discovery document.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00013BF2 File Offset: 0x00011DF2
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00013C08 File Offset: 0x00011E08
		[XmlAttribute("ref")]
		public string Ref
		{
			get
			{
				if (this.reference != null)
				{
					return this.reference;
				}
				return "";
			}
			set
			{
				this.reference = value;
			}
		}

		/// <summary>Gets the name of the default file to use when saving the referenced discovery document.</summary>
		/// <returns>Name of the default file to use when saving the referenced document to a file.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00013C11 File Offset: 0x00011E11
		[XmlIgnore]
		public override string DefaultFilename
		{
			get
			{
				return Path.ChangeExtension(DiscoveryReference.FilenameFromUrl(this.Url), ".disco");
			}
		}

		/// <summary>Gets the contents of the referenced discovery document as a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> representing the contents of the referenced discovery document.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null.-or- An error occurred during the download or resolution of the XSD schema using <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" />. </exception>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00013C28 File Offset: 0x00011E28
		[XmlIgnore]
		public DiscoveryDocument Document
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
				DiscoveryDocument discoveryDocument = obj as DiscoveryDocument;
				if (discoveryDocument == null)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidDocType", new object[]
					{
						typeof(DiscoveryDocument).FullName,
						(obj == null) ? string.Empty : obj.GetType().FullName,
						this.Url
					}));
				}
				return discoveryDocument;
			}
		}

		/// <summary>Writes the passed <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="document">The <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> to write into <paramref name="stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized discovery document is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600044F RID: 1103 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public override void WriteDocument(object document, Stream stream)
		{
			WebServicesSection.Current.DiscoveryDocumentSerializer.Serialize(new StreamWriter(stream, new UTF8Encoding(false)), document);
		}

		/// <summary>Reads and returns the discovery document from the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of the referenced discovery document.</returns>
		/// <param name="stream">
		///   <see cref="T:System.IO.Stream" /> containing the discovery document. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPrincipal" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000450 RID: 1104 RVA: 0x00013CF6 File Offset: 0x00011EF6
		public override object ReadDocument(Stream stream)
		{
			return WebServicesSection.Current.DiscoveryDocumentSerializer.Deserialize(stream);
		}

		/// <summary>Gets or sets the URL of the referenced discovery document.</summary>
		/// <returns>The URL of the referenced discovery document.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00013D08 File Offset: 0x00011F08
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00013D10 File Offset: 0x00011F10
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

		// Token: 0x06000453 RID: 1107 RVA: 0x00013D1C File Offset: 0x00011F1C
		private static DiscoveryDocument GetDocumentNoParse(ref string url, DiscoveryClientProtocol client)
		{
			DiscoveryDocument discoveryDocument = (DiscoveryDocument)client.Documents[url];
			if (discoveryDocument != null)
			{
				return discoveryDocument;
			}
			string text = null;
			Stream stream = client.Download(ref url, ref text);
			DiscoveryDocument discoveryDocument2;
			try
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(text)));
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
				xmlTextReader.XmlResolver = null;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				if (!DiscoveryDocument.CanRead(xmlTextReader))
				{
					ArgumentException ex = new ArgumentException(Res.GetString("WebInvalidFormat"));
					throw new InvalidOperationException(Res.GetString("WebMissingDocument", new object[] { url }), ex);
				}
				discoveryDocument2 = DiscoveryDocument.Read(xmlTextReader);
			}
			finally
			{
				stream.Close();
			}
			return discoveryDocument2;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013DD0 File Offset: 0x00011FD0
		protected internal override void Resolve(string contentType, Stream stream)
		{
			DiscoveryDocument discoveryDocument = null;
			if (ContentType.IsHtml(contentType))
			{
				string text = LinkGrep.SearchForLink(stream);
				if (text == null)
				{
					throw new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[] { contentType }), contentType);
				}
				string text2 = DiscoveryReference.UriToString(this.Url, text);
				discoveryDocument = DiscoveryDocumentReference.GetDocumentNoParse(ref text2, base.ClientProtocol);
				this.Url = text2;
			}
			if (discoveryDocument == null)
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
				xmlTextReader.XmlResolver = null;
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
				xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
				if (DiscoveryDocument.CanRead(xmlTextReader))
				{
					discoveryDocument = DiscoveryDocument.Read(xmlTextReader);
				}
				else
				{
					stream.Position = 0L;
					XmlTextReader xmlTextReader2 = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
					xmlTextReader2.XmlResolver = null;
					xmlTextReader2.DtdProcessing = DtdProcessing.Prohibit;
					while (xmlTextReader2.NodeType != XmlNodeType.Element)
					{
						if (xmlTextReader2.NodeType == XmlNodeType.ProcessingInstruction)
						{
							StringBuilder stringBuilder = new StringBuilder("<pi ");
							stringBuilder.Append(xmlTextReader2.Value);
							stringBuilder.Append("/>");
							XmlTextReader xmlTextReader3 = new XmlTextReader(new StringReader(stringBuilder.ToString()));
							xmlTextReader3.XmlResolver = null;
							xmlTextReader3.DtdProcessing = DtdProcessing.Prohibit;
							xmlTextReader3.Read();
							string text3 = xmlTextReader3["type"];
							string text4 = xmlTextReader3["alternate"];
							string text5 = xmlTextReader3["href"];
							if (text3 != null && ContentType.MatchesBase(text3, "text/xml") && text4 != null && string.Compare(text4, "yes", StringComparison.OrdinalIgnoreCase) == 0 && text5 != null)
							{
								string text6 = DiscoveryReference.UriToString(this.Url, text5);
								discoveryDocument = DiscoveryDocumentReference.GetDocumentNoParse(ref text6, base.ClientProtocol);
								this.Url = text6;
								break;
							}
						}
						xmlTextReader2.Read();
					}
				}
			}
			if (discoveryDocument == null)
			{
				Exception ex;
				if (ContentType.IsXml(contentType))
				{
					ex = new ArgumentException(Res.GetString("WebInvalidFormat"));
				}
				else
				{
					ex = new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[] { contentType }), contentType);
				}
				throw new InvalidOperationException(Res.GetString("WebMissingDocument", new object[] { this.Url }), ex);
			}
			base.ClientProtocol.References[this.Url] = this;
			base.ClientProtocol.Documents[this.Url] = discoveryDocument;
			foreach (object obj in discoveryDocument.References)
			{
				if (obj is DiscoveryReference)
				{
					DiscoveryReference discoveryReference = (DiscoveryReference)obj;
					if (discoveryReference.Url.Length == 0)
					{
						throw new InvalidOperationException(Res.GetString("WebEmptyRef", new object[]
						{
							discoveryReference.GetType().FullName,
							this.Url
						}));
					}
					discoveryReference.Url = DiscoveryReference.UriToString(this.Url, discoveryReference.Url);
					ContractReference contractReference = discoveryReference as ContractReference;
					if (contractReference != null && contractReference.DocRef != null)
					{
						contractReference.DocRef = DiscoveryReference.UriToString(this.Url, contractReference.DocRef);
					}
					discoveryReference.ClientProtocol = base.ClientProtocol;
					base.ClientProtocol.References[discoveryReference.Url] = discoveryReference;
				}
				else
				{
					base.ClientProtocol.AdditionalInformation.Add(obj);
				}
			}
		}

		/// <summary>Verifies that all referenced documents within the discovery document are valid.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null.-or- The discovery document could not be downloaded and verified successfully. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000455 RID: 1109 RVA: 0x0001412C File Offset: 0x0001232C
		public void ResolveAll()
		{
			this.ResolveAll(true);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014138 File Offset: 0x00012338
		internal void ResolveAll(bool throwOnError)
		{
			try
			{
				base.Resolve();
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (throwOnError)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex);
				}
				return;
			}
			foreach (object obj in this.Document.References)
			{
				DiscoveryDocumentReference discoveryDocumentReference = obj as DiscoveryDocumentReference;
				if (discoveryDocumentReference != null && base.ClientProtocol.Documents[discoveryDocumentReference.Url] == null)
				{
					discoveryDocumentReference.ClientProtocol = base.ClientProtocol;
					discoveryDocumentReference.ResolveAll(throwOnError);
				}
			}
		}

		// Token: 0x04000331 RID: 817
		private string reference;
	}
}
