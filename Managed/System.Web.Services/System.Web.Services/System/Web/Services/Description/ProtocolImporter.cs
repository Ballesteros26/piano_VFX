using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides common functionality across communication protocols for generating classes for Web services. </summary>
	// Token: 0x020000E0 RID: 224
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class ProtocolImporter
	{
		// Token: 0x060005A9 RID: 1449 RVA: 0x00019715 File Offset: 0x00017915
		internal void Initialize(ServiceDescriptionImporter importer)
		{
			this.importer = importer;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> objects that belong to the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance that are searched for bindings from which to generate binding classes.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> objects that belong to the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance that are searched for bindings from which to generate binding classes.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001971E File Offset: 0x0001791E
		public ServiceDescriptionCollection ServiceDescriptions
		{
			get
			{
				return this.importer.ServiceDescriptions;
			}
		}

		/// <summary>Gets all the XML schemas, both abstract and concrete, used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
		/// <returns>The XML schemas, both abstract and concrete, used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001972B File Offset: 0x0001792B
		public XmlSchemas Schemas
		{
			get
			{
				return this.importer.AllSchemas;
			}
		}

		/// <summary>Gets the abstract XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
		/// <returns>The abstract XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00019738 File Offset: 0x00017938
		public XmlSchemas AbstractSchemas
		{
			get
			{
				return this.importer.AbstractSchemas;
			}
		}

		/// <summary>Gets the concrete XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</summary>
		/// <returns>The concrete XML schemas used by the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00019745 File Offset: 0x00017945
		public XmlSchemas ConcreteSchemas
		{
			get
			{
				return this.importer.ConcreteSchemas;
			}
		}

		/// <summary>Gets a representation of the .NET Framework namespace of the binding classes that are being generated.</summary>
		/// <returns>A representation of the .NET Framework namespace of the binding classes that are being generated.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00019752 File Offset: 0x00017952
		public CodeNamespace CodeNamespace
		{
			get
			{
				return this.codeNamespace;
			}
		}

		/// <summary>Gets a representation of the binding class that is currently being generated.</summary>
		/// <returns>A representation of the binding class that is currently being generated.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001975A File Offset: 0x0001795A
		public CodeTypeDeclaration CodeTypeDeclaration
		{
			get
			{
				return this.codeClass;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00019762 File Offset: 0x00017962
		internal CodeTypeDeclarationCollection ExtraCodeClasses
		{
			get
			{
				if (this.classes == null)
				{
					this.classes = new CodeTypeDeclarationCollection();
				}
				return this.classes;
			}
		}

		/// <summary>Gets an enumeration value that indicates whether a client proxy class or an abstract server class is being generated. The values are Client and Server. The value is that of the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance's <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</summary>
		/// <returns>An enumeration value that indicates whether a client proxy class or an abstract server class is being generated. The values are Client and Server. The value is that of the associated <see cref="T:System.Web.Services.Description.ServiceDescriptionImporter" /> instance's <see cref="P:System.Web.Services.Description.ServiceDescriptionImporter.Style" /> property.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001977D File Offset: 0x0001797D
		public ServiceDescriptionImportStyle Style
		{
			get
			{
				return this.importer.Style;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration value that indicates the types of warnings, if any, issued by the protocol importer while generating binding classes.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration value that indicates the types of warnings, if any, issued by the protocol importer while generating binding classes.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001978A File Offset: 0x0001798A
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00019792 File Offset: 0x00017992
		public ServiceDescriptionImportWarnings Warnings
		{
			get
			{
				return this.warnings;
			}
			set
			{
				this.warnings = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that generates a unique name for the binding class that is currently being generated.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that generates a unique name for the binding class that is currently being generated.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001979B File Offset: 0x0001799B
		public CodeIdentifiers ClassNames
		{
			get
			{
				return this.importContext.TypeIdentifiers;
			}
		}

		/// <summary>Gets the name of the binding class method which that the protocol importer is currently generating.</summary>
		/// <returns>The name of the binding class method which that the protocol importer is currently generating.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000197A8 File Offset: 0x000179A8
		public string MethodName
		{
			get
			{
				return CodeIdentifier.MakeValid(XmlConvert.DecodeName(this.Operation.Name));
			}
		}

		/// <summary>Gets the name of the binding class that is currently being generated.</summary>
		/// <returns>The name of the binding class that is currently being generated.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000197BF File Offset: 0x000179BF
		public string ClassName
		{
			get
			{
				return this.className;
			}
		}

		/// <summary>Gets a Web Services Description Language (WSDL) port that contains a reference to the binding that the protocol importer is currently processing to generate a binding class. If more than one port refers to the current binding, the current port is the one in which the binding has most recently been found.</summary>
		/// <returns>The Web Services Description Language (WSDL) port that contains a reference to the binding that the protocol importer is currently processing to generate a binding class. If more than one port refers to the current binding, the current port is the one in which the binding has most recently been found.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x000197C7 File Offset: 0x000179C7
		public Port Port
		{
			get
			{
				return this.port;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) <see cref="P:System.Web.Services.Description.ProtocolImporter.PortType" /> that is implemented by the binding that the protocol importer is currently processing to generate a binding class.</summary>
		/// <returns>The Web Services Description Language (WSDL) <see cref="P:System.Web.Services.Description.ProtocolImporter.PortType" /> that is implemented by the binding that the protocol importer is currently processing to generate a binding class.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000197CF File Offset: 0x000179CF
		public PortType PortType
		{
			get
			{
				return this.portType;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) binding that the protocol importer is currently processing to generate a class.</summary>
		/// <returns>The Web Services Description Language (WSDL) binding that the protocol importer is currently processing to generate a class.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x000197D7 File Offset: 0x000179D7
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) service that contains a reference to the binding that the protocol importer is currently processing to generate a binding class.</summary>
		/// <returns>The Web Services Description Language (WSDL) service that contains a reference to the binding that the protocol importer is currently processing to generate a binding class.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x000197DF File Offset: 0x000179DF
		public Service Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000197E7 File Offset: 0x000179E7
		internal ServiceDescriptionImporter ServiceImporter
		{
			get
			{
				return this.importer;
			}
		}

		/// <summary>Gets the abstract Web Services Description Language (WSDL) operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
		/// <returns>The abstract Web Services Description Language (WSDL) operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x000197EF File Offset: 0x000179EF
		public Operation Operation
		{
			get
			{
				return this.operation;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) operation binding that the protocol importer is currently processing to generate a method in a binding class.</summary>
		/// <returns>The Web Services Description Language (WSDL) operation binding that the protocol importer is currently processing to generate a method in a binding class.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000197F7 File Offset: 0x000179F7
		public OperationBinding OperationBinding
		{
			get
			{
				return this.operationBinding;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) input message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
		/// <returns>The Web Services Description Language (WSDL) input message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000197FF File Offset: 0x000179FF
		public Message InputMessage
		{
			get
			{
				return this.inputMessage;
			}
		}

		/// <summary>Gets the Web Services Description Language (WSDL) output message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</summary>
		/// <returns>The Web Services Description Language (WSDL) output message for the abstract operation that the protocol importer is currently processing to generate a method in a binding class.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00019807 File Offset: 0x00017A07
		public Message OutputMessage
		{
			get
			{
				return this.outputMessage;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001980F File Offset: 0x00017A0F
		internal ImportContext ImportContext
		{
			get
			{
				return this.importContext;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00019817 File Offset: 0x00017A17
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0001981F File Offset: 0x00017A1F
		internal bool IsEncodedBinding
		{
			get
			{
				return this.encodedBinding;
			}
			set
			{
				this.encodedBinding = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00019828 File Offset: 0x00017A28
		internal Hashtable ExportContext
		{
			get
			{
				if (this.exportContext == null)
				{
					this.exportContext = new Hashtable();
				}
				return this.exportContext;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00019843 File Offset: 0x00017A43
		internal CodeIdentifiers MethodNames
		{
			get
			{
				if (this.methodNames == null)
				{
					this.methodNames = new CodeIdentifiers();
				}
				return this.methodNames;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019860 File Offset: 0x00017A60
		internal bool GenerateCode(CodeNamespace codeNamespace, ImportContext importContext, Hashtable exportContext)
		{
			this.bindingCount = 0;
			this.anyPorts = false;
			this.codeNamespace = codeNamespace;
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			foreach (object obj in this.ServiceDescriptions)
			{
				foreach (object obj2 in ((ServiceDescription)obj).Services)
				{
					Service service = (Service)obj2;
					foreach (object obj3 in service.Ports)
					{
						Port port = (Port)obj3;
						Binding binding = this.ServiceDescriptions.GetBinding(port.Binding);
						if (!hashtable.Contains(binding))
						{
							PortType portType = this.ServiceDescriptions.GetPortType(binding.Type);
							this.MoveToBinding(service, port, binding, portType);
							if (this.IsBindingSupported())
							{
								this.bindingCount++;
								this.anyPorts = true;
								hashtable.Add(binding, binding);
							}
							else if (binding != null)
							{
								hashtable2[binding] = binding;
							}
						}
					}
				}
			}
			if (this.bindingCount == 0)
			{
				foreach (object obj4 in this.ServiceDescriptions)
				{
					foreach (object obj5 in ((ServiceDescription)obj4).Bindings)
					{
						Binding binding2 = (Binding)obj5;
						if (!hashtable2.Contains(binding2))
						{
							PortType portType2 = this.ServiceDescriptions.GetPortType(binding2.Type);
							this.MoveToBinding(binding2, portType2);
							if (this.IsBindingSupported())
							{
								this.bindingCount++;
							}
						}
					}
				}
			}
			if (this.bindingCount == 0)
			{
				return codeNamespace.Comments.Count > 0;
			}
			this.importContext = importContext;
			this.exportContext = exportContext;
			this.BeginNamespace();
			hashtable.Clear();
			foreach (object obj6 in this.ServiceDescriptions)
			{
				ServiceDescription serviceDescription = (ServiceDescription)obj6;
				if (this.anyPorts)
				{
					using (IEnumerator enumerator2 = serviceDescription.Services.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj7 = enumerator2.Current;
							Service service2 = (Service)obj7;
							foreach (object obj8 in service2.Ports)
							{
								Port port2 = (Port)obj8;
								Binding binding3 = this.ServiceDescriptions.GetBinding(port2.Binding);
								PortType portType3 = this.ServiceDescriptions.GetPortType(binding3.Type);
								this.MoveToBinding(service2, port2, binding3, portType3);
								if (this.IsBindingSupported() && !hashtable.Contains(binding3))
								{
									this.GenerateClassForBinding();
									hashtable.Add(binding3, binding3);
								}
							}
						}
						continue;
					}
				}
				foreach (object obj9 in serviceDescription.Bindings)
				{
					Binding binding4 = (Binding)obj9;
					PortType portType4 = this.ServiceDescriptions.GetPortType(binding4.Type);
					this.MoveToBinding(binding4, portType4);
					if (this.IsBindingSupported())
					{
						this.GenerateClassForBinding();
					}
				}
			}
			this.EndNamespace();
			return true;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019D1C File Offset: 0x00017F1C
		private void MoveToBinding(Binding binding, PortType portType)
		{
			this.MoveToBinding(null, null, binding, portType);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019D28 File Offset: 0x00017F28
		private void MoveToBinding(Service service, Port port, Binding binding, PortType portType)
		{
			this.service = service;
			this.port = port;
			this.portType = portType;
			this.binding = binding;
			this.encodedBinding = false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00019D50 File Offset: 0x00017F50
		private void MoveToOperation(Operation operation)
		{
			this.operation = operation;
			this.operationBinding = null;
			foreach (object obj in this.binding.Operations)
			{
				OperationBinding operationBinding = (OperationBinding)obj;
				if (operation.IsBoundBy(operationBinding))
				{
					if (this.operationBinding != null)
					{
						throw this.OperationSyntaxException(Res.GetString("DuplicateInputOutputNames0"));
					}
					this.operationBinding = operationBinding;
				}
			}
			if (this.operationBinding == null)
			{
				throw this.OperationSyntaxException(Res.GetString("MissingBinding0"));
			}
			if (operation.Messages.Input != null && this.operationBinding.Input == null)
			{
				throw this.OperationSyntaxException(Res.GetString("MissingInputBinding0"));
			}
			if (operation.Messages.Output != null && this.operationBinding.Output == null)
			{
				throw this.OperationSyntaxException(Res.GetString("MissingOutputBinding0"));
			}
			this.inputMessage = ((operation.Messages.Input == null) ? null : this.ServiceDescriptions.GetMessage(operation.Messages.Input.Message));
			this.outputMessage = ((operation.Messages.Output == null) ? null : this.ServiceDescriptions.GetMessage(operation.Messages.Output.Message));
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00019EB0 File Offset: 0x000180B0
		private void GenerateClassForBinding()
		{
			try
			{
				if (this.bindingCount == 1 && this.service != null && this.Style != ServiceDescriptionImportStyle.ServerInterface)
				{
					this.className = XmlConvert.DecodeName(this.service.Name);
				}
				else
				{
					this.className = this.binding.Name;
					if (this.Style == ServiceDescriptionImportStyle.ServerInterface)
					{
						this.className = "I" + CodeIdentifier.MakePascal(this.className);
					}
				}
				this.className = XmlConvert.DecodeName(this.className);
				this.className = this.ClassNames.AddUnique(CodeIdentifier.MakeValid(this.className), null);
				this.codeClass = this.BeginClass();
				int num = 0;
				int i = 0;
				while (i < this.portType.Operations.Count)
				{
					this.MoveToOperation(this.portType.Operations[i]);
					if (this.IsOperationFlowSupported(this.operation.Messages.Flow))
					{
						goto IL_0158;
					}
					switch (this.operation.Messages.Flow)
					{
					case OperationFlow.OneWay:
						this.UnsupportedOperationWarning(Res.GetString("OneWayIsNotSupported0"));
						break;
					case OperationFlow.Notification:
						this.UnsupportedOperationWarning(Res.GetString("NotificationIsNotSupported0"));
						break;
					case OperationFlow.RequestResponse:
						this.UnsupportedOperationWarning(Res.GetString("RequestResponseIsNotSupported0"));
						break;
					case OperationFlow.SolicitResponse:
						this.UnsupportedOperationWarning(Res.GetString("SolicitResponseIsNotSupported0"));
						break;
					default:
						goto IL_0158;
					}
					IL_0226:
					i++;
					continue;
					IL_0158:
					CodeMemberMethod codeMemberMethod;
					try
					{
						codeMemberMethod = this.GenerateMethod();
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
						throw new InvalidOperationException(Res.GetString("UnableToImportOperation1", new object[] { this.operation.Name }), ex);
					}
					if (codeMemberMethod != null)
					{
						this.AddExtensionWarningComments(this.codeClass.Comments, this.operationBinding.Extensions);
						if (this.operationBinding.Input != null)
						{
							this.AddExtensionWarningComments(this.codeClass.Comments, this.operationBinding.Input.Extensions);
						}
						if (this.operationBinding.Output != null)
						{
							this.AddExtensionWarningComments(this.codeClass.Comments, this.operationBinding.Output.Extensions);
						}
						num++;
						goto IL_0226;
					}
					goto IL_0226;
				}
				if ((this.ServiceImporter.CodeGenerationOptions & CodeGenerationOptions.GenerateNewAsync) != CodeGenerationOptions.None && this.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareEvents) && this.ServiceImporter.CodeGenerator.Supports(GeneratorSupport.DeclareDelegates) && num > 0 && this.Style == ServiceDescriptionImportStyle.Client)
				{
					CodeAttributeDeclarationCollection codeAttributeDeclarationCollection = new CodeAttributeDeclarationCollection();
					string text = "CancelAsync";
					string text2 = this.MethodNames.AddUnique(text, text);
					CodeMemberMethod codeMemberMethod2 = WebCodeGenerator.AddMethod(this.CodeTypeDeclaration, text2, new CodeFlags[1], new string[] { typeof(object).FullName }, new string[] { "userState" }, typeof(void).FullName, codeAttributeDeclarationCollection, CodeFlags.IsPublic | ((text != text2) ? ((CodeFlags)0) : CodeFlags.IsNew));
					codeMemberMethod2.Comments.Add(new CodeCommentStatement(Res.GetString("CodeRemarks"), true));
					CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), text, Array.Empty<CodeExpression>());
					codeMethodInvokeExpression.Parameters.Add(new CodeArgumentReferenceExpression("userState"));
					codeMemberMethod2.Statements.Add(codeMethodInvokeExpression);
				}
				this.EndClass();
				if (this.portType.Operations.Count == 0)
				{
					this.NoMethodsGeneratedWarning();
				}
				this.AddExtensionWarningComments(this.codeClass.Comments, this.binding.Extensions);
				if (this.port != null)
				{
					this.AddExtensionWarningComments(this.codeClass.Comments, this.port.Extensions);
				}
				this.codeNamespace.Types.Add(this.codeClass);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidOperationException(Res.GetString("UnableToImportBindingFromNamespace2", new object[]
				{
					this.binding.Name,
					this.binding.ServiceDescription.TargetNamespace
				}), ex2);
			}
		}

		/// <summary>For each unhandled extension or XML element in the input extensions collection, turns on a <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.RequiredExtensionsIgnored" /> or <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.OptionalExtensionsIgnored" /> warning for each unhandled extension or XML element in the input extensions collection. </summary>
		/// <param name="comments">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that specifies the collection of code comments to which each warning message is added.</param>
		/// <param name="extensions">A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> that specifies the extensions or XML elements for which warnings should be generated if they are not handled.</param>
		// Token: 0x060005CA RID: 1482 RVA: 0x0001A324 File Offset: 0x00018524
		public void AddExtensionWarningComments(CodeCommentStatementCollection comments, ServiceDescriptionFormatExtensionCollection extensions)
		{
			foreach (object obj in extensions)
			{
				if (!extensions.IsHandled(obj))
				{
					string text = null;
					string text2 = null;
					if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						text = xmlElement.LocalName;
						text2 = xmlElement.NamespaceURI;
					}
					else if (obj is ServiceDescriptionFormatExtension)
					{
						XmlFormatExtensionAttribute[] array = (XmlFormatExtensionAttribute[])obj.GetType().GetCustomAttributes(typeof(XmlFormatExtensionAttribute), false);
						if (array.Length != 0)
						{
							text = array[0].ElementName;
							text2 = array[0].Namespace;
						}
					}
					if (text != null)
					{
						if (extensions.IsRequired(obj))
						{
							this.warnings |= ServiceDescriptionImportWarnings.RequiredExtensionsIgnored;
							ProtocolImporter.AddWarningComment(comments, Res.GetString("WebServiceDescriptionIgnoredRequired", new object[] { text, text2 }));
						}
						else
						{
							this.warnings |= ServiceDescriptionImportWarnings.OptionalExtensionsIgnored;
							ProtocolImporter.AddWarningComment(comments, Res.GetString("WebServiceDescriptionIgnoredOptional", new object[] { text, text2 }));
						}
					}
				}
			}
		}

		/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedBindingsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
		/// <param name="text">Annotation to be added to the warning message, which already indicates that the binding has been ignored.</param>
		// Token: 0x060005CB RID: 1483 RVA: 0x0001A448 File Offset: 0x00018648
		public void UnsupportedBindingWarning(string text)
		{
			ProtocolImporter.AddWarningComment((this.codeClass == null) ? this.codeNamespace.Comments : this.codeClass.Comments, Res.GetString("TheBinding0FromNamespace1WasIgnored2", new object[]
			{
				this.Binding.Name,
				this.Binding.ServiceDescription.TargetNamespace,
				text
			}));
			this.warnings |= ServiceDescriptionImportWarnings.UnsupportedBindingsIgnored;
		}

		/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
		/// <param name="text">Annotation to be added to the warning message, which already indicates that the operation binding has been ignored.</param>
		// Token: 0x060005CC RID: 1484 RVA: 0x0001A4C0 File Offset: 0x000186C0
		public void UnsupportedOperationWarning(string text)
		{
			ProtocolImporter.AddWarningComment((this.codeClass == null) ? this.codeNamespace.Comments : this.codeClass.Comments, Res.GetString("TheOperation0FromNamespace1WasIgnored2", new object[]
			{
				this.operation.Name,
				this.operation.PortType.ServiceDescription.TargetNamespace,
				text
			}));
			this.warnings |= ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored;
		}

		/// <summary>Turns on an <see cref="F:System.Web.Services.Description.ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored" /> warning in the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> enumeration obtained through the <see cref="P:System.Web.Services.Description.ProtocolImporter.Warnings" /> property. This method also adds a warning message to the comments for the class that is being generated.</summary>
		/// <param name="text">Annotation to be added to the warning message, which already indicates that the operation binding has been ignored.</param>
		// Token: 0x060005CD RID: 1485 RVA: 0x0001A53C File Offset: 0x0001873C
		public void UnsupportedOperationBindingWarning(string text)
		{
			ProtocolImporter.AddWarningComment((this.codeClass == null) ? this.codeNamespace.Comments : this.codeClass.Comments, Res.GetString("TheOperationBinding0FromNamespace1WasIgnored", new object[]
			{
				this.operationBinding.Name,
				this.operationBinding.Binding.ServiceDescription.TargetNamespace,
				text
			}));
			this.warnings |= ServiceDescriptionImportWarnings.UnsupportedOperationsIgnored;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001A5B6 File Offset: 0x000187B6
		private void NoMethodsGeneratedWarning()
		{
			ProtocolImporter.AddWarningComment(this.codeClass.Comments, Res.GetString("NoMethodsWereFoundInTheWSDLForThisProtocol"));
			this.warnings |= ServiceDescriptionImportWarnings.NoMethodsGenerated;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001A5E1 File Offset: 0x000187E1
		internal static void AddWarningComment(CodeCommentStatementCollection comments, string text)
		{
			comments.Add(new CodeCommentStatement(Res.GetString("CodegenWarningDetails", new object[] { text })));
		}

		/// <summary>Produces an Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.Operation" /> instance for which a binding class is being generated is invalid within the target namespace.</summary>
		/// <returns>An Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.Operation" /> instance for which a binding class is being generated is invalid within the target namespace.</returns>
		/// <param name="text">Annotation to be added to the exception message, which already indicates that the operation syntax is invalid.</param>
		// Token: 0x060005D0 RID: 1488 RVA: 0x0001A604 File Offset: 0x00018804
		public Exception OperationSyntaxException(string text)
		{
			return new Exception(Res.GetString("TheOperationFromNamespaceHadInvalidSyntax3", new object[]
			{
				this.operation.Name,
				this.operation.PortType.Name,
				this.operation.PortType.ServiceDescription.TargetNamespace,
				text
			}));
		}

		/// <summary>Produces an Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.OperationBinding" /> instance for which a binding class is being generated is invalid within the target namespace.</summary>
		/// <returns>An Exception indicating that the current <see cref="P:System.Web.Services.Description.ProtocolImporter.OperationBinding" /> instance for which a binding class is being generated is invalid within the target namespace.</returns>
		/// <param name="text">Annotation to be added to the exception message, which already indicates that the operation binding syntax is invalid.</param>
		// Token: 0x060005D1 RID: 1489 RVA: 0x0001A664 File Offset: 0x00018864
		public Exception OperationBindingSyntaxException(string text)
		{
			return new Exception(Res.GetString("TheOperationBindingFromNamespaceHadInvalid3", new object[]
			{
				this.operationBinding.Name,
				this.operationBinding.Binding.ServiceDescription.TargetNamespace,
				text
			}));
		}

		/// <summary>Abstract property that concrete derived classes must implement to get the name of the protocol being used.</summary>
		/// <returns>The name of the protocol being used.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005D2 RID: 1490
		public abstract string ProtocolName { get; }

		/// <summary>When overridden in a derived class, performs namespace-wide initialization during code generation.</summary>
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001A6B0 File Offset: 0x000188B0
		protected virtual void BeginNamespace()
		{
			this.MethodNames.Clear();
		}

		/// <summary>When overridden in a derived class, determines whether a class can be generated for the current binding.</summary>
		/// <returns>True if the binding is supported; otherwise false.</returns>
		// Token: 0x060005D4 RID: 1492
		protected abstract bool IsBindingSupported();

		/// <summary>When overridden in a derived class, determines whether the current operation's operation flow is supported.</summary>
		/// <returns>True if the current operation's operation flow is supported.</returns>
		/// <param name="flow">An <see cref="T:System.Web.Services.Description.OperationFlow" />  enumeration value that represents a transmission pattern.</param>
		// Token: 0x060005D5 RID: 1493
		protected abstract bool IsOperationFlowSupported(OperationFlow flow);

		/// <summary>When overridden in a derived class, initializes the generation of a binding class.</summary>
		/// <returns>The generated class.</returns>
		// Token: 0x060005D6 RID: 1494
		protected abstract CodeTypeDeclaration BeginClass();

		/// <summary>When overridden in a derived class, generates method code for binding classes.</summary>
		/// <returns>The generated method.</returns>
		// Token: 0x060005D7 RID: 1495
		protected abstract CodeMemberMethod GenerateMethod();

		/// <summary>When overridden in a derived class, processes a binding class.</summary>
		// Token: 0x060005D8 RID: 1496 RVA: 0x0000210D File Offset: 0x0000030D
		protected virtual void EndClass()
		{
		}

		/// <summary>When overridden in a derived class, performs processing for an entire namespace.</summary>
		// Token: 0x060005D9 RID: 1497 RVA: 0x0001A6C0 File Offset: 0x000188C0
		protected virtual void EndNamespace()
		{
			if (this.classes != null)
			{
				foreach (object obj in this.classes)
				{
					CodeTypeDeclaration codeTypeDeclaration = (CodeTypeDeclaration)obj;
					this.codeNamespace.Types.Add(codeTypeDeclaration);
				}
			}
			CodeGenerator.ValidateIdentifiers(this.codeNamespace);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001A738 File Offset: 0x00018938
		internal static string UniqueName(string baseName, string[] scope)
		{
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			for (int i = 0; i < scope.Length; i++)
			{
				codeIdentifiers.AddUnique(scope[i], scope[i]);
			}
			return codeIdentifiers.AddUnique(baseName, baseName);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001A770 File Offset: 0x00018970
		internal static string MethodSignature(string methodName, string returnType, CodeFlags[] parameterFlags, string[] parameterTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(returnType);
			stringBuilder.Append(" ");
			stringBuilder.Append(methodName);
			stringBuilder.Append(" (");
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				if ((parameterFlags[i] & CodeFlags.IsByRef) != (CodeFlags)0)
				{
					stringBuilder.Append("ref ");
				}
				else if ((parameterFlags[i] & CodeFlags.IsOut) != (CodeFlags)0)
				{
					stringBuilder.Append("out ");
				}
				stringBuilder.Append(parameterTypes[i]);
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x040003A2 RID: 930
		private ServiceDescriptionImporter importer;

		// Token: 0x040003A3 RID: 931
		private CodeNamespace codeNamespace;

		// Token: 0x040003A4 RID: 932
		private CodeIdentifiers methodNames;

		// Token: 0x040003A5 RID: 933
		private CodeTypeDeclaration codeClass;

		// Token: 0x040003A6 RID: 934
		private CodeTypeDeclarationCollection classes;

		// Token: 0x040003A7 RID: 935
		private ServiceDescriptionImportWarnings warnings;

		// Token: 0x040003A8 RID: 936
		private Port port;

		// Token: 0x040003A9 RID: 937
		private PortType portType;

		// Token: 0x040003AA RID: 938
		private Binding binding;

		// Token: 0x040003AB RID: 939
		private Operation operation;

		// Token: 0x040003AC RID: 940
		private OperationBinding operationBinding;

		// Token: 0x040003AD RID: 941
		private bool encodedBinding;

		// Token: 0x040003AE RID: 942
		private ImportContext importContext;

		// Token: 0x040003AF RID: 943
		private Hashtable exportContext;

		// Token: 0x040003B0 RID: 944
		private Service service;

		// Token: 0x040003B1 RID: 945
		private Message inputMessage;

		// Token: 0x040003B2 RID: 946
		private Message outputMessage;

		// Token: 0x040003B3 RID: 947
		private string className;

		// Token: 0x040003B4 RID: 948
		private int bindingCount;

		// Token: 0x040003B5 RID: 949
		private bool anyPorts;
	}
}
