using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	/// <summary>Represents the attributes and metadata for an XML Web service method. This class cannot be inherited.</summary>
	// Token: 0x0200007C RID: 124
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SoapServerMethod
	{
		/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</summary>
		// Token: 0x06000336 RID: 822 RVA: 0x0000210F File Offset: 0x0000030F
		public SoapServerMethod()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</summary>
		/// <param name="serverType">The <see cref="T:System.Type" /> to which this method belongs.</param>
		/// <param name="methodInfo">The <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> with which to initialize this <see cref="T:System.Web.Services.Protocols.SoapServerMethod" />.</param>
		// Token: 0x06000337 RID: 823 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public SoapServerMethod(Type serverType, LogicalMethodInfo methodInfo)
		{
			this.methodInfo = methodInfo;
			string @namespace = WebServiceReflector.GetAttribute(serverType).Namespace;
			bool flag = SoapReflector.ServiceDefaultIsEncoded(serverType);
			SoapReflectionImporter soapReflectionImporter = SoapReflector.CreateSoapImporter(@namespace, flag);
			XmlReflectionImporter xmlReflectionImporter = SoapReflector.CreateXmlImporter(@namespace, flag);
			SoapReflector.IncludeTypes(methodInfo, soapReflectionImporter);
			WebMethodReflector.IncludeTypes(methodInfo, xmlReflectionImporter);
			SoapReflectedMethod soapReflectedMethod = SoapReflector.ReflectMethod(methodInfo, false, xmlReflectionImporter, soapReflectionImporter, @namespace);
			this.ImportReflectedMethod(soapReflectedMethod);
			this.ImportSerializers(soapReflectedMethod, this.GetServerTypeEvidence(serverType));
			this.ImportHeaderSerializers(soapReflectedMethod);
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> associated with this XML Web service method.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> associated with this XML Web service method.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000E61A File Offset: 0x0000C81A
		public LogicalMethodInfo MethodInfo
		{
			get
			{
				return this.methodInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with return values from this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with return values from this Web service method.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E622 File Offset: 0x0000C822
		public XmlSerializer ReturnSerializer
		{
			get
			{
				return this.returnSerializer;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with parameters that are passed to this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with parameters that are passed to this Web service method.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000E62A File Offset: 0x0000C82A
		public XmlSerializer ParameterSerializer
		{
			get
			{
				return this.parameterSerializer;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP requests to this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP requests to this Web service method.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000E632 File Offset: 0x0000C832
		public XmlSerializer InHeaderSerializer
		{
			get
			{
				return this.inHeaderSerializer;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP responses from this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializer" /> used with SOAP responses from this Web service method.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000E63A File Offset: 0x0000C83A
		public XmlSerializer OutHeaderSerializer
		{
			get
			{
				return this.outHeaderSerializer;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP requests to this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP requests to this Web service method.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E642 File Offset: 0x0000C842
		public SoapHeaderMapping[] InHeaderMappings
		{
			get
			{
				return this.inHeaderMappings;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP responses from this Web service method.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> used with SOAP responses from this Web service method.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000E64A File Offset: 0x0000C84A
		public SoapHeaderMapping[] OutHeaderMappings
		{
			get
			{
				return this.outHeaderMappings;
			}
		}

		/// <summary>Gets the SOAPAction HTTP header field of SOAP requests that are sent to this XML Web service method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SOAPAction HTTP header field of SOAP requests that are sent to this XML Web service method.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E652 File Offset: 0x0000C852
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> that indicates whether an XML Web service client waits for the Web server to finish processing this XML Web service method.</summary>
		/// <returns>true if the XML Web service client does not wait for the Web server to completely process this XML Web service method; otherwise, false.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000E65A File Offset: 0x0000C85A
		public bool OneWay
		{
			get
			{
				return this.oneWay;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> that indicates whether SOAP messages sent to and from this XML Web service method use RPC formatting.</summary>
		/// <returns>true if SOAP messages sent to and from this XML Web service method use RPC formatting; otherwise, false.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E662 File Offset: 0x0000C862
		public bool Rpc
		{
			get
			{
				return this.rpc;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.SoapBindingUse" /> value that specifies whether the parts of SOAP messages sent to this XML Web service method are encoded as abstract type definitions or concrete schema definitions.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.SoapBindingUse" /> value that specifies whether the parts of SOAP messages sent to this XML Web service method are encoded as abstract type definitions or concrete schema definitions.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000E66A File Offset: 0x0000C86A
		public SoapBindingUse BindingUse
		{
			get
			{
				return this.use;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> object that specifies how parameters are formatted in SOAP messages sent to this XML Web service method.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> that specifies how parameters are formatted in SOAP messages sent to this XML Web service method.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E672 File Offset: 0x0000C872
		public SoapParameterStyle ParameterStyle
		{
			get
			{
				return this.paramStyle;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.WsiProfiles" /> value that indicates the Web Services Interoperability (WSI) specification to which this Web service claims to conform.</summary>
		/// <returns>A <see cref="T:System.Web.Services.WsiProfiles" /> value that indicates the Web Services Interoperability (WSI) specification to which this Web service claims to conform.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000E67A File Offset: 0x0000C87A
		public WsiProfiles WsiClaims
		{
			get
			{
				return this.wsiClaims;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000E682 File Offset: 0x0000C882
		[SecurityPermission(SecurityAction.Assert, ControlEvidence = true)]
		private Evidence GetServerTypeEvidence(Type type)
		{
			return type.Assembly.Evidence;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000E690 File Offset: 0x0000C890
		private List<XmlMapping> GetXmlMappingsForMethod(SoapReflectedMethod soapMethod)
		{
			List<XmlMapping> list = new List<XmlMapping>();
			list.Add(soapMethod.requestMappings);
			if (soapMethod.responseMappings != null)
			{
				list.Add(soapMethod.responseMappings);
			}
			list.Add(soapMethod.inHeaderMappings);
			if (soapMethod.outHeaderMappings != null)
			{
				list.Add(soapMethod.outHeaderMappings);
			}
			return list;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		private void ImportReflectedMethod(SoapReflectedMethod soapMethod)
		{
			this.action = soapMethod.action;
			this.extensions = soapMethod.extensions;
			this.extensionInitializers = SoapReflectedExtension.GetInitializers(this.methodInfo, soapMethod.extensions);
			this.oneWay = soapMethod.oneWay;
			this.rpc = soapMethod.rpc;
			this.use = soapMethod.use;
			this.paramStyle = soapMethod.paramStyle;
			this.wsiClaims = ((soapMethod.binding == null) ? WsiProfiles.None : soapMethod.binding.ConformsTo);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000E76C File Offset: 0x0000C96C
		private void ImportHeaderSerializers(SoapReflectedMethod soapMethod)
		{
			List<SoapHeaderMapping> list = new List<SoapHeaderMapping>();
			List<SoapHeaderMapping> list2 = new List<SoapHeaderMapping>();
			for (int i = 0; i < soapMethod.headers.Length; i++)
			{
				SoapHeaderMapping soapHeaderMapping = new SoapHeaderMapping();
				SoapReflectedHeader soapReflectedHeader = soapMethod.headers[i];
				soapHeaderMapping.memberInfo = soapReflectedHeader.memberInfo;
				soapHeaderMapping.repeats = soapReflectedHeader.repeats;
				soapHeaderMapping.custom = soapReflectedHeader.custom;
				soapHeaderMapping.direction = soapReflectedHeader.direction;
				soapHeaderMapping.headerType = soapReflectedHeader.headerType;
				if (soapHeaderMapping.direction == SoapHeaderDirection.In)
				{
					list.Add(soapHeaderMapping);
				}
				else if (soapHeaderMapping.direction == SoapHeaderDirection.Out)
				{
					list2.Add(soapHeaderMapping);
				}
				else
				{
					list.Add(soapHeaderMapping);
					list2.Add(soapHeaderMapping);
				}
			}
			this.inHeaderMappings = list.ToArray();
			if (this.outHeaderSerializer != null)
			{
				this.outHeaderMappings = list2.ToArray();
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000E844 File Offset: 0x0000CA44
		private void ImportSerializers(SoapReflectedMethod soapMethod, Evidence serverEvidence)
		{
			XmlMapping[] array = this.GetXmlMappingsForMethod(soapMethod).ToArray();
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "ImportSerializers", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceCreateSerializer"), traceMethod, new TraceMethod(typeof(XmlSerializer), "FromMappings", new object[] { array, serverEvidence }));
			}
			XmlSerializer[] array2;
			if (AppDomain.CurrentDomain.IsHomogenous)
			{
				array2 = XmlSerializer.FromMappings(array);
			}
			else
			{
				array2 = XmlSerializer.FromMappings(array, serverEvidence);
			}
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceCreateSerializer"), traceMethod);
			}
			int num = 0;
			this.parameterSerializer = array2[num++];
			if (soapMethod.responseMappings != null)
			{
				this.returnSerializer = array2[num++];
			}
			this.inHeaderSerializer = array2[num++];
			if (soapMethod.outHeaderMappings != null)
			{
				this.outHeaderSerializer = array2[num++];
			}
		}

		// Token: 0x040002D9 RID: 729
		internal LogicalMethodInfo methodInfo;

		// Token: 0x040002DA RID: 730
		internal XmlSerializer returnSerializer;

		// Token: 0x040002DB RID: 731
		internal XmlSerializer parameterSerializer;

		// Token: 0x040002DC RID: 732
		internal XmlSerializer inHeaderSerializer;

		// Token: 0x040002DD RID: 733
		internal XmlSerializer outHeaderSerializer;

		// Token: 0x040002DE RID: 734
		internal SoapHeaderMapping[] inHeaderMappings;

		// Token: 0x040002DF RID: 735
		internal SoapHeaderMapping[] outHeaderMappings;

		// Token: 0x040002E0 RID: 736
		internal SoapReflectedExtension[] extensions;

		// Token: 0x040002E1 RID: 737
		internal object[] extensionInitializers;

		// Token: 0x040002E2 RID: 738
		internal string action;

		// Token: 0x040002E3 RID: 739
		internal bool oneWay;

		// Token: 0x040002E4 RID: 740
		internal bool rpc;

		// Token: 0x040002E5 RID: 741
		internal SoapBindingUse use;

		// Token: 0x040002E6 RID: 742
		internal SoapParameterStyle paramStyle;

		// Token: 0x040002E7 RID: 743
		internal WsiProfiles wsiClaims;
	}
}
