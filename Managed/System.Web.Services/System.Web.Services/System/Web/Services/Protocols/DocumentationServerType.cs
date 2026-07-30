using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200002A RID: 42
	internal class DocumentationServerType : ServerType
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000047A3 File Offset: 0x000029A3
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000047AB File Offset: 0x000029AB
		public List<Action<Uri>> UriFixups { get; private set; }

		// Token: 0x060000ED RID: 237 RVA: 0x000047B4 File Offset: 0x000029B4
		private void AddUriFixup(Action<Uri> fixup)
		{
			if (this.UriFixups != null)
			{
				this.UriFixups.Add(fixup);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000047CC File Offset: 0x000029CC
		internal DocumentationServerType(Type type, string uri, bool excludeSchemeHostPortFromCachingKey)
			: base(typeof(DocumentationServerProtocol))
		{
			if (excludeSchemeHostPortFromCachingKey)
			{
				this.UriFixups = new List<Action<Uri>>();
			}
			uri = new Uri(uri, true).GetLeftPart(UriPartial.Path);
			this.methodInfo = new LogicalMethodInfo(typeof(DocumentationServerProtocol).GetMethod("Documentation", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
			ServiceDescriptionReflector serviceDescriptionReflector = new ServiceDescriptionReflector(this.UriFixups);
			serviceDescriptionReflector.Reflect(type, uri);
			this.schemas = serviceDescriptionReflector.Schemas;
			this.serviceDescriptions = serviceDescriptionReflector.ServiceDescriptions;
			this.schemasWithPost = serviceDescriptionReflector.SchemasWithPost;
			this.serviceDescriptionsWithPost = serviceDescriptionReflector.ServiceDescriptionsWithPost;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000486B File Offset: 0x00002A6B
		internal LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.methodInfo;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004873 File Offset: 0x00002A73
		internal XmlSchemas Schemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000487B File Offset: 0x00002A7B
		internal ServiceDescriptionCollection ServiceDescriptions
		{
			get
			{
				return this.serviceDescriptions;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004883 File Offset: 0x00002A83
		internal ServiceDescriptionCollection ServiceDescriptionsWithPost
		{
			get
			{
				return this.serviceDescriptionsWithPost;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000488B File Offset: 0x00002A8B
		internal XmlSchemas SchemasWithPost
		{
			get
			{
				return this.schemasWithPost;
			}
		}

		// Token: 0x040001DC RID: 476
		private ServiceDescriptionCollection serviceDescriptions;

		// Token: 0x040001DD RID: 477
		private ServiceDescriptionCollection serviceDescriptionsWithPost;

		// Token: 0x040001DE RID: 478
		private XmlSchemas schemas;

		// Token: 0x040001DF RID: 479
		private XmlSchemas schemasWithPost;

		// Token: 0x040001E0 RID: 480
		private LogicalMethodInfo methodInfo;
	}
}
