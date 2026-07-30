using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000419 RID: 1049
	internal class SchemaInfo : IDtdInfo
	{
		// Token: 0x0600293B RID: 10555 RVA: 0x000F915C File Offset: 0x000F735C
		internal SchemaInfo()
		{
			this.schemaType = SchemaType.None;
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000F91C3 File Offset: 0x000F73C3
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x000F91CB File Offset: 0x000F73CB
		public XmlQualifiedName DocTypeName
		{
			get
			{
				return this.docTypeName;
			}
			set
			{
				this.docTypeName = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x000F91D4 File Offset: 0x000F73D4
		// (set) Token: 0x0600293F RID: 10559 RVA: 0x000F91DC File Offset: 0x000F73DC
		internal string InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
			set
			{
				this.internalDtdSubset = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000F91E5 File Offset: 0x000F73E5
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDecls
		{
			get
			{
				return this.elementDecls;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000F91ED File Offset: 0x000F73ED
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> UndeclaredElementDecls
		{
			get
			{
				return this.undeclaredElementDecls;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x000F91F5 File Offset: 0x000F73F5
		internal Dictionary<XmlQualifiedName, SchemaEntity> GeneralEntities
		{
			get
			{
				if (this.generalEntities == null)
				{
					this.generalEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.generalEntities;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x000F9210 File Offset: 0x000F7410
		internal Dictionary<XmlQualifiedName, SchemaEntity> ParameterEntities
		{
			get
			{
				if (this.parameterEntities == null)
				{
					this.parameterEntities = new Dictionary<XmlQualifiedName, SchemaEntity>();
				}
				return this.parameterEntities;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x000F922B File Offset: 0x000F742B
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x000F9233 File Offset: 0x000F7433
		internal SchemaType SchemaType
		{
			get
			{
				return this.schemaType;
			}
			set
			{
				this.schemaType = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000F923C File Offset: 0x000F743C
		internal Dictionary<string, bool> TargetNamespaces
		{
			get
			{
				return this.targetNamespaces;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000F9244 File Offset: 0x000F7444
		internal Dictionary<XmlQualifiedName, SchemaElementDecl> ElementDeclsByType
		{
			get
			{
				return this.elementDeclsByType;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x000F924C File Offset: 0x000F744C
		internal Dictionary<XmlQualifiedName, SchemaAttDef> AttributeDecls
		{
			get
			{
				return this.attributeDecls;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000F9254 File Offset: 0x000F7454
		internal Dictionary<string, SchemaNotation> Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new Dictionary<string, SchemaNotation>();
				}
				return this.notations;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x000F926F File Offset: 0x000F746F
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x000F9277 File Offset: 0x000F7477
		internal int ErrorCount
		{
			get
			{
				return this.errorCount;
			}
			set
			{
				this.errorCount = value;
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000F9280 File Offset: 0x000F7480
		internal SchemaElementDecl GetElementDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl schemaElementDecl;
			if (this.elementDecls.TryGetValue(qname, out schemaElementDecl))
			{
				return schemaElementDecl;
			}
			return null;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000F92A0 File Offset: 0x000F74A0
		internal SchemaElementDecl GetTypeDecl(XmlQualifiedName qname)
		{
			SchemaElementDecl schemaElementDecl;
			if (this.elementDeclsByType.TryGetValue(qname, out schemaElementDecl))
			{
				return schemaElementDecl;
			}
			return null;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000F92C0 File Offset: 0x000F74C0
		internal XmlSchemaElement GetElement(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000F92E0 File Offset: 0x000F74E0
		internal XmlSchemaAttribute GetAttribute(XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = this.attributeDecls[qname];
			if (schemaAttDef != null)
			{
				return schemaAttDef.SchemaAttribute;
			}
			return null;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000F9308 File Offset: 0x000F7508
		internal XmlSchemaElement GetType(XmlQualifiedName qname)
		{
			SchemaElementDecl elementDecl = this.GetElementDecl(qname);
			if (elementDecl != null)
			{
				return elementDecl.SchemaElement;
			}
			return null;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000F9328 File Offset: 0x000F7528
		internal bool HasSchema(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000F9328 File Offset: 0x000F7528
		internal bool Contains(string ns)
		{
			return this.targetNamespaces.ContainsKey(ns);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000F9338 File Offset: 0x000F7538
		internal SchemaAttDef GetAttributeXdr(SchemaElementDecl ed, XmlQualifiedName qname)
		{
			SchemaAttDef schemaAttDef = null;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef == null)
				{
					if (!ed.ContentValidator.IsOpen || qname.Namespace.Length == 0)
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
					if (!this.attributeDecls.TryGetValue(qname, out schemaAttDef) && this.targetNamespaces.ContainsKey(qname.Namespace))
					{
						throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
					}
				}
			}
			return schemaAttDef;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000F93B8 File Offset: 0x000F75B8
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, XmlSchemaObject partialValidationType, out AttributeMatchState attributeMatchState)
		{
			SchemaAttDef schemaAttDef = null;
			attributeMatchState = AttributeMatchState.UndeclaredAttribute;
			if (ed != null)
			{
				schemaAttDef = ed.GetAttDef(qname);
				if (schemaAttDef != null)
				{
					attributeMatchState = AttributeMatchState.AttributeFound;
					return schemaAttDef;
				}
				XmlSchemaAnyAttribute anyAttribute = ed.AnyAttribute;
				if (anyAttribute != null)
				{
					if (!anyAttribute.NamespaceList.Allows(qname))
					{
						attributeMatchState = AttributeMatchState.ProhibitedAnyAttribute;
					}
					else if (anyAttribute.ProcessContentsCorrect != XmlSchemaContentProcessing.Skip)
					{
						if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
						{
							if (schemaAttDef.Datatype.TypeCode == XmlTypeCode.Id)
							{
								attributeMatchState = AttributeMatchState.AnyIdAttributeFound;
							}
							else
							{
								attributeMatchState = AttributeMatchState.AttributeFound;
							}
						}
						else if (anyAttribute.ProcessContentsCorrect == XmlSchemaContentProcessing.Lax)
						{
							attributeMatchState = AttributeMatchState.AnyAttributeLax;
						}
					}
					else
					{
						attributeMatchState = AttributeMatchState.AnyAttributeSkip;
					}
				}
				else if (ed.ProhibitedAttributes.ContainsKey(qname))
				{
					attributeMatchState = AttributeMatchState.ProhibitedAttribute;
				}
			}
			else if (partialValidationType != null)
			{
				XmlSchemaAttribute xmlSchemaAttribute = partialValidationType as XmlSchemaAttribute;
				if (xmlSchemaAttribute != null)
				{
					if (qname.Equals(xmlSchemaAttribute.QualifiedName))
					{
						schemaAttDef = xmlSchemaAttribute.AttDef;
						attributeMatchState = AttributeMatchState.AttributeFound;
					}
					else
					{
						attributeMatchState = AttributeMatchState.AttributeNameMismatch;
					}
				}
				else
				{
					attributeMatchState = AttributeMatchState.ValidateAttributeInvalidCall;
				}
			}
			else if (this.attributeDecls.TryGetValue(qname, out schemaAttDef))
			{
				attributeMatchState = AttributeMatchState.AttributeFound;
			}
			else
			{
				attributeMatchState = AttributeMatchState.UndeclaredElementAndAttribute;
			}
			return schemaAttDef;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000F94B0 File Offset: 0x000F76B0
		internal SchemaAttDef GetAttributeXsd(SchemaElementDecl ed, XmlQualifiedName qname, ref bool skip)
		{
			AttributeMatchState attributeMatchState;
			SchemaAttDef attributeXsd = this.GetAttributeXsd(ed, qname, null, out attributeMatchState);
			switch (attributeMatchState)
			{
			case AttributeMatchState.UndeclaredAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not declared.", qname.ToString());
			case AttributeMatchState.AnyAttributeSkip:
				skip = true;
				break;
			case AttributeMatchState.ProhibitedAnyAttribute:
			case AttributeMatchState.ProhibitedAttribute:
				throw new XmlSchemaException("The '{0}' attribute is not allowed.", qname.ToString());
			}
			return attributeXsd;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000F9518 File Offset: 0x000F7718
		internal void Add(SchemaInfo sinfo, ValidationEventHandler eventhandler)
		{
			if (this.schemaType == SchemaType.None)
			{
				this.schemaType = sinfo.SchemaType;
			}
			else if (this.schemaType != sinfo.SchemaType)
			{
				if (eventhandler != null)
				{
					eventhandler(this, new ValidationEventArgs(new XmlSchemaException("Different schema types cannot be mixed.", string.Empty)));
				}
				return;
			}
			foreach (string text in sinfo.TargetNamespaces.Keys)
			{
				if (!this.targetNamespaces.ContainsKey(text))
				{
					this.targetNamespaces.Add(text, true);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair in sinfo.elementDecls)
			{
				if (!this.elementDecls.ContainsKey(keyValuePair.Key))
				{
					this.elementDecls.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<XmlQualifiedName, SchemaElementDecl> keyValuePair2 in sinfo.elementDeclsByType)
			{
				if (!this.elementDeclsByType.ContainsKey(keyValuePair2.Key))
				{
					this.elementDeclsByType.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			foreach (SchemaAttDef schemaAttDef in sinfo.AttributeDecls.Values)
			{
				if (!this.attributeDecls.ContainsKey(schemaAttDef.Name))
				{
					this.attributeDecls.Add(schemaAttDef.Name, schemaAttDef);
				}
			}
			foreach (SchemaNotation schemaNotation in sinfo.Notations.Values)
			{
				if (!this.Notations.ContainsKey(schemaNotation.Name.Name))
				{
					this.Notations.Add(schemaNotation.Name.Name, schemaNotation);
				}
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000F9778 File Offset: 0x000F7978
		internal void Finish()
		{
			Dictionary<XmlQualifiedName, SchemaElementDecl> dictionary = this.elementDecls;
			for (int i = 0; i < 2; i++)
			{
				foreach (SchemaElementDecl schemaElementDecl in dictionary.Values)
				{
					if (schemaElementDecl.HasNonCDataAttribute)
					{
						this.hasNonCDataAttributes = true;
					}
					if (schemaElementDecl.DefaultAttDefs != null)
					{
						this.hasDefaultAttributes = true;
					}
				}
				dictionary = this.undeclaredElementDecls;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x000F97FC File Offset: 0x000F79FC
		bool IDtdInfo.HasDefaultAttributes
		{
			get
			{
				return this.hasDefaultAttributes;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x000F9804 File Offset: 0x000F7A04
		bool IDtdInfo.HasNonCDataAttributes
		{
			get
			{
				return this.hasNonCDataAttributes;
			}
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000F980C File Offset: 0x000F7A0C
		IDtdAttributeListInfo IDtdInfo.LookupAttributeList(string prefix, string localName)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(prefix, localName);
			SchemaElementDecl schemaElementDecl;
			if (!this.elementDecls.TryGetValue(xmlQualifiedName, out schemaElementDecl))
			{
				this.undeclaredElementDecls.TryGetValue(xmlQualifiedName, out schemaElementDecl);
			}
			return schemaElementDecl;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000F9841 File Offset: 0x000F7A41
		IEnumerable<IDtdAttributeListInfo> IDtdInfo.GetAttributeLists()
		{
			foreach (IDtdAttributeListInfo dtdAttributeListInfo in this.elementDecls.Values)
			{
				yield return dtdAttributeListInfo;
			}
			Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator enumerator = default(Dictionary<XmlQualifiedName, SchemaElementDecl>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000F9854 File Offset: 0x000F7A54
		IDtdEntityInfo IDtdInfo.LookupEntity(string name)
		{
			if (this.generalEntities == null)
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name);
			SchemaEntity schemaEntity;
			if (this.generalEntities.TryGetValue(xmlQualifiedName, out schemaEntity))
			{
				return schemaEntity;
			}
			return null;
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000F91C3 File Offset: 0x000F73C3
		XmlQualifiedName IDtdInfo.Name
		{
			get
			{
				return this.docTypeName;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600295E RID: 10590 RVA: 0x000F91D4 File Offset: 0x000F73D4
		string IDtdInfo.InternalDtdSubset
		{
			get
			{
				return this.internalDtdSubset;
			}
		}

		// Token: 0x04001B2A RID: 6954
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04001B2B RID: 6955
		private Dictionary<XmlQualifiedName, SchemaElementDecl> undeclaredElementDecls = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04001B2C RID: 6956
		private Dictionary<XmlQualifiedName, SchemaEntity> generalEntities;

		// Token: 0x04001B2D RID: 6957
		private Dictionary<XmlQualifiedName, SchemaEntity> parameterEntities;

		// Token: 0x04001B2E RID: 6958
		private XmlQualifiedName docTypeName = XmlQualifiedName.Empty;

		// Token: 0x04001B2F RID: 6959
		private string internalDtdSubset = string.Empty;

		// Token: 0x04001B30 RID: 6960
		private bool hasNonCDataAttributes;

		// Token: 0x04001B31 RID: 6961
		private bool hasDefaultAttributes;

		// Token: 0x04001B32 RID: 6962
		private Dictionary<string, bool> targetNamespaces = new Dictionary<string, bool>();

		// Token: 0x04001B33 RID: 6963
		private Dictionary<XmlQualifiedName, SchemaAttDef> attributeDecls = new Dictionary<XmlQualifiedName, SchemaAttDef>();

		// Token: 0x04001B34 RID: 6964
		private int errorCount;

		// Token: 0x04001B35 RID: 6965
		private SchemaType schemaType;

		// Token: 0x04001B36 RID: 6966
		private Dictionary<XmlQualifiedName, SchemaElementDecl> elementDeclsByType = new Dictionary<XmlQualifiedName, SchemaElementDecl>();

		// Token: 0x04001B37 RID: 6967
		private Dictionary<string, SchemaNotation> notations;
	}
}
