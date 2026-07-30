using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides a managed way of dynamically viewing, creating or invoking types supported by an XML Web service.</summary>
	// Token: 0x02000110 RID: 272
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ServiceDescriptionReflector
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001E998 File Offset: 0x0001CB98
		internal List<Action<Uri>> UriFixups
		{
			get
			{
				return this.uriFixups;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> associated with the XML Web service.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> associated with the XML Web service.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		public ServiceDescriptionCollection ServiceDescriptions
		{
			get
			{
				return this.descriptions;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Xml.Serialization.XmlSchemas" /> associated with the XML Web service.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> collection.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		public XmlSchemas Schemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		internal ServiceDescriptionCollection ServiceDescriptionsWithPost
		{
			get
			{
				return this.descriptionsWithPost;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		internal XmlSchemas SchemasWithPost
		{
			get
			{
				return this.schemasWithPost;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		internal ServiceDescription ServiceDescription
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		internal Service Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		internal Type ServiceType
		{
			get
			{
				return this.serviceType;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		internal LogicalMethodInfo[] Methods
		{
			get
			{
				return this.methods;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		internal string ServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		internal XmlSchemaExporter SchemaExporter
		{
			get
			{
				return this.exporter;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		internal XmlReflectionImporter ReflectionImporter
		{
			get
			{
				return this.importer;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		internal WebServiceAttribute ServiceAttribute
		{
			get
			{
				return this.serviceAttr;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001EA00 File Offset: 0x0001CC00
		internal Hashtable ReflectionContext
		{
			get
			{
				if (this.reflectionContext == null)
				{
					this.reflectionContext = new Hashtable();
				}
				return this.reflectionContext;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionReflector" /> class.</summary>
		// Token: 0x06000798 RID: 1944 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		public ServiceDescriptionReflector()
		{
			Type[] protocolReflectorTypes = WebServicesSection.Current.ProtocolReflectorTypes;
			this.reflectors = new ProtocolReflector[protocolReflectorTypes.Length];
			for (int i = 0; i < this.reflectors.Length; i++)
			{
				ProtocolReflector protocolReflector = (ProtocolReflector)Activator.CreateInstance(protocolReflectorTypes[i]);
				protocolReflector.Initialize(this);
				this.reflectors[i] = protocolReflector;
			}
			WebServiceProtocols enabledProtocols = WebServicesSection.Current.EnabledProtocols;
			if ((enabledProtocols & WebServiceProtocols.HttpPost) == WebServiceProtocols.Unknown && (enabledProtocols & WebServiceProtocols.HttpPostLocalhost) != WebServiceProtocols.Unknown)
			{
				this.reflectorsWithPost = new ProtocolReflector[this.reflectors.Length + 1];
				for (int j = 0; j < this.reflectorsWithPost.Length - 1; j++)
				{
					ProtocolReflector protocolReflector2 = (ProtocolReflector)Activator.CreateInstance(protocolReflectorTypes[j]);
					protocolReflector2.Initialize(this);
					this.reflectorsWithPost[j] = protocolReflector2;
				}
				ProtocolReflector protocolReflector3 = new HttpPostProtocolReflector();
				protocolReflector3.Initialize(this);
				this.reflectorsWithPost[this.reflectorsWithPost.Length - 1] = protocolReflector3;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001EB18 File Offset: 0x0001CD18
		internal ServiceDescriptionReflector(List<Action<Uri>> uriFixups)
			: this()
		{
			this.uriFixups = uriFixups;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001EB28 File Offset: 0x0001CD28
		private void ReflectInternal(ProtocolReflector[] reflectors)
		{
			this.description = new ServiceDescription();
			this.description.TargetNamespace = this.serviceAttr.Namespace;
			this.ServiceDescriptions.Add(this.description);
			this.service = new Service();
			string text = this.serviceAttr.Name;
			if (text == null || text.Length == 0)
			{
				text = this.serviceType.Name;
			}
			this.service.Name = XmlConvert.EncodeLocalName(text);
			if (this.serviceAttr.Description != null && this.serviceAttr.Description.Length > 0)
			{
				this.service.Documentation = this.serviceAttr.Description;
			}
			this.description.Services.Add(this.service);
			this.reflectionContext = new Hashtable();
			this.exporter = new XmlSchemaExporter(this.description.Types.Schemas);
			this.importer = SoapReflector.CreateXmlImporter(this.serviceAttr.Namespace, SoapReflector.ServiceDefaultIsEncoded(this.serviceType));
			WebMethodReflector.IncludeTypes(this.methods, this.importer);
			for (int i = 0; i < reflectors.Length; i++)
			{
				reflectors[i].Reflect();
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.Services.Description.ServiceDescription" /> including the specified <see cref="T:System.Type" /> for the XML Web service at the specified URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the class or interface to reflect. </param>
		/// <param name="url">The address (URL) of the XML Web service. </param>
		// Token: 0x0600079B RID: 1947 RVA: 0x0001EC64 File Offset: 0x0001CE64
		public void Reflect(Type type, string url)
		{
			this.serviceType = type;
			this.serviceUrl = url;
			this.serviceAttr = WebServiceReflector.GetAttribute(type);
			this.methods = WebMethodReflector.GetMethods(type);
			this.CheckForDuplicateMethods(this.methods);
			this.descriptionsWithPost = this.descriptions;
			this.schemasWithPost = this.schemas;
			if (this.reflectorsWithPost != null)
			{
				this.ReflectInternal(this.reflectorsWithPost);
				this.descriptions = new ServiceDescriptionCollection();
				this.schemas = new XmlSchemas();
			}
			this.ReflectInternal(this.reflectors);
			if (this.serviceAttr.Description != null && this.serviceAttr.Description.Length > 0)
			{
				this.ServiceDescription.Documentation = this.serviceAttr.Description;
			}
			this.ServiceDescription.Types.Schemas.Compile(null, false);
			if (this.ServiceDescriptions.Count > 1)
			{
				this.Schemas.Add(this.ServiceDescription.Types.Schemas);
				this.ServiceDescription.Types.Schemas.Clear();
				return;
			}
			if (this.ServiceDescription.Types.Schemas.Count > 0)
			{
				XmlSchema[] array = new XmlSchema[this.ServiceDescription.Types.Schemas.Count];
				this.ServiceDescription.Types.Schemas.CopyTo(array, 0);
				foreach (XmlSchema xmlSchema in array)
				{
					if (XmlSchemas.IsDataSet(xmlSchema))
					{
						this.ServiceDescription.Types.Schemas.Remove(xmlSchema);
						this.Schemas.Add(xmlSchema);
					}
				}
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001EE08 File Offset: 0x0001D008
		private void CheckForDuplicateMethods(LogicalMethodInfo[] methods)
		{
			Hashtable hashtable = new Hashtable();
			foreach (LogicalMethodInfo logicalMethodInfo in methods)
			{
				string text = logicalMethodInfo.MethodAttribute.MessageName;
				if (text.Length == 0)
				{
					text = logicalMethodInfo.Name;
				}
				string text2 = ((logicalMethodInfo.Binding == null) ? text : (logicalMethodInfo.Binding.Name + "." + text));
				LogicalMethodInfo logicalMethodInfo2 = (LogicalMethodInfo)hashtable[text2];
				if (logicalMethodInfo2 != null)
				{
					throw new InvalidOperationException(Res.GetString("BothAndUseTheMessageNameUseTheMessageName3", new object[]
					{
						logicalMethodInfo,
						logicalMethodInfo2,
						XmlConvert.EncodeLocalName(text)
					}));
				}
				hashtable.Add(text2, logicalMethodInfo);
			}
		}

		// Token: 0x0400043B RID: 1083
		private ProtocolReflector[] reflectors;

		// Token: 0x0400043C RID: 1084
		private ProtocolReflector[] reflectorsWithPost;

		// Token: 0x0400043D RID: 1085
		private ServiceDescriptionCollection descriptions = new ServiceDescriptionCollection();

		// Token: 0x0400043E RID: 1086
		private XmlSchemas schemas = new XmlSchemas();

		// Token: 0x0400043F RID: 1087
		private ServiceDescriptionCollection descriptionsWithPost;

		// Token: 0x04000440 RID: 1088
		private XmlSchemas schemasWithPost;

		// Token: 0x04000441 RID: 1089
		private WebServiceAttribute serviceAttr;

		// Token: 0x04000442 RID: 1090
		private ServiceDescription description;

		// Token: 0x04000443 RID: 1091
		private Service service;

		// Token: 0x04000444 RID: 1092
		private LogicalMethodInfo[] methods;

		// Token: 0x04000445 RID: 1093
		private XmlSchemaExporter exporter;

		// Token: 0x04000446 RID: 1094
		private XmlReflectionImporter importer;

		// Token: 0x04000447 RID: 1095
		private Type serviceType;

		// Token: 0x04000448 RID: 1096
		private string serviceUrl;

		// Token: 0x04000449 RID: 1097
		private Hashtable reflectionContext;

		// Token: 0x0400044A RID: 1098
		private List<Action<Uri>> uriFixups;
	}
}
