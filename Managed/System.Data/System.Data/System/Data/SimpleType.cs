using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	internal sealed class SimpleType : ISerializable
	{
		// Token: 0x06000CD5 RID: 3285 RVA: 0x0003B984 File Offset: 0x00039B84
		internal SimpleType(string baseType)
		{
			this._baseType = baseType;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003BA0C File Offset: 0x00039C0C
		internal SimpleType(XmlSchemaSimpleType node)
		{
			this._name = node.Name;
			this._ns = ((node.QualifiedName != null) ? node.QualifiedName.Namespace : "");
			this.LoadTypeValues(node);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003BACC File Offset: 0x00039CCC
		internal void LoadTypeValues(XmlSchemaSimpleType node)
		{
			if (node.Content is XmlSchemaSimpleTypeList || node.Content is XmlSchemaSimpleTypeUnion)
			{
				throw ExceptionBuilder.SimpleTypeNotSupported();
			}
			if (node.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)node.Content;
				XmlSchemaSimpleType xmlSchemaSimpleType = node.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this._baseSimpleType = new SimpleType(node.BaseXmlSchemaType as XmlSchemaSimpleType);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.Name;
				}
				else
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString();
				}
				if (this._baseSimpleType != null && this._baseSimpleType.Name != null && this._baseSimpleType.Name.Length > 0)
				{
					this._xmlBaseType = this._baseSimpleType.XmlBaseType;
				}
				else
				{
					this._xmlBaseType = xmlSchemaSimpleTypeRestriction.BaseTypeName;
				}
				if (this._baseType == null || this._baseType.Length == 0)
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseType.Name;
					this._xmlBaseType = null;
				}
				if (this._baseType == "NOTATION")
				{
					this._baseType = "string";
				}
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet)
					{
						this._length = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						this._minLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
					{
						this._maxLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaPatternFacet)
					{
						this._pattern = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaEnumerationFacet)
					{
						this._enumeration = ((!string.IsNullOrEmpty(this._enumeration)) ? (this._enumeration + " " + xmlSchemaFacet.Value) : xmlSchemaFacet.Value);
					}
					if (xmlSchemaFacet is XmlSchemaMinExclusiveFacet)
					{
						this._minExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMinInclusiveFacet)
					{
						this._minInclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxExclusiveFacet)
					{
						this._maxExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxInclusiveFacet)
					{
						this._maxInclusive = xmlSchemaFacet.Value;
					}
				}
			}
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "targetNamespace");
			if (msdataAttribute != null)
			{
				this._ns = msdataAttribute;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0003BD8C File Offset: 0x00039F8C
		internal bool IsPlainString()
		{
			return XSDSchema.QualifiedName(this._baseType) == XSDSchema.QualifiedName("string") && string.IsNullOrEmpty(this._name) && this._length == -1 && this._minLength == -1 && this._maxLength == -1 && string.IsNullOrEmpty(this._pattern) && string.IsNullOrEmpty(this._maxExclusive) && string.IsNullOrEmpty(this._maxInclusive) && string.IsNullOrEmpty(this._minExclusive) && string.IsNullOrEmpty(this._minInclusive) && string.IsNullOrEmpty(this._enumeration);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0003BE2B File Offset: 0x0003A02B
		internal string BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0003BE33 File Offset: 0x0003A033
		internal XmlQualifiedName XmlBaseType
		{
			get
			{
				return this._xmlBaseType;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0003BE3B File Offset: 0x0003A03B
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0003BE43 File Offset: 0x0003A043
		internal string Namespace
		{
			get
			{
				return this._ns;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0003BE4B File Offset: 0x0003A04B
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0003BE53 File Offset: 0x0003A053
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0003BE5B File Offset: 0x0003A05B
		internal int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0003BE64 File Offset: 0x0003A064
		internal SimpleType BaseSimpleType
		{
			get
			{
				return this._baseSimpleType;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0003BE6C File Offset: 0x0003A06C
		public string SimpleTypeQualifiedName
		{
			get
			{
				if (this._ns.Length == 0)
				{
					return this._name;
				}
				return this._ns + ":" + this._name;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003BE98 File Offset: 0x0003A098
		internal string QualifiedName(string name)
		{
			if (name.IndexOf(':') == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003BEB4 File Offset: 0x0003A0B4
		internal XmlNode ToNode(XmlDocument dc, Hashtable prefixes, bool inRemoting)
		{
			XmlElement xmlElement = dc.CreateElement("xs", "simpleType", "http://www.w3.org/2001/XMLSchema");
			if (this._name != null && this._name.Length != 0)
			{
				xmlElement.SetAttribute("name", this._name);
				if (inRemoting)
				{
					xmlElement.SetAttribute("targetNamespace", "urn:schemas-microsoft-com:xml-msdata", this.Namespace);
				}
			}
			XmlElement xmlElement2 = dc.CreateElement("xs", "restriction", "http://www.w3.org/2001/XMLSchema");
			if (!inRemoting)
			{
				if (this._baseSimpleType != null)
				{
					if (this._baseSimpleType.Namespace != null && this._baseSimpleType.Namespace.Length > 0)
					{
						string text = ((prefixes != null) ? ((string)prefixes[this._baseSimpleType.Namespace]) : null);
						if (text != null)
						{
							xmlElement2.SetAttribute("base", text + ":" + this._baseSimpleType.Name);
						}
						else
						{
							xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
						}
					}
					else
					{
						xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
					}
				}
				else
				{
					xmlElement2.SetAttribute("base", this.QualifiedName(this._baseType));
				}
			}
			else
			{
				xmlElement2.SetAttribute("base", (this._baseSimpleType != null) ? this._baseSimpleType.Name : this.QualifiedName(this._baseType));
			}
			if (this._length >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "length", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._length.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			if (this._maxLength >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "maxLength", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._maxLength.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003C0A3 File Offset: 0x0003A2A3
		internal static SimpleType CreateEnumeratedType(string values)
		{
			return new SimpleType("string")
			{
				_enumeration = values
			};
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003C0B6 File Offset: 0x0003A2B6
		internal static SimpleType CreateByteArrayType(string encoding)
		{
			return new SimpleType("base64Binary");
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0003C0C2 File Offset: 0x0003A2C2
		internal static SimpleType CreateLimitedStringType(int length)
		{
			return new SimpleType("string")
			{
				_maxLength = length
			};
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003C0D5 File Offset: 0x0003A2D5
		internal static SimpleType CreateSimpleType(StorageType typeCode, Type type)
		{
			if (typeCode == StorageType.Char && type == typeof(char))
			{
				return new SimpleType("string")
				{
					_length = 1
				};
			}
			return null;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0003C100 File Offset: 0x0003A300
		internal string HasConflictingDefinition(SimpleType otherSimpleType)
		{
			if (otherSimpleType == null)
			{
				return "otherSimpleType";
			}
			if (this.MaxLength != otherSimpleType.MaxLength)
			{
				return "MaxLength";
			}
			if (!string.Equals(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal))
			{
				return "BaseType";
			}
			if (this.BaseSimpleType != null && otherSimpleType.BaseSimpleType != null && this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0)
			{
				return "BaseSimpleType";
			}
			return string.Empty;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003C178 File Offset: 0x0003A378
		internal bool CanHaveMaxLength()
		{
			SimpleType simpleType = this;
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return string.Equals(simpleType.BaseType, "string", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		internal void ConvertToAnnonymousSimpleType()
		{
			this._name = null;
			this._ns = string.Empty;
			SimpleType simpleType = this;
			while (simpleType._baseSimpleType != null)
			{
				simpleType = simpleType._baseSimpleType;
			}
			this._baseType = simpleType._baseType;
			this._baseSimpleType = simpleType._baseSimpleType;
			this._xmlBaseType = simpleType._xmlBaseType;
		}

		// Token: 0x04000871 RID: 2161
		private string _baseType;

		// Token: 0x04000872 RID: 2162
		private SimpleType _baseSimpleType;

		// Token: 0x04000873 RID: 2163
		private XmlQualifiedName _xmlBaseType;

		// Token: 0x04000874 RID: 2164
		private string _name = string.Empty;

		// Token: 0x04000875 RID: 2165
		private int _length = -1;

		// Token: 0x04000876 RID: 2166
		private int _minLength = -1;

		// Token: 0x04000877 RID: 2167
		private int _maxLength = -1;

		// Token: 0x04000878 RID: 2168
		private string _pattern = string.Empty;

		// Token: 0x04000879 RID: 2169
		private string _ns = string.Empty;

		// Token: 0x0400087A RID: 2170
		private string _maxExclusive = string.Empty;

		// Token: 0x0400087B RID: 2171
		private string _maxInclusive = string.Empty;

		// Token: 0x0400087C RID: 2172
		private string _minExclusive = string.Empty;

		// Token: 0x0400087D RID: 2173
		private string _minInclusive = string.Empty;

		// Token: 0x0400087E RID: 2174
		internal string _enumeration = string.Empty;
	}
}
