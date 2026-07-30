using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003B2 RID: 946
	internal abstract class DatatypeImplementation : XmlSchemaDatatype
	{
		// Token: 0x060025A5 RID: 9637 RVA: 0x000E2414 File Offset: 0x000E0614
		static DatatypeImplementation()
		{
			DatatypeImplementation[] array = new DatatypeImplementation[13];
			array[0] = DatatypeImplementation.c_string;
			array[1] = DatatypeImplementation.c_ID;
			array[2] = DatatypeImplementation.c_IDREF;
			array[3] = DatatypeImplementation.c_IDREFS;
			array[4] = DatatypeImplementation.c_ENTITY;
			array[5] = DatatypeImplementation.c_ENTITIES;
			array[6] = DatatypeImplementation.c_NMTOKEN;
			array[7] = DatatypeImplementation.c_NMTOKENS;
			array[8] = DatatypeImplementation.c_NOTATION;
			array[9] = DatatypeImplementation.c_ENUMERATION;
			array[10] = DatatypeImplementation.c_QNameXdr;
			array[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypes = array;
			DatatypeImplementation[] array2 = new DatatypeImplementation[13];
			array2[0] = DatatypeImplementation.c_string;
			array2[1] = DatatypeImplementation.c_ID;
			array2[2] = DatatypeImplementation.c_IDREF;
			array2[3] = DatatypeImplementation.c_IDREFS;
			array2[4] = DatatypeImplementation.c_ENTITY;
			array2[5] = DatatypeImplementation.c_ENTITIES;
			array2[6] = DatatypeImplementation.c_NMTOKEN;
			array2[7] = DatatypeImplementation.c_NMTOKENS;
			array2[8] = DatatypeImplementation.c_NOTATION;
			array2[9] = DatatypeImplementation.c_ENUMERATION;
			array2[10] = DatatypeImplementation.c_QName;
			array2[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypesXsd = array2;
			DatatypeImplementation.c_XdrTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("bin.base64", DatatypeImplementation.c_base64Binary),
				new DatatypeImplementation.SchemaDatatypeMap("bin.hex", DatatypeImplementation.c_hexBinary),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean),
				new DatatypeImplementation.SchemaDatatypeMap("char", DatatypeImplementation.c_char),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTimeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime.tz", DatatypeImplementation.c_dateTimeTz),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal),
				new DatatypeImplementation.SchemaDatatypeMap("entities", DatatypeImplementation.c_ENTITIES),
				new DatatypeImplementation.SchemaDatatypeMap("entity", DatatypeImplementation.c_ENTITY),
				new DatatypeImplementation.SchemaDatatypeMap("enumeration", DatatypeImplementation.c_ENUMERATION),
				new DatatypeImplementation.SchemaDatatypeMap("fixed.14.4", DatatypeImplementation.c_fixed),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.32", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.64", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("i1", DatatypeImplementation.c_byte),
				new DatatypeImplementation.SchemaDatatypeMap("i2", DatatypeImplementation.c_short),
				new DatatypeImplementation.SchemaDatatypeMap("i4", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("i8", DatatypeImplementation.c_long),
				new DatatypeImplementation.SchemaDatatypeMap("id", DatatypeImplementation.c_ID),
				new DatatypeImplementation.SchemaDatatypeMap("idref", DatatypeImplementation.c_IDREF),
				new DatatypeImplementation.SchemaDatatypeMap("idrefs", DatatypeImplementation.c_IDREFS),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("nmtoken", DatatypeImplementation.c_NMTOKEN),
				new DatatypeImplementation.SchemaDatatypeMap("nmtokens", DatatypeImplementation.c_NMTOKENS),
				new DatatypeImplementation.SchemaDatatypeMap("notation", DatatypeImplementation.c_NOTATION),
				new DatatypeImplementation.SchemaDatatypeMap("number", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r4", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r8", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_timeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("time.tz", DatatypeImplementation.c_timeTz),
				new DatatypeImplementation.SchemaDatatypeMap("ui1", DatatypeImplementation.c_unsignedByte),
				new DatatypeImplementation.SchemaDatatypeMap("ui2", DatatypeImplementation.c_unsignedShort),
				new DatatypeImplementation.SchemaDatatypeMap("ui4", DatatypeImplementation.c_unsignedInt),
				new DatatypeImplementation.SchemaDatatypeMap("ui8", DatatypeImplementation.c_unsignedLong),
				new DatatypeImplementation.SchemaDatatypeMap("uri", DatatypeImplementation.c_anyURI),
				new DatatypeImplementation.SchemaDatatypeMap("uuid", DatatypeImplementation.c_uuid)
			};
			DatatypeImplementation.c_XsdTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("ENTITIES", DatatypeImplementation.c_ENTITIES, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ENTITY", DatatypeImplementation.c_ENTITY, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ID", DatatypeImplementation.c_ID, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREF", DatatypeImplementation.c_IDREF, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREFS", DatatypeImplementation.c_IDREFS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NCName", DatatypeImplementation.c_NCName, 9),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKEN", DatatypeImplementation.c_NMTOKEN, 40),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKENS", DatatypeImplementation.c_NMTOKENS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NOTATION", DatatypeImplementation.c_NOTATION, 11),
				new DatatypeImplementation.SchemaDatatypeMap("Name", DatatypeImplementation.c_Name, 40),
				new DatatypeImplementation.SchemaDatatypeMap("QName", DatatypeImplementation.c_QName, 11),
				new DatatypeImplementation.SchemaDatatypeMap("anySimpleType", DatatypeImplementation.c_anySimpleType, -1),
				new DatatypeImplementation.SchemaDatatypeMap("anyURI", DatatypeImplementation.c_anyURI, 11),
				new DatatypeImplementation.SchemaDatatypeMap("base64Binary", DatatypeImplementation.c_base64Binary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean, 11),
				new DatatypeImplementation.SchemaDatatypeMap("byte", DatatypeImplementation.c_byte, 37),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date, 11),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTime, 11),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal, 11),
				new DatatypeImplementation.SchemaDatatypeMap("double", DatatypeImplementation.c_double, 11),
				new DatatypeImplementation.SchemaDatatypeMap("duration", DatatypeImplementation.c_duration, 11),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_float, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gDay", DatatypeImplementation.c_day, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonth", DatatypeImplementation.c_month, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonthDay", DatatypeImplementation.c_monthDay, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYear", DatatypeImplementation.c_year, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYearMonth", DatatypeImplementation.c_yearMonth, 11),
				new DatatypeImplementation.SchemaDatatypeMap("hexBinary", DatatypeImplementation.c_hexBinary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int, 31),
				new DatatypeImplementation.SchemaDatatypeMap("integer", DatatypeImplementation.c_integer, 18),
				new DatatypeImplementation.SchemaDatatypeMap("language", DatatypeImplementation.c_language, 40),
				new DatatypeImplementation.SchemaDatatypeMap("long", DatatypeImplementation.c_long, 29),
				new DatatypeImplementation.SchemaDatatypeMap("negativeInteger", DatatypeImplementation.c_negativeInteger, 34),
				new DatatypeImplementation.SchemaDatatypeMap("nonNegativeInteger", DatatypeImplementation.c_nonNegativeInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("nonPositiveInteger", DatatypeImplementation.c_nonPositiveInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("normalizedString", DatatypeImplementation.c_normalizedString, 38),
				new DatatypeImplementation.SchemaDatatypeMap("positiveInteger", DatatypeImplementation.c_positiveInteger, 33),
				new DatatypeImplementation.SchemaDatatypeMap("short", DatatypeImplementation.c_short, 28),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string, 11),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_time, 11),
				new DatatypeImplementation.SchemaDatatypeMap("token", DatatypeImplementation.c_token, 35),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedByte", DatatypeImplementation.c_unsignedByte, 44),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedInt", DatatypeImplementation.c_unsignedInt, 43),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedLong", DatatypeImplementation.c_unsignedLong, 33),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedShort", DatatypeImplementation.c_unsignedShort, 42)
			};
			DatatypeImplementation.CreateBuiltinTypes();
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000E2EB2 File Offset: 0x000E10B2
		internal static XmlSchemaSimpleType AnySimpleType
		{
			get
			{
				return DatatypeImplementation.anySimpleType;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000E2EB9 File Offset: 0x000E10B9
		internal static XmlSchemaSimpleType AnyAtomicType
		{
			get
			{
				return DatatypeImplementation.anyAtomicType;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000E2EC0 File Offset: 0x000E10C0
		internal static XmlSchemaSimpleType UntypedAtomicType
		{
			get
			{
				return DatatypeImplementation.untypedAtomicType;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000E2EC7 File Offset: 0x000E10C7
		internal static XmlSchemaSimpleType YearMonthDurationType
		{
			get
			{
				return DatatypeImplementation.yearMonthDurationType;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x000E2ECE File Offset: 0x000E10CE
		internal static XmlSchemaSimpleType DayTimeDurationType
		{
			get
			{
				return DatatypeImplementation.dayTimeDurationType;
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000E2ED5 File Offset: 0x000E10D5
		internal new static DatatypeImplementation FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypes[(int)token];
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000E2EDE File Offset: 0x000E10DE
		internal new static DatatypeImplementation FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypesXsd[(int)token];
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000E2EE8 File Offset: 0x000E10E8
		internal new static DatatypeImplementation FromXdrName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XdrTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XdrTypes[num];
			}
			return null;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000E2F14 File Offset: 0x000E1114
		private static DatatypeImplementation FromTypeName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XsdTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XsdTypes[num];
			}
			return null;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000E2F40 File Offset: 0x000E1140
		internal static XmlSchemaSimpleType StartBuiltinType(XmlQualifiedName qname, XmlSchemaDatatype dataType)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.SetQualifiedName(qname);
			xmlSchemaSimpleType.SetDatatype(dataType);
			xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(dataType);
			xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
			return xmlSchemaSimpleType;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000E2F7C File Offset: 0x000E117C
		internal static void FinishBuiltinType(XmlSchemaSimpleType derivedType, XmlSchemaSimpleType baseType)
		{
			derivedType.SetBaseSchemaType(baseType);
			derivedType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				derivedType.Content = new XmlSchemaSimpleTypeRestriction
				{
					BaseTypeName = baseType.QualifiedName
				};
			}
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
				derivedType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				XmlTypeCode typeCode = derivedType.Datatype.TypeCode;
				if (typeCode != XmlTypeCode.NmToken)
				{
					if (typeCode != XmlTypeCode.Idref)
					{
						if (typeCode == XmlTypeCode.Entity)
						{
							xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[39]);
						}
					}
					else
					{
						xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[38]);
					}
				}
				else
				{
					xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[34]);
				}
				derivedType.Content = xmlSchemaSimpleTypeList;
			}
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000E3048 File Offset: 0x000E1248
		internal static void CreateBuiltinTypes()
		{
			DatatypeImplementation.SchemaDatatypeMap schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[11];
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
			DatatypeImplementation datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
			DatatypeImplementation.anySimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
			datatypeImplementation.parentSchemaType = DatatypeImplementation.anySimpleType;
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anySimpleType);
			for (int i = 0; i < DatatypeImplementation.c_XsdTypes.Length; i++)
			{
				if (i != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[i];
					xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
					datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
					XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
					datatypeImplementation.parentSchemaType = xmlSchemaSimpleType;
					DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, xmlSchemaSimpleType);
					if (datatypeImplementation.variety == XmlSchemaDatatypeVariety.Atomic)
					{
						DatatypeImplementation.enumToTypeCode[(int)datatypeImplementation.TypeCode] = xmlSchemaSimpleType;
					}
				}
			}
			for (int j = 0; j < DatatypeImplementation.c_XsdTypes.Length; j++)
			{
				if (j != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[j];
					XmlSchemaSimpleType xmlSchemaSimpleType2 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema")];
					if (schemaDatatypeMap.ParentIndex == 11)
					{
						DatatypeImplementation.FinishBuiltinType(xmlSchemaSimpleType2, DatatypeImplementation.anySimpleType);
					}
					else
					{
						XmlSchemaSimpleType xmlSchemaSimpleType3 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(DatatypeImplementation.c_XsdTypes[schemaDatatypeMap.ParentIndex].Name, "http://www.w3.org/2001/XMLSchema")];
						DatatypeImplementation.FinishBuiltinType(xmlSchemaSimpleType2, xmlSchemaSimpleType3);
					}
				}
			}
			xmlQualifiedName = new XmlQualifiedName("anyAtomicType", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.anyAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_anyAtomicType);
			DatatypeImplementation.c_anyAtomicType.parentSchemaType = DatatypeImplementation.anyAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.anyAtomicType, DatatypeImplementation.anySimpleType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.enumToTypeCode[10] = DatatypeImplementation.anyAtomicType;
			xmlQualifiedName = new XmlQualifiedName("untypedAtomic", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.untypedAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_untypedAtomicType);
			DatatypeImplementation.c_untypedAtomicType.parentSchemaType = DatatypeImplementation.untypedAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.untypedAtomicType, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.untypedAtomicType);
			DatatypeImplementation.enumToTypeCode[11] = DatatypeImplementation.untypedAtomicType;
			xmlQualifiedName = new XmlQualifiedName("yearMonthDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.yearMonthDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_yearMonthDuration);
			DatatypeImplementation.c_yearMonthDuration.parentSchemaType = DatatypeImplementation.yearMonthDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.yearMonthDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.yearMonthDurationType);
			DatatypeImplementation.enumToTypeCode[53] = DatatypeImplementation.yearMonthDurationType;
			xmlQualifiedName = new XmlQualifiedName("dayTimeDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.dayTimeDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_dayTimeDuration);
			DatatypeImplementation.c_dayTimeDuration.parentSchemaType = DatatypeImplementation.dayTimeDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.dayTimeDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.dayTimeDurationType);
			DatatypeImplementation.enumToTypeCode[54] = DatatypeImplementation.dayTimeDurationType;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000E331F File Offset: 0x000E151F
		internal static XmlSchemaSimpleType GetSimpleTypeFromTypeCode(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.enumToTypeCode[(int)typeCode];
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000E3328 File Offset: 0x000E1528
		internal static XmlSchemaSimpleType GetSimpleTypeFromXsdType(XmlQualifiedName qname)
		{
			return (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[qname];
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000E333C File Offset: 0x000E153C
		internal static XmlSchemaSimpleType GetNormalizedStringTypeV1Compat()
		{
			if (DatatypeImplementation.normalizedStringTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NormalizedString).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.normalizedStringTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.normalizedStringTypeV1Compat;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000E339C File Offset: 0x000E159C
		internal static XmlSchemaSimpleType GetTokenTypeV1Compat()
		{
			if (DatatypeImplementation.tokenTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.Token).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.tokenTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.tokenTypeV1Compat;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000E33FA File Offset: 0x000E15FA
		internal static XmlSchemaSimpleType[] GetBuiltInTypes()
		{
			return DatatypeImplementation.enumToTypeCode;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000E3404 File Offset: 0x000E1604
		internal static XmlTypeCode GetPrimitiveTypeCode(XmlTypeCode typeCode)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.enumToTypeCode[(int)typeCode];
			while (xmlSchemaSimpleType.BaseXmlSchemaType != DatatypeImplementation.AnySimpleType)
			{
				xmlSchemaSimpleType = xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType;
			}
			return xmlSchemaSimpleType.TypeCode;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000E343A File Offset: 0x000E163A
		internal override XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType)
		{
			DatatypeImplementation datatypeImplementation = (DatatypeImplementation)base.MemberwiseClone();
			datatypeImplementation.restriction = this.FacetsChecker.ConstructRestriction(this, facets, nameTable);
			datatypeImplementation.baseType = this;
			datatypeImplementation.parentSchemaType = schemaType;
			datatypeImplementation.valueConverter = null;
			return datatypeImplementation;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000E3470 File Offset: 0x000E1670
		internal override XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType)
		{
			return this.DeriveByList(0, schemaType);
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000E347C File Offset: 0x000E167C
		internal XmlSchemaDatatype DeriveByList(int minSize, XmlSchemaType schemaType)
		{
			if (this.variety == XmlSchemaDatatypeVariety.List)
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			if (this.variety == XmlSchemaDatatypeVariety.Union && !((Datatype_union)this).HasAtomicMembers())
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			return new Datatype_List(this, minSize)
			{
				variety = XmlSchemaDatatypeVariety.List,
				restriction = null,
				baseType = DatatypeImplementation.c_anySimpleType,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000E34EF File Offset: 0x000E16EF
		internal new static DatatypeImplementation DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return new Datatype_union(types)
			{
				baseType = DatatypeImplementation.c_anySimpleType,
				variety = XmlSchemaDatatypeVariety.Union,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000E3510 File Offset: 0x000E1710
		public override bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			if (datatype == null)
			{
				return false;
			}
			for (DatatypeImplementation datatypeImplementation = this; datatypeImplementation != null; datatypeImplementation = datatypeImplementation.baseType)
			{
				if (datatypeImplementation == datatype)
				{
					return true;
				}
			}
			if (((DatatypeImplementation)datatype).baseType == null)
			{
				Type type = base.GetType();
				Type type2 = datatype.GetType();
				return type2 == type || type.IsSubclassOf(type2);
			}
			if (datatype.Variety == XmlSchemaDatatypeVariety.Union && !datatype.HasLexicalFacets && !datatype.HasValueFacets && this.variety != XmlSchemaDatatypeVariety.Union)
			{
				return ((Datatype_union)datatype).IsUnionBaseOf(this);
			}
			return (this.variety == XmlSchemaDatatypeVariety.Union || this.variety == XmlSchemaDatatypeVariety.List) && this.restriction == null && datatype == DatatypeImplementation.anySimpleType.Datatype;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000E35BC File Offset: 0x000E17BC
		internal override bool IsEqual(object o1, object o2)
		{
			return this.Compare(o1, o2) == 0;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000E35CC File Offset: 0x000E17CC
		internal override bool IsComparable(XmlSchemaDatatype dtype)
		{
			XmlTypeCode typeCode = this.TypeCode;
			XmlTypeCode typeCode2 = dtype.TypeCode;
			return typeCode == typeCode2 || DatatypeImplementation.GetPrimitiveTypeCode(typeCode) == DatatypeImplementation.GetPrimitiveTypeCode(typeCode2) || (this.IsDerivedFrom(dtype) || dtype.IsDerivedFrom(this));
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0000365F File Offset: 0x0000185F
		internal virtual XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return null;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000E3612 File Offset: 0x000E1812
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x000E3619 File Offset: 0x000E1819
		internal override XmlValueConverter ValueConverter
		{
			get
			{
				if (this.valueConverter == null)
				{
					this.valueConverter = this.CreateValueConverter(this.parentSchemaType);
				}
				return this.valueConverter;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000163C5 File Offset: 0x000145C5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x000296C6 File Offset: 0x000278C6
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x000E363B File Offset: 0x000E183B
		public override XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return this.variety;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x0000226C File Offset: 0x0000046C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000E3643 File Offset: 0x000E1843
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x000E364B File Offset: 0x000E184B
		internal override RestrictionFacets Restriction
		{
			get
			{
				return this.restriction;
			}
			set
			{
				this.restriction = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000E3654 File Offset: 0x000E1854
		internal override bool HasLexicalFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = ((this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0));
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x000E3688 File Offset: 0x000E1888
		internal override bool HasValueFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = ((this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0));
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Enumeration | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000E36BB File Offset: 0x000E18BB
		protected DatatypeImplementation Base
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060025CC RID: 9676
		internal abstract Type ListValueType { get; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060025CD RID: 9677
		internal abstract RestrictionFlags ValidRestrictionFlags { get; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x0000226C File Offset: 0x0000046C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000E36C3 File Offset: 0x000E18C3
		internal override object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ValueConverter.ChangeType(this.ParseValue(s, nameTable, nsmgr), typDest, nsmgr);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000E36E0 File Offset: 0x000E18E0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			if (this.Variety == XmlSchemaDatatypeVariety.Union)
			{
				return (obj as XsdSimpleValue).TypedValue;
			}
			return obj;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000E3740 File Offset: 0x000E1940
		internal override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue)
		{
			if (!createAtomicValue)
			{
				return this.ParseValue(s, nameTable, nsmgr);
			}
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			return obj;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000E3798 File Offset: 0x000E1998
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			Exception ex = null;
			typedValue = null;
			if (value == null)
			{
				return new ArgumentNullException("value");
			}
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			try
			{
				object obj = value;
				if (value.GetType() != this.ValueType)
				{
					obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				}
				if (this.HasLexicalFacets)
				{
					string text2 = (string)this.ValueConverter.ChangeType(value, typeof(string), namespaceResolver);
					ex = this.FacetsChecker.CheckLexicalFacets(ref text2, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = this.FacetsChecker.CheckValueFacets(obj, this);
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

		// Token: 0x060025D3 RID: 9683 RVA: 0x000E389C File Offset: 0x000E1A9C
		internal string GetTypeName()
		{
			XmlSchemaType xmlSchemaType = this.parentSchemaType;
			string text;
			if (xmlSchemaType == null || xmlSchemaType.QualifiedName.IsEmpty)
			{
				text = base.TypeCodeString;
			}
			else
			{
				text = xmlSchemaType.QualifiedName.ToString();
			}
			return text;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000E38D8 File Offset: 0x000E1AD8
		protected int Compare(byte[] value1, byte[] value2)
		{
			int num = value1.Length;
			if (num != value2.Length)
			{
				return -1;
			}
			for (int i = 0; i < num; i++)
			{
				if (value1[i] != value2[i])
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x0400197F RID: 6527
		private XmlSchemaDatatypeVariety variety;

		// Token: 0x04001980 RID: 6528
		private RestrictionFacets restriction;

		// Token: 0x04001981 RID: 6529
		private DatatypeImplementation baseType;

		// Token: 0x04001982 RID: 6530
		private XmlValueConverter valueConverter;

		// Token: 0x04001983 RID: 6531
		private XmlSchemaType parentSchemaType;

		// Token: 0x04001984 RID: 6532
		private static Hashtable builtinTypes = new Hashtable();

		// Token: 0x04001985 RID: 6533
		private static XmlSchemaSimpleType[] enumToTypeCode = new XmlSchemaSimpleType[55];

		// Token: 0x04001986 RID: 6534
		private static XmlSchemaSimpleType anySimpleType;

		// Token: 0x04001987 RID: 6535
		private static XmlSchemaSimpleType anyAtomicType;

		// Token: 0x04001988 RID: 6536
		private static XmlSchemaSimpleType untypedAtomicType;

		// Token: 0x04001989 RID: 6537
		private static XmlSchemaSimpleType yearMonthDurationType;

		// Token: 0x0400198A RID: 6538
		private static XmlSchemaSimpleType dayTimeDurationType;

		// Token: 0x0400198B RID: 6539
		private static volatile XmlSchemaSimpleType normalizedStringTypeV1Compat;

		// Token: 0x0400198C RID: 6540
		private static volatile XmlSchemaSimpleType tokenTypeV1Compat;

		// Token: 0x0400198D RID: 6541
		private const int anySimpleTypeIndex = 11;

		// Token: 0x0400198E RID: 6542
		internal static XmlQualifiedName QnAnySimpleType = new XmlQualifiedName("anySimpleType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x0400198F RID: 6543
		internal static XmlQualifiedName QnAnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04001990 RID: 6544
		internal static FacetsChecker stringFacetsChecker = new StringFacetsChecker();

		// Token: 0x04001991 RID: 6545
		internal static FacetsChecker miscFacetsChecker = new MiscFacetsChecker();

		// Token: 0x04001992 RID: 6546
		internal static FacetsChecker numeric2FacetsChecker = new Numeric2FacetsChecker();

		// Token: 0x04001993 RID: 6547
		internal static FacetsChecker binaryFacetsChecker = new BinaryFacetsChecker();

		// Token: 0x04001994 RID: 6548
		internal static FacetsChecker dateTimeFacetsChecker = new DateTimeFacetsChecker();

		// Token: 0x04001995 RID: 6549
		internal static FacetsChecker durationFacetsChecker = new DurationFacetsChecker();

		// Token: 0x04001996 RID: 6550
		internal static FacetsChecker listFacetsChecker = new ListFacetsChecker();

		// Token: 0x04001997 RID: 6551
		internal static FacetsChecker qnameFacetsChecker = new QNameFacetsChecker();

		// Token: 0x04001998 RID: 6552
		internal static FacetsChecker unionFacetsChecker = new UnionFacetsChecker();

		// Token: 0x04001999 RID: 6553
		private static readonly DatatypeImplementation c_anySimpleType = new Datatype_anySimpleType();

		// Token: 0x0400199A RID: 6554
		private static readonly DatatypeImplementation c_anyURI = new Datatype_anyURI();

		// Token: 0x0400199B RID: 6555
		private static readonly DatatypeImplementation c_base64Binary = new Datatype_base64Binary();

		// Token: 0x0400199C RID: 6556
		private static readonly DatatypeImplementation c_boolean = new Datatype_boolean();

		// Token: 0x0400199D RID: 6557
		private static readonly DatatypeImplementation c_byte = new Datatype_byte();

		// Token: 0x0400199E RID: 6558
		private static readonly DatatypeImplementation c_char = new Datatype_char();

		// Token: 0x0400199F RID: 6559
		private static readonly DatatypeImplementation c_date = new Datatype_date();

		// Token: 0x040019A0 RID: 6560
		private static readonly DatatypeImplementation c_dateTime = new Datatype_dateTime();

		// Token: 0x040019A1 RID: 6561
		private static readonly DatatypeImplementation c_dateTimeNoTz = new Datatype_dateTimeNoTimeZone();

		// Token: 0x040019A2 RID: 6562
		private static readonly DatatypeImplementation c_dateTimeTz = new Datatype_dateTimeTimeZone();

		// Token: 0x040019A3 RID: 6563
		private static readonly DatatypeImplementation c_day = new Datatype_day();

		// Token: 0x040019A4 RID: 6564
		private static readonly DatatypeImplementation c_decimal = new Datatype_decimal();

		// Token: 0x040019A5 RID: 6565
		private static readonly DatatypeImplementation c_double = new Datatype_double();

		// Token: 0x040019A6 RID: 6566
		private static readonly DatatypeImplementation c_doubleXdr = new Datatype_doubleXdr();

		// Token: 0x040019A7 RID: 6567
		private static readonly DatatypeImplementation c_duration = new Datatype_duration();

		// Token: 0x040019A8 RID: 6568
		private static readonly DatatypeImplementation c_ENTITY = new Datatype_ENTITY();

		// Token: 0x040019A9 RID: 6569
		private static readonly DatatypeImplementation c_ENTITIES = (DatatypeImplementation)DatatypeImplementation.c_ENTITY.DeriveByList(1, null);

		// Token: 0x040019AA RID: 6570
		private static readonly DatatypeImplementation c_ENUMERATION = new Datatype_ENUMERATION();

		// Token: 0x040019AB RID: 6571
		private static readonly DatatypeImplementation c_fixed = new Datatype_fixed();

		// Token: 0x040019AC RID: 6572
		private static readonly DatatypeImplementation c_float = new Datatype_float();

		// Token: 0x040019AD RID: 6573
		private static readonly DatatypeImplementation c_floatXdr = new Datatype_floatXdr();

		// Token: 0x040019AE RID: 6574
		private static readonly DatatypeImplementation c_hexBinary = new Datatype_hexBinary();

		// Token: 0x040019AF RID: 6575
		private static readonly DatatypeImplementation c_ID = new Datatype_ID();

		// Token: 0x040019B0 RID: 6576
		private static readonly DatatypeImplementation c_IDREF = new Datatype_IDREF();

		// Token: 0x040019B1 RID: 6577
		private static readonly DatatypeImplementation c_IDREFS = (DatatypeImplementation)DatatypeImplementation.c_IDREF.DeriveByList(1, null);

		// Token: 0x040019B2 RID: 6578
		private static readonly DatatypeImplementation c_int = new Datatype_int();

		// Token: 0x040019B3 RID: 6579
		private static readonly DatatypeImplementation c_integer = new Datatype_integer();

		// Token: 0x040019B4 RID: 6580
		private static readonly DatatypeImplementation c_language = new Datatype_language();

		// Token: 0x040019B5 RID: 6581
		private static readonly DatatypeImplementation c_long = new Datatype_long();

		// Token: 0x040019B6 RID: 6582
		private static readonly DatatypeImplementation c_month = new Datatype_month();

		// Token: 0x040019B7 RID: 6583
		private static readonly DatatypeImplementation c_monthDay = new Datatype_monthDay();

		// Token: 0x040019B8 RID: 6584
		private static readonly DatatypeImplementation c_Name = new Datatype_Name();

		// Token: 0x040019B9 RID: 6585
		private static readonly DatatypeImplementation c_NCName = new Datatype_NCName();

		// Token: 0x040019BA RID: 6586
		private static readonly DatatypeImplementation c_negativeInteger = new Datatype_negativeInteger();

		// Token: 0x040019BB RID: 6587
		private static readonly DatatypeImplementation c_NMTOKEN = new Datatype_NMTOKEN();

		// Token: 0x040019BC RID: 6588
		private static readonly DatatypeImplementation c_NMTOKENS = (DatatypeImplementation)DatatypeImplementation.c_NMTOKEN.DeriveByList(1, null);

		// Token: 0x040019BD RID: 6589
		private static readonly DatatypeImplementation c_nonNegativeInteger = new Datatype_nonNegativeInteger();

		// Token: 0x040019BE RID: 6590
		private static readonly DatatypeImplementation c_nonPositiveInteger = new Datatype_nonPositiveInteger();

		// Token: 0x040019BF RID: 6591
		private static readonly DatatypeImplementation c_normalizedString = new Datatype_normalizedString();

		// Token: 0x040019C0 RID: 6592
		private static readonly DatatypeImplementation c_NOTATION = new Datatype_NOTATION();

		// Token: 0x040019C1 RID: 6593
		private static readonly DatatypeImplementation c_positiveInteger = new Datatype_positiveInteger();

		// Token: 0x040019C2 RID: 6594
		private static readonly DatatypeImplementation c_QName = new Datatype_QName();

		// Token: 0x040019C3 RID: 6595
		private static readonly DatatypeImplementation c_QNameXdr = new Datatype_QNameXdr();

		// Token: 0x040019C4 RID: 6596
		private static readonly DatatypeImplementation c_short = new Datatype_short();

		// Token: 0x040019C5 RID: 6597
		private static readonly DatatypeImplementation c_string = new Datatype_string();

		// Token: 0x040019C6 RID: 6598
		private static readonly DatatypeImplementation c_time = new Datatype_time();

		// Token: 0x040019C7 RID: 6599
		private static readonly DatatypeImplementation c_timeNoTz = new Datatype_timeNoTimeZone();

		// Token: 0x040019C8 RID: 6600
		private static readonly DatatypeImplementation c_timeTz = new Datatype_timeTimeZone();

		// Token: 0x040019C9 RID: 6601
		private static readonly DatatypeImplementation c_token = new Datatype_token();

		// Token: 0x040019CA RID: 6602
		private static readonly DatatypeImplementation c_unsignedByte = new Datatype_unsignedByte();

		// Token: 0x040019CB RID: 6603
		private static readonly DatatypeImplementation c_unsignedInt = new Datatype_unsignedInt();

		// Token: 0x040019CC RID: 6604
		private static readonly DatatypeImplementation c_unsignedLong = new Datatype_unsignedLong();

		// Token: 0x040019CD RID: 6605
		private static readonly DatatypeImplementation c_unsignedShort = new Datatype_unsignedShort();

		// Token: 0x040019CE RID: 6606
		private static readonly DatatypeImplementation c_uuid = new Datatype_uuid();

		// Token: 0x040019CF RID: 6607
		private static readonly DatatypeImplementation c_year = new Datatype_year();

		// Token: 0x040019D0 RID: 6608
		private static readonly DatatypeImplementation c_yearMonth = new Datatype_yearMonth();

		// Token: 0x040019D1 RID: 6609
		internal static readonly DatatypeImplementation c_normalizedStringV1Compat = new Datatype_normalizedStringV1Compat();

		// Token: 0x040019D2 RID: 6610
		internal static readonly DatatypeImplementation c_tokenV1Compat = new Datatype_tokenV1Compat();

		// Token: 0x040019D3 RID: 6611
		private static readonly DatatypeImplementation c_anyAtomicType = new Datatype_anyAtomicType();

		// Token: 0x040019D4 RID: 6612
		private static readonly DatatypeImplementation c_dayTimeDuration = new Datatype_dayTimeDuration();

		// Token: 0x040019D5 RID: 6613
		private static readonly DatatypeImplementation c_untypedAtomicType = new Datatype_untypedAtomicType();

		// Token: 0x040019D6 RID: 6614
		private static readonly DatatypeImplementation c_yearMonthDuration = new Datatype_yearMonthDuration();

		// Token: 0x040019D7 RID: 6615
		private static readonly DatatypeImplementation[] c_tokenizedTypes;

		// Token: 0x040019D8 RID: 6616
		private static readonly DatatypeImplementation[] c_tokenizedTypesXsd;

		// Token: 0x040019D9 RID: 6617
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XdrTypes;

		// Token: 0x040019DA RID: 6618
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XsdTypes;

		// Token: 0x020003B3 RID: 947
		private class SchemaDatatypeMap : IComparable
		{
			// Token: 0x060025D6 RID: 9686 RVA: 0x000E3910 File Offset: 0x000E1B10
			internal SchemaDatatypeMap(string name, DatatypeImplementation type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x060025D7 RID: 9687 RVA: 0x000E3926 File Offset: 0x000E1B26
			internal SchemaDatatypeMap(string name, DatatypeImplementation type, int parentIndex)
			{
				this.name = name;
				this.type = type;
				this.parentIndex = parentIndex;
			}

			// Token: 0x060025D8 RID: 9688 RVA: 0x000E3943 File Offset: 0x000E1B43
			public static explicit operator DatatypeImplementation(DatatypeImplementation.SchemaDatatypeMap sdm)
			{
				return sdm.type;
			}

			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000E394B File Offset: 0x000E1B4B
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x060025DA RID: 9690 RVA: 0x000E3953 File Offset: 0x000E1B53
			public int ParentIndex
			{
				get
				{
					return this.parentIndex;
				}
			}

			// Token: 0x060025DB RID: 9691 RVA: 0x000E395B File Offset: 0x000E1B5B
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x040019DB RID: 6619
			private string name;

			// Token: 0x040019DC RID: 6620
			private DatatypeImplementation type;

			// Token: 0x040019DD RID: 6621
			private int parentIndex;
		}
	}
}
