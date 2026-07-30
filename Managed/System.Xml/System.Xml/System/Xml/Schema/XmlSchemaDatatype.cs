using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Schema
{
	/// <summary>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> class is an abstract class for mapping XML Schema definition language (XSD) types to Common Language Runtime (CLR) types.</summary>
	// Token: 0x0200044B RID: 1099
	public abstract class XmlSchemaDatatype
	{
		/// <summary>When overridden in a derived class, gets the Common Language Runtime (CLR) type of the item.</summary>
		/// <returns>The Common Language Runtime (CLR) type of the item.</returns>
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002B9F RID: 11167
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the type for the string as specified in the World Wide Web Consortium (W3C) XML 1.0 specification.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlTokenizedType" /> value for the string.</returns>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002BA0 RID: 11168
		public abstract XmlTokenizedType TokenizedType { get; }

		/// <summary>When overridden in a derived class, validates the string specified against a built-in or user-defined simple type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast safely to the type returned by the <see cref="P:System.Xml.Schema.XmlSchemaDatatype.ValueType" /> property.</returns>
		/// <param name="s">The string to validate against the simple type.</param>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use for atomization while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:NCName type. </param>
		/// <param name="nsmgr">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:QName type.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The input value is not a valid instance of this W3C XML Schema type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value to parse cannot be null.</exception>
		// Token: 0x06002BA1 RID: 11169
		public abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</returns>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return XmlSchemaDatatypeVariety.Atomic;
			}
		}

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified.</summary>
		/// <returns>The converted input value.</returns>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x06002BA3 RID: 11171 RVA: 0x001061A0 File Offset: 0x001043A0
		public virtual object ChangeType(object value, Type targetType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return this.ValueConverter.ChangeType(value, targetType);
		}

		/// <summary>Converts the value specified, whose type is one of the valid Common Language Runtime (CLR) representations of the XML schema type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />, to the CLR type specified using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> represents the xs:QName type or a type derived from it.</summary>
		/// <returns>The converted input value.</returns>
		/// <param name="value">The input value to convert to the specified type.</param>
		/// <param name="targetType">The target type to convert the input value to.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> used for resolving namespace prefixes. This is only of use if the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />  represents the xs:QName type or a type derived from it.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Object" /> or <see cref="T:System.Type" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidCastException">The type represented by the <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />   does not support a conversion from type of the value specified to the type specified.</exception>
		// Token: 0x06002BA4 RID: 11172 RVA: 0x001061D1 File Offset: 0x001043D1
		public virtual object ChangeType(object value, Type targetType, IXmlNamespaceResolver namespaceResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			return this.ValueConverter.ChangeType(value, targetType, namespaceResolver);
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		/// <summary>The <see cref="M:System.Xml.Schema.XmlSchemaDatatype.IsDerivedFrom(System.Xml.Schema.XmlSchemaDatatype)" /> method always returns false.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="datatype">The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />.</param>
		// Token: 0x06002BA6 RID: 11174 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002BA7 RID: 11175
		internal abstract bool HasLexicalFacets { get; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002BA8 RID: 11176
		internal abstract bool HasValueFacets { get; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002BA9 RID: 11177
		internal abstract XmlValueConverter ValueConverter { get; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002BAA RID: 11178
		// (set) Token: 0x06002BAB RID: 11179
		internal abstract RestrictionFacets Restriction { get; set; }

		// Token: 0x06002BAC RID: 11180
		internal abstract int Compare(object value1, object value2);

		// Token: 0x06002BAD RID: 11181
		internal abstract object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		// Token: 0x06002BAE RID: 11182
		internal abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue);

		// Token: 0x06002BAF RID: 11183
		internal abstract Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue);

		// Token: 0x06002BB0 RID: 11184
		internal abstract Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue);

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002BB1 RID: 11185
		internal abstract FacetsChecker FacetsChecker { get; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002BB2 RID: 11186
		internal abstract XmlSchemaWhiteSpace BuiltInWhitespaceFacet { get; }

		// Token: 0x06002BB3 RID: 11187
		internal abstract XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType);

		// Token: 0x06002BB4 RID: 11188
		internal abstract XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType);

		// Token: 0x06002BB5 RID: 11189
		internal abstract void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller);

		// Token: 0x06002BB6 RID: 11190
		internal abstract bool IsEqual(object o1, object o2);

		// Token: 0x06002BB7 RID: 11191
		internal abstract bool IsComparable(XmlSchemaDatatype dtype);

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x00106214 File Offset: 0x00104414
		internal string TypeCodeString
		{
			get
			{
				string text = string.Empty;
				XmlTypeCode typeCode = this.TypeCode;
				switch (this.Variety)
				{
				case XmlSchemaDatatypeVariety.Atomic:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						text = "anySimpleType";
					}
					else
					{
						text = this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.List:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						text = "List of Union";
					}
					else
					{
						text = "List of " + this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.Union:
					text = "Union";
					break;
				}
				return text;
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00106288 File Offset: 0x00104488
		internal string TypeCodeToString(XmlTypeCode typeCode)
		{
			switch (typeCode)
			{
			case XmlTypeCode.None:
				return "None";
			case XmlTypeCode.Item:
				return "AnyType";
			case XmlTypeCode.AnyAtomicType:
				return "AnyAtomicType";
			case XmlTypeCode.String:
				return "String";
			case XmlTypeCode.Boolean:
				return "Boolean";
			case XmlTypeCode.Decimal:
				return "Decimal";
			case XmlTypeCode.Float:
				return "Float";
			case XmlTypeCode.Double:
				return "Double";
			case XmlTypeCode.Duration:
				return "Duration";
			case XmlTypeCode.DateTime:
				return "DateTime";
			case XmlTypeCode.Time:
				return "Time";
			case XmlTypeCode.Date:
				return "Date";
			case XmlTypeCode.GYearMonth:
				return "GYearMonth";
			case XmlTypeCode.GYear:
				return "GYear";
			case XmlTypeCode.GMonthDay:
				return "GMonthDay";
			case XmlTypeCode.GDay:
				return "GDay";
			case XmlTypeCode.GMonth:
				return "GMonth";
			case XmlTypeCode.HexBinary:
				return "HexBinary";
			case XmlTypeCode.Base64Binary:
				return "Base64Binary";
			case XmlTypeCode.AnyUri:
				return "AnyUri";
			case XmlTypeCode.QName:
				return "QName";
			case XmlTypeCode.Notation:
				return "Notation";
			case XmlTypeCode.NormalizedString:
				return "NormalizedString";
			case XmlTypeCode.Token:
				return "Token";
			case XmlTypeCode.Language:
				return "Language";
			case XmlTypeCode.NmToken:
				return "NmToken";
			case XmlTypeCode.Name:
				return "Name";
			case XmlTypeCode.NCName:
				return "NCName";
			case XmlTypeCode.Id:
				return "Id";
			case XmlTypeCode.Idref:
				return "Idref";
			case XmlTypeCode.Entity:
				return "Entity";
			case XmlTypeCode.Integer:
				return "Integer";
			case XmlTypeCode.NonPositiveInteger:
				return "NonPositiveInteger";
			case XmlTypeCode.NegativeInteger:
				return "NegativeInteger";
			case XmlTypeCode.Long:
				return "Long";
			case XmlTypeCode.Int:
				return "Int";
			case XmlTypeCode.Short:
				return "Short";
			case XmlTypeCode.Byte:
				return "Byte";
			case XmlTypeCode.NonNegativeInteger:
				return "NonNegativeInteger";
			case XmlTypeCode.UnsignedLong:
				return "UnsignedLong";
			case XmlTypeCode.UnsignedInt:
				return "UnsignedInt";
			case XmlTypeCode.UnsignedShort:
				return "UnsignedShort";
			case XmlTypeCode.UnsignedByte:
				return "UnsignedByte";
			case XmlTypeCode.PositiveInteger:
				return "PositiveInteger";
			}
			return typeCode.ToString();
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0010648C File Offset: 0x0010468C
		internal static string ConcatenatedToString(object value)
		{
			Type type = value.GetType();
			string text = string.Empty;
			if (type == typeof(IEnumerable) && type != typeof(string))
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = (value as IEnumerable).GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("{");
					object obj = enumerator.Current;
					if (obj is IFormattable)
					{
						stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(" , ");
						obj = enumerator.Current;
						if (obj is IFormattable)
						{
							stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
						}
						else
						{
							stringBuilder.Append(obj.ToString());
						}
					}
					stringBuilder.Append("}");
					text = stringBuilder.ToString();
				}
			}
			else if (value is IFormattable)
			{
				text = ((IFormattable)value).ToString("", CultureInfo.InvariantCulture);
			}
			else
			{
				text = value.ToString();
			}
			return text;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x001065C8 File Offset: 0x001047C8
		internal static XmlSchemaDatatype FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedType(token);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x001065D0 File Offset: 0x001047D0
		internal static XmlSchemaDatatype FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedTypeXsd(token);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x001065D8 File Offset: 0x001047D8
		internal static XmlSchemaDatatype FromXdrName(string name)
		{
			return DatatypeImplementation.FromXdrName(name);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x001065E0 File Offset: 0x001047E0
		internal static XmlSchemaDatatype DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return DatatypeImplementation.DeriveByUnion(types, schemaType);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x001065EC File Offset: 0x001047EC
		internal static string XdrCanonizeUri(string uri, XmlNameTable nameTable, SchemaNames schemaNames)
		{
			int num = 5;
			bool flag = false;
			if (uri.Length > 5 && uri.StartsWith("uuid:", StringComparison.Ordinal))
			{
				flag = true;
			}
			else if (uri.Length > 9 && uri.StartsWith("urn:uuid:", StringComparison.Ordinal))
			{
				flag = true;
				num = 9;
			}
			string text;
			if (flag)
			{
				text = nameTable.Add(uri.Substring(0, num) + uri.Substring(num, uri.Length - num).ToUpper(CultureInfo.InvariantCulture));
			}
			else
			{
				text = uri;
			}
			if (Ref.Equal(schemaNames.NsDataTypeAlias, text) || Ref.Equal(schemaNames.NsDataTypeOld, text))
			{
				text = schemaNames.NsDataType;
			}
			else if (Ref.Equal(schemaNames.NsXdrAlias, text))
			{
				text = schemaNames.NsXdr;
			}
			return text;
		}
	}
}
