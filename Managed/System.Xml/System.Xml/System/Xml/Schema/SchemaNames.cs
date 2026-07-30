using System;

namespace System.Xml.Schema
{
	// Token: 0x0200041B RID: 1051
	internal sealed class SchemaNames
	{
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000F99F7 File Offset: 0x000F7BF7
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000F9A00 File Offset: 0x000F7C00
		public SchemaNames(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.NsDataType = nameTable.Add("urn:schemas-microsoft-com:datatypes");
			this.NsDataTypeAlias = nameTable.Add("uuid:C2F41010-65B3-11D1-A29F-00AA00C14882");
			this.NsDataTypeOld = nameTable.Add("urn:uuid:C2F41010-65B3-11D1-A29F-00AA00C14882/");
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.NsXmlNs = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXdr = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this.NsXdrAlias = nameTable.Add("uuid:BDC6E3F0-6DA3-11D1-A2A3-00AA00C14882");
			this.NsXs = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = nameTable.Add("type");
			this.XsiNil = nameTable.Add("nil");
			this.XsiSchemaLocation = nameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = nameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = nameTable.Add("schema");
			this.XdrSchema = nameTable.Add("Schema");
			this.QnPCData = new XmlQualifiedName(nameTable.Add("#PCDATA"));
			this.QnXml = new XmlQualifiedName(nameTable.Add("xml"));
			this.QnXmlNs = new XmlQualifiedName(nameTable.Add("xmlns"), this.NsXmlNs);
			this.QnDtDt = new XmlQualifiedName(nameTable.Add("dt"), this.NsDataType);
			this.QnXmlLang = new XmlQualifiedName(nameTable.Add("lang"), this.NsXml);
			this.QnName = new XmlQualifiedName(nameTable.Add("name"));
			this.QnType = new XmlQualifiedName(nameTable.Add("type"));
			this.QnMaxOccurs = new XmlQualifiedName(nameTable.Add("maxOccurs"));
			this.QnMinOccurs = new XmlQualifiedName(nameTable.Add("minOccurs"));
			this.QnInfinite = new XmlQualifiedName(nameTable.Add("*"));
			this.QnModel = new XmlQualifiedName(nameTable.Add("model"));
			this.QnOpen = new XmlQualifiedName(nameTable.Add("open"));
			this.QnClosed = new XmlQualifiedName(nameTable.Add("closed"));
			this.QnContent = new XmlQualifiedName(nameTable.Add("content"));
			this.QnMixed = new XmlQualifiedName(nameTable.Add("mixed"));
			this.QnEmpty = new XmlQualifiedName(nameTable.Add("empty"));
			this.QnEltOnly = new XmlQualifiedName(nameTable.Add("eltOnly"));
			this.QnTextOnly = new XmlQualifiedName(nameTable.Add("textOnly"));
			this.QnOrder = new XmlQualifiedName(nameTable.Add("order"));
			this.QnSeq = new XmlQualifiedName(nameTable.Add("seq"));
			this.QnOne = new XmlQualifiedName(nameTable.Add("one"));
			this.QnMany = new XmlQualifiedName(nameTable.Add("many"));
			this.QnRequired = new XmlQualifiedName(nameTable.Add("required"));
			this.QnYes = new XmlQualifiedName(nameTable.Add("yes"));
			this.QnNo = new XmlQualifiedName(nameTable.Add("no"));
			this.QnString = new XmlQualifiedName(nameTable.Add("string"));
			this.QnID = new XmlQualifiedName(nameTable.Add("id"));
			this.QnIDRef = new XmlQualifiedName(nameTable.Add("idref"));
			this.QnIDRefs = new XmlQualifiedName(nameTable.Add("idrefs"));
			this.QnEntity = new XmlQualifiedName(nameTable.Add("entity"));
			this.QnEntities = new XmlQualifiedName(nameTable.Add("entities"));
			this.QnNmToken = new XmlQualifiedName(nameTable.Add("nmtoken"));
			this.QnNmTokens = new XmlQualifiedName(nameTable.Add("nmtokens"));
			this.QnEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"));
			this.QnDefault = new XmlQualifiedName(nameTable.Add("default"));
			this.QnTargetNamespace = new XmlQualifiedName(nameTable.Add("targetNamespace"));
			this.QnVersion = new XmlQualifiedName(nameTable.Add("version"));
			this.QnFinalDefault = new XmlQualifiedName(nameTable.Add("finalDefault"));
			this.QnBlockDefault = new XmlQualifiedName(nameTable.Add("blockDefault"));
			this.QnFixed = new XmlQualifiedName(nameTable.Add("fixed"));
			this.QnAbstract = new XmlQualifiedName(nameTable.Add("abstract"));
			this.QnBlock = new XmlQualifiedName(nameTable.Add("block"));
			this.QnSubstitutionGroup = new XmlQualifiedName(nameTable.Add("substitutionGroup"));
			this.QnFinal = new XmlQualifiedName(nameTable.Add("final"));
			this.QnNillable = new XmlQualifiedName(nameTable.Add("nillable"));
			this.QnRef = new XmlQualifiedName(nameTable.Add("ref"));
			this.QnBase = new XmlQualifiedName(nameTable.Add("base"));
			this.QnDerivedBy = new XmlQualifiedName(nameTable.Add("derivedBy"));
			this.QnNamespace = new XmlQualifiedName(nameTable.Add("namespace"));
			this.QnProcessContents = new XmlQualifiedName(nameTable.Add("processContents"));
			this.QnRefer = new XmlQualifiedName(nameTable.Add("refer"));
			this.QnPublic = new XmlQualifiedName(nameTable.Add("public"));
			this.QnSystem = new XmlQualifiedName(nameTable.Add("system"));
			this.QnSchemaLocation = new XmlQualifiedName(nameTable.Add("schemaLocation"));
			this.QnValue = new XmlQualifiedName(nameTable.Add("value"));
			this.QnUse = new XmlQualifiedName(nameTable.Add("use"));
			this.QnForm = new XmlQualifiedName(nameTable.Add("form"));
			this.QnAttributeFormDefault = new XmlQualifiedName(nameTable.Add("attributeFormDefault"));
			this.QnElementFormDefault = new XmlQualifiedName(nameTable.Add("elementFormDefault"));
			this.QnSource = new XmlQualifiedName(nameTable.Add("source"));
			this.QnMemberTypes = new XmlQualifiedName(nameTable.Add("memberTypes"));
			this.QnItemType = new XmlQualifiedName(nameTable.Add("itemType"));
			this.QnXPath = new XmlQualifiedName(nameTable.Add("xpath"));
			this.QnXdrSchema = new XmlQualifiedName(this.XdrSchema, this.NsXdr);
			this.QnXdrElementType = new XmlQualifiedName(nameTable.Add("ElementType"), this.NsXdr);
			this.QnXdrElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXdr);
			this.QnXdrGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXdr);
			this.QnXdrAttributeType = new XmlQualifiedName(nameTable.Add("AttributeType"), this.NsXdr);
			this.QnXdrAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXdr);
			this.QnXdrDataType = new XmlQualifiedName(nameTable.Add("datatype"), this.NsXdr);
			this.QnXdrDescription = new XmlQualifiedName(nameTable.Add("description"), this.NsXdr);
			this.QnXdrExtends = new XmlQualifiedName(nameTable.Add("extends"), this.NsXdr);
			this.QnXdrAliasSchema = new XmlQualifiedName(nameTable.Add("Schema"), this.NsDataTypeAlias);
			this.QnDtType = new XmlQualifiedName(nameTable.Add("type"), this.NsDataType);
			this.QnDtValues = new XmlQualifiedName(nameTable.Add("values"), this.NsDataType);
			this.QnDtMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsDataType);
			this.QnDtMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsDataType);
			this.QnDtMax = new XmlQualifiedName(nameTable.Add("max"), this.NsDataType);
			this.QnDtMin = new XmlQualifiedName(nameTable.Add("min"), this.NsDataType);
			this.QnDtMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsDataType);
			this.QnDtMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsDataType);
			this.QnXsdSchema = new XmlQualifiedName(this.XsdSchema, this.NsXs);
			this.QnXsdAnnotation = new XmlQualifiedName(nameTable.Add("annotation"), this.NsXs);
			this.QnXsdInclude = new XmlQualifiedName(nameTable.Add("include"), this.NsXs);
			this.QnXsdImport = new XmlQualifiedName(nameTable.Add("import"), this.NsXs);
			this.QnXsdElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXs);
			this.QnXsdAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXs);
			this.QnXsdAttributeGroup = new XmlQualifiedName(nameTable.Add("attributeGroup"), this.NsXs);
			this.QnXsdAnyAttribute = new XmlQualifiedName(nameTable.Add("anyAttribute"), this.NsXs);
			this.QnXsdGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXs);
			this.QnXsdAll = new XmlQualifiedName(nameTable.Add("all"), this.NsXs);
			this.QnXsdChoice = new XmlQualifiedName(nameTable.Add("choice"), this.NsXs);
			this.QnXsdSequence = new XmlQualifiedName(nameTable.Add("sequence"), this.NsXs);
			this.QnXsdAny = new XmlQualifiedName(nameTable.Add("any"), this.NsXs);
			this.QnXsdNotation = new XmlQualifiedName(nameTable.Add("notation"), this.NsXs);
			this.QnXsdSimpleType = new XmlQualifiedName(nameTable.Add("simpleType"), this.NsXs);
			this.QnXsdComplexType = new XmlQualifiedName(nameTable.Add("complexType"), this.NsXs);
			this.QnXsdUnique = new XmlQualifiedName(nameTable.Add("unique"), this.NsXs);
			this.QnXsdKey = new XmlQualifiedName(nameTable.Add("key"), this.NsXs);
			this.QnXsdKeyRef = new XmlQualifiedName(nameTable.Add("keyref"), this.NsXs);
			this.QnXsdSelector = new XmlQualifiedName(nameTable.Add("selector"), this.NsXs);
			this.QnXsdField = new XmlQualifiedName(nameTable.Add("field"), this.NsXs);
			this.QnXsdMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsXs);
			this.QnXsdMinInclusive = new XmlQualifiedName(nameTable.Add("minInclusive"), this.NsXs);
			this.QnXsdMaxInclusive = new XmlQualifiedName(nameTable.Add("maxInclusive"), this.NsXs);
			this.QnXsdMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsXs);
			this.QnXsdTotalDigits = new XmlQualifiedName(nameTable.Add("totalDigits"), this.NsXs);
			this.QnXsdFractionDigits = new XmlQualifiedName(nameTable.Add("fractionDigits"), this.NsXs);
			this.QnXsdLength = new XmlQualifiedName(nameTable.Add("length"), this.NsXs);
			this.QnXsdMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsXs);
			this.QnXsdMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsXs);
			this.QnXsdEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"), this.NsXs);
			this.QnXsdPattern = new XmlQualifiedName(nameTable.Add("pattern"), this.NsXs);
			this.QnXsdDocumentation = new XmlQualifiedName(nameTable.Add("documentation"), this.NsXs);
			this.QnXsdAppinfo = new XmlQualifiedName(nameTable.Add("appinfo"), this.NsXs);
			this.QnXsdComplexContent = new XmlQualifiedName(nameTable.Add("complexContent"), this.NsXs);
			this.QnXsdSimpleContent = new XmlQualifiedName(nameTable.Add("simpleContent"), this.NsXs);
			this.QnXsdRestriction = new XmlQualifiedName(nameTable.Add("restriction"), this.NsXs);
			this.QnXsdExtension = new XmlQualifiedName(nameTable.Add("extension"), this.NsXs);
			this.QnXsdUnion = new XmlQualifiedName(nameTable.Add("union"), this.NsXs);
			this.QnXsdList = new XmlQualifiedName(nameTable.Add("list"), this.NsXs);
			this.QnXsdWhiteSpace = new XmlQualifiedName(nameTable.Add("whiteSpace"), this.NsXs);
			this.QnXsdRedefine = new XmlQualifiedName(nameTable.Add("redefine"), this.NsXs);
			this.QnXsdAnyType = new XmlQualifiedName(nameTable.Add("anyType"), this.NsXs);
			this.CreateTokenToQNameTable();
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000FA74C File Offset: 0x000F894C
		public void CreateTokenToQNameTable()
		{
			this.TokenToQName[1] = this.QnName;
			this.TokenToQName[2] = this.QnType;
			this.TokenToQName[3] = this.QnMaxOccurs;
			this.TokenToQName[4] = this.QnMinOccurs;
			this.TokenToQName[5] = this.QnInfinite;
			this.TokenToQName[6] = this.QnModel;
			this.TokenToQName[7] = this.QnOpen;
			this.TokenToQName[8] = this.QnClosed;
			this.TokenToQName[9] = this.QnContent;
			this.TokenToQName[10] = this.QnMixed;
			this.TokenToQName[11] = this.QnEmpty;
			this.TokenToQName[12] = this.QnEltOnly;
			this.TokenToQName[13] = this.QnTextOnly;
			this.TokenToQName[14] = this.QnOrder;
			this.TokenToQName[15] = this.QnSeq;
			this.TokenToQName[16] = this.QnOne;
			this.TokenToQName[17] = this.QnMany;
			this.TokenToQName[18] = this.QnRequired;
			this.TokenToQName[19] = this.QnYes;
			this.TokenToQName[20] = this.QnNo;
			this.TokenToQName[21] = this.QnString;
			this.TokenToQName[22] = this.QnID;
			this.TokenToQName[23] = this.QnIDRef;
			this.TokenToQName[24] = this.QnIDRefs;
			this.TokenToQName[25] = this.QnEntity;
			this.TokenToQName[26] = this.QnEntities;
			this.TokenToQName[27] = this.QnNmToken;
			this.TokenToQName[28] = this.QnNmTokens;
			this.TokenToQName[29] = this.QnEnumeration;
			this.TokenToQName[30] = this.QnDefault;
			this.TokenToQName[31] = this.QnXdrSchema;
			this.TokenToQName[32] = this.QnXdrElementType;
			this.TokenToQName[33] = this.QnXdrElement;
			this.TokenToQName[34] = this.QnXdrGroup;
			this.TokenToQName[35] = this.QnXdrAttributeType;
			this.TokenToQName[36] = this.QnXdrAttribute;
			this.TokenToQName[37] = this.QnXdrDataType;
			this.TokenToQName[38] = this.QnXdrDescription;
			this.TokenToQName[39] = this.QnXdrExtends;
			this.TokenToQName[40] = this.QnXdrAliasSchema;
			this.TokenToQName[41] = this.QnDtType;
			this.TokenToQName[42] = this.QnDtValues;
			this.TokenToQName[43] = this.QnDtMaxLength;
			this.TokenToQName[44] = this.QnDtMinLength;
			this.TokenToQName[45] = this.QnDtMax;
			this.TokenToQName[46] = this.QnDtMin;
			this.TokenToQName[47] = this.QnDtMinExclusive;
			this.TokenToQName[48] = this.QnDtMaxExclusive;
			this.TokenToQName[49] = this.QnTargetNamespace;
			this.TokenToQName[50] = this.QnVersion;
			this.TokenToQName[51] = this.QnFinalDefault;
			this.TokenToQName[52] = this.QnBlockDefault;
			this.TokenToQName[53] = this.QnFixed;
			this.TokenToQName[54] = this.QnAbstract;
			this.TokenToQName[55] = this.QnBlock;
			this.TokenToQName[56] = this.QnSubstitutionGroup;
			this.TokenToQName[57] = this.QnFinal;
			this.TokenToQName[58] = this.QnNillable;
			this.TokenToQName[59] = this.QnRef;
			this.TokenToQName[60] = this.QnBase;
			this.TokenToQName[61] = this.QnDerivedBy;
			this.TokenToQName[62] = this.QnNamespace;
			this.TokenToQName[63] = this.QnProcessContents;
			this.TokenToQName[64] = this.QnRefer;
			this.TokenToQName[65] = this.QnPublic;
			this.TokenToQName[66] = this.QnSystem;
			this.TokenToQName[67] = this.QnSchemaLocation;
			this.TokenToQName[68] = this.QnValue;
			this.TokenToQName[119] = this.QnItemType;
			this.TokenToQName[120] = this.QnMemberTypes;
			this.TokenToQName[121] = this.QnXPath;
			this.TokenToQName[74] = this.QnXsdSchema;
			this.TokenToQName[75] = this.QnXsdAnnotation;
			this.TokenToQName[76] = this.QnXsdInclude;
			this.TokenToQName[77] = this.QnXsdImport;
			this.TokenToQName[78] = this.QnXsdElement;
			this.TokenToQName[79] = this.QnXsdAttribute;
			this.TokenToQName[80] = this.QnXsdAttributeGroup;
			this.TokenToQName[81] = this.QnXsdAnyAttribute;
			this.TokenToQName[82] = this.QnXsdGroup;
			this.TokenToQName[83] = this.QnXsdAll;
			this.TokenToQName[84] = this.QnXsdChoice;
			this.TokenToQName[85] = this.QnXsdSequence;
			this.TokenToQName[86] = this.QnXsdAny;
			this.TokenToQName[87] = this.QnXsdNotation;
			this.TokenToQName[88] = this.QnXsdSimpleType;
			this.TokenToQName[89] = this.QnXsdComplexType;
			this.TokenToQName[90] = this.QnXsdUnique;
			this.TokenToQName[91] = this.QnXsdKey;
			this.TokenToQName[92] = this.QnXsdKeyRef;
			this.TokenToQName[93] = this.QnXsdSelector;
			this.TokenToQName[94] = this.QnXsdField;
			this.TokenToQName[95] = this.QnXsdMinExclusive;
			this.TokenToQName[96] = this.QnXsdMinInclusive;
			this.TokenToQName[97] = this.QnXsdMaxExclusive;
			this.TokenToQName[98] = this.QnXsdMaxInclusive;
			this.TokenToQName[99] = this.QnXsdTotalDigits;
			this.TokenToQName[100] = this.QnXsdFractionDigits;
			this.TokenToQName[101] = this.QnXsdLength;
			this.TokenToQName[102] = this.QnXsdMinLength;
			this.TokenToQName[103] = this.QnXsdMaxLength;
			this.TokenToQName[104] = this.QnXsdEnumeration;
			this.TokenToQName[105] = this.QnXsdPattern;
			this.TokenToQName[117] = this.QnXsdWhiteSpace;
			this.TokenToQName[106] = this.QnXsdDocumentation;
			this.TokenToQName[107] = this.QnXsdAppinfo;
			this.TokenToQName[108] = this.QnXsdComplexContent;
			this.TokenToQName[110] = this.QnXsdRestriction;
			this.TokenToQName[113] = this.QnXsdRestriction;
			this.TokenToQName[115] = this.QnXsdRestriction;
			this.TokenToQName[109] = this.QnXsdExtension;
			this.TokenToQName[112] = this.QnXsdExtension;
			this.TokenToQName[111] = this.QnXsdSimpleContent;
			this.TokenToQName[116] = this.QnXsdUnion;
			this.TokenToQName[114] = this.QnXsdList;
			this.TokenToQName[118] = this.QnXsdRedefine;
			this.TokenToQName[69] = this.QnSource;
			this.TokenToQName[72] = this.QnUse;
			this.TokenToQName[73] = this.QnForm;
			this.TokenToQName[71] = this.QnElementFormDefault;
			this.TokenToQName[70] = this.QnAttributeFormDefault;
			this.TokenToQName[122] = this.QnXmlLang;
			this.TokenToQName[0] = XmlQualifiedName.Empty;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000FAE84 File Offset: 0x000F9084
		public SchemaType SchemaTypeFromRoot(string localName, string ns)
		{
			if (this.IsXSDRoot(localName, ns))
			{
				return SchemaType.XSD;
			}
			if (this.IsXDRRoot(localName, XmlSchemaDatatype.XdrCanonizeUri(ns, this.nameTable, this)))
			{
				return SchemaType.XDR;
			}
			return SchemaType.None;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000FAEAB File Offset: 0x000F90AB
		public bool IsXSDRoot(string localName, string ns)
		{
			return localName == this.XsdSchema && ns == this.NsXs;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000FAEC9 File Offset: 0x000F90C9
		public bool IsXDRRoot(string localName, string ns)
		{
			return localName == this.XdrSchema && ns == this.NsXdr;
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000FAEE7 File Offset: 0x000F90E7
		public XmlQualifiedName GetName(SchemaNames.Token token)
		{
			return this.TokenToQName[(int)token];
		}

		// Token: 0x04001B3D RID: 6973
		private XmlNameTable nameTable;

		// Token: 0x04001B3E RID: 6974
		public string NsDataType;

		// Token: 0x04001B3F RID: 6975
		public string NsDataTypeAlias;

		// Token: 0x04001B40 RID: 6976
		public string NsDataTypeOld;

		// Token: 0x04001B41 RID: 6977
		public string NsXml;

		// Token: 0x04001B42 RID: 6978
		public string NsXmlNs;

		// Token: 0x04001B43 RID: 6979
		public string NsXdr;

		// Token: 0x04001B44 RID: 6980
		public string NsXdrAlias;

		// Token: 0x04001B45 RID: 6981
		public string NsXs;

		// Token: 0x04001B46 RID: 6982
		public string NsXsi;

		// Token: 0x04001B47 RID: 6983
		public string XsiType;

		// Token: 0x04001B48 RID: 6984
		public string XsiNil;

		// Token: 0x04001B49 RID: 6985
		public string XsiSchemaLocation;

		// Token: 0x04001B4A RID: 6986
		public string XsiNoNamespaceSchemaLocation;

		// Token: 0x04001B4B RID: 6987
		public string XsdSchema;

		// Token: 0x04001B4C RID: 6988
		public string XdrSchema;

		// Token: 0x04001B4D RID: 6989
		public XmlQualifiedName QnPCData;

		// Token: 0x04001B4E RID: 6990
		public XmlQualifiedName QnXml;

		// Token: 0x04001B4F RID: 6991
		public XmlQualifiedName QnXmlNs;

		// Token: 0x04001B50 RID: 6992
		public XmlQualifiedName QnDtDt;

		// Token: 0x04001B51 RID: 6993
		public XmlQualifiedName QnXmlLang;

		// Token: 0x04001B52 RID: 6994
		public XmlQualifiedName QnName;

		// Token: 0x04001B53 RID: 6995
		public XmlQualifiedName QnType;

		// Token: 0x04001B54 RID: 6996
		public XmlQualifiedName QnMaxOccurs;

		// Token: 0x04001B55 RID: 6997
		public XmlQualifiedName QnMinOccurs;

		// Token: 0x04001B56 RID: 6998
		public XmlQualifiedName QnInfinite;

		// Token: 0x04001B57 RID: 6999
		public XmlQualifiedName QnModel;

		// Token: 0x04001B58 RID: 7000
		public XmlQualifiedName QnOpen;

		// Token: 0x04001B59 RID: 7001
		public XmlQualifiedName QnClosed;

		// Token: 0x04001B5A RID: 7002
		public XmlQualifiedName QnContent;

		// Token: 0x04001B5B RID: 7003
		public XmlQualifiedName QnMixed;

		// Token: 0x04001B5C RID: 7004
		public XmlQualifiedName QnEmpty;

		// Token: 0x04001B5D RID: 7005
		public XmlQualifiedName QnEltOnly;

		// Token: 0x04001B5E RID: 7006
		public XmlQualifiedName QnTextOnly;

		// Token: 0x04001B5F RID: 7007
		public XmlQualifiedName QnOrder;

		// Token: 0x04001B60 RID: 7008
		public XmlQualifiedName QnSeq;

		// Token: 0x04001B61 RID: 7009
		public XmlQualifiedName QnOne;

		// Token: 0x04001B62 RID: 7010
		public XmlQualifiedName QnMany;

		// Token: 0x04001B63 RID: 7011
		public XmlQualifiedName QnRequired;

		// Token: 0x04001B64 RID: 7012
		public XmlQualifiedName QnYes;

		// Token: 0x04001B65 RID: 7013
		public XmlQualifiedName QnNo;

		// Token: 0x04001B66 RID: 7014
		public XmlQualifiedName QnString;

		// Token: 0x04001B67 RID: 7015
		public XmlQualifiedName QnID;

		// Token: 0x04001B68 RID: 7016
		public XmlQualifiedName QnIDRef;

		// Token: 0x04001B69 RID: 7017
		public XmlQualifiedName QnIDRefs;

		// Token: 0x04001B6A RID: 7018
		public XmlQualifiedName QnEntity;

		// Token: 0x04001B6B RID: 7019
		public XmlQualifiedName QnEntities;

		// Token: 0x04001B6C RID: 7020
		public XmlQualifiedName QnNmToken;

		// Token: 0x04001B6D RID: 7021
		public XmlQualifiedName QnNmTokens;

		// Token: 0x04001B6E RID: 7022
		public XmlQualifiedName QnEnumeration;

		// Token: 0x04001B6F RID: 7023
		public XmlQualifiedName QnDefault;

		// Token: 0x04001B70 RID: 7024
		public XmlQualifiedName QnXdrSchema;

		// Token: 0x04001B71 RID: 7025
		public XmlQualifiedName QnXdrElementType;

		// Token: 0x04001B72 RID: 7026
		public XmlQualifiedName QnXdrElement;

		// Token: 0x04001B73 RID: 7027
		public XmlQualifiedName QnXdrGroup;

		// Token: 0x04001B74 RID: 7028
		public XmlQualifiedName QnXdrAttributeType;

		// Token: 0x04001B75 RID: 7029
		public XmlQualifiedName QnXdrAttribute;

		// Token: 0x04001B76 RID: 7030
		public XmlQualifiedName QnXdrDataType;

		// Token: 0x04001B77 RID: 7031
		public XmlQualifiedName QnXdrDescription;

		// Token: 0x04001B78 RID: 7032
		public XmlQualifiedName QnXdrExtends;

		// Token: 0x04001B79 RID: 7033
		public XmlQualifiedName QnXdrAliasSchema;

		// Token: 0x04001B7A RID: 7034
		public XmlQualifiedName QnDtType;

		// Token: 0x04001B7B RID: 7035
		public XmlQualifiedName QnDtValues;

		// Token: 0x04001B7C RID: 7036
		public XmlQualifiedName QnDtMaxLength;

		// Token: 0x04001B7D RID: 7037
		public XmlQualifiedName QnDtMinLength;

		// Token: 0x04001B7E RID: 7038
		public XmlQualifiedName QnDtMax;

		// Token: 0x04001B7F RID: 7039
		public XmlQualifiedName QnDtMin;

		// Token: 0x04001B80 RID: 7040
		public XmlQualifiedName QnDtMinExclusive;

		// Token: 0x04001B81 RID: 7041
		public XmlQualifiedName QnDtMaxExclusive;

		// Token: 0x04001B82 RID: 7042
		public XmlQualifiedName QnTargetNamespace;

		// Token: 0x04001B83 RID: 7043
		public XmlQualifiedName QnVersion;

		// Token: 0x04001B84 RID: 7044
		public XmlQualifiedName QnFinalDefault;

		// Token: 0x04001B85 RID: 7045
		public XmlQualifiedName QnBlockDefault;

		// Token: 0x04001B86 RID: 7046
		public XmlQualifiedName QnFixed;

		// Token: 0x04001B87 RID: 7047
		public XmlQualifiedName QnAbstract;

		// Token: 0x04001B88 RID: 7048
		public XmlQualifiedName QnBlock;

		// Token: 0x04001B89 RID: 7049
		public XmlQualifiedName QnSubstitutionGroup;

		// Token: 0x04001B8A RID: 7050
		public XmlQualifiedName QnFinal;

		// Token: 0x04001B8B RID: 7051
		public XmlQualifiedName QnNillable;

		// Token: 0x04001B8C RID: 7052
		public XmlQualifiedName QnRef;

		// Token: 0x04001B8D RID: 7053
		public XmlQualifiedName QnBase;

		// Token: 0x04001B8E RID: 7054
		public XmlQualifiedName QnDerivedBy;

		// Token: 0x04001B8F RID: 7055
		public XmlQualifiedName QnNamespace;

		// Token: 0x04001B90 RID: 7056
		public XmlQualifiedName QnProcessContents;

		// Token: 0x04001B91 RID: 7057
		public XmlQualifiedName QnRefer;

		// Token: 0x04001B92 RID: 7058
		public XmlQualifiedName QnPublic;

		// Token: 0x04001B93 RID: 7059
		public XmlQualifiedName QnSystem;

		// Token: 0x04001B94 RID: 7060
		public XmlQualifiedName QnSchemaLocation;

		// Token: 0x04001B95 RID: 7061
		public XmlQualifiedName QnValue;

		// Token: 0x04001B96 RID: 7062
		public XmlQualifiedName QnUse;

		// Token: 0x04001B97 RID: 7063
		public XmlQualifiedName QnForm;

		// Token: 0x04001B98 RID: 7064
		public XmlQualifiedName QnElementFormDefault;

		// Token: 0x04001B99 RID: 7065
		public XmlQualifiedName QnAttributeFormDefault;

		// Token: 0x04001B9A RID: 7066
		public XmlQualifiedName QnItemType;

		// Token: 0x04001B9B RID: 7067
		public XmlQualifiedName QnMemberTypes;

		// Token: 0x04001B9C RID: 7068
		public XmlQualifiedName QnXPath;

		// Token: 0x04001B9D RID: 7069
		public XmlQualifiedName QnXsdSchema;

		// Token: 0x04001B9E RID: 7070
		public XmlQualifiedName QnXsdAnnotation;

		// Token: 0x04001B9F RID: 7071
		public XmlQualifiedName QnXsdInclude;

		// Token: 0x04001BA0 RID: 7072
		public XmlQualifiedName QnXsdImport;

		// Token: 0x04001BA1 RID: 7073
		public XmlQualifiedName QnXsdElement;

		// Token: 0x04001BA2 RID: 7074
		public XmlQualifiedName QnXsdAttribute;

		// Token: 0x04001BA3 RID: 7075
		public XmlQualifiedName QnXsdAttributeGroup;

		// Token: 0x04001BA4 RID: 7076
		public XmlQualifiedName QnXsdAnyAttribute;

		// Token: 0x04001BA5 RID: 7077
		public XmlQualifiedName QnXsdGroup;

		// Token: 0x04001BA6 RID: 7078
		public XmlQualifiedName QnXsdAll;

		// Token: 0x04001BA7 RID: 7079
		public XmlQualifiedName QnXsdChoice;

		// Token: 0x04001BA8 RID: 7080
		public XmlQualifiedName QnXsdSequence;

		// Token: 0x04001BA9 RID: 7081
		public XmlQualifiedName QnXsdAny;

		// Token: 0x04001BAA RID: 7082
		public XmlQualifiedName QnXsdNotation;

		// Token: 0x04001BAB RID: 7083
		public XmlQualifiedName QnXsdSimpleType;

		// Token: 0x04001BAC RID: 7084
		public XmlQualifiedName QnXsdComplexType;

		// Token: 0x04001BAD RID: 7085
		public XmlQualifiedName QnXsdUnique;

		// Token: 0x04001BAE RID: 7086
		public XmlQualifiedName QnXsdKey;

		// Token: 0x04001BAF RID: 7087
		public XmlQualifiedName QnXsdKeyRef;

		// Token: 0x04001BB0 RID: 7088
		public XmlQualifiedName QnXsdSelector;

		// Token: 0x04001BB1 RID: 7089
		public XmlQualifiedName QnXsdField;

		// Token: 0x04001BB2 RID: 7090
		public XmlQualifiedName QnXsdMinExclusive;

		// Token: 0x04001BB3 RID: 7091
		public XmlQualifiedName QnXsdMinInclusive;

		// Token: 0x04001BB4 RID: 7092
		public XmlQualifiedName QnXsdMaxInclusive;

		// Token: 0x04001BB5 RID: 7093
		public XmlQualifiedName QnXsdMaxExclusive;

		// Token: 0x04001BB6 RID: 7094
		public XmlQualifiedName QnXsdTotalDigits;

		// Token: 0x04001BB7 RID: 7095
		public XmlQualifiedName QnXsdFractionDigits;

		// Token: 0x04001BB8 RID: 7096
		public XmlQualifiedName QnXsdLength;

		// Token: 0x04001BB9 RID: 7097
		public XmlQualifiedName QnXsdMinLength;

		// Token: 0x04001BBA RID: 7098
		public XmlQualifiedName QnXsdMaxLength;

		// Token: 0x04001BBB RID: 7099
		public XmlQualifiedName QnXsdEnumeration;

		// Token: 0x04001BBC RID: 7100
		public XmlQualifiedName QnXsdPattern;

		// Token: 0x04001BBD RID: 7101
		public XmlQualifiedName QnXsdDocumentation;

		// Token: 0x04001BBE RID: 7102
		public XmlQualifiedName QnXsdAppinfo;

		// Token: 0x04001BBF RID: 7103
		public XmlQualifiedName QnSource;

		// Token: 0x04001BC0 RID: 7104
		public XmlQualifiedName QnXsdComplexContent;

		// Token: 0x04001BC1 RID: 7105
		public XmlQualifiedName QnXsdSimpleContent;

		// Token: 0x04001BC2 RID: 7106
		public XmlQualifiedName QnXsdRestriction;

		// Token: 0x04001BC3 RID: 7107
		public XmlQualifiedName QnXsdExtension;

		// Token: 0x04001BC4 RID: 7108
		public XmlQualifiedName QnXsdUnion;

		// Token: 0x04001BC5 RID: 7109
		public XmlQualifiedName QnXsdList;

		// Token: 0x04001BC6 RID: 7110
		public XmlQualifiedName QnXsdWhiteSpace;

		// Token: 0x04001BC7 RID: 7111
		public XmlQualifiedName QnXsdRedefine;

		// Token: 0x04001BC8 RID: 7112
		public XmlQualifiedName QnXsdAnyType;

		// Token: 0x04001BC9 RID: 7113
		internal XmlQualifiedName[] TokenToQName = new XmlQualifiedName[123];

		// Token: 0x0200041C RID: 1052
		public enum Token
		{
			// Token: 0x04001BCB RID: 7115
			Empty,
			// Token: 0x04001BCC RID: 7116
			SchemaName,
			// Token: 0x04001BCD RID: 7117
			SchemaType,
			// Token: 0x04001BCE RID: 7118
			SchemaMaxOccurs,
			// Token: 0x04001BCF RID: 7119
			SchemaMinOccurs,
			// Token: 0x04001BD0 RID: 7120
			SchemaInfinite,
			// Token: 0x04001BD1 RID: 7121
			SchemaModel,
			// Token: 0x04001BD2 RID: 7122
			SchemaOpen,
			// Token: 0x04001BD3 RID: 7123
			SchemaClosed,
			// Token: 0x04001BD4 RID: 7124
			SchemaContent,
			// Token: 0x04001BD5 RID: 7125
			SchemaMixed,
			// Token: 0x04001BD6 RID: 7126
			SchemaEmpty,
			// Token: 0x04001BD7 RID: 7127
			SchemaElementOnly,
			// Token: 0x04001BD8 RID: 7128
			SchemaTextOnly,
			// Token: 0x04001BD9 RID: 7129
			SchemaOrder,
			// Token: 0x04001BDA RID: 7130
			SchemaSeq,
			// Token: 0x04001BDB RID: 7131
			SchemaOne,
			// Token: 0x04001BDC RID: 7132
			SchemaMany,
			// Token: 0x04001BDD RID: 7133
			SchemaRequired,
			// Token: 0x04001BDE RID: 7134
			SchemaYes,
			// Token: 0x04001BDF RID: 7135
			SchemaNo,
			// Token: 0x04001BE0 RID: 7136
			SchemaString,
			// Token: 0x04001BE1 RID: 7137
			SchemaId,
			// Token: 0x04001BE2 RID: 7138
			SchemaIdref,
			// Token: 0x04001BE3 RID: 7139
			SchemaIdrefs,
			// Token: 0x04001BE4 RID: 7140
			SchemaEntity,
			// Token: 0x04001BE5 RID: 7141
			SchemaEntities,
			// Token: 0x04001BE6 RID: 7142
			SchemaNmtoken,
			// Token: 0x04001BE7 RID: 7143
			SchemaNmtokens,
			// Token: 0x04001BE8 RID: 7144
			SchemaEnumeration,
			// Token: 0x04001BE9 RID: 7145
			SchemaDefault,
			// Token: 0x04001BEA RID: 7146
			XdrRoot,
			// Token: 0x04001BEB RID: 7147
			XdrElementType,
			// Token: 0x04001BEC RID: 7148
			XdrElement,
			// Token: 0x04001BED RID: 7149
			XdrGroup,
			// Token: 0x04001BEE RID: 7150
			XdrAttributeType,
			// Token: 0x04001BEF RID: 7151
			XdrAttribute,
			// Token: 0x04001BF0 RID: 7152
			XdrDatatype,
			// Token: 0x04001BF1 RID: 7153
			XdrDescription,
			// Token: 0x04001BF2 RID: 7154
			XdrExtends,
			// Token: 0x04001BF3 RID: 7155
			SchemaXdrRootAlias,
			// Token: 0x04001BF4 RID: 7156
			SchemaDtType,
			// Token: 0x04001BF5 RID: 7157
			SchemaDtValues,
			// Token: 0x04001BF6 RID: 7158
			SchemaDtMaxLength,
			// Token: 0x04001BF7 RID: 7159
			SchemaDtMinLength,
			// Token: 0x04001BF8 RID: 7160
			SchemaDtMax,
			// Token: 0x04001BF9 RID: 7161
			SchemaDtMin,
			// Token: 0x04001BFA RID: 7162
			SchemaDtMinExclusive,
			// Token: 0x04001BFB RID: 7163
			SchemaDtMaxExclusive,
			// Token: 0x04001BFC RID: 7164
			SchemaTargetNamespace,
			// Token: 0x04001BFD RID: 7165
			SchemaVersion,
			// Token: 0x04001BFE RID: 7166
			SchemaFinalDefault,
			// Token: 0x04001BFF RID: 7167
			SchemaBlockDefault,
			// Token: 0x04001C00 RID: 7168
			SchemaFixed,
			// Token: 0x04001C01 RID: 7169
			SchemaAbstract,
			// Token: 0x04001C02 RID: 7170
			SchemaBlock,
			// Token: 0x04001C03 RID: 7171
			SchemaSubstitutionGroup,
			// Token: 0x04001C04 RID: 7172
			SchemaFinal,
			// Token: 0x04001C05 RID: 7173
			SchemaNillable,
			// Token: 0x04001C06 RID: 7174
			SchemaRef,
			// Token: 0x04001C07 RID: 7175
			SchemaBase,
			// Token: 0x04001C08 RID: 7176
			SchemaDerivedBy,
			// Token: 0x04001C09 RID: 7177
			SchemaNamespace,
			// Token: 0x04001C0A RID: 7178
			SchemaProcessContents,
			// Token: 0x04001C0B RID: 7179
			SchemaRefer,
			// Token: 0x04001C0C RID: 7180
			SchemaPublic,
			// Token: 0x04001C0D RID: 7181
			SchemaSystem,
			// Token: 0x04001C0E RID: 7182
			SchemaSchemaLocation,
			// Token: 0x04001C0F RID: 7183
			SchemaValue,
			// Token: 0x04001C10 RID: 7184
			SchemaSource,
			// Token: 0x04001C11 RID: 7185
			SchemaAttributeFormDefault,
			// Token: 0x04001C12 RID: 7186
			SchemaElementFormDefault,
			// Token: 0x04001C13 RID: 7187
			SchemaUse,
			// Token: 0x04001C14 RID: 7188
			SchemaForm,
			// Token: 0x04001C15 RID: 7189
			XsdSchema,
			// Token: 0x04001C16 RID: 7190
			XsdAnnotation,
			// Token: 0x04001C17 RID: 7191
			XsdInclude,
			// Token: 0x04001C18 RID: 7192
			XsdImport,
			// Token: 0x04001C19 RID: 7193
			XsdElement,
			// Token: 0x04001C1A RID: 7194
			XsdAttribute,
			// Token: 0x04001C1B RID: 7195
			xsdAttributeGroup,
			// Token: 0x04001C1C RID: 7196
			XsdAnyAttribute,
			// Token: 0x04001C1D RID: 7197
			XsdGroup,
			// Token: 0x04001C1E RID: 7198
			XsdAll,
			// Token: 0x04001C1F RID: 7199
			XsdChoice,
			// Token: 0x04001C20 RID: 7200
			XsdSequence,
			// Token: 0x04001C21 RID: 7201
			XsdAny,
			// Token: 0x04001C22 RID: 7202
			XsdNotation,
			// Token: 0x04001C23 RID: 7203
			XsdSimpleType,
			// Token: 0x04001C24 RID: 7204
			XsdComplexType,
			// Token: 0x04001C25 RID: 7205
			XsdUnique,
			// Token: 0x04001C26 RID: 7206
			XsdKey,
			// Token: 0x04001C27 RID: 7207
			XsdKeyref,
			// Token: 0x04001C28 RID: 7208
			XsdSelector,
			// Token: 0x04001C29 RID: 7209
			XsdField,
			// Token: 0x04001C2A RID: 7210
			XsdMinExclusive,
			// Token: 0x04001C2B RID: 7211
			XsdMinInclusive,
			// Token: 0x04001C2C RID: 7212
			XsdMaxExclusive,
			// Token: 0x04001C2D RID: 7213
			XsdMaxInclusive,
			// Token: 0x04001C2E RID: 7214
			XsdTotalDigits,
			// Token: 0x04001C2F RID: 7215
			XsdFractionDigits,
			// Token: 0x04001C30 RID: 7216
			XsdLength,
			// Token: 0x04001C31 RID: 7217
			XsdMinLength,
			// Token: 0x04001C32 RID: 7218
			XsdMaxLength,
			// Token: 0x04001C33 RID: 7219
			XsdEnumeration,
			// Token: 0x04001C34 RID: 7220
			XsdPattern,
			// Token: 0x04001C35 RID: 7221
			XsdDocumentation,
			// Token: 0x04001C36 RID: 7222
			XsdAppInfo,
			// Token: 0x04001C37 RID: 7223
			XsdComplexContent,
			// Token: 0x04001C38 RID: 7224
			XsdComplexContentExtension,
			// Token: 0x04001C39 RID: 7225
			XsdComplexContentRestriction,
			// Token: 0x04001C3A RID: 7226
			XsdSimpleContent,
			// Token: 0x04001C3B RID: 7227
			XsdSimpleContentExtension,
			// Token: 0x04001C3C RID: 7228
			XsdSimpleContentRestriction,
			// Token: 0x04001C3D RID: 7229
			XsdSimpleTypeList,
			// Token: 0x04001C3E RID: 7230
			XsdSimpleTypeRestriction,
			// Token: 0x04001C3F RID: 7231
			XsdSimpleTypeUnion,
			// Token: 0x04001C40 RID: 7232
			XsdWhitespace,
			// Token: 0x04001C41 RID: 7233
			XsdRedefine,
			// Token: 0x04001C42 RID: 7234
			SchemaItemType,
			// Token: 0x04001C43 RID: 7235
			SchemaMemberTypes,
			// Token: 0x04001C44 RID: 7236
			SchemaXPath,
			// Token: 0x04001C45 RID: 7237
			XmlLang
		}
	}
}
