using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	/// <summary>Controls deserialization by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </summary>
	// Token: 0x0200034B RID: 843
	public abstract class XmlSerializationReader : XmlSerializationGeneratedCode
	{
		// Token: 0x060020EE RID: 8430 RVA: 0x000B8A40 File Offset: 0x000B6C40
		static XmlSerializationReader()
		{
			XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
			XmlSerializationReader.checkDeserializeAdvances = xmlSerializerSection != null && xmlSerializerSection.CheckDeserializeAdvances;
		}

		/// <summary>Stores element and attribute names in a <see cref="T:System.Xml.NameTable" /> object. </summary>
		// Token: 0x060020EF RID: 8431
		protected abstract void InitIDs();

		// Token: 0x060020F0 RID: 8432 RVA: 0x000B8A70 File Offset: 0x000B6C70
		internal void Init(XmlReader r, XmlDeserializationEvents events, string encodingStyle, TempAssembly tempAssembly)
		{
			this.events = events;
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				this.countingReader = new XmlCountingReader(r);
				this.r = this.countingReader;
			}
			else
			{
				this.r = r;
			}
			this.d = null;
			this.soap12 = encodingStyle == "http://www.w3.org/2003/05/soap-encoding";
			base.Init(tempAssembly);
			this.schemaNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.schemaNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema");
			this.schemaNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema");
			this.schemaNonXsdTypesNsID = r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.instanceNsID = r.NameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.instanceNs2000ID = r.NameTable.Add("http://www.w3.org/2000/10/XMLSchema-instance");
			this.instanceNs1999ID = r.NameTable.Add("http://www.w3.org/1999/XMLSchema-instance");
			this.soapNsID = r.NameTable.Add("http://schemas.xmlsoap.org/soap/encoding/");
			this.soap12NsID = r.NameTable.Add("http://www.w3.org/2003/05/soap-encoding");
			this.schemaID = r.NameTable.Add("schema");
			this.wsdlNsID = r.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
			this.wsdlArrayTypeID = r.NameTable.Add("arrayType");
			this.nullID = r.NameTable.Add("null");
			this.nilID = r.NameTable.Add("nil");
			this.typeID = r.NameTable.Add("type");
			this.arrayTypeID = r.NameTable.Add("arrayType");
			this.itemTypeID = r.NameTable.Add("itemType");
			this.arraySizeID = r.NameTable.Add("arraySize");
			this.arrayID = r.NameTable.Add("Array");
			this.urTypeID = r.NameTable.Add("anyType");
			this.InitIDs();
		}

		/// <summary>Gets or sets a value that determines whether XML strings are translated into valid .NET Framework type names.</summary>
		/// <returns>true if XML strings are decoded into valid .NET Framework type names; otherwise, false.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x000B8C8A File Offset: 0x000B6E8A
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x000B8C92 File Offset: 0x000B6E92
		protected bool DecodeName
		{
			get
			{
				return this.decodeName;
			}
			set
			{
				this.decodeName = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlReader" /> object that is being used by <see cref="T:System.Xml.Serialization.XmlSerializationReader" />. </summary>
		/// <returns>The <see cref="T:System.Xml.XmlReader" /> that is being used by the <see cref="T:System.Xml.Serialization.XmlSerializationReader" />.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x000B8C9B File Offset: 0x000B6E9B
		protected XmlReader Reader
		{
			get
			{
				return this.r;
			}
		}

		/// <summary>Gets the current count of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The current count of an <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x000B8CA3 File Offset: 0x000B6EA3
		protected int ReaderCount
		{
			get
			{
				if (!XmlSerializationReader.checkDeserializeAdvances)
				{
					return 0;
				}
				return this.countingReader.AdvanceCount;
			}
		}

		/// <summary>Gets the XML document object into which the XML document is being deserialized. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that represents the deserialized <see cref="T:System.Xml.XmlDocument" /> data.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x000B8CB9 File Offset: 0x000B6EB9
		protected XmlDocument Document
		{
			get
			{
				if (this.d == null)
				{
					this.d = new XmlDocument(this.r.NameTable);
					this.d.SetBaseURI(this.r.BaseURI);
				}
				return this.d;
			}
		}

		/// <summary>Gets a dynamically generated assembly by name.</summary>
		/// <returns>A dynamically generated <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <param name="assemblyFullName">The full name of the assembly.</param>
		// Token: 0x060020F6 RID: 8438 RVA: 0x000B8CF5 File Offset: 0x000B6EF5
		protected static Assembly ResolveDynamicAssembly(string assemblyFullName)
		{
			return DynamicAssemblies.Get(assemblyFullName);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000B8D00 File Offset: 0x000B6F00
		private void InitPrimitiveIDs()
		{
			if (this.tokenID != null)
			{
				return;
			}
			this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.r.NameTable.Add("http://microsoft.com/wsdl/types/");
			this.stringID = this.r.NameTable.Add("string");
			this.intID = this.r.NameTable.Add("int");
			this.booleanID = this.r.NameTable.Add("boolean");
			this.shortID = this.r.NameTable.Add("short");
			this.longID = this.r.NameTable.Add("long");
			this.floatID = this.r.NameTable.Add("float");
			this.doubleID = this.r.NameTable.Add("double");
			this.decimalID = this.r.NameTable.Add("decimal");
			this.dateTimeID = this.r.NameTable.Add("dateTime");
			this.qnameID = this.r.NameTable.Add("QName");
			this.dateID = this.r.NameTable.Add("date");
			this.timeID = this.r.NameTable.Add("time");
			this.hexBinaryID = this.r.NameTable.Add("hexBinary");
			this.base64BinaryID = this.r.NameTable.Add("base64Binary");
			this.unsignedByteID = this.r.NameTable.Add("unsignedByte");
			this.byteID = this.r.NameTable.Add("byte");
			this.unsignedShortID = this.r.NameTable.Add("unsignedShort");
			this.unsignedIntID = this.r.NameTable.Add("unsignedInt");
			this.unsignedLongID = this.r.NameTable.Add("unsignedLong");
			this.oldDecimalID = this.r.NameTable.Add("decimal");
			this.oldTimeInstantID = this.r.NameTable.Add("timeInstant");
			this.charID = this.r.NameTable.Add("char");
			this.guidID = this.r.NameTable.Add("guid");
			if (LocalAppContextSwitches.EnableTimeSpanSerialization)
			{
				this.timeSpanID = this.r.NameTable.Add("TimeSpan");
			}
			this.base64ID = this.r.NameTable.Add("base64");
			this.anyURIID = this.r.NameTable.Add("anyURI");
			this.durationID = this.r.NameTable.Add("duration");
			this.ENTITYID = this.r.NameTable.Add("ENTITY");
			this.ENTITIESID = this.r.NameTable.Add("ENTITIES");
			this.gDayID = this.r.NameTable.Add("gDay");
			this.gMonthID = this.r.NameTable.Add("gMonth");
			this.gMonthDayID = this.r.NameTable.Add("gMonthDay");
			this.gYearID = this.r.NameTable.Add("gYear");
			this.gYearMonthID = this.r.NameTable.Add("gYearMonth");
			this.IDID = this.r.NameTable.Add("ID");
			this.IDREFID = this.r.NameTable.Add("IDREF");
			this.IDREFSID = this.r.NameTable.Add("IDREFS");
			this.integerID = this.r.NameTable.Add("integer");
			this.languageID = this.r.NameTable.Add("language");
			this.NameID = this.r.NameTable.Add("Name");
			this.NCNameID = this.r.NameTable.Add("NCName");
			this.NMTOKENID = this.r.NameTable.Add("NMTOKEN");
			this.NMTOKENSID = this.r.NameTable.Add("NMTOKENS");
			this.negativeIntegerID = this.r.NameTable.Add("negativeInteger");
			this.nonNegativeIntegerID = this.r.NameTable.Add("nonNegativeInteger");
			this.nonPositiveIntegerID = this.r.NameTable.Add("nonPositiveInteger");
			this.normalizedStringID = this.r.NameTable.Add("normalizedString");
			this.NOTATIONID = this.r.NameTable.Add("NOTATION");
			this.positiveIntegerID = this.r.NameTable.Add("positiveInteger");
			this.tokenID = this.r.NameTable.Add("token");
		}

		/// <summary>Gets the value of the xsi:type attribute for the XML element at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An XML qualified name that indicates the data type of an XML element.</returns>
		// Token: 0x060020F8 RID: 8440 RVA: 0x000B9290 File Offset: 0x000B7490
		protected XmlQualifiedName GetXsiType()
		{
			string text = this.r.GetAttribute(this.typeID, this.instanceNsID);
			if (text == null)
			{
				text = this.r.GetAttribute(this.typeID, this.instanceNs2000ID);
				if (text == null)
				{
					text = this.r.GetAttribute(this.typeID, this.instanceNs1999ID);
					if (text == null)
					{
						return null;
					}
				}
			}
			return this.ToXmlQualifiedName(text, false);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000B92F8 File Offset: 0x000B74F8
		private Type GetPrimitiveType(XmlQualifiedName typeName, bool throwOnUnknown)
		{
			this.InitPrimitiveIDs();
			if (typeName.Namespace == this.schemaNsID || typeName.Namespace == this.soapNsID || typeName.Namespace == this.soap12NsID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.decimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.dateTimeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.base64BinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.unsignedByteID)
				{
					return typeof(byte);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNs2000ID || typeName.Namespace == this.schemaNs1999ID)
			{
				if (typeName.Name == this.stringID || typeName.Name == this.anyURIID || typeName.Name == this.durationID || typeName.Name == this.ENTITYID || typeName.Name == this.ENTITIESID || typeName.Name == this.gDayID || typeName.Name == this.gMonthID || typeName.Name == this.gMonthDayID || typeName.Name == this.gYearID || typeName.Name == this.gYearMonthID || typeName.Name == this.IDID || typeName.Name == this.IDREFID || typeName.Name == this.IDREFSID || typeName.Name == this.integerID || typeName.Name == this.languageID || typeName.Name == this.NameID || typeName.Name == this.NCNameID || typeName.Name == this.NMTOKENID || typeName.Name == this.NMTOKENSID || typeName.Name == this.negativeIntegerID || typeName.Name == this.nonPositiveIntegerID || typeName.Name == this.nonNegativeIntegerID || typeName.Name == this.normalizedStringID || typeName.Name == this.NOTATIONID || typeName.Name == this.positiveIntegerID || typeName.Name == this.tokenID)
				{
					return typeof(string);
				}
				if (typeName.Name == this.intID)
				{
					return typeof(int);
				}
				if (typeName.Name == this.booleanID)
				{
					return typeof(bool);
				}
				if (typeName.Name == this.shortID)
				{
					return typeof(short);
				}
				if (typeName.Name == this.longID)
				{
					return typeof(long);
				}
				if (typeName.Name == this.floatID)
				{
					return typeof(float);
				}
				if (typeName.Name == this.doubleID)
				{
					return typeof(double);
				}
				if (typeName.Name == this.oldDecimalID)
				{
					return typeof(decimal);
				}
				if (typeName.Name == this.oldTimeInstantID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.qnameID)
				{
					return typeof(XmlQualifiedName);
				}
				if (typeName.Name == this.dateID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.timeID)
				{
					return typeof(DateTime);
				}
				if (typeName.Name == this.hexBinaryID)
				{
					return typeof(byte[]);
				}
				if (typeName.Name == this.byteID)
				{
					return typeof(sbyte);
				}
				if (typeName.Name == this.unsignedShortID)
				{
					return typeof(ushort);
				}
				if (typeName.Name == this.unsignedIntID)
				{
					return typeof(uint);
				}
				if (typeName.Name == this.unsignedLongID)
				{
					return typeof(ulong);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else if (typeName.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (typeName.Name == this.charID)
				{
					return typeof(char);
				}
				if (typeName.Name == this.guidID)
				{
					return typeof(Guid);
				}
				throw this.CreateUnknownTypeException(typeName);
			}
			else
			{
				if (throwOnUnknown)
				{
					throw this.CreateUnknownTypeException(typeName);
				}
				return null;
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000B9A5B File Offset: 0x000B7C5B
		private bool IsPrimitiveNamespace(string ns)
		{
			return ns == this.schemaNsID || ns == this.schemaNonXsdTypesNsID || ns == this.soapNsID || ns == this.soap12NsID || ns == this.schemaNs2000ID || ns == this.schemaNs1999ID;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000B9A95 File Offset: 0x000B7C95
		private string ReadStringValue()
		{
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return string.Empty;
			}
			this.r.ReadStartElement();
			string text = this.r.ReadString();
			this.ReadEndElement();
			return text;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000B9AD4 File Offset: 0x000B7CD4
		private XmlQualifiedName ReadXmlQualifiedName()
		{
			bool flag = false;
			string text;
			if (this.r.IsEmptyElement)
			{
				text = string.Empty;
				flag = true;
			}
			else
			{
				this.r.ReadStartElement();
				text = this.r.ReadString();
			}
			XmlQualifiedName xmlQualifiedName = this.ToXmlQualifiedName(text);
			if (flag)
			{
				this.r.Skip();
				return xmlQualifiedName;
			}
			this.ReadEndElement();
			return xmlQualifiedName;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000B9B30 File Offset: 0x000B7D30
		private byte[] ReadByteArray(bool isBase64)
		{
			ArrayList arrayList = new ArrayList();
			int num = 1024;
			int num2 = -1;
			int num3 = 0;
			int num4 = 0;
			byte[] array = new byte[num];
			arrayList.Add(array);
			while (num2 != 0)
			{
				if (num3 == array.Length)
				{
					num = Math.Min(num * 2, 65536);
					array = new byte[num];
					num3 = 0;
					arrayList.Add(array);
				}
				if (isBase64)
				{
					num2 = this.r.ReadElementContentAsBase64(array, num3, array.Length - num3);
				}
				else
				{
					num2 = this.r.ReadElementContentAsBinHex(array, num3, array.Length - num3);
				}
				num3 += num2;
				num4 += num2;
			}
			byte[] array2 = new byte[num4];
			num3 = 0;
			foreach (object obj in arrayList)
			{
				byte[] array3 = (byte[])obj;
				num = Math.Min(array3.Length, num4);
				if (num > 0)
				{
					Buffer.BlockCopy(array3, 0, array2, num3, num);
					num3 += num;
					num4 -= num;
				}
			}
			arrayList.Clear();
			return array2;
		}

		/// <summary>Gets the value of the XML node at which the <see cref="T:System.Xml.XmlReader" /> is currently positioned. </summary>
		/// <returns>The value of the node as a .NET Framework value type, if the value is a simple XML Schema data type.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x060020FE RID: 8446 RVA: 0x000B9C4C File Offset: 0x000B7E4C
		protected object ReadTypedPrimitive(XmlQualifiedName type)
		{
			return this.ReadTypedPrimitive(type, false);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000B9C58 File Offset: 0x000B7E58
		private object ReadTypedPrimitive(XmlQualifiedName type, bool elementCanBeType)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return this.ReadXmlNodes(elementCanBeType);
			}
			object obj;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					obj = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					obj = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					obj = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					obj = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					obj = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					obj = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					obj = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					obj = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.decimalID)
				{
					obj = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.dateTimeID)
				{
					obj = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					obj = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					obj = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					obj = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					obj = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					obj = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					obj = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					obj = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					obj = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else if (type.Name == this.hexBinaryID)
				{
					obj = this.ToByteArrayHex(false);
				}
				else if (type.Name == this.base64BinaryID)
				{
					obj = this.ToByteArrayBase64(false);
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					obj = this.ToByteArrayBase64(false);
				}
				else
				{
					obj = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNs2000ID || type.Namespace == this.schemaNs1999ID)
			{
				if (type.Name == this.stringID || type.Name == this.normalizedStringID)
				{
					obj = this.ReadStringValue();
				}
				else if (type.Name == this.anyURIID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					obj = this.CollapseWhitespace(this.ReadStringValue());
				}
				else if (type.Name == this.intID)
				{
					obj = XmlConvert.ToInt32(this.ReadStringValue());
				}
				else if (type.Name == this.booleanID)
				{
					obj = XmlConvert.ToBoolean(this.ReadStringValue());
				}
				else if (type.Name == this.shortID)
				{
					obj = XmlConvert.ToInt16(this.ReadStringValue());
				}
				else if (type.Name == this.longID)
				{
					obj = XmlConvert.ToInt64(this.ReadStringValue());
				}
				else if (type.Name == this.floatID)
				{
					obj = XmlConvert.ToSingle(this.ReadStringValue());
				}
				else if (type.Name == this.doubleID)
				{
					obj = XmlConvert.ToDouble(this.ReadStringValue());
				}
				else if (type.Name == this.oldDecimalID)
				{
					obj = XmlConvert.ToDecimal(this.ReadStringValue());
				}
				else if (type.Name == this.oldTimeInstantID)
				{
					obj = XmlSerializationReader.ToDateTime(this.ReadStringValue());
				}
				else if (type.Name == this.qnameID)
				{
					obj = this.ReadXmlQualifiedName();
				}
				else if (type.Name == this.dateID)
				{
					obj = XmlSerializationReader.ToDate(this.ReadStringValue());
				}
				else if (type.Name == this.timeID)
				{
					obj = XmlSerializationReader.ToTime(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedByteID)
				{
					obj = XmlConvert.ToByte(this.ReadStringValue());
				}
				else if (type.Name == this.byteID)
				{
					obj = XmlConvert.ToSByte(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedShortID)
				{
					obj = XmlConvert.ToUInt16(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedIntID)
				{
					obj = XmlConvert.ToUInt32(this.ReadStringValue());
				}
				else if (type.Name == this.unsignedLongID)
				{
					obj = XmlConvert.ToUInt64(this.ReadStringValue());
				}
				else
				{
					obj = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					obj = XmlSerializationReader.ToChar(this.ReadStringValue());
				}
				else if (type.Name == this.guidID)
				{
					obj = new Guid(this.CollapseWhitespace(this.ReadStringValue()));
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					obj = XmlConvert.ToTimeSpan(this.ReadStringValue());
				}
				else
				{
					obj = this.ReadXmlNodes(elementCanBeType);
				}
			}
			else
			{
				obj = this.ReadXmlNodes(elementCanBeType);
			}
			return obj;
		}

		/// <summary>Reads an XML element that allows null values (xsi:nil = 'true') and returns a generic <see cref="T:System.Nullable`1" /> value. </summary>
		/// <returns>A generic <see cref="T:System.Nullable`1" /> that represents a null XML value.</returns>
		/// <param name="type">The <see cref="T:System.Xml.XmlQualifiedName" /> that represents the simple data type for the current location of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06002100 RID: 8448 RVA: 0x000BA5DC File Offset: 0x000B87DC
		protected object ReadTypedNull(XmlQualifiedName type)
		{
			this.InitPrimitiveIDs();
			if (!this.IsPrimitiveNamespace(type.Namespace) || type.Name == this.urTypeID)
			{
				return null;
			}
			object obj;
			if (type.Namespace == this.schemaNsID || type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID)
			{
				if (type.Name == this.stringID || type.Name == this.anyURIID || type.Name == this.durationID || type.Name == this.ENTITYID || type.Name == this.ENTITIESID || type.Name == this.gDayID || type.Name == this.gMonthID || type.Name == this.gMonthDayID || type.Name == this.gYearID || type.Name == this.gYearMonthID || type.Name == this.IDID || type.Name == this.IDREFID || type.Name == this.IDREFSID || type.Name == this.integerID || type.Name == this.languageID || type.Name == this.NameID || type.Name == this.NCNameID || type.Name == this.NMTOKENID || type.Name == this.NMTOKENSID || type.Name == this.negativeIntegerID || type.Name == this.nonPositiveIntegerID || type.Name == this.nonNegativeIntegerID || type.Name == this.normalizedStringID || type.Name == this.NOTATIONID || type.Name == this.positiveIntegerID || type.Name == this.tokenID)
				{
					obj = null;
				}
				else if (type.Name == this.intID)
				{
					obj = null;
				}
				else if (type.Name == this.booleanID)
				{
					obj = null;
				}
				else if (type.Name == this.shortID)
				{
					obj = null;
				}
				else if (type.Name == this.longID)
				{
					obj = null;
				}
				else if (type.Name == this.floatID)
				{
					obj = null;
				}
				else if (type.Name == this.doubleID)
				{
					obj = null;
				}
				else if (type.Name == this.decimalID)
				{
					obj = null;
				}
				else if (type.Name == this.dateTimeID)
				{
					obj = null;
				}
				else if (type.Name == this.qnameID)
				{
					obj = null;
				}
				else if (type.Name == this.dateID)
				{
					obj = null;
				}
				else if (type.Name == this.timeID)
				{
					obj = null;
				}
				else if (type.Name == this.unsignedByteID)
				{
					obj = null;
				}
				else if (type.Name == this.byteID)
				{
					obj = null;
				}
				else if (type.Name == this.unsignedShortID)
				{
					obj = null;
				}
				else if (type.Name == this.unsignedIntID)
				{
					obj = null;
				}
				else if (type.Name == this.unsignedLongID)
				{
					obj = null;
				}
				else if (type.Name == this.hexBinaryID)
				{
					obj = null;
				}
				else if (type.Name == this.base64BinaryID)
				{
					obj = null;
				}
				else if (type.Name == this.base64ID && (type.Namespace == this.soapNsID || type.Namespace == this.soap12NsID))
				{
					obj = null;
				}
				else
				{
					obj = null;
				}
			}
			else if (type.Namespace == this.schemaNonXsdTypesNsID)
			{
				if (type.Name == this.charID)
				{
					obj = null;
				}
				else if (type.Name == this.guidID)
				{
					obj = null;
				}
				else if (type.Name == this.timeSpanID && LocalAppContextSwitches.EnableTimeSpanSerialization)
				{
					obj = null;
				}
				else
				{
					obj = null;
				}
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		/// <summary>Determines whether an XML attribute name indicates an XML namespace. </summary>
		/// <returns>true if the XML attribute name indicates an XML namespace; otherwise, false.</returns>
		/// <param name="name">The name of an XML attribute.</param>
		// Token: 0x06002101 RID: 8449 RVA: 0x000BA9DD File Offset: 0x000B8BDD
		protected bool IsXmlnsAttribute(string name)
		{
			return name.StartsWith("xmlns", StringComparison.Ordinal) && (name.Length == 5 || name[5] == ':');
		}

		/// <summary>Sets the value of the XML attribute if it is of type arrayType from the Web Services Description Language (WSDL) namespace. </summary>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that may have the type wsdl:array.</param>
		// Token: 0x06002102 RID: 8450 RVA: 0x000BAA08 File Offset: 0x000B8C08
		protected void ParseWsdlArrayType(XmlAttribute attr)
		{
			if (attr.LocalName == this.wsdlArrayTypeID && attr.NamespaceURI == this.wsdlNsID)
			{
				int num = attr.Value.LastIndexOf(':');
				if (num < 0)
				{
					attr.Value = this.r.LookupNamespace("") + ":" + attr.Value;
					return;
				}
				attr.Value = this.r.LookupNamespace(attr.Value.Substring(0, num)) + ":" + attr.Value.Substring(num + 1);
			}
		}

		/// <summary>Gets or sets a value that should be true for a SOAP 1.1 return value.</summary>
		/// <returns>true, if the value is a return value. </returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000BAAA0 File Offset: 0x000B8CA0
		// (set) Token: 0x06002104 RID: 8452 RVA: 0x000BAAB5 File Offset: 0x000B8CB5
		protected bool IsReturnValue
		{
			get
			{
				return this.isReturnValue && !this.soap12;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the current XML element if the element has a null attribute with the value true. </summary>
		/// <returns>true if the element has a null="true" attribute value and has been read; otherwise, false.</returns>
		// Token: 0x06002105 RID: 8453 RVA: 0x000BAAC0 File Offset: 0x000B8CC0
		protected bool ReadNull()
		{
			if (!this.GetNullAttr())
			{
				return false;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return true;
			}
			this.r.ReadStartElement();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement)
			{
				this.UnknownNode(null);
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.ReadEndElement();
			return true;
		}

		/// <summary>Determines whether the XML element where the <see cref="T:System.Xml.XmlReader" /> is currently positioned has a null attribute set to the value true.</summary>
		/// <returns>true if <see cref="T:System.Xml.XmlReader" /> is currently positioned over a null attribute with the value true; otherwise, false.</returns>
		// Token: 0x06002106 RID: 8454 RVA: 0x000BAB30 File Offset: 0x000B8D30
		protected bool GetNullAttr()
		{
			string text = this.r.GetAttribute(this.nilID, this.instanceNsID);
			if (text == null)
			{
				text = this.r.GetAttribute(this.nullID, this.instanceNsID);
			}
			if (text == null)
			{
				text = this.r.GetAttribute(this.nullID, this.instanceNs2000ID);
				if (text == null)
				{
					text = this.r.GetAttribute(this.nullID, this.instanceNs1999ID);
				}
			}
			return text != null && XmlConvert.ToBoolean(text);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read a simple, text-only XML element that could be null. </summary>
		/// <returns>The string value; otherwise, null.</returns>
		// Token: 0x06002107 RID: 8455 RVA: 0x000BABB4 File Offset: 0x000B8DB4
		protected string ReadNullableString()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.r.ReadElementString();
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>A <see cref="T:System.Xml.XmlQualifiedName" /> that represents the fully qualified name of the current XML element; otherwise, null if a null="true" attribute value is present.</returns>
		// Token: 0x06002108 RID: 8456 RVA: 0x000BABCB File Offset: 0x000B8DCB
		protected XmlQualifiedName ReadNullableQualifiedName()
		{
			if (this.ReadNull())
			{
				return null;
			}
			return this.ReadElementQualifiedName();
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read the fully qualified name of the element where it is currently positioned. </summary>
		/// <returns>The fully qualified name of the current XML element.</returns>
		// Token: 0x06002109 RID: 8457 RVA: 0x000BABE0 File Offset: 0x000B8DE0
		protected XmlQualifiedName ReadElementQualifiedName()
		{
			if (this.r.IsEmptyElement)
			{
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(string.Empty, this.r.LookupNamespace(""));
				this.r.Skip();
				return xmlQualifiedName;
			}
			XmlQualifiedName xmlQualifiedName2 = this.ToXmlQualifiedName(this.CollapseWhitespace(this.r.ReadString()));
			this.r.ReadEndElement();
			return xmlQualifiedName2;
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read an XML document root element at its current position.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlDocument" /> that contains the root element that has been read.</returns>
		/// <param name="wrapped">true if the method should read content only after reading the element's start element; otherwise, false.</param>
		// Token: 0x0600210A RID: 8458 RVA: 0x000BAC44 File Offset: 0x000B8E44
		protected XmlDocument ReadXmlDocument(bool wrapped)
		{
			XmlNode xmlNode = this.ReadXmlNode(wrapped);
			if (xmlNode == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.AppendChild(xmlDocument.ImportNode(xmlNode, true));
			return xmlDocument;
		}

		/// <summary>Removes all occurrences of white space characters from the beginning and end of the specified string.</summary>
		/// <returns>The trimmed string.</returns>
		/// <param name="value">The string that will have its white space trimmed.</param>
		// Token: 0x0600210B RID: 8459 RVA: 0x000BAC72 File Offset: 0x000B8E72
		protected string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the XML node at its current position. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> that represents the XML node that has been read.</returns>
		/// <param name="wrapped">true to read content only after reading the element's start element; otherwise, false.</param>
		// Token: 0x0600210C RID: 8460 RVA: 0x000BAC80 File Offset: 0x000B8E80
		protected XmlNode ReadXmlNode(bool wrapped)
		{
			XmlNode xmlNode = null;
			if (wrapped)
			{
				if (this.ReadNull())
				{
					return null;
				}
				this.r.ReadStartElement();
				this.r.MoveToContent();
				if (this.r.NodeType != XmlNodeType.EndElement)
				{
					xmlNode = this.Document.ReadNode(this.r);
				}
				int num = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					this.UnknownNode(null);
					this.CheckReaderCount(ref num, ref readerCount);
				}
				this.r.ReadEndElement();
			}
			else
			{
				xmlNode = this.Document.ReadNode(this.r);
			}
			return xmlNode;
		}

		/// <summary>Produces a base-64 byte array from an input string. </summary>
		/// <returns>A base-64 byte array.</returns>
		/// <param name="value">A string to translate into a base-64 byte array.</param>
		// Token: 0x0600210D RID: 8461 RVA: 0x000BAD1F File Offset: 0x000B8F1F
		protected static byte[] ToByteArrayBase64(string value)
		{
			return XmlCustomFormatter.ToByteArrayBase64(value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a base-64 byte array.</summary>
		/// <returns>A base-64 byte array; otherwise, null if the value of the <paramref name="isNull" /> parameter is true.</returns>
		/// <param name="isNull">true to return null; false to return a base-64 byte array.</param>
		// Token: 0x0600210E RID: 8462 RVA: 0x000BAD27 File Offset: 0x000B8F27
		protected byte[] ToByteArrayBase64(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(true);
		}

		/// <summary>Produces a hexadecimal byte array from an input string.</summary>
		/// <returns>A hexadecimal byte array.</returns>
		/// <param name="value">A string to translate into a hexadecimal byte array.</param>
		// Token: 0x0600210F RID: 8463 RVA: 0x000BAD35 File Offset: 0x000B8F35
		protected static byte[] ToByteArrayHex(string value)
		{
			return XmlCustomFormatter.ToByteArrayHex(value);
		}

		/// <summary>Instructs the <see cref="T:System.Xml.XmlReader" /> to read the string value at its current position and return it as a hexadecimal byte array.</summary>
		/// <returns>A hexadecimal byte array; otherwise, null if the value of the <paramref name="isNull" /> parameter is true. </returns>
		/// <param name="isNull">true to return null; false to return a hexadecimal byte array.</param>
		// Token: 0x06002110 RID: 8464 RVA: 0x000BAD3D File Offset: 0x000B8F3D
		protected byte[] ToByteArrayHex(bool isNull)
		{
			if (isNull)
			{
				return null;
			}
			return this.ReadByteArray(false);
		}

		/// <summary>Gets the length of the SOAP-encoded array where the <see cref="T:System.Xml.XmlReader" /> is currently positioned.</summary>
		/// <returns>The length of the SOAP array.</returns>
		/// <param name="name">The local name that the array should have.</param>
		/// <param name="ns">The namespace that the array should have.</param>
		// Token: 0x06002111 RID: 8465 RVA: 0x000BAD4C File Offset: 0x000B8F4C
		protected int GetArrayLength(string name, string ns)
		{
			if (this.GetNullAttr())
			{
				return 0;
			}
			string attribute = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = this.ParseArrayType(attribute);
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[] { this.CurrentTag() }));
			}
			XmlQualifiedName xmlQualifiedName = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
			if (xmlQualifiedName.Name != name)
			{
				throw new InvalidOperationException(Res.GetString("The SOAP-ENC:arrayType references type is named '{0}'; a type named '{1}' was expected at {2}.", new object[]
				{
					xmlQualifiedName.Name,
					name,
					this.CurrentTag()
				}));
			}
			if (xmlQualifiedName.Namespace != ns)
			{
				throw new InvalidOperationException(Res.GetString("The SOAP-ENC:arrayType references type is from namespace '{0}'; the namespace '{1}' was expected at {2}.", new object[]
				{
					xmlQualifiedName.Namespace,
					ns,
					this.CurrentTag()
				}));
			}
			return soapArrayInfo.length;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000BAE34 File Offset: 0x000B9034
		private XmlSerializationReader.SoapArrayInfo ParseArrayType(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(Res.GetString("SOAP-ENC:arrayType was missing at {0}.", new object[] { this.CurrentTag() }));
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType was empty at {0}.", new object[] { this.CurrentTag() }), "value");
			}
			char[] array = value.ToCharArray();
			int num = array.Length;
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = default(XmlSerializationReader.SoapArrayInfo);
			int num2 = num - 1;
			if (array[num2] != ']')
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType must end with a ']' character."), "value");
			}
			num2--;
			while (num2 != -1 && array[num2] != '[')
			{
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[] { this.CurrentTag() }), "value");
				}
				num2--;
			}
			if (num2 == -1)
			{
				throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType has mismatched brackets."), "value");
			}
			int num3 = num - num2 - 2;
			if (num3 > 0)
			{
				string text = new string(array, num2 + 1, num3);
				try
				{
					soapArrayInfo.length = int.Parse(text, CultureInfo.InvariantCulture);
					goto IL_014F;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType could not handle '{1}' as the length of the array.", new object[] { text }), "value");
				}
			}
			soapArrayInfo.length = -1;
			IL_014F:
			num2--;
			soapArrayInfo.jaggedDimensions = 0;
			while (num2 != -1 && array[num2] == ']')
			{
				num2--;
				if (num2 < 0)
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType has mismatched brackets."), "value");
				}
				if (array[num2] == ',')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[] { this.CurrentTag() }), "value");
				}
				if (array[num2] != '[')
				{
					throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType must end with a ']' character."), "value");
				}
				num2--;
				soapArrayInfo.jaggedDimensions++;
			}
			soapArrayInfo.dimensions = 1;
			soapArrayInfo.qname = new string(array, 0, num2 + 1);
			return soapArrayInfo;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000BB048 File Offset: 0x000B9248
		private XmlSerializationReader.SoapArrayInfo ParseSoap12ArrayType(string itemType, string arraySize)
		{
			XmlSerializationReader.SoapArrayInfo soapArrayInfo = default(XmlSerializationReader.SoapArrayInfo);
			if (itemType != null && itemType.Length > 0)
			{
				soapArrayInfo.qname = itemType;
			}
			else
			{
				soapArrayInfo.qname = "";
			}
			string[] array;
			if (arraySize != null && arraySize.Length > 0)
			{
				array = arraySize.Split(null);
			}
			else
			{
				array = new string[0];
			}
			soapArrayInfo.dimensions = 0;
			soapArrayInfo.length = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length > 0)
				{
					if (array[i] == "*")
					{
						soapArrayInfo.dimensions++;
					}
					else
					{
						try
						{
							soapArrayInfo.length = int.Parse(array[i], CultureInfo.InvariantCulture);
							soapArrayInfo.dimensions++;
						}
						catch (Exception ex)
						{
							if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
							{
								throw;
							}
							throw new ArgumentException(Res.GetString("SOAP-ENC:arrayType could not handle '{1}' as the length of the array.", new object[] { array[i] }), "value");
						}
					}
				}
			}
			if (soapArrayInfo.dimensions == 0)
			{
				soapArrayInfo.dimensions = 1;
			}
			return soapArrayInfo;
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		// Token: 0x06002114 RID: 8468 RVA: 0x000BB168 File Offset: 0x000B9368
		protected static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.DateTime" />object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> class object.</param>
		// Token: 0x06002115 RID: 8469 RVA: 0x000BB170 File Offset: 0x000B9370
		protected static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDate(value);
		}

		/// <summary>Produces a <see cref="T:System.DateTime" /> from a string that represents the time. </summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.DateTime" /> object.</param>
		// Token: 0x06002116 RID: 8470 RVA: 0x000BB178 File Offset: 0x000B9378
		protected static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToTime(value);
		}

		/// <summary>Produces a <see cref="T:System.Char" /> object from an input string. </summary>
		/// <returns>A <see cref="T:System.Char" /> object.</returns>
		/// <param name="value">A string to translate into a <see cref="T:System.Char" /> object.</param>
		// Token: 0x06002117 RID: 8471 RVA: 0x000BB180 File Offset: 0x000B9380
		protected static char ToChar(string value)
		{
			return XmlCustomFormatter.ToChar(value);
		}

		/// <summary>Produces a numeric enumeration value from a string that consists of delimited identifiers that represent constants from the enumerator list. </summary>
		/// <returns>A long value that consists of the enumeration value as a series of bitwise OR operations.</returns>
		/// <param name="value">A string that consists of delimited identifiers where each identifier represents a constant from the set enumerator list.</param>
		/// <param name="h">A <see cref="T:System.Collections.Hashtable" /> that consists of the identifiers as keys and the constants as integral numbers.</param>
		/// <param name="typeName">The name of the enumeration type.</param>
		// Token: 0x06002118 RID: 8472 RVA: 0x000BB188 File Offset: 0x000B9388
		protected static long ToEnum(string value, Hashtable h, string typeName)
		{
			return XmlCustomFormatter.ToEnum(value, h, typeName, true);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x06002119 RID: 8473 RVA: 0x000BB193 File Offset: 0x000B9393
		protected static string ToXmlName(string value)
		{
			return XmlCustomFormatter.ToXmlName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x0600211A RID: 8474 RVA: 0x000BB19B File Offset: 0x000B939B
		protected static string ToXmlNCName(string value)
		{
			return XmlCustomFormatter.ToXmlNCName(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x0600211B RID: 8475 RVA: 0x000BB1A3 File Offset: 0x000B93A3
		protected static string ToXmlNmToken(string value)
		{
			return XmlCustomFormatter.ToXmlNmToken(value);
		}

		/// <summary>Decodes an XML name.</summary>
		/// <returns>A decoded string.</returns>
		/// <param name="value">An XML name to be decoded.</param>
		// Token: 0x0600211C RID: 8476 RVA: 0x000BB1AB File Offset: 0x000B93AB
		protected static string ToXmlNmTokens(string value)
		{
			return XmlCustomFormatter.ToXmlNmTokens(value);
		}

		/// <summary>Obtains an <see cref="T:System.Xml.XmlQualifiedName" /> from a name that may contain a prefix. </summary>
		/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> that represents a namespace-qualified XML name.</returns>
		/// <param name="value">A name that may contain a prefix.</param>
		// Token: 0x0600211D RID: 8477 RVA: 0x000BB1B3 File Offset: 0x000B93B3
		protected XmlQualifiedName ToXmlQualifiedName(string value)
		{
			return this.ToXmlQualifiedName(value, this.DecodeName);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000BB1C4 File Offset: 0x000B93C4
		internal XmlQualifiedName ToXmlQualifiedName(string value, bool decodeName)
		{
			int num = ((value == null) ? (-1) : value.LastIndexOf(':'));
			string text = ((num < 0) ? null : value.Substring(0, num));
			string text2 = value.Substring(num + 1);
			if (decodeName)
			{
				text = XmlConvert.DecodeName(text);
				text2 = XmlConvert.DecodeName(text2);
			}
			if (text == null || text.Length == 0)
			{
				return new XmlQualifiedName(this.r.NameTable.Add(value), this.r.LookupNamespace(string.Empty));
			}
			string text3 = this.r.LookupNamespace(text);
			if (text3 == null)
			{
				throw new InvalidOperationException(Res.GetString("Namespace prefix '{0}' is not defined.", new object[] { text }));
			}
			return new XmlQualifiedName(this.r.NameTable.Add(text2), text3);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">An <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		// Token: 0x0600211F RID: 8479 RVA: 0x000BB27E File Offset: 0x000B947E
		protected void UnknownAttribute(object o, XmlAttribute attr)
		{
			this.UnknownAttribute(o, attr, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="attr">A <see cref="T:System.Xml.XmlAttribute" /> that represents the attribute in question.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06002120 RID: 8480 RVA: 0x000BB28C File Offset: 0x000B948C
		protected void UnknownAttribute(object o, XmlAttribute attr, string qnames)
		{
			if (this.events.OnUnknownAttribute != null)
			{
				int num;
				int num2;
				this.GetCurrentPosition(out num, out num2);
				XmlAttributeEventArgs xmlAttributeEventArgs = new XmlAttributeEventArgs(attr, num, num2, o, qnames);
				this.events.OnUnknownAttribute(this.events.sender, xmlAttributeEventArgs);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The <see cref="T:System.Object" /> that is being deserialized.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		// Token: 0x06002121 RID: 8481 RVA: 0x000BB2D7 File Offset: 0x000B94D7
		protected void UnknownElement(object o, XmlElement elem)
		{
			this.UnknownElement(o, elem, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.XmlAttributeEventArgs.ObjectBeingDeserialized" /> property.</param>
		/// <param name="elem">The <see cref="T:System.Xml.XmlElement" /> for which an event is raised.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06002122 RID: 8482 RVA: 0x000BB2E4 File Offset: 0x000B94E4
		protected void UnknownElement(object o, XmlElement elem, string qnames)
		{
			if (this.events.OnUnknownElement != null)
			{
				int num;
				int num2;
				this.GetCurrentPosition(out num, out num2);
				XmlElementEventArgs xmlElementEventArgs = new XmlElementEventArgs(elem, num, num2, o, qnames);
				this.events.OnUnknownElement(this.events.sender, xmlElementEventArgs);
			}
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <param name="o">The object that is being deserialized.</param>
		// Token: 0x06002123 RID: 8483 RVA: 0x000BB32F File Offset: 0x000B952F
		protected void UnknownNode(object o)
		{
			this.UnknownNode(o, null);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="o">The object being deserialized.</param>
		/// <param name="qnames">A comma-delimited list of XML qualified names.</param>
		// Token: 0x06002124 RID: 8484 RVA: 0x000BB33C File Offset: 0x000B953C
		protected void UnknownNode(object o, string qnames)
		{
			if (this.r.NodeType == XmlNodeType.None || this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Read();
				return;
			}
			if (this.r.NodeType == XmlNodeType.EndElement)
			{
				return;
			}
			if (this.events.OnUnknownNode != null)
			{
				this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
				return;
			}
			if (this.r.NodeType == XmlNodeType.Attribute && this.events.OnUnknownAttribute == null)
			{
				return;
			}
			if (this.r.NodeType == XmlNodeType.Element && this.events.OnUnknownElement == null)
			{
				this.r.Skip();
				return;
			}
			this.UnknownNode(this.Document.ReadNode(this.r), o, qnames);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000BB408 File Offset: 0x000B9608
		private void UnknownNode(XmlNode unknownNode, object o, string qnames)
		{
			if (unknownNode == null)
			{
				return;
			}
			if (unknownNode.NodeType != XmlNodeType.None && unknownNode.NodeType != XmlNodeType.Whitespace && this.events.OnUnknownNode != null)
			{
				int num;
				int num2;
				this.GetCurrentPosition(out num, out num2);
				XmlNodeEventArgs xmlNodeEventArgs = new XmlNodeEventArgs(unknownNode, num, num2, o);
				this.events.OnUnknownNode(this.events.sender, xmlNodeEventArgs);
			}
			if (unknownNode.NodeType == XmlNodeType.Attribute)
			{
				this.UnknownAttribute(o, (XmlAttribute)unknownNode, qnames);
				return;
			}
			if (unknownNode.NodeType == XmlNodeType.Element)
			{
				this.UnknownElement(o, (XmlElement)unknownNode, qnames);
			}
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000BB498 File Offset: 0x000B9698
		private void GetCurrentPosition(out int lineNumber, out int linePosition)
		{
			if (this.Reader is IXmlLineInfo)
			{
				IXmlLineInfo xmlLineInfo = (IXmlLineInfo)this.Reader;
				lineNumber = xmlLineInfo.LineNumber;
				linePosition = xmlLineInfo.LinePosition;
				return;
			}
			lineNumber = (linePosition = -1);
		}

		/// <summary>Raises an <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event for the current position of the <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="id">A unique string that is used to identify the unreferenced object, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedId" /> property.</param>
		/// <param name="o">An object that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is attempting to deserialize, subsequently accessible through the <see cref="P:System.Xml.Serialization.UnreferencedObjectEventArgs.UnreferencedObject" /> property.</param>
		// Token: 0x06002127 RID: 8487 RVA: 0x000BB4D8 File Offset: 0x000B96D8
		protected void UnreferencedObject(string id, object o)
		{
			if (this.events.OnUnreferencedObject != null)
			{
				UnreferencedObjectEventArgs unreferencedObjectEventArgs = new UnreferencedObjectEventArgs(o, id);
				this.events.OnUnreferencedObject(this.events.sender, unreferencedObjectEventArgs);
			}
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000BB518 File Offset: 0x000B9718
		private string CurrentTag()
		{
			XmlNodeType nodeType = this.r.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				return string.Concat(new string[]
				{
					"<",
					this.r.LocalName,
					" xmlns='",
					this.r.NamespaceURI,
					"'>"
				});
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
				break;
			case XmlNodeType.Text:
				return this.r.Value;
			case XmlNodeType.CDATA:
				return "CDATA";
			case XmlNodeType.ProcessingInstruction:
				return "<?";
			case XmlNodeType.Comment:
				return "<--";
			default:
				if (nodeType == XmlNodeType.EndElement)
				{
					return ">";
				}
				break;
			}
			return "(unknown)";
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a type is unknown. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="type">An <see cref="T:System.Xml.XmlQualifiedName" /> that represents the name of the unknown type.</param>
		// Token: 0x06002129 RID: 8489 RVA: 0x000BB5C9 File Offset: 0x000B97C9
		protected Exception CreateUnknownTypeException(XmlQualifiedName type)
		{
			return new InvalidOperationException(Res.GetString("The specified type was not recognized: name='{0}', namespace='{1}', at {2}.", new object[]
			{
				type.Name,
				type.Namespace,
				this.CurrentTag()
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a SOAP-encoded collection type cannot be modified and its values cannot be filled in. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The fully qualified name of the .NET Framework type for which there is a mapping.</param>
		// Token: 0x0600212A RID: 8490 RVA: 0x000BB5FB File Offset: 0x000B97FB
		protected Exception CreateReadOnlyCollectionException(string name)
		{
			return new InvalidOperationException(Res.GetString("Could not deserialize {0}. Parameterless constructor is required for collections and enumerators.", new object[] { name }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized should be abstract. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The name of the abstract type.</param>
		/// <param name="ns">The .NET Framework namespace of the abstract type.</param>
		// Token: 0x0600212B RID: 8491 RVA: 0x000BB616 File Offset: 0x000B9816
		protected Exception CreateAbstractTypeException(string name, string ns)
		{
			return new InvalidOperationException(Res.GetString("The specified type is abstract: name='{0}', namespace='{1}', at {2}.", new object[]
			{
				name,
				ns,
				this.CurrentTag()
			}));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because there is no constructor available.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="typeName">The name of the type.</param>
		// Token: 0x0600212C RID: 8492 RVA: 0x000BB63E File Offset: 0x000B983E
		protected Exception CreateInaccessibleConstructorException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("{0} cannot be serialized because it does not have a parameterless constructor.", new object[] { typeName }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an object being deserialized cannot be instantiated because the constructor throws a security exception.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="typeName">The name of the type.</param>
		// Token: 0x0600212D RID: 8493 RVA: 0x000BB659 File Offset: 0x000B9859
		protected Exception CreateCtorHasSecurityException(string typeName)
		{
			return new InvalidOperationException(Res.GetString("The type '{0}' cannot be serialized because its parameterless constructor is decorated with declarative security permission attributes. Consider using imperative asserts or demands in the constructor.", new object[] { typeName }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that the current position of <see cref="T:System.Xml.XmlReader" /> represents an unknown XML node. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		// Token: 0x0600212E RID: 8494 RVA: 0x000BB674 File Offset: 0x000B9874
		protected Exception CreateUnknownNodeException()
		{
			return new InvalidOperationException(Res.GetString("{0} was not expected.", new object[] { this.CurrentTag() }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that an enumeration value is not valid. </summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="value">The enumeration value that is not valid.</param>
		/// <param name="enumType">The enumeration type.</param>
		// Token: 0x0600212F RID: 8495 RVA: 0x000BB694 File Offset: 0x000B9894
		protected Exception CreateUnknownConstantException(string value, Type enumType)
		{
			return new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[] { value, enumType.Name }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		// Token: 0x06002130 RID: 8496 RVA: 0x000BB6B8 File Offset: 0x000B98B8
		protected Exception CreateInvalidCastException(Type type, object value)
		{
			return this.CreateInvalidCastException(type, value, null);
		}

		/// <summary>Creates an <see cref="T:System.InvalidCastException" /> that indicates that an explicit reference conversion failed.</summary>
		/// <returns>An <see cref="T:System.InvalidCastException" /> exception.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> that an object cannot be cast to. This type is incorporated into the exception message.</param>
		/// <param name="value">The object that cannot be cast. This object is incorporated into the exception message.</param>
		/// <param name="id">A string identifier.</param>
		// Token: 0x06002131 RID: 8497 RVA: 0x000BB6C4 File Offset: 0x000B98C4
		protected Exception CreateInvalidCastException(Type type, object value, string id)
		{
			if (value == null)
			{
				return new InvalidCastException(Res.GetString("Cannot assign null value to an object of type {1}.", new object[] { type.FullName }));
			}
			if (id == null)
			{
				return new InvalidCastException(Res.GetString("Cannot assign object of type {0} to an object of type {1}.", new object[]
				{
					value.GetType().FullName,
					type.FullName
				}));
			}
			return new InvalidCastException(Res.GetString("Cannot assign object of type {0} to an object of type {1}. The error occurred while reading node with id='{2}'.", new object[]
			{
				value.GetType().FullName,
				type.FullName,
				id
			}));
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />, with an option to read the inner element.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="xsdDerived">The local name of the derived XML Schema data type.</param>
		/// <param name="nsDerived">The namespace of the derived XML Schema data type.</param>
		/// <param name="xsdBase">The local name of the base XML Schema data type.</param>
		/// <param name="nsBase">The namespace of the base XML Schema data type.</param>
		/// <param name="clrDerived">The namespace of the derived .NET Framework type.</param>
		/// <param name="clrBase">The name of the base .NET Framework type.</param>
		// Token: 0x06002132 RID: 8498 RVA: 0x000BB753 File Offset: 0x000B9953
		protected Exception CreateBadDerivationException(string xsdDerived, string nsDerived, string xsdBase, string nsBase, string clrDerived, string clrBase)
		{
			return new InvalidOperationException(Res.GetString("Type '{0}' from namespace '{1}' declared as derivation of type '{2}' from namespace '{3}, but corresponding CLR types are not compatible.  Cannot convert type '{4}' to '{5}'.", new object[] { xsdDerived, nsDerived, xsdBase, nsBase, clrDerived, clrBase }));
		}

		/// <summary>Creates an <see cref="T:System.InvalidOperationException" /> that indicates that a derived type that is mapped to an XML Schema data type cannot be located.</summary>
		/// <returns>An <see cref="T:System.InvalidOperationException" /> exception.</returns>
		/// <param name="name">The local name of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="ns">The namespace of the XML Schema data type that is mapped to the unavailable derived type.</param>
		/// <param name="clrType">The full name of the .NET Framework base type for which a derived type cannot be located.</param>
		// Token: 0x06002133 RID: 8499 RVA: 0x000BB785 File Offset: 0x000B9985
		protected Exception CreateMissingIXmlSerializableType(string name, string ns, string clrType)
		{
			return new InvalidOperationException(Res.GetString("Type '{0}' from namespace '{1}' does not have corresponding IXmlSerializable type. Please consider adding {2} to '{3}'.", new object[]
			{
				name,
				ns,
				typeof(XmlIncludeAttribute).Name,
				clrType
			}));
		}

		/// <summary>Ensures that a given array, or a copy, is large enough to contain a specified index. </summary>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already large enough; otherwise, a new, larger array that contains the original array's elements.</returns>
		/// <param name="a">The <see cref="T:System.Array" /> that is being checked.</param>
		/// <param name="index">The required index.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		// Token: 0x06002134 RID: 8500 RVA: 0x000BB7BC File Offset: 0x000B99BC
		protected Array EnsureArrayIndex(Array a, int index, Type elementType)
		{
			if (a == null)
			{
				return Array.CreateInstance(elementType, 32);
			}
			if (index < a.Length)
			{
				return a;
			}
			Array array = Array.CreateInstance(elementType, a.Length * 2);
			Array.Copy(a, array, index);
			return array;
		}

		/// <summary>Ensures that a given array, or a copy, is no larger than a specified length. </summary>
		/// <returns>The existing <see cref="T:System.Array" />, if it is already small enough; otherwise, a new, smaller array that contains the original array's elements up to the size of<paramref name=" length" />.</returns>
		/// <param name="a">The array that is being checked.</param>
		/// <param name="length">The maximum length of the array.</param>
		/// <param name="elementType">The <see cref="T:System.Type" /> of the array's elements.</param>
		/// <param name="isNullable">true if null for the array, if present for the input array, can be returned; otherwise, a new, smaller array.</param>
		// Token: 0x06002135 RID: 8501 RVA: 0x000BB7F8 File Offset: 0x000B99F8
		protected Array ShrinkArray(Array a, int length, Type elementType, bool isNullable)
		{
			if (a == null)
			{
				if (isNullable)
				{
					return null;
				}
				return Array.CreateInstance(elementType, 0);
			}
			else
			{
				if (a.Length == length)
				{
					return a;
				}
				Array array = Array.CreateInstance(elementType, length);
				Array.Copy(a, array, length);
				return array;
			}
		}

		/// <summary>Produces the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value. </summary>
		/// <returns>The result of call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method appended to the input value.</returns>
		/// <param name="value">A string to prefix to the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method.</param>
		// Token: 0x06002136 RID: 8502 RVA: 0x000BB832 File Offset: 0x000B9A32
		protected string ReadString(string value)
		{
			return this.ReadString(value, false);
		}

		/// <summary>Returns the result of a call to the <see cref="M:System.Xml.XmlReader.ReadString" /> method of the <see cref="T:System.Xml.XmlReader" /> class, trimmed of white space if needed, and appended to the input value.</summary>
		/// <returns>The result of the read operation appended to the input value.</returns>
		/// <param name="value">A string that will be appended to.</param>
		/// <param name="trim">true if the result of the read operation should be trimmed; otherwise, false.</param>
		// Token: 0x06002137 RID: 8503 RVA: 0x000BB83C File Offset: 0x000B9A3C
		protected string ReadString(string value, bool trim)
		{
			string text = this.r.ReadString();
			if (text != null && trim)
			{
				text = text.Trim();
			}
			if (value == null || value.Length == 0)
			{
				return text;
			}
			return value + text;
		}

		/// <summary>Populates an object from its XML representation at the current location of the <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>An object that implements the <see cref="T:System.Xml.Serialization.IXmlSerializable" /> interface with its members populated from the location of the <see cref="T:System.Xml.XmlReader" />.</returns>
		/// <param name="serializable">An <see cref="T:System.Xml.Serialization.IXmlSerializable" /> that corresponds to the current position of the <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x06002138 RID: 8504 RVA: 0x000BB878 File Offset: 0x000B9A78
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable)
		{
			return this.ReadSerializable(serializable, false);
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An object that implements the IXmlSerializable interface with its members populated from the location of the XmlReader.</returns>
		/// <param name="serializable">An IXmlSerializable object that corresponds to the current position of the XMLReader.</param>
		/// <param name="wrappedAny">Specifies whether the serializable object is wrapped.</param>
		// Token: 0x06002139 RID: 8505 RVA: 0x000BB884 File Offset: 0x000B9A84
		protected IXmlSerializable ReadSerializable(IXmlSerializable serializable, bool wrappedAny)
		{
			string text = null;
			string text2 = null;
			if (wrappedAny)
			{
				text = this.r.LocalName;
				text2 = this.r.NamespaceURI;
				this.r.Read();
				this.r.MoveToContent();
			}
			serializable.ReadXml(this.r);
			if (wrappedAny)
			{
				while (this.r.NodeType == XmlNodeType.Whitespace)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.None)
				{
					this.r.Skip();
				}
				if (this.r.NodeType == XmlNodeType.EndElement && this.r.LocalName == text && this.r.NamespaceURI == text2)
				{
					this.Reader.Read();
				}
			}
			return serializable;
		}

		/// <summary>Reads the value of the href attribute (ref attribute for SOAP 1.2) that is used to refer to an XML element in SOAP encoding. </summary>
		/// <returns>true if the value was read; otherwise, false.</returns>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x0600213A RID: 8506 RVA: 0x000BB950 File Offset: 0x000B9B50
		protected bool ReadReference(out string fixupReference)
		{
			string text = (this.soap12 ? this.r.GetAttribute("ref", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("href"));
			if (text == null)
			{
				fixupReference = null;
				return false;
			}
			if (!this.soap12)
			{
				if (!text.StartsWith("#", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("The referenced element with ID '{0}' is located outside the current document and cannot be retrieved.", new object[] { text }));
				}
				fixupReference = text.Substring(1);
			}
			else
			{
				fixupReference = text;
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
			}
			else
			{
				this.r.ReadStartElement();
				this.ReadEndElement();
			}
			return true;
		}

		/// <summary>Stores an object that is being deserialized from a SOAP-encoded multiRef element for later access through the <see cref="M:System.Xml.Serialization.XmlSerializationReader.GetTarget(System.String)" /> method. </summary>
		/// <param name="id">The value of the id attribute of a multiRef element that identifies the element.</param>
		/// <param name="o">The object that is deserialized from the XML element.</param>
		// Token: 0x0600213B RID: 8507 RVA: 0x000BBA00 File Offset: 0x000B9C00
		protected void AddTarget(string id, object o)
		{
			if (id == null)
			{
				if (this.targetsWithoutIds == null)
				{
					this.targetsWithoutIds = new ArrayList();
				}
				if (o != null)
				{
					this.targetsWithoutIds.Add(o);
					return;
				}
			}
			else
			{
				if (this.targets == null)
				{
					this.targets = new Hashtable();
				}
				if (!this.targets.Contains(id))
				{
					this.targets.Add(id, o);
				}
			}
		}

		/// <summary>Stores an object that contains a callback method instance that will be called, as necessary, to fill in the objects in a SOAP-encoded array. </summary>
		/// <param name="fixup">An <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x0600213C RID: 8508 RVA: 0x000BBA62 File Offset: 0x000B9C62
		protected void AddFixup(XmlSerializationReader.Fixup fixup)
		{
			if (this.fixups == null)
			{
				this.fixups = new ArrayList();
			}
			this.fixups.Add(fixup);
		}

		/// <summary>Stores an object that contains a callback method that will be called, as necessary, to fill in .NET Framework collections or enumerations that map to SOAP-encoded arrays or SOAP-encoded, multi-referenced elements. </summary>
		/// <param name="fixup">A <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate and the callback method's input data.</param>
		// Token: 0x0600213D RID: 8509 RVA: 0x000BBA84 File Offset: 0x000B9C84
		protected void AddFixup(XmlSerializationReader.CollectionFixup fixup)
		{
			if (this.collectionFixups == null)
			{
				this.collectionFixups = new ArrayList();
			}
			this.collectionFixups.Add(fixup);
		}

		/// <summary>Gets an object that is being deserialized from a SOAP-encoded multiRef element and that was stored earlier by <see cref="M:System.Xml.Serialization.XmlSerializationReader.AddTarget(System.String,System.Object)" />.  </summary>
		/// <returns>An object to be deserialized from a SOAP-encoded multiRef element.</returns>
		/// <param name="id">The value of the id attribute of a multiRef element that identifies the element.</param>
		// Token: 0x0600213E RID: 8510 RVA: 0x000BBAA8 File Offset: 0x000B9CA8
		protected object GetTarget(string id)
		{
			object obj = ((this.targets != null) ? this.targets[id] : null);
			if (obj == null)
			{
				throw new InvalidOperationException(Res.GetString("The referenced element with ID '{0}' was not found in the document.", new object[] { id }));
			}
			this.Referenced(obj);
			return obj;
		}

		/// <summary>Stores an object to be deserialized from a SOAP-encoded multiRef element.</summary>
		/// <param name="o">The object to be deserialized.</param>
		// Token: 0x0600213F RID: 8511 RVA: 0x000BBAF2 File Offset: 0x000B9CF2
		protected void Referenced(object o)
		{
			if (o == null)
			{
				return;
			}
			if (this.referencedTargets == null)
			{
				this.referencedTargets = new Hashtable();
			}
			this.referencedTargets[o] = o;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000BBB18 File Offset: 0x000B9D18
		private void HandleUnreferencedObjects()
		{
			if (this.targets != null)
			{
				foreach (object obj in this.targets)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (this.referencedTargets == null || !this.referencedTargets.Contains(dictionaryEntry.Value))
					{
						this.UnreferencedObject((string)dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
			}
			if (this.targetsWithoutIds != null)
			{
				foreach (object obj2 in this.targetsWithoutIds)
				{
					if (this.referencedTargets == null || !this.referencedTargets.Contains(obj2))
					{
						this.UnreferencedObject(null, obj2);
					}
				}
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000BBC0C File Offset: 0x000B9E0C
		private void DoFixups()
		{
			if (this.fixups == null)
			{
				return;
			}
			for (int i = 0; i < this.fixups.Count; i++)
			{
				XmlSerializationReader.Fixup fixup = (XmlSerializationReader.Fixup)this.fixups[i];
				fixup.Callback(fixup);
			}
			if (this.collectionFixups == null)
			{
				return;
			}
			for (int j = 0; j < this.collectionFixups.Count; j++)
			{
				XmlSerializationReader.CollectionFixup collectionFixup = (XmlSerializationReader.CollectionFixup)this.collectionFixups[j];
				collectionFixup.Callback(collectionFixup.Collection, collectionFixup.CollectionItems);
			}
		}

		/// <summary>Fills in the values of a SOAP-encoded array whose data type maps to a .NET Framework reference type.</summary>
		/// <param name="fixup">An object that contains the array whose values are filled in.</param>
		// Token: 0x06002142 RID: 8514 RVA: 0x000BBCA0 File Offset: 0x000B9EA0
		protected void FixupArrayRefs(object fixup)
		{
			XmlSerializationReader.Fixup fixup2 = (XmlSerializationReader.Fixup)fixup;
			Array array = (Array)fixup2.Source;
			for (int i = 0; i < array.Length; i++)
			{
				string text = fixup2.Ids[i];
				if (text != null)
				{
					object target = this.GetTarget(text);
					try
					{
						array.SetValue(target, i);
					}
					catch (InvalidCastException)
					{
						throw new InvalidOperationException(Res.GetString("Invalid reference id='{0}'. Object of type {1} cannot be stored in an array of this type. Details: array index={2}.", new object[]
						{
							text,
							target.GetType().FullName,
							i.ToString(CultureInfo.InvariantCulture)
						}));
					}
				}
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000BBD3C File Offset: 0x000B9F3C
		private object ReadArray(string typeName, string typeNs)
		{
			Type type = null;
			XmlSerializationReader.SoapArrayInfo soapArrayInfo;
			if (this.soap12)
			{
				string attribute = this.r.GetAttribute(this.itemTypeID, this.soap12NsID);
				string attribute2 = this.r.GetAttribute(this.arraySizeID, this.soap12NsID);
				Type type2 = (Type)this.types[new XmlQualifiedName(typeName, typeNs)];
				if (attribute == null && attribute2 == null && (type2 == null || !type2.IsArray))
				{
					return null;
				}
				soapArrayInfo = this.ParseSoap12ArrayType(attribute, attribute2);
				if (type2 != null)
				{
					type = TypeScope.GetArrayElementType(type2, null);
				}
			}
			else
			{
				string attribute3 = this.r.GetAttribute(this.arrayTypeID, this.soapNsID);
				if (attribute3 == null)
				{
					return null;
				}
				soapArrayInfo = this.ParseArrayType(attribute3);
			}
			if (soapArrayInfo.dimensions != 1)
			{
				throw new InvalidOperationException(Res.GetString("SOAP-ENC:arrayType with multidimensional array found at {0}. Only single-dimensional arrays are supported. Consider using an array of arrays instead.", new object[] { this.CurrentTag() }));
			}
			Type type3 = null;
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.schemaNsID);
			XmlQualifiedName xmlQualifiedName2;
			if (soapArrayInfo.qname.Length > 0)
			{
				xmlQualifiedName2 = this.ToXmlQualifiedName(soapArrayInfo.qname, false);
				type3 = (Type)this.types[xmlQualifiedName2];
			}
			else
			{
				xmlQualifiedName2 = xmlQualifiedName;
			}
			if (this.soap12 && type3 == typeof(object))
			{
				type3 = null;
			}
			bool flag;
			if (type3 == null)
			{
				if (!this.soap12)
				{
					type3 = this.GetPrimitiveType(xmlQualifiedName2, true);
					flag = true;
				}
				else
				{
					if (xmlQualifiedName2 != xmlQualifiedName)
					{
						type3 = this.GetPrimitiveType(xmlQualifiedName2, false);
					}
					if (type3 != null)
					{
						flag = true;
					}
					else if (type == null)
					{
						type3 = typeof(object);
						flag = false;
					}
					else
					{
						type3 = type;
						XmlQualifiedName xmlQualifiedName3 = (XmlQualifiedName)this.typesReverse[type3];
						if (xmlQualifiedName3 == null)
						{
							xmlQualifiedName3 = XmlSerializationWriter.GetPrimitiveTypeNameInternal(type3);
							flag = true;
						}
						else
						{
							flag = type3.IsPrimitive;
						}
						if (xmlQualifiedName3 != null)
						{
							xmlQualifiedName2 = xmlQualifiedName3;
						}
					}
				}
			}
			else
			{
				flag = type3.IsPrimitive;
			}
			if (!this.soap12 && soapArrayInfo.jaggedDimensions > 0)
			{
				for (int i = 0; i < soapArrayInfo.jaggedDimensions; i++)
				{
					type3 = type3.MakeArrayType();
				}
			}
			if (this.r.IsEmptyElement)
			{
				this.r.Skip();
				return Array.CreateInstance(type3, 0);
			}
			this.r.ReadStartElement();
			this.r.MoveToContent();
			int num = 0;
			Array array = null;
			if (type3.IsValueType)
			{
				if (!flag && !type3.IsEnum)
				{
					throw new NotSupportedException(Res.GetString("Cannot serialize {0}. Arrays of structs are not supported with encoded SOAP.", new object[] { type3.FullName }));
				}
				int num2 = 0;
				int readerCount = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array.SetValue(this.ReadReferencedElement(xmlQualifiedName2.Name, xmlQualifiedName2.Namespace), num);
					num++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num2, ref readerCount);
				}
				array = this.ShrinkArray(array, num, type3, false);
			}
			else
			{
				string[] array2 = null;
				int num3 = 0;
				int num4 = 0;
				int readerCount2 = this.ReaderCount;
				while (this.r.NodeType != XmlNodeType.EndElement)
				{
					array = this.EnsureArrayIndex(array, num, type3);
					array2 = (string[])this.EnsureArrayIndex(array2, num3, typeof(string));
					string text;
					string text2;
					if (this.r.NamespaceURI.Length != 0)
					{
						text = this.r.LocalName;
						if (this.r.NamespaceURI == this.soapNsID)
						{
							text2 = "http://www.w3.org/2001/XMLSchema";
						}
						else
						{
							text2 = this.r.NamespaceURI;
						}
					}
					else
					{
						text = xmlQualifiedName2.Name;
						text2 = xmlQualifiedName2.Namespace;
					}
					array.SetValue(this.ReadReferencingElement(text, text2, out array2[num3]), num);
					num++;
					num3++;
					this.r.MoveToContent();
					this.CheckReaderCount(ref num4, ref readerCount2);
				}
				if (this.soap12 && type3 == typeof(object))
				{
					Type type4 = null;
					for (int j = 0; j < num; j++)
					{
						object value = array.GetValue(j);
						if (value != null)
						{
							Type type5 = value.GetType();
							if (type5.IsValueType)
							{
								type4 = null;
								break;
							}
							if (type4 == null || type5.IsAssignableFrom(type4))
							{
								type4 = type5;
							}
							else if (!type4.IsAssignableFrom(type5))
							{
								type4 = null;
								break;
							}
						}
					}
					if (type4 != null)
					{
						type3 = type4;
					}
				}
				array2 = (string[])this.ShrinkArray(array2, num3, typeof(string), false);
				array = this.ShrinkArray(array, num, type3, false);
				XmlSerializationReader.Fixup fixup = new XmlSerializationReader.Fixup(array, new XmlSerializationFixupCallback(this.FixupArrayRefs), array2);
				this.AddFixup(fixup);
			}
			this.ReadEndElement();
			return array;
		}

		/// <summary>Initializes callback methods that populate objects that map to SOAP-encoded XML data. </summary>
		// Token: 0x06002144 RID: 8516
		protected abstract void InitCallbacks();

		/// <summary>Deserializes objects from the SOAP-encoded multiRef elements in a SOAP message. </summary>
		// Token: 0x06002145 RID: 8517 RVA: 0x000BC22C File Offset: 0x000BA42C
		protected void ReadReferencedElements()
		{
			this.r.MoveToContent();
			int num = 0;
			int readerCount = this.ReaderCount;
			while (this.r.NodeType != XmlNodeType.EndElement && this.r.NodeType != XmlNodeType.None)
			{
				string text;
				this.ReadReferencingElement(null, null, true, out text);
				this.r.MoveToContent();
				this.CheckReaderCount(ref num, ref readerCount);
			}
			this.DoFixups();
			this.HandleUnreferencedObjects();
		}

		/// <summary>Deserializes an object from a SOAP-encoded multiRef XML element. </summary>
		/// <returns>The value of the referenced element in the document.</returns>
		// Token: 0x06002146 RID: 8518 RVA: 0x000BC29A File Offset: 0x000BA49A
		protected object ReadReferencedElement()
		{
			return this.ReadReferencedElement(null, null);
		}

		/// <summary>Deserializes an object from a SOAP-encoded multiRef XML element. </summary>
		/// <returns>The value of the referenced element in the document.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		// Token: 0x06002147 RID: 8519 RVA: 0x000BC2A4 File Offset: 0x000BA4A4
		protected object ReadReferencedElement(string name, string ns)
		{
			string text;
			return this.ReadReferencingElement(name, ns, out text);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element. </summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x06002148 RID: 8520 RVA: 0x000BC2BB File Offset: 0x000BA4BB
		protected object ReadReferencingElement(out string fixupReference)
		{
			return this.ReadReferencingElement(null, null, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element. </summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="fixupReference">An output string into which the href attribute value is read.</param>
		// Token: 0x06002149 RID: 8521 RVA: 0x000BC2C6 File Offset: 0x000BA4C6
		protected object ReadReferencingElement(string name, string ns, out string fixupReference)
		{
			return this.ReadReferencingElement(name, ns, false, out fixupReference);
		}

		/// <summary>Deserializes an object from an XML element in a SOAP message that contains a reference to a multiRef element.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="name">The local name of the element's XML Schema data type.</param>
		/// <param name="ns">The namespace of the element's XML Schema data type.</param>
		/// <param name="elementCanBeType">true if the element name is also the XML Schema data type name; otherwise, false.</param>
		/// <param name="fixupReference">An output string into which the value of the href attribute is read.</param>
		// Token: 0x0600214A RID: 8522 RVA: 0x000BC2D4 File Offset: 0x000BA4D4
		protected object ReadReferencingElement(string name, string ns, bool elementCanBeType, out string fixupReference)
		{
			if (this.callbacks == null)
			{
				this.callbacks = new Hashtable();
				this.types = new Hashtable();
				XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.urTypeID, this.r.NameTable.Add("http://www.w3.org/2001/XMLSchema"));
				this.types.Add(xmlQualifiedName, typeof(object));
				this.typesReverse = new Hashtable();
				this.typesReverse.Add(typeof(object), xmlQualifiedName);
				this.InitCallbacks();
			}
			this.r.MoveToContent();
			if (this.ReadReference(out fixupReference))
			{
				return null;
			}
			if (this.ReadNull())
			{
				return null;
			}
			string text = (this.soap12 ? this.r.GetAttribute("id", "http://www.w3.org/2003/05/soap-encoding") : this.r.GetAttribute("id", null));
			object obj;
			if ((obj = this.ReadArray(name, ns)) == null)
			{
				XmlQualifiedName xmlQualifiedName2 = this.GetXsiType();
				if (xmlQualifiedName2 == null)
				{
					if (name == null)
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(this.r.LocalName), this.r.NameTable.Add(this.r.NamespaceURI));
					}
					else
					{
						xmlQualifiedName2 = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
					}
				}
				XmlSerializationReadCallback xmlSerializationReadCallback = (XmlSerializationReadCallback)this.callbacks[xmlQualifiedName2];
				if (xmlSerializationReadCallback != null)
				{
					obj = xmlSerializationReadCallback();
				}
				else
				{
					obj = this.ReadTypedPrimitive(xmlQualifiedName2, elementCanBeType);
				}
			}
			this.AddTarget(text, obj);
			return obj;
		}

		/// <summary>Stores an implementation of the <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate and its input data for a later invocation. </summary>
		/// <param name="name">The name of the .NET Framework type that is being deserialized.</param>
		/// <param name="ns">The namespace of the .NET Framework type that is being deserialized.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to be deserialized.</param>
		/// <param name="read">An <see cref="T:System.Xml.Serialization.XmlSerializationReadCallback" /> delegate.</param>
		// Token: 0x0600214B RID: 8523 RVA: 0x000BC470 File Offset: 0x000BA670
		protected void AddReadCallback(string name, string ns, Type type, XmlSerializationReadCallback read)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(this.r.NameTable.Add(name), this.r.NameTable.Add(ns));
			this.callbacks[xmlQualifiedName] = read;
			this.types[xmlQualifiedName] = type;
			this.typesReverse[type] = xmlQualifiedName;
		}

		/// <summary>Makes the <see cref="T:System.Xml.XmlReader" /> read an XML end tag.</summary>
		// Token: 0x0600214C RID: 8524 RVA: 0x000BC4D0 File Offset: 0x000BA6D0
		protected void ReadEndElement()
		{
			while (this.r.NodeType == XmlNodeType.Whitespace)
			{
				this.r.Skip();
			}
			if (this.r.NodeType == XmlNodeType.None)
			{
				this.r.Skip();
				return;
			}
			this.r.ReadEndElement();
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000BC520 File Offset: 0x000BA720
		private object ReadXmlNodes(bool elementCanBeType)
		{
			ArrayList arrayList = new ArrayList();
			string localName = this.Reader.LocalName;
			string namespaceURI = this.Reader.NamespaceURI;
			string name = this.Reader.Name;
			string text = null;
			string text2 = null;
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			XmlNode xmlNode;
			if (this.Reader.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = this.Document.CreateAttribute(name, namespaceURI);
				xmlAttribute.Value = this.Reader.Value;
				xmlNode = xmlAttribute;
			}
			else
			{
				xmlNode = this.Document.CreateElement(name, namespaceURI);
			}
			this.GetCurrentPosition(out num2, out num3);
			XmlElement xmlElement = xmlNode as XmlElement;
			while (this.Reader.MoveToNextAttribute())
			{
				if (this.IsXmlnsAttribute(this.Reader.Name) || (this.Reader.Name == "id" && (!this.soap12 || this.Reader.NamespaceURI == "http://www.w3.org/2003/05/soap-encoding")))
				{
					num++;
				}
				if (this.Reader.LocalName == this.typeID && (this.Reader.NamespaceURI == this.instanceNsID || this.Reader.NamespaceURI == this.instanceNs2000ID || this.Reader.NamespaceURI == this.instanceNs1999ID))
				{
					string value = this.Reader.Value;
					int num4 = value.LastIndexOf(':');
					text = ((num4 >= 0) ? value.Substring(num4 + 1) : value);
					text2 = this.Reader.LookupNamespace((num4 >= 0) ? value.Substring(0, num4) : "");
				}
				XmlAttribute xmlAttribute2 = (XmlAttribute)this.Document.ReadNode(this.r);
				arrayList.Add(xmlAttribute2);
				if (xmlElement != null)
				{
					xmlElement.SetAttributeNode(xmlAttribute2);
				}
			}
			if (elementCanBeType && text == null)
			{
				text = localName;
				text2 = namespaceURI;
				XmlAttribute xmlAttribute3 = this.Document.CreateAttribute(this.typeID, this.instanceNsID);
				xmlAttribute3.Value = name;
				arrayList.Add(xmlAttribute3);
			}
			if (text == "anyType" && (text2 == this.schemaNsID || text2 == this.schemaNs1999ID || text2 == this.schemaNs2000ID))
			{
				num++;
			}
			this.Reader.MoveToElement();
			if (this.Reader.IsEmptyElement)
			{
				this.Reader.Skip();
			}
			else
			{
				this.Reader.ReadStartElement();
				this.Reader.MoveToContent();
				int num5 = 0;
				int readerCount = this.ReaderCount;
				while (this.Reader.NodeType != XmlNodeType.EndElement)
				{
					XmlNode xmlNode2 = this.Document.ReadNode(this.r);
					arrayList.Add(xmlNode2);
					if (xmlElement != null)
					{
						xmlElement.AppendChild(xmlNode2);
					}
					this.Reader.MoveToContent();
					this.CheckReaderCount(ref num5, ref readerCount);
				}
				this.ReadEndElement();
			}
			if (arrayList.Count <= num)
			{
				return new object();
			}
			object obj = (XmlNode[])arrayList.ToArray(typeof(XmlNode));
			this.UnknownNode(xmlNode, null, null);
			return obj;
		}

		/// <summary>Checks whether the deserializer has advanced.</summary>
		/// <param name="whileIterations">The current count in a while loop.</param>
		/// <param name="readerCount">The current <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.Serialization.XmlSerializationReader.ReaderCount" /> has not advanced. </exception>
		// Token: 0x0600214E RID: 8526 RVA: 0x000BC825 File Offset: 0x000BAA25
		protected void CheckReaderCount(ref int whileIterations, ref int readerCount)
		{
			if (XmlSerializationReader.checkDeserializeAdvances)
			{
				whileIterations++;
				if ((whileIterations & 128) == 128)
				{
					if (readerCount == this.ReaderCount)
					{
						throw new InvalidOperationException(Res.GetString("Internal error: deserialization failed to advance over underlying stream."));
					}
					readerCount = this.ReaderCount;
				}
			}
		}

		// Token: 0x040017AE RID: 6062
		private XmlReader r;

		// Token: 0x040017AF RID: 6063
		private XmlCountingReader countingReader;

		// Token: 0x040017B0 RID: 6064
		private XmlDocument d;

		// Token: 0x040017B1 RID: 6065
		private Hashtable callbacks;

		// Token: 0x040017B2 RID: 6066
		private Hashtable types;

		// Token: 0x040017B3 RID: 6067
		private Hashtable typesReverse;

		// Token: 0x040017B4 RID: 6068
		private XmlDeserializationEvents events;

		// Token: 0x040017B5 RID: 6069
		private Hashtable targets;

		// Token: 0x040017B6 RID: 6070
		private Hashtable referencedTargets;

		// Token: 0x040017B7 RID: 6071
		private ArrayList targetsWithoutIds;

		// Token: 0x040017B8 RID: 6072
		private ArrayList fixups;

		// Token: 0x040017B9 RID: 6073
		private ArrayList collectionFixups;

		// Token: 0x040017BA RID: 6074
		private bool soap12;

		// Token: 0x040017BB RID: 6075
		private bool isReturnValue;

		// Token: 0x040017BC RID: 6076
		private bool decodeName = true;

		// Token: 0x040017BD RID: 6077
		private string schemaNsID;

		// Token: 0x040017BE RID: 6078
		private string schemaNs1999ID;

		// Token: 0x040017BF RID: 6079
		private string schemaNs2000ID;

		// Token: 0x040017C0 RID: 6080
		private string schemaNonXsdTypesNsID;

		// Token: 0x040017C1 RID: 6081
		private string instanceNsID;

		// Token: 0x040017C2 RID: 6082
		private string instanceNs2000ID;

		// Token: 0x040017C3 RID: 6083
		private string instanceNs1999ID;

		// Token: 0x040017C4 RID: 6084
		private string soapNsID;

		// Token: 0x040017C5 RID: 6085
		private string soap12NsID;

		// Token: 0x040017C6 RID: 6086
		private string schemaID;

		// Token: 0x040017C7 RID: 6087
		private string wsdlNsID;

		// Token: 0x040017C8 RID: 6088
		private string wsdlArrayTypeID;

		// Token: 0x040017C9 RID: 6089
		private string nullID;

		// Token: 0x040017CA RID: 6090
		private string nilID;

		// Token: 0x040017CB RID: 6091
		private string typeID;

		// Token: 0x040017CC RID: 6092
		private string arrayTypeID;

		// Token: 0x040017CD RID: 6093
		private string itemTypeID;

		// Token: 0x040017CE RID: 6094
		private string arraySizeID;

		// Token: 0x040017CF RID: 6095
		private string arrayID;

		// Token: 0x040017D0 RID: 6096
		private string urTypeID;

		// Token: 0x040017D1 RID: 6097
		private string stringID;

		// Token: 0x040017D2 RID: 6098
		private string intID;

		// Token: 0x040017D3 RID: 6099
		private string booleanID;

		// Token: 0x040017D4 RID: 6100
		private string shortID;

		// Token: 0x040017D5 RID: 6101
		private string longID;

		// Token: 0x040017D6 RID: 6102
		private string floatID;

		// Token: 0x040017D7 RID: 6103
		private string doubleID;

		// Token: 0x040017D8 RID: 6104
		private string decimalID;

		// Token: 0x040017D9 RID: 6105
		private string dateTimeID;

		// Token: 0x040017DA RID: 6106
		private string qnameID;

		// Token: 0x040017DB RID: 6107
		private string dateID;

		// Token: 0x040017DC RID: 6108
		private string timeID;

		// Token: 0x040017DD RID: 6109
		private string hexBinaryID;

		// Token: 0x040017DE RID: 6110
		private string base64BinaryID;

		// Token: 0x040017DF RID: 6111
		private string base64ID;

		// Token: 0x040017E0 RID: 6112
		private string unsignedByteID;

		// Token: 0x040017E1 RID: 6113
		private string byteID;

		// Token: 0x040017E2 RID: 6114
		private string unsignedShortID;

		// Token: 0x040017E3 RID: 6115
		private string unsignedIntID;

		// Token: 0x040017E4 RID: 6116
		private string unsignedLongID;

		// Token: 0x040017E5 RID: 6117
		private string oldDecimalID;

		// Token: 0x040017E6 RID: 6118
		private string oldTimeInstantID;

		// Token: 0x040017E7 RID: 6119
		private string anyURIID;

		// Token: 0x040017E8 RID: 6120
		private string durationID;

		// Token: 0x040017E9 RID: 6121
		private string ENTITYID;

		// Token: 0x040017EA RID: 6122
		private string ENTITIESID;

		// Token: 0x040017EB RID: 6123
		private string gDayID;

		// Token: 0x040017EC RID: 6124
		private string gMonthID;

		// Token: 0x040017ED RID: 6125
		private string gMonthDayID;

		// Token: 0x040017EE RID: 6126
		private string gYearID;

		// Token: 0x040017EF RID: 6127
		private string gYearMonthID;

		// Token: 0x040017F0 RID: 6128
		private string IDID;

		// Token: 0x040017F1 RID: 6129
		private string IDREFID;

		// Token: 0x040017F2 RID: 6130
		private string IDREFSID;

		// Token: 0x040017F3 RID: 6131
		private string integerID;

		// Token: 0x040017F4 RID: 6132
		private string languageID;

		// Token: 0x040017F5 RID: 6133
		private string NameID;

		// Token: 0x040017F6 RID: 6134
		private string NCNameID;

		// Token: 0x040017F7 RID: 6135
		private string NMTOKENID;

		// Token: 0x040017F8 RID: 6136
		private string NMTOKENSID;

		// Token: 0x040017F9 RID: 6137
		private string negativeIntegerID;

		// Token: 0x040017FA RID: 6138
		private string nonPositiveIntegerID;

		// Token: 0x040017FB RID: 6139
		private string nonNegativeIntegerID;

		// Token: 0x040017FC RID: 6140
		private string normalizedStringID;

		// Token: 0x040017FD RID: 6141
		private string NOTATIONID;

		// Token: 0x040017FE RID: 6142
		private string positiveIntegerID;

		// Token: 0x040017FF RID: 6143
		private string tokenID;

		// Token: 0x04001800 RID: 6144
		private string charID;

		// Token: 0x04001801 RID: 6145
		private string guidID;

		// Token: 0x04001802 RID: 6146
		private string timeSpanID;

		// Token: 0x04001803 RID: 6147
		private static bool checkDeserializeAdvances;

		// Token: 0x0200034C RID: 844
		private struct SoapArrayInfo
		{
			// Token: 0x04001804 RID: 6148
			public string qname;

			// Token: 0x04001805 RID: 6149
			public int dimensions;

			// Token: 0x04001806 RID: 6150
			public int length;

			// Token: 0x04001807 RID: 6151
			public int jaggedDimensions;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate instance, plus the method's inputs; also serves as the parameter for the method.</summary>
		// Token: 0x0200034D RID: 845
		protected class Fixup
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class.</summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="count">The size of the string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06002150 RID: 8528 RVA: 0x000BC874 File Offset: 0x000BAA74
			public Fixup(object o, XmlSerializationFixupCallback callback, int count)
				: this(o, callback, new string[count])
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.Fixup" /> class.</summary>
			/// <param name="o">The object that contains other objects whose values get filled in by the callback implementation.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</param>
			/// <param name="ids">The string array obtained through the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Ids" /> property.</param>
			// Token: 0x06002151 RID: 8529 RVA: 0x000BC884 File Offset: 0x000BAA84
			public Fixup(object o, XmlSerializationFixupCallback callback, string[] ids)
			{
				this.callback = callback;
				this.Source = o;
				this.ids = ids;
			}

			/// <summary>Gets the callback method that creates an instance of the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</summary>
			/// <returns>The callback method that creates an instance of the <see cref="T:System.Xml.Serialization.XmlSerializationFixupCallback" /> delegate.</returns>
			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x06002152 RID: 8530 RVA: 0x000BC8A1 File Offset: 0x000BAAA1
			public XmlSerializationFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets or sets the object that contains other objects whose values get filled in by the callback implementation.</summary>
			/// <returns>The source containing objects with values to fill.</returns>
			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x06002153 RID: 8531 RVA: 0x000BC8A9 File Offset: 0x000BAAA9
			// (set) Token: 0x06002154 RID: 8532 RVA: 0x000BC8B1 File Offset: 0x000BAAB1
			public object Source
			{
				get
				{
					return this.source;
				}
				set
				{
					this.source = value;
				}
			}

			/// <summary>Gets or sets an array of keys for the objects that belong to the <see cref="P:System.Xml.Serialization.XmlSerializationReader.Fixup.Source" /> property whose values get filled in by the callback implementation.</summary>
			/// <returns>The array of keys.</returns>
			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06002155 RID: 8533 RVA: 0x000BC8BA File Offset: 0x000BAABA
			public string[] Ids
			{
				get
				{
					return this.ids;
				}
			}

			// Token: 0x04001808 RID: 6152
			private XmlSerializationFixupCallback callback;

			// Token: 0x04001809 RID: 6153
			private object source;

			// Token: 0x0400180A RID: 6154
			private string[] ids;
		}

		/// <summary>Holds an <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate instance, plus the method's inputs; also supplies the method's parameters. </summary>
		// Token: 0x0200034E RID: 846
		protected class CollectionFixup
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializationReader.CollectionFixup" /> class with parameters for a callback method. </summary>
			/// <param name="collection">A collection into which the callback method copies the collection items array.</param>
			/// <param name="callback">A method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate.</param>
			/// <param name="collectionItems">An array into which the callback method copies a collection.</param>
			// Token: 0x06002156 RID: 8534 RVA: 0x000BC8C2 File Offset: 0x000BAAC2
			public CollectionFixup(object collection, XmlSerializationCollectionFixupCallback callback, object collectionItems)
			{
				this.callback = callback;
				this.collection = collection;
				this.collectionItems = collectionItems;
			}

			/// <summary>Gets the callback method that instantiates the <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate. </summary>
			/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializationCollectionFixupCallback" /> delegate that points to the callback method.</returns>
			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06002157 RID: 8535 RVA: 0x000BC8DF File Offset: 0x000BAADF
			public XmlSerializationCollectionFixupCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			/// <summary>Gets the <paramref name="object collection" /> for the callback method. </summary>
			/// <returns>The collection that is used for the fixup.</returns>
			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06002158 RID: 8536 RVA: 0x000BC8E7 File Offset: 0x000BAAE7
			public object Collection
			{
				get
				{
					return this.collection;
				}
			}

			/// <summary>Gets the array into which the callback method copies a collection. </summary>
			/// <returns>The array into which the callback method copies a collection.</returns>
			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06002159 RID: 8537 RVA: 0x000BC8EF File Offset: 0x000BAAEF
			public object CollectionItems
			{
				get
				{
					return this.collectionItems;
				}
			}

			// Token: 0x0400180B RID: 6155
			private XmlSerializationCollectionFixupCallback callback;

			// Token: 0x0400180C RID: 6156
			private object collection;

			// Token: 0x0400180D RID: 6157
			private object collectionItems;
		}
	}
}
