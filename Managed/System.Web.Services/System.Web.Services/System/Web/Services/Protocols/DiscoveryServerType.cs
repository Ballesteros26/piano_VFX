using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000022 RID: 34
	internal class DiscoveryServerType : ServerType
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public List<Action<Uri>> UriFixups { get; private set; }

		// Token: 0x060000CB RID: 203 RVA: 0x00003BD9 File Offset: 0x00001DD9
		private void AddUriFixup(Action<Uri> fixup)
		{
			if (this.UriFixups != null)
			{
				this.UriFixups.Add(fixup);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003BF0 File Offset: 0x00001DF0
		internal DiscoveryServerType(Type type, string uri, bool excludeSchemeHostPortFromCachingKey)
			: base(typeof(DiscoveryServerProtocol))
		{
			if (excludeSchemeHostPortFromCachingKey)
			{
				this.UriFixups = new List<Action<Uri>>();
			}
			uri = new Uri(uri, true).GetLeftPart(UriPartial.Path);
			this.methodInfo = new LogicalMethodInfo(typeof(DiscoveryServerProtocol).GetMethod("Discover", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
			ServiceDescriptionReflector serviceDescriptionReflector = new ServiceDescriptionReflector(this.UriFixups);
			serviceDescriptionReflector.Reflect(type, uri);
			XmlSchemas schemas = serviceDescriptionReflector.Schemas;
			this.description = serviceDescriptionReflector.ServiceDescription;
			XmlSerializer serializer = ServiceDescription.Serializer;
			this.AddSchemaImports(schemas, uri, serviceDescriptionReflector.ServiceDescriptions);
			for (int i = 1; i < serviceDescriptionReflector.ServiceDescriptions.Count; i++)
			{
				ServiceDescription serviceDescription = serviceDescriptionReflector.ServiceDescriptions[i];
				Import import = new Import();
				import.Namespace = serviceDescription.TargetNamespace;
				string text = "wsdl" + i.ToString(CultureInfo.InvariantCulture);
				import.Location = uri + "?wsdl=" + text;
				this.AddUriFixup(delegate(Uri current)
				{
					import.Location = DiscoveryServerType.CombineUris(current, import.Location);
				});
				serviceDescriptionReflector.ServiceDescription.Imports.Add(import);
				this.wsdlTable.Add(text, serviceDescription);
			}
			this.discoDoc = new DiscoveryDocument();
			ContractReference contractReference = new ContractReference(uri + "?wsdl", uri);
			this.AddUriFixup(delegate(Uri current)
			{
				contractReference.Ref = DiscoveryServerType.CombineUris(current, contractReference.Ref);
				contractReference.DocRef = DiscoveryServerType.CombineUris(current, contractReference.DocRef);
			});
			this.discoDoc.References.Add(contractReference);
			foreach (object obj in serviceDescriptionReflector.ServiceDescription.Services)
			{
				foreach (object obj2 in ((Service)obj).Ports)
				{
					Port port = (Port)obj2;
					SoapAddressBinding soapAddressBinding = (SoapAddressBinding)port.Extensions.Find(typeof(SoapAddressBinding));
					if (soapAddressBinding != null)
					{
						global::System.Web.Services.Discovery.SoapBinding binding = new global::System.Web.Services.Discovery.SoapBinding();
						binding.Binding = port.Binding;
						binding.Address = soapAddressBinding.Location;
						this.AddUriFixup(delegate(Uri current)
						{
							binding.Address = DiscoveryServerType.CombineUris(current, binding.Address);
						});
						this.discoDoc.References.Add(binding);
					}
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal void AddExternal(XmlSchema schema, string ns, string location)
		{
			if (schema == null)
			{
				return;
			}
			if (schema.TargetNamespace == ns)
			{
				XmlSchemaInclude include = new XmlSchemaInclude();
				include.SchemaLocation = location;
				this.AddUriFixup(delegate(Uri current)
				{
					include.SchemaLocation = DiscoveryServerType.CombineUris(current, include.SchemaLocation);
				});
				schema.Includes.Add(include);
				return;
			}
			XmlSchemaImport import = new XmlSchemaImport();
			import.SchemaLocation = location;
			this.AddUriFixup(delegate(Uri current)
			{
				import.SchemaLocation = DiscoveryServerType.CombineUris(current, import.SchemaLocation);
			});
			import.Namespace = ns;
			schema.Includes.Add(import);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003F84 File Offset: 0x00002184
		private void AddSchemaImports(XmlSchemas schemas, string uri, ServiceDescriptionCollection descriptions)
		{
			int num = 0;
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema != null)
				{
					if (xmlSchema.Id == null || xmlSchema.Id.Length == 0)
					{
						XmlSchema xmlSchema2 = xmlSchema;
						string text = "schema";
						int num2 = num + 1;
						num = num2;
						xmlSchema2.Id = text + num2.ToString(CultureInfo.InvariantCulture);
					}
					string text2 = uri + "?schema=" + xmlSchema.Id;
					foreach (object obj2 in descriptions)
					{
						ServiceDescription serviceDescription = (ServiceDescription)obj2;
						if (serviceDescription.Types.Schemas.Count == 0)
						{
							XmlSchema xmlSchema3 = new XmlSchema();
							xmlSchema3.TargetNamespace = serviceDescription.TargetNamespace;
							xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
							this.AddExternal(xmlSchema3, xmlSchema.TargetNamespace, text2);
							serviceDescription.Types.Schemas.Add(xmlSchema3);
						}
						else
						{
							this.AddExternal(serviceDescription.Types.Schemas[0], xmlSchema.TargetNamespace, text2);
						}
					}
					this.schemaTable.Add(xmlSchema.Id, xmlSchema);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004114 File Offset: 0x00002314
		internal XmlSchema GetSchema(string id)
		{
			return (XmlSchema)this.schemaTable[id];
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004127 File Offset: 0x00002327
		internal ServiceDescription GetServiceDescription(string id)
		{
			return (ServiceDescription)this.wsdlTable[id];
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000413A File Offset: 0x0000233A
		internal ServiceDescription Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004142 File Offset: 0x00002342
		internal LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.methodInfo;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000414A File Offset: 0x0000234A
		internal DiscoveryDocument Disco
		{
			get
			{
				return this.discoDoc;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004152 File Offset: 0x00002352
		internal static string CombineUris(Uri schemeHostPort, string absolutePathAndQuery)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", schemeHostPort.Scheme, schemeHostPort.Authority, new Uri(absolutePathAndQuery).PathAndQuery);
		}

		// Token: 0x040001CF RID: 463
		private ServiceDescription description;

		// Token: 0x040001D0 RID: 464
		private LogicalMethodInfo methodInfo;

		// Token: 0x040001D1 RID: 465
		private Hashtable schemaTable = new Hashtable();

		// Token: 0x040001D2 RID: 466
		private Hashtable wsdlTable = new Hashtable();

		// Token: 0x040001D3 RID: 467
		private DiscoveryDocument discoDoc;
	}
}
