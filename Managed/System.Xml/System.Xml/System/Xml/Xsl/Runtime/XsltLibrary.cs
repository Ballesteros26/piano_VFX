using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000628 RID: 1576
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XsltLibrary
	{
		// Token: 0x06003DB8 RID: 15800 RVA: 0x001557FC File Offset: 0x001539FC
		internal XsltLibrary(XmlQueryRuntime runtime)
		{
			this.runtime = runtime;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x0015580C File Offset: 0x00153A0C
		public string FormatMessage(string res, IList<string> args)
		{
			string[] array = new string[args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = args[i];
			}
			return XslTransformException.CreateMessage(res, array);
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x00155844 File Offset: 0x00153A44
		public int CheckScriptNamespace(string nsUri)
		{
			if (this.runtime.ExternalContext.GetLateBoundObject(nsUri) != null)
			{
				throw new XslTransformException("Cannot have both an extension object and a script implementing the same namespace '{0}'.", new string[] { nsUri });
			}
			return 0;
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0015586F File Offset: 0x00153A6F
		public bool ElementAvailable(XmlQualifiedName name)
		{
			return QilGenerator.IsElementAvailable(name);
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00155878 File Offset: 0x00153A78
		public bool FunctionAvailable(XmlQualifiedName name)
		{
			if (this.functionsAvail == null)
			{
				this.functionsAvail = new HybridDictionary();
			}
			else
			{
				object obj = this.functionsAvail[name];
				if (obj != null)
				{
					return (bool)obj;
				}
			}
			bool flag = this.FunctionAvailableHelper(name);
			this.functionsAvail[name] = flag;
			return flag;
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x001558CC File Offset: 0x00153ACC
		private bool FunctionAvailableHelper(XmlQualifiedName name)
		{
			return QilGenerator.IsFunctionAvailable(name.Name, name.Namespace) || (name.Namespace.Length != 0 && !(name.Namespace == "http://www.w3.org/1999/XSL/Transform") && (this.runtime.ExternalContext.LateBoundFunctionExists(name.Name, name.Namespace) || this.runtime.EarlyBoundFunctionExists(name.Name, name.Namespace)));
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x00155946 File Offset: 0x00153B46
		public int RegisterDecimalFormat(XmlQualifiedName name, string infinitySymbol, string nanSymbol, string characters)
		{
			if (this.decimalFormats == null)
			{
				this.decimalFormats = new Dictionary<XmlQualifiedName, DecimalFormat>();
			}
			this.decimalFormats.Add(name, this.CreateDecimalFormat(infinitySymbol, nanSymbol, characters));
			return 0;
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00155974 File Offset: 0x00153B74
		private DecimalFormat CreateDecimalFormat(string infinitySymbol, string nanSymbol, string characters)
		{
			NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
			numberFormatInfo.NumberDecimalSeparator = char.ToString(characters[0]);
			numberFormatInfo.NumberGroupSeparator = char.ToString(characters[1]);
			numberFormatInfo.PositiveInfinitySymbol = infinitySymbol;
			numberFormatInfo.NegativeSign = char.ToString(characters[7]);
			numberFormatInfo.NaNSymbol = nanSymbol;
			numberFormatInfo.PercentSymbol = char.ToString(characters[2]);
			numberFormatInfo.PerMilleSymbol = char.ToString(characters[3]);
			numberFormatInfo.NegativeInfinitySymbol = numberFormatInfo.NegativeSign + numberFormatInfo.PositiveInfinitySymbol;
			return new DecimalFormat(numberFormatInfo, characters[5], characters[4], characters[6]);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00155A21 File Offset: 0x00153C21
		public double RegisterDecimalFormatter(string formatPicture, string infinitySymbol, string nanSymbol, string characters)
		{
			if (this.decimalFormatters == null)
			{
				this.decimalFormatters = new List<DecimalFormatter>();
			}
			this.decimalFormatters.Add(new DecimalFormatter(formatPicture, this.CreateDecimalFormat(infinitySymbol, nanSymbol, characters)));
			return (double)(this.decimalFormatters.Count - 1);
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00155A60 File Offset: 0x00153C60
		public string FormatNumberStatic(double value, double decimalFormatterIndex)
		{
			int num = (int)decimalFormatterIndex;
			return this.decimalFormatters[num].Format(value);
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00155A84 File Offset: 0x00153C84
		public string FormatNumberDynamic(double value, string formatPicture, XmlQualifiedName decimalFormatName, string errorMessageName)
		{
			DecimalFormat decimalFormat;
			if (this.decimalFormats == null || !this.decimalFormats.TryGetValue(decimalFormatName, out decimalFormat))
			{
				throw new XslTransformException("Decimal format '{0}' is not defined.", new string[] { errorMessageName });
			}
			return new DecimalFormatter(formatPicture, decimalFormat).Format(value);
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00155ACC File Offset: 0x00153CCC
		public string NumberFormat(IList<XPathItem> value, string formatString, double lang, string letterValue, string groupingSeparator, double groupingSize)
		{
			return new NumberFormatter(formatString, (int)lang, letterValue, groupingSeparator, (int)groupingSize).FormatSequence(value);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x00155AE3 File Offset: 0x00153CE3
		public int LangToLcid(string lang, bool forwardCompatibility)
		{
			return XsltLibrary.LangToLcidInternal(lang, forwardCompatibility, null);
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x00155AF0 File Offset: 0x00153CF0
		internal static int LangToLcidInternal(string lang, bool forwardCompatibility, IErrorHelper errorHelper)
		{
			int num = 127;
			if (lang != null)
			{
				if (lang.Length == 0)
				{
					if (!forwardCompatibility)
					{
						if (errorHelper == null)
						{
							throw new XslTransformException("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "lang", lang });
						}
						errorHelper.ReportError("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "lang", lang });
					}
				}
				else
				{
					try
					{
						num = new CultureInfo(lang).LCID;
					}
					catch (ArgumentException)
					{
						if (!forwardCompatibility)
						{
							if (errorHelper == null)
							{
								throw new XslTransformException("'{0}' is not a supported language identifier.", new string[] { lang });
							}
							errorHelper.ReportError("'{0}' is not a supported language identifier.", new string[] { lang });
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x00155BA8 File Offset: 0x00153DA8
		private static TypeCode GetTypeCode(XPathItem item)
		{
			Type valueType = item.ValueType;
			if (valueType == XsltConvert.StringType)
			{
				return TypeCode.String;
			}
			if (valueType == XsltConvert.DoubleType)
			{
				return TypeCode.Double;
			}
			return TypeCode.Boolean;
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x00155BDD File Offset: 0x00153DDD
		private static TypeCode WeakestTypeCode(TypeCode typeCode1, TypeCode typeCode2)
		{
			if (typeCode1 >= typeCode2)
			{
				return typeCode2;
			}
			return typeCode1;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x00155BE8 File Offset: 0x00153DE8
		private static bool CompareNumbers(XsltLibrary.ComparisonOperator op, double left, double right)
		{
			switch (op)
			{
			case XsltLibrary.ComparisonOperator.Eq:
				return left == right;
			case XsltLibrary.ComparisonOperator.Ne:
				return left != right;
			case XsltLibrary.ComparisonOperator.Lt:
				return left < right;
			case XsltLibrary.ComparisonOperator.Le:
				return left <= right;
			case XsltLibrary.ComparisonOperator.Gt:
				return left > right;
			default:
				return left >= right;
			}
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x00155C38 File Offset: 0x00153E38
		private static bool CompareValues(XsltLibrary.ComparisonOperator op, XPathItem left, XPathItem right, TypeCode compType)
		{
			if (compType == TypeCode.Double)
			{
				return XsltLibrary.CompareNumbers(op, XsltConvert.ToDouble(left), XsltConvert.ToDouble(right));
			}
			if (compType == TypeCode.String)
			{
				return XsltConvert.ToString(left) == XsltConvert.ToString(right) == (op == XsltLibrary.ComparisonOperator.Eq);
			}
			return XsltConvert.ToBoolean(left) == XsltConvert.ToBoolean(right) == (op == XsltLibrary.ComparisonOperator.Eq);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x00155C90 File Offset: 0x00153E90
		private static bool CompareNodeSetAndValue(XsltLibrary.ComparisonOperator op, IList<XPathNavigator> nodeset, XPathItem val, TypeCode compType)
		{
			if (compType == TypeCode.Boolean)
			{
				return XsltLibrary.CompareNumbers(op, (double)((nodeset.Count != 0) ? 1 : 0), (double)(XsltConvert.ToBoolean(val) ? 1 : 0));
			}
			int count = nodeset.Count;
			for (int i = 0; i < count; i++)
			{
				if (XsltLibrary.CompareValues(op, nodeset[i], val, compType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00155CEC File Offset: 0x00153EEC
		private static bool CompareNodeSetAndNodeSet(XsltLibrary.ComparisonOperator op, IList<XPathNavigator> left, IList<XPathNavigator> right, TypeCode compType)
		{
			int count = left.Count;
			int count2 = right.Count;
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count2; j++)
				{
					if (XsltLibrary.CompareValues(op, left[i], right[j], compType))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x00155D3C File Offset: 0x00153F3C
		public bool EqualityOperator(double opCode, IList<XPathItem> left, IList<XPathItem> right)
		{
			XsltLibrary.ComparisonOperator comparisonOperator = (XsltLibrary.ComparisonOperator)opCode;
			if (XsltLibrary.IsNodeSetOrRtf(left))
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					return XsltLibrary.CompareNodeSetAndNodeSet(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(left), XsltLibrary.ToNodeSetOrRtf(right), TypeCode.String);
				}
				XPathItem xpathItem = right[0];
				return XsltLibrary.CompareNodeSetAndValue(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(left), xpathItem, XsltLibrary.GetTypeCode(xpathItem));
			}
			else
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					XPathItem xpathItem2 = left[0];
					return XsltLibrary.CompareNodeSetAndValue(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(right), xpathItem2, XsltLibrary.GetTypeCode(xpathItem2));
				}
				XPathItem xpathItem3 = left[0];
				XPathItem xpathItem4 = right[0];
				return XsltLibrary.CompareValues(comparisonOperator, xpathItem3, xpathItem4, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem3), XsltLibrary.GetTypeCode(xpathItem4)));
			}
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00155DDD File Offset: 0x00153FDD
		private static XsltLibrary.ComparisonOperator InvertOperator(XsltLibrary.ComparisonOperator op)
		{
			switch (op)
			{
			case XsltLibrary.ComparisonOperator.Lt:
				return XsltLibrary.ComparisonOperator.Gt;
			case XsltLibrary.ComparisonOperator.Le:
				return XsltLibrary.ComparisonOperator.Ge;
			case XsltLibrary.ComparisonOperator.Gt:
				return XsltLibrary.ComparisonOperator.Lt;
			case XsltLibrary.ComparisonOperator.Ge:
				return XsltLibrary.ComparisonOperator.Le;
			default:
				return op;
			}
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x00155E04 File Offset: 0x00154004
		public bool RelationalOperator(double opCode, IList<XPathItem> left, IList<XPathItem> right)
		{
			XsltLibrary.ComparisonOperator comparisonOperator = (XsltLibrary.ComparisonOperator)opCode;
			if (XsltLibrary.IsNodeSetOrRtf(left))
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					return XsltLibrary.CompareNodeSetAndNodeSet(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(left), XsltLibrary.ToNodeSetOrRtf(right), TypeCode.Double);
				}
				XPathItem xpathItem = right[0];
				return XsltLibrary.CompareNodeSetAndValue(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(left), xpathItem, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem), TypeCode.Double));
			}
			else
			{
				if (XsltLibrary.IsNodeSetOrRtf(right))
				{
					XPathItem xpathItem2 = left[0];
					comparisonOperator = XsltLibrary.InvertOperator(comparisonOperator);
					return XsltLibrary.CompareNodeSetAndValue(comparisonOperator, XsltLibrary.ToNodeSetOrRtf(right), xpathItem2, XsltLibrary.WeakestTypeCode(XsltLibrary.GetTypeCode(xpathItem2), TypeCode.Double));
				}
				XPathItem xpathItem3 = left[0];
				XPathItem xpathItem4 = right[0];
				return XsltLibrary.CompareValues(comparisonOperator, xpathItem3, xpathItem4, TypeCode.Double);
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x00155EAC File Offset: 0x001540AC
		public bool IsSameNodeSort(XPathNavigator nav1, XPathNavigator nav2)
		{
			XPathNodeType nodeType = nav1.NodeType;
			XPathNodeType nodeType2 = nav2.NodeType;
			if (XPathNodeType.Text <= nodeType && nodeType <= XPathNodeType.Whitespace)
			{
				return XPathNodeType.Text <= nodeType2 && nodeType2 <= XPathNodeType.Whitespace;
			}
			return nodeType == nodeType2 && Ref.Equal(nav1.LocalName, nav2.LocalName) && Ref.Equal(nav1.NamespaceURI, nav2.NamespaceURI);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		internal static void CheckXsltValue(XPathItem item)
		{
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x00155F08 File Offset: 0x00154108
		[Conditional("DEBUG")]
		internal static void CheckXsltValue(IList<XPathItem> val)
		{
			if (val.Count == 1)
			{
				XsltFunctions.EXslObjectType(val);
				return;
			}
			int count = val.Count;
			int num = 0;
			while (num < count && val[num].IsNode)
			{
				if (num == 1)
				{
					num += Math.Max(count - 4, 0);
				}
				num++;
			}
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00155F57 File Offset: 0x00154157
		private static bool IsNodeSetOrRtf(IList<XPathItem> val)
		{
			return val.Count != 1 || val[0].IsNode;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x00155F70 File Offset: 0x00154170
		private static IList<XPathNavigator> ToNodeSetOrRtf(IList<XPathItem> val)
		{
			return XmlILStorageConverter.ItemsToNavigators(val);
		}

		// Token: 0x04002829 RID: 10281
		private XmlQueryRuntime runtime;

		// Token: 0x0400282A RID: 10282
		private HybridDictionary functionsAvail;

		// Token: 0x0400282B RID: 10283
		private Dictionary<XmlQualifiedName, DecimalFormat> decimalFormats;

		// Token: 0x0400282C RID: 10284
		private List<DecimalFormatter> decimalFormatters;

		// Token: 0x0400282D RID: 10285
		internal const int InvariantCultureLcid = 127;

		// Token: 0x02000629 RID: 1577
		internal enum ComparisonOperator
		{
			// Token: 0x0400282F RID: 10287
			Eq,
			// Token: 0x04002830 RID: 10288
			Ne,
			// Token: 0x04002831 RID: 10289
			Lt,
			// Token: 0x04002832 RID: 10290
			Le,
			// Token: 0x04002833 RID: 10291
			Gt,
			// Token: 0x04002834 RID: 10292
			Ge
		}
	}
}
