using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Design;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Generates classes for Web services that use the SOAP protocol.</summary>
	// Token: 0x0200012D RID: 301
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class SoapProtocolImporter : ProtocolImporter
	{
		/// <summary>Gets a value of "Soap".</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the string "Soap".</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0003CF10 File Offset: 0x0003B110
		public override string ProtocolName
		{
			get
			{
				return "Soap";
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Services.Description.SoapBinding" /> instance obtained through the <see cref="P:System.Web.Services.Description.SoapProtocolImporter.SoapBinding" /> property's <see cref="P:System.Web.Services.Description.Binding.Extensions" /> property.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.SoapBinding" /> instance obtained through the <see cref="P:System.Web.Services.Description.SoapProtocolImporter.SoapBinding" /> property's <see cref="P:System.Web.Services.Description.Binding.Extensions" /> property.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0003CF17 File Offset: 0x0003B117
		public SoapBinding SoapBinding
		{
			get
			{
				return this.soapBinding;
			}
		}

		/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.SoapSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between SOAP-encoded XML schema content and .NET Framework types.</summary>
		/// <returns>An object of type <see cref="T:System.Xml.Serialization.SoapSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between SOAP-encoded XML schema content and .NET Framework types.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0003CF1F File Offset: 0x0003B11F
		public SoapSchemaImporter SoapImporter
		{
			get
			{
				return this.soapImporter;
			}
		}

		/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between literal XML schema content and .NET Framework types.</summary>
		/// <returns>An object of type <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate mappings between literal XML schema content and .NET Framework types.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0003CF27 File Offset: 0x0003B127
		public XmlSchemaImporter XmlImporter
		{
			get
			{
				return this.xmlImporter;
			}
		}

		/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between literal XML schema content and .NET Framework types.</summary>
		/// <returns>An object of type <see cref="T:System.Xml.Serialization.XmlCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between literal XML schema content and .NET Framework types.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0003CF2F File Offset: 0x0003B12F
		public XmlCodeExporter XmlExporter
		{
			get
			{
				return this.xmlExporter;
			}
		}

		/// <summary>Gets the object of type <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between SOAP-encoded XML schema content and .NET Framework types.</summary>
		/// <returns>An object of type <see cref="T:System.Xml.Serialization.SoapCodeExporter" /> used internally by the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> class to generate code from mappings between SOAP-encoded XML schema content and .NET Framework types.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0003CF37 File Offset: 0x0003B137
		public SoapCodeExporter SoapExporter
		{
			get
			{
				return this.soapExporter;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0003CF3F File Offset: 0x0003B13F
		private static TypedDataSetSchemaImporterExtension TypedDataSetSchemaImporterExtension
		{
			get
			{
				if (SoapProtocolImporter.typedDataSetSchemaImporterExtension == null)
				{
					SoapProtocolImporter.typedDataSetSchemaImporterExtension = new TypedDataSetSchemaImporterExtension();
				}
				return SoapProtocolImporter.typedDataSetSchemaImporterExtension;
			}
		}

		/// <summary>Performs initialization for an entire code namespace during code generation.</summary>
		// Token: 0x06000901 RID: 2305 RVA: 0x0003CF58 File Offset: 0x0003B158
		protected override void BeginNamespace()
		{
			try
			{
				base.MethodNames.Clear();
				base.ExtraCodeClasses.Clear();
				this.soapImporter = new SoapSchemaImporter(base.AbstractSchemas, base.ServiceImporter.CodeGenerationOptions, base.ImportContext);
				this.xmlImporter = new XmlSchemaImporter(base.ConcreteSchemas, base.ServiceImporter.CodeGenerationOptions, base.ServiceImporter.CodeGenerator, base.ImportContext);
				foreach (Type type in base.ServiceImporter.Extensions)
				{
					this.xmlImporter.Extensions.Add(type.FullName, type);
				}
				this.xmlImporter.Extensions.Add(SoapProtocolImporter.TypedDataSetSchemaImporterExtension);
				this.xmlImporter.Extensions.Add(new DataSetSchemaImporterExtension());
				this.xmlExporter = new XmlCodeExporter(base.CodeNamespace, base.ServiceImporter.CodeCompileUnit, base.ServiceImporter.CodeGenerator, base.ServiceImporter.CodeGenerationOptions, base.ExportContext);
				this.soapExporter = new SoapCodeExporter(base.CodeNamespace, null, base.ServiceImporter.CodeGenerator, base.ServiceImporter.CodeGenerationOptions, base.ExportContext);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidOperationException(Res.GetString("InitFailed"), ex);
			}
		}

		/// <summary>Performs processing for an entire code namespace after binding class generation.</summary>
		// Token: 0x06000902 RID: 2306 RVA: 0x0003D10C File Offset: 0x0003B30C
		protected override void EndNamespace()
		{
			base.ConcreteSchemas.Compile(null, false);
			foreach (object obj in this.headers.Values)
			{
				GlobalSoapHeader globalSoapHeader = (GlobalSoapHeader)obj;
				if (globalSoapHeader.isEncoded)
				{
					this.soapExporter.ExportTypeMapping(globalSoapHeader.mapping);
				}
				else
				{
					this.xmlExporter.ExportTypeMapping(globalSoapHeader.mapping);
				}
			}
			foreach (object obj2 in this.xmlMembers)
			{
				XmlMembersMapping xmlMembersMapping = (XmlMembersMapping)obj2;
				this.xmlExporter.ExportMembersMapping(xmlMembersMapping);
			}
			foreach (object obj3 in this.soapMembers)
			{
				XmlMembersMapping xmlMembersMapping2 = (XmlMembersMapping)obj3;
				this.soapExporter.ExportMembersMapping(xmlMembersMapping2);
			}
			foreach (object obj4 in this.codeClasses)
			{
				CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj4;
				foreach (object obj5 in this.xmlExporter.IncludeMetadata)
				{
					CodeAttributeDeclaration codeAttributeDeclaration = (CodeAttributeDeclaration)obj5;
					codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
				}
				foreach (object obj6 in this.soapExporter.IncludeMetadata)
				{
					CodeAttributeDeclaration codeAttributeDeclaration2 = (CodeAttributeDeclaration)obj6;
					codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration2);
				}
			}
			foreach (object obj7 in base.ExtraCodeClasses)
			{
				CodeTypeDeclaration codeTypeDeclaration2 = (CodeTypeDeclaration)obj7;
				base.CodeNamespace.Types.Add(codeTypeDeclaration2);
			}
			CodeGenerator.ValidateIdentifiers(base.CodeNamespace);
		}

		/// <summary>Determines whether a class can be generated for the current binding.</summary>
		/// <returns>true if the class can be generated for the current binding; otherwise false.</returns>
		// Token: 0x06000903 RID: 2307 RVA: 0x0003D394 File Offset: 0x0003B594
		protected override bool IsBindingSupported()
		{
			SoapBinding soapBinding = (SoapBinding)base.Binding.Extensions.Find(typeof(SoapBinding));
			if (soapBinding == null || soapBinding.GetType() != typeof(SoapBinding))
			{
				return false;
			}
			if (this.GetTransport(soapBinding.Transport) == null)
			{
				base.UnsupportedBindingWarning(Res.GetString("ThereIsNoSoapTransportImporterThatUnderstands1", new object[] { soapBinding.Transport }));
				return false;
			}
			return true;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003D410 File Offset: 0x0003B610
		internal SoapTransportImporter GetTransport(string transport)
		{
			Type[] soapTransportImporters = WebServicesSection.Current.SoapTransportImporters;
			for (int i = 0; i < soapTransportImporters.Length; i++)
			{
				SoapTransportImporter soapTransportImporter = (SoapTransportImporter)Activator.CreateInstance(soapTransportImporters[i]);
				soapTransportImporter.ImportContext = this;
				if (soapTransportImporter.IsSupportedTransport(transport))
				{
					return soapTransportImporter;
				}
			}
			return null;
		}

		/// <summary>Initializes the generation of a binding class.</summary>
		/// <returns>A binding class.</returns>
		// Token: 0x06000905 RID: 2309 RVA: 0x0003D458 File Offset: 0x0003B658
		protected override CodeTypeDeclaration BeginClass()
		{
			base.MethodNames.Clear();
			this.soapBinding = (SoapBinding)base.Binding.Extensions.Find(typeof(SoapBinding));
			this.transport = this.GetTransport(this.soapBinding.Transport);
			Type[] array = new Type[]
			{
				typeof(SoapDocumentMethodAttribute),
				typeof(XmlAttributeAttribute),
				typeof(WebService),
				typeof(object),
				typeof(DebuggerStepThroughAttribute),
				typeof(DesignerCategoryAttribute)
			};
			WebCodeGenerator.AddImports(base.CodeNamespace, WebCodeGenerator.GetNamespacesForTypes(array));
			CodeFlags codeFlags = (CodeFlags)0;
			if (base.Style == ServiceDescriptionImportStyle.Server)
			{
				codeFlags = CodeFlags.IsAbstract;
			}
			else if (base.Style == ServiceDescriptionImportStyle.ServerInterface)
			{
				codeFlags = CodeFlags.IsInterface;
			}
			CodeTypeDeclaration codeTypeDeclaration = WebCodeGenerator.CreateClass(base.ClassName, null, new string[0], null, CodeFlags.IsPublic | codeFlags, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes));
			codeTypeDeclaration.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			if (base.Style == ServiceDescriptionImportStyle.Client)
			{
				codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DebuggerStepThroughAttribute).FullName));
				codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(typeof(DesignerCategoryAttribute).FullName, new CodeAttributeArgument[]
				{
					new CodeAttributeArgument(new CodePrimitiveExpression("code"))
				}));
			}
			else if (base.Style == ServiceDescriptionImportStyle.Server)
			{
				CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(WebServiceAttribute).FullName);
				string text = ((base.Service != null) ? base.Service.ServiceDescription.TargetNamespace : base.Binding.ServiceDescription.TargetNamespace);
				codeAttributeDeclaration.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(text)));
				codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration);
			}
			CodeAttributeDeclaration codeAttributeDeclaration2 = new CodeAttributeDeclaration(typeof(WebServiceBindingAttribute).FullName);
			codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Name", new CodePrimitiveExpression(XmlConvert.DecodeName(base.Binding.Name))));
			codeAttributeDeclaration2.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression(base.Binding.ServiceDescription.TargetNamespace)));
			codeTypeDeclaration.CustomAttributes.Add(codeAttributeDeclaration2);
			this.codeClasses.Add(codeTypeDeclaration);
			this.classHeaders.Clear();
			return codeTypeDeclaration;
		}

		/// <summary>Processes a binding class after the generation of methods.</summary>
		// Token: 0x06000906 RID: 2310 RVA: 0x0003D6DF File Offset: 0x0003B8DF
		protected override void EndClass()
		{
			if (this.transport != null)
			{
				this.transport.ImportClass();
			}
			this.soapBinding = null;
		}

		/// <summary>Determines whether the current operation's operation flow is supported.</summary>
		/// <returns>true if the current operation's operation flow is supported; otherwise false.</returns>
		/// <param name="flow">An <see cref="T:System.Web.Services.Description.OperationFlow" /> enumeration value that represents a transmission pattern.</param>
		// Token: 0x06000907 RID: 2311 RVA: 0x0003D6FB File Offset: 0x0003B8FB
		protected override bool IsOperationFlowSupported(OperationFlow flow)
		{
			return flow == OperationFlow.OneWay || flow == OperationFlow.RequestResponse;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0003D707 File Offset: 0x0003B907
		private void BeginMetadata()
		{
			this.propertyNames.Clear();
			this.propertyValues.Clear();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0003D71F File Offset: 0x0003B91F
		private bool MetadataPropertiesAdded
		{
			get
			{
				return this.propertyNames.Count > 0;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0003D72F File Offset: 0x0003B92F
		private void AddMetadataProperty(string name, object value)
		{
			this.AddMetadataProperty(name, new CodePrimitiveExpression(value));
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0003D73E File Offset: 0x0003B93E
		private void AddMetadataProperty(string name, CodeExpression expr)
		{
			this.propertyNames.Add(name);
			this.propertyValues.Add(expr);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0003D75C File Offset: 0x0003B95C
		private void EndMetadata(CodeAttributeDeclarationCollection metadata, Type attributeType, string parameter)
		{
			CodeExpression[] array;
			if (parameter == null)
			{
				array = new CodeExpression[0];
			}
			else
			{
				array = new CodeExpression[]
				{
					new CodePrimitiveExpression(parameter)
				};
			}
			WebCodeGenerator.AddCustomAttribute(metadata, attributeType, array, (string[])this.propertyNames.ToArray(typeof(string)), (CodeExpression[])this.propertyValues.ToArray(typeof(CodeExpression)));
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		private void GenerateExtensionMetadata(CodeAttributeDeclarationCollection metadata)
		{
			if (this.extensions == null)
			{
				TypeElementCollection soapExtensionImporterTypes = WebServicesSection.Current.SoapExtensionImporterTypes;
				this.extensions = new SoapExtensionImporter[soapExtensionImporterTypes.Count];
				for (int i = 0; i < this.extensions.Length; i++)
				{
					SoapExtensionImporter soapExtensionImporter = (SoapExtensionImporter)Activator.CreateInstance(soapExtensionImporterTypes[i].Type);
					soapExtensionImporter.ImportContext = this;
					this.extensions[i] = soapExtensionImporter;
				}
			}
			SoapExtensionImporter[] array = this.extensions;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].ImportMethod(metadata);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0003D854 File Offset: 0x0003BA54
		private void PrepareHeaders(MessageBinding messageBinding)
		{
			SoapHeaderBinding[] array = (SoapHeaderBinding[])messageBinding.Extensions.FindAll(typeof(SoapHeaderBinding));
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MapToProperty = true;
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0003D894 File Offset: 0x0003BA94
		private void GenerateHeaders(CodeAttributeDeclarationCollection metadata, SoapBindingUse use, bool rpc, MessageBinding requestMessage, MessageBinding responseMessage)
		{
			Hashtable hashtable = new Hashtable();
			int i = 0;
			while (i < 2)
			{
				MessageBinding messageBinding;
				if (i == 0)
				{
					messageBinding = requestMessage;
					SoapHeaderDirection soapHeaderDirection = SoapHeaderDirection.In;
					goto IL_0023;
				}
				if (responseMessage != null)
				{
					messageBinding = responseMessage;
					SoapHeaderDirection soapHeaderDirection = SoapHeaderDirection.Out;
					goto IL_0023;
				}
				IL_0422:
				i++;
				continue;
				IL_0023:
				foreach (SoapHeaderBinding soapHeaderBinding in (SoapHeaderBinding[])messageBinding.Extensions.FindAll(typeof(SoapHeaderBinding)))
				{
					if (soapHeaderBinding.MapToProperty)
					{
						if (use != soapHeaderBinding.Use)
						{
							throw new InvalidOperationException(Res.GetString("WebDescriptionHeaderAndBodyUseMismatch"));
						}
						if (use == SoapBindingUse.Encoded && !this.IsSoapEncodingPresent(soapHeaderBinding.Encoding))
						{
							throw new InvalidOperationException(Res.GetString("WebUnknownEncodingStyle", new object[] { soapHeaderBinding.Encoding }));
						}
						Message message = base.ServiceDescriptions.GetMessage(soapHeaderBinding.Message);
						if (message == null)
						{
							throw new InvalidOperationException(Res.GetString("MissingMessage2", new object[]
							{
								soapHeaderBinding.Message.Name,
								soapHeaderBinding.Message.Namespace
							}));
						}
						MessagePart messagePart = message.FindPartByName(soapHeaderBinding.Part);
						if (messagePart == null)
						{
							throw new InvalidOperationException(Res.GetString("MissingMessagePartForMessageFromNamespace3", new object[]
							{
								messagePart.Name,
								soapHeaderBinding.Message.Name,
								soapHeaderBinding.Message.Namespace
							}));
						}
						XmlTypeMapping xmlTypeMapping;
						string text;
						if (use == SoapBindingUse.Encoded)
						{
							if (messagePart.Type.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("WebDescriptionPartTypeRequired", new object[]
								{
									messagePart.Name,
									soapHeaderBinding.Message.Name,
									soapHeaderBinding.Message.Namespace
								}));
							}
							if (!messagePart.Element.IsEmpty)
							{
								base.UnsupportedOperationBindingWarning(Res.GetString("WebDescriptionPartElementWarning", new object[]
								{
									messagePart.Name,
									soapHeaderBinding.Message.Name,
									soapHeaderBinding.Message.Namespace
								}));
							}
							xmlTypeMapping = this.soapImporter.ImportDerivedTypeMapping(messagePart.Type, typeof(SoapHeader), true);
							text = "type=" + messagePart.Type.ToString();
						}
						else
						{
							if (messagePart.Element.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("WebDescriptionPartElementRequired", new object[]
								{
									messagePart.Name,
									soapHeaderBinding.Message.Name,
									soapHeaderBinding.Message.Namespace
								}));
							}
							if (!messagePart.Type.IsEmpty)
							{
								base.UnsupportedOperationBindingWarning(Res.GetString("WebDescriptionPartTypeWarning", new object[]
								{
									messagePart.Name,
									soapHeaderBinding.Message.Name,
									soapHeaderBinding.Message.Namespace
								}));
							}
							xmlTypeMapping = this.xmlImporter.ImportDerivedTypeMapping(messagePart.Element, typeof(SoapHeader), true);
							text = "element=" + messagePart.Element.ToString();
						}
						LocalSoapHeader localSoapHeader = (LocalSoapHeader)hashtable[text];
						SoapHeaderDirection soapHeaderDirection;
						if (localSoapHeader == null)
						{
							GlobalSoapHeader globalSoapHeader = (GlobalSoapHeader)this.classHeaders[text];
							if (globalSoapHeader == null)
							{
								globalSoapHeader = new GlobalSoapHeader();
								globalSoapHeader.isEncoded = use == SoapBindingUse.Encoded;
								string text2 = CodeIdentifier.MakeValid(xmlTypeMapping.ElementName);
								if (text2 == xmlTypeMapping.TypeName)
								{
									text2 += "Value";
								}
								text2 = base.MethodNames.AddUnique(text2, xmlTypeMapping);
								globalSoapHeader.fieldName = text2;
								WebCodeGenerator.AddMember(base.CodeTypeDeclaration, xmlTypeMapping.TypeFullName, globalSoapHeader.fieldName, null, null, CodeFlags.IsPublic, base.ServiceImporter.CodeGenerationOptions);
								globalSoapHeader.mapping = xmlTypeMapping;
								this.classHeaders.Add(text, globalSoapHeader);
								if (this.headers[text] == null)
								{
									this.headers.Add(text, globalSoapHeader);
								}
							}
							hashtable.Add(text, new LocalSoapHeader
							{
								fieldName = globalSoapHeader.fieldName,
								direction = soapHeaderDirection
							});
						}
						else if (localSoapHeader.direction != soapHeaderDirection)
						{
							localSoapHeader.direction = SoapHeaderDirection.InOut;
						}
					}
				}
				goto IL_0422;
			}
			foreach (object obj in hashtable.Values)
			{
				LocalSoapHeader localSoapHeader2 = (LocalSoapHeader)obj;
				this.BeginMetadata();
				if (localSoapHeader2.direction == SoapHeaderDirection.Out)
				{
					this.AddMetadataProperty("Direction", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapHeaderDirection).FullName), SoapHeaderDirection.Out.ToString()));
				}
				else if (localSoapHeader2.direction == SoapHeaderDirection.InOut)
				{
					this.AddMetadataProperty("Direction", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapHeaderDirection).FullName), SoapHeaderDirection.InOut.ToString()));
				}
				this.EndMetadata(metadata, typeof(SoapHeaderAttribute), localSoapHeader2.fieldName);
			}
		}

		/// <summary>Generates method code for a binding class.</summary>
		/// <returns>The method code for a binding class.</returns>
		// Token: 0x06000910 RID: 2320 RVA: 0x0003DDC0 File Offset: 0x0003BFC0
		protected override CodeMemberMethod GenerateMethod()
		{
			SoapOperationBinding soapOperationBinding = (SoapOperationBinding)base.OperationBinding.Extensions.Find(typeof(SoapOperationBinding));
			if (soapOperationBinding == null)
			{
				throw base.OperationBindingSyntaxException(Res.GetString("MissingSoapOperationBinding0"));
			}
			SoapBindingStyle soapBindingStyle = soapOperationBinding.Style;
			if (soapBindingStyle == SoapBindingStyle.Default)
			{
				soapBindingStyle = this.SoapBinding.Style;
			}
			if (soapBindingStyle == SoapBindingStyle.Default)
			{
				soapBindingStyle = SoapBindingStyle.Document;
			}
			string[] parameterOrder = base.Operation.ParameterOrder;
			Message inputMessage = base.InputMessage;
			MessageBinding input = base.OperationBinding.Input;
			SoapBodyBinding soapBodyBinding = (SoapBodyBinding)base.OperationBinding.Input.Extensions.Find(typeof(SoapBodyBinding));
			if (soapBodyBinding == null)
			{
				base.UnsupportedOperationBindingWarning(Res.GetString("MissingSoapBodyInputBinding0"));
				return null;
			}
			Message message;
			MessageBinding messageBinding;
			SoapBodyBinding soapBodyBinding2;
			if (base.Operation.Messages.Output != null)
			{
				message = base.OutputMessage;
				messageBinding = base.OperationBinding.Output;
				soapBodyBinding2 = (SoapBodyBinding)base.OperationBinding.Output.Extensions.Find(typeof(SoapBodyBinding));
				if (soapBodyBinding2 == null)
				{
					base.UnsupportedOperationBindingWarning(Res.GetString("MissingSoapBodyOutputBinding0"));
					return null;
				}
			}
			else
			{
				message = null;
				messageBinding = null;
				soapBodyBinding2 = null;
			}
			CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
			this.PrepareHeaders(input);
			if (messageBinding != null)
			{
				this.PrepareHeaders(messageBinding);
			}
			string text = null;
			string text2 = ((!string.IsNullOrEmpty(input.Name) && soapBindingStyle != SoapBindingStyle.Rpc) ? input.Name : base.Operation.Name);
			text2 = XmlConvert.DecodeName(text2);
			if (messageBinding != null)
			{
				text = ((!string.IsNullOrEmpty(messageBinding.Name) && soapBindingStyle != SoapBindingStyle.Rpc) ? messageBinding.Name : (base.Operation.Name + "Response"));
				text = XmlConvert.DecodeName(text);
			}
			this.GenerateExtensionMetadata(codeAttributeDeclarationCollection);
			this.GenerateHeaders(codeAttributeDeclarationCollection, soapBodyBinding.Use, soapBindingStyle == SoapBindingStyle.Rpc, input, messageBinding);
			MessagePart[] messageParts = this.GetMessageParts(inputMessage, soapBodyBinding);
			bool flag;
			if (!this.CheckMessageStyles(base.MethodName, messageParts, soapBodyBinding, soapBindingStyle, out flag))
			{
				return null;
			}
			MessagePart[] array = null;
			if (message != null)
			{
				array = this.GetMessageParts(message, soapBodyBinding2);
				bool flag2;
				if (!this.CheckMessageStyles(base.MethodName, array, soapBodyBinding2, soapBindingStyle, out flag2))
				{
					return null;
				}
				if (flag != flag2)
				{
					flag = false;
				}
			}
			bool flag3 = (soapBindingStyle != SoapBindingStyle.Rpc && flag) || (soapBodyBinding.Use == SoapBindingUse.Literal && soapBindingStyle == SoapBindingStyle.Rpc);
			XmlMembersMapping xmlMembersMapping = this.ImportMessage(text2, messageParts, soapBodyBinding, soapBindingStyle, flag);
			if (xmlMembersMapping == null)
			{
				return null;
			}
			XmlMembersMapping xmlMembersMapping2 = null;
			if (message != null)
			{
				xmlMembersMapping2 = this.ImportMessage(text, array, soapBodyBinding2, soapBindingStyle, flag);
				if (xmlMembersMapping2 == null)
				{
					return null;
				}
			}
			string text3 = CodeIdentifier.MakeValid(XmlConvert.DecodeName(base.Operation.Name));
			if (base.ClassName == text3)
			{
				text3 = "Call" + text3;
			}
			string text4 = base.MethodNames.AddUnique(CodeIdentifier.MakeValid(XmlConvert.DecodeName(text3)), base.Operation);
			bool flag4 = text3 != text4;
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers(false);
			codeIdentifiers.AddReserved(text4);
			SoapParameters soapParameters = new SoapParameters(xmlMembersMapping, xmlMembersMapping2, parameterOrder, base.MethodNames);
			foreach (object obj in soapParameters.Parameters)
			{
				SoapParameter soapParameter = (SoapParameter)obj;
				if ((soapParameter.IsOut || soapParameter.IsByRef) && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ReferenceParameters))
				{
					base.UnsupportedOperationWarning(Res.GetString("CodeGenSupportReferenceParameters", new object[] { base.ServiceImporter.CodeGenerator.GetType().Name }));
					return null;
				}
				soapParameter.name = codeIdentifiers.AddUnique(soapParameter.name, null);
				if (soapParameter.mapping.CheckSpecified)
				{
					soapParameter.specifiedName = codeIdentifiers.AddUnique(soapParameter.name + "Specified", null);
				}
			}
			if (base.Style > ServiceDescriptionImportStyle.Client || flag4)
			{
				this.BeginMetadata();
				if (flag4)
				{
					this.AddMetadataProperty("MessageName", text4);
				}
				this.EndMetadata(codeAttributeDeclarationCollection, typeof(WebMethodAttribute), null);
			}
			this.BeginMetadata();
			if (flag3 && xmlMembersMapping.ElementName.Length > 0 && xmlMembersMapping.ElementName != text4)
			{
				this.AddMetadataProperty("RequestElementName", xmlMembersMapping.ElementName);
			}
			if (xmlMembersMapping.Namespace != null)
			{
				this.AddMetadataProperty("RequestNamespace", xmlMembersMapping.Namespace);
			}
			if (xmlMembersMapping2 == null)
			{
				this.AddMetadataProperty("OneWay", true);
			}
			else
			{
				if (flag3 && xmlMembersMapping2.ElementName.Length > 0 && xmlMembersMapping2.ElementName != text4 + "Response")
				{
					this.AddMetadataProperty("ResponseElementName", xmlMembersMapping2.ElementName);
				}
				if (xmlMembersMapping2.Namespace != null)
				{
					this.AddMetadataProperty("ResponseNamespace", xmlMembersMapping2.Namespace);
				}
			}
			if (soapBindingStyle == SoapBindingStyle.Rpc)
			{
				if (soapBodyBinding.Use != SoapBindingUse.Encoded)
				{
					this.AddMetadataProperty("Use", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapBindingUse).FullName), Enum.Format(typeof(SoapBindingUse), soapBodyBinding.Use, "G")));
				}
				this.EndMetadata(codeAttributeDeclarationCollection, typeof(SoapRpcMethodAttribute), soapOperationBinding.SoapAction);
			}
			else
			{
				this.AddMetadataProperty("Use", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapBindingUse).FullName), Enum.Format(typeof(SoapBindingUse), soapBodyBinding.Use, "G")));
				this.AddMetadataProperty("ParameterStyle", new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(SoapParameterStyle).FullName), Enum.Format(typeof(SoapParameterStyle), flag ? SoapParameterStyle.Wrapped : SoapParameterStyle.Bare, "G")));
				this.EndMetadata(codeAttributeDeclarationCollection, typeof(SoapDocumentMethodAttribute), soapOperationBinding.SoapAction);
			}
			base.IsEncodedBinding = base.IsEncodedBinding || soapBodyBinding.Use == SoapBindingUse.Encoded;
			CodeAttributeDeclarationCollection[] array2 = new CodeAttributeDeclarationCollection[soapParameters.Parameters.Count + soapParameters.CheckSpecifiedCount];
			int num = 0;
			CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(typeof(XmlIgnoreAttribute).FullName);
			foreach (object obj2 in soapParameters.Parameters)
			{
				SoapParameter soapParameter2 = (SoapParameter)obj2;
				array2[num] = new CodeAttributeDeclarationCollection();
				if (soapBodyBinding.Use == SoapBindingUse.Encoded)
				{
					this.soapExporter.AddMappingMetadata(array2[num], soapParameter2.mapping, soapParameter2.name != soapParameter2.mapping.MemberName);
				}
				else
				{
					string text5 = ((soapBindingStyle == SoapBindingStyle.Rpc) ? soapParameter2.mapping.Namespace : (soapParameter2.IsOut ? xmlMembersMapping2.Namespace : xmlMembersMapping.Namespace));
					bool flag5 = soapParameter2.name != soapParameter2.mapping.MemberName;
					this.xmlExporter.AddMappingMetadata(array2[num], soapParameter2.mapping, text5, flag5);
					if (soapParameter2.mapping.CheckSpecified)
					{
						num++;
						array2[num] = new CodeAttributeDeclarationCollection();
						this.xmlExporter.AddMappingMetadata(array2[num], soapParameter2.mapping, text5, soapParameter2.specifiedName != soapParameter2.mapping.MemberName + "Specified");
						array2[num].Add(codeAttributeDeclaration);
					}
				}
				if (array2[num].Count > 0 && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ParameterAttributes))
				{
					base.UnsupportedOperationWarning(Res.GetString("CodeGenSupportParameterAttributes", new object[] { base.ServiceImporter.CodeGenerator.GetType().Name }));
					return null;
				}
				num++;
			}
			CodeFlags[] codeFlags = SoapParameter.GetCodeFlags(soapParameters.Parameters, soapParameters.CheckSpecifiedCount);
			string[] typeFullNames = SoapParameter.GetTypeFullNames(soapParameters.Parameters, soapParameters.CheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
			string text6 = ((soapParameters.Return == null) ? typeof(void).FullName : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator));
			CodeMemberMethod codeMemberMethod = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, text3, codeFlags, typeFullNames, SoapParameter.GetNames(soapParameters.Parameters, soapParameters.CheckSpecifiedCount), array2, text6, codeAttributeDeclarationCollection, CodeFlags.IsPublic | ((base.Style == ServiceDescriptionImportStyle.Client) ? ((CodeFlags)0) : CodeFlags.IsAbstract));
			codeMemberMethod.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
			if (soapParameters.Return != null)
			{
				if (soapBodyBinding.Use == SoapBindingUse.Encoded)
				{
					this.soapExporter.AddMappingMetadata(codeMemberMethod.ReturnTypeCustomAttributes, soapParameters.Return, soapParameters.Return.ElementName != text4 + "Result");
				}
				else
				{
					this.xmlExporter.AddMappingMetadata(codeMemberMethod.ReturnTypeCustomAttributes, soapParameters.Return, xmlMembersMapping2.Namespace, soapParameters.Return.ElementName != text4 + "Result");
				}
				if (codeMemberMethod.ReturnTypeCustomAttributes.Count != 0 && !base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.ReturnTypeAttributes))
				{
					base.UnsupportedOperationWarning(Res.GetString("CodeGenSupportReturnTypeAttributes", new object[] { base.ServiceImporter.CodeGenerator.GetType().Name }));
					return null;
				}
			}
			string text7 = codeIdentifiers.MakeUnique("results");
			if (base.Style == ServiceDescriptionImportStyle.Client)
			{
				bool flag6 = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateOldAsync) > CodeGenerationOptions.None;
				bool flag7 = (base.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateNewAsync) != CodeGenerationOptions.None && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareEvents) && base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareDelegates);
				CodeExpression[] array3 = new CodeExpression[2];
				this.CreateInvokeParams(array3, text4, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Invoke", array3);
				this.WriteReturnMappings(codeMemberMethod, codeMethodInvokeExpression, soapParameters, text7);
				if (flag6)
				{
					int num2 = soapParameters.InParameters.Count + soapParameters.InCheckSpecifiedCount;
					string[] array4 = new string[num2 + 2];
					SoapParameter.GetTypeFullNames(soapParameters.InParameters, array4, 0, soapParameters.InCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
					array4[num2] = typeof(AsyncCallback).FullName;
					array4[num2 + 1] = typeof(object).FullName;
					string[] array5 = new string[num2 + 2];
					SoapParameter.GetNames(soapParameters.InParameters, array5, 0, soapParameters.InCheckSpecifiedCount);
					array5[num2] = "callback";
					array5[num2 + 1] = "asyncState";
					CodeFlags[] array6 = new CodeFlags[num2 + 2];
					CodeMemberMethod codeMemberMethod2 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "Begin" + text4, array6, array4, array5, typeof(IAsyncResult).FullName, null, CodeFlags.IsPublic);
					codeMemberMethod2.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
					array3 = new CodeExpression[4];
					this.CreateInvokeParams(array3, text4, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
					array3[2] = new CodeArgumentReferenceExpression("callback");
					array3[3] = new CodeArgumentReferenceExpression("asyncState");
					codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "BeginInvoke", array3);
					codeMemberMethod2.Statements.Add(new CodeMethodReturnStatement(codeMethodInvokeExpression));
					int num3 = soapParameters.OutParameters.Count + soapParameters.OutCheckSpecifiedCount;
					string[] array7 = new string[num3 + 1];
					SoapParameter.GetTypeFullNames(soapParameters.OutParameters, array7, 1, soapParameters.OutCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
					array7[0] = typeof(IAsyncResult).FullName;
					string[] array8 = new string[num3 + 1];
					SoapParameter.GetNames(soapParameters.OutParameters, array8, 1, soapParameters.OutCheckSpecifiedCount);
					array8[0] = "asyncResult";
					CodeFlags[] array9 = new CodeFlags[num3 + 1];
					for (int i = 0; i < num3; i++)
					{
						array9[i + 1] = CodeFlags.IsOut;
					}
					CodeMemberMethod codeMemberMethod3 = WebCodeGenerator.AddMethod(base.CodeTypeDeclaration, "End" + text4, array9, array7, array8, (soapParameters.Return == null) ? typeof(void).FullName : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator), null, CodeFlags.IsPublic);
					codeMemberMethod3.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
					CodeExpression codeExpression = new CodeArgumentReferenceExpression("asyncResult");
					codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "EndInvoke", new CodeExpression[] { codeExpression });
					this.WriteReturnMappings(codeMemberMethod3, codeMethodInvokeExpression, soapParameters, text7);
				}
				if (flag7)
				{
					string text8 = ProtocolImporter.MethodSignature(text4, text6, codeFlags, typeFullNames);
					DelegateInfo delegateInfo = (DelegateInfo)base.ExportContext[text8];
					if (delegateInfo == null)
					{
						string text9 = base.ClassNames.AddUnique(text4 + "CompletedEventHandler", text4);
						string text10 = base.ClassNames.AddUnique(text4 + "CompletedEventArgs", text4);
						delegateInfo = new DelegateInfo(text9, text10);
					}
					string text11 = base.MethodNames.AddUnique(text4 + "Completed", text4);
					string text12 = base.MethodNames.AddUnique(text4 + "Async", text4);
					string text13 = base.MethodNames.AddUnique(text4 + "OperationCompleted", text4);
					string text14 = base.MethodNames.AddUnique("On" + text4 + "OperationCompleted", text4);
					WebCodeGenerator.AddEvent(base.CodeTypeDeclaration.Members, delegateInfo.handlerType, text11);
					WebCodeGenerator.AddCallbackDeclaration(base.CodeTypeDeclaration.Members, text13);
					string[] names = SoapParameter.GetNames(soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
					string text15 = ProtocolImporter.UniqueName("userState", names);
					CodeMemberMethod codeMemberMethod4 = WebCodeGenerator.AddAsyncMethod(base.CodeTypeDeclaration, text12, SoapParameter.GetTypeFullNames(soapParameters.InParameters, soapParameters.InCheckSpecifiedCount, base.ServiceImporter.CodeGenerator), names, text13, text14, text15);
					array3 = new CodeExpression[4];
					this.CreateInvokeParams(array3, text4, soapParameters.InParameters, soapParameters.InCheckSpecifiedCount);
					array3[2] = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), text13);
					array3[3] = new CodeArgumentReferenceExpression(text15);
					codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "InvokeAsync", array3);
					codeMemberMethod4.Statements.Add(codeMethodInvokeExpression);
					bool flag8 = soapParameters.Return != null || soapParameters.OutParameters.Count > 0;
					WebCodeGenerator.AddCallbackImplementation(base.CodeTypeDeclaration, text14, text11, delegateInfo.handlerArgs, flag8);
					if (base.ExportContext[text8] == null)
					{
						WebCodeGenerator.AddDelegate(base.ExtraCodeClasses, delegateInfo.handlerType, flag8 ? delegateInfo.handlerArgs : typeof(AsyncCompletedEventArgs).FullName);
						if (flag8)
						{
							int num4 = soapParameters.OutParameters.Count + soapParameters.OutCheckSpecifiedCount;
							string[] array10 = new string[num4 + 1];
							SoapParameter.GetTypeFullNames(soapParameters.OutParameters, array10, 1, soapParameters.OutCheckSpecifiedCount, base.ServiceImporter.CodeGenerator);
							array10[0] = ((soapParameters.Return == null) ? null : WebCodeGenerator.FullTypeName(soapParameters.Return, base.ServiceImporter.CodeGenerator));
							string[] array11 = new string[num4 + 1];
							SoapParameter.GetNames(soapParameters.OutParameters, array11, 1, soapParameters.OutCheckSpecifiedCount);
							array11[0] = ((soapParameters.Return == null) ? null : "Result");
							base.ExtraCodeClasses.Add(WebCodeGenerator.CreateArgsClass(delegateInfo.handlerArgs, array10, array11, base.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.PartialTypes)));
						}
						base.ExportContext[text8] = delegateInfo;
					}
				}
			}
			return codeMemberMethod;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003EDF0 File Offset: 0x0003CFF0
		private void WriteReturnMappings(CodeMemberMethod codeMethod, CodeExpression invoke, SoapParameters parameters, string resultsName)
		{
			if (parameters.Return == null && parameters.OutParameters.Count == 0)
			{
				codeMethod.Statements.Add(new CodeExpressionStatement(invoke));
				return;
			}
			codeMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(object[]), resultsName, invoke));
			int num = ((parameters.Return == null) ? 0 : 1);
			for (int i = 0; i < parameters.OutParameters.Count; i++)
			{
				SoapParameter soapParameter = (SoapParameter)parameters.OutParameters[i];
				CodeExpression codeExpression = new CodeArgumentReferenceExpression(soapParameter.name);
				CodeExpression codeExpression2 = new CodeArrayIndexerExpression();
				((CodeArrayIndexerExpression)codeExpression2).TargetObject = new CodeVariableReferenceExpression(resultsName);
				((CodeArrayIndexerExpression)codeExpression2).Indices.Add(new CodePrimitiveExpression(num++));
				codeExpression2 = new CodeCastExpression(WebCodeGenerator.FullTypeName(soapParameter.mapping, base.ServiceImporter.CodeGenerator), codeExpression2);
				codeMethod.Statements.Add(new CodeAssignStatement(codeExpression, codeExpression2));
				if (soapParameter.mapping.CheckSpecified)
				{
					codeExpression = new CodeArgumentReferenceExpression(soapParameter.name + "Specified");
					codeExpression2 = new CodeArrayIndexerExpression();
					((CodeArrayIndexerExpression)codeExpression2).TargetObject = new CodeVariableReferenceExpression(resultsName);
					((CodeArrayIndexerExpression)codeExpression2).Indices.Add(new CodePrimitiveExpression(num++));
					codeExpression2 = new CodeCastExpression(typeof(bool).FullName, codeExpression2);
					codeMethod.Statements.Add(new CodeAssignStatement(codeExpression, codeExpression2));
				}
			}
			if (parameters.Return != null)
			{
				CodeExpression codeExpression3 = new CodeArrayIndexerExpression();
				((CodeArrayIndexerExpression)codeExpression3).TargetObject = new CodeVariableReferenceExpression(resultsName);
				((CodeArrayIndexerExpression)codeExpression3).Indices.Add(new CodePrimitiveExpression(0));
				codeExpression3 = new CodeCastExpression(WebCodeGenerator.FullTypeName(parameters.Return, base.ServiceImporter.CodeGenerator), codeExpression3);
				codeMethod.Statements.Add(new CodeMethodReturnStatement(codeExpression3));
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0003EFF4 File Offset: 0x0003D1F4
		private void CreateInvokeParams(CodeExpression[] invokeParams, string methodName, IList parameters, int checkSpecifiedCount)
		{
			invokeParams[0] = new CodePrimitiveExpression(methodName);
			CodeExpression[] array = new CodeExpression[parameters.Count + checkSpecifiedCount];
			int num = 0;
			for (int i = 0; i < parameters.Count; i++)
			{
				SoapParameter soapParameter = (SoapParameter)parameters[i];
				array[num++] = new CodeArgumentReferenceExpression(soapParameter.name);
				if (soapParameter.mapping.CheckSpecified)
				{
					array[num++] = new CodeArgumentReferenceExpression(soapParameter.specifiedName);
				}
			}
			invokeParams[1] = new CodeArrayCreateExpression(typeof(object).FullName, array);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003F084 File Offset: 0x0003D284
		private bool CheckMessageStyles(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, out bool hasWrapper)
		{
			hasWrapper = false;
			if (soapBodyBinding.Use == SoapBindingUse.Default)
			{
				soapBodyBinding.Use = SoapBindingUse.Literal;
			}
			if (soapBodyBinding.Use == SoapBindingUse.Literal)
			{
				if (soapBindingStyle == SoapBindingStyle.Rpc)
				{
					for (int i = 0; i < parts.Length; i++)
					{
						if (!parts[i].Element.IsEmpty)
						{
							base.UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInRpcUseLiteralMessageMustSpecify0"));
							return false;
						}
					}
					return true;
				}
				if (parts.Length == 1 && !parts[0].Type.IsEmpty)
				{
					if (!parts[0].Element.IsEmpty)
					{
						base.UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIs0"));
						return false;
					}
					if (this.xmlImporter.ImportAnyType(parts[0].Type, parts[0].Name) == null)
					{
						base.UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIsAny", new object[]
						{
							parts[0].Type.Name,
							parts[0].Type.Namespace
						}));
						return false;
					}
					return true;
				}
				else
				{
					foreach (MessagePart messagePart in parts)
					{
						if (!messagePart.Type.IsEmpty)
						{
							base.UnsupportedOperationBindingWarning(Res.GetString("SpecifyingATypeForUseLiteralMessagesIs0"));
							return false;
						}
						if (messagePart.Element.IsEmpty)
						{
							base.UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInAUseLiteralMessageMustSpecify0"));
							return false;
						}
					}
				}
			}
			else if (soapBodyBinding.Use == SoapBindingUse.Encoded)
			{
				if (!this.IsSoapEncodingPresent(soapBodyBinding.Encoding))
				{
					base.UnsupportedOperationBindingWarning(Res.GetString("TheEncodingIsNotSupported1", new object[] { soapBodyBinding.Encoding }));
					return false;
				}
				foreach (MessagePart messagePart2 in parts)
				{
					if (!messagePart2.Element.IsEmpty)
					{
						base.UnsupportedOperationBindingWarning(Res.GetString("SpecifyingAnElementForUseEncodedMessageParts0"));
						return false;
					}
					if (messagePart2.Type.IsEmpty)
					{
						base.UnsupportedOperationBindingWarning(Res.GetString("EachMessagePartInAnUseEncodedMessageMustSpecify0"));
						return false;
					}
				}
			}
			if (soapBindingStyle == SoapBindingStyle.Rpc)
			{
				return true;
			}
			if (soapBindingStyle == SoapBindingStyle.Document)
			{
				hasWrapper = parts.Length == 1 && string.Compare(parts[0].Name, "parameters", StringComparison.Ordinal) == 0;
				return true;
			}
			return false;
		}

		/// <summary>Checks for the presence of "http://schemas.xmlsoap.org/soap/encoding/" in a string that represents a list of Uniform Resource Indicators (URIs).</summary>
		/// <returns>true it the string contains http://schemas.xmlsoap.org/soap/encoding/; otherwise false.</returns>
		/// <param name="uriList">A space-delimited list of URIs.</param>
		// Token: 0x06000914 RID: 2324 RVA: 0x0003F290 File Offset: 0x0003D490
		protected virtual bool IsSoapEncodingPresent(string uriList)
		{
			int num = 0;
			for (;;)
			{
				num = uriList.IndexOf("http://schemas.xmlsoap.org/soap/encoding/", num, StringComparison.Ordinal);
				if (num < 0)
				{
					break;
				}
				int num2 = num + "http://schemas.xmlsoap.org/soap/encoding/".Length;
				if ((num == 0 || uriList[num - 1] == ' ') && (num2 == uriList.Length || uriList[num2] == ' '))
				{
					return true;
				}
				num = num2;
				if (num >= uriList.Length)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		private MessagePart[] GetMessageParts(Message message, SoapBodyBinding soapBodyBinding)
		{
			MessagePart[] array;
			if (soapBodyBinding.Parts == null)
			{
				array = new MessagePart[message.Parts.Count];
				message.Parts.CopyTo(array, 0);
			}
			else
			{
				array = message.FindPartsByName(soapBodyBinding.Parts);
			}
			return array;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0003F337 File Offset: 0x0003D537
		private XmlMembersMapping ImportMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, bool wrapped)
		{
			if (soapBodyBinding.Use == SoapBindingUse.Encoded)
			{
				return this.ImportEncodedMessage(messageName, parts, soapBodyBinding, wrapped);
			}
			return this.ImportLiteralMessage(messageName, parts, soapBodyBinding, soapBindingStyle, wrapped);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0003F35C File Offset: 0x0003D55C
		private XmlMembersMapping ImportEncodedMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, bool wrapped)
		{
			XmlMembersMapping xmlMembersMapping;
			if (wrapped)
			{
				SoapSchemaMember soapSchemaMember = new SoapSchemaMember();
				soapSchemaMember.MemberName = parts[0].Name;
				soapSchemaMember.MemberType = parts[0].Type;
				xmlMembersMapping = this.soapImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, soapSchemaMember);
			}
			else
			{
				SoapSchemaMember[] array = new SoapSchemaMember[parts.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MessagePart messagePart = parts[i];
					array[i] = new SoapSchemaMember
					{
						MemberName = messagePart.Name,
						MemberType = messagePart.Type
					};
				}
				xmlMembersMapping = this.soapImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, array);
			}
			this.soapMembers.Add(xmlMembersMapping);
			return xmlMembersMapping;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0003F40C File Offset: 0x0003D60C
		private XmlMembersMapping ImportLiteralMessage(string messageName, MessagePart[] parts, SoapBodyBinding soapBodyBinding, SoapBindingStyle soapBindingStyle, bool wrapped)
		{
			XmlMembersMapping xmlMembersMapping;
			if (soapBindingStyle == SoapBindingStyle.Rpc)
			{
				SoapSchemaMember[] array = new SoapSchemaMember[parts.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MessagePart messagePart = parts[i];
					array[i] = new SoapSchemaMember
					{
						MemberName = messagePart.Name,
						MemberType = messagePart.Type
					};
				}
				xmlMembersMapping = this.xmlImporter.ImportMembersMapping(messageName, soapBodyBinding.Namespace, array);
			}
			else if (wrapped)
			{
				xmlMembersMapping = this.xmlImporter.ImportMembersMapping(parts[0].Element);
			}
			else
			{
				if (parts.Length == 1 && !parts[0].Type.IsEmpty)
				{
					xmlMembersMapping = this.xmlImporter.ImportAnyType(parts[0].Type, parts[0].Name);
					this.xmlMembers.Add(xmlMembersMapping);
					return xmlMembersMapping;
				}
				XmlQualifiedName[] array2 = new XmlQualifiedName[parts.Length];
				for (int j = 0; j < parts.Length; j++)
				{
					array2[j] = parts[j].Element;
				}
				xmlMembersMapping = this.xmlImporter.ImportMembersMapping(array2);
			}
			this.xmlMembers.Add(xmlMembersMapping);
			return xmlMembersMapping;
		}

		// Token: 0x0400055D RID: 1373
		private XmlSchemaImporter xmlImporter;

		// Token: 0x0400055E RID: 1374
		private XmlCodeExporter xmlExporter;

		// Token: 0x0400055F RID: 1375
		private SoapSchemaImporter soapImporter;

		// Token: 0x04000560 RID: 1376
		private SoapCodeExporter soapExporter;

		// Token: 0x04000561 RID: 1377
		private ArrayList xmlMembers = new ArrayList();

		// Token: 0x04000562 RID: 1378
		private ArrayList soapMembers = new ArrayList();

		// Token: 0x04000563 RID: 1379
		private Hashtable headers = new Hashtable();

		// Token: 0x04000564 RID: 1380
		private Hashtable classHeaders = new Hashtable();

		// Token: 0x04000565 RID: 1381
		private ArrayList propertyNames = new ArrayList();

		// Token: 0x04000566 RID: 1382
		private ArrayList propertyValues = new ArrayList();

		// Token: 0x04000567 RID: 1383
		private SoapExtensionImporter[] extensions;

		// Token: 0x04000568 RID: 1384
		private SoapTransportImporter transport;

		// Token: 0x04000569 RID: 1385
		private SoapBinding soapBinding;

		// Token: 0x0400056A RID: 1386
		private ArrayList codeClasses = new ArrayList();

		// Token: 0x0400056B RID: 1387
		private static TypedDataSetSchemaImporterExtension typedDataSetSchemaImporterExtension;
	}
}
