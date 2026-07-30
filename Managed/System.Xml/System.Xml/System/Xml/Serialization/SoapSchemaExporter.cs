using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Populates <see cref="T:System.Xml.Schema.XmlSchema" /> objects with XML Schema data type definitions for .NET Framework types that are serialized using SOAP encoding.</summary>
	// Token: 0x02000314 RID: 788
	public class SoapSchemaExporter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.SoapSchemaExporter" /> class, which supplies the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which XML Schema element declarations are to be added.</summary>
		/// <param name="schemas">A collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects to which element declarations obtained from type mappings are to be added.</param>
		// Token: 0x06001D7F RID: 7551 RVA: 0x000A1AE6 File Offset: 0x0009FCE6
		public SoapSchemaExporter(XmlSchemas schemas)
		{
			this.schemas = schemas;
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for a .NET Framework type.</summary>
		/// <param name="xmlTypeMapping">An internal mapping between a .NET Framework type and an XML Schema element.</param>
		// Token: 0x06001D80 RID: 7552 RVA: 0x000A1B00 File Offset: 0x0009FD00
		public void ExportTypeMapping(XmlTypeMapping xmlTypeMapping)
		{
			this.CheckScope(xmlTypeMapping.Scope);
			this.ExportTypeMapping(xmlTypeMapping.Mapping, null);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		// Token: 0x06001D81 RID: 7553 RVA: 0x000A1B1C File Offset: 0x0009FD1C
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping)
		{
			this.ExportMembersMapping(xmlMembersMapping, false);
		}

		/// <summary>Adds to the applicable <see cref="T:System.Xml.Schema.XmlSchema" /> object a data type definition for each of the element parts of a SOAP-encoded message definition.</summary>
		/// <param name="xmlMembersMapping">Internal .NET Framework type mappings for the element parts of a WSDL message definition.</param>
		/// <param name="exportEnclosingType">true to export a type definition for the parent element of the WSDL parts; otherwise, false.</param>
		// Token: 0x06001D82 RID: 7554 RVA: 0x000A1B28 File Offset: 0x0009FD28
		public void ExportMembersMapping(XmlMembersMapping xmlMembersMapping, bool exportEnclosingType)
		{
			this.CheckScope(xmlMembersMapping.Scope);
			MembersMapping membersMapping = (MembersMapping)xmlMembersMapping.Accessor.Mapping;
			if (exportEnclosingType)
			{
				this.ExportTypeMapping(membersMapping, null);
				return;
			}
			foreach (MemberMapping memberMapping in membersMapping.Members)
			{
				if (memberMapping.Elements.Length != 0)
				{
					this.ExportTypeMapping(memberMapping.Elements[0].Mapping, null);
				}
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000A1B96 File Offset: 0x0009FD96
		private void CheckScope(TypeScope scope)
		{
			if (this.scope == null)
			{
				this.scope = scope;
				return;
			}
			if (this.scope != scope)
			{
				throw new InvalidOperationException(Res.GetString("Exported mappings must come from the same importer."));
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000A1BC1 File Offset: 0x0009FDC1
		internal XmlDocument Document
		{
			get
			{
				if (this.document == null)
				{
					this.document = new XmlDocument();
				}
				return this.document;
			}
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000A1BDC File Offset: 0x0009FDDC
		private void CheckForDuplicateType(string newTypeName, string newNamespace)
		{
			XmlSchema xmlSchema = this.schemas[newNamespace];
			if (xmlSchema != null)
			{
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					XmlSchemaType xmlSchemaType = xmlSchemaObject as XmlSchemaType;
					if (xmlSchemaType != null && xmlSchemaType.Name == newTypeName)
					{
						throw new InvalidOperationException(Res.GetString("A type with the name {0} has already been added in namespace {1}.", new object[] { newTypeName, newNamespace }));
					}
				}
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000A1C70 File Offset: 0x0009FE70
		private void AddSchemaItem(XmlSchemaObject item, string ns, string referencingNs)
		{
			if (!this.SchemaContainsItem(item, ns))
			{
				XmlSchema xmlSchema = this.schemas[ns];
				if (xmlSchema == null)
				{
					xmlSchema = new XmlSchema();
					xmlSchema.TargetNamespace = ((ns == null || ns.Length == 0) ? null : ns);
					xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
					this.schemas.Add(xmlSchema);
				}
				xmlSchema.Items.Add(item);
			}
			if (referencingNs != null)
			{
				this.AddSchemaImport(ns, referencingNs);
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000A1CE0 File Offset: 0x0009FEE0
		private void AddSchemaImport(string ns, string referencingNs)
		{
			if (referencingNs == null || ns == null)
			{
				return;
			}
			if (ns == referencingNs)
			{
				return;
			}
			XmlSchema xmlSchema = this.schemas[referencingNs];
			if (xmlSchema == null)
			{
				throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[] { referencingNs }));
			}
			if (ns != null && ns.Length > 0 && this.FindImport(xmlSchema, ns) == null)
			{
				XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
				xmlSchemaImport.Namespace = ns;
				xmlSchema.Includes.Add(xmlSchemaImport);
			}
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000A1D58 File Offset: 0x0009FF58
		private bool SchemaContainsItem(XmlSchemaObject item, string ns)
		{
			XmlSchema xmlSchema = this.schemas[ns];
			return xmlSchema != null && xmlSchema.Items.Contains(item);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000A1D84 File Offset: 0x0009FF84
		private XmlSchemaImport FindImport(XmlSchema schema, string ns)
		{
			foreach (object obj in schema.Includes)
			{
				if (obj is XmlSchemaImport)
				{
					XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)obj;
					if (xmlSchemaImport.Namespace == ns)
					{
						return xmlSchemaImport;
					}
				}
			}
			return null;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000A1DF8 File Offset: 0x0009FFF8
		private XmlQualifiedName ExportTypeMapping(TypeMapping mapping, string ns)
		{
			if (mapping is ArrayMapping)
			{
				return this.ExportArrayMapping((ArrayMapping)mapping, ns);
			}
			if (mapping is EnumMapping)
			{
				return this.ExportEnumMapping((EnumMapping)mapping, ns);
			}
			if (mapping is PrimitiveMapping)
			{
				PrimitiveMapping primitiveMapping = (PrimitiveMapping)mapping;
				if (primitiveMapping.TypeDesc.IsXsdType)
				{
					return this.ExportPrimitiveMapping(primitiveMapping);
				}
				return this.ExportNonXsdPrimitiveMapping(primitiveMapping, ns);
			}
			else
			{
				if (mapping is StructMapping)
				{
					return this.ExportStructMapping((StructMapping)mapping, ns);
				}
				if (mapping is NullableMapping)
				{
					return this.ExportTypeMapping(((NullableMapping)mapping).BaseMapping, ns);
				}
				if (mapping is MembersMapping)
				{
					return this.ExportMembersMapping((MembersMapping)mapping, ns);
				}
				throw new ArgumentException(Res.GetString("Internal error."), "mapping");
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000A1EBC File Offset: 0x000A00BC
		private XmlQualifiedName ExportNonXsdPrimitiveMapping(PrimitiveMapping mapping, string ns)
		{
			XmlSchemaType dataType = mapping.TypeDesc.DataType;
			if (!this.SchemaContainsItem(dataType, "http://microsoft.com/wsdl/types/"))
			{
				this.AddSchemaItem(dataType, "http://microsoft.com/wsdl/types/", ns);
			}
			else
			{
				this.AddSchemaImport("http://microsoft.com/wsdl/types/", ns);
			}
			return new XmlQualifiedName(mapping.TypeDesc.DataType.Name, "http://microsoft.com/wsdl/types/");
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000A1F18 File Offset: 0x000A0118
		private XmlQualifiedName ExportPrimitiveMapping(PrimitiveMapping mapping)
		{
			return new XmlQualifiedName(mapping.TypeDesc.DataType.Name, "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000A1F34 File Offset: 0x000A0134
		private XmlQualifiedName ExportArrayMapping(ArrayMapping mapping, string ns)
		{
			while (mapping.Next != null)
			{
				mapping = mapping.Next;
			}
			if ((XmlSchemaComplexType)this.types[mapping] == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				this.AddSchemaImport("http://schemas.xmlsoap.org/soap/encoding/", mapping.Namespace);
				this.AddSchemaImport("http://schemas.xmlsoap.org/wsdl/", mapping.Namespace);
				XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
				XmlQualifiedName xmlQualifiedName = this.ExportTypeMapping(mapping.Elements[0].Mapping, mapping.Namespace);
				if (xmlQualifiedName.IsEmpty)
				{
					xmlQualifiedName = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
				}
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.RefName = SoapSchemaExporter.ArrayTypeQName;
				xmlSchemaAttribute.UnhandledAttributes = new XmlAttribute[]
				{
					new XmlAttribute("wsdl", "arrayType", "http://schemas.xmlsoap.org/wsdl/", this.Document)
					{
						Value = xmlQualifiedName.Namespace + ":" + xmlQualifiedName.Name + "[]"
					}
				};
				xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute);
				xmlSchemaComplexContentRestriction.BaseTypeName = SoapSchemaExporter.ArrayQName;
				xmlSchemaComplexType.ContentModel = new XmlSchemaComplexContent
				{
					Content = xmlSchemaComplexContentRestriction
				};
				if (xmlQualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this.AddSchemaImport(xmlQualifiedName.Namespace, mapping.Namespace);
				}
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(mapping.TypeName, mapping.Namespace);
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000A20D8 File Offset: 0x000A02D8
		private void ExportElementAccessors(XmlSchemaGroupBase group, ElementAccessor[] accessors, bool repeats, bool valueTypeOptional, string ns)
		{
			if (accessors.Length == 0)
			{
				return;
			}
			if (accessors.Length == 1)
			{
				this.ExportElementAccessor(group, accessors[0], repeats, valueTypeOptional, ns);
				return;
			}
			XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
			xmlSchemaChoice.MaxOccurs = (repeats ? decimal.MaxValue : 1m);
			xmlSchemaChoice.MinOccurs = (repeats ? 0 : 1);
			for (int i = 0; i < accessors.Length; i++)
			{
				this.ExportElementAccessor(xmlSchemaChoice, accessors[i], false, valueTypeOptional, ns);
			}
			if (xmlSchemaChoice.Items.Count > 0)
			{
				group.Items.Add(xmlSchemaChoice);
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000A216C File Offset: 0x000A036C
		private void ExportElementAccessor(XmlSchemaGroupBase group, ElementAccessor accessor, bool repeats, bool valueTypeOptional, string ns)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.MinOccurs = ((repeats || valueTypeOptional) ? 0 : 1);
			xmlSchemaElement.MaxOccurs = (repeats ? decimal.MaxValue : 1m);
			xmlSchemaElement.Name = accessor.Name;
			xmlSchemaElement.IsNillable = accessor.IsNullable || accessor.Mapping is NullableMapping;
			xmlSchemaElement.Form = XmlSchemaForm.Unqualified;
			xmlSchemaElement.SchemaTypeName = this.ExportTypeMapping(accessor.Mapping, accessor.Namespace);
			group.Items.Add(xmlSchemaElement);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000A2205 File Offset: 0x000A0405
		private XmlQualifiedName ExportRootMapping(StructMapping mapping)
		{
			if (!this.exportedRoot)
			{
				this.exportedRoot = true;
				this.ExportDerivedMappings(mapping);
			}
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000A222C File Offset: 0x000A042C
		private XmlQualifiedName ExportStructMapping(StructMapping mapping, string ns)
		{
			if (mapping.TypeDesc.IsRoot)
			{
				return this.ExportRootMapping(mapping);
			}
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.types[mapping];
			if (xmlSchemaComplexType == null)
			{
				if (!mapping.IncludeInSchema)
				{
					throw new InvalidOperationException(Res.GetString("The type {0} may not be exported to a schema because the IncludeInSchema property of the SoapType attribute is 'false'.", new object[] { mapping.TypeDesc.Name }));
				}
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				xmlSchemaComplexType.IsAbstract = mapping.TypeDesc.IsAbstract;
				if (mapping.BaseMapping != null && mapping.BaseMapping.IncludeInSchema)
				{
					XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
					xmlSchemaComplexContentExtension.BaseTypeName = this.ExportStructMapping(mapping.BaseMapping, mapping.Namespace);
					xmlSchemaComplexType.ContentModel = new XmlSchemaComplexContent
					{
						Content = xmlSchemaComplexContentExtension
					};
				}
				this.ExportTypeMembers(xmlSchemaComplexType, mapping.Members, mapping.Namespace);
				this.ExportDerivedMappings(mapping);
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(xmlSchemaComplexType.Name, mapping.Namespace);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000A2364 File Offset: 0x000A0564
		private XmlQualifiedName ExportMembersMapping(MembersMapping mapping, string ns)
		{
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)this.types[mapping];
			if (xmlSchemaComplexType == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaComplexType);
				this.AddSchemaItem(xmlSchemaComplexType, mapping.Namespace, ns);
				this.ExportTypeMembers(xmlSchemaComplexType, mapping.Members, mapping.Namespace);
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(xmlSchemaComplexType.Name, mapping.Namespace);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000A23F8 File Offset: 0x000A05F8
		private void ExportTypeMembers(XmlSchemaComplexType type, MemberMapping[] members, string ns)
		{
			XmlSchemaGroupBase xmlSchemaGroupBase = new XmlSchemaSequence();
			foreach (MemberMapping memberMapping in members)
			{
				if (memberMapping.Elements.Length != 0)
				{
					bool flag = memberMapping.CheckSpecified != SpecifiedAccessor.None || memberMapping.CheckShouldPersist || !memberMapping.TypeDesc.IsValueType;
					this.ExportElementAccessors(xmlSchemaGroupBase, memberMapping.Elements, false, flag, ns);
				}
			}
			if (xmlSchemaGroupBase.Items.Count > 0)
			{
				if (type.ContentModel != null)
				{
					if (type.ContentModel.Content is XmlSchemaComplexContentExtension)
					{
						((XmlSchemaComplexContentExtension)type.ContentModel.Content).Particle = xmlSchemaGroupBase;
						return;
					}
					if (type.ContentModel.Content is XmlSchemaComplexContentRestriction)
					{
						((XmlSchemaComplexContentRestriction)type.ContentModel.Content).Particle = xmlSchemaGroupBase;
						return;
					}
					throw new InvalidOperationException(Res.GetString("Invalid content {0}.", new object[] { type.ContentModel.Content.GetType().Name }));
				}
				else
				{
					type.Particle = xmlSchemaGroupBase;
				}
			}
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000A24FC File Offset: 0x000A06FC
		private void ExportDerivedMappings(StructMapping mapping)
		{
			for (StructMapping structMapping = mapping.DerivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
			{
				if (structMapping.IncludeInSchema)
				{
					this.ExportStructMapping(structMapping, mapping.TypeDesc.IsRoot ? null : mapping.Namespace);
				}
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000A2544 File Offset: 0x000A0744
		private XmlQualifiedName ExportEnumMapping(EnumMapping mapping, string ns)
		{
			if ((XmlSchemaSimpleType)this.types[mapping] == null)
			{
				this.CheckForDuplicateType(mapping.TypeName, mapping.Namespace);
				XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
				xmlSchemaSimpleType.Name = mapping.TypeName;
				this.types.Add(mapping, xmlSchemaSimpleType);
				this.AddSchemaItem(xmlSchemaSimpleType, mapping.Namespace, ns);
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
				xmlSchemaSimpleTypeRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
				for (int i = 0; i < mapping.Constants.Length; i++)
				{
					ConstantMapping constantMapping = mapping.Constants[i];
					XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
					xmlSchemaEnumerationFacet.Value = constantMapping.XmlName;
					xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
				}
				if (!mapping.IsFlags)
				{
					xmlSchemaSimpleType.Content = xmlSchemaSimpleTypeRestriction;
				}
				else
				{
					xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
					{
						ItemType = new XmlSchemaSimpleType
						{
							Content = xmlSchemaSimpleTypeRestriction
						}
					};
				}
			}
			else
			{
				this.AddSchemaImport(mapping.Namespace, ns);
			}
			return new XmlQualifiedName(mapping.TypeName, mapping.Namespace);
		}

		// Token: 0x040016A1 RID: 5793
		internal const XmlSchemaForm elementFormDefault = XmlSchemaForm.Qualified;

		// Token: 0x040016A2 RID: 5794
		private XmlSchemas schemas;

		// Token: 0x040016A3 RID: 5795
		private Hashtable types = new Hashtable();

		// Token: 0x040016A4 RID: 5796
		private bool exportedRoot;

		// Token: 0x040016A5 RID: 5797
		private TypeScope scope;

		// Token: 0x040016A6 RID: 5798
		private XmlDocument document;

		// Token: 0x040016A7 RID: 5799
		private static XmlQualifiedName ArrayQName = new XmlQualifiedName("Array", "http://schemas.xmlsoap.org/soap/encoding/");

		// Token: 0x040016A8 RID: 5800
		private static XmlQualifiedName ArrayTypeQName = new XmlQualifiedName("arrayType", "http://schemas.xmlsoap.org/soap/encoding/");
	}
}
