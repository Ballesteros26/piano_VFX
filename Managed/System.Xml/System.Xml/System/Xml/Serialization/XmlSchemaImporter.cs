using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	/// <summary>Generates internal mappings to .NET Framework types for XML schema element declarations, including literal XSD message parts in a WSDL document. </summary>
	// Token: 0x02000342 RID: 834
	public class XmlSchemaImporter : SchemaImporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class, taking a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects representing the XML schemas used by SOAP literal messages defined in a WSDL document. </summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</param>
		// Token: 0x06002022 RID: 8226 RVA: 0x000A2683 File Offset: 0x000A0883
		public XmlSchemaImporter(XmlSchemas schemas)
			: base(schemas, CodeGenerationOptions.GenerateProperties, null, new ImportContext())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class, taking a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects that represents the XML schemas used by SOAP literal messages, plus classes being generated for bindings defined in a Web Services Description Language (WSDL) document. </summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Serialization.XmlSchemas" /> object.</param>
		/// <param name="typeIdentifiers">A <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that specifies a collection of classes being generated for bindings defined in a WSDL document.</param>
		// Token: 0x06002023 RID: 8227 RVA: 0x000A2693 File Offset: 0x000A0893
		public XmlSchemaImporter(XmlSchemas schemas, CodeIdentifiers typeIdentifiers)
			: base(schemas, CodeGenerationOptions.GenerateProperties, null, new ImportContext(typeIdentifiers, false))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class, taking a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects that represents the XML schemas used by SOAP literal messages, plus classes being generated for bindings defined in a WSDL document, and a <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration value.</summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</param>
		/// <param name="typeIdentifiers">A <see cref="T:System.Xml.Serialization.CodeIdentifiers" /> object that specifies a collection of classes being generated for bindings defined in a WSDL document.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> values that specifies the options to use when generating .NET Framework types for a Web service.</param>
		// Token: 0x06002024 RID: 8228 RVA: 0x000A26A5 File Offset: 0x000A08A5
		public XmlSchemaImporter(XmlSchemas schemas, CodeIdentifiers typeIdentifiers, CodeGenerationOptions options)
			: base(schemas, options, null, new ImportContext(typeIdentifiers, false))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class for a collection of XML schemas, using the specified code generation options and import context.</summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</param>
		/// <param name="options">A <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> enumeration that specifies code generation options.</param>
		/// <param name="context">A <see cref="T:System.Xml.Serialization.ImportContext" /> instance that specifies the import context.</param>
		// Token: 0x06002025 RID: 8229 RVA: 0x000A26B7 File Offset: 0x000A08B7
		public XmlSchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, ImportContext context)
			: base(schemas, options, null, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" /> class. </summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</param>
		/// <param name="options">A bitwise combination of the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> values that specifies the options to use when generating .NET Framework types for a Web service.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> used to generate the serialization code.</param>
		/// <param name="context">A <see cref="T:System.Xml.Serialization.ImportContext" /> instance that specifies the import context.</param>
		// Token: 0x06002026 RID: 8230 RVA: 0x000A26C3 File Offset: 0x000A08C3
		public XmlSchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, CodeDomProvider codeProvider, ImportContext context)
			: base(schemas, options, codeProvider, context)
		{
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document. </summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> representing the.NET Framework type mapping information for an XML schema element.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the name of an element defined in an XML schema document.</param>
		/// <param name="baseType">A base type for the .NET Framework type that is generated to correspond to an XSD element's type.</param>
		// Token: 0x06002027 RID: 8231 RVA: 0x000B1405 File Offset: 0x000AF605
		public XmlTypeMapping ImportDerivedTypeMapping(XmlQualifiedName name, Type baseType)
		{
			return this.ImportDerivedTypeMapping(name, baseType, false);
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000B1410 File Offset: 0x000AF610
		internal bool GenerateOrder
		{
			get
			{
				return (base.Options & CodeGenerationOptions.GenerateOrder) > CodeGenerationOptions.None;
			}
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000B1420 File Offset: 0x000AF620
		internal TypeMapping GetDefaultMapping(TypeFlags flags)
		{
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			primitiveMapping.TypeDesc = base.Scope.GetTypeDesc("string", "http://www.w3.org/2001/XMLSchema", flags);
			primitiveMapping.TypeName = primitiveMapping.TypeDesc.DataType.Name;
			primitiveMapping.Namespace = "http://www.w3.org/2001/XMLSchema";
			return primitiveMapping;
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document or as a part in a WSDL document.</summary>
		/// <returns>The .NET Framework type mapping information for an XML schema element.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the name of an element defined in an XML schema document.</param>
		/// <param name="baseType">A base type for the .NET Framework type that is generated to correspond to an XSD element's type.</param>
		/// <param name="baseTypeCanBeIndirect">true to indicate that the type corresponding to an XSD element can indirectly inherit from the base type; otherwise, false.</param>
		// Token: 0x0600202A RID: 8234 RVA: 0x000B1470 File Offset: 0x000AF670
		public XmlTypeMapping ImportDerivedTypeMapping(XmlQualifiedName name, Type baseType, bool baseTypeCanBeIndirect)
		{
			ElementAccessor elementAccessor = this.ImportElement(name, typeof(TypeMapping), baseType);
			if (elementAccessor.Mapping is StructMapping)
			{
				base.MakeDerived((StructMapping)elementAccessor.Mapping, baseType, baseTypeCanBeIndirect);
			}
			else if (baseType != null)
			{
				if (!(elementAccessor.Mapping is ArrayMapping))
				{
					throw new InvalidOperationException(Res.GetString("Element '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.", new object[] { name.Name, name.Namespace, baseType.FullName }));
				}
				elementAccessor.Mapping = ((ArrayMapping)elementAccessor.Mapping).TopLevelMapping;
				base.MakeDerived((StructMapping)elementAccessor.Mapping, baseType, baseTypeCanBeIndirect);
			}
			return new XmlTypeMapping(base.Scope, elementAccessor);
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> object that describes a type mapping.</returns>
		/// <param name="typeName">A <see cref="T:System.Xml.XmlQualifiedName" /> that specifies an XML element.</param>
		// Token: 0x0600202B RID: 8235 RVA: 0x000B1530 File Offset: 0x000AF730
		public XmlTypeMapping ImportSchemaType(XmlQualifiedName typeName)
		{
			return this.ImportSchemaType(typeName, null, false);
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> object that describes a type mapping.</returns>
		/// <param name="typeName">A <see cref="T:System.Xml.XmlQualifiedName" /> that specifies an XML element.</param>
		/// <param name="baseType">A <see cref="T:System.Type" /> object that specifies a base type.</param>
		// Token: 0x0600202C RID: 8236 RVA: 0x000B153B File Offset: 0x000AF73B
		public XmlTypeMapping ImportSchemaType(XmlQualifiedName typeName, Type baseType)
		{
			return this.ImportSchemaType(typeName, baseType, false);
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document. </summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.XmlTypeMapping" /> object that describes a type mapping.</returns>
		/// <param name="typeName">A <see cref="T:System.Xml.XmlQualifiedName" /> that specifies an XML element.</param>
		/// <param name="baseType">A <see cref="T:System.Type" /> object that specifies a base type.</param>
		/// <param name="baseTypeCanBeIndirect">A <see cref="T:System.Boolean" /> value that specifies whether the generated type can indirectly inherit the <paramref name="baseType" />.</param>
		// Token: 0x0600202D RID: 8237 RVA: 0x000B1548 File Offset: 0x000AF748
		public XmlTypeMapping ImportSchemaType(XmlQualifiedName typeName, Type baseType, bool baseTypeCanBeIndirect)
		{
			TypeMapping typeMapping = this.ImportType(typeName, typeof(TypeMapping), baseType, TypeFlags.CanBeElementValue, true);
			typeMapping.ReferencedByElement = false;
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.IsTopLevelInSchema = true;
			elementAccessor.Name = typeName.Name;
			elementAccessor.Namespace = typeName.Namespace;
			elementAccessor.Mapping = typeMapping;
			if (typeMapping is SpecialMapping && ((SpecialMapping)typeMapping).NamedAny)
			{
				elementAccessor.Any = true;
			}
			elementAccessor.IsNullable = typeMapping.TypeDesc.IsNullable;
			elementAccessor.Form = XmlSchemaForm.Qualified;
			if (elementAccessor.Mapping is StructMapping)
			{
				base.MakeDerived((StructMapping)elementAccessor.Mapping, baseType, baseTypeCanBeIndirect);
			}
			else if (baseType != null)
			{
				if (!(elementAccessor.Mapping is ArrayMapping))
				{
					throw new InvalidOperationException(Res.GetString("Type '{0}' from namespace '{1}' is not a complex type and cannot be used as a {2}.", new object[] { typeName.Name, typeName.Namespace, baseType.FullName }));
				}
				elementAccessor.Mapping = ((ArrayMapping)elementAccessor.Mapping).TopLevelMapping;
				base.MakeDerived((StructMapping)elementAccessor.Mapping, baseType, baseTypeCanBeIndirect);
			}
			return new XmlTypeMapping(base.Scope, elementAccessor);
		}

		/// <summary>Generates internal type mapping information for an element defined in an XML schema document. </summary>
		/// <returns>The .NET Framework type mapping information for an XML schema element.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the name of an element defined in an XML schema document.</param>
		// Token: 0x0600202E RID: 8238 RVA: 0x000B1672 File Offset: 0x000AF872
		public XmlTypeMapping ImportTypeMapping(XmlQualifiedName name)
		{
			return this.ImportDerivedTypeMapping(name, null);
		}

		/// <summary>Generates internal type mapping information for a single element part of a literal-use SOAP message defined in a WSDL document. </summary>
		/// <returns>The .NET Framework type mapping for a WSDL message definition containing a single element part.</returns>
		/// <param name="name">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the name of the message part.</param>
		// Token: 0x0600202F RID: 8239 RVA: 0x000B167C File Offset: 0x000AF87C
		public XmlMembersMapping ImportMembersMapping(XmlQualifiedName name)
		{
			return new XmlMembersMapping(base.Scope, this.ImportElement(name, typeof(MembersMapping), null), XmlMappingAccess.Read | XmlMappingAccess.Write);
		}

		/// <summary>Generates internal type mapping information for a single, (SOAP) literal element part defined in a WSDL document.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> representing the .NET Framework type mapping for a single element part of a WSDL message definition.</returns>
		/// <param name="typeName">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the name of an element's type for which a .NET Framework type is generated.</param>
		/// <param name="elementName">The name of the part element in the WSDL document.</param>
		// Token: 0x06002030 RID: 8240 RVA: 0x000B169C File Offset: 0x000AF89C
		public XmlMembersMapping ImportAnyType(XmlQualifiedName typeName, string elementName)
		{
			MembersMapping membersMapping = this.ImportType(typeName, typeof(MembersMapping), null, TypeFlags.CanBeElementValue, true) as MembersMapping;
			if (membersMapping == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = elementName;
				xmlSchemaElement.SchemaTypeName = typeName;
				xmlSchemaSequence.Items.Add(xmlSchemaElement);
				membersMapping = this.ImportMembersType(xmlSchemaComplexType, typeName.Namespace, elementName);
			}
			if (membersMapping.Members.Length != 1 || !membersMapping.Members[0].Accessor.Any)
			{
				return null;
			}
			membersMapping.Members[0].Name = elementName;
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.Name = elementName;
			elementAccessor.Namespace = typeName.Namespace;
			elementAccessor.Mapping = membersMapping;
			elementAccessor.Any = true;
			XmlSchemaObject xmlSchemaObject = base.Schemas.SchemaSet.GlobalTypes[typeName];
			if (xmlSchemaObject != null)
			{
				XmlSchema xmlSchema = xmlSchemaObject.Parent as XmlSchema;
				if (xmlSchema != null)
				{
					elementAccessor.Form = ((xmlSchema.ElementFormDefault == XmlSchemaForm.None) ? XmlSchemaForm.Unqualified : xmlSchema.ElementFormDefault);
				}
			}
			return new XmlMembersMapping(base.Scope, elementAccessor, XmlMappingAccess.Read | XmlMappingAccess.Write);
		}

		/// <summary>Generates internal type mapping information for the element parts of a literal-use SOAP message defined in a WSDL document. </summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> that represents the .NET Framework type mappings for the element parts of a WSDL message definition.</returns>
		/// <param name="names">An array of type <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the names of the message parts.</param>
		// Token: 0x06002031 RID: 8241 RVA: 0x000B17B8 File Offset: 0x000AF9B8
		public XmlMembersMapping ImportMembersMapping(XmlQualifiedName[] names)
		{
			return this.ImportMembersMapping(names, null, false);
		}

		/// <summary>Generates internal type mapping information for the element parts of a literal-use SOAP message defined in a WSDL document.</summary>
		/// <returns>The .NET Framework type mappings for the element parts of a WSDL message definition.</returns>
		/// <param name="names">An array of type <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the names of the message parts.</param>
		/// <param name="baseType">A base type for all .NET Framework types that are generated to correspond to message parts.</param>
		/// <param name="baseTypeCanBeIndirect">true to indicate that the types corresponding to message parts can indirectly inherit from the base type; otherwise, false.</param>
		// Token: 0x06002032 RID: 8242 RVA: 0x000B17C4 File Offset: 0x000AF9C4
		public XmlMembersMapping ImportMembersMapping(XmlQualifiedName[] names, Type baseType, bool baseTypeCanBeIndirect)
		{
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.UseCamelCasing = true;
			MemberMapping[] array = new MemberMapping[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				XmlQualifiedName xmlQualifiedName = names[i];
				ElementAccessor elementAccessor = this.ImportElement(xmlQualifiedName, typeof(TypeMapping), baseType);
				if (baseType != null && elementAccessor.Mapping is StructMapping)
				{
					base.MakeDerived((StructMapping)elementAccessor.Mapping, baseType, baseTypeCanBeIndirect);
				}
				MemberMapping memberMapping = new MemberMapping();
				memberMapping.Name = CodeIdentifier.MakeValid(Accessor.UnescapeName(elementAccessor.Name));
				memberMapping.Name = codeIdentifiers.AddUnique(memberMapping.Name, memberMapping);
				memberMapping.TypeDesc = elementAccessor.Mapping.TypeDesc;
				memberMapping.Elements = new ElementAccessor[] { elementAccessor };
				array[i] = memberMapping;
			}
			MembersMapping membersMapping = new MembersMapping();
			membersMapping.HasWrapperElement = false;
			membersMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(object[]));
			membersMapping.Members = array;
			ElementAccessor elementAccessor2 = new ElementAccessor();
			elementAccessor2.Mapping = membersMapping;
			return new XmlMembersMapping(base.Scope, elementAccessor2, XmlMappingAccess.Read | XmlMappingAccess.Write);
		}

		/// <summary>Generates internal type mapping information for the element parts of a literal-use SOAP message defined in a WSDL document.</summary>
		/// <returns>A <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> that contains type mapping information.</returns>
		/// <param name="name">The name of the element for which to generate a mapping.</param>
		/// <param name="ns">The namespace of the element for which to generate a mapping.</param>
		/// <param name="members">An array of <see cref="T:System.Xml.Serialization.SoapSchemaMember" /> instances that specifies the members of the element for which to generate a mapping.</param>
		// Token: 0x06002033 RID: 8243 RVA: 0x000B18F0 File Offset: 0x000AFAF0
		public XmlMembersMapping ImportMembersMapping(string name, string ns, SoapSchemaMember[] members)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			foreach (SoapSchemaMember soapSchemaMember in members)
			{
				XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
				xmlSchemaElement.Name = soapSchemaMember.MemberName;
				xmlSchemaElement.SchemaTypeName = soapSchemaMember.MemberType;
				xmlSchemaSequence.Items.Add(xmlSchemaElement);
			}
			MembersMapping membersMapping = this.ImportMembersType(xmlSchemaComplexType, null, name);
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.Name = Accessor.EscapeName(name);
			elementAccessor.Namespace = ns;
			elementAccessor.Mapping = membersMapping;
			elementAccessor.IsNullable = false;
			elementAccessor.Form = XmlSchemaForm.Qualified;
			return new XmlMembersMapping(base.Scope, elementAccessor, XmlMappingAccess.Read | XmlMappingAccess.Write);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000B19A4 File Offset: 0x000AFBA4
		private ElementAccessor ImportElement(XmlQualifiedName name, Type desiredMappingType, Type baseType)
		{
			XmlSchemaElement xmlSchemaElement = this.FindElement(name);
			ElementAccessor elementAccessor = (ElementAccessor)base.ImportedElements[xmlSchemaElement];
			if (elementAccessor != null)
			{
				return elementAccessor;
			}
			elementAccessor = this.ImportElement(xmlSchemaElement, string.Empty, desiredMappingType, baseType, name.Namespace, true);
			ElementAccessor elementAccessor2 = (ElementAccessor)base.ImportedElements[xmlSchemaElement];
			if (elementAccessor2 != null)
			{
				return elementAccessor2;
			}
			base.ImportedElements.Add(xmlSchemaElement, elementAccessor);
			return elementAccessor;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000B1A0C File Offset: 0x000AFC0C
		private ElementAccessor ImportElement(XmlSchemaElement element, string identifier, Type desiredMappingType, Type baseType, string ns, bool topLevelElement)
		{
			if (!element.RefName.IsEmpty)
			{
				ElementAccessor elementAccessor = this.ImportElement(element.RefName, desiredMappingType, baseType);
				if (element.IsMultipleOccurrence && elementAccessor.Mapping is ArrayMapping)
				{
					ElementAccessor elementAccessor2 = elementAccessor.Clone();
					elementAccessor2.IsTopLevelInSchema = false;
					elementAccessor2.Mapping.ReferencedByElement = true;
					return elementAccessor2;
				}
				return elementAccessor;
			}
			else
			{
				if (element.Name.Length == 0)
				{
					XmlQualifiedName parentName = XmlSchemas.GetParentName(element);
					throw new InvalidOperationException(Res.GetString("This element has no name. Please review schema type '{0}' from namespace '{1}'.", new object[] { parentName.Name, parentName.Namespace }));
				}
				string text = Accessor.UnescapeName(element.Name);
				if (identifier.Length == 0)
				{
					identifier = CodeIdentifier.MakeValid(text);
				}
				else
				{
					identifier += CodeIdentifier.MakePascal(text);
				}
				TypeMapping typeMapping = this.ImportElementType(element, identifier, desiredMappingType, baseType, ns);
				ElementAccessor elementAccessor3 = new ElementAccessor();
				elementAccessor3.IsTopLevelInSchema = element.Parent is XmlSchema;
				elementAccessor3.Name = element.Name;
				elementAccessor3.Namespace = ns;
				elementAccessor3.Mapping = typeMapping;
				elementAccessor3.IsOptional = element.MinOccurs == 0m;
				if (element.DefaultValue != null)
				{
					elementAccessor3.Default = element.DefaultValue;
				}
				else if (element.FixedValue != null)
				{
					elementAccessor3.Default = element.FixedValue;
					elementAccessor3.IsFixed = true;
				}
				if (typeMapping is SpecialMapping && ((SpecialMapping)typeMapping).NamedAny)
				{
					elementAccessor3.Any = true;
				}
				elementAccessor3.IsNullable = element.IsNillable;
				if (topLevelElement)
				{
					elementAccessor3.Form = XmlSchemaForm.Qualified;
				}
				else
				{
					elementAccessor3.Form = this.ElementForm(ns, element);
				}
				return elementAccessor3;
			}
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000B1BA4 File Offset: 0x000AFDA4
		private TypeMapping ImportElementType(XmlSchemaElement element, string identifier, Type desiredMappingType, Type baseType, string ns)
		{
			TypeMapping typeMapping;
			if (!element.SchemaTypeName.IsEmpty)
			{
				typeMapping = this.ImportType(element.SchemaTypeName, desiredMappingType, baseType, TypeFlags.CanBeElementValue, false);
				if (!typeMapping.ReferencedByElement)
				{
					object obj = this.FindType(element.SchemaTypeName, TypeFlags.CanBeElementValue);
					XmlSchemaObject xmlSchemaObject = element;
					while (xmlSchemaObject.Parent != null && obj != xmlSchemaObject)
					{
						xmlSchemaObject = xmlSchemaObject.Parent;
					}
					typeMapping.ReferencedByElement = obj != xmlSchemaObject;
				}
			}
			else if (element.SchemaType != null)
			{
				if (element.SchemaType is XmlSchemaComplexType)
				{
					typeMapping = this.ImportType((XmlSchemaComplexType)element.SchemaType, ns, identifier, desiredMappingType, baseType, TypeFlags.CanBeElementValue);
				}
				else
				{
					typeMapping = this.ImportDataType((XmlSchemaSimpleType)element.SchemaType, ns, identifier, baseType, (TypeFlags)56, false);
				}
				typeMapping.ReferencedByElement = true;
			}
			else if (!element.SubstitutionGroup.IsEmpty)
			{
				typeMapping = this.ImportElementType(this.FindElement(element.SubstitutionGroup), identifier, desiredMappingType, baseType, ns);
			}
			else if (desiredMappingType == typeof(MembersMapping))
			{
				typeMapping = this.ImportMembersType(new XmlSchemaType(), ns, identifier);
			}
			else
			{
				typeMapping = base.ImportRootMapping();
			}
			if (!desiredMappingType.IsAssignableFrom(typeMapping.GetType()))
			{
				throw new InvalidOperationException(Res.GetString("The element, {0}, from namespace, {1}, was imported in two different contexts: ({2}, {3}).", new object[]
				{
					element.Name,
					ns,
					typeMapping.GetType().Name,
					desiredMappingType.Name
				}));
			}
			if (!typeMapping.TypeDesc.IsMappedType)
			{
				this.RunSchemaExtensions(typeMapping, element.SchemaTypeName, element.SchemaType, element, TypeFlags.CanBeElementValue);
			}
			return typeMapping;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000B1D28 File Offset: 0x000AFF28
		private void RunSchemaExtensions(TypeMapping mapping, XmlQualifiedName qname, XmlSchemaType type, XmlSchemaObject context, TypeFlags flags)
		{
			string text = null;
			SchemaImporterExtension schemaImporterExtension = null;
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			if (!qname.IsEmpty)
			{
				text = this.FindExtendedType(qname.Name, qname.Namespace, context, codeCompileUnit, codeNamespace, out schemaImporterExtension);
			}
			else if (type != null)
			{
				text = this.FindExtendedType(type, context, codeCompileUnit, codeNamespace, out schemaImporterExtension);
			}
			else if (context is XmlSchemaAny)
			{
				text = this.FindExtendedAnyElement((XmlSchemaAny)context, (flags & TypeFlags.CanBeTextValue) > TypeFlags.None, codeCompileUnit, codeNamespace, out schemaImporterExtension);
			}
			if (text != null && text.Length > 0)
			{
				text = text.Replace('+', '.');
				try
				{
					CodeGenerator.ValidateIdentifiers(new CodeTypeReference(text));
				}
				catch (ArgumentException)
				{
					if (qname.IsEmpty)
					{
						throw new InvalidOperationException(Res.GetString("Schema importer extension {0} returned invalid type information: '{1}' is not a valid type name.", new object[]
						{
							schemaImporterExtension.GetType().FullName,
							text
						}));
					}
					throw new InvalidOperationException(Res.GetString("Schema importer extension {0} returned invalid type information for xsd type {1} from namespace='{2}': '{3}' is not a valid type name.", new object[]
					{
						schemaImporterExtension.GetType().FullName,
						qname.Name,
						qname.Namespace,
						text
					}));
				}
				foreach (object obj in codeCompileUnit.Namespaces)
				{
					CodeGenerator.ValidateIdentifiers((CodeNamespace)obj);
				}
				mapping.TypeDesc = mapping.TypeDesc.CreateMappedTypeDesc(new MappedTypeDesc(text, qname.Name, qname.Namespace, type, context, schemaImporterExtension, codeNamespace, codeCompileUnit.ReferencedAssemblies));
				if (mapping is ArrayMapping)
				{
					StructMapping topLevelMapping = ((ArrayMapping)mapping).TopLevelMapping;
					topLevelMapping.TypeName = mapping.TypeName;
					topLevelMapping.TypeDesc = mapping.TypeDesc;
					return;
				}
				mapping.TypeName = (qname.IsEmpty ? null : text);
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000B1F08 File Offset: 0x000B0108
		private string GenerateUniqueTypeName(string desiredName, string ns)
		{
			int num = 1;
			string text = desiredName;
			for (;;)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(text, ns);
				if (base.Schemas.Find(xmlQualifiedName, typeof(XmlSchemaType)) == null)
				{
					break;
				}
				text = desiredName + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			text = CodeIdentifier.MakeValid(text);
			return base.TypeIdentifiers.AddUnique(text, text);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000B1F68 File Offset: 0x000B0168
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		internal override void ImportDerivedTypes(XmlQualifiedName baseName)
		{
			foreach (object obj in base.Schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (!base.Schemas.IsReference(xmlSchema) && !XmlSchemas.IsDataSet(xmlSchema))
				{
					XmlSchemas.Preprocess(xmlSchema);
					foreach (object obj2 in xmlSchema.SchemaTypes.Values)
					{
						if (obj2 is XmlSchemaType)
						{
							XmlSchemaType xmlSchemaType = (XmlSchemaType)obj2;
							if (xmlSchemaType.DerivedFrom == baseName && base.TypesInUse[xmlSchemaType.Name, xmlSchema.TargetNamespace] == null)
							{
								this.ImportType(xmlSchemaType.QualifiedName, typeof(TypeMapping), null, TypeFlags.CanBeElementValue, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000B2080 File Offset: 0x000B0280
		private TypeMapping ImportType(XmlQualifiedName name, Type desiredMappingType, Type baseType, TypeFlags flags, bool addref)
		{
			if (name.Name == "anyType" && name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return base.ImportRootMapping();
			}
			object obj = this.FindType(name, flags);
			TypeMapping typeMapping = (TypeMapping)base.ImportedMappings[obj];
			if (typeMapping != null && desiredMappingType.IsAssignableFrom(typeMapping.GetType()))
			{
				return typeMapping;
			}
			if (addref)
			{
				base.AddReference(name, base.TypesInUse, "Type '{0}' from targetNamespace='{1}' has invalid definition: Circular type reference.");
			}
			if (obj is XmlSchemaComplexType)
			{
				typeMapping = this.ImportType((XmlSchemaComplexType)obj, name.Namespace, name.Name, desiredMappingType, baseType, flags);
			}
			else
			{
				if (!(obj is XmlSchemaSimpleType))
				{
					throw new InvalidOperationException(Res.GetString("Internal error."));
				}
				typeMapping = this.ImportDataType((XmlSchemaSimpleType)obj, name.Namespace, name.Name, baseType, flags, false);
			}
			if (addref && name.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				base.RemoveReference(name, base.TypesInUse);
			}
			return typeMapping;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000B2180 File Offset: 0x000B0380
		private TypeMapping ImportType(XmlSchemaComplexType type, string typeNs, string identifier, Type desiredMappingType, Type baseType, TypeFlags flags)
		{
			if (type.Redefined != null)
			{
				throw new NotSupportedException(Res.GetString("Cannot import schema for type '{0}' from namespace '{1}'. Redefine not supported.", new object[] { type.Name, typeNs }));
			}
			if (desiredMappingType == typeof(TypeMapping))
			{
				TypeMapping typeMapping = null;
				if (baseType == null)
				{
					typeMapping = this.ImportArrayMapping(type, identifier, typeNs, false) ?? this.ImportAnyMapping(type, identifier, typeNs, false);
				}
				if (typeMapping == null)
				{
					typeMapping = this.ImportStructType(type, typeNs, identifier, baseType, false);
					if (typeMapping != null && type.Name != null && type.Name.Length != 0)
					{
						this.ImportDerivedTypes(new XmlQualifiedName(identifier, typeNs));
					}
				}
				return typeMapping;
			}
			if (desiredMappingType == typeof(MembersMapping))
			{
				return this.ImportMembersType(type, typeNs, identifier);
			}
			throw new ArgumentException(Res.GetString("Internal error."), "desiredMappingType");
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000B2258 File Offset: 0x000B0458
		private MembersMapping ImportMembersType(XmlSchemaType type, string typeNs, string identifier)
		{
			if (!type.DerivedFrom.IsEmpty)
			{
				throw new InvalidOperationException(Res.GetString("These members may not be derived."));
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.UseCamelCasing = true;
			bool flag = false;
			MemberMapping[] array = this.ImportTypeMembers(type, typeNs, identifier, codeIdentifiers, new CodeIdentifiers(), new NameTable(), ref flag, false, false);
			return new MembersMapping
			{
				HasWrapperElement = true,
				TypeDesc = base.Scope.GetTypeDesc(typeof(object[])),
				Members = array
			};
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000B22D8 File Offset: 0x000B04D8
		private StructMapping ImportStructType(XmlSchemaType type, string typeNs, string identifier, Type baseType, bool arrayLike)
		{
			TypeDesc typeDesc = null;
			TypeMapping typeMapping = null;
			bool flag = false;
			if (!type.DerivedFrom.IsEmpty)
			{
				typeMapping = this.ImportType(type.DerivedFrom, typeof(TypeMapping), null, (TypeFlags)48, false);
				if (typeMapping is StructMapping)
				{
					typeDesc = ((StructMapping)typeMapping).TypeDesc;
				}
				else if (typeMapping is ArrayMapping)
				{
					typeMapping = ((ArrayMapping)typeMapping).TopLevelMapping;
					if (typeMapping != null)
					{
						typeMapping.ReferencedByTopLevelElement = false;
						typeMapping.ReferencedByElement = true;
						typeDesc = typeMapping.TypeDesc;
					}
				}
				else
				{
					typeMapping = null;
				}
			}
			if (typeDesc == null && baseType != null)
			{
				typeDesc = base.Scope.GetTypeDesc(baseType);
			}
			if (typeMapping == null)
			{
				typeMapping = base.GetRootMapping();
				flag = true;
			}
			Mapping mapping = (Mapping)base.ImportedMappings[type];
			if (mapping != null)
			{
				if (mapping is StructMapping)
				{
					return (StructMapping)mapping;
				}
				if (!arrayLike || !(mapping is ArrayMapping))
				{
					throw new InvalidOperationException(Res.GetString("The type '{0}' from namespace '{1}' was used in two different ways.", new object[]
					{
						type.QualifiedName.Name,
						type.QualifiedName.Namespace
					}));
				}
				ArrayMapping arrayMapping = (ArrayMapping)mapping;
				if (arrayMapping.TopLevelMapping != null)
				{
					return arrayMapping.TopLevelMapping;
				}
			}
			StructMapping structMapping = new StructMapping();
			structMapping.IsReference = base.Schemas.IsReference(type);
			TypeFlags typeFlags = TypeFlags.Reference;
			if (type is XmlSchemaComplexType && ((XmlSchemaComplexType)type).IsAbstract)
			{
				typeFlags |= TypeFlags.Abstract;
			}
			identifier = Accessor.UnescapeName(identifier);
			string text = ((type.Name == null || type.Name.Length == 0) ? this.GenerateUniqueTypeName(identifier, typeNs) : base.GenerateUniqueTypeName(identifier));
			structMapping.TypeDesc = new TypeDesc(text, text, TypeKind.Struct, typeDesc, typeFlags);
			structMapping.Namespace = typeNs;
			structMapping.TypeName = ((type.Name == null || type.Name.Length == 0) ? null : identifier);
			structMapping.BaseMapping = (StructMapping)typeMapping;
			if (!arrayLike)
			{
				base.ImportedMappings.Add(type, structMapping);
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			CodeIdentifiers codeIdentifiers2 = structMapping.BaseMapping.Scope.Clone();
			codeIdentifiers.AddReserved(text);
			codeIdentifiers2.AddReserved(text);
			base.AddReservedIdentifiersForDataBinding(codeIdentifiers);
			if (flag)
			{
				base.AddReservedIdentifiersForDataBinding(codeIdentifiers2);
			}
			bool flag2 = false;
			structMapping.Members = this.ImportTypeMembers(type, typeNs, identifier, codeIdentifiers, codeIdentifiers2, structMapping, ref flag2, true, true);
			if (!this.IsAllGroup(type))
			{
				if (flag2 && !this.GenerateOrder)
				{
					structMapping.SetSequence();
				}
				else if (this.GenerateOrder)
				{
					structMapping.IsSequence = true;
				}
			}
			for (int i = 0; i < structMapping.Members.Length; i++)
			{
				StructMapping structMapping2;
				MemberMapping memberMapping = ((StructMapping)typeMapping).FindDeclaringMapping(structMapping.Members[i], out structMapping2, structMapping.TypeName);
				if (memberMapping != null && memberMapping.TypeDesc != structMapping.Members[i].TypeDesc)
				{
					throw new InvalidOperationException(Res.GetString("Error: Type '{0}' could not be imported because it redefines inherited member '{1}' with a different type. '{1}' is declared as type '{3}' on '{0}', but as type '{2}' on base class '{4}'.", new object[]
					{
						type.Name,
						memberMapping.Name,
						memberMapping.TypeDesc.FullName,
						structMapping.Members[i].TypeDesc.FullName,
						structMapping2.TypeDesc.FullName
					}));
				}
			}
			structMapping.Scope = codeIdentifiers2;
			base.Scope.AddTypeMapping(structMapping);
			return structMapping;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000B2618 File Offset: 0x000B0818
		private bool IsAllGroup(XmlSchemaType type)
		{
			XmlSchemaImporter.TypeItems typeItems = this.GetTypeItems(type);
			return typeItems.Particle != null && typeItems.Particle is XmlSchemaAll;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000B2648 File Offset: 0x000B0848
		private StructMapping ImportStructDataType(XmlSchemaSimpleType dataType, string typeNs, string identifier, Type baseType)
		{
			identifier = Accessor.UnescapeName(identifier);
			string text = base.GenerateUniqueTypeName(identifier);
			StructMapping structMapping = new StructMapping();
			structMapping.IsReference = base.Schemas.IsReference(dataType);
			TypeFlags typeFlags = TypeFlags.Reference;
			TypeDesc typeDesc = base.Scope.GetTypeDesc(baseType);
			structMapping.TypeDesc = new TypeDesc(text, text, TypeKind.Struct, typeDesc, typeFlags);
			structMapping.Namespace = typeNs;
			structMapping.TypeName = identifier;
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			codeIdentifiers.AddReserved(text);
			base.AddReservedIdentifiersForDataBinding(codeIdentifiers);
			this.ImportTextMember(codeIdentifiers, new CodeIdentifiers(), null);
			structMapping.Members = (MemberMapping[])codeIdentifiers.ToArray(typeof(MemberMapping));
			structMapping.Scope = codeIdentifiers;
			base.Scope.AddTypeMapping(structMapping);
			return structMapping;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000B2704 File Offset: 0x000B0904
		private MemberMapping[] ImportTypeMembers(XmlSchemaType type, string typeNs, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, ref bool needExplicitOrder, bool order, bool allowUnboundedElements)
		{
			XmlSchemaImporter.TypeItems typeItems = this.GetTypeItems(type);
			bool flag = XmlSchemaImporter.IsMixed(type);
			if (flag)
			{
				XmlSchemaType xmlSchemaType = type;
				while (!xmlSchemaType.DerivedFrom.IsEmpty)
				{
					xmlSchemaType = this.FindType(xmlSchemaType.DerivedFrom, (TypeFlags)48);
					if (XmlSchemaImporter.IsMixed(xmlSchemaType))
					{
						flag = false;
						break;
					}
				}
			}
			if (typeItems.Particle != null)
			{
				this.ImportGroup(typeItems.Particle, identifier, members, membersScope, elementsScope, typeNs, flag, ref needExplicitOrder, order, typeItems.IsUnbounded, allowUnboundedElements);
			}
			for (int i = 0; i < typeItems.Attributes.Count; i++)
			{
				object obj = typeItems.Attributes[i];
				if (obj is XmlSchemaAttribute)
				{
					this.ImportAttributeMember((XmlSchemaAttribute)obj, identifier, members, membersScope, typeNs);
				}
				else if (obj is XmlSchemaAttributeGroupRef)
				{
					XmlQualifiedName refName = ((XmlSchemaAttributeGroupRef)obj).RefName;
					this.ImportAttributeGroupMembers(this.FindAttributeGroup(refName), identifier, members, membersScope, refName.Namespace);
				}
			}
			if (typeItems.AnyAttribute != null)
			{
				this.ImportAnyAttributeMember(typeItems.AnyAttribute, members, membersScope);
			}
			if (typeItems.baseSimpleType != null || (typeItems.Particle == null && flag))
			{
				this.ImportTextMember(members, membersScope, flag ? null : typeItems.baseSimpleType);
			}
			this.ImportXmlnsDeclarationsMember(type, members, membersScope);
			return (MemberMapping[])members.ToArray(typeof(MemberMapping));
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000B2858 File Offset: 0x000B0A58
		internal static bool IsMixed(XmlSchemaType type)
		{
			if (!(type is XmlSchemaComplexType))
			{
				return false;
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)type;
			bool flag = xmlSchemaComplexType.IsMixed;
			if (!flag && xmlSchemaComplexType.ContentModel != null && xmlSchemaComplexType.ContentModel is XmlSchemaComplexContent)
			{
				flag = ((XmlSchemaComplexContent)xmlSchemaComplexType.ContentModel).IsMixed;
			}
			return flag;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000B28A8 File Offset: 0x000B0AA8
		private XmlSchemaImporter.TypeItems GetTypeItems(XmlSchemaType type)
		{
			XmlSchemaImporter.TypeItems typeItems = new XmlSchemaImporter.TypeItems();
			if (type is XmlSchemaComplexType)
			{
				XmlSchemaParticle xmlSchemaParticle = null;
				XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)type;
				if (xmlSchemaComplexType.ContentModel != null)
				{
					XmlSchemaContent content = xmlSchemaComplexType.ContentModel.Content;
					if (content is XmlSchemaComplexContentExtension)
					{
						XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = (XmlSchemaComplexContentExtension)content;
						typeItems.Attributes = xmlSchemaComplexContentExtension.Attributes;
						typeItems.AnyAttribute = xmlSchemaComplexContentExtension.AnyAttribute;
						xmlSchemaParticle = xmlSchemaComplexContentExtension.Particle;
					}
					else if (content is XmlSchemaSimpleContentExtension)
					{
						XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = (XmlSchemaSimpleContentExtension)content;
						typeItems.Attributes = xmlSchemaSimpleContentExtension.Attributes;
						typeItems.AnyAttribute = xmlSchemaSimpleContentExtension.AnyAttribute;
						typeItems.baseSimpleType = xmlSchemaSimpleContentExtension.BaseTypeName;
					}
				}
				else
				{
					typeItems.Attributes = xmlSchemaComplexType.Attributes;
					typeItems.AnyAttribute = xmlSchemaComplexType.AnyAttribute;
					xmlSchemaParticle = xmlSchemaComplexType.Particle;
				}
				if (xmlSchemaParticle is XmlSchemaGroupRef)
				{
					XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)xmlSchemaParticle;
					typeItems.Particle = this.FindGroup(xmlSchemaGroupRef.RefName).Particle;
					typeItems.IsUnbounded = xmlSchemaParticle.IsMultipleOccurrence;
				}
				else if (xmlSchemaParticle is XmlSchemaGroupBase)
				{
					typeItems.Particle = (XmlSchemaGroupBase)xmlSchemaParticle;
					typeItems.IsUnbounded = xmlSchemaParticle.IsMultipleOccurrence;
				}
			}
			return typeItems;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000B29C8 File Offset: 0x000B0BC8
		private void ImportGroup(XmlSchemaGroupBase group, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, string ns, bool mixed, ref bool needExplicitOrder, bool allowDuplicates, bool groupRepeats, bool allowUnboundedElements)
		{
			if (group is XmlSchemaChoice)
			{
				this.ImportChoiceGroup((XmlSchemaChoice)group, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref needExplicitOrder, allowDuplicates);
			}
			else
			{
				this.ImportGroupMembers(group, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref mixed, ref needExplicitOrder, allowDuplicates, allowUnboundedElements);
			}
			if (mixed)
			{
				this.ImportTextMember(members, membersScope, null);
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000B2A24 File Offset: 0x000B0C24
		private MemberMapping ImportChoiceGroup(XmlSchemaGroupBase group, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, string ns, bool groupRepeats, ref bool needExplicitOrder, bool allowDuplicates)
		{
			NameTable nameTable = new NameTable();
			if (this.GatherGroupChoices(group, nameTable, identifier, ns, ref needExplicitOrder, allowDuplicates))
			{
				groupRepeats = true;
			}
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = (ElementAccessor[])nameTable.ToArray(typeof(ElementAccessor));
			Array.Sort(memberMapping.Elements, new XmlSchemaImporter.ElementComparer());
			this.AddScopeElements(elementsScope, memberMapping.Elements, ref needExplicitOrder, allowDuplicates);
			bool flag = false;
			bool flag2 = false;
			Hashtable hashtable = new Hashtable(memberMapping.Elements.Length);
			for (int i = 0; i < memberMapping.Elements.Length; i++)
			{
				ElementAccessor elementAccessor = memberMapping.Elements[i];
				string fullName = elementAccessor.Mapping.TypeDesc.FullName;
				object obj = hashtable[fullName];
				if (obj != null)
				{
					flag = true;
					ElementAccessor elementAccessor2 = (ElementAccessor)obj;
					if (!flag2 && elementAccessor2.IsNullable != elementAccessor.IsNullable)
					{
						flag2 = true;
					}
				}
				else
				{
					hashtable.Add(fullName, elementAccessor);
				}
				ArrayMapping arrayMapping = elementAccessor.Mapping as ArrayMapping;
				if (arrayMapping != null && this.IsNeedXmlSerializationAttributes(arrayMapping))
				{
					elementAccessor.Mapping = arrayMapping.TopLevelMapping;
					elementAccessor.Mapping.ReferencedByTopLevelElement = false;
					elementAccessor.Mapping.ReferencedByElement = true;
				}
			}
			if (flag2)
			{
				memberMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(object));
			}
			else
			{
				TypeDesc[] array = new TypeDesc[hashtable.Count];
				IEnumerator enumerator = hashtable.Values.GetEnumerator();
				int num = 0;
				while (num < array.Length && enumerator.MoveNext())
				{
					array[num] = ((ElementAccessor)enumerator.Current).Mapping.TypeDesc;
					num++;
				}
				memberMapping.TypeDesc = TypeDesc.FindCommonBaseTypeDesc(array);
				if (memberMapping.TypeDesc == null)
				{
					memberMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(object));
				}
			}
			if (groupRepeats)
			{
				memberMapping.TypeDesc = memberMapping.TypeDesc.CreateArrayTypeDesc();
			}
			if (membersScope != null)
			{
				memberMapping.Name = membersScope.AddUnique(groupRepeats ? "Items" : "Item", memberMapping);
				if (members != null)
				{
					members.Add(memberMapping.Name, memberMapping);
				}
			}
			if (flag)
			{
				memberMapping.ChoiceIdentifier = new ChoiceIdentifierAccessor();
				memberMapping.ChoiceIdentifier.MemberName = memberMapping.Name + "ElementName";
				memberMapping.ChoiceIdentifier.Mapping = this.ImportEnumeratedChoice(memberMapping.Elements, ns, memberMapping.Name + "ChoiceType");
				memberMapping.ChoiceIdentifier.MemberIds = new string[memberMapping.Elements.Length];
				ConstantMapping[] constants = ((EnumMapping)memberMapping.ChoiceIdentifier.Mapping).Constants;
				for (int j = 0; j < memberMapping.Elements.Length; j++)
				{
					memberMapping.ChoiceIdentifier.MemberIds[j] = constants[j].Name;
				}
				MemberMapping memberMapping2 = new MemberMapping();
				memberMapping2.Ignore = true;
				memberMapping2.Name = memberMapping.ChoiceIdentifier.MemberName;
				if (groupRepeats)
				{
					memberMapping2.TypeDesc = memberMapping.ChoiceIdentifier.Mapping.TypeDesc.CreateArrayTypeDesc();
				}
				else
				{
					memberMapping2.TypeDesc = memberMapping.ChoiceIdentifier.Mapping.TypeDesc;
				}
				ElementAccessor elementAccessor3 = new ElementAccessor();
				elementAccessor3.Name = memberMapping2.Name;
				elementAccessor3.Namespace = ns;
				elementAccessor3.Mapping = memberMapping.ChoiceIdentifier.Mapping;
				memberMapping2.Elements = new ElementAccessor[] { elementAccessor3 };
				if (membersScope != null)
				{
					elementAccessor3.Name = (memberMapping2.Name = (memberMapping.ChoiceIdentifier.MemberName = membersScope.AddUnique(memberMapping.ChoiceIdentifier.MemberName, memberMapping2)));
					if (members != null)
					{
						members.Add(elementAccessor3.Name, memberMapping2);
					}
				}
			}
			return memberMapping;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000B2DE8 File Offset: 0x000B0FE8
		private bool IsNeedXmlSerializationAttributes(ArrayMapping arrayMapping)
		{
			if (arrayMapping.Elements.Length != 1)
			{
				return true;
			}
			ElementAccessor elementAccessor = arrayMapping.Elements[0];
			TypeMapping mapping = elementAccessor.Mapping;
			if (elementAccessor.Name != mapping.DefaultElementName)
			{
				return true;
			}
			if (elementAccessor.Form != XmlSchemaForm.None && elementAccessor.Form != XmlSchemaForm.Qualified)
			{
				return true;
			}
			if (elementAccessor.Mapping.TypeDesc != null)
			{
				if (elementAccessor.IsNullable != elementAccessor.Mapping.TypeDesc.IsNullable)
				{
					return true;
				}
				if (elementAccessor.Mapping.TypeDesc.IsAmbiguousDataType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000B2E76 File Offset: 0x000B1076
		private bool GatherGroupChoices(XmlSchemaGroup group, NameTable choiceElements, string identifier, string ns, ref bool needExplicitOrder, bool allowDuplicates)
		{
			return this.GatherGroupChoices(group.Particle, choiceElements, identifier, ns, ref needExplicitOrder, allowDuplicates);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000B2E8C File Offset: 0x000B108C
		private bool GatherGroupChoices(XmlSchemaParticle particle, NameTable choiceElements, string identifier, string ns, ref bool needExplicitOrder, bool allowDuplicates)
		{
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle;
				if (!xmlSchemaGroupRef.RefName.IsEmpty)
				{
					base.AddReference(xmlSchemaGroupRef.RefName, base.GroupsInUse, "Group '{0}' from targetNamespace='{1}' has invalid definition: Circular group reference.");
					if (this.GatherGroupChoices(this.FindGroup(xmlSchemaGroupRef.RefName), choiceElements, identifier, xmlSchemaGroupRef.RefName.Namespace, ref needExplicitOrder, allowDuplicates))
					{
						base.RemoveReference(xmlSchemaGroupRef.RefName, base.GroupsInUse);
						return true;
					}
					base.RemoveReference(xmlSchemaGroupRef.RefName, base.GroupsInUse);
				}
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaGroupBase xmlSchemaGroupBase = (XmlSchemaGroupBase)particle;
				bool flag = xmlSchemaGroupBase.IsMultipleOccurrence;
				XmlSchemaAny xmlSchemaAny = null;
				bool flag2 = false;
				for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
				{
					object obj = xmlSchemaGroupBase.Items[i];
					if (obj is XmlSchemaGroupBase || obj is XmlSchemaGroupRef)
					{
						if (this.GatherGroupChoices((XmlSchemaParticle)obj, choiceElements, identifier, ns, ref needExplicitOrder, allowDuplicates))
						{
							flag = true;
						}
					}
					else if (obj is XmlSchemaAny)
					{
						if (this.GenerateOrder)
						{
							this.AddScopeElements(choiceElements, this.ImportAny((XmlSchemaAny)obj, true, ns), ref flag2, allowDuplicates);
						}
						else
						{
							xmlSchemaAny = (XmlSchemaAny)obj;
						}
					}
					else if (obj is XmlSchemaElement)
					{
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
						XmlSchemaElement topLevelElement = this.GetTopLevelElement(xmlSchemaElement);
						if (topLevelElement != null)
						{
							XmlSchemaElement[] equivalentElements = this.GetEquivalentElements(topLevelElement);
							for (int j = 0; j < equivalentElements.Length; j++)
							{
								if (equivalentElements[j].IsMultipleOccurrence)
								{
									flag = true;
								}
								this.AddScopeElement(choiceElements, this.ImportElement(equivalentElements[j], identifier, typeof(TypeMapping), null, equivalentElements[j].QualifiedName.Namespace, true), ref flag2, allowDuplicates);
							}
						}
						if (xmlSchemaElement.IsMultipleOccurrence)
						{
							flag = true;
						}
						this.AddScopeElement(choiceElements, this.ImportElement(xmlSchemaElement, identifier, typeof(TypeMapping), null, xmlSchemaElement.QualifiedName.Namespace, false), ref flag2, allowDuplicates);
					}
				}
				if (xmlSchemaAny != null)
				{
					this.AddScopeElements(choiceElements, this.ImportAny(xmlSchemaAny, true, ns), ref flag2, allowDuplicates);
				}
				if (!flag && !(xmlSchemaGroupBase is XmlSchemaChoice) && xmlSchemaGroupBase.Items.Count > 1)
				{
					flag = true;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000B30CC File Offset: 0x000B12CC
		private void AddScopeElement(INameScope scope, ElementAccessor element, ref bool duplicateElements, bool allowDuplicates)
		{
			if (scope == null)
			{
				return;
			}
			ElementAccessor elementAccessor = (ElementAccessor)scope[element.Name, element.Namespace];
			if (elementAccessor == null)
			{
				scope[element.Name, element.Namespace] = element;
				return;
			}
			if (!allowDuplicates)
			{
				throw new InvalidOperationException(Res.GetString("The XML element named '{0}' from namespace '{1}' is already present in the current scope.", new object[] { element.Name, element.Namespace }));
			}
			if (elementAccessor.Mapping.TypeDesc != element.Mapping.TypeDesc)
			{
				throw new InvalidOperationException(Res.GetString("The XML element named '{0}' from namespace '{1}' is already present in the current scope. Elements with the same name in the same scope must have the same type.", new object[] { element.Name, element.Namespace }));
			}
			duplicateElements = true;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000B317C File Offset: 0x000B137C
		private void AddScopeElements(INameScope scope, ElementAccessor[] elements, ref bool duplicateElements, bool allowDuplicates)
		{
			for (int i = 0; i < elements.Length; i++)
			{
				this.AddScopeElement(scope, elements[i], ref duplicateElements, allowDuplicates);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000B31A4 File Offset: 0x000B13A4
		private void ImportGroupMembers(XmlSchemaParticle particle, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, string ns, bool groupRepeats, ref bool mixed, ref bool needExplicitOrder, bool allowDuplicates, bool allowUnboundedElements)
		{
			if (particle is XmlSchemaGroupRef)
			{
				XmlSchemaGroupRef xmlSchemaGroupRef = (XmlSchemaGroupRef)particle;
				if (!xmlSchemaGroupRef.RefName.IsEmpty)
				{
					base.AddReference(xmlSchemaGroupRef.RefName, base.GroupsInUse, "Group '{0}' from targetNamespace='{1}' has invalid definition: Circular group reference.");
					this.ImportGroupMembers(this.FindGroup(xmlSchemaGroupRef.RefName).Particle, identifier, members, membersScope, elementsScope, xmlSchemaGroupRef.RefName.Namespace, groupRepeats | xmlSchemaGroupRef.IsMultipleOccurrence, ref mixed, ref needExplicitOrder, allowDuplicates, allowUnboundedElements);
					base.RemoveReference(xmlSchemaGroupRef.RefName, base.GroupsInUse);
					return;
				}
			}
			else if (particle is XmlSchemaGroupBase)
			{
				XmlSchemaGroupBase xmlSchemaGroupBase = (XmlSchemaGroupBase)particle;
				if (xmlSchemaGroupBase.IsMultipleOccurrence)
				{
					groupRepeats = true;
				}
				if (this.GenerateOrder && groupRepeats && xmlSchemaGroupBase.Items.Count > 1)
				{
					this.ImportChoiceGroup(xmlSchemaGroupBase, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref needExplicitOrder, allowDuplicates);
					return;
				}
				for (int i = 0; i < xmlSchemaGroupBase.Items.Count; i++)
				{
					object obj = xmlSchemaGroupBase.Items[i];
					if (obj is XmlSchemaChoice)
					{
						this.ImportChoiceGroup((XmlSchemaGroupBase)obj, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref needExplicitOrder, allowDuplicates);
					}
					else if (obj is XmlSchemaElement)
					{
						this.ImportElementMember((XmlSchemaElement)obj, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref needExplicitOrder, allowDuplicates, allowUnboundedElements);
					}
					else if (obj is XmlSchemaAny)
					{
						this.ImportAnyMember((XmlSchemaAny)obj, identifier, members, membersScope, elementsScope, ns, ref mixed, ref needExplicitOrder, allowDuplicates);
					}
					else if (obj is XmlSchemaParticle)
					{
						this.ImportGroupMembers((XmlSchemaParticle)obj, identifier, members, membersScope, elementsScope, ns, groupRepeats, ref mixed, ref needExplicitOrder, allowDuplicates, true);
					}
				}
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000B333E File Offset: 0x000B153E
		private XmlSchemaElement GetTopLevelElement(XmlSchemaElement element)
		{
			if (!element.RefName.IsEmpty)
			{
				return this.FindElement(element.RefName);
			}
			return null;
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000B335C File Offset: 0x000B155C
		private XmlSchemaElement[] GetEquivalentElements(XmlSchemaElement element)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in base.Schemas.SchemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				for (int i = 0; i < xmlSchema.Items.Count; i++)
				{
					object obj2 = xmlSchema.Items[i];
					if (obj2 is XmlSchemaElement)
					{
						XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj2;
						if (!xmlSchemaElement.IsAbstract && xmlSchemaElement.SubstitutionGroup.Namespace == xmlSchema.TargetNamespace && xmlSchemaElement.SubstitutionGroup.Name == element.Name)
						{
							arrayList.Add(xmlSchemaElement);
						}
					}
				}
			}
			return (XmlSchemaElement[])arrayList.ToArray(typeof(XmlSchemaElement));
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000B3458 File Offset: 0x000B1658
		private bool ImportSubstitutionGroupMember(XmlSchemaElement element, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, string ns, bool repeats, ref bool needExplicitOrder, bool allowDuplicates)
		{
			XmlSchemaElement[] equivalentElements = this.GetEquivalentElements(element);
			if (equivalentElements.Length == 0)
			{
				return false;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			for (int i = 0; i < equivalentElements.Length; i++)
			{
				xmlSchemaChoice.Items.Add(equivalentElements[i]);
			}
			if (!element.IsAbstract)
			{
				xmlSchemaChoice.Items.Add(element);
			}
			if (identifier.Length == 0)
			{
				identifier = CodeIdentifier.MakeValid(Accessor.UnescapeName(element.Name));
			}
			else
			{
				identifier += CodeIdentifier.MakePascal(Accessor.UnescapeName(element.Name));
			}
			this.ImportChoiceGroup(xmlSchemaChoice, identifier, members, membersScope, null, ns, repeats, ref needExplicitOrder, allowDuplicates);
			return true;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000B34F8 File Offset: 0x000B16F8
		private void ImportTextMember(CodeIdentifiers members, CodeIdentifiers membersScope, XmlQualifiedName simpleContentType)
		{
			bool flag = false;
			TypeMapping typeMapping;
			if (simpleContentType != null)
			{
				typeMapping = this.ImportType(simpleContentType, typeof(TypeMapping), null, (TypeFlags)48, false);
				if (!(typeMapping is PrimitiveMapping) && !typeMapping.TypeDesc.CanBeTextValue)
				{
					return;
				}
			}
			else
			{
				flag = true;
				typeMapping = this.GetDefaultMapping((TypeFlags)48);
			}
			TextAccessor textAccessor = new TextAccessor();
			textAccessor.Mapping = typeMapping;
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = new ElementAccessor[0];
			memberMapping.Text = textAccessor;
			if (flag)
			{
				memberMapping.TypeDesc = textAccessor.Mapping.TypeDesc.CreateArrayTypeDesc();
				memberMapping.Name = members.MakeRightCase("Text");
			}
			else if (((PrimitiveMapping)textAccessor.Mapping).IsList)
			{
				memberMapping.TypeDesc = textAccessor.Mapping.TypeDesc.CreateArrayTypeDesc();
				memberMapping.Name = members.MakeRightCase("Text");
			}
			else
			{
				memberMapping.TypeDesc = textAccessor.Mapping.TypeDesc;
				memberMapping.Name = members.MakeRightCase("Value");
			}
			memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
			members.Add(memberMapping.Name, memberMapping);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000B3618 File Offset: 0x000B1818
		private MemberMapping ImportAnyMember(XmlSchemaAny any, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, string ns, ref bool mixed, ref bool needExplicitOrder, bool allowDuplicates)
		{
			ElementAccessor[] array = this.ImportAny(any, !mixed, ns);
			this.AddScopeElements(elementsScope, array, ref needExplicitOrder, allowDuplicates);
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = array;
			memberMapping.Name = membersScope.MakeRightCase("Any");
			memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
			members.Add(memberMapping.Name, memberMapping);
			memberMapping.TypeDesc = array[0].Mapping.TypeDesc;
			bool flag = any.IsMultipleOccurrence;
			if (mixed)
			{
				SpecialMapping specialMapping = new SpecialMapping();
				specialMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(XmlNode));
				specialMapping.TypeName = specialMapping.TypeDesc.Name;
				memberMapping.TypeDesc = specialMapping.TypeDesc;
				memberMapping.Text = new TextAccessor
				{
					Mapping = specialMapping
				};
				flag = true;
				mixed = false;
			}
			if (flag)
			{
				memberMapping.TypeDesc = memberMapping.TypeDesc.CreateArrayTypeDesc();
			}
			return memberMapping;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000B3714 File Offset: 0x000B1914
		private ElementAccessor[] ImportAny(XmlSchemaAny any, bool makeElement, string targetNamespace)
		{
			SpecialMapping specialMapping = new SpecialMapping();
			specialMapping.TypeDesc = base.Scope.GetTypeDesc(makeElement ? typeof(XmlElement) : typeof(XmlNode));
			specialMapping.TypeName = specialMapping.TypeDesc.Name;
			TypeFlags typeFlags = TypeFlags.CanBeElementValue;
			if (makeElement)
			{
				typeFlags |= TypeFlags.CanBeTextValue;
			}
			this.RunSchemaExtensions(specialMapping, XmlQualifiedName.Empty, null, any, typeFlags);
			if (this.GenerateOrder && any.Namespace != null)
			{
				NamespaceList namespaceList = new NamespaceList(any.Namespace, targetNamespace);
				if (namespaceList.Type == NamespaceList.ListType.Set)
				{
					ICollection enumerate = namespaceList.Enumerate;
					ElementAccessor[] array = new ElementAccessor[(enumerate.Count == 0) ? 1 : enumerate.Count];
					int num = 0;
					foreach (object obj in namespaceList.Enumerate)
					{
						string text = (string)obj;
						ElementAccessor elementAccessor = new ElementAccessor();
						elementAccessor.Mapping = specialMapping;
						elementAccessor.Any = true;
						elementAccessor.Namespace = text;
						array[num++] = elementAccessor;
					}
					if (num > 0)
					{
						return array;
					}
				}
			}
			return new ElementAccessor[]
			{
				new ElementAccessor
				{
					Mapping = specialMapping,
					Any = true
				}
			};
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000B3870 File Offset: 0x000B1A70
		private ElementAccessor ImportArray(XmlSchemaElement element, string identifier, string ns, bool repeats)
		{
			if (repeats)
			{
				return null;
			}
			if (element.SchemaType == null)
			{
				return null;
			}
			if (element.IsMultipleOccurrence)
			{
				return null;
			}
			XmlSchemaType schemaType = element.SchemaType;
			ArrayMapping arrayMapping = this.ImportArrayMapping(schemaType, identifier, ns, repeats);
			if (arrayMapping == null)
			{
				return null;
			}
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.Name = element.Name;
			elementAccessor.Namespace = ns;
			elementAccessor.Mapping = arrayMapping;
			if (arrayMapping.TypeDesc.IsNullable)
			{
				elementAccessor.IsNullable = element.IsNillable;
			}
			elementAccessor.Form = this.ElementForm(ns, element);
			return elementAccessor;
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000B38F8 File Offset: 0x000B1AF8
		private ArrayMapping ImportArrayMapping(XmlSchemaType type, string identifier, string ns, bool repeats)
		{
			if (!(type is XmlSchemaComplexType))
			{
				return null;
			}
			if (!type.DerivedFrom.IsEmpty)
			{
				return null;
			}
			if (XmlSchemaImporter.IsMixed(type))
			{
				return null;
			}
			Mapping mapping = (Mapping)base.ImportedMappings[type];
			if (mapping != null)
			{
				if (mapping is ArrayMapping)
				{
					return (ArrayMapping)mapping;
				}
				return null;
			}
			else
			{
				XmlSchemaImporter.TypeItems typeItems = this.GetTypeItems(type);
				if (typeItems.Attributes != null && typeItems.Attributes.Count > 0)
				{
					return null;
				}
				if (typeItems.AnyAttribute != null)
				{
					return null;
				}
				if (typeItems.Particle == null)
				{
					return null;
				}
				XmlSchemaGroupBase particle = typeItems.Particle;
				ArrayMapping arrayMapping = new ArrayMapping();
				arrayMapping.TypeName = identifier;
				arrayMapping.Namespace = ns;
				if (particle is XmlSchemaChoice)
				{
					XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)particle;
					if (!xmlSchemaChoice.IsMultipleOccurrence)
					{
						return null;
					}
					bool flag = false;
					MemberMapping memberMapping = this.ImportChoiceGroup(xmlSchemaChoice, identifier, null, null, null, ns, true, ref flag, false);
					if (memberMapping.ChoiceIdentifier != null)
					{
						return null;
					}
					arrayMapping.TypeDesc = memberMapping.TypeDesc;
					arrayMapping.Elements = memberMapping.Elements;
					arrayMapping.TypeName = ((type.Name == null || type.Name.Length == 0) ? ("ArrayOf" + CodeIdentifier.MakePascal(arrayMapping.TypeDesc.Name)) : type.Name);
				}
				else
				{
					if (!(particle is XmlSchemaAll) && !(particle is XmlSchemaSequence))
					{
						return null;
					}
					if (particle.Items.Count != 1 || !(particle.Items[0] is XmlSchemaElement))
					{
						return null;
					}
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)particle.Items[0];
					if (!xmlSchemaElement.IsMultipleOccurrence)
					{
						return null;
					}
					if (this.IsCyclicReferencedType(xmlSchemaElement, new List<string>(1) { identifier }))
					{
						return null;
					}
					ElementAccessor elementAccessor = this.ImportElement(xmlSchemaElement, identifier, typeof(TypeMapping), null, ns, false);
					if (elementAccessor.Any)
					{
						return null;
					}
					arrayMapping.Elements = new ElementAccessor[] { elementAccessor };
					arrayMapping.TypeDesc = elementAccessor.Mapping.TypeDesc.CreateArrayTypeDesc();
					arrayMapping.TypeName = ((type.Name == null || type.Name.Length == 0) ? ("ArrayOf" + CodeIdentifier.MakePascal(elementAccessor.Mapping.TypeDesc.Name)) : type.Name);
				}
				base.ImportedMappings[type] = arrayMapping;
				base.Scope.AddTypeMapping(arrayMapping);
				arrayMapping.TopLevelMapping = this.ImportStructType(type, ns, identifier, null, true);
				arrayMapping.TopLevelMapping.ReferencedByTopLevelElement = true;
				if (type.Name != null && type.Name.Length != 0)
				{
					this.ImportDerivedTypes(new XmlQualifiedName(identifier, ns));
				}
				return arrayMapping;
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000B3B90 File Offset: 0x000B1D90
		private bool IsCyclicReferencedType(XmlSchemaElement element, List<string> identifiers)
		{
			if (!element.RefName.IsEmpty)
			{
				XmlSchemaElement xmlSchemaElement = this.FindElement(element.RefName);
				string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(xmlSchemaElement.Name));
				foreach (string text2 in identifiers)
				{
					if (text == text2)
					{
						return true;
					}
				}
				identifiers.Add(text);
				XmlSchemaType schemaType = xmlSchemaElement.SchemaType;
				if (!(schemaType is XmlSchemaComplexType))
				{
					return false;
				}
				XmlSchemaImporter.TypeItems typeItems = this.GetTypeItems(schemaType);
				if ((!(typeItems.Particle is XmlSchemaSequence) && !(typeItems.Particle is XmlSchemaAll)) || typeItems.Particle.Items.Count != 1 || !(typeItems.Particle.Items[0] is XmlSchemaElement))
				{
					return false;
				}
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)typeItems.Particle.Items[0];
				if (xmlSchemaElement2.IsMultipleOccurrence)
				{
					return this.IsCyclicReferencedType(xmlSchemaElement2, identifiers);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000B3CB0 File Offset: 0x000B1EB0
		private SpecialMapping ImportAnyMapping(XmlSchemaType type, string identifier, string ns, bool repeats)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.DerivedFrom.IsEmpty)
			{
				return null;
			}
			bool flag = XmlSchemaImporter.IsMixed(type);
			XmlSchemaImporter.TypeItems typeItems = this.GetTypeItems(type);
			if (typeItems.Particle == null)
			{
				return null;
			}
			if (!(typeItems.Particle is XmlSchemaAll) && !(typeItems.Particle is XmlSchemaSequence))
			{
				return null;
			}
			if (typeItems.Attributes != null && typeItems.Attributes.Count > 0)
			{
				return null;
			}
			XmlSchemaGroupBase particle = typeItems.Particle;
			if (particle.Items.Count != 1 || !(particle.Items[0] is XmlSchemaAny))
			{
				return null;
			}
			XmlSchemaAny xmlSchemaAny = (XmlSchemaAny)particle.Items[0];
			SpecialMapping specialMapping = new SpecialMapping();
			if (typeItems.AnyAttribute != null && xmlSchemaAny.IsMultipleOccurrence && flag)
			{
				specialMapping.NamedAny = true;
				specialMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(XmlElement));
			}
			else
			{
				if (typeItems.AnyAttribute != null || xmlSchemaAny.IsMultipleOccurrence)
				{
					return null;
				}
				specialMapping.TypeDesc = base.Scope.GetTypeDesc(flag ? typeof(XmlNode) : typeof(XmlElement));
			}
			TypeFlags typeFlags = TypeFlags.CanBeElementValue;
			if (typeItems.AnyAttribute != null || flag)
			{
				typeFlags |= TypeFlags.CanBeTextValue;
			}
			this.RunSchemaExtensions(specialMapping, XmlQualifiedName.Empty, null, xmlSchemaAny, typeFlags);
			specialMapping.TypeName = specialMapping.TypeDesc.Name;
			if (repeats)
			{
				specialMapping.TypeDesc = specialMapping.TypeDesc.CreateArrayTypeDesc();
			}
			return specialMapping;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000B3E30 File Offset: 0x000B2030
		private void ImportElementMember(XmlSchemaElement element, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, INameScope elementsScope, string ns, bool repeats, ref bool needExplicitOrder, bool allowDuplicates, bool allowUnboundedElements)
		{
			repeats |= element.IsMultipleOccurrence;
			XmlSchemaElement topLevelElement = this.GetTopLevelElement(element);
			if (topLevelElement != null && this.ImportSubstitutionGroupMember(topLevelElement, identifier, members, membersScope, ns, repeats, ref needExplicitOrder, allowDuplicates))
			{
				return;
			}
			ElementAccessor elementAccessor = this.ImportArray(element, identifier, ns, repeats) ?? this.ImportElement(element, identifier, typeof(TypeMapping), null, ns, false);
			MemberMapping memberMapping = new MemberMapping();
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(elementAccessor.Name));
			memberMapping.Name = membersScope.AddUnique(text, memberMapping);
			if (memberMapping.Name.EndsWith("Specified", StringComparison.Ordinal))
			{
				text = memberMapping.Name;
				memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
				membersScope.Remove(text);
			}
			members.Add(memberMapping.Name, memberMapping);
			if (elementAccessor.Mapping.IsList)
			{
				elementAccessor.Mapping = this.GetDefaultMapping((TypeFlags)48);
				memberMapping.TypeDesc = elementAccessor.Mapping.TypeDesc;
			}
			else
			{
				memberMapping.TypeDesc = elementAccessor.Mapping.TypeDesc;
			}
			this.AddScopeElement(elementsScope, elementAccessor, ref needExplicitOrder, allowDuplicates);
			memberMapping.Elements = new ElementAccessor[] { elementAccessor };
			if (element.IsMultipleOccurrence || repeats)
			{
				if (!allowUnboundedElements && elementAccessor.Mapping is ArrayMapping)
				{
					elementAccessor.Mapping = ((ArrayMapping)elementAccessor.Mapping).TopLevelMapping;
					elementAccessor.Mapping.ReferencedByTopLevelElement = false;
					elementAccessor.Mapping.ReferencedByElement = true;
				}
				memberMapping.TypeDesc = elementAccessor.Mapping.TypeDesc.CreateArrayTypeDesc();
			}
			if (element.MinOccurs == 0m && memberMapping.TypeDesc.IsValueType && !element.HasDefault && !memberMapping.TypeDesc.HasIsEmpty)
			{
				memberMapping.CheckSpecified = SpecifiedAccessor.ReadWrite;
			}
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000B3FF0 File Offset: 0x000B21F0
		private void ImportAttributeMember(XmlSchemaAttribute attribute, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, string ns)
		{
			AttributeAccessor attributeAccessor = this.ImportAttribute(attribute, identifier, ns, attribute);
			if (attributeAccessor == null)
			{
				return;
			}
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = new ElementAccessor[0];
			memberMapping.Attribute = attributeAccessor;
			memberMapping.Name = CodeIdentifier.MakeValid(Accessor.UnescapeName(attributeAccessor.Name));
			memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
			if (memberMapping.Name.EndsWith("Specified", StringComparison.Ordinal))
			{
				string name = memberMapping.Name;
				memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
				membersScope.Remove(name);
			}
			members.Add(memberMapping.Name, memberMapping);
			memberMapping.TypeDesc = (attributeAccessor.IsList ? attributeAccessor.Mapping.TypeDesc.CreateArrayTypeDesc() : attributeAccessor.Mapping.TypeDesc);
			if ((attribute.Use == XmlSchemaUse.Optional || attribute.Use == XmlSchemaUse.None) && memberMapping.TypeDesc.IsValueType && !attribute.HasDefault && !memberMapping.TypeDesc.HasIsEmpty)
			{
				memberMapping.CheckSpecified = SpecifiedAccessor.ReadWrite;
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000B40F8 File Offset: 0x000B22F8
		private void ImportAnyAttributeMember(XmlSchemaAnyAttribute any, CodeIdentifiers members, CodeIdentifiers membersScope)
		{
			SpecialMapping specialMapping = new SpecialMapping();
			specialMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(XmlAttribute));
			specialMapping.TypeName = specialMapping.TypeDesc.Name;
			AttributeAccessor attributeAccessor = new AttributeAccessor();
			attributeAccessor.Any = true;
			attributeAccessor.Mapping = specialMapping;
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = new ElementAccessor[0];
			memberMapping.Attribute = attributeAccessor;
			memberMapping.Name = membersScope.MakeRightCase("AnyAttr");
			memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
			members.Add(memberMapping.Name, memberMapping);
			memberMapping.TypeDesc = attributeAccessor.Mapping.TypeDesc;
			memberMapping.TypeDesc = memberMapping.TypeDesc.CreateArrayTypeDesc();
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000B41B8 File Offset: 0x000B23B8
		private bool KeepXmlnsDeclarations(XmlSchemaType type, out string xmlnsMemberName)
		{
			xmlnsMemberName = null;
			if (type.Annotation == null)
			{
				return false;
			}
			if (type.Annotation.Items == null || type.Annotation.Items.Count == 0)
			{
				return false;
			}
			foreach (XmlSchemaObject xmlSchemaObject in type.Annotation.Items)
			{
				if (xmlSchemaObject is XmlSchemaAppInfo)
				{
					XmlNode[] markup = ((XmlSchemaAppInfo)xmlSchemaObject).Markup;
					if (markup != null && markup.Length != 0)
					{
						foreach (XmlNode xmlNode in markup)
						{
							if (xmlNode is XmlElement)
							{
								XmlElement xmlElement = (XmlElement)xmlNode;
								if (xmlElement.Name == "keepNamespaceDeclarations")
								{
									if (xmlElement.LastNode is XmlText)
									{
										xmlnsMemberName = ((XmlText)xmlElement.LastNode).Value.Trim(null);
									}
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000B42CC File Offset: 0x000B24CC
		private void ImportXmlnsDeclarationsMember(XmlSchemaType type, CodeIdentifiers members, CodeIdentifiers membersScope)
		{
			string text;
			if (!this.KeepXmlnsDeclarations(type, out text))
			{
				return;
			}
			TypeDesc typeDesc = base.Scope.GetTypeDesc(typeof(XmlSerializerNamespaces));
			StructMapping structMapping = new StructMapping();
			structMapping.TypeDesc = typeDesc;
			structMapping.TypeName = structMapping.TypeDesc.Name;
			structMapping.Members = new MemberMapping[0];
			structMapping.IncludeInSchema = false;
			structMapping.ReferencedByTopLevelElement = true;
			ElementAccessor elementAccessor = new ElementAccessor();
			elementAccessor.Mapping = structMapping;
			MemberMapping memberMapping = new MemberMapping();
			memberMapping.Elements = new ElementAccessor[] { elementAccessor };
			memberMapping.Name = CodeIdentifier.MakeValid((text == null) ? "Namespaces" : text);
			memberMapping.Name = membersScope.AddUnique(memberMapping.Name, memberMapping);
			members.Add(memberMapping.Name, memberMapping);
			memberMapping.TypeDesc = typeDesc;
			memberMapping.Xmlns = new XmlnsAccessor();
			memberMapping.Ignore = true;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000B43B0 File Offset: 0x000B25B0
		private void ImportAttributeGroupMembers(XmlSchemaAttributeGroup group, string identifier, CodeIdentifiers members, CodeIdentifiers membersScope, string ns)
		{
			for (int i = 0; i < group.Attributes.Count; i++)
			{
				object obj = group.Attributes[i];
				if (obj is XmlSchemaAttributeGroup)
				{
					this.ImportAttributeGroupMembers((XmlSchemaAttributeGroup)obj, identifier, members, membersScope, ns);
				}
				else if (obj is XmlSchemaAttribute)
				{
					this.ImportAttributeMember((XmlSchemaAttribute)obj, identifier, members, membersScope, ns);
				}
			}
			if (group.AnyAttribute != null)
			{
				this.ImportAnyAttributeMember(group.AnyAttribute, members, membersScope);
			}
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000B4430 File Offset: 0x000B2630
		private AttributeAccessor ImportSpecialAttribute(XmlQualifiedName name, string identifier)
		{
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			primitiveMapping.TypeDesc = base.Scope.GetTypeDesc(typeof(string));
			primitiveMapping.TypeName = primitiveMapping.TypeDesc.DataType.Name;
			AttributeAccessor attributeAccessor = new AttributeAccessor();
			attributeAccessor.Name = name.Name;
			attributeAccessor.Namespace = "http://www.w3.org/XML/1998/namespace";
			attributeAccessor.CheckSpecial();
			attributeAccessor.Mapping = primitiveMapping;
			return attributeAccessor;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000B44A0 File Offset: 0x000B26A0
		private AttributeAccessor ImportAttribute(XmlSchemaAttribute attribute, string identifier, string ns, XmlSchemaAttribute defaultValueProvider)
		{
			if (attribute.Use == XmlSchemaUse.Prohibited)
			{
				return null;
			}
			if (!attribute.RefName.IsEmpty)
			{
				if (attribute.RefName.Namespace == "http://www.w3.org/XML/1998/namespace")
				{
					return this.ImportSpecialAttribute(attribute.RefName, identifier);
				}
				return this.ImportAttribute(this.FindAttribute(attribute.RefName), identifier, attribute.RefName.Namespace, defaultValueProvider);
			}
			else
			{
				if (attribute.Name.Length == 0)
				{
					throw new InvalidOperationException(Res.GetString("This attribute has no name."));
				}
				if (identifier.Length == 0)
				{
					identifier = CodeIdentifier.MakeValid(attribute.Name);
				}
				else
				{
					identifier += CodeIdentifier.MakePascal(attribute.Name);
				}
				TypeMapping typeMapping;
				if (!attribute.SchemaTypeName.IsEmpty)
				{
					typeMapping = this.ImportType(attribute.SchemaTypeName, typeof(TypeMapping), null, TypeFlags.CanBeAttributeValue, false);
				}
				else if (attribute.SchemaType != null)
				{
					typeMapping = this.ImportDataType(attribute.SchemaType, ns, identifier, null, TypeFlags.CanBeAttributeValue, false);
				}
				else
				{
					typeMapping = this.GetDefaultMapping(TypeFlags.CanBeAttributeValue);
				}
				if (typeMapping != null && !typeMapping.TypeDesc.IsMappedType)
				{
					this.RunSchemaExtensions(typeMapping, attribute.SchemaTypeName, attribute.SchemaType, attribute, (TypeFlags)56);
				}
				AttributeAccessor attributeAccessor = new AttributeAccessor();
				attributeAccessor.Name = attribute.Name;
				attributeAccessor.Namespace = ns;
				attributeAccessor.Form = this.AttributeForm(ns, attribute);
				attributeAccessor.CheckSpecial();
				attributeAccessor.Mapping = typeMapping;
				attributeAccessor.IsList = typeMapping.IsList;
				attributeAccessor.IsOptional = attribute.Use != XmlSchemaUse.Required;
				if (defaultValueProvider.DefaultValue != null)
				{
					attributeAccessor.Default = defaultValueProvider.DefaultValue;
				}
				else if (defaultValueProvider.FixedValue != null)
				{
					attributeAccessor.Default = defaultValueProvider.FixedValue;
					attributeAccessor.IsFixed = true;
				}
				else if (attribute != defaultValueProvider)
				{
					if (attribute.DefaultValue != null)
					{
						attributeAccessor.Default = attribute.DefaultValue;
					}
					else if (attribute.FixedValue != null)
					{
						attributeAccessor.Default = attribute.FixedValue;
						attributeAccessor.IsFixed = true;
					}
				}
				return attributeAccessor;
			}
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000B4688 File Offset: 0x000B2888
		private TypeMapping ImportDataType(XmlSchemaSimpleType dataType, string typeNs, string identifier, Type baseType, TypeFlags flags, bool isList)
		{
			if (baseType != null)
			{
				return this.ImportStructDataType(dataType, typeNs, identifier, baseType);
			}
			TypeMapping typeMapping = this.ImportNonXsdPrimitiveDataType(dataType, typeNs, flags);
			if (typeMapping != null)
			{
				return typeMapping;
			}
			if (dataType.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)dataType.Content;
				using (XmlSchemaObjectEnumerator enumerator = xmlSchemaSimpleTypeRestriction.Facets.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current is XmlSchemaEnumerationFacet)
						{
							return this.ImportEnumeratedDataType(dataType, typeNs, identifier, flags, isList);
						}
					}
				}
				if (xmlSchemaSimpleTypeRestriction.BaseType != null)
				{
					return this.ImportDataType(xmlSchemaSimpleTypeRestriction.BaseType, typeNs, identifier, null, flags, false);
				}
				base.AddReference(xmlSchemaSimpleTypeRestriction.BaseTypeName, base.TypesInUse, "Type '{0}' from targetNamespace='{1}' has invalid definition: Circular type reference.");
				typeMapping = this.ImportDataType(this.FindDataType(xmlSchemaSimpleTypeRestriction.BaseTypeName, flags), xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace, identifier, null, flags, false);
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					base.RemoveReference(xmlSchemaSimpleTypeRestriction.BaseTypeName, base.TypesInUse);
				}
				return typeMapping;
			}
			if (dataType.Content is XmlSchemaSimpleTypeList || dataType.Content is XmlSchemaSimpleTypeUnion)
			{
				if (dataType.Content is XmlSchemaSimpleTypeList)
				{
					XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)dataType.Content;
					if (xmlSchemaSimpleTypeList.ItemType != null)
					{
						typeMapping = this.ImportDataType(xmlSchemaSimpleTypeList.ItemType, typeNs, identifier, null, flags, true);
						if (typeMapping != null)
						{
							typeMapping.TypeName = dataType.Name;
							return typeMapping;
						}
					}
					else if (xmlSchemaSimpleTypeList.ItemTypeName != null && !xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
					{
						typeMapping = this.ImportType(xmlSchemaSimpleTypeList.ItemTypeName, typeof(TypeMapping), null, TypeFlags.CanBeAttributeValue, true);
						if (typeMapping != null && typeMapping is PrimitiveMapping)
						{
							((PrimitiveMapping)typeMapping).IsList = true;
							return typeMapping;
						}
					}
				}
				return this.GetDefaultMapping(flags);
			}
			return this.ImportPrimitiveDataType(dataType, flags);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000B4884 File Offset: 0x000B2A84
		private TypeMapping ImportEnumeratedDataType(XmlSchemaSimpleType dataType, string typeNs, string identifier, TypeFlags flags, bool isList)
		{
			TypeMapping typeMapping = (TypeMapping)base.ImportedMappings[dataType];
			if (typeMapping != null)
			{
				return typeMapping;
			}
			XmlSchemaType xmlSchemaType = dataType;
			while (!xmlSchemaType.DerivedFrom.IsEmpty)
			{
				xmlSchemaType = this.FindType(xmlSchemaType.DerivedFrom, (TypeFlags)40);
			}
			if (xmlSchemaType is XmlSchemaComplexType)
			{
				return null;
			}
			TypeDesc typeDesc = base.Scope.GetTypeDesc((XmlSchemaSimpleType)xmlSchemaType);
			if (typeDesc != null && typeDesc.FullName != typeof(string).FullName)
			{
				return this.ImportPrimitiveDataType(dataType, flags);
			}
			identifier = Accessor.UnescapeName(identifier);
			string text = base.GenerateUniqueTypeName(identifier);
			EnumMapping enumMapping = new EnumMapping();
			enumMapping.IsReference = base.Schemas.IsReference(dataType);
			enumMapping.TypeDesc = new TypeDesc(text, text, TypeKind.Enum, null, TypeFlags.None);
			if (dataType.Name != null && dataType.Name.Length > 0)
			{
				enumMapping.TypeName = identifier;
			}
			enumMapping.Namespace = typeNs;
			enumMapping.IsFlags = isList;
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			XmlSchemaSimpleTypeContent content = dataType.Content;
			if (content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)content;
				for (int i = 0; i < xmlSchemaSimpleTypeRestriction.Facets.Count; i++)
				{
					object obj = xmlSchemaSimpleTypeRestriction.Facets[i];
					if (obj is XmlSchemaEnumerationFacet)
					{
						XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = (XmlSchemaEnumerationFacet)obj;
						if (typeDesc != null && typeDesc.HasCustomFormatter)
						{
							XmlCustomFormatter.ToDefaultValue(xmlSchemaEnumerationFacet.Value, typeDesc.FormatterName);
						}
						ConstantMapping constantMapping = new ConstantMapping();
						string text2 = CodeIdentifier.MakeValid(xmlSchemaEnumerationFacet.Value);
						constantMapping.Name = codeIdentifiers.AddUnique(text2, constantMapping);
						constantMapping.XmlName = xmlSchemaEnumerationFacet.Value;
						constantMapping.Value = (long)i;
					}
				}
			}
			enumMapping.Constants = (ConstantMapping[])codeIdentifiers.ToArray(typeof(ConstantMapping));
			if (isList && enumMapping.Constants.Length > 63)
			{
				typeMapping = this.GetDefaultMapping((TypeFlags)56);
				base.ImportedMappings.Add(dataType, typeMapping);
				return typeMapping;
			}
			base.ImportedMappings.Add(dataType, enumMapping);
			base.Scope.AddTypeMapping(enumMapping);
			return enumMapping;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000B4AA0 File Offset: 0x000B2CA0
		private EnumMapping ImportEnumeratedChoice(ElementAccessor[] choice, string typeNs, string typeName)
		{
			typeName = this.GenerateUniqueTypeName(Accessor.UnescapeName(typeName), typeNs);
			EnumMapping enumMapping = new EnumMapping();
			enumMapping.TypeDesc = new TypeDesc(typeName, typeName, TypeKind.Enum, null, TypeFlags.None);
			enumMapping.TypeName = typeName;
			enumMapping.Namespace = typeNs;
			enumMapping.IsFlags = false;
			enumMapping.IncludeInSchema = false;
			if (this.GenerateOrder)
			{
				Array.Sort(choice, new XmlSchemaImporter.ElementComparer());
			}
			CodeIdentifiers codeIdentifiers = new CodeIdentifiers();
			for (int i = 0; i < choice.Length; i++)
			{
				ElementAccessor elementAccessor = choice[i];
				ConstantMapping constantMapping = new ConstantMapping();
				string text = CodeIdentifier.MakeValid(elementAccessor.Name);
				constantMapping.Name = codeIdentifiers.AddUnique(text, constantMapping);
				constantMapping.XmlName = elementAccessor.ToString(typeNs);
				constantMapping.Value = (long)i;
			}
			enumMapping.Constants = (ConstantMapping[])codeIdentifiers.ToArray(typeof(ConstantMapping));
			base.Scope.AddTypeMapping(enumMapping);
			return enumMapping;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000B4B80 File Offset: 0x000B2D80
		private PrimitiveMapping ImportPrimitiveDataType(XmlSchemaSimpleType dataType, TypeFlags flags)
		{
			TypeDesc dataTypeSource = this.GetDataTypeSource(dataType, flags);
			PrimitiveMapping primitiveMapping = new PrimitiveMapping();
			primitiveMapping.TypeDesc = dataTypeSource;
			primitiveMapping.TypeName = dataTypeSource.DataType.Name;
			primitiveMapping.Namespace = (primitiveMapping.TypeDesc.IsXsdType ? "http://www.w3.org/2001/XMLSchema" : "http://microsoft.com/wsdl/types/");
			return primitiveMapping;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000B4BD4 File Offset: 0x000B2DD4
		private PrimitiveMapping ImportNonXsdPrimitiveDataType(XmlSchemaSimpleType dataType, string ns, TypeFlags flags)
		{
			PrimitiveMapping primitiveMapping = null;
			if (dataType.Name != null && dataType.Name.Length != 0)
			{
				TypeDesc typeDesc = base.Scope.GetTypeDesc(dataType.Name, ns, flags);
				if (typeDesc != null)
				{
					primitiveMapping = new PrimitiveMapping();
					primitiveMapping.TypeDesc = typeDesc;
					primitiveMapping.TypeName = typeDesc.DataType.Name;
					primitiveMapping.Namespace = (primitiveMapping.TypeDesc.IsXsdType ? "http://www.w3.org/2001/XMLSchema" : ns);
				}
			}
			return primitiveMapping;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000B4C4C File Offset: 0x000B2E4C
		private XmlSchemaGroup FindGroup(XmlQualifiedName name)
		{
			XmlSchemaGroup xmlSchemaGroup = (XmlSchemaGroup)base.Schemas.Find(name, typeof(XmlSchemaGroup));
			if (xmlSchemaGroup == null)
			{
				throw new InvalidOperationException(Res.GetString("Group {0} is missing.", new object[] { name.Name }));
			}
			return xmlSchemaGroup;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000B4C98 File Offset: 0x000B2E98
		private XmlSchemaAttributeGroup FindAttributeGroup(XmlQualifiedName name)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = (XmlSchemaAttributeGroup)base.Schemas.Find(name, typeof(XmlSchemaAttributeGroup));
			if (xmlSchemaAttributeGroup == null)
			{
				throw new InvalidOperationException(Res.GetString("The attribute group {0} is missing.", new object[] { name.Name }));
			}
			return xmlSchemaAttributeGroup;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000B4CE4 File Offset: 0x000B2EE4
		internal static XmlQualifiedName BaseTypeName(XmlSchemaSimpleType dataType)
		{
			XmlSchemaSimpleTypeContent content = dataType.Content;
			if (content is XmlSchemaSimpleTypeRestriction)
			{
				return ((XmlSchemaSimpleTypeRestriction)content).BaseTypeName;
			}
			if (content is XmlSchemaSimpleTypeList)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = (XmlSchemaSimpleTypeList)content;
				if (xmlSchemaSimpleTypeList.ItemTypeName != null && !xmlSchemaSimpleTypeList.ItemTypeName.IsEmpty)
				{
					return xmlSchemaSimpleTypeList.ItemTypeName;
				}
				if (xmlSchemaSimpleTypeList.ItemType != null)
				{
					return XmlSchemaImporter.BaseTypeName(xmlSchemaSimpleTypeList.ItemType);
				}
			}
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000B4D60 File Offset: 0x000B2F60
		private TypeDesc GetDataTypeSource(XmlSchemaSimpleType dataType, TypeFlags flags)
		{
			TypeDesc typeDesc;
			if (dataType.Name != null && dataType.Name.Length != 0)
			{
				typeDesc = base.Scope.GetTypeDesc(dataType);
				if (typeDesc != null)
				{
					return typeDesc;
				}
			}
			XmlQualifiedName xmlQualifiedName = XmlSchemaImporter.BaseTypeName(dataType);
			base.AddReference(xmlQualifiedName, base.TypesInUse, "Type '{0}' from targetNamespace='{1}' has invalid definition: Circular type reference.");
			typeDesc = this.GetDataTypeSource(this.FindDataType(xmlQualifiedName, flags), flags);
			if (xmlQualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
			{
				base.RemoveReference(xmlQualifiedName, base.TypesInUse);
			}
			return typeDesc;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000B4DE0 File Offset: 0x000B2FE0
		private XmlSchemaSimpleType FindDataType(XmlQualifiedName name, TypeFlags flags)
		{
			if (name == null || name.IsEmpty)
			{
				return (XmlSchemaSimpleType)base.Scope.GetTypeDesc(typeof(string)).DataType;
			}
			TypeDesc typeDesc = base.Scope.GetTypeDesc(name.Name, name.Namespace, flags);
			if (typeDesc != null && typeDesc.DataType is XmlSchemaSimpleType)
			{
				return (XmlSchemaSimpleType)typeDesc.DataType;
			}
			XmlSchemaSimpleType xmlSchemaSimpleType = (XmlSchemaSimpleType)base.Schemas.Find(name, typeof(XmlSchemaSimpleType));
			if (xmlSchemaSimpleType != null)
			{
				return xmlSchemaSimpleType;
			}
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return (XmlSchemaSimpleType)base.Scope.GetTypeDesc("string", "http://www.w3.org/2001/XMLSchema", flags).DataType;
			}
			if (name.Name == "Array" && name.Namespace == "http://schemas.xmlsoap.org/soap/encoding/")
			{
				throw new InvalidOperationException(Res.GetString("Referenced type '{0}' is only valid for encoded SOAP.", new object[] { name.ToString() }));
			}
			throw new InvalidOperationException(Res.GetString("The datatype '{0}' is missing.", new object[] { name.ToString() }));
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000B4F08 File Offset: 0x000B3108
		private XmlSchemaType FindType(XmlQualifiedName name, TypeFlags flags)
		{
			if (name == null || name.IsEmpty)
			{
				return base.Scope.GetTypeDesc(typeof(string)).DataType;
			}
			object obj = base.Schemas.Find(name, typeof(XmlSchemaComplexType));
			if (obj != null)
			{
				return (XmlSchemaComplexType)obj;
			}
			return this.FindDataType(name, flags);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000B4F6C File Offset: 0x000B316C
		private XmlSchemaElement FindElement(XmlQualifiedName name)
		{
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)base.Schemas.Find(name, typeof(XmlSchemaElement));
			if (xmlSchemaElement == null)
			{
				throw new InvalidOperationException(Res.GetString("The element '{0}' is missing.", new object[] { name.ToString() }));
			}
			return xmlSchemaElement;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000B4FB8 File Offset: 0x000B31B8
		private XmlSchemaAttribute FindAttribute(XmlQualifiedName name)
		{
			XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)base.Schemas.Find(name, typeof(XmlSchemaAttribute));
			if (xmlSchemaAttribute == null)
			{
				throw new InvalidOperationException(Res.GetString("The attribute {0} is missing.", new object[] { name.Name }));
			}
			return xmlSchemaAttribute;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000B5004 File Offset: 0x000B3204
		private XmlSchemaForm ElementForm(string ns, XmlSchemaElement element)
		{
			if (element.Form != XmlSchemaForm.None)
			{
				return element.Form;
			}
			XmlSchemaObject xmlSchemaObject = element;
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			XmlSchema xmlSchema = xmlSchemaObject as XmlSchema;
			if (xmlSchema == null)
			{
				return XmlSchemaForm.Qualified;
			}
			if (ns == null || ns.Length == 0)
			{
				if (xmlSchema.ElementFormDefault != XmlSchemaForm.None)
				{
					return xmlSchema.ElementFormDefault;
				}
				return XmlSchemaForm.Unqualified;
			}
			else
			{
				XmlSchemas.Preprocess(xmlSchema);
				if (element.QualifiedName.Namespace != null && element.QualifiedName.Namespace.Length != 0)
				{
					return XmlSchemaForm.Qualified;
				}
				return XmlSchemaForm.Unqualified;
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x000B5084 File Offset: 0x000B3284
		internal string FindExtendedAnyElement(XmlSchemaAny any, bool mixed, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, out SchemaImporterExtension extension)
		{
			extension = null;
			foreach (object obj in base.Extensions)
			{
				SchemaImporterExtension schemaImporterExtension = (SchemaImporterExtension)obj;
				string text = schemaImporterExtension.ImportAnyElement(any, mixed, base.Schemas, this, compileUnit, mainNamespace, base.Options, base.CodeProvider);
				if (text != null && text.Length > 0)
				{
					extension = schemaImporterExtension;
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x000B5114 File Offset: 0x000B3314
		internal string FindExtendedType(string name, string ns, XmlSchemaObject context, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, out SchemaImporterExtension extension)
		{
			extension = null;
			foreach (object obj in base.Extensions)
			{
				SchemaImporterExtension schemaImporterExtension = (SchemaImporterExtension)obj;
				string text = schemaImporterExtension.ImportSchemaType(name, ns, context, base.Schemas, this, compileUnit, mainNamespace, base.Options, base.CodeProvider);
				if (text != null && text.Length > 0)
				{
					extension = schemaImporterExtension;
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x000B51A8 File Offset: 0x000B33A8
		internal string FindExtendedType(XmlSchemaType type, XmlSchemaObject context, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, out SchemaImporterExtension extension)
		{
			extension = null;
			foreach (object obj in base.Extensions)
			{
				SchemaImporterExtension schemaImporterExtension = (SchemaImporterExtension)obj;
				string text = schemaImporterExtension.ImportSchemaType(type, context, base.Schemas, this, compileUnit, mainNamespace, base.Options, base.CodeProvider);
				if (text != null && text.Length > 0)
				{
					extension = schemaImporterExtension;
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x000B5238 File Offset: 0x000B3438
		private XmlSchemaForm AttributeForm(string ns, XmlSchemaAttribute attribute)
		{
			if (attribute.Form != XmlSchemaForm.None)
			{
				return attribute.Form;
			}
			XmlSchemaObject xmlSchemaObject = attribute;
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			XmlSchema xmlSchema = xmlSchemaObject as XmlSchema;
			if (xmlSchema == null)
			{
				return XmlSchemaForm.Unqualified;
			}
			if (ns == null || ns.Length == 0)
			{
				if (xmlSchema.AttributeFormDefault != XmlSchemaForm.None)
				{
					return xmlSchema.AttributeFormDefault;
				}
				return XmlSchemaForm.Unqualified;
			}
			else
			{
				XmlSchemas.Preprocess(xmlSchema);
				if (attribute.QualifiedName.Namespace != null && attribute.QualifiedName.Namespace.Length != 0)
				{
					return XmlSchemaForm.Qualified;
				}
				return XmlSchemaForm.Unqualified;
			}
		}

		// Token: 0x02000343 RID: 835
		private class TypeItems
		{
			// Token: 0x04001779 RID: 6009
			internal XmlSchemaObjectCollection Attributes = new XmlSchemaObjectCollection();

			// Token: 0x0400177A RID: 6010
			internal XmlSchemaAnyAttribute AnyAttribute;

			// Token: 0x0400177B RID: 6011
			internal XmlSchemaGroupBase Particle;

			// Token: 0x0400177C RID: 6012
			internal XmlQualifiedName baseSimpleType;

			// Token: 0x0400177D RID: 6013
			internal bool IsUnbounded;
		}

		// Token: 0x02000344 RID: 836
		internal class ElementComparer : IComparer
		{
			// Token: 0x06002070 RID: 8304 RVA: 0x000B52CC File Offset: 0x000B34CC
			public int Compare(object o1, object o2)
			{
				Accessor accessor = (ElementAccessor)o1;
				ElementAccessor elementAccessor = (ElementAccessor)o2;
				return string.Compare(accessor.ToString(string.Empty), elementAccessor.ToString(string.Empty), StringComparison.Ordinal);
			}
		}
	}
}
