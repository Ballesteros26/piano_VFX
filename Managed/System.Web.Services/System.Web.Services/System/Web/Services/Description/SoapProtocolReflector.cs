using System;
using System.Collections;
using System.Reflection;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	// Token: 0x0200012E RID: 302
	internal class SoapProtocolReflector : ProtocolReflector
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00002B54 File Offset: 0x00000D54
		internal override WsiProfiles ConformsTo
		{
			get
			{
				return WsiProfiles.BasicProfile1_1;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override string ProtocolName
		{
			get
			{
				return "Soap";
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0003F578 File Offset: 0x0003D778
		internal SoapReflectedMethod SoapMethod
		{
			get
			{
				return this.soapMethod;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0003F580 File Offset: 0x0003D780
		internal SoapReflectionImporter SoapImporter
		{
			get
			{
				SoapReflectionImporter soapReflectionImporter = base.ReflectionContext[typeof(SoapReflectionImporter)] as SoapReflectionImporter;
				if (soapReflectionImporter == null)
				{
					soapReflectionImporter = SoapReflector.CreateSoapImporter(base.DefaultNamespace, SoapReflector.ServiceDefaultIsEncoded(base.ServiceType));
					base.ReflectionContext[typeof(SoapReflectionImporter)] = soapReflectionImporter;
				}
				return soapReflectionImporter;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0003F5DC File Offset: 0x0003D7DC
		internal SoapSchemaExporter SoapExporter
		{
			get
			{
				SoapSchemaExporter soapSchemaExporter = base.ReflectionContext[typeof(SoapSchemaExporter)] as SoapSchemaExporter;
				if (soapSchemaExporter == null)
				{
					soapSchemaExporter = new SoapSchemaExporter(base.ServiceDescription.Types.Schemas);
					base.ReflectionContext[typeof(SoapSchemaExporter)] = soapSchemaExporter;
				}
				return soapSchemaExporter;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0003F634 File Offset: 0x0003D834
		protected override bool ReflectMethod()
		{
			this.soapMethod = base.ReflectionContext[base.Method] as SoapReflectedMethod;
			if (this.soapMethod == null)
			{
				this.soapMethod = SoapReflector.ReflectMethod(base.Method, false, base.ReflectionImporter, this.SoapImporter, base.DefaultNamespace);
				base.ReflectionContext[base.Method] = this.soapMethod;
				this.soapMethod.portType = ((base.Binding != null) ? base.Binding.Type : null);
			}
			WebMethodAttribute methodAttribute = base.Method.MethodAttribute;
			base.OperationBinding.Extensions.Add(this.CreateSoapOperationBinding(this.soapMethod.rpc ? SoapBindingStyle.Rpc : SoapBindingStyle.Document, this.soapMethod.action));
			this.CreateMessage(this.soapMethod.rpc, this.soapMethod.use, this.soapMethod.paramStyle, base.InputMessage, base.OperationBinding.Input, this.soapMethod.requestMappings);
			if (!this.soapMethod.oneWay)
			{
				this.CreateMessage(this.soapMethod.rpc, this.soapMethod.use, this.soapMethod.paramStyle, base.OutputMessage, base.OperationBinding.Output, this.soapMethod.responseMappings);
			}
			this.CreateHeaderMessages(this.soapMethod.name, this.soapMethod.use, this.soapMethod.inHeaderMappings, this.soapMethod.outHeaderMappings, this.soapMethod.headers, this.soapMethod.rpc);
			if (this.soapMethod.rpc && this.soapMethod.use == SoapBindingUse.Encoded && this.soapMethod.methodInfo.OutParameters.Length != 0)
			{
				base.Operation.ParameterOrder = SoapProtocolReflector.GetParameterOrder(this.soapMethod.methodInfo);
			}
			this.AllowExtensionsToReflectMethod();
			return true;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0003F82B File Offset: 0x0003DA2B
		protected override void ReflectDescription()
		{
			this.AllowExtensionsToReflectDescription();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003F834 File Offset: 0x0003DA34
		private void CreateHeaderMessages(string methodName, SoapBindingUse use, XmlMembersMapping inHeaderMappings, XmlMembersMapping outHeaderMappings, SoapReflectedHeader[] headers, bool rpc)
		{
			if (use == SoapBindingUse.Encoded)
			{
				this.SoapExporter.ExportMembersMapping(inHeaderMappings, false);
				if (outHeaderMappings != null)
				{
					this.SoapExporter.ExportMembersMapping(outHeaderMappings, false);
				}
			}
			else
			{
				base.SchemaExporter.ExportMembersMapping(inHeaderMappings);
				if (outHeaderMappings != null)
				{
					base.SchemaExporter.ExportMembersMapping(outHeaderMappings);
				}
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			int num = 0;
			int num2 = 0;
			foreach (SoapReflectedHeader soapReflectedHeader in headers)
			{
				if (soapReflectedHeader.custom)
				{
					XmlMemberMapping xmlMemberMapping;
					if ((soapReflectedHeader.direction & SoapHeaderDirection.In) != (SoapHeaderDirection)0)
					{
						xmlMemberMapping = inHeaderMappings[num++];
						if (soapReflectedHeader.direction != SoapHeaderDirection.In)
						{
							num2++;
						}
					}
					else
					{
						xmlMemberMapping = outHeaderMappings[num2++];
					}
					MessagePart messagePart = new MessagePart();
					messagePart.Name = xmlMemberMapping.XsdElementName;
					if (use == SoapBindingUse.Encoded)
					{
						messagePart.Type = new XmlQualifiedName(xmlMemberMapping.TypeName, xmlMemberMapping.TypeNamespace);
					}
					else
					{
						messagePart.Element = new XmlQualifiedName(xmlMemberMapping.XsdElementName, xmlMemberMapping.Namespace);
					}
					Message message = new Message();
					message.Name = codeIdentifiers.AddUnique(methodName + messagePart.Name, message);
					message.Parts.Add(messagePart);
					base.HeaderMessages.Add(message);
					ServiceDescriptionFormatExtension serviceDescriptionFormatExtension = this.CreateSoapHeaderBinding(new XmlQualifiedName(message.Name, base.Binding.ServiceDescription.TargetNamespace), messagePart.Name, rpc ? xmlMemberMapping.Namespace : null, use);
					if ((soapReflectedHeader.direction & SoapHeaderDirection.In) != (SoapHeaderDirection)0)
					{
						base.OperationBinding.Input.Extensions.Add(serviceDescriptionFormatExtension);
					}
					if ((soapReflectedHeader.direction & SoapHeaderDirection.Out) != (SoapHeaderDirection)0)
					{
						base.OperationBinding.Output.Extensions.Add(serviceDescriptionFormatExtension);
					}
					if ((soapReflectedHeader.direction & SoapHeaderDirection.Fault) != (SoapHeaderDirection)0)
					{
						if (this.soapMethod.IsClaimsConformance)
						{
							throw new InvalidOperationException(Res.GetString("BPConformanceHeaderFault", new object[]
							{
								this.soapMethod.methodInfo.ToString(),
								this.soapMethod.methodInfo.DeclaringType.FullName,
								"Direction",
								typeof(SoapHeaderDirection).Name,
								SoapHeaderDirection.Fault.ToString()
							}));
						}
						base.OperationBinding.Output.Extensions.Add(serviceDescriptionFormatExtension);
					}
				}
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		private void CreateMessage(bool rpc, SoapBindingUse use, SoapParameterStyle paramStyle, Message message, MessageBinding messageBinding, XmlMembersMapping members)
		{
			bool flag = paramStyle != SoapParameterStyle.Bare;
			if (use == SoapBindingUse.Encoded)
			{
				this.CreateEncodedMessage(message, messageBinding, members, flag && !rpc);
				return;
			}
			this.CreateLiteralMessage(message, messageBinding, members, flag && !rpc, rpc);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003FAE4 File Offset: 0x0003DCE4
		private void CreateEncodedMessage(Message message, MessageBinding messageBinding, XmlMembersMapping members, bool wrapped)
		{
			this.SoapExporter.ExportMembersMapping(members, wrapped);
			if (wrapped)
			{
				MessagePart messagePart = new MessagePart();
				messagePart.Name = "parameters";
				messagePart.Type = new XmlQualifiedName(members.TypeName, members.TypeNamespace);
				message.Parts.Add(messagePart);
			}
			else
			{
				for (int i = 0; i < members.Count; i++)
				{
					XmlMemberMapping xmlMemberMapping = members[i];
					MessagePart messagePart2 = new MessagePart();
					messagePart2.Name = xmlMemberMapping.XsdElementName;
					messagePart2.Type = new XmlQualifiedName(xmlMemberMapping.TypeName, xmlMemberMapping.TypeNamespace);
					message.Parts.Add(messagePart2);
				}
			}
			messageBinding.Extensions.Add(this.CreateSoapBodyBinding(SoapBindingUse.Encoded, members.Namespace));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		private void CreateLiteralMessage(Message message, MessageBinding messageBinding, XmlMembersMapping members, bool wrapped, bool rpc)
		{
			if (members.Count == 1 && members[0].Any && members[0].ElementName.Length == 0 && !wrapped)
			{
				string text = base.SchemaExporter.ExportAnyType(members[0].Namespace);
				MessagePart messagePart = new MessagePart();
				messagePart.Name = members[0].MemberName;
				messagePart.Type = new XmlQualifiedName(text, members[0].Namespace);
				message.Parts.Add(messagePart);
			}
			else
			{
				base.SchemaExporter.ExportMembersMapping(members, !rpc);
				if (wrapped)
				{
					MessagePart messagePart2 = new MessagePart();
					messagePart2.Name = "parameters";
					messagePart2.Element = new XmlQualifiedName(members.XsdElementName, members.Namespace);
					message.Parts.Add(messagePart2);
				}
				else
				{
					for (int i = 0; i < members.Count; i++)
					{
						XmlMemberMapping xmlMemberMapping = members[i];
						MessagePart messagePart3 = new MessagePart();
						if (rpc)
						{
							if (xmlMemberMapping.TypeName == null || xmlMemberMapping.TypeName.Length == 0)
							{
								throw new InvalidOperationException(Res.GetString("WsdlGenRpcLitAnonimousType", new object[]
								{
									base.Method.DeclaringType.Name,
									base.Method.Name,
									xmlMemberMapping.MemberName
								}));
							}
							messagePart3.Name = xmlMemberMapping.XsdElementName;
							messagePart3.Type = new XmlQualifiedName(xmlMemberMapping.TypeName, xmlMemberMapping.TypeNamespace);
						}
						else
						{
							messagePart3.Name = XmlConvert.EncodeLocalName(xmlMemberMapping.MemberName);
							messagePart3.Element = new XmlQualifiedName(xmlMemberMapping.XsdElementName, xmlMemberMapping.Namespace);
						}
						message.Parts.Add(messagePart3);
					}
				}
			}
			messageBinding.Extensions.Add(this.CreateSoapBodyBinding(SoapBindingUse.Literal, rpc ? members.Namespace : null));
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003FD94 File Offset: 0x0003DF94
		private static string[] GetParameterOrder(LogicalMethodInfo methodInfo)
		{
			ParameterInfo[] parameters = methodInfo.Parameters;
			string[] array = new string[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].Name;
			}
			return array;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003FDCB File Offset: 0x0003DFCB
		protected override string ReflectMethodBinding()
		{
			return SoapReflector.GetSoapMethodBinding(base.Method);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003FDD8 File Offset: 0x0003DFD8
		protected override void BeginClass()
		{
			if (base.Binding != null)
			{
				SoapBindingStyle soapBindingStyle;
				if (SoapReflector.GetSoapServiceAttribute(base.ServiceType) is SoapRpcServiceAttribute)
				{
					soapBindingStyle = SoapBindingStyle.Rpc;
				}
				else
				{
					soapBindingStyle = SoapBindingStyle.Document;
				}
				base.Binding.Extensions.Add(this.CreateSoapBinding(soapBindingStyle));
				SoapReflector.IncludeTypes(base.Methods, this.SoapImporter);
			}
			base.Port.Extensions.Add(this.CreateSoapAddressBinding(base.ServiceUrl));
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003FE4C File Offset: 0x0003E04C
		private void AllowExtensionsToReflectMethod()
		{
			if (this.extensions == null)
			{
				TypeElementCollection soapExtensionReflectorTypes = WebServicesSection.Current.SoapExtensionReflectorTypes;
				this.extensions = new SoapExtensionReflector[soapExtensionReflectorTypes.Count];
				for (int i = 0; i < this.extensions.Length; i++)
				{
					SoapExtensionReflector soapExtensionReflector = (SoapExtensionReflector)Activator.CreateInstance(soapExtensionReflectorTypes[i].Type);
					soapExtensionReflector.ReflectionContext = this;
					this.extensions[i] = soapExtensionReflector;
				}
			}
			SoapExtensionReflector[] array = this.extensions;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].ReflectMethod();
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003FEDC File Offset: 0x0003E0DC
		private void AllowExtensionsToReflectDescription()
		{
			if (this.extensions == null)
			{
				TypeElementCollection soapExtensionReflectorTypes = WebServicesSection.Current.SoapExtensionReflectorTypes;
				this.extensions = new SoapExtensionReflector[soapExtensionReflectorTypes.Count];
				for (int i = 0; i < this.extensions.Length; i++)
				{
					SoapExtensionReflector soapExtensionReflector = (SoapExtensionReflector)Activator.CreateInstance(soapExtensionReflectorTypes[i].Type);
					soapExtensionReflector.ReflectionContext = this;
					this.extensions[i] = soapExtensionReflector;
				}
			}
			SoapExtensionReflector[] array = this.extensions;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].ReflectDescription();
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0003FF69 File Offset: 0x0003E169
		protected virtual SoapBinding CreateSoapBinding(SoapBindingStyle style)
		{
			return new SoapBinding
			{
				Transport = "http://schemas.xmlsoap.org/soap/http",
				Style = style
			};
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0003FF84 File Offset: 0x0003E184
		protected virtual SoapAddressBinding CreateSoapAddressBinding(string serviceUrl)
		{
			SoapAddressBinding soapAddress = new SoapAddressBinding();
			soapAddress.Location = serviceUrl;
			if (base.UriFixups != null)
			{
				base.UriFixups.Add(delegate(Uri current)
				{
					soapAddress.Location = DiscoveryServerType.CombineUris(current, soapAddress.Location);
				});
			}
			return soapAddress;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0003FFD3 File Offset: 0x0003E1D3
		protected virtual SoapOperationBinding CreateSoapOperationBinding(SoapBindingStyle style, string action)
		{
			return new SoapOperationBinding
			{
				SoapAction = action,
				Style = style
			};
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0003FFE8 File Offset: 0x0003E1E8
		protected virtual SoapBodyBinding CreateSoapBodyBinding(SoapBindingUse use, string ns)
		{
			SoapBodyBinding soapBodyBinding = new SoapBodyBinding();
			soapBodyBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soapBodyBinding.Encoding = "http://schemas.xmlsoap.org/soap/encoding/";
			}
			soapBodyBinding.Namespace = ns;
			return soapBodyBinding;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0003C23D File Offset: 0x0003A43D
		protected virtual SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, SoapBindingUse use)
		{
			return this.CreateSoapHeaderBinding(message, partName, null, use);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0004001C File Offset: 0x0003E21C
		protected virtual SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, string ns, SoapBindingUse use)
		{
			SoapHeaderBinding soapHeaderBinding = new SoapHeaderBinding();
			soapHeaderBinding.Message = message;
			soapHeaderBinding.Part = partName;
			soapHeaderBinding.Use = use;
			if (use == SoapBindingUse.Encoded)
			{
				soapHeaderBinding.Encoding = "http://schemas.xmlsoap.org/soap/encoding/";
				soapHeaderBinding.Namespace = ns;
			}
			return soapHeaderBinding;
		}

		// Token: 0x0400056C RID: 1388
		private ArrayList mappings = new ArrayList();

		// Token: 0x0400056D RID: 1389
		private SoapExtensionReflector[] extensions;

		// Token: 0x0400056E RID: 1390
		private SoapReflectedMethod soapMethod;
	}
}
