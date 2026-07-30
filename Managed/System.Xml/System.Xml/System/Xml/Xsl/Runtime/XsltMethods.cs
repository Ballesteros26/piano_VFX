using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000627 RID: 1575
	internal static class XsltMethods
	{
		// Token: 0x06003DB5 RID: 15797 RVA: 0x001553B8 File Offset: 0x001535B8
		public static MethodInfo GetMethod(Type className, string methName)
		{
			return className.GetMethod(methName);
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x001553C1 File Offset: 0x001535C1
		public static MethodInfo GetMethod(Type className, string methName, params Type[] args)
		{
			return className.GetMethod(methName, args);
		}

		// Token: 0x04002804 RID: 10244
		public static readonly MethodInfo FormatMessage = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatMessage");

		// Token: 0x04002805 RID: 10245
		public static readonly MethodInfo EnsureNodeSet = XsltMethods.GetMethod(typeof(XsltConvert), "EnsureNodeSet", new Type[] { typeof(IList<XPathItem>) });

		// Token: 0x04002806 RID: 10246
		public static readonly MethodInfo EqualityOperator = XsltMethods.GetMethod(typeof(XsltLibrary), "EqualityOperator");

		// Token: 0x04002807 RID: 10247
		public static readonly MethodInfo RelationalOperator = XsltMethods.GetMethod(typeof(XsltLibrary), "RelationalOperator");

		// Token: 0x04002808 RID: 10248
		public static readonly MethodInfo StartsWith = XsltMethods.GetMethod(typeof(XsltFunctions), "StartsWith");

		// Token: 0x04002809 RID: 10249
		public static readonly MethodInfo Contains = XsltMethods.GetMethod(typeof(XsltFunctions), "Contains");

		// Token: 0x0400280A RID: 10250
		public static readonly MethodInfo SubstringBefore = XsltMethods.GetMethod(typeof(XsltFunctions), "SubstringBefore");

		// Token: 0x0400280B RID: 10251
		public static readonly MethodInfo SubstringAfter = XsltMethods.GetMethod(typeof(XsltFunctions), "SubstringAfter");

		// Token: 0x0400280C RID: 10252
		public static readonly MethodInfo Substring2 = XsltMethods.GetMethod(typeof(XsltFunctions), "Substring", new Type[]
		{
			typeof(string),
			typeof(double)
		});

		// Token: 0x0400280D RID: 10253
		public static readonly MethodInfo Substring3 = XsltMethods.GetMethod(typeof(XsltFunctions), "Substring", new Type[]
		{
			typeof(string),
			typeof(double),
			typeof(double)
		});

		// Token: 0x0400280E RID: 10254
		public static readonly MethodInfo NormalizeSpace = XsltMethods.GetMethod(typeof(XsltFunctions), "NormalizeSpace");

		// Token: 0x0400280F RID: 10255
		public static readonly MethodInfo Translate = XsltMethods.GetMethod(typeof(XsltFunctions), "Translate");

		// Token: 0x04002810 RID: 10256
		public static readonly MethodInfo Lang = XsltMethods.GetMethod(typeof(XsltFunctions), "Lang");

		// Token: 0x04002811 RID: 10257
		public static readonly MethodInfo Floor = XsltMethods.GetMethod(typeof(Math), "Floor", new Type[] { typeof(double) });

		// Token: 0x04002812 RID: 10258
		public static readonly MethodInfo Ceiling = XsltMethods.GetMethod(typeof(Math), "Ceiling", new Type[] { typeof(double) });

		// Token: 0x04002813 RID: 10259
		public static readonly MethodInfo Round = XsltMethods.GetMethod(typeof(XsltFunctions), "Round");

		// Token: 0x04002814 RID: 10260
		public static readonly MethodInfo SystemProperty = XsltMethods.GetMethod(typeof(XsltFunctions), "SystemProperty");

		// Token: 0x04002815 RID: 10261
		public static readonly MethodInfo BaseUri = XsltMethods.GetMethod(typeof(XsltFunctions), "BaseUri");

		// Token: 0x04002816 RID: 10262
		public static readonly MethodInfo OuterXml = XsltMethods.GetMethod(typeof(XsltFunctions), "OuterXml");

		// Token: 0x04002817 RID: 10263
		public static readonly MethodInfo OnCurrentNodeChanged = XsltMethods.GetMethod(typeof(XmlQueryRuntime), "OnCurrentNodeChanged");

		// Token: 0x04002818 RID: 10264
		public static readonly MethodInfo MSFormatDateTime = XsltMethods.GetMethod(typeof(XsltFunctions), "MSFormatDateTime");

		// Token: 0x04002819 RID: 10265
		public static readonly MethodInfo MSStringCompare = XsltMethods.GetMethod(typeof(XsltFunctions), "MSStringCompare");

		// Token: 0x0400281A RID: 10266
		public static readonly MethodInfo MSUtc = XsltMethods.GetMethod(typeof(XsltFunctions), "MSUtc");

		// Token: 0x0400281B RID: 10267
		public static readonly MethodInfo MSNumber = XsltMethods.GetMethod(typeof(XsltFunctions), "MSNumber");

		// Token: 0x0400281C RID: 10268
		public static readonly MethodInfo MSLocalName = XsltMethods.GetMethod(typeof(XsltFunctions), "MSLocalName");

		// Token: 0x0400281D RID: 10269
		public static readonly MethodInfo MSNamespaceUri = XsltMethods.GetMethod(typeof(XsltFunctions), "MSNamespaceUri");

		// Token: 0x0400281E RID: 10270
		public static readonly MethodInfo EXslObjectType = XsltMethods.GetMethod(typeof(XsltFunctions), "EXslObjectType");

		// Token: 0x0400281F RID: 10271
		public static readonly MethodInfo CheckScriptNamespace = XsltMethods.GetMethod(typeof(XsltLibrary), "CheckScriptNamespace");

		// Token: 0x04002820 RID: 10272
		public static readonly MethodInfo FunctionAvailable = XsltMethods.GetMethod(typeof(XsltLibrary), "FunctionAvailable");

		// Token: 0x04002821 RID: 10273
		public static readonly MethodInfo ElementAvailable = XsltMethods.GetMethod(typeof(XsltLibrary), "ElementAvailable");

		// Token: 0x04002822 RID: 10274
		public static readonly MethodInfo RegisterDecimalFormat = XsltMethods.GetMethod(typeof(XsltLibrary), "RegisterDecimalFormat");

		// Token: 0x04002823 RID: 10275
		public static readonly MethodInfo RegisterDecimalFormatter = XsltMethods.GetMethod(typeof(XsltLibrary), "RegisterDecimalFormatter");

		// Token: 0x04002824 RID: 10276
		public static readonly MethodInfo FormatNumberStatic = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatNumberStatic");

		// Token: 0x04002825 RID: 10277
		public static readonly MethodInfo FormatNumberDynamic = XsltMethods.GetMethod(typeof(XsltLibrary), "FormatNumberDynamic");

		// Token: 0x04002826 RID: 10278
		public static readonly MethodInfo IsSameNodeSort = XsltMethods.GetMethod(typeof(XsltLibrary), "IsSameNodeSort");

		// Token: 0x04002827 RID: 10279
		public static readonly MethodInfo LangToLcid = XsltMethods.GetMethod(typeof(XsltLibrary), "LangToLcid");

		// Token: 0x04002828 RID: 10280
		public static readonly MethodInfo NumberFormat = XsltMethods.GetMethod(typeof(XsltLibrary), "NumberFormat");
	}
}
