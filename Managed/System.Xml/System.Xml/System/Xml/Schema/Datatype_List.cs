using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003B4 RID: 948
	internal class Datatype_List : Datatype_anySimpleType
	{
		// Token: 0x060025DC RID: 9692 RVA: 0x000E3970 File Offset: 0x000E1B70
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType;
			if (xmlSchemaComplexType != null)
			{
				do
				{
					xmlSchemaSimpleType = xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType != null)
					{
						break;
					}
					xmlSchemaComplexType = xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaComplexType;
					if (xmlSchemaComplexType == null)
					{
						break;
					}
				}
				while (xmlSchemaComplexType != XmlSchemaComplexType.AnyType);
			}
			else
			{
				xmlSchemaSimpleType = schemaType as XmlSchemaSimpleType;
			}
			if (xmlSchemaSimpleType != null)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList;
				for (;;)
				{
					xmlSchemaSimpleTypeList = xmlSchemaSimpleType.Content as XmlSchemaSimpleTypeList;
					if (xmlSchemaSimpleTypeList != null)
					{
						break;
					}
					xmlSchemaSimpleType = xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType;
					if (xmlSchemaSimpleType == null || xmlSchemaSimpleType == DatatypeImplementation.AnySimpleType)
					{
						goto IL_006D;
					}
				}
				xmlSchemaType = xmlSchemaSimpleTypeList.BaseItemType;
			}
			IL_006D:
			if (xmlSchemaType == null)
			{
				xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromTypeCode(schemaType.Datatype.TypeCode);
			}
			return XmlListConverter.Create(xmlSchemaType.ValueConverter);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000E3A09 File Offset: 0x000E1C09
		internal Datatype_List(DatatypeImplementation type)
			: this(type, 0)
		{
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000E3A13 File Offset: 0x000E1C13
		internal Datatype_List(DatatypeImplementation type, int minListSize)
		{
			this.itemType = type;
			this.minListSize = minListSize;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000E3A2C File Offset: 0x000E1C2C
		internal override int Compare(object value1, object value2)
		{
			Array array = (Array)value1;
			Array array2 = (Array)value2;
			if (array.Length != array2.Length)
			{
				return -1;
			}
			XmlAtomicValue[] array3 = array as XmlAtomicValue[];
			if (array3 != null)
			{
				XmlAtomicValue[] array4 = array2 as XmlAtomicValue[];
				for (int i = 0; i < array3.Length; i++)
				{
					XmlSchemaType xmlType = array3[i].XmlType;
					if (xmlType != array4[i].XmlType || !xmlType.Datatype.IsEqual(array3[i].TypedValue, array4[i].TypedValue))
					{
						return -1;
					}
				}
				return 0;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (this.itemType.Compare(array.GetValue(j), array2.GetValue(j)) != 0)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000E3AEB File Offset: 0x000E1CEB
		public override Type ValueType
		{
			get
			{
				return this.ListValueType;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x000E3AF3 File Offset: 0x000E1CF3
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return this.itemType.TokenizedType;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000E3B00 File Offset: 0x000E1D00
		internal override Type ListValueType
		{
			get
			{
				return this.itemType.ListValueType;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000E3B0D File Offset: 0x000E1D0D
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.listFacetsChecker;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000E3B14 File Offset: 0x000E1D14
		public override XmlTypeCode TypeCode
		{
			get
			{
				return this.itemType.TypeCode;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000E3B21 File Offset: 0x000E1D21
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000E3B25 File Offset: 0x000E1D25
		internal DatatypeImplementation ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000E3B30 File Offset: 0x000E1D30
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = value as string;
			typedValue = null;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			Exception ex;
			try
			{
				object obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				Array array = obj as Array;
				bool hasLexicalFacets = this.itemType.HasLexicalFacets;
				bool hasValueFacets = this.itemType.HasValueFacets;
				FacetsChecker facetsChecker = this.itemType.FacetsChecker;
				XmlValueConverter valueConverter = this.itemType.ValueConverter;
				for (int i = 0; i < array.Length; i++)
				{
					object value2 = array.GetValue(i);
					if (hasLexicalFacets)
					{
						string text2 = (string)valueConverter.ChangeType(value2, typeof(string), namespaceResolver);
						ex = facetsChecker.CheckLexicalFacets(ref text2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
					if (hasValueFacets)
					{
						ex = facetsChecker.CheckValueFacets(value2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
				}
				if (this.HasLexicalFacets)
				{
					string text3 = (string)this.ValueConverter.ChangeType(obj, typeof(string), namespaceResolver);
					ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref text3, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
				return null;
			}
			catch (FormatException ex)
			{
			}
			catch (InvalidCastException ex)
			{
			}
			catch (OverflowException ex)
			{
			}
			catch (ArgumentException ex)
			{
			}
			return ex;
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000E3CF4 File Offset: 0x000E1EF4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ArrayList arrayList = new ArrayList();
				object obj2;
				if (this.itemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					string[] array = XmlConvert.SplitString(s);
					for (int i = 0; i < array.Length; i++)
					{
						object obj;
						ex = this.itemType.TryParseValue(array[i], nameTable, nsmgr, out obj);
						if (ex != null)
						{
							return ex;
						}
						XsdSimpleValue xsdSimpleValue = (XsdSimpleValue)obj;
						arrayList.Add(new XmlAtomicValue(xsdSimpleValue.XmlType, xsdSimpleValue.TypedValue, nsmgr));
					}
					obj2 = arrayList.ToArray(typeof(XmlAtomicValue));
				}
				else
				{
					string[] array2 = XmlConvert.SplitString(s);
					for (int j = 0; j < array2.Length; j++)
					{
						ex = this.itemType.TryParseValue(array2[j], nameTable, nsmgr, out typedValue);
						if (ex != null)
						{
							return ex;
						}
						arrayList.Add(typedValue);
					}
					obj2 = arrayList.ToArray(this.itemType.ValueType);
				}
				if (arrayList.Count < this.minListSize)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj2, this);
				if (ex == null)
				{
					typedValue = obj2;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x040019DE RID: 6622
		private DatatypeImplementation itemType;

		// Token: 0x040019DF RID: 6623
		private int minListSize;
	}
}
