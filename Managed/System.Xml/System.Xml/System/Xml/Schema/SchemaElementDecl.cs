using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000416 RID: 1046
	internal sealed class SchemaElementDecl : SchemaDeclBase, IDtdAttributeListInfo
	{
		// Token: 0x060028ED RID: 10477 RVA: 0x000F8C50 File Offset: 0x000F6E50
		internal SchemaElementDecl()
		{
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000F8C6E File Offset: 0x000F6E6E
		internal SchemaElementDecl(XmlSchemaDatatype dtype)
		{
			base.Datatype = dtype;
			this.contentValidator = ContentValidator.TextOnly;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000F8C9E File Offset: 0x000F6E9E
		internal SchemaElementDecl(XmlQualifiedName name, string prefix)
			: base(name, prefix)
		{
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000F8CBE File Offset: 0x000F6EBE
		internal static SchemaElementDecl CreateAnyTypeElementDecl()
		{
			return new SchemaElementDecl
			{
				Datatype = DatatypeImplementation.AnySimpleType.Datatype
			};
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000F147C File Offset: 0x000EF67C
		string IDtdAttributeListInfo.Prefix
		{
			get
			{
				return base.Prefix;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000F1484 File Offset: 0x000EF684
		string IDtdAttributeListInfo.LocalName
		{
			get
			{
				return base.Name.Name;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x000F8CD5 File Offset: 0x000F6ED5
		bool IDtdAttributeListInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000F8CE0 File Offset: 0x000F6EE0
		IDtdAttributeInfo IDtdAttributeListInfo.LookupAttribute(string prefix, string localName)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(localName, prefix);
			SchemaAttDef schemaAttDef;
			if (this.attdefs.TryGetValue(xmlQualifiedName, out schemaAttDef))
			{
				return schemaAttDef;
			}
			return null;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000F8D08 File Offset: 0x000F6F08
		IEnumerable<IDtdDefaultAttributeInfo> IDtdAttributeListInfo.LookupDefaultAttributes()
		{
			return this.defaultAttdefs;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000F8D10 File Offset: 0x000F6F10
		IDtdAttributeInfo IDtdAttributeListInfo.LookupIdAttribute()
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (schemaAttDef.TokenizedType == XmlTokenizedType.ID)
				{
					return schemaAttDef;
				}
			}
			return null;
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x000F8D74 File Offset: 0x000F6F74
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x000F8D7C File Offset: 0x000F6F7C
		internal bool IsIdDeclared
		{
			get
			{
				return this.isIdDeclared;
			}
			set
			{
				this.isIdDeclared = value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000F8CD5 File Offset: 0x000F6ED5
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x000F8D85 File Offset: 0x000F6F85
		internal bool HasNonCDataAttribute
		{
			get
			{
				return this.hasNonCDataAttribute;
			}
			set
			{
				this.hasNonCDataAttribute = value;
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x000F8D8E File Offset: 0x000F6F8E
		internal SchemaElementDecl Clone()
		{
			return (SchemaElementDecl)base.MemberwiseClone();
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000F8D9B File Offset: 0x000F6F9B
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000F8DA3 File Offset: 0x000F6FA3
		internal bool IsAbstract
		{
			get
			{
				return this.isAbstract;
			}
			set
			{
				this.isAbstract = value;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000F8DAC File Offset: 0x000F6FAC
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x000F8DB4 File Offset: 0x000F6FB4
		internal bool IsNillable
		{
			get
			{
				return this.isNillable;
			}
			set
			{
				this.isNillable = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000F8DBD File Offset: 0x000F6FBD
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x000F8DC5 File Offset: 0x000F6FC5
		internal XmlSchemaDerivationMethod Block
		{
			get
			{
				return this.block;
			}
			set
			{
				this.block = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000F8DCE File Offset: 0x000F6FCE
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x000F8DD6 File Offset: 0x000F6FD6
		internal bool IsNotationDeclared
		{
			get
			{
				return this.isNotationDeclared;
			}
			set
			{
				this.isNotationDeclared = value;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000F8DDF File Offset: 0x000F6FDF
		internal bool HasDefaultAttribute
		{
			get
			{
				return this.defaultAttdefs != null;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000F8DEA File Offset: 0x000F6FEA
		// (set) Token: 0x06002906 RID: 10502 RVA: 0x000F8DF2 File Offset: 0x000F6FF2
		internal bool HasRequiredAttribute
		{
			get
			{
				return this.hasRequiredAttribute;
			}
			set
			{
				this.hasRequiredAttribute = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000F8DFB File Offset: 0x000F6FFB
		// (set) Token: 0x06002908 RID: 10504 RVA: 0x000F8E03 File Offset: 0x000F7003
		internal ContentValidator ContentValidator
		{
			get
			{
				return this.contentValidator;
			}
			set
			{
				this.contentValidator = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x000F8E0C File Offset: 0x000F700C
		// (set) Token: 0x0600290A RID: 10506 RVA: 0x000F8E14 File Offset: 0x000F7014
		internal XmlSchemaAnyAttribute AnyAttribute
		{
			get
			{
				return this.anyAttribute;
			}
			set
			{
				this.anyAttribute = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x000F8E1D File Offset: 0x000F701D
		// (set) Token: 0x0600290C RID: 10508 RVA: 0x000F8E25 File Offset: 0x000F7025
		internal CompiledIdentityConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
			set
			{
				this.constraints = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x000F8E2E File Offset: 0x000F702E
		// (set) Token: 0x0600290E RID: 10510 RVA: 0x000F8E36 File Offset: 0x000F7036
		internal XmlSchemaElement SchemaElement
		{
			get
			{
				return this.schemaElement;
			}
			set
			{
				this.schemaElement = value;
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000F8E40 File Offset: 0x000F7040
		internal void AddAttDef(SchemaAttDef attdef)
		{
			this.attdefs.Add(attdef.Name, attdef);
			if (attdef.Presence == SchemaDeclBase.Use.Required || attdef.Presence == SchemaDeclBase.Use.RequiredFixed)
			{
				this.hasRequiredAttribute = true;
			}
			if (attdef.Presence == SchemaDeclBase.Use.Default || attdef.Presence == SchemaDeclBase.Use.Fixed)
			{
				if (this.defaultAttdefs == null)
				{
					this.defaultAttdefs = new List<IDtdDefaultAttributeInfo>();
				}
				this.defaultAttdefs.Add(attdef);
			}
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000F8EA8 File Offset: 0x000F70A8
		internal SchemaAttDef GetAttDef(XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef;
			if (this.attdefs.TryGetValue(qname, out schemaAttDef))
			{
				return schemaAttDef;
			}
			return null;
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x000F8D08 File Offset: 0x000F6F08
		internal IList<IDtdDefaultAttributeInfo> DefaultAttDefs
		{
			get
			{
				return this.defaultAttdefs;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x000F8EC8 File Offset: 0x000F70C8
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttDefs
		{
			get
			{
				return this.attdefs;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x000F8ED0 File Offset: 0x000F70D0
		internal Dictionary<XmlQualifiedName, XmlQualifiedName> ProhibitedAttributes
		{
			get
			{
				return this.prohibitedAttributes;
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000F8ED8 File Offset: 0x000F70D8
		internal void CheckAttributes(Hashtable presence, bool standalone)
		{
			foreach (SchemaAttDef schemaAttDef in this.attdefs.Values)
			{
				if (presence[schemaAttDef.Name] == null)
				{
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Required)
					{
						throw new XmlSchemaException("The required attribute '{0}' is missing.", schemaAttDef.Name.ToString());
					}
					if (standalone && schemaAttDef.IsDeclaredInExternal && (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed))
					{
						throw new XmlSchemaException("The standalone document declaration must have a value of 'no'.", string.Empty);
					}
				}
			}
		}

		// Token: 0x04001B03 RID: 6915
		private Dictionary<XmlQualifiedName, SchemaAttDef> attdefs = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04001B04 RID: 6916
		private List<IDtdDefaultAttributeInfo> defaultAttdefs;

		// Token: 0x04001B05 RID: 6917
		private bool isIdDeclared;

		// Token: 0x04001B06 RID: 6918
		private bool hasNonCDataAttribute;

		// Token: 0x04001B07 RID: 6919
		private bool isAbstract;

		// Token: 0x04001B08 RID: 6920
		private bool isNillable;

		// Token: 0x04001B09 RID: 6921
		private bool hasRequiredAttribute;

		// Token: 0x04001B0A RID: 6922
		private bool isNotationDeclared;

		// Token: 0x04001B0B RID: 6923
		private Dictionary<XmlQualifiedName, XmlQualifiedName> prohibitedAttributes = new Dictionary<XmlQualifiedName, XmlQualifiedName>();

		// Token: 0x04001B0C RID: 6924
		private ContentValidator contentValidator;

		// Token: 0x04001B0D RID: 6925
		private XmlSchemaAnyAttribute anyAttribute;

		// Token: 0x04001B0E RID: 6926
		private XmlSchemaDerivationMethod block;

		// Token: 0x04001B0F RID: 6927
		private CompiledIdentityConstraint[] constraints;

		// Token: 0x04001B10 RID: 6928
		private XmlSchemaElement schemaElement;

		// Token: 0x04001B11 RID: 6929
		internal static readonly SchemaElementDecl Empty = new SchemaElementDecl();
	}
}
