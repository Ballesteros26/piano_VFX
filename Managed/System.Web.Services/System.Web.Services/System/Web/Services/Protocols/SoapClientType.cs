using System;
using System.Collections;
using System.Reflection;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200005B RID: 91
	internal class SoapClientType
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00009E54 File Offset: 0x00008054
		internal SoapClientType(Type type)
		{
			this.binding = WebServiceBindingReflector.GetAttribute(type);
			if (this.binding == null)
			{
				throw new InvalidOperationException(Res.GetString("WebClientBindingAttributeRequired"));
			}
			this.serviceNamespace = this.binding.Namespace;
			this.serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(type);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			SoapClientType.GenerateXmlMappings(type, arrayList, this.serviceNamespace, this.serviceDefaultIsEncoded, arrayList2);
			XmlMapping[] array = (XmlMapping[])arrayList2.ToArray(typeof(XmlMapping));
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, ".ctor", new object[] { type }) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), traceMethod, new TraceMethod(typeof(XmlSerializer), "FromMappings", new object[] { array, type }));
			}
			XmlSerializer[] array2 = XmlSerializer.FromMappings(array, type);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), traceMethod);
			}
			SoapExtensionTypeElementCollection soapExtensionTypes = WebServicesSection.Current.SoapExtensionTypes;
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			for (int i = 0; i < soapExtensionTypes.Count; i++)
			{
				SoapExtensionTypeElement soapExtensionTypeElement = soapExtensionTypes[i];
				SoapReflectedExtension soapReflectedExtension = new SoapReflectedExtension(soapExtensionTypes[i].Type, null, soapExtensionTypes[i].Priority);
				if (soapExtensionTypes[i].Group == PriorityGroup.High)
				{
					arrayList3.Add(soapReflectedExtension);
				}
				else
				{
					arrayList4.Add(soapReflectedExtension);
				}
			}
			this.HighPriExtensions = (SoapReflectedExtension[])arrayList3.ToArray(typeof(SoapReflectedExtension));
			this.LowPriExtensions = (SoapReflectedExtension[])arrayList4.ToArray(typeof(SoapReflectedExtension));
			Array.Sort<SoapReflectedExtension>(this.HighPriExtensions);
			Array.Sort<SoapReflectedExtension>(this.LowPriExtensions);
			this.HighPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, this.HighPriExtensions);
			this.LowPriExtensionInitializers = SoapReflectedExtension.GetInitializers(type, this.LowPriExtensions);
			int num = 0;
			for (int j = 0; j < arrayList.Count; j++)
			{
				SoapReflectedMethod soapReflectedMethod = (SoapReflectedMethod)arrayList[j];
				SoapClientMethod soapClientMethod = new SoapClientMethod();
				soapClientMethod.parameterSerializer = array2[num++];
				if (soapReflectedMethod.responseMappings != null)
				{
					soapClientMethod.returnSerializer = array2[num++];
				}
				soapClientMethod.inHeaderSerializer = array2[num++];
				if (soapReflectedMethod.outHeaderMappings != null)
				{
					soapClientMethod.outHeaderSerializer = array2[num++];
				}
				soapClientMethod.action = soapReflectedMethod.action;
				soapClientMethod.oneWay = soapReflectedMethod.oneWay;
				soapClientMethod.rpc = soapReflectedMethod.rpc;
				soapClientMethod.use = soapReflectedMethod.use;
				soapClientMethod.paramStyle = soapReflectedMethod.paramStyle;
				soapClientMethod.methodInfo = soapReflectedMethod.methodInfo;
				soapClientMethod.extensions = soapReflectedMethod.extensions;
				soapClientMethod.extensionInitializers = SoapReflectedExtension.GetInitializers(soapClientMethod.methodInfo, soapReflectedMethod.extensions);
				ArrayList arrayList5 = new ArrayList();
				ArrayList arrayList6 = new ArrayList();
				for (int k = 0; k < soapReflectedMethod.headers.Length; k++)
				{
					SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
					SoapReflectedHeader soapReflectedHeader = soapReflectedMethod.headers[k];
					soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
					soapHeaderMapping.repeats = soapReflectedHeader.repeats;
					soapHeaderMapping.custom = soapReflectedHeader.custom;
					soapHeaderMapping.direction = soapReflectedHeader.direction;
					soapHeaderMapping.headerType = soapReflectedHeader.headerType;
					if ((soapHeaderMapping.direction & SoapHeaderDirection.In) != (SoapHeaderDirection)0)
					{
						arrayList5.Add(soapHeaderMapping);
					}
					if ((soapHeaderMapping.direction & (SoapHeaderDirection.Out | SoapHeaderDirection.Fault)) != (SoapHeaderDirection)0)
					{
						arrayList6.Add(soapHeaderMapping);
					}
				}
				soapClientMethod.inHeaderMappings = (SoapHeaderMapping[])arrayList5.ToArray(typeof(SoapHeaderMapping));
				if (soapClientMethod.outHeaderSerializer != null)
				{
					soapClientMethod.outHeaderMappings = (SoapHeaderMapping[])arrayList6.ToArray(typeof(SoapHeaderMapping));
				}
				this.methods.Add(soapReflectedMethod.name, soapClientMethod);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A264 File Offset: 0x00008464
		internal static void GenerateXmlMappings(Type type, ArrayList soapMethodList, string serviceNamespace, bool serviceDefaultIsEncoded, ArrayList mappings)
		{
			LogicalMethodInfo[] array = LogicalMethodInfo.Create(type.GetMethods(BindingFlags.Instance | BindingFlags.Public), LogicalMethodTypes.Sync);
			SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(serviceNamespace, serviceDefaultIsEncoded);
			XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(serviceNamespace, serviceDefaultIsEncoded);
			WebMethodReflector.IncludeTypes(array, xmlReflectionImporter);
			SoapReflector.IncludeTypes(array, soapReflectionImporter);
			for (int i = 0; i < array.Length; i++)
			{
				SoapReflectedMethod soapReflectedMethod = SoapReflector.ReflectMethod(array[i], true, xmlReflectionImporter, soapReflectionImporter, serviceNamespace);
				if (soapReflectedMethod != null)
				{
					soapMethodList.Add(soapReflectedMethod);
					mappings.Add(soapReflectedMethod.requestMappings);
					if (soapReflectedMethod.responseMappings != null)
					{
						mappings.Add(soapReflectedMethod.responseMappings);
					}
					mappings.Add(soapReflectedMethod.inHeaderMappings);
					if (soapReflectedMethod.outHeaderMappings != null)
					{
						mappings.Add(soapReflectedMethod.outHeaderMappings);
					}
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A315 File Offset: 0x00008515
		internal SoapClientMethod GetMethod(string name)
		{
			return (SoapClientMethod)this.methods[name];
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000A328 File Offset: 0x00008528
		internal WebServiceBindingAttribute Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x0400023D RID: 573
		private Hashtable methods = new Hashtable();

		// Token: 0x0400023E RID: 574
		private WebServiceBindingAttribute binding;

		// Token: 0x0400023F RID: 575
		internal SoapReflectedExtension[] HighPriExtensions;

		// Token: 0x04000240 RID: 576
		internal SoapReflectedExtension[] LowPriExtensions;

		// Token: 0x04000241 RID: 577
		internal object[] HighPriExtensionInitializers;

		// Token: 0x04000242 RID: 578
		internal object[] LowPriExtensionInitializers;

		// Token: 0x04000243 RID: 579
		internal string serviceNamespace;

		// Token: 0x04000244 RID: 580
		internal bool serviceDefaultIsEncoded;
	}
}
