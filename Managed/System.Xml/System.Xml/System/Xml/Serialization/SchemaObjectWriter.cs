using System;
using System.Collections;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000309 RID: 777
	internal class SchemaObjectWriter
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x0009D4B0 File Offset: 0x0009B6B0
		private void WriteIndent()
		{
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.w.Append(" ");
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0009D4E0 File Offset: 0x0009B6E0
		protected void WriteAttribute(string localName, string ns, string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.w.Append(",");
			this.w.Append(ns);
			if (ns != null && ns.Length != 0)
			{
				this.w.Append(":");
			}
			this.w.Append(localName);
			this.w.Append("=");
			this.w.Append(value);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0009D55E File Offset: 0x0009B75E
		protected void WriteAttribute(string localName, string ns, XmlQualifiedName value)
		{
			if (value.IsEmpty)
			{
				return;
			}
			this.WriteAttribute(localName, ns, value.ToString());
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0009D577 File Offset: 0x0009B777
		protected void WriteStartElement(string name)
		{
			this.NewLine();
			this.indentLevel++;
			this.w.Append("[");
			this.w.Append(name);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0009D5AB File Offset: 0x0009B7AB
		protected void WriteEndElement()
		{
			this.w.Append("]");
			this.indentLevel--;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0009D5CC File Offset: 0x0009B7CC
		protected void NewLine()
		{
			this.w.Append(Environment.NewLine);
			this.WriteIndent();
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0009D5E5 File Offset: 0x0009B7E5
		protected string GetString()
		{
			return this.w.ToString();
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0009D5F2 File Offset: 0x0009B7F2
		private void WriteAttribute(XmlAttribute a)
		{
			if (a.Value != null)
			{
				this.WriteAttribute(a.Name, a.NamespaceURI, a.Value);
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0009D614 File Offset: 0x0009B814
		private void WriteAttributes(XmlAttribute[] a, XmlSchemaObject o)
		{
			if (a == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < a.Length; i++)
			{
				arrayList.Add(a[i]);
			}
			arrayList.Sort(new XmlAttributeComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)arrayList[j];
				this.WriteAttribute(xmlAttribute);
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0009D674 File Offset: 0x0009B874
		internal static string ToString(NamespaceList list)
		{
			if (list == null)
			{
				return null;
			}
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				return "##any";
			case NamespaceList.ListType.Other:
				return "##other";
			case NamespaceList.ListType.Set:
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in list.Enumerate)
				{
					string text = (string)obj;
					arrayList.Add(text);
				}
				arrayList.Sort();
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (object obj2 in arrayList)
				{
					string text2 = (string)obj2;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					if (text2.Length == 0)
					{
						stringBuilder.Append("##local");
					}
					else
					{
						stringBuilder.Append(text2);
					}
				}
				return stringBuilder.ToString();
			}
			default:
				return list.ToString();
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0009D7A0 File Offset: 0x0009B9A0
		internal string WriteXmlSchemaObject(XmlSchemaObject o)
		{
			if (o == null)
			{
				return string.Empty;
			}
			this.Write3_XmlSchemaObject(o);
			return this.GetString();
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0009D7B8 File Offset: 0x0009B9B8
		private void WriteSortedItems(XmlSchemaObjectCollection items)
		{
			if (items == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < items.Count; i++)
			{
				arrayList.Add(items[i]);
			}
			arrayList.Sort(new XmlSchemaObjectComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				this.Write3_XmlSchemaObject((XmlSchemaObject)arrayList[j]);
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0009D81C File Offset: 0x0009BA1C
		private void Write1_XmlSchemaAttribute(XmlSchemaAttribute o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attribute");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("default", "", o.DefaultValue);
			this.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Parent != null && !(o.Parent is XmlSchema))
			{
				if (o.QualifiedName != null && !o.QualifiedName.IsEmpty && o.QualifiedName.Namespace != null && o.QualifiedName.Namespace.Length != 0)
				{
					this.WriteAttribute("form", "", "qualified");
				}
				else
				{
					this.WriteAttribute("form", "", "unqualified");
				}
			}
			this.WriteAttribute("name", "", o.Name);
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			else if (!o.SchemaTypeName.IsEmpty)
			{
				this.WriteAttribute("type", "", o.SchemaTypeName);
			}
			XmlSchemaUse xmlSchemaUse = ((o.Use == XmlSchemaUse.None) ? XmlSchemaUse.Optional : o.Use);
			this.WriteAttribute("use", "", this.Write30_XmlSchemaUse(xmlSchemaUse));
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.SchemaType);
			this.WriteEndElement();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0009D9A8 File Offset: 0x0009BBA8
		private void Write3_XmlSchemaObject(XmlSchemaObject o)
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			if (type == typeof(XmlSchemaComplexType))
			{
				this.Write35_XmlSchemaComplexType((XmlSchemaComplexType)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleType))
			{
				this.Write9_XmlSchemaSimpleType((XmlSchemaSimpleType)o);
				return;
			}
			if (type == typeof(XmlSchemaElement))
			{
				this.Write46_XmlSchemaElement((XmlSchemaElement)o);
				return;
			}
			if (type == typeof(XmlSchemaAppInfo))
			{
				this.Write7_XmlSchemaAppInfo((XmlSchemaAppInfo)o);
				return;
			}
			if (type == typeof(XmlSchemaDocumentation))
			{
				this.Write6_XmlSchemaDocumentation((XmlSchemaDocumentation)o);
				return;
			}
			if (type == typeof(XmlSchemaAnnotation))
			{
				this.Write5_XmlSchemaAnnotation((XmlSchemaAnnotation)o);
				return;
			}
			if (type == typeof(XmlSchemaGroup))
			{
				this.Write57_XmlSchemaGroup((XmlSchemaGroup)o);
				return;
			}
			if (type == typeof(XmlSchemaXPath))
			{
				this.Write49_XmlSchemaXPath("xpath", "", (XmlSchemaXPath)o);
				return;
			}
			if (type == typeof(XmlSchemaIdentityConstraint))
			{
				this.Write48_XmlSchemaIdentityConstraint((XmlSchemaIdentityConstraint)o);
				return;
			}
			if (type == typeof(XmlSchemaUnique))
			{
				this.Write51_XmlSchemaUnique((XmlSchemaUnique)o);
				return;
			}
			if (type == typeof(XmlSchemaKeyref))
			{
				this.Write50_XmlSchemaKeyref((XmlSchemaKeyref)o);
				return;
			}
			if (type == typeof(XmlSchemaKey))
			{
				this.Write47_XmlSchemaKey((XmlSchemaKey)o);
				return;
			}
			if (type == typeof(XmlSchemaGroupRef))
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o);
				return;
			}
			if (type == typeof(XmlSchemaAny))
			{
				this.Write53_XmlSchemaAny((XmlSchemaAny)o);
				return;
			}
			if (type == typeof(XmlSchemaSequence))
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o);
				return;
			}
			if (type == typeof(XmlSchemaChoice))
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o);
				return;
			}
			if (type == typeof(XmlSchemaAll))
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContentRestriction))
			{
				this.Write56_XmlSchemaComplexContentRestriction((XmlSchemaComplexContentRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContentExtension))
			{
				this.Write42_XmlSchemaComplexContentExtension((XmlSchemaComplexContentExtension)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContentRestriction))
			{
				this.Write40_XmlSchemaSimpleContentRestriction((XmlSchemaSimpleContentRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContentExtension))
			{
				this.Write38_XmlSchemaSimpleContentExtension((XmlSchemaSimpleContentExtension)o);
				return;
			}
			if (type == typeof(XmlSchemaComplexContent))
			{
				this.Write41_XmlSchemaComplexContent((XmlSchemaComplexContent)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleContent))
			{
				this.Write36_XmlSchemaSimpleContent((XmlSchemaSimpleContent)o);
				return;
			}
			if (type == typeof(XmlSchemaAnyAttribute))
			{
				this.Write33_XmlSchemaAnyAttribute((XmlSchemaAnyAttribute)o);
				return;
			}
			if (type == typeof(XmlSchemaAttributeGroupRef))
			{
				this.Write32_XmlSchemaAttributeGroupRef((XmlSchemaAttributeGroupRef)o);
				return;
			}
			if (type == typeof(XmlSchemaAttributeGroup))
			{
				this.Write31_XmlSchemaAttributeGroup((XmlSchemaAttributeGroup)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeRestriction))
			{
				this.Write15_XmlSchemaSimpleTypeRestriction((XmlSchemaSimpleTypeRestriction)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeList))
			{
				this.Write14_XmlSchemaSimpleTypeList((XmlSchemaSimpleTypeList)o);
				return;
			}
			if (type == typeof(XmlSchemaSimpleTypeUnion))
			{
				this.Write12_XmlSchemaSimpleTypeUnion((XmlSchemaSimpleTypeUnion)o);
				return;
			}
			if (type == typeof(XmlSchemaAttribute))
			{
				this.Write1_XmlSchemaAttribute((XmlSchemaAttribute)o);
				return;
			}
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0009DD6C File Offset: 0x0009BF6C
		private void Write5_XmlSchemaAnnotation(XmlSchemaAnnotation o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("annotation");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = items[i];
					if (xmlSchemaObject is XmlSchemaAppInfo)
					{
						this.Write7_XmlSchemaAppInfo((XmlSchemaAppInfo)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaDocumentation)
					{
						this.Write6_XmlSchemaDocumentation((XmlSchemaDocumentation)xmlSchemaObject);
					}
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x0009DE00 File Offset: 0x0009C000
		private void Write6_XmlSchemaDocumentation(XmlSchemaDocumentation o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("documentation");
			this.WriteAttribute("source", "", o.Source);
			this.WriteAttribute("lang", "http://www.w3.org/XML/1998/namespace", o.Language);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					this.WriteStartElement("node");
					this.WriteAttribute("xml", "", xmlNode.OuterXml);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0009DE8C File Offset: 0x0009C08C
		private void Write7_XmlSchemaAppInfo(XmlSchemaAppInfo o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("appinfo");
			this.WriteAttribute("source", "", o.Source);
			XmlNode[] markup = o.Markup;
			if (markup != null)
			{
				foreach (XmlNode xmlNode in markup)
				{
					this.WriteStartElement("node");
					this.WriteAttribute("xml", "", xmlNode.OuterXml);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0009DF04 File Offset: 0x0009C104
		private void Write9_XmlSchemaSimpleType(XmlSchemaSimpleType o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("simpleType");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaSimpleTypeUnion)
			{
				this.Write12_XmlSchemaSimpleTypeUnion((XmlSchemaSimpleTypeUnion)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleTypeRestriction)
			{
				this.Write15_XmlSchemaSimpleTypeRestriction((XmlSchemaSimpleTypeRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleTypeList)
			{
				this.Write14_XmlSchemaSimpleTypeList((XmlSchemaSimpleTypeList)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0009DFE5 File Offset: 0x0009C1E5
		private string Write11_XmlSchemaDerivationMethod(XmlSchemaDerivationMethod v)
		{
			return v.ToString();
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0009DFF4 File Offset: 0x0009C1F4
		private void Write12_XmlSchemaSimpleTypeUnion(XmlSchemaSimpleTypeUnion o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("union");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (o.MemberTypes != null)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < o.MemberTypes.Length; i++)
				{
					arrayList.Add(o.MemberTypes[i]);
				}
				arrayList.Sort(new QNameComparer());
				this.w.Append(",");
				this.w.Append("memberTypes=");
				for (int j = 0; j < arrayList.Count; j++)
				{
					XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)arrayList[j];
					this.w.Append(xmlQualifiedName.ToString());
					this.w.Append(",");
				}
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.BaseTypes);
			this.WriteEndElement();
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0009E0F4 File Offset: 0x0009C2F4
		private void Write14_XmlSchemaSimpleTypeList(XmlSchemaSimpleTypeList o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("list");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.ItemTypeName.IsEmpty)
			{
				this.WriteAttribute("itemType", "", o.ItemTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.ItemType);
			this.WriteEndElement();
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0009E174 File Offset: 0x0009C374
		private void Write15_XmlSchemaSimpleTypeRestriction(XmlSchemaSimpleTypeRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.BaseType);
			this.WriteFacets(o.Facets);
			this.WriteEndElement();
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0009E200 File Offset: 0x0009C400
		private void WriteFacets(XmlSchemaObjectCollection facets)
		{
			if (facets == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < facets.Count; i++)
			{
				arrayList.Add(facets[i]);
			}
			arrayList.Sort(new XmlFacetComparer());
			for (int j = 0; j < arrayList.Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[j];
				if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
				{
					this.Write_XmlSchemaFacet("minExclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
				{
					this.Write_XmlSchemaFacet("maxInclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
				{
					this.Write_XmlSchemaFacet("maxExclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
				{
					this.Write_XmlSchemaFacet("minInclusive", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaLengthFacet)
				{
					this.Write_XmlSchemaFacet("length", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
				{
					this.Write_XmlSchemaFacet("enumeration", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMinLengthFacet)
				{
					this.Write_XmlSchemaFacet("minLength", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaPatternFacet)
				{
					this.Write_XmlSchemaFacet("pattern", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
				{
					this.Write_XmlSchemaFacet("totalDigits", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
				{
					this.Write_XmlSchemaFacet("maxLength", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
				{
					this.Write_XmlSchemaFacet("whiteSpace", (XmlSchemaFacet)xmlSchemaObject);
				}
				else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
				{
					this.Write_XmlSchemaFacet("fractionDigit", (XmlSchemaFacet)xmlSchemaObject);
				}
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0009E3BC File Offset: 0x0009C5BC
		private void Write_XmlSchemaFacet(string name, XmlSchemaFacet o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement(name);
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("value", "", o.Value);
			if (o.IsFixed)
			{
				this.WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0009E444 File Offset: 0x0009C644
		private string Write30_XmlSchemaUse(XmlSchemaUse v)
		{
			string text = null;
			switch (v)
			{
			case XmlSchemaUse.Optional:
				text = "optional";
				break;
			case XmlSchemaUse.Prohibited:
				text = "prohibited";
				break;
			case XmlSchemaUse.Required:
				text = "required";
				break;
			}
			return text;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0009E480 File Offset: 0x0009C680
		private void Write31_XmlSchemaAttributeGroup(XmlSchemaAttributeGroup o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attributeGroup");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0009E500 File Offset: 0x0009C700
		private void Write32_XmlSchemaAttributeGroupRef(XmlSchemaAttributeGroupRef o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("attributeGroup");
			this.WriteAttribute("id", "", o.Id);
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x0009E574 File Offset: 0x0009C774
		private void Write33_XmlSchemaAnyAttribute(XmlSchemaAnyAttribute o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("anyAttribute");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("namespace", "", SchemaObjectWriter.ToString(o.NamespaceList));
			XmlSchemaContentProcessing xmlSchemaContentProcessing = ((o.ProcessContents == XmlSchemaContentProcessing.None) ? XmlSchemaContentProcessing.Strict : o.ProcessContents);
			this.WriteAttribute("processContents", "", this.Write34_XmlSchemaContentProcessing(xmlSchemaContentProcessing));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0009E60C File Offset: 0x0009C80C
		private string Write34_XmlSchemaContentProcessing(XmlSchemaContentProcessing v)
		{
			string text = null;
			switch (v)
			{
			case XmlSchemaContentProcessing.Skip:
				text = "skip";
				break;
			case XmlSchemaContentProcessing.Lax:
				text = "lax";
				break;
			case XmlSchemaContentProcessing.Strict:
				text = "strict";
				break;
			}
			return text;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0009E648 File Offset: 0x0009C848
		private void Write35_XmlSchemaComplexType(XmlSchemaComplexType o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("complexType");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			if (o.IsAbstract)
			{
				this.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			this.WriteAttribute("block", "", this.Write11_XmlSchemaDerivationMethod(o.BlockResolved));
			if (o.IsMixed)
			{
				this.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.ContentModel is XmlSchemaComplexContent)
			{
				this.Write41_XmlSchemaComplexContent((XmlSchemaComplexContent)o.ContentModel);
			}
			else if (o.ContentModel is XmlSchemaSimpleContent)
			{
				this.Write36_XmlSchemaSimpleContent((XmlSchemaSimpleContent)o.ContentModel);
			}
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x0009E804 File Offset: 0x0009CA04
		private void Write36_XmlSchemaSimpleContent(XmlSchemaSimpleContent o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("simpleContent");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaSimpleContentRestriction)
			{
				this.Write40_XmlSchemaSimpleContentRestriction((XmlSchemaSimpleContentRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaSimpleContentExtension)
			{
				this.Write38_XmlSchemaSimpleContentExtension((XmlSchemaSimpleContentExtension)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x0009E894 File Offset: 0x0009CA94
		private void Write38_XmlSchemaSimpleContentExtension(XmlSchemaSimpleContentExtension o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("extension");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0009E920 File Offset: 0x0009CB20
		private void Write40_XmlSchemaSimpleContentRestriction(XmlSchemaSimpleContentRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write9_XmlSchemaSimpleType(o.BaseType);
			this.WriteFacets(o.Facets);
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0009E9C4 File Offset: 0x0009CBC4
		private void Write41_XmlSchemaComplexContent(XmlSchemaComplexContent o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("complexContent");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Content is XmlSchemaComplexContentRestriction)
			{
				this.Write56_XmlSchemaComplexContentRestriction((XmlSchemaComplexContentRestriction)o.Content);
			}
			else if (o.Content is XmlSchemaComplexContentExtension)
			{
				this.Write42_XmlSchemaComplexContentExtension((XmlSchemaComplexContentExtension)o.Content);
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0009EA70 File Offset: 0x0009CC70
		private void Write42_XmlSchemaComplexContentExtension(XmlSchemaComplexContentExtension o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("extension");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0009EB7C File Offset: 0x0009CD7C
		private void Write43_XmlSchemaAll(XmlSchemaAll o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("all");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Items);
			this.WriteEndElement();
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0009EC30 File Offset: 0x0009CE30
		private void Write46_XmlSchemaElement(XmlSchemaElement o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("element");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			if (o.IsAbstract)
			{
				this.WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
			}
			this.WriteAttribute("block", "", this.Write11_XmlSchemaDerivationMethod(o.BlockResolved));
			this.WriteAttribute("default", "", o.DefaultValue);
			this.WriteAttribute("final", "", this.Write11_XmlSchemaDerivationMethod(o.FinalResolved));
			this.WriteAttribute("fixed", "", o.FixedValue);
			if (o.Parent != null && !(o.Parent is XmlSchema))
			{
				if (o.QualifiedName != null && !o.QualifiedName.IsEmpty && o.QualifiedName.Namespace != null && o.QualifiedName.Namespace.Length != 0)
				{
					this.WriteAttribute("form", "", "qualified");
				}
				else
				{
					this.WriteAttribute("form", "", "unqualified");
				}
			}
			if (o.Name != null && o.Name.Length != 0)
			{
				this.WriteAttribute("name", "", o.Name);
			}
			if (o.IsNillable)
			{
				this.WriteAttribute("nillable", "", XmlConvert.ToString(o.IsNillable));
			}
			if (!o.SubstitutionGroup.IsEmpty)
			{
				this.WriteAttribute("substitutionGroup", "", o.SubstitutionGroup);
			}
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			else if (!o.SchemaTypeName.IsEmpty)
			{
				this.WriteAttribute("type", "", o.SchemaTypeName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.SchemaType is XmlSchemaComplexType)
			{
				this.Write35_XmlSchemaComplexType((XmlSchemaComplexType)o.SchemaType);
			}
			else if (o.SchemaType is XmlSchemaSimpleType)
			{
				this.Write9_XmlSchemaSimpleType((XmlSchemaSimpleType)o.SchemaType);
			}
			this.WriteSortedItems(o.Constraints);
			this.WriteEndElement();
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0009EEE4 File Offset: 0x0009D0E4
		private void Write47_XmlSchemaKey(XmlSchemaKey o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("key");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0009EFA0 File Offset: 0x0009D1A0
		private void Write48_XmlSchemaIdentityConstraint(XmlSchemaIdentityConstraint o)
		{
			if (o == null)
			{
				return;
			}
			Type type = o.GetType();
			if (type == typeof(XmlSchemaUnique))
			{
				this.Write51_XmlSchemaUnique((XmlSchemaUnique)o);
				return;
			}
			if (type == typeof(XmlSchemaKeyref))
			{
				this.Write50_XmlSchemaKeyref((XmlSchemaKeyref)o);
				return;
			}
			if (type == typeof(XmlSchemaKey))
			{
				this.Write47_XmlSchemaKey((XmlSchemaKey)o);
				return;
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0009F018 File Offset: 0x0009D218
		private void Write49_XmlSchemaXPath(string name, string ns, XmlSchemaXPath o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement(name);
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("xpath", "", o.XPath);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0009F07C File Offset: 0x0009D27C
		private void Write50_XmlSchemaKeyref(XmlSchemaKeyref o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("keyref");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.WriteAttribute("refer", "", o.Refer);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0009F150 File Offset: 0x0009D350
		private void Write51_XmlSchemaUnique(XmlSchemaUnique o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("unique");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.Write49_XmlSchemaXPath("selector", "", o.Selector);
			XmlSchemaObjectCollection fields = o.Fields;
			if (fields != null)
			{
				for (int i = 0; i < fields.Count; i++)
				{
					this.Write49_XmlSchemaXPath("field", "", (XmlSchemaXPath)fields[i]);
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0009F20C File Offset: 0x0009D40C
		private void Write52_XmlSchemaChoice(XmlSchemaChoice o)
		{
			if (o == null)
			{
				return;
			}
			o.GetType();
			this.WriteStartElement("choice");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteSortedItems(o.Items);
			this.WriteEndElement();
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0009F2C4 File Offset: 0x0009D4C4
		private void Write53_XmlSchemaAny(XmlSchemaAny o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("any");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttribute("namespace", "", SchemaObjectWriter.ToString(o.NamespaceList));
			XmlSchemaContentProcessing xmlSchemaContentProcessing = ((o.ProcessContents == XmlSchemaContentProcessing.None) ? XmlSchemaContentProcessing.Strict : o.ProcessContents);
			this.WriteAttribute("processContents", "", this.Write34_XmlSchemaContentProcessing(xmlSchemaContentProcessing));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0009F3B0 File Offset: 0x0009D5B0
		private void Write54_XmlSchemaSequence(XmlSchemaSequence o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("sequence");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			XmlSchemaObjectCollection items = o.Items;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					XmlSchemaObject xmlSchemaObject = items[i];
					if (xmlSchemaObject is XmlSchemaAny)
					{
						this.Write53_XmlSchemaAny((XmlSchemaAny)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaSequence)
					{
						this.Write54_XmlSchemaSequence((XmlSchemaSequence)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaChoice)
					{
						this.Write52_XmlSchemaChoice((XmlSchemaChoice)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaElement)
					{
						this.Write46_XmlSchemaElement((XmlSchemaElement)xmlSchemaObject);
					}
					else if (xmlSchemaObject is XmlSchemaGroupRef)
					{
						this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)xmlSchemaObject);
					}
				}
			}
			this.WriteEndElement();
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x0009F4EC File Offset: 0x0009D6EC
		private void Write55_XmlSchemaGroupRef(XmlSchemaGroupRef o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("group");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("minOccurs", "", XmlConvert.ToString(o.MinOccurs));
			this.WriteAttribute("maxOccurs", "", (o.MaxOccurs == decimal.MaxValue) ? "unbounded" : XmlConvert.ToString(o.MaxOccurs));
			if (!o.RefName.IsEmpty)
			{
				this.WriteAttribute("ref", "", o.RefName);
			}
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			this.WriteEndElement();
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x0009F5B4 File Offset: 0x0009D7B4
		private void Write56_XmlSchemaComplexContentRestriction(XmlSchemaComplexContentRestriction o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("restriction");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttributes(o.UnhandledAttributes, o);
			if (!o.BaseTypeName.IsEmpty)
			{
				this.WriteAttribute("base", "", o.BaseTypeName);
			}
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaGroupRef)
			{
				this.Write55_XmlSchemaGroupRef((XmlSchemaGroupRef)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteSortedItems(o.Attributes);
			this.Write33_XmlSchemaAnyAttribute(o.AnyAttribute);
			this.WriteEndElement();
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x0009F6C0 File Offset: 0x0009D8C0
		private void Write57_XmlSchemaGroup(XmlSchemaGroup o)
		{
			if (o == null)
			{
				return;
			}
			this.WriteStartElement("group");
			this.WriteAttribute("id", "", o.Id);
			this.WriteAttribute("name", "", o.Name);
			this.WriteAttributes(o.UnhandledAttributes, o);
			this.Write5_XmlSchemaAnnotation(o.Annotation);
			if (o.Particle is XmlSchemaSequence)
			{
				this.Write54_XmlSchemaSequence((XmlSchemaSequence)o.Particle);
			}
			else if (o.Particle is XmlSchemaChoice)
			{
				this.Write52_XmlSchemaChoice((XmlSchemaChoice)o.Particle);
			}
			else if (o.Particle is XmlSchemaAll)
			{
				this.Write43_XmlSchemaAll((XmlSchemaAll)o.Particle);
			}
			this.WriteEndElement();
		}

		// Token: 0x04001684 RID: 5764
		private StringBuilder w = new StringBuilder();

		// Token: 0x04001685 RID: 5765
		private int indentLevel = -1;
	}
}
