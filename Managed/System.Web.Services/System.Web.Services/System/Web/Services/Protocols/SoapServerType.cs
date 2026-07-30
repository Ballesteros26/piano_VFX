using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapServerType" /> class represents the type on which the XML Web service is based.</summary>
	// Token: 0x0200007D RID: 125
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SoapServerType : ServerType
	{
		/// <summary>Gets a <see cref="T:System.String" /> that contains the namespace to which this XML Web service belongs.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the namespace to which this XML Web service belongs.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000E92E File Offset: 0x0000CB2E
		public string ServiceNamespace
		{
			get
			{
				return this.serviceNamespace;
			}
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether SOAP data transmissions sent to and from this XML Web service are encoded by default.</summary>
		/// <returns>true if SOAP data transmissions sent to and from this XML Web service are encoded by default; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E936 File Offset: 0x0000CB36
		public bool ServiceDefaultIsEncoded
		{
			get
			{
				return this.serviceDefaultIsEncoded;
			}
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether SOAP messages that are routed to this XML Web service are routed based on the SOAPAction HTTP header.</summary>
		/// <returns>true if SOAP messages that are routed to this XML Web service are routed based on the SOAPAction HTTP header; otherwise, false.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000E93E File Offset: 0x0000CB3E
		public bool ServiceRoutingOnSoapAction
		{
			get
			{
				return this.routingOnSoapAction;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapServerType" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> on which this XML Web service is based.</param>
		/// <param name="protocolsSupported">A <see cref="T:System.Web.Services.Configuration.WebServiceProtocols" /> value that specifies the transmission protocols that are used to decrypt data sent in the XML Web service request.</param>
		// Token: 0x0600034D RID: 845 RVA: 0x0000E948 File Offset: 0x0000CB48
		public SoapServerType(Type type, WebServiceProtocols protocolsSupported)
			: base(type)
		{
			this.protocolsSupported = protocolsSupported;
			bool flag = (protocolsSupported & WebServiceProtocols.HttpSoap) > WebServiceProtocols.Unknown;
			LogicalMethodInfo[] array = WebMethodReflector.GetMethods(type);
			ArrayList arrayList = new ArrayList();
			WebServiceAttribute attribute = WebServiceReflector.GetAttribute(type);
			object soapServiceAttribute = SoapReflector.GetSoapServiceAttribute(type);
			this.routingOnSoapAction = SoapReflector.GetSoapServiceRoutingStyle(soapServiceAttribute) == SoapServiceRoutingStyle.SoapAction;
			this.serviceNamespace = attribute.Namespace;
			this.serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(type);
			SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(this.serviceNamespace, this.serviceDefaultIsEncoded);
			XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(this.serviceNamespace, this.serviceDefaultIsEncoded);
			SoapReflector.IncludeTypes(array, soapReflectionImporter);
			WebMethodReflector.IncludeTypes(array, xmlReflectionImporter);
			SoapReflectedMethod[] array2 = new SoapReflectedMethod[array.Length];
			SoapExtensionTypeElementCollection soapExtensionTypes = WebServicesSection.Current.SoapExtensionTypes;
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			for (int i = 0; i < soapExtensionTypes.Count; i++)
			{
				SoapExtensionTypeElement soapExtensionTypeElement = soapExtensionTypes[i];
				if (soapExtensionTypeElement != null)
				{
					SoapReflectedExtension soapReflectedExtension = new SoapReflectedExtension(soapExtensionTypeElement.Type, null, soapExtensionTypeElement.Priority);
					if (soapExtensionTypeElement.Group == PriorityGroup.High)
					{
						arrayList2.Add(soapReflectedExtension);
					}
					else
					{
						arrayList3.Add(soapReflectedExtension);
					}
				}
			}
			this.HighPriExtensions = (SoapReflectedExtension[])arrayList2.ToArray(typeof(SoapReflectedExtension));
			this.LowPriExtensions = (SoapReflectedExtension[])arrayList3.ToArray(typeof(SoapReflectedExtension));
			Array.Sort<SoapReflectedExtension>(this.HighPriExtensions);
			Array.Sort<SoapReflectedExtension>(this.LowPriExtensions);
			this.HighPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, this.HighPriExtensions);
			this.LowPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, this.LowPriExtensions);
			for (int j = 0; j < array.Length; j++)
			{
				SoapReflectedMethod soapReflectedMethod = SoapReflector.ReflectMethod(array[j], false, xmlReflectionImporter, soapReflectionImporter, attribute.Namespace);
				arrayList.Add(soapReflectedMethod.requestMappings);
				if (soapReflectedMethod.responseMappings != null)
				{
					arrayList.Add(soapReflectedMethod.responseMappings);
				}
				arrayList.Add(soapReflectedMethod.inHeaderMappings);
				if (soapReflectedMethod.outHeaderMappings != null)
				{
					arrayList.Add(soapReflectedMethod.outHeaderMappings);
				}
				array2[j] = soapReflectedMethod;
			}
			XmlMapping[] array3 = (XmlMapping[])arrayList.ToArray(typeof(XmlMapping));
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, ".ctor", new object[] { type, protocolsSupported }) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), traceMethod, new TraceMethod(typeof(XmlSerializer), "FromMappings", new object[] { array3, base.Evidence }));
			}
			XmlSerializer[] array4;
			if (AppDomain.CurrentDomain.IsHomogenous)
			{
				array4 = XmlSerializer.FromMappings(array3);
			}
			else
			{
				array4 = XmlSerializer.FromMappings(array3, base.Evidence);
			}
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), traceMethod);
			}
			int num = 0;
			for (int k = 0; k < array2.Length; k++)
			{
				SoapServerMethod soapServerMethod = new SoapServerMethod();
				SoapReflectedMethod soapReflectedMethod2 = array2[k];
				soapServerMethod.parameterSerializer = array4[num++];
				if (soapReflectedMethod2.responseMappings != null)
				{
					soapServerMethod.returnSerializer = array4[num++];
				}
				soapServerMethod.inHeaderSerializer = array4[num++];
				if (soapReflectedMethod2.outHeaderMappings != null)
				{
					soapServerMethod.outHeaderSerializer = array4[num++];
				}
				soapServerMethod.methodInfo = soapReflectedMethod2.methodInfo;
				soapServerMethod.action = soapReflectedMethod2.action;
				soapServerMethod.extensions = soapReflectedMethod2.extensions;
				soapServerMethod.extensionInitializers = SoapReflectedExtension.GetInitializers(soapServerMethod.methodInfo, soapReflectedMethod2.extensions);
				soapServerMethod.oneWay = soapReflectedMethod2.oneWay;
				soapServerMethod.rpc = soapReflectedMethod2.rpc;
				soapServerMethod.use = soapReflectedMethod2.use;
				soapServerMethod.paramStyle = soapReflectedMethod2.paramStyle;
				soapServerMethod.wsiClaims = ((soapReflectedMethod2.binding == null) ? WsiProfiles.None : soapReflectedMethod2.binding.ConformsTo);
				ArrayList arrayList4 = new ArrayList();
				ArrayList arrayList5 = new ArrayList();
				for (int l = 0; l < soapReflectedMethod2.headers.Length; l++)
				{
					SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
					SoapReflectedHeader soapReflectedHeader = soapReflectedMethod2.headers[l];
					soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
					soapHeaderMapping.repeats = soapReflectedHeader.repeats;
					soapHeaderMapping.custom = soapReflectedHeader.custom;
					soapHeaderMapping.direction = soapReflectedHeader.direction;
					soapHeaderMapping.headerType = soapReflectedHeader.headerType;
					if (soapHeaderMapping.direction == SoapHeaderDirection.In)
					{
						arrayList4.Add(soapHeaderMapping);
					}
					else if (soapHeaderMapping.direction == SoapHeaderDirection.Out)
					{
						arrayList5.Add(soapHeaderMapping);
					}
					else
					{
						arrayList4.Add(soapHeaderMapping);
						arrayList5.Add(soapHeaderMapping);
					}
				}
				soapServerMethod.inHeaderMappings = (SoapHeaderMapping[])arrayList4.ToArray(typeof(SoapHeaderMapping));
				if (soapServerMethod.outHeaderSerializer != null)
				{
					soapServerMethod.outHeaderMappings = (SoapHeaderMapping[])arrayList5.ToArray(typeof(SoapHeaderMapping));
				}
				if (flag && !this.routingOnSoapAction && soapReflectedMethod2.requestElementName.IsEmpty)
				{
					throw new SoapException(Res.GetString("TheMethodDoesNotHaveARequestElementEither1", new object[] { soapServerMethod.methodInfo.Name }), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
				}
				if (this.methods[soapReflectedMethod2.action] == null)
				{
					this.methods[soapReflectedMethod2.action] = soapServerMethod;
				}
				else
				{
					if (flag && this.routingOnSoapAction)
					{
						SoapServerMethod soapServerMethod2 = (SoapServerMethod)this.methods[soapReflectedMethod2.action];
						throw new SoapException(Res.GetString("TheMethodsAndUseTheSameSoapActionWhenTheService3", new object[]
						{
							soapServerMethod.methodInfo.Name,
							soapServerMethod2.methodInfo.Name,
							soapReflectedMethod2.action
						}), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
					}
					this.duplicateMethods[soapReflectedMethod2.action] = soapServerMethod;
				}
				if (this.methods[soapReflectedMethod2.requestElementName] == null)
				{
					this.methods[soapReflectedMethod2.requestElementName] = soapServerMethod;
				}
				else
				{
					if (flag && !this.routingOnSoapAction)
					{
						SoapServerMethod soapServerMethod3 = (SoapServerMethod)this.methods[soapReflectedMethod2.requestElementName];
						throw new SoapException(Res.GetString("TheMethodsAndUseTheSameRequestElementXmlns4", new object[]
						{
							soapServerMethod.methodInfo.Name,
							soapServerMethod3.methodInfo.Name,
							soapReflectedMethod2.requestElementName.Name,
							soapReflectedMethod2.requestElementName.Namespace
						}), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
					}
					this.duplicateMethods[soapReflectedMethod2.requestElementName] = soapServerMethod;
				}
			}
		}

		/// <summary>Returns the <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</returns>
		/// <param name="key">The key associated with the desired <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
		// Token: 0x0600034E RID: 846 RVA: 0x0000F010 File Offset: 0x0000D210
		public SoapServerMethod GetMethod(object key)
		{
			return (SoapServerMethod)this.methods[key];
		}

		/// <summary>Returns the duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</summary>
		/// <returns>The duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> associated with the specified key.</returns>
		/// <param name="key">The key associated with the desired duplicate <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
		// Token: 0x0600034F RID: 847 RVA: 0x0000F023 File Offset: 0x0000D223
		public SoapServerMethod GetDuplicateMethod(object key)
		{
			return (SoapServerMethod)this.duplicateMethods[key];
		}

		// Token: 0x040002E8 RID: 744
		private Hashtable methods = new Hashtable();

		// Token: 0x040002E9 RID: 745
		private Hashtable duplicateMethods = new Hashtable();

		// Token: 0x040002EA RID: 746
		internal SoapReflectedExtension[] HighPriExtensions;

		// Token: 0x040002EB RID: 747
		internal SoapReflectedExtension[] LowPriExtensions;

		// Token: 0x040002EC RID: 748
		internal object[] HighPriExtensionInitializers;

		// Token: 0x040002ED RID: 749
		internal object[] LowPriExtensionInitializers;

		// Token: 0x040002EE RID: 750
		internal string serviceNamespace;

		// Token: 0x040002EF RID: 751
		internal bool serviceDefaultIsEncoded;

		// Token: 0x040002F0 RID: 752
		internal bool routingOnSoapAction;

		// Token: 0x040002F1 RID: 753
		internal WebServiceProtocols protocolsSupported;
	}
}
