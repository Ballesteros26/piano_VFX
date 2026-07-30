using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002F4 RID: 756
	internal class SerializableMapping : SpecialMapping
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x0009ACAA File Offset: 0x00098EAA
		internal SerializableMapping()
		{
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0009ACB9 File Offset: 0x00098EB9
		internal SerializableMapping(MethodInfo getSchemaMethod, bool any, string ns)
		{
			this.getSchemaMethod = getSchemaMethod;
			this.any = any;
			base.Namespace = ns;
			this.needSchema = getSchemaMethod != null;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0009ACEA File Offset: 0x00098EEA
		internal SerializableMapping(XmlQualifiedName xsiType, XmlSchemaSet schemas)
		{
			this.xsiType = xsiType;
			this.schemas = schemas;
			base.TypeName = xsiType.Name;
			base.Namespace = xsiType.Namespace;
			this.needSchema = false;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0009AD28 File Offset: 0x00098F28
		internal void SetBaseMapping(SerializableMapping mapping)
		{
			this.baseMapping = mapping;
			if (this.baseMapping != null)
			{
				this.nextDerivedMapping = this.baseMapping.derivedMappings;
				this.baseMapping.derivedMappings = this;
				if (this == this.nextDerivedMapping)
				{
					throw new InvalidOperationException(Res.GetString("Circular reference in derivation of IXmlSerializable type '{0}'.", new object[] { base.TypeDesc.FullName }));
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0009AD90 File Offset: 0x00098F90
		internal bool IsAny
		{
			get
			{
				if (this.any)
				{
					return true;
				}
				if (this.getSchemaMethod == null)
				{
					return false;
				}
				if (this.needSchema && typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return false;
				}
				this.RetrieveSerializableSchema();
				return this.any;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x0009ADEC File Offset: 0x00098FEC
		internal string NamespaceList
		{
			get
			{
				this.RetrieveSerializableSchema();
				if (this.namespaces == null)
				{
					if (this.schemas != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in this.schemas.Schemas())
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							if (xmlSchema.TargetNamespace != null && xmlSchema.TargetNamespace.Length > 0)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append(xmlSchema.TargetNamespace);
							}
						}
						this.namespaces = stringBuilder.ToString();
					}
					else
					{
						this.namespaces = string.Empty;
					}
				}
				return this.namespaces;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0009AEBC File Offset: 0x000990BC
		internal SerializableMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0009AEC4 File Offset: 0x000990C4
		internal SerializableMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0009AECC File Offset: 0x000990CC
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0009AED4 File Offset: 0x000990D4
		internal SerializableMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0009AEDD File Offset: 0x000990DD
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x0009AEE5 File Offset: 0x000990E5
		internal Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0009AEEE File Offset: 0x000990EE
		internal XmlSchemaSet Schemas
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schemas;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0009AEFC File Offset: 0x000990FC
		internal XmlSchema Schema
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schema;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0009AF0C File Offset: 0x0009910C
		internal XmlQualifiedName XsiType
		{
			get
			{
				if (!this.needSchema)
				{
					return this.xsiType;
				}
				if (this.getSchemaMethod == null)
				{
					return null;
				}
				if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return null;
				}
				this.RetrieveSerializableSchema();
				return this.xsiType;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x0009AF62 File Offset: 0x00099162
		internal XmlSchemaType XsdType
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.xsdType;
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0009AF70 File Offset: 0x00099170
		internal static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
		{
			if (args.Severity == XmlSeverityType.Error)
			{
				throw new InvalidOperationException(Res.GetString("Schema type information provided by {0} is invalid: {1}", new object[]
				{
					typeof(IXmlSerializable).Name,
					args.Message
				}));
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0009AFAC File Offset: 0x000991AC
		internal void CheckDuplicateElement(XmlSchemaElement element, string elementNs)
		{
			if (element == null)
			{
				return;
			}
			if (element.Parent == null || !(element.Parent is XmlSchema))
			{
				return;
			}
			XmlSchemaObjectTable xmlSchemaObjectTable;
			if (this.Schema != null && this.Schema.TargetNamespace == elementNs)
			{
				XmlSchemas.Preprocess(this.Schema);
				xmlSchemaObjectTable = this.Schema.Elements;
			}
			else
			{
				if (this.Schemas == null)
				{
					return;
				}
				xmlSchemaObjectTable = this.Schemas.GlobalElements;
			}
			foreach (object obj in xmlSchemaObjectTable.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (xmlSchemaElement.Name == element.Name && xmlSchemaElement.QualifiedName.Namespace == elementNs)
				{
					if (this.Match(xmlSchemaElement, element))
					{
						break;
					}
					throw new InvalidOperationException(Res.GetString("Cannot reconcile schema for '{0}'. Please use [XmlRoot] attribute to change default name or namespace of the top-level element to avoid duplicate element declarations: element name='{1}' namespace='{2}'.", new object[]
					{
						this.getSchemaMethod.DeclaringType.FullName,
						xmlSchemaElement.Name,
						elementNs
					}));
				}
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0009B0D0 File Offset: 0x000992D0
		private bool Match(XmlSchemaElement e1, XmlSchemaElement e2)
		{
			return e1.IsNillable == e2.IsNillable && !(e1.RefName != e2.RefName) && e1.SchemaType == e2.SchemaType && !(e1.SchemaTypeName != e2.SchemaTypeName) && !(e1.MinOccurs != e2.MinOccurs) && !(e1.MaxOccurs != e2.MaxOccurs) && e1.IsAbstract == e2.IsAbstract && !(e1.DefaultValue != e2.DefaultValue) && !(e1.SubstitutionGroup != e2.SubstitutionGroup);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0009B18C File Offset: 0x0009938C
		private void RetrieveSerializableSchema()
		{
			if (this.needSchema)
			{
				this.needSchema = false;
				if (this.getSchemaMethod != null)
				{
					if (this.schemas == null)
					{
						this.schemas = new XmlSchemaSet();
					}
					object obj = this.getSchemaMethod.Invoke(null, new object[] { this.schemas });
					this.xsiType = XmlQualifiedName.Empty;
					if (obj != null)
					{
						if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
						{
							this.xsdType = (XmlSchemaType)obj;
							this.xsiType = this.xsdType.QualifiedName;
						}
						else
						{
							if (!typeof(XmlQualifiedName).IsAssignableFrom(this.getSchemaMethod.ReturnType))
							{
								throw new InvalidOperationException(Res.GetString("Method {0}.{1}() specified by {2} has invalid signature: return type must be compatible with {3}.", new object[]
								{
									this.type.Name,
									this.getSchemaMethod.Name,
									typeof(XmlSchemaProviderAttribute).Name,
									typeof(XmlQualifiedName).FullName
								}));
							}
							this.xsiType = (XmlQualifiedName)obj;
							if (this.xsiType.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name.", new object[]
								{
									this.type.FullName,
									this.getSchemaMethod.Name
								}));
							}
						}
					}
					else
					{
						this.any = true;
					}
					this.schemas.ValidationEventHandler += SerializableMapping.ValidationCallbackWithErrorCode;
					this.schemas.Compile();
					if (!this.xsiType.IsEmpty && this.xsiType.Namespace != "http://www.w3.org/2001/XMLSchema")
					{
						ArrayList arrayList = (ArrayList)this.schemas.Schemas(this.xsiType.Namespace);
						if (arrayList.Count == 0)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[] { this.xsiType.Namespace }));
						}
						if (arrayList.Count > 1)
						{
							throw new InvalidOperationException(Res.GetString("Multiple schemas with targetNamespace='{0}' returned by {1}.{2}().  Please use only the main (parent) schema, and add the others to the schema Includes.", new object[]
							{
								this.xsiType.Namespace,
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name
							}));
						}
						XmlSchema xmlSchema = (XmlSchema)arrayList[0];
						if (xmlSchema == null)
						{
							throw new InvalidOperationException(Res.GetString("Missing schema targetNamespace=\"{0}\".", new object[] { this.xsiType.Namespace }));
						}
						this.xsdType = (XmlSchemaType)xmlSchema.SchemaTypes[this.xsiType];
						if (this.xsdType == null)
						{
							throw new InvalidOperationException(Res.GetString("{0}.{1}() must return a valid type name. Type '{2}' cannot be found in the targetNamespace='{3}'.", new object[]
							{
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name,
								this.xsiType.Name,
								this.xsiType.Namespace
							}));
						}
						this.xsdType = ((this.xsdType.Redefined != null) ? this.xsdType.Redefined : this.xsdType);
						return;
					}
				}
				else
				{
					IXmlSerializable xmlSerializable = (IXmlSerializable)Activator.CreateInstance(this.type);
					this.schema = xmlSerializable.GetSchema();
					if (this.schema != null && (this.schema.Id == null || this.schema.Id.Length == 0))
					{
						throw new InvalidOperationException(Res.GetString("Schema Id is missing. The schema returned from {0}.GetSchema() must have an Id.", new object[] { this.type.FullName }));
					}
				}
			}
		}

		// Token: 0x0400163E RID: 5694
		private XmlSchema schema;

		// Token: 0x0400163F RID: 5695
		private Type type;

		// Token: 0x04001640 RID: 5696
		private bool needSchema = true;

		// Token: 0x04001641 RID: 5697
		private MethodInfo getSchemaMethod;

		// Token: 0x04001642 RID: 5698
		private XmlQualifiedName xsiType;

		// Token: 0x04001643 RID: 5699
		private XmlSchemaType xsdType;

		// Token: 0x04001644 RID: 5700
		private XmlSchemaSet schemas;

		// Token: 0x04001645 RID: 5701
		private bool any;

		// Token: 0x04001646 RID: 5702
		private string namespaces;

		// Token: 0x04001647 RID: 5703
		private SerializableMapping baseMapping;

		// Token: 0x04001648 RID: 5704
		private SerializableMapping derivedMappings;

		// Token: 0x04001649 RID: 5705
		private SerializableMapping nextDerivedMapping;

		// Token: 0x0400164A RID: 5706
		private SerializableMapping next;
	}
}
