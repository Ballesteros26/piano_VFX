using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	/// <summary>Represents an XML Schema Definition Language (XSD) Schema validation engine. The <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class cannot be inherited.</summary>
	// Token: 0x0200048E RID: 1166
	public sealed class XmlSchemaValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
		/// <param name="nameTable">An <see cref="T:System.Xml.XmlNameTable" /> object containing element and attribute names as atomized strings.</param>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the XML Schema Definition Language (XSD) schemas used for validation.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used for resolving namespaces encountered during validation.</param>
		/// <param name="validationFlags">An <see cref="T:System.Xml.Schema.XmlSchemaValidationFlags" /> value specifying schema validation options.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">An error occurred while compiling schemas in the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> parameter.</exception>
		// Token: 0x06002DAB RID: 11691 RVA: 0x0010A3E0 File Offset: 0x001085E0
		public XmlSchemaValidator(XmlNameTable nameTable, XmlSchemaSet schemas, IXmlNamespaceResolver namespaceResolver, XmlSchemaValidationFlags validationFlags)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			this.nameTable = nameTable;
			this.nsResolver = namespaceResolver;
			this.validationFlags = validationFlags;
			if ((validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None)
			{
				this.schemaSet = new XmlSchemaSet(nameTable);
				this.schemaSet.ValidationEventHandler += schemas.GetEventHandler();
				this.schemaSet.CompilationSettings = schemas.CompilationSettings;
				this.schemaSet.XmlResolver = schemas.GetResolver();
				this.schemaSet.Add(schemas);
				this.validatedNamespaces = new Hashtable();
			}
			else
			{
				this.schemaSet = schemas;
			}
			this.Init();
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0010A4C0 File Offset: 0x001086C0
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.dummyPositionInfo = new PositionInfo();
			this.positionInfo = this.dummyPositionInfo;
			this.validationEventSender = this;
			this.currentState = ValidatorState.None;
			this.textValue = new StringBuilder(100);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.contextQName = new XmlQualifiedName();
			this.Reset();
			this.RecompileSchemaSet();
			this.NsXs = this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.NsXmlNs = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXml = this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.xsiTypeString = this.nameTable.Add("type");
			this.xsiNilString = this.nameTable.Add("nil");
			this.xsiSchemaLocationString = this.nameTable.Add("schemaLocation");
			this.xsiNoNamespaceSchemaLocationString = this.nameTable.Add("noNamespaceSchemaLocation");
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0010A5F4 File Offset: 0x001087F4
		private void Reset()
		{
			this.isRoot = true;
			this.rootHasSchema = true;
			while (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
			}
			this.startIDConstraint = -1;
			this.partialValidationType = null;
			if (this.IDs != null)
			{
				this.IDs.Clear();
			}
			if (this.ProcessSchemaHints)
			{
				this.validatedNamespaces.Clear();
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> object used to resolve xs:import and xs:include elements as well as xsi:schemaLocation and xsi:noNamespaceSchemaLocation attributes.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object; the default is an <see cref="T:System.Xml.XmlUrlResolver" /> object.</returns>
		// Token: 0x17000A12 RID: 2578
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0010A65F File Offset: 0x0010885F
		public XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the line number information for the XML node being validated.</summary>
		/// <returns>An <see cref="T:System.Xml.IXmlLineInfo" /> object.</returns>
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x0010A668 File Offset: 0x00108868
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x0010A670 File Offset: 0x00108870
		public IXmlLineInfo LineInfoProvider
		{
			get
			{
				return this.positionInfo;
			}
			set
			{
				if (value == null)
				{
					this.positionInfo = this.dummyPositionInfo;
					return;
				}
				this.positionInfo = value;
			}
		}

		/// <summary>Gets or sets the source URI for the XML node being validated.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object representing the source URI for the XML node being validated; the default is null.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x0010A689 File Offset: 0x00108889
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x0010A691 File Offset: 0x00108891
		public Uri SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
				this.sourceUriString = this.sourceUri.ToString();
			}
		}

		/// <summary>Gets or sets the object sent as the sender object of a validation event.</summary>
		/// <returns>An <see cref="T:System.Object" />; the default is this <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</returns>
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x0010A6AB File Offset: 0x001088AB
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x0010A6B3 File Offset: 0x001088B3
		public object ValidationEventSender
		{
			get
			{
				return this.validationEventSender;
			}
			set
			{
				this.validationEventSender = value;
			}
		}

		/// <summary>The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> that receives schema validation warnings and errors encountered during schema validation.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06002DB5 RID: 11701 RVA: 0x0010A6BC File Offset: 0x001088BC
		// (remove) Token: 0x06002DB6 RID: 11702 RVA: 0x0010A6D5 File Offset: 0x001088D5
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
			}
		}

		/// <summary>Adds an XML Schema Definition Language (XSD) schema to the set of schemas used for validation.</summary>
		/// <param name="schema">An <see cref="T:System.Xml.Schema.XmlSchema" /> object to add to the set of schemas used for validation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter specified is null.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The target namespace of the <see cref="T:System.Xml.Schema.XmlSchema" /> parameter matches that of any element or attribute already encountered by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaException">The <see cref="T:System.Xml.Schema.XmlSchema" /> parameter is invalid.</exception>
		// Token: 0x06002DB7 RID: 11703 RVA: 0x0010A6F0 File Offset: 0x001088F0
		public void AddSchema(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) == XmlSchemaValidationFlags.None)
			{
				return;
			}
			string text = schema.TargetNamespace;
			if (text == null)
			{
				text = string.Empty;
			}
			Hashtable schemaLocations = this.schemaSet.SchemaLocations;
			DictionaryEntry[] array = new DictionaryEntry[schemaLocations.Count];
			schemaLocations.CopyTo(array, 0);
			if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(schema.BaseUri, text, array) == null)
			{
				this.SendValidationEvent("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text, XmlSeverityType.Error);
			}
			if (schema.ErrorCount == 0)
			{
				try
				{
					this.schemaSet.Add(schema);
					this.RecompileSchemaSet();
				}
				catch (XmlSchemaException ex)
				{
					this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[]
					{
						schema.BaseUri.ToString(),
						ex.Message
					}, ex);
				}
				for (int i = 0; i < schema.ImportedSchemas.Count; i++)
				{
					XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
					text = xmlSchema.TargetNamespace;
					if (text == null)
					{
						text = string.Empty;
					}
					if (this.validatedNamespaces[text] != null && this.schemaSet.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, array) == null)
					{
						this.SendValidationEvent("An element or attribute information item has already been validated from the '{0}' namespace. It is an error if 'xsi:schemaLocation', 'xsi:noNamespaceSchemaLocation', or an inline schema occurs for that namespace.", text, XmlSeverityType.Error);
						this.schemaSet.RemoveRecursive(schema);
						return;
					}
				}
			}
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		// Token: 0x06002DB8 RID: 11704 RVA: 0x0010A84C File Offset: 0x00108A4C
		public void Initialize()
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				throw new InvalidOperationException(Res.GetString("The transition from the '{0}' method to the '{1}' method is not allowed.", new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				}));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
		}

		/// <summary>Initializes the state of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object using the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> specified for partial validation.</summary>
		/// <param name="partialValidationType">An <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object used to initialize the validation context of the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object for partial validation.</param>
		/// <exception cref="T:System.InvalidOperationException">Calling the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.Initialize" /> method is valid immediately after the construction of an <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object or after a call to <see cref="M:System.Xml.Schema.XmlSchemaValidator.EndValidation" /> only.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter is not an <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaAttribute" />, or <see cref="T:System.Xml.Schema.XmlSchemaType" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter cannot be null.</exception>
		// Token: 0x06002DB9 RID: 11705 RVA: 0x0010A8A8 File Offset: 0x00108AA8
		public void Initialize(XmlSchemaObject partialValidationType)
		{
			if (this.currentState != ValidatorState.None && this.currentState != ValidatorState.Finish)
			{
				throw new InvalidOperationException(Res.GetString("The transition from the '{0}' method to the '{1}' method is not allowed.", new string[]
				{
					XmlSchemaValidator.MethodNames[(int)this.currentState],
					XmlSchemaValidator.MethodNames[1]
				}));
			}
			if (partialValidationType == null)
			{
				throw new ArgumentNullException("partialValidationType");
			}
			if (!(partialValidationType is XmlSchemaElement) && !(partialValidationType is XmlSchemaAttribute) && !(partialValidationType is XmlSchemaType))
			{
				throw new ArgumentException(Res.GetString("The partial validation type has to be 'XmlSchemaElement', 'XmlSchemaAttribute', or 'XmlSchemaType'."));
			}
			this.currentState = ValidatorState.Start;
			this.Reset();
			this.partialValidationType = partialValidationType;
		}

		/// <summary>Validates the element in the current context.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06002DBA RID: 11706 RVA: 0x0010A940 File Offset: 0x00108B40
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo)
		{
			this.ValidateElement(localName, namespaceUri, schemaInfo, null, null, null, null);
		}

		/// <summary>Validates the element in the current context with the xsi:Type, xsi:Nil, xsi:SchemaLocation, and xsi:NoNamespaceSchemaLocation attribute values specified.</summary>
		/// <param name="localName">The local name of the element to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the element to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element's name. This parameter can be null.</param>
		/// <param name="xsiType">The xsi:Type attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiNil">The xsi:Nil attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiSchemaLocation">The xsi:SchemaLocation attribute value of the element. This parameter can be null.</param>
		/// <param name="xsiNoNamespaceSchemaLocation">The xsi:NoNamespaceSchemaLocation attribute value of the element. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's name is not valid in the current context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateElement" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06002DBB RID: 11707 RVA: 0x0010A950 File Offset: 0x00108B50
		public void ValidateElement(string localName, string namespaceUri, XmlSchemaInfo schemaInfo, string xsiType, string xsiNil, string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			if (localName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (namespaceUri == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			this.CheckStateTransition(ValidatorState.Element, XmlSchemaValidator.MethodNames[4]);
			this.ClearPSVI();
			this.contextQName.Init(localName, namespaceUri);
			XmlQualifiedName xmlQualifiedName = this.contextQName;
			bool flag;
			object obj = this.ValidateElementContext(xmlQualifiedName, out flag);
			SchemaElementDecl schemaElementDecl = this.FastGetElementDecl(xmlQualifiedName, obj);
			this.Push(xmlQualifiedName);
			if (flag)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if ((this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) != XmlSchemaValidationFlags.None && this.xmlResolver != null)
			{
				this.ProcessSchemaLocations(xsiSchemaLocation, xsiNoNamespaceSchemaLocation);
			}
			if (this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (schemaElementDecl == null && this.partialValidationType == null)
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(xmlQualifiedName);
				}
				bool flag2 = schemaElementDecl != null;
				if (xsiType != null || xsiNil != null)
				{
					schemaElementDecl = this.CheckXsiTypeAndNil(schemaElementDecl, xsiType, xsiNil, ref flag2);
				}
				if (schemaElementDecl == null)
				{
					this.ThrowDeclNotFoundWarningOrError(flag2);
				}
			}
			this.context.ElementDecl = schemaElementDecl;
			XmlSchemaElement xmlSchemaElement = null;
			XmlSchemaType xmlSchemaType = null;
			if (schemaElementDecl != null)
			{
				this.CheckElementProperties();
				this.attPresence.Clear();
				this.context.NeedValidateChildren = this.processContents != XmlSchemaContentProcessing.Skip;
				this.ValidateStartElementIdentityConstraints();
				schemaElementDecl.ContentValidator.InitValidation(this.context);
				xmlSchemaType = schemaElementDecl.SchemaType;
				xmlSchemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = xmlSchemaType;
				schemaInfo.SchemaElement = xmlSchemaElement;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.Validity = this.context.Validity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[namespaceUri] == null)
			{
				this.validatedNamespaces.Add(namespaceUri, namespaceUri);
			}
			if (this.isRoot)
			{
				this.isRoot = false;
			}
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <returns>The validated attribute's value.</returns>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">The value of the attribute to validate.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x06002DBC RID: 11708 RVA: 0x0010AAF9 File Offset: 0x00108CF9
		public object ValidateAttribute(string localName, string namespaceUri, string attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, null, attributeValue, schemaInfo);
		}

		/// <summary>Validates the attribute name, namespace URI, and value in the current element context.</summary>
		/// <returns>The validated attribute's value.</returns>
		/// <param name="localName">The local name of the attribute to validate.</param>
		/// <param name="namespaceUri">The namespace URI of the attribute to validate.</param>
		/// <param name="attributeValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the attribute's value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the attribute. This parameter and can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The attribute is not valid in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method was not called in the correct sequence. For example, calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x06002DBD RID: 11709 RVA: 0x0010AB15 File Offset: 0x00108D15
		public object ValidateAttribute(string localName, string namespaceUri, XmlValueGetter attributeValue, XmlSchemaInfo schemaInfo)
		{
			if (attributeValue == null)
			{
				throw new ArgumentNullException("attributeValue");
			}
			return this.ValidateAttribute(localName, namespaceUri, attributeValue, null, schemaInfo);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0010AB34 File Offset: 0x00108D34
		private object ValidateAttribute(string lName, string ns, XmlValueGetter attributeValueGetter, string attributeStringValue, XmlSchemaInfo schemaInfo)
		{
			if (lName == null)
			{
				throw new ArgumentNullException("localName");
			}
			if (ns == null)
			{
				throw new ArgumentNullException("namespaceUri");
			}
			ValidatorState validatorState = ((this.validationStack.Length > 1) ? ValidatorState.Attribute : ValidatorState.TopLevelAttribute);
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			object obj = null;
			this.attrValid = true;
			XmlSchemaValidity xmlSchemaValidity = XmlSchemaValidity.NotKnown;
			XmlSchemaAttribute xmlSchemaAttribute = null;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			ns = this.nameTable.Add(ns);
			if (Ref.Equal(ns, this.NsXmlNs))
			{
				return null;
			}
			SchemaAttDef schemaAttDef = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(lName, ns);
			if (this.attPresence[xmlQualifiedName] != null)
			{
				this.SendValidationEvent("The '{0}' attribute has already been validated and is a duplicate attribute.", xmlQualifiedName.ToString());
				if (schemaInfo != null)
				{
					schemaInfo.Clear();
				}
				return null;
			}
			if (!Ref.Equal(ns, this.NsXsi))
			{
				XmlSchemaObject xmlSchemaObject = ((this.currentState == ValidatorState.TopLevelAttribute) ? this.partialValidationType : null);
				AttributeMatchState attributeMatchState;
				schemaAttDef = this.compiledSchemaInfo.GetAttributeXsd(elementDecl, xmlQualifiedName, xmlSchemaObject, out attributeMatchState);
				switch (attributeMatchState)
				{
				case AttributeMatchState.AttributeFound:
					break;
				case AttributeMatchState.AnyIdAttributeFound:
					if (this.wildID != null)
					{
						this.SendValidationEvent("It is an error if more than one attribute whose type is xs:ID or is derived from xs:ID, matches an attribute wildcard on an element.", string.Empty);
						goto IL_03F5;
					}
					this.wildID = schemaAttDef;
					if ((elementDecl.SchemaType as XmlSchemaComplexType).ContainsIdAttribute(false))
					{
						this.SendValidationEvent("It is an error if there is a member of the attribute uses of a type definition with type xs:ID or derived from xs:ID and another attribute with type xs:ID matches an attribute wildcard.", string.Empty);
						goto IL_03F5;
					}
					break;
				case AttributeMatchState.UndeclaredElementAndAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						if (elementDecl == null && this.processContents == XmlSchemaContentProcessing.Strict && xmlQualifiedName.Namespace.Length != 0 && this.compiledSchemaInfo.Contains(xmlQualifiedName.Namespace))
						{
							this.attrValid = false;
							this.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.ToString());
							goto IL_03F5;
						}
						if (this.processContents != XmlSchemaContentProcessing.Skip)
						{
							this.SendValidationEvent("Could not find schema information for the attribute '{0}'.", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
							goto IL_03F5;
						}
						goto IL_03F5;
					}
					break;
				case AttributeMatchState.UndeclaredAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.ToString());
						goto IL_03F5;
					}
					break;
				case AttributeMatchState.AnyAttributeLax:
					this.SendValidationEvent("Could not find schema information for the attribute '{0}'.", xmlQualifiedName.ToString(), XmlSeverityType.Warning);
					goto IL_03F5;
				case AttributeMatchState.AnyAttributeSkip:
					goto IL_03F5;
				case AttributeMatchState.ProhibitedAnyAttribute:
					if ((schemaAttDef = this.CheckIsXmlAttribute(xmlQualifiedName)) == null)
					{
						this.attrValid = false;
						this.SendValidationEvent("The '{0}' attribute is not allowed.", xmlQualifiedName.ToString());
						goto IL_03F5;
					}
					break;
				case AttributeMatchState.ProhibitedAttribute:
					this.attrValid = false;
					this.SendValidationEvent("The '{0}' attribute is not allowed.", xmlQualifiedName.ToString());
					goto IL_03F5;
				case AttributeMatchState.AttributeNameMismatch:
					this.attrValid = false;
					this.SendValidationEvent("The attribute name '{0}' does not match the name '{1}' of the 'XmlSchemaAttribute' set as a partial validation type.", new string[]
					{
						xmlQualifiedName.ToString(),
						((XmlSchemaAttribute)xmlSchemaObject).QualifiedName.ToString()
					});
					goto IL_03F5;
				case AttributeMatchState.ValidateAttributeInvalidCall:
					this.currentState = ValidatorState.Start;
					this.attrValid = false;
					this.SendValidationEvent("If the partial validation type is 'XmlSchemaElement' or 'XmlSchemaType', the 'ValidateAttribute' method cannot be called.", string.Empty);
					goto IL_03F5;
				default:
					goto IL_03F5;
				}
				xmlSchemaAttribute = schemaAttDef.SchemaAttribute;
				if (elementDecl != null)
				{
					this.attPresence.Add(xmlQualifiedName, schemaAttDef);
				}
				object obj2;
				if (attributeValueGetter != null)
				{
					obj2 = attributeValueGetter();
				}
				else
				{
					obj2 = attributeStringValue;
				}
				obj = this.CheckAttributeValue(obj2, schemaAttDef);
				XmlSchemaDatatype xmlSchemaDatatype = schemaAttDef.Datatype;
				if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.Union && obj != null)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					xmlSchemaSimpleType = xsdSimpleValue.XmlType;
					xmlSchemaDatatype = xsdSimpleValue.XmlType.Datatype;
					obj = xsdSimpleValue.TypedValue;
				}
				this.CheckTokenizedTypes(xmlSchemaDatatype, obj, true);
				if (this.HasIdentityConstraints)
				{
					this.AttributeIdentityConstraints(xmlQualifiedName.Name, xmlQualifiedName.Namespace, obj, obj2.ToString(), xmlSchemaDatatype);
				}
			}
			else
			{
				lName = this.nameTable.Add(lName);
				if (Ref.Equal(lName, this.xsiTypeString) || Ref.Equal(lName, this.xsiNilString) || Ref.Equal(lName, this.xsiSchemaLocationString) || Ref.Equal(lName, this.xsiNoNamespaceSchemaLocationString))
				{
					this.attPresence.Add(xmlQualifiedName, SchemaAttDef.Empty);
				}
				else
				{
					this.attrValid = false;
					this.SendValidationEvent("The attribute '{0}' does not match one of the four allowed attributes in the 'xsi' namespace.", xmlQualifiedName.ToString());
				}
			}
			IL_03F5:
			if (!this.attrValid)
			{
				xmlSchemaValidity = XmlSchemaValidity.Invalid;
			}
			else if (schemaAttDef != null)
			{
				xmlSchemaValidity = XmlSchemaValidity.Valid;
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaAttribute = xmlSchemaAttribute;
				schemaInfo.SchemaType = ((xmlSchemaAttribute == null) ? null : xmlSchemaAttribute.AttributeSchemaType);
				schemaInfo.MemberType = xmlSchemaSimpleType;
				schemaInfo.IsDefault = false;
				schemaInfo.Validity = xmlSchemaValidity;
			}
			if (this.ProcessSchemaHints && this.validatedNamespaces[ns] == null)
			{
				this.validatedNamespaces.Add(ns, ns);
			}
			return obj;
		}

		/// <summary>Validates identity constraints on the default attributes and populates the <see cref="T:System.Collections.ArrayList" /> specified with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes with default values that have not been previously validated using the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" /> method in the element context. </summary>
		/// <param name="defaultAttributes">An <see cref="T:System.Collections.ArrayList" /> to populate with <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects for any attributes not yet encountered during validation in the element context.</param>
		// Token: 0x06002DBF RID: 11711 RVA: 0x0010AFA4 File Offset: 0x001091A4
		public void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes)
		{
			if (defaultAttributes == null)
			{
				throw new ArgumentNullException("defaultAttributes");
			}
			this.CheckStateTransition(ValidatorState.Attribute, "GetUnspecifiedDefaultAttributes");
			this.GetUnspecifiedDefaultAttributes(defaultAttributes, false);
		}

		/// <summary>Verifies whether all the required attributes in the element context are present and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate the child content of the element.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful verification that all the required attributes in the element context are present. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">One or more of the required attributes in the current element context were not found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.ValidateEndOfAttributes(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One or more of the parameters specified are null.</exception>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x0010AFC8 File Offset: 0x001091C8
		public void ValidateEndOfAttributes(XmlSchemaInfo schemaInfo)
		{
			this.CheckStateTransition(ValidatorState.EndOfAttributes, XmlSchemaValidator.MethodNames[6]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasRequiredAttribute)
			{
				this.context.CheckRequiredAttribute = false;
				this.CheckRequiredAttributes(elementDecl);
			}
			if (schemaInfo != null)
			{
				schemaInfo.Validity = this.context.Validity;
			}
		}

		/// <summary>Validates whether the text string specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A text string to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text string specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text string parameter cannot be null.</exception>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x0010B021 File Offset: 0x00109221
		public void ValidateText(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(elementValue, null);
		}

		/// <summary>Validates whether the text returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the text for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the text value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The text string specified is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method was not called in the correct sequence. For example, the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The text string parameter cannot be null.</exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x0010B039 File Offset: 0x00109239
		public void ValidateText(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateText(null, elementValue);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x0010B054 File Offset: 0x00109254
		private void ValidateText(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = ((this.validationStack.Length > 1) ? ValidatorState.Text : ValidatorState.TopLevelTextOrWS);
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("The element cannot contain text. Content model is empty.", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
				{
					string text = ((elementValueGetter != null) ? elementValueGetter().ToString() : elementStringValue);
					if (!this.xmlCharType.IsOnlyWhitespace(text))
					{
						ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
						if (arrayList == null || arrayList.Count == 0)
						{
							this.SendValidationEvent("The element {0} cannot contain text.", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
							return;
						}
						this.SendValidationEvent("The element {0} cannot contain text. List of possible elements expected: {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, true)
						});
						return;
					}
					break;
				}
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		/// <summary>Validates whether the white space in the string specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">A white space string to validate in the current element context.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06002DC4 RID: 11716 RVA: 0x0010B1F7 File Offset: 0x001093F7
		public void ValidateWhitespace(string elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(elementValue, null);
		}

		/// <summary>Validates whether the white space returned by the <see cref="T:System.Xml.Schema.XmlValueGetter" /> object specified is allowed in the current element context, and accumulates the white space for validation if the current element has simple content.</summary>
		/// <param name="elementValue">An <see cref="T:System.Xml.Schema.XmlValueGetter" />delegate used to pass the white space value as a Common Language Runtime (CLR) type compatible with the XML Schema Definition Language (XSD) type of the attribute.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">White space is not allowed in the current element context.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateWhitespace" /> method is called after calling <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateAttribute" />.</exception>
		// Token: 0x06002DC5 RID: 11717 RVA: 0x0010B20F File Offset: 0x0010940F
		public void ValidateWhitespace(XmlValueGetter elementValue)
		{
			if (elementValue == null)
			{
				throw new ArgumentNullException("elementValue");
			}
			this.ValidateWhitespace(null, elementValue);
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0010B228 File Offset: 0x00109428
		private void ValidateWhitespace(string elementStringValue, XmlValueGetter elementValueGetter)
		{
			ValidatorState validatorState = ((this.validationStack.Length > 1) ? ValidatorState.Whitespace : ValidatorState.TopLevelTextOrWS);
			this.CheckStateTransition(validatorState, XmlSchemaValidator.MethodNames[(int)validatorState]);
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				switch (this.context.ElementDecl.ContentValidator.ContentType)
				{
				case XmlSchemaContentType.TextOnly:
					if (elementValueGetter != null)
					{
						this.SaveTextValue(elementValueGetter());
						return;
					}
					this.SaveTextValue(elementStringValue);
					return;
				case XmlSchemaContentType.Empty:
					this.SendValidationEvent("The element cannot contain white space. Content model is empty.", string.Empty);
					return;
				case XmlSchemaContentType.ElementOnly:
					break;
				case XmlSchemaContentType.Mixed:
					if (this.context.ElementDecl.DefaultValueTyped != null)
					{
						if (elementValueGetter != null)
						{
							this.SaveTextValue(elementValueGetter());
							return;
						}
						this.SaveTextValue(elementStringValue);
					}
					break;
				default:
					return;
				}
			}
		}

		/// <summary>Verifies if the text content of the element is valid according to its data type for elements with simple content, and verifies if the content of the current element is complete for elements with complex content.</summary>
		/// <returns>The parsed, typed text value of the element if the element has simple content.</returns>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the element. This parameter can be null.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence. For example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x06002DC7 RID: 11719 RVA: 0x0010B318 File Offset: 0x00109518
		public object ValidateEndElement(XmlSchemaInfo schemaInfo)
		{
			return this.InternalValidateEndElement(schemaInfo, null);
		}

		/// <summary>Verifies if the text content of the element specified is valid according to its data type.</summary>
		/// <returns>The parsed, typed simple content of the element.</returns>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set on successful validation of the text content of the element. This parameter can be null.</param>
		/// <param name="typedValue">The typed text content of the element.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The element's text content is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method was not called in the correct sequence (for example, if the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateEndElement" /> method is called after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />), calls to the <see cref="Overload:System.Xml.Schema.XmlSchemaValidator.ValidateText" /> method have been previously made, or the element has complex content.</exception>
		/// <exception cref="T:System.ArgumentNullException">The typed text content parameter cannot be null.</exception>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x0010B322 File Offset: 0x00109522
		public object ValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (typedValue == null)
			{
				throw new ArgumentNullException("typedValue");
			}
			if (this.textValue.Length > 0)
			{
				throw new InvalidOperationException(Res.GetString("It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' after 'ValidateText' or 'ValidateWhitespace' methods have been called."));
			}
			return this.InternalValidateEndElement(schemaInfo, typedValue);
		}

		/// <summary>Skips validation of the current element content and prepares the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> object to validate content in the parent element's context.</summary>
		/// <param name="schemaInfo">An <see cref="T:System.Xml.Schema.XmlSchemaInfo" /> object whose properties are set if the current element content is successfully skipped. This parameter can be null.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> method was not called in the correct sequence. For example, calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" /> after calling <see cref="M:System.Xml.Schema.XmlSchemaValidator.SkipToEndElement(System.Xml.Schema.XmlSchemaInfo)" />.</exception>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x0010B358 File Offset: 0x00109558
		public void SkipToEndElement(XmlSchemaInfo schemaInfo)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.", new object[] { XmlSchemaValidator.MethodNames[10] }));
			}
			this.CheckStateTransition(ValidatorState.SkipToEndElement, XmlSchemaValidator.MethodNames[10]);
			if (schemaInfo != null)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				if (elementDecl != null)
				{
					schemaInfo.SchemaType = elementDecl.SchemaType;
					schemaInfo.SchemaElement = this.GetSchemaElement();
				}
				else
				{
					schemaInfo.SchemaType = null;
					schemaInfo.SchemaElement = null;
				}
				schemaInfo.MemberType = null;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				schemaInfo.Validity = this.context.Validity;
			}
			this.context.ValidationSkipped = true;
			this.currentState = ValidatorState.SkipToEndElement;
			this.Pop();
		}

		/// <summary>Ends validation and checks identity constraints for the entire XML document.</summary>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">An identity constraint error was found in the XML document.</exception>
		// Token: 0x06002DCA RID: 11722 RVA: 0x0010B432 File Offset: 0x00109632
		public void EndValidation()
		{
			if (this.validationStack.Length > 1)
			{
				throw new InvalidOperationException(Res.GetString("The 'EndValidation' method cannot not be called when all the elements have not been validated. 'ValidateEndElement' calls corresponding to 'ValidateElement' calls might be missing."));
			}
			this.CheckStateTransition(ValidatorState.Finish, XmlSchemaValidator.MethodNames[11]);
			this.CheckForwardRefs();
		}

		/// <summary>Returns the expected particles in the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaParticle" /> objects or an empty array if there are no expected particles.</returns>
		// Token: 0x06002DCB RID: 11723 RVA: 0x0010B468 File Offset: 0x00109668
		public XmlSchemaParticle[] GetExpectedParticles()
		{
			if (this.currentState != ValidatorState.Start && this.currentState != ValidatorState.TopLevelTextOrWS)
			{
				if (this.context.ElementDecl != null)
				{
					ArrayList arrayList = this.context.ElementDecl.ContentValidator.ExpectedParticles(this.context, false, this.schemaSet);
					if (arrayList != null)
					{
						return arrayList.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
					}
				}
				return XmlSchemaValidator.EmptyParticleArray;
			}
			if (this.partialValidationType == null)
			{
				ICollection values = this.schemaSet.GlobalElements.Values;
				ArrayList arrayList2 = new ArrayList(values.Count);
				foreach (object obj in values)
				{
					ContentValidator.AddParticleToExpected((XmlSchemaElement)obj, this.schemaSet, arrayList2, true);
				}
				return arrayList2.ToArray(typeof(XmlSchemaParticle)) as XmlSchemaParticle[];
			}
			XmlSchemaElement xmlSchemaElement = this.partialValidationType as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return new XmlSchemaParticle[] { xmlSchemaElement };
			}
			return XmlSchemaValidator.EmptyParticleArray;
		}

		/// <summary>Returns the expected attributes for the current element context.</summary>
		/// <returns>An array of <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> objects or an empty array if there are no expected attributes.</returns>
		// Token: 0x06002DCC RID: 11724 RVA: 0x0010B580 File Offset: 0x00109780
		public XmlSchemaAttribute[] GetExpectedAttributes()
		{
			if (this.currentState == ValidatorState.Element || this.currentState == ValidatorState.Attribute)
			{
				SchemaElementDecl elementDecl = this.context.ElementDecl;
				ArrayList arrayList = new ArrayList();
				if (elementDecl != null)
				{
					foreach (SchemaAttDef schemaAttDef in elementDecl.AttDefs.Values)
					{
						if (this.attPresence[schemaAttDef.Name] == null)
						{
							arrayList.Add(schemaAttDef.SchemaAttribute);
						}
					}
				}
				if (this.nsResolver.LookupPrefix(this.NsXsi) != null)
				{
					this.AddXsiAttributes(arrayList);
				}
				return arrayList.ToArray(typeof(XmlSchemaAttribute)) as XmlSchemaAttribute[];
			}
			if (this.currentState == ValidatorState.Start && this.partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = this.partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					return new XmlSchemaAttribute[] { xmlSchemaAttribute };
				}
			}
			return XmlSchemaValidator.EmptyAttributeArray;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x0010B680 File Offset: 0x00109880
		internal void GetUnspecifiedDefaultAttributes(ArrayList defaultAttributes, bool createNodeData)
		{
			this.currentState = ValidatorState.Attribute;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDecl != null && elementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < elementDecl.DefaultAttDefs.Count; i++)
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)elementDecl.DefaultAttDefs[i];
					if (!this.attPresence.Contains(schemaAttDef.Name) && schemaAttDef.DefaultValueTyped != null)
					{
						string text = this.nameTable.Add(schemaAttDef.Name.Namespace);
						string text2 = string.Empty;
						if (text.Length > 0)
						{
							text2 = this.GetDefaultAttributePrefix(text);
							if (text2 == null || text2.Length == 0)
							{
								this.SendValidationEvent("Default attribute '{0}' for element '{1}' could not be applied as the attribute namespace is not mapped to a prefix in the instance document.", new string[]
								{
									schemaAttDef.Name.ToString(),
									XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
								});
								goto IL_0242;
							}
						}
						XmlSchemaDatatype xmlSchemaDatatype = schemaAttDef.Datatype;
						if (createNodeData)
						{
							ValidatingReaderNodeData validatingReaderNodeData = new ValidatingReaderNodeData();
							validatingReaderNodeData.LocalName = this.nameTable.Add(schemaAttDef.Name.Name);
							validatingReaderNodeData.Namespace = text;
							validatingReaderNodeData.Prefix = this.nameTable.Add(text2);
							validatingReaderNodeData.NodeType = XmlNodeType.Attribute;
							AttributePSVIInfo attributePSVIInfo = new AttributePSVIInfo();
							XmlSchemaInfo attributeSchemaInfo = attributePSVIInfo.attributeSchemaInfo;
							if (schemaAttDef.Datatype.Variety == XmlSchemaDatatypeVariety.Union)
							{
								XsdSimpleValue xsdSimpleValue = schemaAttDef.DefaultValueTyped as XsdSimpleValue;
								attributeSchemaInfo.MemberType = xsdSimpleValue.XmlType;
								xmlSchemaDatatype = xsdSimpleValue.XmlType.Datatype;
								attributePSVIInfo.typedAttributeValue = xsdSimpleValue.TypedValue;
							}
							else
							{
								attributePSVIInfo.typedAttributeValue = schemaAttDef.DefaultValueTyped;
							}
							attributeSchemaInfo.IsDefault = true;
							attributeSchemaInfo.Validity = XmlSchemaValidity.Valid;
							attributeSchemaInfo.SchemaType = schemaAttDef.SchemaType;
							attributeSchemaInfo.SchemaAttribute = schemaAttDef.SchemaAttribute;
							validatingReaderNodeData.RawValue = attributeSchemaInfo.XmlType.ValueConverter.ToString(attributePSVIInfo.typedAttributeValue);
							validatingReaderNodeData.AttInfo = attributePSVIInfo;
							defaultAttributes.Add(validatingReaderNodeData);
						}
						else
						{
							defaultAttributes.Add(schemaAttDef.SchemaAttribute);
						}
						this.CheckTokenizedTypes(xmlSchemaDatatype, schemaAttDef.DefaultValueTyped, true);
						if (this.HasIdentityConstraints)
						{
							this.AttributeIdentityConstraints(schemaAttDef.Name.Name, schemaAttDef.Name.Namespace, schemaAttDef.DefaultValueTyped, schemaAttDef.DefaultValueRaw, xmlSchemaDatatype);
						}
					}
					IL_0242:;
				}
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x0010B8E4 File Offset: 0x00109AE4
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				return this.schemaSet;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x0010B8EC File Offset: 0x00109AEC
		internal XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x0010B8F4 File Offset: 0x00109AF4
		internal XmlSchemaContentType CurrentContentType
		{
			get
			{
				if (this.context.ElementDecl == null)
				{
					return XmlSchemaContentType.Empty;
				}
				return this.context.ElementDecl.ContentValidator.ContentType;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x0010B91A File Offset: 0x00109B1A
		internal XmlSchemaContentProcessing CurrentProcessContents
		{
			get
			{
				return this.processContents;
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x0010B922 File Offset: 0x00109B22
		internal void SetDtdSchemaInfo(IDtdInfo dtdSchemaInfo)
		{
			this.dtdSchemaInfo = dtdSchemaInfo;
			this.checkEntity = true;
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x0010B932 File Offset: 0x00109B32
		private bool StrictlyAssessed
		{
			get
			{
				return (this.processContents == XmlSchemaContentProcessing.Strict || this.processContents == XmlSchemaContentProcessing.Lax) && this.context.ElementDecl != null && !this.context.ValidationSkipped;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x0010B963 File Offset: 0x00109B63
		private bool HasSchema
		{
			get
			{
				if (this.isRoot)
				{
					this.isRoot = false;
					if (!this.compiledSchemaInfo.Contains(this.context.Namespace))
					{
						this.rootHasSchema = false;
					}
				}
				return this.rootHasSchema;
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x0010B999 File Offset: 0x00109B99
		internal string GetConcatenatedValue()
		{
			return this.textValue.ToString();
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0010B9A8 File Offset: 0x00109BA8
		private object InternalValidateEndElement(XmlSchemaInfo schemaInfo, object typedValue)
		{
			if (this.validationStack.Length <= 1)
			{
				throw new InvalidOperationException(Res.GetString("The call to the '{0}' method does not match a corresponding call to 'ValidateElement' method.", new object[] { XmlSchemaValidator.MethodNames[9] }));
			}
			this.CheckStateTransition(ValidatorState.EndElement, XmlSchemaValidator.MethodNames[9]);
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaElement xmlSchemaElement = null;
			string text = string.Empty;
			if (elementDecl != null)
			{
				if (this.context.CheckRequiredAttribute && elementDecl.HasRequiredAttribute)
				{
					this.CheckRequiredAttributes(elementDecl);
				}
				if (!this.context.IsNill && this.context.NeedValidateChildren)
				{
					switch (elementDecl.ContentValidator.ContentType)
					{
					case XmlSchemaContentType.TextOnly:
						if (typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.ValidateAtomicValue(text, out xmlSchemaSimpleType);
						}
						else
						{
							typedValue = this.ValidateAtomicValue(typedValue, out xmlSchemaSimpleType);
						}
						break;
					case XmlSchemaContentType.ElementOnly:
						if (typedValue != null)
						{
							throw new InvalidOperationException(Res.GetString("It is invalid to call the 'ValidateEndElement' overload that takes in a 'typedValue' for elements with complex content."));
						}
						break;
					case XmlSchemaContentType.Mixed:
						if (elementDecl.DefaultValueTyped != null && typedValue == null)
						{
							text = this.textValue.ToString();
							typedValue = this.CheckMixedValueConstraint(text);
						}
						break;
					}
					if (!elementDecl.ContentValidator.CompleteValidation(this.context))
					{
						XmlSchemaValidator.CompleteValidationError(this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
						this.context.Validity = XmlSchemaValidity.Invalid;
					}
				}
				if (this.HasIdentityConstraints)
				{
					XmlSchemaType xmlSchemaType2 = ((xmlSchemaSimpleType == null) ? elementDecl.SchemaType : xmlSchemaSimpleType);
					this.EndElementIdentityConstraints(typedValue, text, xmlSchemaType2.Datatype);
				}
				xmlSchemaType = elementDecl.SchemaType;
				xmlSchemaElement = this.GetSchemaElement();
			}
			if (schemaInfo != null)
			{
				schemaInfo.SchemaType = xmlSchemaType;
				schemaInfo.SchemaElement = xmlSchemaElement;
				schemaInfo.MemberType = xmlSchemaSimpleType;
				schemaInfo.IsNil = this.context.IsNill;
				schemaInfo.IsDefault = this.context.IsDefault;
				if (this.context.Validity == XmlSchemaValidity.NotKnown && this.StrictlyAssessed)
				{
					this.context.Validity = XmlSchemaValidity.Valid;
				}
				schemaInfo.Validity = this.context.Validity;
			}
			this.Pop();
			return typedValue;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x0010BBD8 File Offset: 0x00109DD8
		private void ProcessSchemaLocations(string xsiSchemaLocation, string xsiNoNamespaceSchemaLocation)
		{
			bool flag = false;
			if (xsiNoNamespaceSchemaLocation != null)
			{
				flag = true;
				this.LoadSchema(string.Empty, xsiNoNamespaceSchemaLocation);
			}
			if (xsiSchemaLocation != null)
			{
				object obj;
				Exception ex = XmlSchemaValidator.dtStringArray.TryParseValue(xsiSchemaLocation, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}", new string[]
					{
						"schemaLocation",
						xsiSchemaLocation,
						XmlSchemaValidator.dtStringArray.TypeCodeString,
						ex.Message
					}, ex);
					return;
				}
				string[] array = (string[])obj;
				flag = true;
				try
				{
					for (int i = 0; i < array.Length - 1; i += 2)
					{
						this.LoadSchema(array[i], array[i + 1]);
					}
				}
				catch (XmlSchemaException ex2)
				{
					this.SendValidationEvent(ex2);
				}
			}
			if (flag)
			{
				this.RecompileSchemaSet();
			}
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0010BCA4 File Offset: 0x00109EA4
		private object ValidateElementContext(XmlQualifiedName elementName, out bool invalidElementInContext)
		{
			object obj = null;
			int num = 0;
			invalidElementInContext = false;
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Element '{0}' must have no character or element children.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				if (this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.Mixed && this.context.ElementDecl.Presence == SchemaDeclBase.Use.Fixed)
				{
					this.SendValidationEvent("Although the '{0}' element's content type is mixed, it cannot have element children, because it has a fixed value constraint in the schema.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return null;
				}
				XmlQualifiedName xmlQualifiedName = elementName;
				bool flag = false;
				for (;;)
				{
					obj = this.context.ElementDecl.ContentValidator.ValidateElement(xmlQualifiedName, this.context, out num);
					if (obj != null)
					{
						goto IL_0111;
					}
					if (num == -2)
					{
						break;
					}
					flag = true;
					XmlSchemaElement substitutionGroupHead = this.GetSubstitutionGroupHead(xmlQualifiedName);
					if (substitutionGroupHead == null)
					{
						goto IL_0111;
					}
					xmlQualifiedName = substitutionGroupHead.QualifiedName;
				}
				this.SendValidationEvent("Element '{0}' cannot appear more than once if content model type is \"all\".", elementName.ToString());
				invalidElementInContext = true;
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				return null;
				IL_0111:
				if (flag)
				{
					XmlSchemaElement xmlSchemaElement = obj as XmlSchemaElement;
					if (xmlSchemaElement == null)
					{
						obj = null;
					}
					else if (xmlSchemaElement.RefName.IsEmpty)
					{
						this.SendValidationEvent("The element {0} cannot substitute for a local element {1} expected in that position.", XmlSchemaValidator.BuildElementName(elementName), XmlSchemaValidator.BuildElementName(xmlSchemaElement.QualifiedName));
						invalidElementInContext = true;
						this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
					}
					else
					{
						obj = this.compiledSchemaInfo.GetElement(elementName);
						this.context.NeedValidateChildren = true;
					}
				}
				if (obj == null)
				{
					XmlSchemaValidator.ElementValidationError(elementName, this.context, this.eventHandler, this.nsResolver, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition, this.schemaSet);
					invalidElementInContext = true;
					this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				}
			}
			return obj;
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x0010BE90 File Offset: 0x0010A090
		private XmlSchemaElement GetSubstitutionGroupHead(XmlQualifiedName member)
		{
			XmlSchemaElement element = this.compiledSchemaInfo.GetElement(member);
			if (element != null)
			{
				XmlQualifiedName substitutionGroup = element.SubstitutionGroup;
				if (!substitutionGroup.IsEmpty)
				{
					XmlSchemaElement element2 = this.compiledSchemaInfo.GetElement(substitutionGroup);
					if (element2 != null)
					{
						if ((element2.BlockResolved & XmlSchemaDerivationMethod.Substitution) != XmlSchemaDerivationMethod.Empty)
						{
							this.SendValidationEvent("Element '{0}' cannot substitute in place of head element '{1}' because it has block='substitution'.", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						if (!XmlSchemaType.IsDerivedFrom(element.ElementSchemaType, element2.ElementSchemaType, element2.BlockResolved))
						{
							this.SendValidationEvent("Member element {0}'s type cannot be derived by restriction or extension from head element {1}'s type, because it has block='restriction' or 'extension'.", new string[]
							{
								member.ToString(),
								substitutionGroup.ToString()
							});
							return null;
						}
						return element2;
					}
				}
			}
			return null;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x0010BF40 File Offset: 0x0010A140
		private object ValidateAtomicValue(string stringValue, out XmlSchemaSimpleType memberType)
		{
			object obj = null;
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (!this.context.IsNill)
			{
				if (stringValue.Length == 0 && elementDecl.DefaultValueTyped != null)
				{
					SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
					if (elementDeclBeforeXsi != null && elementDeclBeforeXsi != elementDecl)
					{
						if (elementDecl.Datatype.TryParseValue(elementDecl.DefaultValueRaw, this.nameTable, this.nsResolver, out obj) != null)
						{
							this.SendValidationEvent("The default value '{0}' of element '{1}' is invalid according to the type specified by xsi:type.", new string[]
							{
								elementDecl.DefaultValueRaw,
								XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
							});
						}
						else
						{
							this.context.IsDefault = true;
						}
					}
					else
					{
						this.context.IsDefault = true;
						obj = elementDecl.DefaultValueTyped;
					}
				}
				else
				{
					obj = this.CheckElementValue(stringValue);
				}
				XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
				XmlSchemaDatatype xmlSchemaDatatype = elementDecl.Datatype;
				if (xsdSimpleValue != null)
				{
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					xmlSchemaDatatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(xmlSchemaDatatype, obj, false);
			}
			return obj;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x0010C054 File Offset: 0x0010A254
		private object ValidateAtomicValue(object parsedValue, out XmlSchemaSimpleType memberType)
		{
			memberType = null;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			object obj = null;
			if (!this.context.IsNill)
			{
				SchemaDeclBase schemaDeclBase = elementDecl;
				XmlSchemaDatatype xmlSchemaDatatype = elementDecl.Datatype;
				Exception ex = xmlSchemaDatatype.TryParseValue(parsedValue, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					string text = parsedValue as string;
					if (text == null)
					{
						text = XmlSchemaDatatype.ConcatenatedToString(parsedValue);
					}
					this.SendValidationEvent("The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
					{
						XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
						text,
						this.GetTypeName(schemaDeclBase),
						ex.Message
					}, ex);
					return null;
				}
				if (!schemaDeclBase.CheckValue(obj))
				{
					this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.Union)
				{
					XsdSimpleValue xsdSimpleValue = obj as XsdSimpleValue;
					memberType = xsdSimpleValue.XmlType;
					obj = xsdSimpleValue.TypedValue;
					xmlSchemaDatatype = memberType.Datatype;
				}
				this.CheckTokenizedTypes(xmlSchemaDatatype, obj, false);
			}
			return obj;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x0010C16C File Offset: 0x0010A36C
		private string GetTypeName(SchemaDeclBase decl)
		{
			string text = decl.SchemaType.QualifiedName.ToString();
			if (text.Length == 0)
			{
				text = decl.Datatype.TypeCodeString;
			}
			return text;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0010C1A0 File Offset: 0x0010A3A0
		private void SaveTextValue(object value)
		{
			string text = value.ToString();
			this.textValue.Append(text);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0010C1C4 File Offset: 0x0010A3C4
		private void Push(XmlQualifiedName elementName)
		{
			this.context = (ValidationState)this.validationStack.Push();
			if (this.context == null)
			{
				this.context = new ValidationState();
				this.validationStack.AddToTop(this.context);
			}
			this.context.LocalName = elementName.Name;
			this.context.Namespace = elementName.Namespace;
			this.context.HasMatched = false;
			this.context.IsNill = false;
			this.context.IsDefault = false;
			this.context.CheckRequiredAttribute = true;
			this.context.ValidationSkipped = false;
			this.context.Validity = XmlSchemaValidity.NotKnown;
			this.context.NeedValidateChildren = false;
			this.context.ProcessContents = this.processContents;
			this.context.ElementDeclBeforeXsi = null;
			this.context.Constr = null;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x0010C2AC File Offset: 0x0010A4AC
		private void Pop()
		{
			ValidationState validationState = (ValidationState)this.validationStack.Pop();
			if (this.startIDConstraint == this.validationStack.Length)
			{
				this.startIDConstraint = -1;
			}
			this.context = (ValidationState)this.validationStack.Peek();
			if (validationState.Validity == XmlSchemaValidity.Invalid)
			{
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (validationState.ValidationSkipped)
			{
				this.context.ValidationSkipped = true;
			}
			this.processContents = this.context.ProcessContents;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x0010C334 File Offset: 0x0010A534
		private void AddXsiAttributes(ArrayList attList)
		{
			XmlSchemaValidator.BuildXsiAttributes();
			if (this.attPresence[XmlSchemaValidator.xsiTypeSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiTypeSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNilSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNilSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiSLSO);
			}
			if (this.attPresence[XmlSchemaValidator.xsiNoNsSLSO.QualifiedName] == null)
			{
				attList.Add(XmlSchemaValidator.xsiNoNsSLSO);
			}
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x0010C3D4 File Offset: 0x0010A5D4
		private SchemaElementDecl FastGetElementDecl(XmlQualifiedName elementName, object particle)
		{
			SchemaElementDecl schemaElementDecl = null;
			if (particle != null)
			{
				XmlSchemaElement xmlSchemaElement = particle as XmlSchemaElement;
				if (xmlSchemaElement != null)
				{
					schemaElementDecl = xmlSchemaElement.ElementDecl;
				}
				else
				{
					XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle;
					this.processContents = xmlSchemaAny.ProcessContentsCorrect;
				}
			}
			if (schemaElementDecl == null && this.processContents != XmlSchemaContentProcessing.Skip)
			{
				if (this.isRoot && this.partialValidationType != null)
				{
					if (this.partialValidationType is XmlSchemaElement)
					{
						XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.partialValidationType;
						if (elementName.Equals(xmlSchemaElement2.QualifiedName))
						{
							schemaElementDecl = xmlSchemaElement2.ElementDecl;
						}
						else
						{
							this.SendValidationEvent("The element name '{0}' does not match the name '{1}' of the 'XmlSchemaElement' set as a partial validation type.", elementName.ToString(), xmlSchemaElement2.QualifiedName.ToString());
						}
					}
					else if (this.partialValidationType is XmlSchemaType)
					{
						schemaElementDecl = ((XmlSchemaType)this.partialValidationType).ElementDecl;
					}
					else
					{
						this.SendValidationEvent("If the partial validation type is 'XmlSchemaAttribute', the 'ValidateElement' method cannot be called.", string.Empty);
					}
				}
				else
				{
					schemaElementDecl = this.compiledSchemaInfo.GetElementDecl(elementName);
				}
			}
			return schemaElementDecl;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0010C4C4 File Offset: 0x0010A6C4
		private SchemaElementDecl CheckXsiTypeAndNil(SchemaElementDecl elementDecl, string xsiType, string xsiNil, ref bool declFound)
		{
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
			if (xsiType != null)
			{
				object obj = null;
				Exception ex = XmlSchemaValidator.dtQName.TryParseValue(xsiType, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					this.SendValidationEvent("The attribute '{0}' has an invalid value '{1}' according to its schema type '{2}' - {3}", new string[]
					{
						"type",
						xsiType,
						XmlSchemaValidator.dtQName.TypeCodeString,
						ex.Message
					}, ex);
				}
				else
				{
					xmlQualifiedName = obj as XmlQualifiedName;
				}
			}
			if (elementDecl != null)
			{
				if (elementDecl.IsNillable)
				{
					if (xsiNil != null)
					{
						this.context.IsNill = XmlConvert.ToBoolean(xsiNil);
						if (this.context.IsNill && elementDecl.Presence == SchemaDeclBase.Use.Fixed)
						{
							this.SendValidationEvent("There must be no fixed value when an attribute is 'xsi:nil' and has a value of 'true'.");
						}
					}
				}
				else if (xsiNil != null)
				{
					this.SendValidationEvent("If the 'nillable' attribute is false in the schema, the 'xsi:nil' attribute must not be present in the instance.");
				}
			}
			if (xmlQualifiedName.IsEmpty)
			{
				if (elementDecl != null && elementDecl.IsAbstract)
				{
					this.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					elementDecl = null;
				}
			}
			else
			{
				SchemaElementDecl schemaElementDecl = this.compiledSchemaInfo.GetTypeDecl(xmlQualifiedName);
				XmlSeverityType xmlSeverityType = XmlSeverityType.Warning;
				if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
				{
					xmlSeverityType = XmlSeverityType.Error;
				}
				if (schemaElementDecl == null && xmlQualifiedName.Namespace == this.NsXs)
				{
					XmlSchemaType xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromXsdType(xmlQualifiedName);
					if (xmlSchemaType == null)
					{
						xmlSchemaType = XmlSchemaType.GetBuiltInComplexType(xmlQualifiedName);
					}
					if (xmlSchemaType != null)
					{
						schemaElementDecl = xmlSchemaType.ElementDecl;
					}
				}
				if (schemaElementDecl == null)
				{
					this.SendValidationEvent("This is an invalid xsi:type '{0}'.", xmlQualifiedName.ToString(), xmlSeverityType);
					elementDecl = null;
				}
				else
				{
					declFound = true;
					if (schemaElementDecl.IsAbstract)
					{
						this.SendValidationEvent("The xsi:type '{0}' cannot be abstract.", xmlQualifiedName.ToString(), xmlSeverityType);
						elementDecl = null;
					}
					else if (elementDecl != null && !XmlSchemaType.IsDerivedFrom(schemaElementDecl.SchemaType, elementDecl.SchemaType, elementDecl.Block))
					{
						this.SendValidationEvent("The xsi:type attribute value '{0}' is not valid for the element '{1}', either because it is not a type validly derived from the type in the schema, or because it has xsi:type derivation blocked.", new string[]
						{
							xmlQualifiedName.ToString(),
							XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace)
						});
						elementDecl = null;
					}
					else
					{
						if (elementDecl != null)
						{
							schemaElementDecl = schemaElementDecl.Clone();
							schemaElementDecl.Constraints = elementDecl.Constraints;
							schemaElementDecl.DefaultValueRaw = elementDecl.DefaultValueRaw;
							schemaElementDecl.DefaultValueTyped = elementDecl.DefaultValueTyped;
							schemaElementDecl.Block = elementDecl.Block;
						}
						this.context.ElementDeclBeforeXsi = elementDecl;
						elementDecl = schemaElementDecl;
					}
				}
			}
			return elementDecl;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0010C710 File Offset: 0x0010A910
		private void ThrowDeclNotFoundWarningOrError(bool declFound)
		{
			if (declFound)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				return;
			}
			if (this.HasSchema && this.processContents == XmlSchemaContentProcessing.Strict)
			{
				this.processContents = (this.context.ProcessContents = XmlSchemaContentProcessing.Skip);
				this.context.NeedValidateChildren = false;
				this.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				return;
			}
			this.SendValidationEvent("Could not find schema information for the element '{0}'.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace), XmlSeverityType.Warning);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x0010C7C2 File Offset: 0x0010A9C2
		private void CheckElementProperties()
		{
			if (this.context.ElementDecl.IsAbstract)
			{
				this.SendValidationEvent("The element '{0}' is abstract or its type is abstract.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x0010C7FC File Offset: 0x0010A9FC
		private void ValidateStartElementIdentityConstraints()
		{
			if (this.ProcessIdentityConstraints && this.context.ElementDecl.Constraints != null)
			{
				this.AddIdentityConstraints();
			}
			if (this.HasIdentityConstraints)
			{
				this.ElementIdentityConstraints();
			}
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x0010C82C File Offset: 0x0010AA2C
		private SchemaAttDef CheckIsXmlAttribute(XmlQualifiedName attQName)
		{
			SchemaAttDef schemaAttDef = null;
			if (Ref.Equal(attQName.Namespace, this.NsXml) && (this.validationFlags & XmlSchemaValidationFlags.AllowXmlAttributes) != XmlSchemaValidationFlags.None)
			{
				if (!this.compiledSchemaInfo.Contains(this.NsXml))
				{
					this.AddXmlNamespaceSchema();
				}
				this.compiledSchemaInfo.AttributeDecls.TryGetValue(attQName, out schemaAttDef);
			}
			return schemaAttDef;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0010C888 File Offset: 0x0010AA88
		private void AddXmlNamespaceSchema()
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.Add(Preprocessor.GetBuildInSchema());
			xmlSchemaSet.Compile();
			this.schemaSet.Add(xmlSchemaSet);
			this.RecompileSchemaSet();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0010C8C0 File Offset: 0x0010AAC0
		internal object CheckMixedValueConstraint(string elementValue)
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (this.context.IsNill)
			{
				return null;
			}
			if (elementValue.Length == 0)
			{
				this.context.IsDefault = true;
				return elementDecl.DefaultValueTyped;
			}
			if (elementDecl.Presence == SchemaDeclBase.Use.Fixed && !elementValue.Equals(elementDecl.DefaultValueRaw))
			{
				this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", elementDecl.Name.ToString());
			}
			return elementValue;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x0010C934 File Offset: 0x0010AB34
		private void LoadSchema(string uri, string url)
		{
			XmlReader xmlReader = null;
			try
			{
				Uri uri2 = this.xmlResolver.ResolveUri(this.sourceUri, url);
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri2, null, null);
				XmlReaderSettings readerSettings = this.schemaSet.ReaderSettings;
				readerSettings.CloseInput = true;
				readerSettings.XmlResolver = this.xmlResolver;
				xmlReader = XmlReader.Create(stream, readerSettings, uri2.ToString());
				this.schemaSet.Add(uri, xmlReader, this.validatedNamespaces);
				while (xmlReader.Read())
				{
				}
			}
			catch (XmlSchemaException ex)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex.Message }, ex);
			}
			catch (Exception ex2)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex2.Message }, ex2, XmlSeverityType.Warning);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x0010CA2C File Offset: 0x0010AC2C
		internal void RecompileSchemaSet()
		{
			if (!this.schemaSet.IsCompiled)
			{
				try
				{
					this.schemaSet.Compile();
				}
				catch (XmlSchemaException ex)
				{
					this.SendValidationEvent(ex);
				}
			}
			this.compiledSchemaInfo = this.schemaSet.CompiledInfo;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x0010CA80 File Offset: 0x0010AC80
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name, bool attrValue)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.ProcessIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						if (attrValue)
						{
							this.attrValid = false;
						}
						this.SendValidationEvent("'{0}' is already used as an ID.", name);
						return;
					}
					if (this.IDs == null)
					{
						this.IDs = new Hashtable();
					}
					this.IDs.Add(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.ProcessIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				this.ProcessEntity(name);
				break;
			default:
				return;
			}
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x0010CB40 File Offset: 0x0010AD40
		private object CheckAttributeValue(object value, SchemaAttDef attdef)
		{
			object obj = null;
			XmlSchemaDatatype datatype = attdef.Datatype;
			string text = value as string;
			Exception ex;
			if (text != null)
			{
				ex = datatype.TryParseValue(text, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_0078;
				}
			}
			else
			{
				ex = datatype.TryParseValue(value, this.nameTable, this.nsResolver, out obj);
				if (ex != null)
				{
					goto IL_0078;
				}
			}
			if (!attdef.CheckValue(obj))
			{
				this.attrValid = false;
				this.SendValidationEvent("The value of the '{0}' attribute does not equal its fixed value.", attdef.Name.ToString());
			}
			return obj;
			IL_0078:
			this.attrValid = false;
			if (text == null)
			{
				text = XmlSchemaDatatype.ConcatenatedToString(value);
			}
			this.SendValidationEvent("The '{0}' attribute is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
			{
				attdef.Name.ToString(),
				text,
				this.GetTypeName(attdef),
				ex.Message
			}, ex);
			return null;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x0010CC10 File Offset: 0x0010AE10
		private object CheckElementValue(string stringValue)
		{
			object obj = null;
			SchemaDeclBase elementDecl = this.context.ElementDecl;
			Exception ex = elementDecl.Datatype.TryParseValue(stringValue, this.nameTable, this.nsResolver, out obj);
			if (ex != null)
			{
				this.SendValidationEvent("The '{0}' element is invalid - The value '{1}' is invalid according to its datatype '{2}' - {3}", new string[]
				{
					XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace),
					stringValue,
					this.GetTypeName(elementDecl),
					ex.Message
				}, ex);
				return null;
			}
			if (!elementDecl.CheckValue(obj))
			{
				this.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return obj;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0010CCC4 File Offset: 0x0010AEC4
		private void CheckTokenizedTypes(XmlSchemaDatatype dtype, object typedValue, bool attrValue)
		{
			if (typedValue == null)
			{
				return;
			}
			XmlTokenizedType tokenizedType = dtype.TokenizedType;
			if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
			{
				if (dtype.Variety == XmlSchemaDatatypeVariety.List)
				{
					string[] array = (string[])typedValue;
					for (int i = 0; i < array.Length; i++)
					{
						this.ProcessTokenizedType(dtype.TokenizedType, array[i], attrValue);
					}
					return;
				}
				this.ProcessTokenizedType(dtype.TokenizedType, (string)typedValue, attrValue);
			}
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0010CD2A File Offset: 0x0010AF2A
		private object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0010CD44 File Offset: 0x0010AF44
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					this.SendValidationEvent(new XmlSchemaValidationException("Reference to undeclared ID is '{0}'.", idRefNode.Id, this.sourceUriString, idRefNode.LineNo, idRefNode.LinePos), XmlSeverityType.Error);
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x0010CDA9 File Offset: 0x0010AFA9
		private bool HasIdentityConstraints
		{
			get
			{
				return this.ProcessIdentityConstraints && this.startIDConstraint != -1;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0010CDC1 File Offset: 0x0010AFC1
		internal bool ProcessIdentityConstraints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x0010CDCE File Offset: 0x0010AFCE
		internal bool ReportValidationWarnings
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ReportValidationWarnings) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x0010CDDB File Offset: 0x0010AFDB
		internal bool ProcessInlineSchema
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x0010CDE8 File Offset: 0x0010AFE8
		internal bool ProcessSchemaLocation
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x0010CDF5 File Offset: 0x0010AFF5
		internal bool ProcessSchemaHints
		{
			get
			{
				return (this.validationFlags & XmlSchemaValidationFlags.ProcessInlineSchema) != XmlSchemaValidationFlags.None || (this.validationFlags & XmlSchemaValidationFlags.ProcessSchemaLocation) > XmlSchemaValidationFlags.None;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0010CE10 File Offset: 0x0010B010
		private void CheckStateTransition(ValidatorState toState, string methodName)
		{
			if (XmlSchemaValidator.ValidStates[(int)this.currentState, (int)toState])
			{
				this.currentState = toState;
				return;
			}
			if (this.currentState == ValidatorState.None)
			{
				throw new InvalidOperationException(Res.GetString("It is invalid to call the '{0}' method in the current state of the validator. The '{1}' method must be called before proceeding with validation.", new string[]
				{
					methodName,
					XmlSchemaValidator.MethodNames[1]
				}));
			}
			throw new InvalidOperationException(Res.GetString("The transition from the '{0}' method to the '{1}' method is not allowed.", new string[]
			{
				XmlSchemaValidator.MethodNames[(int)this.currentState],
				methodName
			}));
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0010CE8C File Offset: 0x0010B08C
		private void ClearPSVI()
		{
			if (this.textValue != null)
			{
				this.textValue.Length = 0;
			}
			this.attPresence.Clear();
			this.wildID = null;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0010CEB4 File Offset: 0x0010B0B4
		private void CheckRequiredAttributes(SchemaElementDecl currentElementDecl)
		{
			foreach (SchemaAttDef schemaAttDef in currentElementDecl.AttDefs.Values)
			{
				if (this.attPresence[schemaAttDef.Name] == null && (schemaAttDef.Presence == SchemaDeclBase.Use.Required || schemaAttDef.Presence == SchemaDeclBase.Use.RequiredFixed))
				{
					this.SendValidationEvent("The required attribute '{0}' is missing.", schemaAttDef.Name.ToString());
				}
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0010CF40 File Offset: 0x0010B140
		private XmlSchemaElement GetSchemaElement()
		{
			SchemaElementDecl elementDeclBeforeXsi = this.context.ElementDeclBeforeXsi;
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			if (elementDeclBeforeXsi != null && elementDeclBeforeXsi.SchemaElement != null)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)elementDeclBeforeXsi.SchemaElement.Clone(null);
				xmlSchemaElement.SchemaTypeName = XmlQualifiedName.Empty;
				xmlSchemaElement.SchemaType = elementDecl.SchemaType;
				xmlSchemaElement.SetElementType(elementDecl.SchemaType);
				xmlSchemaElement.ElementDecl = elementDecl;
				return xmlSchemaElement;
			}
			return elementDecl.SchemaElement;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0010CFB4 File Offset: 0x0010B1B4
		internal string GetDefaultAttributePrefix(string attributeNS)
		{
			IEnumerable<KeyValuePair<string, string>> namespacesInScope = this.nsResolver.GetNamespacesInScope(XmlNamespaceScope.All);
			string text = null;
			foreach (KeyValuePair<string, string> keyValuePair in namespacesInScope)
			{
				if (Ref.Equal(this.nameTable.Add(keyValuePair.Value), attributeNS))
				{
					text = keyValuePair.Key;
					if (text.Length != 0)
					{
						return text;
					}
				}
			}
			return text;
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0010D034 File Offset: 0x0010B234
		private void AddIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			this.context.Constr = new ConstraintStruct[elementDecl.Constraints.Length];
			int num = 0;
			for (int i = 0; i < elementDecl.Constraints.Length; i++)
			{
				this.context.Constr[num++] = new ConstraintStruct(elementDecl.Constraints[i]);
			}
			for (int j = 0; j < this.context.Constr.Length; j++)
			{
				if (this.context.Constr[j].constraint.Role == CompiledIdentityConstraint.ConstraintRole.Keyref)
				{
					bool flag = false;
					for (int k = this.validationStack.Length - 1; k >= ((this.startIDConstraint >= 0) ? this.startIDConstraint : (this.validationStack.Length - 1)); k--)
					{
						if (((ValidationState)this.validationStack[k]).Constr != null)
						{
							ConstraintStruct[] constr = ((ValidationState)this.validationStack[k]).Constr;
							for (int l = 0; l < constr.Length; l++)
							{
								if (constr[l].constraint.name == this.context.Constr[j].constraint.refer)
								{
									flag = true;
									if (constr[l].keyrefTable == null)
									{
										constr[l].keyrefTable = new Hashtable();
									}
									this.context.Constr[j].qualifiedTable = constr[l].keyrefTable;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					if (!flag)
					{
						this.SendValidationEvent("The Keyref '{0}' cannot find the referred key or unique in scope.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					}
				}
			}
			if (this.startIDConstraint == -1)
			{
				this.startIDConstraint = this.validationStack.Length - 1;
			}
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0010D210 File Offset: 0x0010B410
		private void ElementIdentityConstraints()
		{
			SchemaElementDecl elementDecl = this.context.ElementDecl;
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						if (constr[j].axisSelector.MoveToStartElement(localName, @namespace))
						{
							constr[j].axisSelector.PushKS(this.positionInfo.LineNumber, this.positionInfo.LinePosition);
						}
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToStartElement(localName, @namespace) && elementDecl != null)
							{
								if (elementDecl.Datatype == null || elementDecl.ContentValidator.ContentType == XmlSchemaContentType.Mixed)
								{
									this.SendValidationEvent("The field '{0}' is expecting an element or attribute with simple type or simple content.", localName);
								}
								else
								{
									locatedActiveAxis.isMatched = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0010D358 File Offset: 0x0010B558
		private void AttributeIdentityConstraints(string name, string ns, object obj, string sobj, XmlSchemaDatatype datatype)
		{
			for (int i = this.startIDConstraint; i < this.validationStack.Length; i++)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.MoveToAttribute(name, ns))
							{
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									this.SendValidationEvent("The field '{0}' is expecting at the most one value.", name);
								}
								else
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(obj, sobj, datatype);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0010D444 File Offset: 0x0010B644
		private void EndElementIdentityConstraints(object typedValue, string stringValue, XmlSchemaDatatype datatype)
		{
			string localName = this.context.LocalName;
			string @namespace = this.context.Namespace;
			for (int i = this.validationStack.Length - 1; i >= this.startIDConstraint; i--)
			{
				if (((ValidationState)this.validationStack[i]).Constr != null)
				{
					ConstraintStruct[] constr = ((ValidationState)this.validationStack[i]).Constr;
					for (int j = 0; j < constr.Length; j++)
					{
						for (int k = 0; k < constr[j].axisFields.Count; k++)
						{
							LocatedActiveAxis locatedActiveAxis = (LocatedActiveAxis)constr[j].axisFields[k];
							if (locatedActiveAxis.isMatched)
							{
								locatedActiveAxis.isMatched = false;
								if (locatedActiveAxis.Ks[locatedActiveAxis.Column] != null)
								{
									this.SendValidationEvent("The field '{0}' is expecting at the most one value.", localName);
								}
								else if (LocalAppContextSwitches.IgnoreEmptyKeySequences)
								{
									if (typedValue != null && stringValue.Length != 0)
									{
										locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
									}
								}
								else if (typedValue != null)
								{
									locatedActiveAxis.Ks[locatedActiveAxis.Column] = new TypedObject(typedValue, stringValue, datatype);
								}
							}
							locatedActiveAxis.EndElement(localName, @namespace);
						}
						if (constr[j].axisSelector.EndElement(localName, @namespace))
						{
							KeySequence keySequence = constr[j].axisSelector.PopKS();
							switch (constr[j].constraint.Role)
							{
							case CompiledIdentityConstraint.ConstraintRole.Unique:
								if (keySequence.IsQualified())
								{
									if (constr[j].qualifiedTable.Contains(keySequence))
									{
										this.SendValidationEvent(new XmlSchemaValidationException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
										{
											keySequence.ToString(),
											constr[j].constraint.name.ToString()
										}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
									}
									else
									{
										constr[j].qualifiedTable.Add(keySequence, keySequence);
									}
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Key:
								if (!keySequence.IsQualified())
								{
									this.SendValidationEvent(new XmlSchemaValidationException("The identity constraint '{0}' validation has failed. Either a key is missing or the existing key has an empty node.", constr[j].constraint.name.ToString(), this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
								}
								else if (constr[j].qualifiedTable.Contains(keySequence))
								{
									this.SendValidationEvent(new XmlSchemaValidationException("There is a duplicate key sequence '{0}' for the '{1}' key or unique identity constraint.", new string[]
									{
										keySequence.ToString(),
										constr[j].constraint.name.ToString()
									}, this.sourceUriString, keySequence.PosLine, keySequence.PosCol));
								}
								else
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							case CompiledIdentityConstraint.ConstraintRole.Keyref:
								if (constr[j].qualifiedTable != null && keySequence.IsQualified() && !constr[j].qualifiedTable.Contains(keySequence))
								{
									constr[j].qualifiedTable.Add(keySequence, keySequence);
								}
								break;
							}
						}
					}
				}
			}
			ConstraintStruct[] constr2 = ((ValidationState)this.validationStack[this.validationStack.Length - 1]).Constr;
			if (constr2 != null)
			{
				for (int l = 0; l < constr2.Length; l++)
				{
					if (constr2[l].constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref && constr2[l].keyrefTable != null)
					{
						foreach (object obj in constr2[l].keyrefTable.Keys)
						{
							KeySequence keySequence2 = (KeySequence)obj;
							if (!constr2[l].qualifiedTable.Contains(keySequence2))
							{
								this.SendValidationEvent(new XmlSchemaValidationException("The key sequence '{0}' in '{1}' Keyref fails to refer to some key.", new string[]
								{
									keySequence2.ToString(),
									constr2[l].constraint.name.ToString()
								}, this.sourceUriString, keySequence2.PosLine, keySequence2.PosCol));
							}
						}
					}
				}
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0010D880 File Offset: 0x0010BA80
		private static void BuildXsiAttributes()
		{
			if (XmlSchemaValidator.xsiTypeSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "type";
				xmlSchemaAttribute.SetQualifiedName(new XmlQualifiedName("type", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiTypeSO, xmlSchemaAttribute, null);
			}
			if (XmlSchemaValidator.xsiNilSO == null)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "nil";
				xmlSchemaAttribute2.SetQualifiedName(new XmlQualifiedName("nil", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute2.SetAttributeType(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Boolean));
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNilSO, xmlSchemaAttribute2, null);
			}
			if (XmlSchemaValidator.xsiSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute3 = new XmlSchemaAttribute();
				xmlSchemaAttribute3.Name = "schemaLocation";
				xmlSchemaAttribute3.SetQualifiedName(new XmlQualifiedName("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute3.SetAttributeType(builtInSimpleType);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiSLSO, xmlSchemaAttribute3, null);
			}
			if (XmlSchemaValidator.xsiNoNsSLSO == null)
			{
				XmlSchemaSimpleType builtInSimpleType2 = XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String);
				XmlSchemaAttribute xmlSchemaAttribute4 = new XmlSchemaAttribute();
				xmlSchemaAttribute4.Name = "noNamespaceSchemaLocation";
				xmlSchemaAttribute4.SetQualifiedName(new XmlQualifiedName("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance"));
				xmlSchemaAttribute4.SetAttributeType(builtInSimpleType2);
				Interlocked.CompareExchange<XmlSchemaAttribute>(ref XmlSchemaValidator.xsiNoNsSLSO, xmlSchemaAttribute4, null);
			}
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x0010D9B4 File Offset: 0x0010BBB4
		internal static void ElementValidationError(XmlQualifiedName name, ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			if (context.ElementDecl != null)
			{
				ContentValidator contentValidator = context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly || (contentType == XmlSchemaContentType.Mixed && contentValidator != ContentValidator.Mixed && contentValidator != ContentValidator.Any))
				{
					bool flag = schemaSet != null;
					ArrayList arrayList;
					if (flag)
					{
						arrayList = contentValidator.ExpectedParticles(context, false, schemaSet);
					}
					else
					{
						arrayList = contentValidator.ExpectedElements(context, false);
					}
					if (arrayList == null || arrayList.Count == 0)
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1} - {2}", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					else
					{
						if (context.TooComplex)
						{
							XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}. List of possible elements expected: {2}. {3}", new string[]
							{
								XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
								XmlSchemaValidator.BuildElementName(name),
								XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
								Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
							}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
							return;
						}
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has invalid child element {1}. List of possible elements expected: {2}.", new string[]
						{
							XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
							XmlSchemaValidator.BuildElementName(name),
							XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
				}
				else
				{
					if (contentType == XmlSchemaContentType.Empty)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element '{0}' cannot contain child element '{1}' because the parent element's content model is empty.", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
						return;
					}
					if (!contentValidator.IsOpen)
					{
						XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element '{0}' cannot contain child element '{1}' because the parent element's content model is text only.", new string[]
						{
							XmlSchemaValidator.QNameString(context.LocalName, context.Namespace),
							name.ToString()
						}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
					}
				}
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0010DBE0 File Offset: 0x0010BDE0
		internal static void CompleteValidationError(ValidationState context, ValidationEventHandler eventHandler, object sender, string sourceUri, int lineNo, int linePos, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = null;
			bool flag = schemaSet != null;
			if (context.ElementDecl != null)
			{
				if (flag)
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedParticles(context, true, schemaSet);
				}
				else
				{
					arrayList = context.ElementDecl.ContentValidator.ExpectedElements(context, true);
				}
			}
			if (arrayList == null || arrayList.Count == 0)
			{
				if (context.TooComplex)
				{
					XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content - {2}", new string[]
					{
						XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
						Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
					}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				}
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content.", XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace), sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			if (context.TooComplex)
			{
				XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content. List of possible elements expected: {1}. {2}", new string[]
				{
					XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
					XmlSchemaValidator.PrintExpectedElements(arrayList, flag),
					Res.GetString("Content model validation resulted in a large number of states, possibly due to large occurrence ranges. Therefore, content model may not be validated accurately.")
				}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
				return;
			}
			XmlSchemaValidator.SendValidationEvent(eventHandler, sender, new XmlSchemaValidationException("The element {0} has incomplete content. List of possible elements expected: {1}.", new string[]
			{
				XmlSchemaValidator.BuildElementName(context.LocalName, context.Namespace),
				XmlSchemaValidator.PrintExpectedElements(arrayList, flag)
			}, sourceUri, lineNo, linePos), XmlSeverityType.Error);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0010DD30 File Offset: 0x0010BF30
		internal static string PrintExpectedElements(ArrayList expected, bool getParticles)
		{
			if (getParticles)
			{
				string @string = Res.GetString("{0}as well as", new string[] { " " });
				XmlSchemaParticle xmlSchemaParticle = null;
				ArrayList arrayList = new ArrayList();
				StringBuilder stringBuilder = new StringBuilder();
				if (expected.Count == 1)
				{
					xmlSchemaParticle = expected[0] as XmlSchemaParticle;
				}
				else
				{
					for (int i = 1; i < expected.Count; i++)
					{
						XmlSchemaParticle xmlSchemaParticle2 = expected[i - 1] as XmlSchemaParticle;
						xmlSchemaParticle = expected[i] as XmlSchemaParticle;
						XmlQualifiedName qualifiedName = xmlSchemaParticle2.GetQualifiedName();
						if (qualifiedName.Namespace != xmlSchemaParticle.GetQualifiedName().Namespace)
						{
							arrayList.Add(qualifiedName);
							XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
							arrayList.Clear();
							stringBuilder.Append(@string);
						}
						else
						{
							arrayList.Add(qualifiedName);
						}
					}
				}
				arrayList.Add(xmlSchemaParticle.GetQualifiedName());
				XmlSchemaValidator.PrintNamesWithNS(arrayList, stringBuilder);
				return stringBuilder.ToString();
			}
			return XmlSchemaValidator.PrintNames(expected);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0010DE20 File Offset: 0x0010C020
		private static string PrintNames(ArrayList expected)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("'");
			stringBuilder.Append(expected[0].ToString());
			for (int i = 1; i < expected.Count; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(expected[i].ToString());
			}
			stringBuilder.Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0010DE94 File Offset: 0x0010C094
		private static void PrintNamesWithNS(ArrayList expected, StringBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = expected[0] as XmlQualifiedName;
			if (expected.Count == 1)
			{
				if (xmlQualifiedName.Name == "*")
				{
					XmlSchemaValidator.EnumerateAny(builder, xmlQualifiedName.Namespace);
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("'{0}' in namespace '{1}'", new object[] { xmlQualifiedName.Name, xmlQualifiedName.Namespace }));
					return;
				}
				builder.Append(Res.GetString("'{0}'", new object[] { xmlQualifiedName.Name }));
				return;
			}
			else
			{
				bool flag = false;
				bool flag2 = true;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < expected.Count; i++)
				{
					xmlQualifiedName = expected[i] as XmlQualifiedName;
					if (xmlQualifiedName.Name == "*")
					{
						flag = true;
					}
					else
					{
						if (flag2)
						{
							flag2 = false;
						}
						else
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(xmlQualifiedName.Name);
					}
				}
				if (flag)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(Res.GetString("any element"));
					return;
				}
				if (xmlQualifiedName.Namespace.Length != 0)
				{
					builder.Append(Res.GetString("'{0}' in namespace '{1}'", new object[]
					{
						stringBuilder.ToString(),
						xmlQualifiedName.Namespace
					}));
					return;
				}
				builder.Append(Res.GetString("'{0}'", new object[] { stringBuilder.ToString() }));
				return;
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x0010E00C File Offset: 0x0010C20C
		private static void EnumerateAny(StringBuilder builder, string namespaces)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (namespaces == "##any" || namespaces == "##other")
			{
				stringBuilder.Append(namespaces);
			}
			else
			{
				string[] array = XmlConvert.SplitString(namespaces);
				stringBuilder.Append(array[0]);
				for (int i = 1; i < array.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(array[i]);
				}
			}
			builder.Append(Res.GetString("any element in namespace '{0}'", new object[] { stringBuilder.ToString() }));
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x0010E098 File Offset: 0x0010C298
		internal static string QNameString(string localName, string ns)
		{
			if (ns.Length == 0)
			{
				return localName;
			}
			return ns + ":" + localName;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0010E0B0 File Offset: 0x0010C2B0
		internal static string BuildElementName(XmlQualifiedName qname)
		{
			return XmlSchemaValidator.BuildElementName(qname.Name, qname.Namespace);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0010E0C3 File Offset: 0x0010C2C3
		internal static string BuildElementName(string localName, string ns)
		{
			if (ns.Length != 0)
			{
				return Res.GetString("'{0}' in namespace '{1}'", new object[] { localName, ns });
			}
			return Res.GetString("'{0}'", new object[] { localName });
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0010E0FC File Offset: 0x0010C2FC
		private void ProcessEntity(string name)
		{
			if (!this.checkEntity)
			{
				return;
			}
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdSchemaInfo != null)
			{
				dtdEntityInfo = this.dtdSchemaInfo.LookupEntity(name);
			}
			if (dtdEntityInfo == null)
			{
				this.SendValidationEvent("Reference to an undeclared entity, '{0}'.", name);
				return;
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				this.SendValidationEvent("Reference to an unparsed entity, '{0}'.", name);
			}
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0010E14D File Offset: 0x0010C34D
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0010E15B File Offset: 0x0010C35B
		private void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0010E186 File Offset: 0x0010C386
		private void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, arg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0010E1B1 File Offset: 0x0010C3B1
		private void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, new string[] { arg1, arg2 }, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0010E1E9 File Offset: 0x0010C3E9
		private void SendValidationEvent(string code, string[] args, Exception innerException, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0010E224 File Offset: 0x0010C424
		private void SendValidationEvent(string code, string[] args, Exception innerException)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(code, args, innerException, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0010E251 File Offset: 0x0010C451
		private void SendValidationEvent(XmlSchemaValidationException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0010E25B File Offset: 0x0010C45B
		private void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(new XmlSchemaValidationException(e.GetRes, e.Args, e.SourceUri, e.LineNumber, e.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0010E287 File Offset: 0x0010C487
		private void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			if (severity != XmlSeverityType.Warning || this.ReportValidationWarnings)
			{
				this.SendValidationEvent(new XmlSchemaValidationException(code, msg, this.sourceUriString, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x0010E2C0 File Offset: 0x0010C4C0
		private void SendValidationEvent(XmlSchemaValidationException e, XmlSeverityType severity)
		{
			bool flag = false;
			if (severity == XmlSeverityType.Error)
			{
				flag = true;
				this.context.Validity = XmlSchemaValidity.Invalid;
			}
			if (!flag)
			{
				if (this.ReportValidationWarnings && this.eventHandler != null)
				{
					this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				}
				return;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(this.validationEventSender, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0010E32E File Offset: 0x0010C52E
		internal static void SendValidationEvent(ValidationEventHandler eventHandler, object sender, XmlSchemaValidationException e, XmlSeverityType severity)
		{
			if (eventHandler != null)
			{
				eventHandler(sender, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x04001E51 RID: 7761
		private XmlSchemaSet schemaSet;

		// Token: 0x04001E52 RID: 7762
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x04001E53 RID: 7763
		private int startIDConstraint = -1;

		// Token: 0x04001E54 RID: 7764
		private const int STACK_INCREMENT = 10;

		// Token: 0x04001E55 RID: 7765
		private bool isRoot;

		// Token: 0x04001E56 RID: 7766
		private bool rootHasSchema;

		// Token: 0x04001E57 RID: 7767
		private bool attrValid;

		// Token: 0x04001E58 RID: 7768
		private bool checkEntity;

		// Token: 0x04001E59 RID: 7769
		private SchemaInfo compiledSchemaInfo;

		// Token: 0x04001E5A RID: 7770
		private IDtdInfo dtdSchemaInfo;

		// Token: 0x04001E5B RID: 7771
		private Hashtable validatedNamespaces;

		// Token: 0x04001E5C RID: 7772
		private HWStack validationStack;

		// Token: 0x04001E5D RID: 7773
		private ValidationState context;

		// Token: 0x04001E5E RID: 7774
		private ValidatorState currentState;

		// Token: 0x04001E5F RID: 7775
		private Hashtable attPresence;

		// Token: 0x04001E60 RID: 7776
		private SchemaAttDef wildID;

		// Token: 0x04001E61 RID: 7777
		private Hashtable IDs;

		// Token: 0x04001E62 RID: 7778
		private IdRefNode idRefListHead;

		// Token: 0x04001E63 RID: 7779
		private XmlQualifiedName contextQName;

		// Token: 0x04001E64 RID: 7780
		private string NsXs;

		// Token: 0x04001E65 RID: 7781
		private string NsXsi;

		// Token: 0x04001E66 RID: 7782
		private string NsXmlNs;

		// Token: 0x04001E67 RID: 7783
		private string NsXml;

		// Token: 0x04001E68 RID: 7784
		private XmlSchemaObject partialValidationType;

		// Token: 0x04001E69 RID: 7785
		private StringBuilder textValue;

		// Token: 0x04001E6A RID: 7786
		private ValidationEventHandler eventHandler;

		// Token: 0x04001E6B RID: 7787
		private object validationEventSender;

		// Token: 0x04001E6C RID: 7788
		private XmlNameTable nameTable;

		// Token: 0x04001E6D RID: 7789
		private IXmlLineInfo positionInfo;

		// Token: 0x04001E6E RID: 7790
		private IXmlLineInfo dummyPositionInfo;

		// Token: 0x04001E6F RID: 7791
		private XmlResolver xmlResolver;

		// Token: 0x04001E70 RID: 7792
		private Uri sourceUri;

		// Token: 0x04001E71 RID: 7793
		private string sourceUriString;

		// Token: 0x04001E72 RID: 7794
		private IXmlNamespaceResolver nsResolver;

		// Token: 0x04001E73 RID: 7795
		private XmlSchemaContentProcessing processContents = XmlSchemaContentProcessing.Strict;

		// Token: 0x04001E74 RID: 7796
		private static XmlSchemaAttribute xsiTypeSO;

		// Token: 0x04001E75 RID: 7797
		private static XmlSchemaAttribute xsiNilSO;

		// Token: 0x04001E76 RID: 7798
		private static XmlSchemaAttribute xsiSLSO;

		// Token: 0x04001E77 RID: 7799
		private static XmlSchemaAttribute xsiNoNsSLSO;

		// Token: 0x04001E78 RID: 7800
		private string xsiTypeString;

		// Token: 0x04001E79 RID: 7801
		private string xsiNilString;

		// Token: 0x04001E7A RID: 7802
		private string xsiSchemaLocationString;

		// Token: 0x04001E7B RID: 7803
		private string xsiNoNamespaceSchemaLocationString;

		// Token: 0x04001E7C RID: 7804
		private static readonly XmlSchemaDatatype dtQName = XmlSchemaDatatype.FromXmlTokenizedTypeXsd(XmlTokenizedType.QName);

		// Token: 0x04001E7D RID: 7805
		private static readonly XmlSchemaDatatype dtCDATA = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);

		// Token: 0x04001E7E RID: 7806
		private static readonly XmlSchemaDatatype dtStringArray = XmlSchemaValidator.dtCDATA.DeriveByList(null);

		// Token: 0x04001E7F RID: 7807
		private const string Quote = "'";

		// Token: 0x04001E80 RID: 7808
		private static XmlSchemaParticle[] EmptyParticleArray = new XmlSchemaParticle[0];

		// Token: 0x04001E81 RID: 7809
		private static XmlSchemaAttribute[] EmptyAttributeArray = new XmlSchemaAttribute[0];

		// Token: 0x04001E82 RID: 7810
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04001E83 RID: 7811
		internal static bool[,] ValidStates = new bool[,]
		{
			{
				true, true, false, false, false, false, false, false, false, false,
				false, false
			},
			{
				false, true, true, true, true, false, false, false, false, false,
				false, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, true
			},
			{
				false, false, false, true, false, true, true, false, false, true,
				true, false
			},
			{
				false, false, false, false, false, true, true, false, false, true,
				true, false
			},
			{
				false, false, false, false, true, false, false, true, true, true,
				true, false
			},
			{
				false, false, false, false, true, false, false, true, true, true,
				true, false
			},
			{
				false, false, false, false, true, false, false, true, true, true,
				true, false
			},
			{
				false, false, false, true, true, false, false, true, true, true,
				true, true
			},
			{
				false, false, false, true, true, false, false, true, true, true,
				true, true
			},
			{
				false, true, false, false, false, false, false, false, false, false,
				false, false
			}
		};

		// Token: 0x04001E84 RID: 7812
		private static string[] MethodNames = new string[]
		{
			"None", "Initialize", "top-level ValidateAttribute", "top-level ValidateText or ValidateWhitespace", "ValidateElement", "ValidateAttribute", "ValidateEndOfAttributes", "ValidateText", "ValidateWhitespace", "ValidateEndElement",
			"SkipToEndElement", "EndValidation"
		};
	}
}
