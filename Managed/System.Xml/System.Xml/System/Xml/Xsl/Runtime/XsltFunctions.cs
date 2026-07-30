using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000626 RID: 1574
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class XsltFunctions
	{
		// Token: 0x06003D9A RID: 15770 RVA: 0x00154A0C File Offset: 0x00152C0C
		public static bool StartsWith(string s1, string s2)
		{
			return s1.Length >= s2.Length && string.CompareOrdinal(s1, 0, s2, 0, s2.Length) == 0;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x00154A30 File Offset: 0x00152C30
		public static bool Contains(string s1, string s2)
		{
			return XsltFunctions.compareInfo.IndexOf(s1, s2, CompareOptions.Ordinal) >= 0;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x00154A4C File Offset: 0x00152C4C
		public static string SubstringBefore(string s1, string s2)
		{
			if (s2.Length == 0)
			{
				return s2;
			}
			int num = XsltFunctions.compareInfo.IndexOf(s1, s2, CompareOptions.Ordinal);
			if (num >= 1)
			{
				return s1.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00154A88 File Offset: 0x00152C88
		public static string SubstringAfter(string s1, string s2)
		{
			if (s2.Length == 0)
			{
				return s1;
			}
			int num = XsltFunctions.compareInfo.IndexOf(s1, s2, CompareOptions.Ordinal);
			if (num >= 0)
			{
				return s1.Substring(num + s2.Length);
			}
			return string.Empty;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00154AC9 File Offset: 0x00152CC9
		public static string Substring(string value, double startIndex)
		{
			startIndex = XsltFunctions.Round(startIndex);
			if (startIndex <= 0.0)
			{
				return value;
			}
			if (startIndex <= (double)value.Length)
			{
				return value.Substring((int)startIndex - 1);
			}
			return string.Empty;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x00154AFC File Offset: 0x00152CFC
		public static string Substring(string value, double startIndex, double length)
		{
			startIndex = XsltFunctions.Round(startIndex) - 1.0;
			if (startIndex >= (double)value.Length)
			{
				return string.Empty;
			}
			double num = startIndex + XsltFunctions.Round(length);
			startIndex = ((startIndex <= 0.0) ? 0.0 : startIndex);
			if (startIndex < num)
			{
				if (num > (double)value.Length)
				{
					num = (double)value.Length;
				}
				return value.Substring((int)startIndex, (int)(num - startIndex));
			}
			return string.Empty;
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x00154B78 File Offset: 0x00152D78
		public static string NormalizeSpace(string value)
		{
			XmlCharType instance = XmlCharType.Instance;
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < value.Length; i++)
			{
				if (instance.IsWhiteSpace(value[i]))
				{
					if (i == num)
					{
						num++;
					}
					else if (value[i] != ' ' || num2 == i)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(value.Length);
						}
						else
						{
							stringBuilder.Append(' ');
						}
						if (num2 == i)
						{
							stringBuilder.Append(value, num, i - num - 1);
						}
						else
						{
							stringBuilder.Append(value, num, i - num);
						}
						num = i + 1;
					}
					else
					{
						num2 = i + 1;
					}
				}
			}
			if (stringBuilder == null)
			{
				if (num == i)
				{
					return string.Empty;
				}
				if (num == 0 && num2 != i)
				{
					return value;
				}
				stringBuilder = new StringBuilder(value.Length);
			}
			else if (i != num)
			{
				stringBuilder.Append(' ');
			}
			if (num2 == i)
			{
				stringBuilder.Append(value, num, i - num - 1);
			}
			else
			{
				stringBuilder.Append(value, num, i - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x00154C6C File Offset: 0x00152E6C
		public static string Translate(string arg, string mapString, string transString)
		{
			if (mapString.Length == 0)
			{
				return arg;
			}
			StringBuilder stringBuilder = new StringBuilder(arg.Length);
			for (int i = 0; i < arg.Length; i++)
			{
				int num = mapString.IndexOf(arg[i]);
				if (num < 0)
				{
					stringBuilder.Append(arg[i]);
				}
				else if (num < transString.Length)
				{
					stringBuilder.Append(transString[num]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x00154CE0 File Offset: 0x00152EE0
		public static bool Lang(string value, XPathNavigator context)
		{
			string xmlLang = context.XmlLang;
			return xmlLang.StartsWith(value, StringComparison.OrdinalIgnoreCase) && (xmlLang.Length == value.Length || xmlLang[value.Length] == '-');
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00154D20 File Offset: 0x00152F20
		public static double Round(double value)
		{
			double num = Math.Round(value);
			if (value - num != 0.5)
			{
				return num;
			}
			return num + 1.0;
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00154D50 File Offset: 0x00152F50
		public static XPathItem SystemProperty(XmlQualifiedName name)
		{
			if (name.Namespace == "http://www.w3.org/1999/XSL/Transform")
			{
				string name2 = name.Name;
				if (name2 == "version")
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.Double), 1.0);
				}
				if (name2 == "vendor")
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), "Microsoft");
				}
				if (name2 == "vendor-url")
				{
					return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), "http://www.microsoft.com");
				}
			}
			else if (name.Namespace == "urn:schemas-microsoft-com:xslt" && name.Name == "version")
			{
				return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), typeof(XsltLibrary).Assembly.ImageRuntimeVersion);
			}
			return new XmlAtomicValue(XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.String), string.Empty);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x00154E2F File Offset: 0x0015302F
		public static string BaseUri(XPathNavigator navigator)
		{
			return navigator.BaseURI;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x00154E38 File Offset: 0x00153038
		public static string OuterXml(XPathNavigator navigator)
		{
			RtfNavigator rtfNavigator = navigator as RtfNavigator;
			if (rtfNavigator == null)
			{
				return navigator.OuterXml;
			}
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				ConformanceLevel = ConformanceLevel.Fragment,
				CheckCharacters = false
			});
			rtfNavigator.CopyToWriter(xmlWriter);
			xmlWriter.Close();
			return stringBuilder.ToString();
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00154E90 File Offset: 0x00153090
		public static string EXslObjectType(IList<XPathItem> value)
		{
			if (value.Count != 1)
			{
				return "node-set";
			}
			XPathItem xpathItem = value[0];
			if (xpathItem is RtfNavigator)
			{
				return "RTF";
			}
			if (xpathItem.IsNode)
			{
				return "node-set";
			}
			object typedValue = xpathItem.TypedValue;
			if (typedValue is string)
			{
				return "string";
			}
			if (typedValue is double)
			{
				return "number";
			}
			if (typedValue is bool)
			{
				return "boolean";
			}
			return "external";
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00154F08 File Offset: 0x00153108
		public static double MSNumber(IList<XPathItem> value)
		{
			if (value.Count == 0)
			{
				return double.NaN;
			}
			XPathItem xpathItem = value[0];
			string text;
			if (xpathItem.IsNode)
			{
				text = xpathItem.Value;
			}
			else
			{
				Type valueType = xpathItem.ValueType;
				if (valueType == XsltConvert.StringType)
				{
					text = xpathItem.Value;
				}
				else
				{
					if (valueType == XsltConvert.DoubleType)
					{
						return xpathItem.ValueAsDouble;
					}
					if (!xpathItem.ValueAsBoolean)
					{
						return 0.0;
					}
					return 1.0;
				}
			}
			double naN;
			if (XmlConvert.TryToDouble(text, out naN) != null)
			{
				naN = double.NaN;
			}
			return naN;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00154FA4 File Offset: 0x001531A4
		public static string MSFormatDateTime(string dateTime, string format, string lang, bool isDate)
		{
			string text;
			try
			{
				XsdDateTime xsdDateTime;
				if (!XsdDateTime.TryParse(dateTime, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Time | XsdDateTimeFlags.Date | XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear | XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GDay | XsdDateTimeFlags.GMonth | XsdDateTimeFlags.XdrDateTime | XsdDateTimeFlags.XdrTimeNoTz, out xsdDateTime))
				{
					text = string.Empty;
				}
				else
				{
					string name = XsltFunctions.GetCultureInfo(lang).Name;
					DateTime dateTime2 = xsdDateTime.ToZulu();
					if (format.Length == 0)
					{
						format = null;
					}
					text = dateTime2.ToString(format, new CultureInfo(name));
				}
			}
			catch (ArgumentException)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00155014 File Offset: 0x00153214
		public static double MSStringCompare(string s1, string s2, string lang, string options)
		{
			CultureInfo cultureInfo = XsltFunctions.GetCultureInfo(lang);
			CompareOptions compareOptions = CompareOptions.None;
			bool flag = false;
			foreach (char c in options)
			{
				if (c != 'i')
				{
					if (c != 'u')
					{
						flag = true;
						compareOptions = CompareOptions.IgnoreCase;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
				}
			}
			if (flag)
			{
				if (compareOptions != CompareOptions.None)
				{
					throw new XslTransformException("String comparison option(s) '{0}' are either invalid or cannot be used together.", new string[] { options });
				}
				compareOptions = CompareOptions.IgnoreCase;
			}
			int num = cultureInfo.CompareInfo.Compare(s1, s2, compareOptions);
			if (flag && num == 0)
			{
				num = -cultureInfo.CompareInfo.Compare(s1, s2, CompareOptions.None);
			}
			return (double)num;
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x001550AC File Offset: 0x001532AC
		public static string MSUtc(string dateTime)
		{
			XsdDateTime xsdDateTime;
			DateTime dateTime2;
			try
			{
				if (!XsdDateTime.TryParse(dateTime, XsdDateTimeFlags.DateTime | XsdDateTimeFlags.Time | XsdDateTimeFlags.Date | XsdDateTimeFlags.GYearMonth | XsdDateTimeFlags.GYear | XsdDateTimeFlags.GMonthDay | XsdDateTimeFlags.GDay | XsdDateTimeFlags.GMonth | XsdDateTimeFlags.XdrDateTime | XsdDateTimeFlags.XdrTimeNoTz, out xsdDateTime))
				{
					return string.Empty;
				}
				dateTime2 = xsdDateTime.ToZulu();
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			char[] array = "----------T00:00:00.000".ToCharArray();
			switch (xsdDateTime.TypeCode)
			{
			case XmlTypeCode.DateTime:
				XsltFunctions.PrintDate(array, dateTime2);
				XsltFunctions.PrintTime(array, dateTime2);
				break;
			case XmlTypeCode.Time:
				XsltFunctions.PrintTime(array, dateTime2);
				break;
			case XmlTypeCode.Date:
				XsltFunctions.PrintDate(array, dateTime2);
				break;
			case XmlTypeCode.GYearMonth:
				XsltFunctions.PrintYear(array, dateTime2.Year);
				XsltFunctions.ShortToCharArray(array, 5, dateTime2.Month);
				break;
			case XmlTypeCode.GYear:
				XsltFunctions.PrintYear(array, dateTime2.Year);
				break;
			case XmlTypeCode.GMonthDay:
				XsltFunctions.ShortToCharArray(array, 5, dateTime2.Month);
				XsltFunctions.ShortToCharArray(array, 8, dateTime2.Day);
				break;
			case XmlTypeCode.GDay:
				XsltFunctions.ShortToCharArray(array, 8, dateTime2.Day);
				break;
			case XmlTypeCode.GMonth:
				XsltFunctions.ShortToCharArray(array, 5, dateTime2.Month);
				break;
			}
			return new string(array);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x001551D0 File Offset: 0x001533D0
		public static string MSLocalName(string name)
		{
			int num;
			if (ValidateNames.ParseQName(name, 0, out num) != name.Length)
			{
				return string.Empty;
			}
			if (num == 0)
			{
				return name;
			}
			return name.Substring(num + 1);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00155204 File Offset: 0x00153404
		public static string MSNamespaceUri(string name, XPathNavigator currentNode)
		{
			int num;
			if (ValidateNames.ParseQName(name, 0, out num) != name.Length)
			{
				return string.Empty;
			}
			string text = name.Substring(0, num);
			if (text == "xmlns")
			{
				return string.Empty;
			}
			string text2 = currentNode.LookupNamespace(text);
			if (text2 != null)
			{
				return text2;
			}
			if (text == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			return string.Empty;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0015526C File Offset: 0x0015346C
		private static CultureInfo GetCultureInfo(string lang)
		{
			if (lang.Length == 0)
			{
				return CultureInfo.CurrentCulture;
			}
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo(lang);
			}
			catch (ArgumentException)
			{
				throw new XslTransformException("'{0}' is not a supported language identifier.", new string[] { lang });
			}
			return cultureInfo;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x001552B8 File Offset: 0x001534B8
		private static void PrintDate(char[] text, DateTime dt)
		{
			XsltFunctions.PrintYear(text, dt.Year);
			XsltFunctions.ShortToCharArray(text, 5, dt.Month);
			XsltFunctions.ShortToCharArray(text, 8, dt.Day);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x001552E3 File Offset: 0x001534E3
		private static void PrintTime(char[] text, DateTime dt)
		{
			XsltFunctions.ShortToCharArray(text, 11, dt.Hour);
			XsltFunctions.ShortToCharArray(text, 14, dt.Minute);
			XsltFunctions.ShortToCharArray(text, 17, dt.Second);
			XsltFunctions.PrintMsec(text, dt.Millisecond);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0015531F File Offset: 0x0015351F
		private static void PrintYear(char[] text, int value)
		{
			text[0] = (char)(value / 1000 % 10 + 48);
			text[1] = (char)(value / 100 % 10 + 48);
			text[2] = (char)(value / 10 % 10 + 48);
			text[3] = (char)(value / 1 % 10 + 48);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0015535B File Offset: 0x0015355B
		private static void PrintMsec(char[] text, int value)
		{
			if (value == 0)
			{
				return;
			}
			text[20] = (char)(value / 100 % 10 + 48);
			text[21] = (char)(value / 10 % 10 + 48);
			text[22] = (char)(value / 1 % 10 + 48);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0015538D File Offset: 0x0015358D
		private static void ShortToCharArray(char[] text, int start, int value)
		{
			text[start] = (char)(value / 10 + 48);
			text[start + 1] = (char)(value % 10 + 48);
		}

		// Token: 0x04002803 RID: 10243
		private static readonly CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
	}
}
