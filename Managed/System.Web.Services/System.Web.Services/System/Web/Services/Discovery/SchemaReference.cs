using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery
{
	/// <summary>Represents a reference in a discovery document to an XML Schema Definition (XSD) language schema. This class cannot be inherited.</summary>
	// Token: 0x020000B8 RID: 184
	[XmlRoot("schemaRef", Namespace = "http://schemas.xmlsoap.org/disco/schema/")]
	public sealed class SchemaReference : DiscoveryReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> class using default values.</summary>
		// Token: 0x060004C1 RID: 1217 RVA: 0x000123CF File Offset: 0x000105CF
		public SchemaReference()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> class using the supplied URL as the XSD schema reference.</summary>
		/// <param name="url">The URL for the XSD schema. Initializes the <see cref="P:System.Web.Services.Discovery.SchemaReference.Ref" /> property. </param>
		// Token: 0x060004C2 RID: 1218 RVA: 0x000164F1 File Offset: 0x000146F1
		public SchemaReference(string url)
		{
			this.Ref = url;
		}

		/// <summary>Gets or sets the URL to the referenced XSD schema.</summary>
		/// <returns>The URL for the referenced XSD schema. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016500 File Offset: 0x00014700
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00016516 File Offset: 0x00014716
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

		/// <summary>Gets or sets the targetNamespace XML attribute of the XSD schema.</summary>
		/// <returns>The targetNamespace XML attribute of the XSD schema. The default value is null.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001651F File Offset: 0x0001471F
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00016527 File Offset: 0x00014727
		[XmlAttribute("targetNamespace")]
		[DefaultValue(null)]
		public string TargetNamespace
		{
			get
			{
				return this.targetNamespace;
			}
			set
			{
				this.targetNamespace = value;
			}
		}

		/// <summary>Gets or sets the URL for the schema reference.</summary>
		/// <returns>The URL for the referenced XSD schema. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00016530 File Offset: 0x00014730
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00016538 File Offset: 0x00014738
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

		// Token: 0x060004C9 RID: 1225 RVA: 0x00016544 File Offset: 0x00014744
		internal XmlSchema GetSchema()
		{
			try
			{
				return this.Schema;
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
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "GetSchema", ex);
				}
			}
			return null;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000165B8 File Offset: 0x000147B8
		internal override void LoadExternals(Hashtable loadedExternals)
		{
			SchemaReference.LoadExternals(this.GetSchema(), this.Url, base.ClientProtocol, loadedExternals);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000165D4 File Offset: 0x000147D4
		internal static void LoadExternals(XmlSchema schema, string url, DiscoveryClientProtocol client, Hashtable loadedExternals)
		{
			if (schema == null)
			{
				return;
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal.SchemaLocation != null && xmlSchemaExternal.SchemaLocation.Length != 0 && xmlSchemaExternal.Schema == null && (xmlSchemaExternal is XmlSchemaInclude || xmlSchemaExternal is XmlSchemaRedefine))
				{
					string text = DiscoveryReference.UriToString(url, xmlSchemaExternal.SchemaLocation);
					if (client.References[text] is SchemaReference)
					{
						SchemaReference schemaReference = (SchemaReference)client.References[text];
						xmlSchemaExternal.Schema = schemaReference.GetSchema();
						if (xmlSchemaExternal.Schema != null)
						{
							loadedExternals[text] = xmlSchemaExternal.Schema;
						}
						schemaReference.LoadExternals(loadedExternals);
					}
				}
			}
		}

		/// <summary>Writes the passed XSD schema into the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="document">The <see cref="T:System.Xml.Schema.XmlSchema" /> to write into <paramref name="stream" />. </param>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized XSD schema is written. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004CC RID: 1228 RVA: 0x000166BC File Offset: 0x000148BC
		public override void WriteDocument(object document, Stream stream)
		{
			((XmlSchema)document).Write(new StreamWriter(stream, new UTF8Encoding(false)));
		}

		/// <summary>Reads and returns the XSD schema from the passed <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> containing the contents of the referenced XSD schema.</returns>
		/// <param name="stream">
		///   <see cref="T:System.IO.Stream" /> containing the XSD schema. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004CD RID: 1229 RVA: 0x000166D5 File Offset: 0x000148D5
		public override object ReadDocument(Stream stream)
		{
			return XmlSchema.Read(new XmlTextReader(this.Url, stream)
			{
				XmlResolver = null
			}, null);
		}

		/// <summary>Gets the name of the default file to use when saving the referenced XSD schema.</summary>
		/// <returns>Default name to use when saving the referenced XSD schema to a file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000166F0 File Offset: 0x000148F0
		[XmlIgnore]
		public override string DefaultFilename
		{
			get
			{
				string text = DiscoveryReference.MakeValidFilename(this.Schema.Id);
				if (text == null || text.Length == 0)
				{
					text = DiscoveryReference.FilenameFromUrl(this.Url);
				}
				return Path.ChangeExtension(text, ".xsd");
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XSD schema.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XSD schema.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is null.-or- An error occurred during the download or resolution of the XSD schema using <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" />. </exception>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016730 File Offset: 0x00014930
		[XmlIgnore]
		public XmlSchema Schema
		{
			get
			{
				if (base.ClientProtocol == null)
				{
					throw new InvalidOperationException(Res.GetString("WebMissingClientProtocol"));
				}
				object obj = base.ClientProtocol.InlinedSchemas[this.Url];
				if (obj == null)
				{
					obj = base.ClientProtocol.Documents[this.Url];
				}
				if (obj == null)
				{
					base.Resolve();
					obj = base.ClientProtocol.Documents[this.Url];
				}
				XmlSchema xmlSchema = obj as XmlSchema;
				if (xmlSchema == null)
				{
					throw new InvalidOperationException(Res.GetString("WebInvalidDocType", new object[]
					{
						typeof(XmlSchema).FullName,
						(obj == null) ? string.Empty : obj.GetType().FullName,
						this.Url
					}));
				}
				return xmlSchema;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000167FC File Offset: 0x000149FC
		protected internal override void Resolve(string contentType, Stream stream)
		{
			if (ContentType.IsHtml(contentType))
			{
				base.ClientProtocol.Errors[this.Url] = new InvalidContentTypeException(Res.GetString("WebInvalidContentType", new object[] { contentType }), contentType);
			}
			XmlSchema xmlSchema = base.ClientProtocol.Documents[this.Url] as XmlSchema;
			if (xmlSchema == null)
			{
				if (base.ClientProtocol.Errors[this.Url] != null)
				{
					throw base.ClientProtocol.Errors[this.Url];
				}
				xmlSchema = (XmlSchema)this.ReadDocument(stream);
				base.ClientProtocol.Documents[this.Url] = xmlSchema;
			}
			if (base.ClientProtocol.References[this.Url] != this)
			{
				base.ClientProtocol.References[this.Url] = this;
			}
			foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				string text = null;
				try
				{
					if (xmlSchemaExternal.SchemaLocation != null && xmlSchemaExternal.SchemaLocation.Length > 0)
					{
						text = DiscoveryReference.UriToString(this.Url, xmlSchemaExternal.SchemaLocation);
						SchemaReference schemaReference = new SchemaReference(text);
						schemaReference.ClientProtocol = base.ClientProtocol;
						base.ClientProtocol.References[text] = schemaReference;
						schemaReference.Resolve();
					}
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new InvalidDocumentContentsException(Res.GetString("TheSchemaDocumentContainsLinksThatCouldNotBeResolved", new object[] { text }), ex);
				}
			}
		}

		/// <summary>XML namespace for XSD schema references in discovery documents.</summary>
		// Token: 0x04000363 RID: 867
		public const string Namespace = "http://schemas.xmlsoap.org/disco/schema/";

		// Token: 0x04000364 RID: 868
		private string reference;

		// Token: 0x04000365 RID: 869
		private string targetNamespace;
	}
}
