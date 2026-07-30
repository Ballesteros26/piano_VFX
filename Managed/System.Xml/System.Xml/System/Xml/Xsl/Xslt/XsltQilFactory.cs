using System;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005B3 RID: 1459
	internal class XsltQilFactory : XPathQilFactory
	{
		// Token: 0x06003A02 RID: 14850 RVA: 0x00148156 File Offset: 0x00146356
		public XsltQilFactory(QilFactory f, bool debug)
			: base(f, debug)
		{
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x00148160 File Offset: 0x00146360
		[Conditional("DEBUG")]
		public void CheckXsltType(QilNode n)
		{
			XmlTypeCode typeCode = n.XmlType.TypeCode;
			if (typeCode <= XmlTypeCode.Boolean)
			{
				if (typeCode > XmlTypeCode.Item)
				{
					int num = typeCode - XmlTypeCode.String;
					return;
				}
			}
			else if (typeCode != XmlTypeCode.Double)
			{
			}
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		public void CheckQName(QilNode n)
		{
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x00148194 File Offset: 0x00146394
		public QilNode DefaultValueMarker()
		{
			return base.QName("default-value", "urn:schemas-microsoft-com:xslt-debug");
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x001481A6 File Offset: 0x001463A6
		public QilNode IsDefaultValueMarker(QilNode n)
		{
			return base.IsType(n, XmlQueryTypeFactory.QNameX);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x001481B4 File Offset: 0x001463B4
		public QilNode InvokeIsSameNodeSort(QilNode n1, QilNode n2)
		{
			return base.XsltInvokeEarlyBound(base.QName("is-same-node-sort"), XsltMethods.IsSameNodeSort, XmlQueryTypeFactory.BooleanX, new QilNode[] { n1, n2 });
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x001481DF File Offset: 0x001463DF
		public QilNode InvokeSystemProperty(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("system-property"), XsltMethods.SystemProperty, XmlQueryTypeFactory.Choice(XmlQueryTypeFactory.DoubleX, XmlQueryTypeFactory.StringX), new QilNode[] { n });
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x00148210 File Offset: 0x00146410
		public QilNode InvokeElementAvailable(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("element-available"), XsltMethods.ElementAvailable, XmlQueryTypeFactory.BooleanX, new QilNode[] { n });
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x00148238 File Offset: 0x00146438
		public QilNode InvokeCheckScriptNamespace(string nsUri)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-script-namespace"), XsltMethods.CheckScriptNamespace, XmlQueryTypeFactory.IntX, new QilNode[] { base.String(nsUri) });
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x00148270 File Offset: 0x00146470
		public QilNode InvokeFunctionAvailable(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("function-available"), XsltMethods.FunctionAvailable, XmlQueryTypeFactory.BooleanX, new QilNode[] { n });
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x00148297 File Offset: 0x00146497
		public QilNode InvokeBaseUri(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("base-uri"), XsltMethods.BaseUri, XmlQueryTypeFactory.StringX, new QilNode[] { n });
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x001482BE File Offset: 0x001464BE
		public QilNode InvokeOnCurrentNodeChanged(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("on-current-node-changed"), XsltMethods.OnCurrentNodeChanged, XmlQueryTypeFactory.IntX, new QilNode[] { n });
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x001482E8 File Offset: 0x001464E8
		public QilNode InvokeLangToLcid(QilNode n, bool fwdCompat)
		{
			return base.XsltInvokeEarlyBound(base.QName("lang-to-lcid"), XsltMethods.LangToLcid, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				n,
				base.Boolean(fwdCompat)
			});
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x00148324 File Offset: 0x00146524
		public QilNode InvokeNumberFormat(QilNode value, QilNode format, QilNode lang, QilNode letterValue, QilNode groupingSeparator, QilNode groupingSize)
		{
			return base.XsltInvokeEarlyBound(base.QName("number-format"), XsltMethods.NumberFormat, XmlQueryTypeFactory.StringX, new QilNode[] { value, format, lang, letterValue, groupingSeparator, groupingSize });
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x00148364 File Offset: 0x00146564
		public QilNode InvokeRegisterDecimalFormat(DecimalFormatDecl format)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-decimal-format"), XsltMethods.RegisterDecimalFormat, XmlQueryTypeFactory.IntX, new QilNode[]
			{
				base.QName(format.Name.Name, format.Name.Namespace),
				base.String(format.InfinitySymbol),
				base.String(format.NanSymbol),
				base.String(new string(format.Characters))
			});
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x001483E4 File Offset: 0x001465E4
		public QilNode InvokeRegisterDecimalFormatter(QilNode formatPicture, DecimalFormatDecl format)
		{
			return base.XsltInvokeEarlyBound(base.QName("register-decimal-formatter"), XsltMethods.RegisterDecimalFormatter, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				formatPicture,
				base.String(format.InfinitySymbol),
				base.String(format.NanSymbol),
				base.String(new string(format.Characters))
			});
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x00148448 File Offset: 0x00146648
		public QilNode InvokeFormatNumberStatic(QilNode value, QilNode decimalFormatIndex)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-number-static"), XsltMethods.FormatNumberStatic, XmlQueryTypeFactory.StringX, new QilNode[] { value, decimalFormatIndex });
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00148473 File Offset: 0x00146673
		public QilNode InvokeFormatNumberDynamic(QilNode value, QilNode formatPicture, QilNode decimalFormatName, QilNode errorMessageName)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-number-dynamic"), XsltMethods.FormatNumberDynamic, XmlQueryTypeFactory.StringX, new QilNode[] { value, formatPicture, decimalFormatName, errorMessageName });
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x001484A7 File Offset: 0x001466A7
		public QilNode InvokeOuterXml(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("outer-xml"), XsltMethods.OuterXml, XmlQueryTypeFactory.StringX, new QilNode[] { n });
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x001484CE File Offset: 0x001466CE
		public QilNode InvokeMsFormatDateTime(QilNode datetime, QilNode format, QilNode lang, QilNode isDate)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:format-date-time"), XsltMethods.MSFormatDateTime, XmlQueryTypeFactory.StringX, new QilNode[] { datetime, format, lang, isDate });
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x00148502 File Offset: 0x00146702
		public QilNode InvokeMsStringCompare(QilNode x, QilNode y, QilNode lang, QilNode options)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:string-compare"), XsltMethods.MSStringCompare, XmlQueryTypeFactory.DoubleX, new QilNode[] { x, y, lang, options });
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00148536 File Offset: 0x00146736
		public QilNode InvokeMsUtc(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:utc"), XsltMethods.MSUtc, XmlQueryTypeFactory.StringX, new QilNode[] { n });
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0014855D File Offset: 0x0014675D
		public QilNode InvokeMsNumber(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:number"), XsltMethods.MSNumber, XmlQueryTypeFactory.DoubleX, new QilNode[] { n });
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x00148584 File Offset: 0x00146784
		public QilNode InvokeMsLocalName(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:local-name"), XsltMethods.MSLocalName, XmlQueryTypeFactory.StringX, new QilNode[] { n });
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x001485AB File Offset: 0x001467AB
		public QilNode InvokeMsNamespaceUri(QilNode n, QilNode currentNode)
		{
			return base.XsltInvokeEarlyBound(base.QName("ms:namespace-uri"), XsltMethods.MSNamespaceUri, XmlQueryTypeFactory.StringX, new QilNode[] { n, currentNode });
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x001485D6 File Offset: 0x001467D6
		public QilNode InvokeEXslObjectType(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("exsl:object-type"), XsltMethods.EXslObjectType, XmlQueryTypeFactory.StringX, new QilNode[] { n });
		}
	}
}
