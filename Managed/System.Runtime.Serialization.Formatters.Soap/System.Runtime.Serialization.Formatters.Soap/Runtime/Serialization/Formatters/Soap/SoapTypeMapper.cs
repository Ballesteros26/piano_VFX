using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap
{
	// Token: 0x0200000F RID: 15
	internal class SoapTypeMapper
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000032B0 File Offset: 0x000014B0
		public SoapTypeMapper(SerializationBinder binder)
		{
			this._binder = binder;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032D4 File Offset: 0x000014D4
		public SoapTypeMapper(XmlTextWriter xmlWriter, FormatterAssemblyStyle assemblyFormat, FormatterTypeStyle typeFormat)
		{
			this._xmlWriter = xmlWriter;
			this._assemblyFormat = assemblyFormat;
			this._prefixNumber = 1L;
			if (typeFormat == FormatterTypeStyle.XsdString)
			{
				this.elementString = new Element("xsd", "string", "http://www.w3.org/2001/XMLSchema");
				return;
			}
			this.elementString = new Element(SoapTypeMapper.SoapEncodingPrefix, "string", SoapTypeMapper.SoapEncodingNamespace);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003348 File Offset: 0x00001548
		static SoapTypeMapper()
		{
			SoapTypeMapper._canBeValueTypeList.Add(typeof(DateTime).ToString());
			SoapTypeMapper._canBeValueTypeList.Add(typeof(TimeSpan).ToString());
			SoapTypeMapper._canBeValueTypeList.Add(typeof(string).ToString());
			SoapTypeMapper._canBeValueTypeList.Add(typeof(decimal).ToString());
			SoapTypeMapper._canBeValueTypeList.Sort();
			SoapTypeMapper.InitMappingTables();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003412 File Offset: 0x00001612
		private static string GetKey(string localName, string namespaceUri)
		{
			return localName + " " + namespaceUri;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003420 File Offset: 0x00001620
		public Type GetType(string xmlName, string xmlNamespace)
		{
			Type type = null;
			string text = XmlConvert.DecodeName(xmlName);
			string text2 = XmlConvert.DecodeName(xmlNamespace);
			string text3;
			string text4;
			SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlNamespace, out text3, out text4);
			string text5 = ((text3 == null || text3 == string.Empty) ? text : (text3 + Type.Delimiter.ToString() + text));
			if (text4 != null && text4 != string.Empty && this._binder != null)
			{
				type = this._binder.BindToType(text4, text5);
			}
			if (type == null)
			{
				string text6 = (string)SoapTypeMapper.xmlNodeToTypeTable[SoapTypeMapper.GetKey(xmlName, xmlNamespace)];
				if (text6 != null)
				{
					type = Type.GetType(text6);
				}
				else
				{
					type = Type.GetType(text5);
					if (type == null)
					{
						if (text4 == null || text4 == string.Empty)
						{
							throw new SerializationException(string.Format("Parse Error, no assembly associated with XML key {0} {1}", text, text2));
						}
						type = FormatterServices.GetTypeFromAssembly(Assembly.Load(text4), text5);
					}
				}
				if (type == null)
				{
					throw new SerializationException();
				}
			}
			return type;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003520 File Offset: 0x00001720
		public Element GetXmlElement(string typeFullName, string assemblyName)
		{
			string text = string.Empty;
			string text2 = typeFullName;
			if (this._assemblyFormat == FormatterAssemblyStyle.Simple)
			{
				assemblyName = assemblyName.Split(new char[] { ',' })[0];
			}
			string text3 = typeFullName + ", " + assemblyName;
			Element element = (Element)SoapTypeMapper.typeToXmlNodeTable[text3];
			if (element == null)
			{
				int num = typeFullName.LastIndexOf('.');
				if (num != -1)
				{
					text = typeFullName.Substring(0, num);
					text2 = typeFullName.Substring(text.Length + 1);
				}
				string text4 = SoapServices.CodeXmlNamespaceForClrTypeNamespace(text, (!assemblyName.StartsWith("mscorlib")) ? assemblyName : string.Empty);
				string text5 = (string)this.namespaceToPrefixTable[text4];
				if (text5 == null || text5 == string.Empty)
				{
					string text6 = "a";
					long prefixNumber = this._prefixNumber;
					this._prefixNumber = prefixNumber + 1L;
					text5 = text6 + prefixNumber.ToString();
					this.namespaceToPrefixTable[text4] = text5;
				}
				int num2 = text2.IndexOf("[");
				if (num2 != -1)
				{
					text2 = XmlConvert.EncodeName(text2.Substring(0, num2)) + text2.Substring(num2);
				}
				else
				{
					int num3 = text2.IndexOf("&");
					if (num3 != -1)
					{
						text2 = XmlConvert.EncodeName(text2.Substring(0, num3)) + text2.Substring(num3);
					}
					else
					{
						text2 = XmlConvert.EncodeName(text2);
					}
				}
				element = new Element(text5, text2, text4);
			}
			return element;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000368C File Offset: 0x0000188C
		public Element GetXmlElement(Type type)
		{
			if (type == typeof(string))
			{
				return this.elementString;
			}
			Element element = (Element)SoapTypeMapper.typeToXmlNodeTable[type.AssemblyQualifiedName];
			if (element == null)
			{
				element = this.GetXmlElement(type.FullName, type.Assembly.FullName);
			}
			else if (this._xmlWriter != null)
			{
				element = new Element((element.Prefix == null) ? this._xmlWriter.LookupPrefix(element.NamespaceURI) : element.Prefix, element.LocalName, element.NamespaceURI);
			}
			if (element == null)
			{
				throw new SerializationException("Oooops");
			}
			return element;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000372F File Offset: 0x0000192F
		private static void RegisterType(Type type, string name, string namspace)
		{
			SoapTypeMapper.RegisterType(type, name, namspace, true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000373C File Offset: 0x0000193C
		private static Element RegisterType(Type type, string name, string namspace, bool reverseMap)
		{
			Element element = new Element(name, namspace);
			SoapTypeMapper.xmlNodeToTypeTable.Add(SoapTypeMapper.GetKey(name, namspace), type.AssemblyQualifiedName);
			if (reverseMap)
			{
				SoapTypeMapper.typeToXmlNodeTable.Add(type.AssemblyQualifiedName, element);
			}
			return element;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003780 File Offset: 0x00001980
		private static void RegisterType(Type type)
		{
			string text = (string)type.GetProperty("XsdType", BindingFlags.Static | BindingFlags.Public).GetValue(null, null);
			Element element = SoapTypeMapper.RegisterType(type, text, "http://www.w3.org/2001/XMLSchema", true);
			element.ParseMethod = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
			if (element.ParseMethod == null)
			{
				throw new InvalidOperationException("Parse method not found in class " + type);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037E8 File Offset: 0x000019E8
		private static void InitMappingTables()
		{
			SoapTypeMapper.RegisterType(typeof(Array), "Array", SoapTypeMapper.SoapEncodingNamespace);
			SoapTypeMapper.RegisterType(typeof(string), "string", "http://www.w3.org/2001/XMLSchema", false);
			SoapTypeMapper.RegisterType(typeof(string), "string", SoapTypeMapper.SoapEncodingNamespace, false);
			SoapTypeMapper.RegisterType(typeof(bool), "boolean", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(sbyte), "byte", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(byte), "unsignedByte", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(long), "long", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(ulong), "unsignedLong", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(int), "int", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(uint), "unsignedInt", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(float), "float", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(double), "double", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(decimal), "decimal", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(short), "short", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(ushort), "unsignedShort", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(object), "anyType", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(DateTime), "dateTime", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(TimeSpan), "duration", "http://www.w3.org/2001/XMLSchema");
			SoapTypeMapper.RegisterType(typeof(SoapFault), "Fault", SoapTypeMapper.SoapEnvelopeNamespace);
			SoapTypeMapper.RegisterType(typeof(byte[]), "base64", SoapTypeMapper.SoapEncodingNamespace);
			SoapTypeMapper.RegisterType(typeof(MethodSignature), "methodSignature", SoapTypeMapper.SoapEncodingNamespace);
			SoapTypeMapper.RegisterType(typeof(SoapAnyUri));
			SoapTypeMapper.RegisterType(typeof(SoapEntity));
			SoapTypeMapper.RegisterType(typeof(SoapMonth));
			SoapTypeMapper.RegisterType(typeof(SoapNonNegativeInteger));
			SoapTypeMapper.RegisterType(typeof(SoapToken));
			SoapTypeMapper.RegisterType(typeof(SoapBase64Binary));
			SoapTypeMapper.RegisterType(typeof(SoapHexBinary));
			SoapTypeMapper.RegisterType(typeof(SoapMonthDay));
			SoapTypeMapper.RegisterType(typeof(SoapNonPositiveInteger));
			SoapTypeMapper.RegisterType(typeof(SoapYear));
			SoapTypeMapper.RegisterType(typeof(SoapDate));
			SoapTypeMapper.RegisterType(typeof(SoapId));
			SoapTypeMapper.RegisterType(typeof(SoapName));
			SoapTypeMapper.RegisterType(typeof(SoapNormalizedString));
			SoapTypeMapper.RegisterType(typeof(SoapYearMonth));
			SoapTypeMapper.RegisterType(typeof(SoapIdref));
			SoapTypeMapper.RegisterType(typeof(SoapNcName));
			SoapTypeMapper.RegisterType(typeof(SoapNotation));
			SoapTypeMapper.RegisterType(typeof(SoapDay));
			SoapTypeMapper.RegisterType(typeof(SoapIdrefs));
			SoapTypeMapper.RegisterType(typeof(SoapNegativeInteger));
			SoapTypeMapper.RegisterType(typeof(SoapPositiveInteger));
			SoapTypeMapper.RegisterType(typeof(SoapInteger));
			SoapTypeMapper.RegisterType(typeof(SoapNmtoken));
			SoapTypeMapper.RegisterType(typeof(SoapQName));
			SoapTypeMapper.RegisterType(typeof(SoapEntities));
			SoapTypeMapper.RegisterType(typeof(SoapLanguage));
			SoapTypeMapper.RegisterType(typeof(SoapNmtokens));
			SoapTypeMapper.RegisterType(typeof(SoapTime));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BBC File Offset: 0x00001DBC
		public static string GetXsdValue(object value)
		{
			if (value is DateTime)
			{
				return SoapDateTime.ToString((DateTime)value);
			}
			if (value is decimal)
			{
				return ((decimal)value).ToString(CultureInfo.InvariantCulture);
			}
			if (value is double)
			{
				return ((double)value).ToString("G17", CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				return ((float)value).ToString("G9", CultureInfo.InvariantCulture);
			}
			if (value is TimeSpan)
			{
				return SoapDuration.ToString((TimeSpan)value);
			}
			if (value is bool)
			{
				if (!(bool)value)
				{
					return "false";
				}
				return "true";
			}
			else
			{
				if (value is MethodSignature)
				{
					return null;
				}
				return value.ToString();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003C7C File Offset: 0x00001E7C
		public static object ParseXsdValue(string value, Type type)
		{
			if (type == typeof(DateTime))
			{
				return SoapDateTime.Parse(value);
			}
			if (type == typeof(decimal))
			{
				return decimal.Parse(value, CultureInfo.InvariantCulture);
			}
			if (type == typeof(double))
			{
				return double.Parse(value, CultureInfo.InvariantCulture);
			}
			if (type == typeof(float))
			{
				return float.Parse(value, CultureInfo.InvariantCulture);
			}
			if (type == typeof(TimeSpan))
			{
				return SoapDuration.Parse(value);
			}
			if (type.IsEnum)
			{
				return Enum.Parse(type, value);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D4A File Offset: 0x00001F4A
		public static bool CanBeValue(Type type)
		{
			return type.IsPrimitive || type.IsEnum || SoapTypeMapper._canBeValueTypeList.BinarySearch(type.ToString()) >= 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D76 File Offset: 0x00001F76
		public bool IsInternalSoapType(Type type)
		{
			return SoapTypeMapper.CanBeValue(type) || typeof(ISoapXsd).IsAssignableFrom(type) || type == typeof(MethodSignature);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003DAC File Offset: 0x00001FAC
		public object ReadInternalSoapValue(SoapReader reader, Type type)
		{
			if (SoapTypeMapper.CanBeValue(type))
			{
				return SoapTypeMapper.ParseXsdValue(reader.XmlReader.ReadElementString(), type);
			}
			if (type == typeof(MethodSignature))
			{
				return MethodSignature.ReadXmlValue(reader);
			}
			string text = reader.XmlReader.ReadElementString();
			Element xmlElement = this.GetXmlElement(type);
			if (xmlElement.ParseMethod != null)
			{
				return xmlElement.ParseMethod.Invoke(null, new object[] { text });
			}
			throw new SerializationException("Can't parse type " + type);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003E35 File Offset: 0x00002035
		public string GetInternalSoapValue(SoapWriter writer, object value)
		{
			if (SoapTypeMapper.CanBeValue(value.GetType()))
			{
				return SoapTypeMapper.GetXsdValue(value);
			}
			if (value is MethodSignature)
			{
				return ((MethodSignature)value).GetXmlValue(writer);
			}
			return value.ToString();
		}

		// Token: 0x04000046 RID: 70
		private static Hashtable xmlNodeToTypeTable = new Hashtable();

		// Token: 0x04000047 RID: 71
		private static Hashtable typeToXmlNodeTable = new Hashtable();

		// Token: 0x04000048 RID: 72
		public static readonly string SoapEncodingNamespace = "http://schemas.xmlsoap.org/soap/encoding/";

		// Token: 0x04000049 RID: 73
		public static readonly string SoapEncodingPrefix = "SOAP-ENC";

		// Token: 0x0400004A RID: 74
		public static readonly string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x0400004B RID: 75
		public static readonly string SoapEnvelopePrefix = "SOAP-ENV";

		// Token: 0x0400004C RID: 76
		private XmlTextWriter _xmlWriter;

		// Token: 0x0400004D RID: 77
		private long _prefixNumber;

		// Token: 0x0400004E RID: 78
		private Hashtable namespaceToPrefixTable = new Hashtable();

		// Token: 0x0400004F RID: 79
		private SerializationBinder _binder;

		// Token: 0x04000050 RID: 80
		private static ArrayList _canBeValueTypeList = new ArrayList();

		// Token: 0x04000051 RID: 81
		private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

		// Token: 0x04000052 RID: 82
		private Element elementString;
	}
}
